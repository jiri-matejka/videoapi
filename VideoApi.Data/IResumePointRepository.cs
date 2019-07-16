using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VideoApi.Data.Entities;

namespace VideoApi.Data
{
    public interface IResumePointRepository
    {
        // the consumers of this interface will be not aware of Mongo data types
        ResumePoint Get(string id);

		Task<IReadOnlyList<ResumePoint>> GetAll(string accountId);
    }
}
