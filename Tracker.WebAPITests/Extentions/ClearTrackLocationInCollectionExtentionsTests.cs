using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tracker.WebAPI.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracker.WebAPI.Services;

namespace Tracker.WebAPI.Extentions.Tests
{
    [TestClass()]
    public class ClearTrackLocationInCollectionExtentionsTests
    {
        [TestMethod()]
        public async Task ClearTrackLocationInCollectionTest()
        {
            // arrange

            var walkService = new WalkService();
            const string IMEI = "359339077003915";
            var walksList = await walkService.Calculate(IMEI);

            // act

            walksList.ClearTrackLocationInCollection();

            // assert

            Assert.AreEqual(0, walksList.FirstOrDefault().TrackLocationCollection.Count);
        }

        [TestMethod()]
        public async Task ClearTrackLocationInCollectionTest1()
        {
            // arrange

            var walkService = new WalkService();
            var walkingDayService = new WalkingDayService();

            const string IMEI = "359339077003915";
            var walksList = await walkService.Calculate(IMEI);

            var walkingDaysList = walkingDayService.Calculate(walksList);
            // act

            walkingDaysList.ClearTrackLocationInCollection();

            // assert

            Assert.AreEqual(0, walkingDaysList.FirstOrDefault().WalksInADay.FirstOrDefault().TrackLocationCollection.Count);
        }
    }
}