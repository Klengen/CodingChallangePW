using MongoDB.Bson.Serialization.Attributes;

namespace RestAPI.Models;

//This class is only to Migrate the Data from the File
public class ProjectMigrate
{
    [BsonId]
    public int Id { get; set; }
    public string City { get; set; }
    public string start_date { get; set; }
    public string end_date { get; set; }
    public double Price { get; set; }
    public string Status { get; set; }
    public string Color { get; set; }
}