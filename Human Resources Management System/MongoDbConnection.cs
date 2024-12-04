using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using BCrypt.Net;

namespace Human_Resources_Management_System
{
    public class MongoDbConnection
    {
        private readonly IMongoDatabase _database;
        public MongoDbConnection() {
            var client = new MongoClient("mongodb://localhost:27017");
            _database = client.GetDatabase("HumanResourcesManagementDb");

        }

        public IMongoCollection<UsersModel> GetUsersCollection()
        {
            return _database.GetCollection<UsersModel>("Users");
        }
        public async Task AddUserAsync(UsersModel user)
        {
            try
            {
                // Hash the password before storing it
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                var collection = GetUsersCollection();
                await collection.InsertOneAsync(user);
            }
            catch (Exception ex)
            {
                // Log or handle exception as needed
                throw new ApplicationException("Failed to add user to the collection.", ex);
            }
        }
        public bool VerifyPassword(string email, string plainTextPassword)
        {
            var collection = GetUsersCollection();
            var user = collection.Find(u => u.Password == email).FirstOrDefault(); // Synchronous version

            if (user == null)
                return false; // User not found

            return BCrypt.Net.BCrypt.Verify(plainTextPassword, user.Password);
        }


    }
}
