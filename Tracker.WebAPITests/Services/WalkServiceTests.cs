using Tracker.WebAPI.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tracker.WebAPI.Services.Tests
{
    [TestClass()]
    public class WalkServiceTests
    {
        [TestMethod()]
        public async Task CreateWalks_CorrectIMEI_WalkList()
        {
            // arrange

            const string IMEI = "359339077003915";
            var walkService = new WalkService();

            // act 

            var walksList = await walkService.Calculate(IMEI);

            // assert

            Assert.IsNotNull(walksList);
            Assert.AreNotEqual(0, walksList.Count);
        }

        [TestMethod()]
        public async Task CreateWalks_IncorrectIMEI_EmptyWalkList()
        {
            // arrange

            const string IMEI = "edf";
            var walkService = new WalkService();

            // act 

            var walksList = await walkService.Calculate(IMEI);

            // assert

            Assert.IsNotNull(walksList);
            Assert.AreEqual(0, walksList.Count);
        }
    }
}