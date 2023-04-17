using Tracker.WebAPI.Models;

namespace Tracker.WebAPI.Services
{
    public sealed class WalkingDayService
    {
        private List<WalkingDay> _walkingDays;


        public WalkingDayService()
        {
            _walkingDays = new List<WalkingDay>();
        }


        public List<WalkingDay> Calculate(List<Walk> walks)
        {
            if(walks.Count == 0) return new List<WalkingDay>();

            GetAllTrackLocationInADay(walks);

            CalculateHowManyTimesWalkedInADay();
            CalculateHowManyKilometersWalkedInADay();

            return _walkingDays;
        }
        
        private void GetAllTrackLocationInADay(List<Walk> Walks)
        {
            var walkingDay = new WalkingDay();

            for (int i = 0; i < Walks.Count - 1; i++)
            {
                var dateOfFirstWalk = Walks[i]?.TrackLocationCollection?.FirstOrDefault()?.DateTrack.Date;
                var dateOfSecondWalk = Walks[i + 1]?.TrackLocationCollection?.FirstOrDefault()?.DateTrack.Date;

                if (dateOfFirstWalk == dateOfSecondWalk)
                {
                    walkingDay.WalksInADay.Add(Walks[i]);
                }
                else
                {
                    walkingDay.WalksInADay.Add(Walks[i]);
                    walkingDay.IMEI = Walks[i]?.TrackLocationCollection?.FirstOrDefault()?.Imei;
                    _walkingDays.Add(walkingDay);
                    walkingDay = new WalkingDay();
                }
            }
        }
        
        private void CalculateHowManyKilometersWalkedInADay()
        {
            foreach (var walkingDay in _walkingDays)
            {
                foreach (var walk in walkingDay.WalksInADay)
                {
                    walkingDay.KilometersOfWalkedInADay += walk.KilometersWalked;
                }
            }
        }
        private void CalculateHowManyTimesWalkedInADay()
        {
            foreach (var walkingDay in _walkingDays)
            {
                foreach (var walk in walkingDay.WalksInADay)
                {
                    walkingDay.DurationOfWalksInADay += walk.DurationOfWalk;
                }
            }
        }
    }
}
