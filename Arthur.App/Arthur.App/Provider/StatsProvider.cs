using Arthur.App.Domain;
using Arthur.App.Interface;
using Microsoft.Extensions.Caching.Memory;

namespace Arthur.App.Provider;

public class StatsProvider : IStatsProvider
{
    private readonly IMemoryCache _memoryCache;
    private readonly IStatsRepository _statsRepository;
    private const string _keyCache = "StatsCachedKey";

    public StatsProvider(IMemoryCache memoryCache, IStatsRepository statsRepository)
    {
        _memoryCache = memoryCache;
        _statsRepository = statsRepository;
    }

    public Stats[] Get()
    {
        if (_memoryCache.TryGetValue(_keyCache, out Stats[]? stats) && stats != null)
        {
            return stats;
        }

        var dateStart = TransformDateToInt(DateTime.Today.AddDays(-1));
        var dateEnd = TransformDateToInt(DateTime.Today.AddYears(-1));
        stats = _statsRepository.Get(dateStart, dateEnd);
        _memoryCache.Set(_keyCache, stats, DateTime.Now.Date.AddDays(1).AddMilliseconds(-1));
        return stats;
    }

    private int TransformDateToInt(DateTime dateTime)
    {
        return dateTime.Year * 10000 + dateTime.Month * 100 + dateTime.Day;
    }
}
