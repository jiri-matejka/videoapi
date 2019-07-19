using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VideoApi.Data.Entities;

namespace VideoApi.Data
{
    public interface IResumePointRepository
    {
        // the consumers of this interface will be not aware of Mongo data types
   
		Task<IReadOnlyList<ResumePoint>> GetAll(string accountId);
		Task<ResumePoint> Get(string accountId, string videoId);
	}
}
