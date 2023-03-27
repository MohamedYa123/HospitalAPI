using HospitalAPI.models;
using HospitalAPI.siteClasses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalAPI.Controllers
{
    [Route(Sitemanager.endpoint+"post/")]
    [ApiController]
    public class PostManager : ControllerBase
    {
        [HttpPost("Create")]
        public ActionResult Create(Post post)
        {
            if (Sitemanager.isAdmin(HttpContext))
            {
                post.user=Sitemanager.loginBYCookies(HttpContext);
                Sitemanager.mydb.posts.Add(post);
                Sitemanager.mydb.SaveChanges();
                return NoContent();
            }
            return NotFound();
        }
        [HttpGet("{id}")]
        public Post read(int id)
        {
           var post= Sitemanager.mydb.posts.Include("user").SingleOrDefault(post=>post.id==id);
            if(post==null)
            {
                return new Post { id = -1 };
            }
            post.user.cover();
            return post;
        }
        [HttpPut("{id}")]
        public ActionResult edit(Post postsent,int id)
        {
            try
            {
                var post = Sitemanager.mydb.posts.Include("user").SingleOrDefault(post => post.id == id);
                if (post == null)
                {
                    return NotFound();
                }
                if (Sitemanager.loginBYCookies(HttpContext).id == post.id)
                {
                    post.name=postsent.name;
                    post.description=postsent.description;
                    Sitemanager.mydb.posts.Update(post);
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
