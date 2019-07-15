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
        public Task<object> GetAllResumePoints(int accountId)
        {
			ResumePoint rp = new ResumePoint { VideoId = "1", TimePoint = 13.5 };
				

            var firstElem = new
            {
                account = 123,
                resumePoint = rp.TimePoint
            };

            var result = new[]
            {
                firstElem
            };

            return Task.FromResult((object)result);
        }

    }
}
