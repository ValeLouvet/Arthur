using Arthur.App.Interface;

namespace Arthur.App.Provider;

public class PrimeProvider : IPrimeProvider
{
    private readonly int[] _primeList;

    public PrimeProvider()
    {
        _primeList = CalculatePrime();
    }

    private int[] CalculatePrime()
    {
        var primeList = new List<int>() { 2 };
        for (int i = 3; i < 10000; i++)
        {
            var j = 0;
            var isNotPrime = false;
            while (j < primeList.Count && !isNotPrime && Math.Sqrt(i) >= primeList[j])
            {
                if (i % primeList[j] == 0)
                {
                    isNotPrime = true;
                }

                ++j;
            }

            if(!isNotPrime)
                primeList.Add(i);
        }

        return primeList.ToArray();
    }

    public int[] Get() => _primeList;
}
