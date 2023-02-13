using Arthur.App.Domain;

namespace Arthur.App.Interface;

public interface IStatsRepository
{
    Stats[] Get(int dateStart, int dateEnd);
}
