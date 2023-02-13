using Arthur.App.Domain;
using Arthur.App.Interface;

namespace Arthur.App.Repository;

public class StatsRepository : IStatsRepository
{
    private readonly ArthurDbContext _arthurDbContext;

    public StatsRepository(ArthurDbContext arthurDbContext)
    {
        _arthurDbContext = arthurDbContext;
    }

    public Stats[] Get(int dateStart, int dateEnd) 
        => _arthurDbContext.Stats.Where(s => s.Date <= dateStart && s.Date >= dateEnd).ToArray();
}
