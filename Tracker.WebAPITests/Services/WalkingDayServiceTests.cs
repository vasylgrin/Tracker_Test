using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tracker.WebAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracker.WebAPI.Services.Tests
{
    [TestClass()]
    public class WalkingDayServiceTests
    {
        [TestMethod()]
        public async Task Calculate_WalksListByImei_WalkingDaysListByImei()
        {
            // arrange

            var _walkService = new WalkService();
            var _walkingDayService = new WalkingDayService();

            const string IMEI = "359339077003915";
            var walksList = await _walkService.Calculate(IMEI);

            // act

            var walkingDaysList = _walkingDayService.Calculate(walksList);

            // assert

            Assert.IsNotNull(walkingDaysList);
            Assert.AreNotEqual(0, walkingDaysList.Count);
        }

        [TestMethod()]
        public async Task Calculate_EmptyWalksList_EmptyWalkingDaysList()
        {
            // arrange

            var _walkService = new WalkService();
            var _walkingDayService = new WalkingDayService();

            const string IMEI = "dsfsd";
            var walksList = await _walkService.Calculate(IMEI);

            // act

            var walkingDaysList = _walkingDayService.Calculate(walksList);

            // assert

            Assert.IsNotNull(walkingDaysList);
            Assert.AreEqual(0, walkingDaysList.Count);
        }
    }
}