using Arthur.App.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Arthur.App.Controllers;

[ApiController]
[Route("[controller]")]
public class PrimeController : Controller
{
    private readonly IPrimeProvider _primeProvider;

    public PrimeController(IPrimeProvider primeProvider)
    {
        _primeProvider = primeProvider;
    }

    [HttpGet]
    public int[] Get() => _primeProvider.Get();
}
