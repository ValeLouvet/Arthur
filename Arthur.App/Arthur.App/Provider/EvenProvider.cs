using Arthur.App.Interface;

namespace Arthur.App.Provider;

public class EvenProvider : IEvenProvider
{
    private readonly int _evenSum;

    public EvenProvider()
    {
        _evenSum = CalculateEvenSum();
    }

    private int CalculateEvenSum()
    {
        var result = 0;
        for(var i = 2; i < 10000; i+=2)
        {
            result+= i;
        }

        return result;
    }

    public int Get() => _evenSum;
}
