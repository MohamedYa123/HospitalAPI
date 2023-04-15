using HospitalAPI.models;
using HospitalAPI.siteClasses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalAPI.Controllers
{
    [Route(Sitemanager.endpoint+"posts/")]
    [ApiController]
    public class PostManager : ControllerBase
    {
        [HttpGet]
        public List<Post> getall()
        {
            try
            {
                List<Post> posts = new List<Post>();    
                var a = Sitemanager.mydb.posts.Include(i=>i.user).ToList();
                foreach(var post in a)
                {
                    var h = post.Clone();
                    h.cover();
                    h.user.cover();
                    posts.Add(h);
                }
                return posts;
            }
            catch { }
            return null;
        }
        [HttpPost("Create")]
        public ActionResult Create(Post post)
        {
            try
            {
                post.id = 0;
                if (Sitemanager.isAdmin(HttpContext))
                {
                    post.user = Sitemanager.loginBYCookies(HttpContext);
                    if (post.user == null) { throw new Exception(); }
                    post.createdDate= DateTime.Now;
                    Sitemanager.mydb.posts.Add(post);
                    Sitemanager.mydb.SaveChanges(HttpContext);
                    return NoContent();
                }
            }
            catch { }
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
                    post.isadvertisement=postsent.isadvertisement;
                    Sitemanager.mydb.posts.Update(post);
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
        public r DeletePost(int id)
        {
            try
            {
                if (Sitemanager.isAdmin(HttpContext))
                {
                    var a = Sitemanager.mydb.posts.Single(i => i.id == id);
                    Sitemanager.mydb.posts.Remove(a);
                    Sitemanager.mydb.SaveChanges(HttpContext);
                    return new r ( "post deleted successfully",1 );
                }
                else
                {
                    return new r ( "Only admin can do this",-1 );
                }
            }
            catch (Exception ex)
            {
                return new r (ex.Message, -1);

            }
        }
    }
}
