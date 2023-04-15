using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.models
{
    public class medicalhistory
    {
        public int id { get; set; }
        public long nationalid { get; set; }
        public string name { get; set; }
        public int Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string description { get; set; }
    }
}
