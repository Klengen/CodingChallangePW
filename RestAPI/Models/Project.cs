using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RestAPI.Models;


namespace RestAPI.Models
{
    /*public enum Status
    {
        Never,
        Once,
        Seldom,
        Often,
        Yearly,
        Monthly,
        Weekly,
        Daily
    }*/
    public class Project
    {
        [BsonId]
        public int Id { get; set; }

        [BsonElement("city")]
        public string City { get; set; }

        [BsonElement("start_date")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime StartDate { get; set; }

        [BsonElement("end_date")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime EndDate { get; set; }

        [BsonElement("price")]
        public double Price { get; set; }

        [BsonElement("status")]

        public string Status { get; set; }

        [BsonElement("color")]
        public string Color { get; set; }
        
        
        public Project(int id, string city, DateTime startDate, DateTime endDate, double price, string status, string color)
        {
            Id = id;
            City = city;
            StartDate = startDate;
            EndDate = endDate;
            Price = price;
            Status = status;
            Color = color;
        }
        
        public Project(){}
    }
}