using System;
using VideoApi.Data.Entities;

namespace VideoApi.Data
{
    public interface IResumePointRepository
    {
        // the consumers of this interface will be not aware of Mongo data types
        ResumePoint Get(string id);
    }
}
