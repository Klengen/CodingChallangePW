using System;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using Newtonsoft.Json;
using RestAPI.Models;

namespace RestAPI.Services
{
    public class ProjectService
    {
        private IMongoCollection<BsonDocument> _collection = new MongoClient("mongodb://db:27017").GetDatabase("projects").GetCollection<BsonDocument>("projects");
        

        public List<Project> GetAllProjects()
        {
            List<Project> projects = new List<Project>();
            List<BsonDocument> bson = _collection.Find(new BsonDocument()).ToList();
            foreach (var project in bson)
            {
                projects.Add(BsonSerializer.Deserialize<Project>(project));
            }
            return projects;
        }

        public Project? GetProjectById(int id)
        {
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("_id", id);
            BsonDocument projectDocument = _collection.Find(filter).FirstOrDefault();
            if (projectDocument != null)
            {
                var project = new Project
                {
                    Id = projectDocument.GetValue("_id").AsInt32,
                    City = projectDocument.GetValue("city").AsString,
                    StartDate = projectDocument.GetValue("start_date").ToUniversalTime(),
                    EndDate =  projectDocument.GetValue("end_date").ToUniversalTime(),
                    Status = projectDocument.GetValue("status").AsString,
                    Price = projectDocument.GetValue("price").AsDouble,
                    Color = projectDocument.GetValue("color").AsString
                    
                };
                Console.WriteLine(project.Status);

                return project;
            }
            return null;
        }

        public Project UpdateProject(Project project)
        {
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("_id", project.Id);
            
            BsonDocument projectDocument = _collection.FindOneAndReplace(filter, project.ToBsonDocument());
            if (projectDocument == null)
            {
                return project;
            }
            return BsonSerializer.Deserialize<Project>(projectDocument);
        }

        public Project AddProject(Project project)
        {
            _collection.InsertOne(project.ToBsonDocument());
            return project;
        }
        public List<Project> AddListOfProjects(List<Project> projects)
        {
            var bson = projects.Select(p=>
            {
                Console.WriteLine(p);
                return p.ToBsonDocument();
            });
            _collection.InsertMany(bson);
            return projects;
        }
        public long DeleteProject(int projectId)
        {
            Console.WriteLine("Service");
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("_id", projectId);
            return _collection.DeleteOne(filter).DeletedCount;
        }
    }
}