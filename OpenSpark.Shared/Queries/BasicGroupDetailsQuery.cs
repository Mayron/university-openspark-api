﻿using OpenSpark.Domain;

namespace OpenSpark.Shared.Queries
{
    public class BasicGroupDetailsQuery : IQuery
    {
        public string ConnectionId { get; set; }
        public User User { get; set; }
        public string Callback { get; set; }
        public string GroupId { get; set; }
    }
}
