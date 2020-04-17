using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using DAL.Interfaces;
using Microsoft.Extensions.Configuration;
using Model;

namespace DAL
{
    public class UserJsonRepo: IUserRepository
    {
        private readonly List<User> _cashUsers = new List<User>();
        private readonly string _pathToFile;

        public UserJsonRepo()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("config.json")
                .Build();
            _pathToFile = config["options:path"];
        }

        public User Get(int id)
        {
            if (_cashUsers.Any(user => user.Id == id))
            {
                var temp = _cashUsers.First(user => user.Id == id);
                _cashUsers.Remove(temp);
                _cashUsers.Insert(0, temp);
                return temp;
            }

            var users = JsonSerializer.Deserialize<List<User>>(FileIo.Read(_pathToFile));
            return users.Find(user => user.Id == id);
        }

        public void Add(User user)
        {
            var users =
                JsonSerializer.Deserialize<List<User>>(
                    FileIo.Read(_pathToFile));
            if (user.Id == null)
            {
                user.Id = users.Max(user1 => user1.Id) + 1;
            }
            users.Add(user);
            _cashUsers.Insert(0, user);
            FileIo.Write(_pathToFile, JsonSerializer.Serialize(users));
            if (_cashUsers.Count > 20)
            {
                _cashUsers.RemoveRange(9, _cashUsers.Count - 9);
            }
        }

        public void Delete(User user)
        {
            var users = JsonSerializer.Deserialize<List<User>>(FileIo.Read(_pathToFile));
            if (!users.Remove(user))
            {
                throw new ArgumentException("Can't delete field with this user!");
            }

            _cashUsers.Remove(user);
            FileIo.Write(_pathToFile, JsonSerializer.Serialize(users));
        }

        public List<User> GetAll()
        {
            return JsonSerializer.Deserialize<List<User>>(FileIo.Read(_pathToFile));
        }
    }
}