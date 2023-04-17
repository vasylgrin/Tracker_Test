namespace Tracker.WebAPI.Models
{
    public sealed class Walk
    {
        public List<TrackLocation> TrackLocationCollection { get; set; } = new List<TrackLocation>();
        public double KilometersWalked { get; set; }
        public TimeSpan DurationOfWalk { get; set; }

        public Walk()
        {
            
        }

        public Walk(List<TrackLocation> trackLocationCollection)
        {
            TrackLocationCollection = trackLocationCollection ?? throw new ArgumentNullException(nameof(trackLocationCollection));
        }
    }
}
