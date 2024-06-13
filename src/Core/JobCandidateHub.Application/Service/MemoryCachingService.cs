using JobCandidateHub.Application.Interfaces.Cashing;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobCandidateHub.Application.Service;

public class MemoryCachingService: ICachingService
{
    private readonly IMemoryCache _memoryCache;

    public MemoryCachingService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public T GetCachedData<T>(string cacheKey)
    {
        return _memoryCache.Get<T>(cacheKey);
    }

    public void SetCachedData<T>(string cacheKey, T data, TimeSpan absoluteExpiration)
    {
        var cacheOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(absoluteExpiration);

        _memoryCache.Set(cacheKey, data, cacheOptions);
    }
}
