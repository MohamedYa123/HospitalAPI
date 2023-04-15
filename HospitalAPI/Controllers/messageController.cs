using HospitalAPI.models;
using HospitalAPI.siteClasses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    [Route(Sitemanager.endpoint + "messages/")]
    [ApiController]
    public class messageController : ControllerBase
    {
        [HttpPost]
        public ActionResult Create(message m)
        {
            try
            {
                Sitemanager.mydb.messages.Add(m);
                Sitemanager.mydb.SaveChanges(Sitemanager.Passcode);
            }
            catch { 
            return NotFound();
            }
            return NoContent();
        }
        [HttpGet]
        public List<message> GetMessages()
        {
            if (Sitemanager.isAdmin(HttpContext))
            {
                return Sitemanager.mydb.messages.ToList();
            }
            return null;
        }
        [HttpGet("{id}")]
        public message GetMessage(int id)
        {
            try
            {
                return Sitemanager.mydb.messages.Single(i => i.id == id);
            }
            catch
            {
                return null;
            }
        }
        [HttpDelete]
        public r DeleteMessage(int id)
        {
            try
            {
                if (Sitemanager.isAdmin(HttpContext))
                {
                    var a = Sitemanager.mydb.messages.Single(i => i.id == id);
                    Sitemanager.mydb.messages.Remove(a);
                    Sitemanager.mydb.SaveChanges(HttpContext);
                    return new r ("message deleted successfully", 1);
                }
                else
                {
                    return new r ("Only admin can do this", -1);
                }
            }
            catch (Exception ex)
            {
                return new r (ex.Message, -1);

            }
        }
    }

}
