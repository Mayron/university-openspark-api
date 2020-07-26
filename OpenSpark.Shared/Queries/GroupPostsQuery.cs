﻿using System.Collections.Generic;
using OpenSpark.Domain;

namespace OpenSpark.Shared.Queries
{
    public class GroupPostsQuery : IQuery
    {
        public User User { get; set; }
        public QueryMetaData MetaData { get; set; }
        public string GroupId { get; set; }
        public List<string> Seen { get; set; }
        public string PostId { get; set; }
    }
}