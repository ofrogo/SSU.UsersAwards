using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Abstract;
using Microsoft.Extensions.Configuration;
using Model;

namespace DAL.File
{
    public class AwardJsonRepo : IRepository<Award>
    {
        private readonly List<Award> _cashAwards = new List<Award>();
        private readonly string _pathToFile;
        private const int MaxSizeCash = 20;
        private const int MinSizeCash = 9;

        public AwardJsonRepo()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("config.json")
                .Build();
            _pathToFile = config["options:path_awards"];
        }

        public Award Get(int id)
        {
            if (_cashAwards.Any(user => user.Id == id))
            {
                var temp = _cashAwards.First(a => a.Id == id);
                _cashAwards.Remove(temp);
                _cashAwards.Insert(0, temp);
                return temp;
            }

            var awards = JsonSerializer.Deserialize<List<Award>>(FileIo.Read(_pathToFile));
            return awards.Find(a => a.Id == id);
        }

        public void Add(Award t)
        {
            var awards =
                JsonSerializer.Deserialize<List<Award>>(
                    FileIo.Read(_pathToFile));
            if (t.Id == null)
            {
                t.Id = awards.Max(a => a.Id) + 1;
            }

            awards.Add(t);
            _cashAwards.Insert(0, t);
            FileIo.Write(_pathToFile, JsonSerializer.Serialize(awards));
            if (_cashAwards.Count > MaxSizeCash)
            {
                _cashAwards.RemoveRange(MinSizeCash, _cashAwards.Count - MinSizeCash);
            }
        }

        public void Delete(Award t)
        {
            var awards = JsonSerializer.Deserialize<List<Award>>(FileIo.Read(_pathToFile));
            if (!awards.Remove(t))
            {
                throw new ArgumentException("Can't delete field with this award!");
            }

            _cashAwards.Remove(t);
            FileIo.Write(_pathToFile, JsonSerializer.Serialize(awards));
        }

        public List<Award> GetAll()
        {
            return JsonSerializer.Deserialize<List<Award>>(FileIo.Read(_pathToFile));
        }
    }
}