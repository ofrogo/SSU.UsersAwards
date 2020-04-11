using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.Json;
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
                    if (configurationProvider == null)
                        throw new JsonException("Invalid JSON file!");
                    if (configurationProvider.TryGet("type_data_base", out _typeDb))
                    {
                        var typeDataBase = Enum.Parse<TypeDataBase>(_typeDb, true);
                        Console.WriteLine("Reading is completed.");
                        Console.WriteLine("Starting command line...");

                        IService<User> userService = new UserService(typeDataBase);
                        if (Cmd(userService))
                        {
                        }
                    }
                    else
                    {
                        throw new DataException("JSON file doesn't contain \"type_data_base\"!");
                    }
                }
                else
                {
                    throw new IOException("File \"config.json\" doesn't exist!");
                }
            }
            catch (IOException ioException)
            {
                Console.WriteLine(ioException.Message);
            }
            catch (JsonException jsonException)
            {
                Console.WriteLine(jsonException.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static bool Cmd<T>(IService<T> userService)
        {
            while (true)
            {
                Console.Write("> ");
                var strCommand = Console.ReadLine();
                Commands command;
                try
                {
                    command = Enum.Parse<Commands>(strCommand, true);
                }
                catch (Exception e)
                {
                    Console.WriteLine(
                        $"Required \"{strCommand}\" was not founded in list commands. Enter \"Help\" to see list of commands.");
                    continue;
                }

                switch (command)
                {
                    case Commands.Add:
                        break;
                    case Commands.List:
                        break;
                    case Commands.Delete:
                        break;
                    case Commands.Get:
                        break;
                    case Commands.Exit:
                        break;
                    case Commands.Help:
                        break;
                    default:
                        Console.WriteLine();
                        break;
                }
            }
        }
    }
}