using System;
using System.Collections.Generic;
using System.Text.Json;
using SSU.UsersAwards;

namespace VL
{
    class Program
    {
        static void Main(string[] args)
        {
            List<User> users = new List<User>()
            {
                new User(0, "Nataliya", new DateOfBirth(9, 6, 1999), 20),
                new User(1, "Danil", new DateOfBirth(18, 11, 1999), 20)
            };
            Console.WriteLine(JsonSerializer.Serialize(users));
            
        }
    }
}