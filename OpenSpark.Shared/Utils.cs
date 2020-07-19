﻿using System;
using System.Text;

namespace OpenSpark.Shared
{
    public static class Utils
    {
        private static readonly char[] Characters = new string(
            "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz-_")
            .ToCharArray();

        public static string GenerateRandomId(string entityName)
        {
            var id = new StringBuilder((entityName.Length + 1) + 11);
            var random = new Random();

            id.AppendFormat("{0}/", entityName.ToLower());
            for (var i = 0; i < 11; i++)
            {
                var randomCharacter = Characters[random.Next(Characters.Length)];
                id.Append(randomCharacter);
            }

            return id.ToString();
        }

        // Removes the entity name portion of the RavenID to return just the raw
        // 11 character ID for use on the client side.
        public static string ConvertToEntityId(this string ravenId) => ravenId.Split("/")[1];

        public static string ConvertToRavenId<T>(this string entityId) => $"{typeof(T).Name.ToLower()}/{entityId}";

        public static string ConvertToHumanFriendlyFormat(this DateTime dateTime)
        {
            const int secondsPerMinute = 60;
            const int secondsPerHour = 60 * secondsPerMinute;
            const int secondsPerDay = 24 * secondsPerHour;
            const int secondsPerMonth = 30 * secondsPerDay;

            var ts = new TimeSpan(DateTime.UtcNow.Ticks - dateTime.Ticks);
            var delta = Math.Abs(ts.TotalSeconds);

            if (delta < secondsPerMinute)
                return ts.Seconds == 1 ? "one second ago" : ts.Seconds + " seconds ago";

            if (delta < 2 * secondsPerMinute)
                return "a minute ago";

            if (delta < 60 * secondsPerMinute)
                return ts.Minutes + " minutes ago";

            if (delta < 120 * secondsPerMinute)
                return "an hour ago";

            if (delta < 24 * secondsPerHour)
                return ts.Hours + " hours ago";

            if (delta < 48 * secondsPerHour)
                return "yesterday";

            if (delta < 30 * secondsPerDay)
                return ts.Days + " days ago";

            if (delta < 12 * secondsPerMonth)
            {
                var months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "one month ago" : months + " months ago";
            }

            var years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
            return years <= 1 ? "one year ago" : years + " years ago";
        }
    }
}