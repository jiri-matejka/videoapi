using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VideoApi.Data;
using VideoApi.Data.Entities;

namespace VideoApi.Controllers
{
    [Route("api/accounts/")]
    public class AccountsVideoController : Controller
    {
		private readonly IResumePointRepository resumePointRepository;

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

    }
}
