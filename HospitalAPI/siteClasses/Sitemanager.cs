using HospitalAPI.models;
using Microsoft.EntityFrameworkCore;

namespace HospitalAPI.siteClasses
{
    public static class Sitemanager
    {
        public static db mydb;
        public static int idle = 8;
        public const string endpoint = "API/v1/";
        public static void setcookie(string key,string value,HttpContext hp)
        {
            hp.Response.Cookies.Delete(key);
            hp.Response.Cookies.Append(key, value,new CookieOptions{Expires=DateTime.Now.AddYears(10) });
           
        }
        public static string getcookie(string key,HttpContext hp)
        {
            return hp.Request.Cookies[key];
        }
        public static void Main()
        {
             mydb= new db();
           // mydb.Database.OpenConnection();
            
            // mydb.Update(null);
            //return;
            var user= mydb.users.Find(1);
            if (user == null)
            {
                position ps= new position{ id=0,name="Admin",positionlevel=100};
                category cat = new category { id = 0, name = "Admin", description = "Site manager" };
                work work = new work { id = 0, name = "Admin" ,category=cat};
                mydb.users.Add(new user { id = 0 ,email="tecyouth123@gmail.com",name="Mohamed Yasser",password="medo123",username="medo",position=ps,work=work}) ;
                mydb.category.Add(cat);
                mydb.positions.Add(ps);
                mydb.SaveChanges();
            }
        }
        public static user loginBYCookies(HttpContext hp)
        {
            user user = new user { username = getcookie("username", hp), password = getcookie("password", hp) };
            if (user.username == "")
            {
                return null;
            }
            var a = userloginlogout.login(user, hp);
            return a;
        }
        public static int CheckPosition(HttpContext hp)
        {
           var a=loginBYCookies(hp);
            if (a == null)
            {
                return -1;
            }
            return a.position.positionlevel;
        }
        public static bool isAdmin(HttpContext hp)
        {
            if (CheckPosition(hp) == 100)
            {
                return true;
            }
            return false;
        }
       
    }
}
