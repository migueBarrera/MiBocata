using MiBocataAPI.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace MiBocataAPI.Framework
{
    public class MBControllerBase : ControllerBase
    {
        protected readonly MBDBContext _context;
        protected readonly IConfiguration configuration;

        public MBControllerBase(IConfiguration configuration, MBDBContext context)
        {
            _context = context;
            this.configuration = configuration;
        }
    }
}
