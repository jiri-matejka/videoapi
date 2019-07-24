using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using VideoApi.Data;
using VideoApi.Data.Entities;

namespace VideoApi.Controllers
{
    [Route("api/accounts/")]
	[VideoApi.Web.ExceptionFilter]
    public class AccountsVideoController : Controller
    {
		private readonly IResumePointRepository resumePointRepository;

		public class CreateOrUpdateResumePointRequest
		{
			public double timePoint;
		}

		public AccountsVideoController(IResumePointRepository resumePointRepository)
		{
			this.resumePointRepository = resumePointRepository;
		}

		[HttpGet("{accountId}/resumepoints")]
        public async Task<IReadOnlyList<ResumePoint>> GetAllResumePoints(string accountId)
        {
			IReadOnlyList<ResumePoint> resumePoints = await this.resumePointRepository.GetAll(accountId);

			return resumePoints;
        }

		[HttpGet("{accountId}/videos/{videoId}/resumepoint")]
		public async Task<ResumePoint> GetResumePointForVideo(string accountId, string videoId)
		{
			ResumePoint resumePoint = await this.resumePointRepository.Get(accountId, videoId);

			return resumePoint;
		}

		[HttpPost("{accountId}/videos/{videoId}/resumepoint")]
		[HttpPut("{accountId}/videos/{videoId}/resumepoint")]
		public async Task<IStatusCodeActionResult> CreateOrUpdateResumePoint(string accountId, string videoId, [FromBody] CreateOrUpdateResumePointRequest request)
		{
			if (request == null)
				return BadRequest(new { error = "Empty request" });
			
			await this.resumePointRepository.InsertOrUpdate(accountId, videoId, request.timePoint);

			return Ok();
		}

    }
}
