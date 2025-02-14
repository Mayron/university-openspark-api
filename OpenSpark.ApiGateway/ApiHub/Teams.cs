﻿// ReSharper disable UnusedMember.Global

using Microsoft.AspNetCore.Authorization;
using OpenSpark.ApiGateway.Handlers.Queries.Teams;

namespace OpenSpark.ApiGateway.ApiHub
{
    public partial class ApiHub
    {
        [Authorize]
        public void FetchTeams(string callback, string projectId) =>
            _mediator.Send(new FetchTeams.Query(Context.ConnectionId, callback, projectId));

        [Authorize]
        public void FetchTeamMembers(string callback, string teamId) =>
            _mediator.Send(new FetchTeamMembers.Query(Context.ConnectionId, callback, teamId));

        [Authorize]
        public void FetchTeamPermissions(string callback, string teamId) =>
            _mediator.Send(new FetchTeamPermissions.Query(Context.ConnectionId, callback, teamId));
    }
}