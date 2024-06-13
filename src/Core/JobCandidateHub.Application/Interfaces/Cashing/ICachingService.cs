using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobCandidateHub.Application.Interfaces.Cashing;

public interface ICachingService
{
    T GetCachedData<T>(string cacheKey);

    void SetCachedData<T>(string cacheKey, T data, TimeSpan absoluteExpiration);
}
