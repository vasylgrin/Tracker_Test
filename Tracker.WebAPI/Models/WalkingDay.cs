namespace Tracker.WebAPI.Models
{
    public sealed class WalkingDay
    {
        public string IMEI { get; set; }
        public List<Walk> WalksInADay { get; set; } = new List<Walk>();
        public double KilometersOfWalkedInADay { get; set; }
        public TimeSpan DurationOfWalksInADay { get; set; }

        public WalkingDay()
        {
            
        }

        public WalkingDay(string iMEI, List<Walk> walksInADay, double kilometersOfWalkedInADay, TimeSpan durationOfWalksInADay)
        {
            if (string.IsNullOrWhiteSpace(iMEI))
            {
                throw new ArgumentNullException($"'{nameof(iMEI)}' cannot be null or whitespace.", nameof(iMEI));
            }
            if(walksInADay is null)
            {
                throw new ArgumentNullException($"'{nameof(walksInADay)}' cannot be null.", nameof(walksInADay));
            }
            if(kilometersOfWalkedInADay < 0)
            {
                throw new ArgumentNullException($"'{nameof(kilometersOfWalkedInADay)}' cannot be less than zero.", nameof(kilometersOfWalkedInADay));
            }

            IMEI = iMEI;
            WalksInADay = walksInADay;
            KilometersOfWalkedInADay = kilometersOfWalkedInADay;
            DurationOfWalksInADay = durationOfWalksInADay;
        }
    }
}
