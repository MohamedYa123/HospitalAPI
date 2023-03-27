using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    [Route(siteClasses.Sitemanager.endpoint + "about")]
    [ApiController]
    public class about : ControllerBase
    {
        
        [HttpGet]
        public r aboutv()
        {
            return new r { def = "gg" };
        }
    }
    public class r
    {
        public string def { get; set; } = "gg";

    }
}
