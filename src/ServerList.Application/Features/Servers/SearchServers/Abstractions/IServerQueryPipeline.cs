using ServerList.Domain.Entities;

namespace ServerList.Application.Features.Server.SearchServers.Abstractions;


public interface IServerQueryPipeline
{
    IQueryable<GameServer> ApplyAll(IQueryable<GameServer> query, ServerSearchFilter filter);
}

