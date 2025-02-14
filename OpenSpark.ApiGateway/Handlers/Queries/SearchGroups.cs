﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OpenSpark.ApiGateway.Builders;
using OpenSpark.ApiGateway.Services;
using OpenSpark.Shared;
using OpenSpark.Shared.Queries;

namespace OpenSpark.ApiGateway.Handlers.Queries
{
    public class SearchGroups
    {
        public class Query : IRequest<Unit>
        {
            public string ConnectionId { get; }
            public string Callback { get; }
            public string SearchQuery { get; }

            public Query(string connectionId, string callback, string searchQuery)
            {
                ConnectionId = connectionId;
                Callback = callback;
                SearchQuery = searchQuery;
            }
        }

        public class Handler : IRequestHandler<Query, Unit>
        {
            private readonly IActorSystemService _actorSystemService;
            private readonly IMessageContextBuilderFactory _builderFactory;

            public Handler(IActorSystemService actorSystem, IMessageContextBuilderFactory builder)
            {
                _actorSystemService = actorSystem;
                _builderFactory = builder;
            }

            public Task<Unit> Handle(Query query, CancellationToken cancellationToken)
            {
                var context = _builderFactory.CreateQueryContext(new SearchGroupsQuery { SearchQuery = query.SearchQuery })
                    .SetClientCallback(query.Callback, query.ConnectionId)
                    .ForRemoteSystem(RemoteSystem.Groups)
                    .Build();

                _actorSystemService.SendQuery(context);

                return Unit.Task;
            }
        }
    }
}