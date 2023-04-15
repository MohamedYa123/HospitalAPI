namespace HospitalAPI.models
{
    public class appointment
    {
        public int id { get; set; }
        public string title { get; set; }

        public string description { get; set; }
        public long nationalid { get; set; }
        public DateTime created { get; set; }
        public string PhoneNumber { get; set; }
    }
}
