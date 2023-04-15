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
            return new r ("gg",0 );
        }
    }
    public class r
    {
        public string information { get; set; } = "gg";
        public int messageCode { get; set; }//1 done, -1 error, 0 info
        public r(string information,int messageCode)
        {
            this.information = information;
            this.messageCode = messageCode;
        }

    }
}
