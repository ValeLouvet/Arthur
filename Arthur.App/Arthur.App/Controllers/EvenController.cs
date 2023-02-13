using Arthur.App.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Arthur.App.Controllers;

[ApiController]
[Route("[controller]")]
public class EvenController : Controller
{
    private readonly IEvenProvider _evenProvider;

    public EvenController(IEvenProvider evenProvider)
    {
        _evenProvider = evenProvider;
    }

    [HttpGet]
    public int Get() => _evenProvider.Get();
}
