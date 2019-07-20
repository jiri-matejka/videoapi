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
        public async Task<object> GetAllResumePoints(string accountId)
        {
			IReadOnlyList<ResumePoint> resumePoints = await this.resumePointRepository.GetAll(accountId);

			return resumePoints;
        }

		[HttpGet("{accountId}/videos/{videoId}/resumepoint")]
		public async Task<object> GetResumePointForVideo(string accountId, string videoId)
		{
			ResumePoint resumePoint = await this.resumePointRepository.Get(accountId, videoId);

			return resumePoint;
		}

		[HttpPost("{accountId}/videos/{videoId}/resumepoint")]
		[HttpPut("{accountId}/videos/{videoId}/resumepoint")]
		public async Task<IStatusCodeActionResult> CreateOrUpdateResumePoint(string accountId, string videoId, [FromBody] CreateOrUpdateResumePointRequest request)
		{
			if (request == null)
				return BadRequest();

			try
			{
				await this.resumePointRepository.InsertOrUpdate(accountId, videoId, request.timePoint);
			}
			catch
			{
				// Normally I would log the exception to the log here
				return StatusCode(500);
			}

			return Ok();
		}

    }
}
