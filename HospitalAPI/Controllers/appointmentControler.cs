using HospitalAPI.models;
using HospitalAPI.siteClasses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    [Route(Sitemanager.endpoint + "appointments")]
    [ApiController]
    public class appointmentControler : ControllerBase
    {
        [HttpPost]
        public ActionResult Post(appointment appointment)
        {
            try
            {
                Sitemanager.mydb.appointments.Add(appointment);
                Sitemanager.mydb.SaveChanges(Sitemanager.Passcode);
            }
            catch { }
            return NotFound();
        }
        [HttpGet]
        public List<appointment> Get()
        {
            try
            {
                var a = Sitemanager.mydb.appointments.ToList();
                return a;
            }
            catch { }
            return null;
        }
        [HttpDelete]
        public ActionResult Delete(int id) 
        {
            try
            {
                var a=Sitemanager.mydb.appointments.Single(i=>i.id==id);
                Sitemanager.mydb.appointments.Remove(a);
                Sitemanager.mydb.SaveChanges(HttpContext);
                return NoContent();
            }
            catch { }
            return NotFound();
        }
        [HttpGet("id")]
        public appointment GetById(int id)
        {
            try
            {
                var a = Sitemanager.mydb.appointments.Single(i => i.id == id);
                return a;
            }
            catch { }
            return null;
        }
    }
}
