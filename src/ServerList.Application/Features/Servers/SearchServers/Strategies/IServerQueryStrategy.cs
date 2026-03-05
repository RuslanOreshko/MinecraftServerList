using ServerList.Application.Features.Server.SearchServers;
using ServerList.Domain.Entities;

namespace ServerList.Application.Features.Server.SearchServers.Strategies;


public interface IServerQueryStrategy
{
    IQueryable<GameServer> Apply(IQueryable<GameServer> query, ServerSearchFilter filter);
}