using System;

namespace CarFinder.Models
{
    public class Car
    {
        public int id { get; set; }
        public string make { get; set; }
        public string model_name { get; set; }
        public string model_trim { get; set; }
        public string model_year { get; set; }
        public string body_style { get; set; }
        public string engine_position { get; set; }
        public string engine_cc { get; set; }
        public string engine_num_cyl { get; set; }
        public string engine_type { get; set; }
        public string engine_power_ps { get; set; }
        public string engine_power_rpm { get; set; }
        public string engine_torque_nm { get; set; }
        public string engine_torque_rpm { get; set; }
        public string engine_fuel { get; set; }
        public string top_speed_kph { get; set; }
        public string zero_to_100_kph { get; set; }
        public string drive_type { get; set; }
        public string transmission_type { get; set; }
        public string seats { get; set; }
        public string doors { get; set; }
        public string weight_kg { get; set; }
        public string lkm_hwy { get; set; }
        public string lkm_mixed { get; set; }
        public string lkm_city { get; set; }
    }

    public class Recalls
    {
        public int Count { get; set; }
        public string Message { get; set; }
        public RecallItem[] Results { get; set; }
    }

    public class RecallItem
    {
        public string NHTSACampaignNumber { get; set; }
        public DateTime ReportReceivedDate { get; set; }
        public string Component { get; set; }
        public string Summary { get; set; }
        public string Conequence { get; set; }
        public string Remedy { get; set; }
    }

    public class CarData
    {
        public Car car { get; set; }
        public Recalls recalls { get; set; }
        public string[] imageURLS { get; set; }
    }

    public class CarsDb : System.Data.Entity.DbContext
    {
        public CarsDb() : base("DefaultConnection")
        {
        }

        public static CarsDb Create()
        {
            return new CarsDb();
        }
    }
}