using HospitalAPI.models;
using HospitalAPI.siteClasses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    [Route(Sitemanager.endpoint+"work/")]
    [ApiController]
    public class workAndCategories : ControllerBase
    {
        [HttpPost("AddWork")]
        public ActionResult AddWork(work work)
        {
            try
            {
                if (Sitemanager.isAdmin(HttpContext))
                {
                    var categoryII=Sitemanager.mydb.category.SingleOrDefault(category=> category.id == work.category.id||category.name==work.category.name);
                    if(categoryII==null)
                    {
                        throw new Exception();
                    }
                    work.category = categoryII;
                    Sitemanager.mydb.work.Add(work);
                    Sitemanager.mydb.SaveChanges();
                    return NoContent();
                }
            }
            catch { }
            return NotFound();
        }
        [HttpPost("AddCategory")]
        public ActionResult AddCategory(category category)
        {
            try
            {
                if (Sitemanager.isAdmin(HttpContext))
                {
                    Sitemanager.mydb.category.Add(category);
                    Sitemanager.mydb.SaveChanges();
                    return NoContent();
                }
            }
            catch { }
            return NotFound();
        }

    }
}
