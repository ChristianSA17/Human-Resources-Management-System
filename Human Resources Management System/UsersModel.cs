using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Human_Resources_Management_System
{
    public class UsersModel
    {
        //For Id
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        //For first name
        [BsonElement("FirstName")]
        public string FirstName { get; set; }
        //for last name
        [BsonElement("LastName")]
        public string LastName { get; set; }
        //for middle name
        [BsonElement("MiddleName")]
        public string MiddleName { get; set; }
        //for contact no
        [BsonElement("ContactNo")]
        public string ContactNo { get; set; }
        //for email 
        [BsonElement("Email")]
        public string Email { get; set; }
        //for username
        [BsonElement("Username")]
        public string Username { get; set; }
        //for password
        [BsonElement("Password")]
        public string Password { get; set; }
    }
}
