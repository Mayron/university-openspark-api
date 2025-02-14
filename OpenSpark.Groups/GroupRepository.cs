﻿using System.Linq;
using OpenSpark.Groups.Domain;

namespace OpenSpark.Groups
{
    public class GroupRepository
    {
        public Member GetGroupMemberByAuthUserId(string userId, string ravenGroupId)
        {
            using var session = DocumentStoreSingleton.Store.OpenSession();

            return session.Query<Member>().SingleOrDefault(
                g => g.AuthUserId == userId && g.GroupId == ravenGroupId);
        }
    }
}