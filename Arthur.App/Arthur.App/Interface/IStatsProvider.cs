using Arthur.App.Domain;

namespace Arthur.App.Interface;

public interface IStatsProvider
{
    Stats[] Get();
}
