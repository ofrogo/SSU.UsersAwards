using System.Collections.Generic;
using System.Text.Json;
using Abstract;
using Microsoft.Extensions.Configuration;
using Model;

namespace DAL.File
{
    public class UsersAwardsJsonRepo:IRepository<UserAward>
    {
        private readonly string _pathToFile;

        public UsersAwardsJsonRepo()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("config.json")
                .Build();
            _pathToFile = config["options:path_users_awards"];
        }

        public UserAward Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Add(UserAward userAward)
        {
            var temp = JsonSerializer.Deserialize<HashSet<UserAward>>(FileIo.Read(_pathToFile));
            temp.Add(userAward);
            FileIo.Write(_pathToFile, JsonSerializer.Serialize(temp));
        }

        public void Delete(UserAward userAward)
        {
            var temp = JsonSerializer.Deserialize<HashSet<UserAward>>(FileIo.Read(_pathToFile));
            temp.Remove(userAward);
            FileIo.Write(_pathToFile, JsonSerializer.Serialize(temp));
        }

        public List<UserAward> GetAll()
        {
            var temp = JsonSerializer.Deserialize<HashSet<UserAward>>(FileIo.Read(_pathToFile));
            return new List<UserAward>(temp);
        }
    }
}