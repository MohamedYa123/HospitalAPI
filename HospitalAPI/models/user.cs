﻿namespace HospitalAPI.models
{
    public class user
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string username { get; set; }
        public position position { get; set; }
        public work work { get; set; }
        public  void cover()
        {
           // email = "";
            username= "";
            password = "";
        }

        public user clone()
        {
            return (user)MemberwiseClone();
        }
    }
}
