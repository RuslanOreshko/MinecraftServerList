using ServerList.Application.Features.Server.SearchServers.Abstractions;
using ServerList.Application.Features.Server.SearchServers.Strategies;
using ServerList.Domain.Entities;

namespace ServerList.Application.Features.Server.SearchServers;


public class ServerQueryPipeline : IServerQueryPipeline
{
    private readonly IEnumerable<IServerQueryStrategy> _strategies;

    public ServerQueryPipeline(IEnumerable<IServerQueryStrategy> strategies)
    {
        _strategies = strategies;
    }

    public IQueryable<GameServer> ApplyAll(IQueryable<GameServer> query, ServerSearchFilter filter)
    {
        foreach(var s in _strategies)
            query = s.Apply(query, filter);

        return query;
    }
}
