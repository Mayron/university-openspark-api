﻿using MediatR;
using Microsoft.AspNetCore.Http;
using OpenSpark.ApiGateway.Extensions;
using OpenSpark.ApiGateway.Services;
using OpenSpark.Domain;
using OpenSpark.Shared.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace OpenSpark.ApiGateway.Handlers
{
    public class FetchNewsFeed
    {
        public class Query : IRequest
        {
            public string ConnectionId { get; }
            public string Callback { get; }

            public Query(string connectionId, string callback)
            {
                ConnectionId = connectionId;
                Callback = callback;
            }
        }

        public class Handler : IRequestHandler<Query>
        {
            private readonly IActorSystemService _actorSystemService;
            private readonly User _user;

            public Handler(IActorSystemService actorSystemService, IHttpContextAccessor context)
            {
                _actorSystemService = actorSystemService;
                _user = context.GetFirebaseUser();
            }

            public async Task<Unit> Handle(Query query, CancellationToken cancellationToken)
            {
                _actorSystemService.SendDiscussionsMessage(new NewsFeedQuery
                {
                    ConnectionId = query.ConnectionId,
                    Callback = query.Callback,
                    User = _user
                });

                return Unit.Value;
            }
        }
    }
}