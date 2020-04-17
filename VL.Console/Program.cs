using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using BLL;
using BLL.Interfaces;
using Microsoft.Extensions.Configuration;
using Model;

namespace VL
{
    static class Program
    {
        private static IConfigurationRoot _config;
        private static string _typeDb;

        public static void Main()
        {
            try
            {
                Console.WriteLine("Trying to find file \"config.json\"...");
                if (File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json")))
                {
                    Console.WriteLine("File is detected.");

                    Console.WriteLine("Start reading config.json...");
                    _config = new ConfigurationBuilder()
                        .AddJsonFile("config.json")
                        .Build();

                    var configurationProvider = _config.Providers.First();
                    if (configurationProvider.TryGet("type_data_base", out _typeDb))
                    {
                        var typeDataBase = Enum.Parse<TypeDataBase>(_typeDb, true);
                        Console.WriteLine("Reading is completed.");
                        switch (typeDataBase)
                        {
                            case TypeDataBase.File:
                                Console.WriteLine("Trying to find file with data base...");
                                if (configurationProvider.TryGet("options:path", out var path))
                                {
                                    if (File.Exists(path))
                                    {
                                        Console.WriteLine($"File {path.Split('\\').Last()} is found.");
                                    }
                                    else
                                    {
                                        throw new FileNotFoundException("File with data base was not found", path);
                                    }
                                }
                                else
                                {
                                    throw new DataException("Config file doesn't contain path of file with data base!");
                                }

                                break;
                            case TypeDataBase.RelationalDb:
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }

                        Console.WriteLine("Starting command line...");

                        IService<User> userService = new UserService(typeDataBase);
                        if (CmdUser(userService))
                        {
                        }
                    }
                    else
                    {
                        throw new DataException("Config file doesn't contain \"type_data_base\"!");
                    }
                }
                else
                {
                    throw new FileNotFoundException("Config file doesn't exist!", "config.json");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

        private static bool CmdUser(IService<User> service)
        {
            while (true)
            {
                Console.Write("> ");
                var txt = Console.ReadLine();
                if (txt == null || txt.Trim().Length == 0) continue;
                var command = txt.TrimStart().TrimEnd().Split(' ');
                Commands typeCommand;
                try
                {
                    typeCommand = Enum.Parse<Commands>(command[0], true);
                }
                catch
                {
                    Console.WriteLine(
                        $"Required \"{command}\" was not founded in list commands. Enter \"Help\" to see list of commands.");
                    continue;
                }

                try
                {
                    switch (typeCommand)
                    {
                        case Commands.Add:
                            var addArgs = new[] {"help", "n", "dob", "a"};
                            var args = new Dictionary<string, string>();
                            foreach (var s in txt.Substring(command[0].Length).Split(" -"))
                            {
                                if (s.Trim() == "")
                                {
                                    continue;
                                }

                                if (s.Contains("-"))
                                {
                                    args.Add(s.Substring(1), "True");
                                }
                                else
                                {
                                    var tmp = s.Split("=");
                                    args.Add(tmp[0], tmp[1]);
                                }
                            }

                            if (args.Count == 0)
                            {
                                throw new ArgumentException(
                                    "This command required arguments. Example: add -n=<Name> -dob=<Date of Birth> -a=<Age>");
                            }

                            foreach (var k in args.Keys.Where(k => !addArgs.Contains(k)))
                            {
                                throw new ArgumentException($"Incorrect argument {k}.");
                            }


                            var name = args.ContainsKey("n") ? args["n"] : null;
                            var dob = args.ContainsKey("dob") ? new DateOfBirth(args["dob"]) : null;
                            var age = args.ContainsKey("a")
                                ? int.Parse(args["a"] ?? throw new ArgumentException("Incorrect value in -a (Age)."))
                                : (int?) null;
                            var user = new User(name, dob, age);
                            Console.WriteLine(service.Add(user) > 0
                                ? "User was added."
                                : $"Error while adding user with attributes: {user}");

                            break;
                        case Commands.List:
                            foreach (var u in service.GetAll())
                            {
                                Console.WriteLine(u.ToString());
                            }

                            break;
                        case Commands.Delete:
                            break;
                        case Commands.Get:
                            break;
                        case Commands.Exit:
                            return true;
                        case Commands.Help:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(typeCommand), typeCommand, "Illegal command.");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}