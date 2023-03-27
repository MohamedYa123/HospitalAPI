using HospitalAPI.models;
using Microsoft.EntityFrameworkCore;

namespace HospitalAPI.siteClasses
{
    public static class userloginlogout
    {
        public static user login(user user,HttpContext hp)
        {
            try
            {
                var a = Sitemanager.mydb.users.Include("position").Single(user1 => user1.username == user.username || user1.email == user.email);
                if (a.password == user.password)
                {
                    Sitemanager.setcookie("username", a.username, hp);
                    Sitemanager.setcookie("password",a.password, hp);
                    return a;
                }
            }
            catch { }
            return null;
        }
        public static void logout(HttpContext hp) 
        {
            try
            {
                Sitemanager.setcookie("username", "", hp);
                Sitemanager.setcookie("password", "", hp);
            }
            catch { }
        }
    }
}
