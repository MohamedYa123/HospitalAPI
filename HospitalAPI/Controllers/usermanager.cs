using HospitalAPI.models;
using HospitalAPI.siteClasses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalAPI.Controllers
{
    [Route(Sitemanager.endpoint+"users/")]
    [ApiController]
    public class usermanager : ControllerBase
    {
        [HttpPatch("modify")]
        public ActionResult modify(int id, string feature, string value)
        {
            
            try
            {
                int nd = 0;
                var w = Sitemanager.mydb.users.Single(i => i.id == id);
                switch (feature)
                {
                    case "name":
                        w.name = value; break;
                    case "description": w.description = value; break;
                    case "email":
                        w.email = value;
                        break;
                    case "position":
                         nd = Convert.ToInt32(value);
                        var p = Sitemanager.mydb.positions.Single(i => i.id == nd);
                        w.position = p;
                        break;
                    case "work":
                         nd = Convert.ToInt32(value);
                        var pp = Sitemanager.mydb.work.Single(i => i.id == nd);
                        w.work = pp;
                        break;
                    case "username":
                        w.username = value;
                        break;
                    case "password":
                        w.password = value;
                        break;
                }
                Sitemanager.mydb.users.Update(w);
                Sitemanager.mydb.SaveChanges(HttpContext);
                return NoContent();
            }
            catch { }
            return NotFound();
        }
        [HttpGet]
        public List<user> getall()
        {
            try
            {
                var a = Sitemanager.mydb.users.Include(i=>i.work).Include(i=>i.work.category).Include(i=>i.position).ToList();
                List<user> list = new List<user>();
                foreach(var v in a)
                {
                    var h = v.clone();
                    h.cover();
                    list.Add(h);
                }
                return list;
            }
            catch { }
            return null;
        }
        [HttpGet("ByWork")]
        public List<user> getusersbycategory(string categoryName) 
        {
            try
            {
                int id = Sitemanager.mydb.category.SingleOrDefault(category => category.name == categoryName).id;
                var a = Sitemanager.mydb.users.Include(i=>i.work).Include(i=>i.work.category).Where(user => user.work.category.id == id).ToList();
                return a;
            }
            catch {
                return null;
            }
        }
        [HttpGet("ByWorkID")]
        public List<user> getusersbycategory(int id) 
        {
            try
            {
                 var a = Sitemanager.mydb.users.Include(i=>i.work).Include(i=>i.work.category).Where(user => user.work.category.id == id).ToList();
                return a;
            }
            catch {
                return null;
            }
        }
        [HttpPost("Login2")]
        public ActionResult Login(user user)
        {
           
            if (userloginlogout.login(user,HttpContext)!=null)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost("Login")]
        public user login2(user user)
        {
            var a = userloginlogout.login(user, HttpContext);
            if (a!= null)
            {
                return a;
            }
            else
            {
                return null;
            }
        }
        [HttpPost("login3")]
        public user login3(user user)
        {
            var a = userloginlogout.login(user, HttpContext);
            return a;
        }
        [HttpGet("profile")]
        public user profile()
        {
            var user=Sitemanager.loginBYCookies(HttpContext);
            return user;
        }
        [HttpGet("{id}")]
        public user GetUser(int id)
        {
            try
            {
                var u = Sitemanager.mydb.users.Include("position").Include("work").Single(user1 => user1.id == id);
                if (u == null)
                {
                    return u;
                }
                u.work = Sitemanager.mydb.work.Include("category").Single(work => work.id == u.work.id);
                u.cover();
                return u;
            }
            catch { }
            return null;
        }
        [HttpPost("Logout")]
        public ActionResult Logout()
        {
            userloginlogout.logout(HttpContext);
            return NoContent();
        }
        [HttpPost("AddUser")]
        public ActionResult AddUser(user user)
        {
            try
            {
                user.id = 0;
                if (true)
                {
                    user.position = Sitemanager.mydb.positions.FirstOrDefault(position => position.id == user.position.id||position.positionlevel==user.position.positionlevel);
                    
                    if (user.work.id == -1||user.work.id==0)
                    {
                        user.work = Sitemanager.mydb.work.SingleOrDefault(work => work.id == Sitemanager.idle);
                    }
                    else
                    {
                        user.work= Sitemanager.mydb.work.SingleOrDefault(work => work.id == user.work.id);
                    }
                    if (user.position == null||user.work==null)
                    {
                        throw new Exception();
                    }
                    Sitemanager.mydb.users.Add(user);
                    Sitemanager.mydb.SaveChanges(HttpContext);
                    return NoContent();
                }
            }
            catch
            {

            }
            return NotFound();
        }
        [HttpDelete]
        public r DeleteUser(int id)
        {
            try
            {
                if (Sitemanager.isAdmin(HttpContext))
                {
                    var a = Sitemanager.mydb.users.Single(i => i.id == id);
                    Sitemanager.mydb.users.Remove(a);
                    Sitemanager.mydb.SaveChanges(HttpContext);
                    return new r ( "user deleted successfully" ,1);
                }
                else
                {
                    return new r("Only admin can do this",-1 );
                }
            }
            catch (Exception ex){
                return new r (ex.Message, -1);

            }
        }
        [HttpGet("isadmin")]
        public r isadmin()
        {
            if (Sitemanager.isAdmin(HttpContext))
            {
                return new r ("True", 0);
            }
            return new r ("False", 0);
        }
    }
}
