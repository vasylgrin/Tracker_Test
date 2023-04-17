using System.Runtime.CompilerServices;
using Tracker.WebAPI.Models;

namespace Tracker.WebAPI.Extentions
{
    public static class ClearTrackLocationInCollectionExtentions
    {
        public static List<Walk> ClearTrackLocationInCollection(this IEnumerable<Walk> walks)
        {
            foreach (var walk in walks)
            {
                walk.TrackLocationCollection.Clear();
            }
            return walks.ToList();
        }

        public static List<WalkingDay> ClearTrackLocationInCollection(this IEnumerable<WalkingDay> walkingDays)
        {
            foreach (var walking in walkingDays)
            {
                foreach(var walk in walking.WalksInADay)
                {
                    walk.TrackLocationCollection.Clear();
                }
            }
            return walkingDays.ToList();
        }
    }
}
