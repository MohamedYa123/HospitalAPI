using HospitalAPI.models;
using HospitalAPI.siteClasses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalAPI.Controllers
{
    [Route(Sitemanager.endpoint+"works/")]
    [ApiController]
    public class workAndCategories : ControllerBase
    {
        [HttpGet]
        public List<work> getworks()
        {
            try
            {
                var a = Sitemanager.mydb.work.ToList();
                return a;
            }
            catch { }
            return null;
        }
        [HttpGet("getworkBycategory")]
        public List<work> getworks(int category)
        {
            try
            {
                var a = Sitemanager.mydb.work.Include(i=>i.category).Where(i=>i.category.id== category).ToList();
                return a;
            }
            catch { }
            return null;
        }
        [HttpGet("Categories")]
        public List<category> getcats()
        {
            try
            {
                var a = Sitemanager.mydb.category.ToList();
                return a;
            }
            catch { }
            return null;
        }
        [HttpPut("resetcategory")]
        public ActionResult resetcat(category cat)
        {
            try { 
            var cat2=Sitemanager.mydb.category.Single(i=>i.id==cat.id);
            cat2.name = cat.name;
            cat2.description = cat.description;
                Sitemanager.mydb.category.Update(cat2);
                Sitemanager.mydb.SaveChanges(HttpContext);
                return NoContent();
            }
            catch { }
            return NotFound();
        }
        [HttpPatch("modifyCategory")]
        public ActionResult modifyCategory(int id, string feature, string value)
        {
            try
            {
                var w = Sitemanager.mydb.category.Single(i => i.id == id);
                switch (feature)
                {
                    case "name":
                        w.name = value; break;
                    case "description": w.description = value; break;
                    
                }
                Sitemanager.mydb.category.Update(w);
                Sitemanager.mydb.SaveChanges(HttpContext);
                return NoContent();
            }
            catch { }
            return NotFound();
        }
        [HttpPatch("modifywork")]
        public ActionResult modify(int id,string feature,string value)
        {
            try
            {
                var w=Sitemanager.mydb.work.Single(i=>i.id==id);
                switch(feature)
                {
                    case "name":
                        w.name = value; break;
                    case "description": w.description = value; break;
                    case "category":
                        int nd=Convert.ToInt32(value);
                        var cat=Sitemanager.mydb.category.Single(i=>i.id==nd);
                        w.category= cat;
                        break;
                }
                Sitemanager.mydb.work.Update(w);
                Sitemanager.mydb.SaveChanges(HttpContext);
                return NoContent();
            }
            catch { }
            return NotFound();
        }
        [HttpPut("resetwork")]
        public ActionResult reset(work w)
        {
            try
            {
                var w2 = Sitemanager.mydb.work.Single(i => i.id == w.id);
                w2.description = w.description;
                w2.name = w.name;
                var cat = Sitemanager.mydb.category.Single(i => i.id == w.category.id);
                if (cat != null)
                {
                    w2.category = cat;
                }
                Sitemanager.mydb.work.Update(w2);
                Sitemanager.mydb.SaveChanges(HttpContext);
                return NoContent();
            }
            catch { }
            return NotFound();
        }
        [HttpGet("getcategoryByid")]
        public category getcats(int id)
        {
            try
            {
                var a = Sitemanager.mydb.category.Single(i=>i.id== id);
                return a;
            }
            catch { }
            return null;
        }
        [HttpPost("AddWork")]
        public ActionResult AddWork(work work)
        {
            try
            {
                if (true)
                {
                    var categoryII=Sitemanager.mydb.category.SingleOrDefault(category=> category.id == work.category.id||category.name==work.category.name);
                    if(categoryII==null)
                    {
                        throw new Exception();
                    }
                    work.category = categoryII;
                    Sitemanager.mydb.work.Add(work);
                    Sitemanager.mydb.SaveChanges(HttpContext);
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
                if (true)
                {
                    Sitemanager.mydb.category.Add(category);
                    Sitemanager.mydb.SaveChanges(HttpContext);
                    return NoContent();
                }
            }
            catch { }
            return NotFound();
        }
        [HttpDelete("DeleteWork")]
        public r DeleteUser(int id)
        {
            try
            {
                if (Sitemanager.isAdmin(HttpContext))
                {
                    var a = Sitemanager.mydb.work.Single(i => i.id == id);
                    Sitemanager.mydb.work.Remove(a);
                    Sitemanager.mydb.SaveChanges(HttpContext);
                    return new r ("work deleted successfully", -1);
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
        [HttpDelete("DeleteCategory")]
        public r DeleteCategory(int id)
        {
            try
            {
                if (Sitemanager.isAdmin(HttpContext))
                {
                    var a = Sitemanager.mydb.category.Single(i => i.id == id);
                    Sitemanager.mydb.category.Remove(a);
                    Sitemanager.mydb.SaveChanges(HttpContext);
                    return new r (  "category deleted successfully",1 );
                }
                else
                {
                    return new r ("Only admin can do this", -1);
                }
            }
            catch (Exception ex)
            {
                return new r( ex.Message,-1 );

            }
        }
    }
}
