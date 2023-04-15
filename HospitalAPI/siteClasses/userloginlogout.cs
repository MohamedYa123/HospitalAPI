using HospitalAPI.models;
using Microsoft.EntityFrameworkCore;

namespace HospitalAPI.siteClasses
{
    public static class userloginlogout
    {
        public static string sa = "";
        public static user login(user user,HttpContext hp)
        {
            sa = "It worked !";
            try
            {
                Sitemanager.mydb = new db();
                var a = Sitemanager.mydb.users.Include("position").Single(user1 => user1.username == user.username || user1.email == user.email);
                if (a.password == user.password)
                {
                    Sitemanager.setcookie("username", a.username, hp);
                    Sitemanager.setcookie("password",a.password, hp);
                    return a;
                }
                else
                {
                    string v = "";
                    try
                    {
                         v = $"db : {a.username},{a.password} ";
                    }
                    catch { }
                    throw new Exception($"Invalid username or password sent {user.username},{user.password} ,{v},user {a}");
                }
            }
            catch (Exception ex){ sa = ex.Message+$" data recieved :{user.id}:{user.name}:{user.username}:{user.password}"; }
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
