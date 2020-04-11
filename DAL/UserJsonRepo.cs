using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using DAL.Interfaces;
using Model;

namespace DAL
{
    public class UserJsonRepo: IUserRepository
    {
        private readonly List<User> _cashUsers = new List<User>();

        public User Get(int id)
        {
            if (_cashUsers.Any(user => user.Id == id))
            {
                var temp = _cashUsers.First(user => user.Id == id);
                _cashUsers.Remove(temp);
                _cashUsers.Insert(0, temp);
                return temp;
            }

            var users = JsonSerializer.Deserialize<List<User>>(FileIo.ReadFromFile("test.json"));
            return users.Find(user => user.Id == id);
        }

        public void Add(User user)
        {
            var users =
                JsonSerializer.Deserialize<List<User>>(
                    FileIo.ReadFromFile("test.json")); //TODO read fileName from property
            users.Add(user);
            _cashUsers.Insert(0, user);
            FileIo.WriteToFile("test.json", JsonSerializer.Serialize(users));
            if (_cashUsers.Count > 20)
            {
                _cashUsers.RemoveRange(9, _cashUsers.Count - 9);
            }
        }

        public void Delete(User user)
        {
            var users = JsonSerializer.Deserialize<List<User>>(FileIo.ReadFromFile("test.json"));
            if (!users.Remove(user))
            {
                throw new ArgumentException("Can't delete field with this user!");
            }

            _cashUsers.Remove(user);
            FileIo.WriteToFile("test.json", JsonSerializer.Serialize(users));
        }

        public List<User> GetAll()
        {
            return JsonSerializer.Deserialize<List<User>>(FileIo.ReadFromFile("test.json"));
        }
    }
}