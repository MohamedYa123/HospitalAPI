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
        
        [HttpPost("Login")]
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
                if (Sitemanager.isAdmin(HttpContext))
                {
                    user.position = Sitemanager.mydb.positions.FirstOrDefault(position => position.id == user.position.id||position.positionlevel==user.position.positionlevel);
                    
                    if (user.work.id == -1)
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
                    Sitemanager.mydb.SaveChanges();
                    return NoContent();
                }
            }
            catch
            {

            }
            return NotFound();
        }
    }
}
