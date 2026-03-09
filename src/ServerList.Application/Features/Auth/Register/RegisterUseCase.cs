using ServerList.Application.Abstractions.Persistance;
using ServerList.Application.Abstractions.Security;
using ServerList.Domain.Entities;

namespace ServerList.Application.Features.Auth.Register;


public sealed class RegisterUseCase : IRegisterUseCase
{
    private readonly IUserRepository _users;
    private readonly IRoleRepository _roles;
    private readonly IUnitOfWork _uow;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterUseCase(
        IUserRepository users,
        IRoleRepository roles,
        IUnitOfWork uow,
        IPasswordHasher passwordHasher
    )
    {
        _users = users;
        _roles = roles;
        _uow = uow;
        _passwordHasher = passwordHasher;
    }

    public async Task<RegisterResult> ExecuteAsync(RegisterRequest request, CancellationToken ct)
    {
        var emailExists = await _users.GetByEmailAsync(request.Email, ct);

        if (emailExists != null)
            throw new Exception("Email alredy user.");

        var userNameExists = await _users.GetByUserNameAsync(request.UserName, ct);

        if (userNameExists != null)
            throw new Exception("Username alredy user.");

        var passwordHash = _passwordHasher.Hash(request.Password);

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            UserName = request.UserName,
            PasswordHash = passwordHash,
            CreatedAt = DateTime.UtcNow,
            IsBlocked = false
        };

        await _users.AddAsync(user, ct);

        var role = await _roles.GetByNameAsync("User", ct);

        if(role == null)
            throw new Exception("Role user not found");

        user.UserRoles.Add(new UserRole
        {
            UserId = user.Id,
            RoleId = role.Id
        });

        await _uow.SaveChangesAsync(ct);

        return new RegisterResult(
            user.Id,
            user.Email,
            user.UserName,
            user.CreatedAt
        );
    }

}