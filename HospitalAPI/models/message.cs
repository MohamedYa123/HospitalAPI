using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.models
{
    public class message
    {
        public int id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public DateTime createdDate { get; set; }
    }
}
