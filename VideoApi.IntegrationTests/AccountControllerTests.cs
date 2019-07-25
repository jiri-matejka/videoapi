using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Tests
{
	public class AccountControllerTests
	{
		private string endpoint;
		HttpClient client;

		[OneTimeSetUp]
		public void Setup()
		{
			this.endpoint = Environment.GetEnvironmentVariable("VIDEOAPI_WEB_ENDPOINT");
			if (this.endpoint == null)
				throw new InvalidOperationException("Environment variable VIDEOAPI_WEB_ENDPOINT must be set");

			this.client = new HttpClient();
			client.BaseAddress = new Uri(this.endpoint);
			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(
				new MediaTypeWithQualityHeaderValue("application/json"));
		}

		[OneTimeTearDown]
		public void TearDown()
		{
			this.client.Dispose();
		}

		[Test]
		public async Task WhenResumePointIsCreatedInPut_ThenIsCorrectlyReturnedInGet()
		{
			string accountId = "5d30890ec66e99000776dd89";
			string videoId = "5d30890ec66e99000776dd8b";
			double timePoint = new Random(DateTime.Now.Millisecond).Next();

			await CallPut(accountId, videoId, timePoint);

			string get = await client.GetStringAsync(
				$"api/accounts/{accountId}/videos/{videoId}/resumepoint");

			JObject result = JObject.Parse(get);
			Assert.True(result["accountId"].Value<string>() == accountId);
			Assert.True(result["videoId"].Value<string>() == videoId);
			Assert.True(result["timePoint"].Value<double>() == timePoint);
		}

		private async Task CallPut(string accountId, string videoId, double timePoint)
		{
			string data = $"{{\"timePoint\":{timePoint} }}";

			HttpContent content = new StringContent(data, UTF8Encoding.UTF8, "application/json");

			HttpResponseMessage response = await client.PutAsync(
				$"api/accounts/{accountId}/videos/{videoId}/resumepoint", content);

			Assert.True(response.IsSuccessStatusCode);
		}
	}
}