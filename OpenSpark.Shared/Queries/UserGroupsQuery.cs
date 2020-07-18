﻿using OpenSpark.Domain;

namespace OpenSpark.Shared.Queries
{
    public class UserGroupsQuery : IQuery
    {
        public string ConnectionId { get; set; }
        public User User { get; set; }
        public string Callback { get; set; }
        public bool OwnedGroups { get; set; }
        public bool Memberships { get; set; }
    }
}
