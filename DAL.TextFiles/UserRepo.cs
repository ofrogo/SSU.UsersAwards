using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using SSU.UsersAwards;

namespace DAL
{
    public static class UserRepo
    {
        private static readonly List<User> CashUsers = new List<User>();

        public static User GetUser(int id)
        {
            if (CashUsers.Any(user => user.Id == id))
            {
                var temp = CashUsers.First(user => user.Id == id);
                CashUsers.Remove(temp);
                CashUsers.Insert(0, temp);
                return temp;
            }

            var users = JsonSerializer.Deserialize<List<User>>(FileIO.ReadFromFile("test.json"));
            return users.Find(user => user.Id == id);
        }

        public static void AddUser(User user)
        {
            var users =
                JsonSerializer.Deserialize<List<User>>(
                    FileIO.ReadFromFile("test.json")); //TODO read fileName from property
            users.Add(user);
            CashUsers.Insert(0, user);
            FileIO.WriteToFile("test.json", JsonSerializer.Serialize(users));
            if (CashUsers.Count > 20)
            {
                CashUsers.RemoveRange(9, CashUsers.Count - 9);
            }
        }

        public static void DeleteUser(User user)
        {
            var users = JsonSerializer.Deserialize<List<User>>(FileIO.ReadFromFile("test.json"));
            if (!users.Remove(user))
            {
                throw new ArgumentException("Can't delete field with this user!");
            }

            CashUsers.Remove(user);
            FileIO.WriteToFile("test.json", JsonSerializer.Serialize(users));
        }

        public static List<User> GetUsers()
        {
            return JsonSerializer.Deserialize<List<User>>(FileIO.ReadFromFile("test.json"));
        }
    }
}