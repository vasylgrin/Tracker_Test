using Tracker.WebAPI.Models;
using Tracker.WebAPI.Repositories.Interfaces;
using Tracker.WebAPI.Repositories;

namespace Tracker.WebAPI.Services
{
    public sealed class WalkService
    {
        private readonly IRepositoryBase<TrackLocation> _repository;
        private List<Walk> _walks;
        private string _IMEI = "";
        private Walk _walk;

        public WalkService()
        {
            _repository = new SQLServerRepository<TrackLocation>();
            _walks = new List<Walk>();
            _walk = new Walk();
        }


        public async Task<List<Walk>> Calculate(string IMEI)
        {
            _IMEI = IMEI;


            var trackLocationArray = await LoadAndOrderByTrack();
            if (trackLocationArray == null || trackLocationArray.Length == 0) return new List<Walk>();

            await CreateWalks(trackLocationArray);
            CalculateKilometrs();
            CalculateTimeOfWalk();

            return await Task.FromResult(_walks);
        }

        private async Task CreateWalks(TrackLocation[] trackLocationArray)
        {
            for (int i = 0; i < trackLocationArray.Length - 1; i++)
            {
                var intervalTimeSpan = SubstractDateTime(trackLocationArray, i);

                AddTrackLocationToWalk(intervalTimeSpan, trackLocationArray, i);
            }
        }
        private async Task<TrackLocation[]> LoadAndOrderByTrack()
        {
            var tracksArray = (await _repository.LoadAsync()).ToArray();
            return tracksArray.Where(t => t.Imei == _IMEI).OrderBy(t => t.DateTrack.Date).ThenBy(t => t.DateTrack.TimeOfDay).ToArray();
        }
        private TimeSpan SubstractDateTime(TrackLocation[] trackLocationArray, int iterator)
        {
            var firstDateTrack = trackLocationArray[iterator].DateTrack;
            var secondDateTrack = trackLocationArray[iterator + 1].DateTrack;

            return secondDateTrack.Subtract(firstDateTrack);
        }
        private void AddTrackLocationToWalk(TimeSpan intervalTimeSpan, TrackLocation[] trackLocationArray, int i)
        {
            if (intervalTimeSpan.Days > 0 || intervalTimeSpan.Hours > 0 || intervalTimeSpan.Minutes > 29)
            {
                _walk.TrackLocationCollection.Add(trackLocationArray[i]);
                _walks.Add(_walk);
                _walk = new Walk();
            }
            else
            {
                _walk.TrackLocationCollection.Add(trackLocationArray[i]);
            }
        }


        private void CalculateKilometrs()
        {
            var walksArray = _walks.ToArray();

            for (int i = 0; i < walksArray.Length; i++)
            {
                if (walksArray[i].TrackLocationCollection.Count <= 1)
                {
                    walksArray[i].KilometersWalked = 0;
                    continue;
                }

                walksArray[i].KilometersWalked = Calculate(walksArray[i]);
            }
        }
        private double Calculate(Walk currentWalk)
        {
            double totalDistance = 0;

            for (int j = 0; j < currentWalk.TrackLocationCollection.Count - 1; j++)
            {
                var lat1 = currentWalk.TrackLocationCollection[j].Latitude;
                var lon1 = currentWalk.TrackLocationCollection[j].Longitude;

                var lat2 = currentWalk.TrackLocationCollection[j + 1].Latitude;
                var lon2 = currentWalk.TrackLocationCollection[j + 1].Longitude;


                var distance = Distance(Convert.ToDouble(lat1), Convert.ToDouble(lon1), Convert.ToDouble(lat2), Convert.ToDouble(lon2));
                totalDistance += distance;
            }

            return totalDistance;
        }
        private double Distance(double lat1, double lon1, double lat2, double lon2)
        {
            const double rEarth = 6371;

            var dLat = ToRadians(lat2 - lat1);
            var dLon = ToRadians(lon2 - lon1);


            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Cos(ToRadians(lat1))
                * Math.Cos(ToRadians(lat2)) * Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            var c = 2 * Math.Asin(Math.Sqrt(a));
            return rEarth * c;

        }
        private static double ToRadians(double angle) => Math.PI * angle / 180;

        private void CalculateTimeOfWalk()
        {
            var walksArray = _walks.ToArray();

            foreach (var walk in walksArray)
            {
                var tracks = walk.TrackLocationCollection;

                var start = tracks.FirstOrDefault().DateTrack;
                var end = tracks.LastOrDefault().DateTrack;

                var time = end - start;

                walk.DurationOfWalk = time;
            }
        }
    }
}
