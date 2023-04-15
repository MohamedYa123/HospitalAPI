using HospitalAPI.models;
using HospitalAPI.siteClasses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    [Route(Sitemanager.endpoint+"Medicalhistories")]
    [ApiController]
    public class medicalhistoryManager : ControllerBase
    {
        [HttpPost]
        public ActionResult Create(medicalhistory md)
        {
            try
            {
                if (Sitemanager.mydb.medicalhistories.Where(i => i.nationalid == md.nationalid).ToList().Count!=0)
                {
                    throw new Exception();
                }
                Sitemanager.mydb.medicalhistories.Add(md);
                Sitemanager.mydb.SaveChanges(HttpContext);
                return NoContent();
            }
            catch { }
            return NotFound();
        }
        [HttpGet("{id}")]
        public medicalhistory Get(int id)
        {
            try { 
                var a= Sitemanager.mydb.medicalhistories.Single(i=>i.id==id);
                return a;
            } catch { }
            return null;
        }
        [HttpGet("nationalid/{id}")]
        public medicalhistory GetMedicalhistory(int id)
        {
            try
            {
                var a = Sitemanager.mydb.medicalhistories.Single(i => i.nationalid == id);
                return a;
            }
            catch { }
            return null;
        }
        [HttpGet]
        public List<medicalhistory> listall()
        {
            try
            {
                if (Sitemanager.isAdmin(HttpContext))
                {
                    var a=Sitemanager.mydb.medicalhistories.ToList();
                    return a;
                }
            }
            catch { }
            return null;
        }
        [HttpDelete]
        public r DeleteMedicalhistory(int id)
        {
            try
            {
                if (Sitemanager.isAdmin(HttpContext))
                {
                    var a = Sitemanager.mydb.medicalhistories.Single(i => i.id == id);
                    Sitemanager.mydb.medicalhistories.Remove(a);
                    Sitemanager.mydb.SaveChanges(HttpContext);
                    return new r (  "medical history deleted successfully",1 );
                }
                else
                {
                    return new r ( "Only admin can do this" ,-1);
                }
            }
            catch (Exception ex)
            {
                return new r(  ex.Message ,-1);

            }
        }
    }
}
