using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using VideoApi.Controllers;
using VideoApi.Data;
using VideoApi.Data.Entities;

namespace Tests
{
	public class AccountsVideoControllerTests
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public async Task GetMethodReturnsCorrectInstance()
		{
			const string accountId = "123";
			const string videoId = "456";
			const double timePoint = 12.3;

			ResumePoint expected = new ResumePoint()
			{
				AccountId = accountId,
				VideoId = videoId,
				TimePoint = timePoint
			};

			var mock = new Mock<IResumePointRepository>();
			mock.Setup(repo => repo.Get(accountId, videoId)).Returns(Task.FromResult(expected));

			AccountsVideoController controller = new AccountsVideoController(mock.Object);

			ResumePoint actual = await controller.GetResumePointForVideo(accountId, videoId);

			Assert.AreEqual(expected, actual);

		}
	}
}