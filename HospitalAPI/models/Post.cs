using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.models
{
    public class Post
    {
        
            public int id { get; set; }
            public string name { get; set; }
            public string description { get; set; }
            public bool isadvertisement { get; set; }
            public user user { get; set; }
            public DateTime createdDate { get; set; }
        

        public void cover()
        {
            description = description.Substring(0, Math.Min( 500,description.Length));
        }
        public Post Clone()
        {
            var a=(Post)MemberwiseClone();
            a.user=user.clone();
            return a;
        }
    }
}
