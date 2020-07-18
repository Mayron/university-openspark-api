﻿using Akka.Actor;
using OpenSpark.Domain;
using OpenSpark.Shared.Events.Payloads;
using OpenSpark.Shared.Queries;
using Raven.Client.Documents;
using Raven.Client.Documents.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenSpark.Projects.Actors
{
    public class UserProjectsActor : ReceiveActor
    {
        public UserProjectsActor()
        {
            Receive<UserProjectsQuery>(query =>
            {
                using var session = DocumentStoreSingleton.Store.OpenSession();

                List<Project> groups;
                var userId = query.User.AuthUserId;

                if (query.OwnedProjects)
                {
                    groups = session.Query<Project>()
                        .Where(g => g.OwnerUserId == userId)
                        .OrderByDescending(g => g.Subscribers.Count)
                        .ToList();
                }
                else if (query.Subscriptions)
                {
                    // We do not want projects we own as they go into their own "Projects" section.
                    groups = session.Query<Project>()
                        .Where(p => p.OwnerUserId != userId && userId.In(p.Subscribers))
                        .OrderByDescending(g => g.Subscribers.Count)
                        .ToList();
                }
                else
                {
                    throw new ActorKilledException("Invalid query request");
                }

                Sender.Tell(new PayloadEvent
                {
                    ConnectionId = query.ConnectionId,
                    Callback = query.Callback,
                    Payload = groups
                });

                Self.GracefulStop(TimeSpan.FromSeconds(5));
            });
        }
    }
}