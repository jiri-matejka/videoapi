using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace VideoApi.Controllers
{
    [Route("api/accounts/")]
    public class AccountsVideoController : Controller
    {
        [HttpGet("{accountId}/resumepoints")]
        public Task<object> GetAllResumePoints(int accountId)
        {
            var firstElem = new
            {
                account = 123,
                resumePoint = 13.3456
            };

            var result = new[]
            {
                firstElem
            };

            return Task.FromResult((object)result);
        }

    }
}
