namespace MiBocataAPI.Framework;

public class MBControllerBase : Controller
{
    protected readonly MBDBContext _context;
    protected readonly IConfiguration configuration;

    public MBControllerBase(
        IConfiguration configuration, 
        MBDBContext context)
    {
        _context = context;
        this.configuration = configuration;
    }
}
