using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Model
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateOfBirth DateOfBirth { get; set; }
        public int? Age { get; set; }

        [JsonIgnore] public List<Award> Awards { get; set; }

        public User()
        {
        }

        public User(string name, DateOfBirth dateOfBirth, int? age)
        {
            Name = name;
            DateOfBirth = dateOfBirth;
            Age = age;
        }

        public User(long id, string name, DateOfBirth dateOfBirth, int? age)
        {
            Id = id;
            Name = name;
            DateOfBirth = dateOfBirth;
            Age = age;
        }

        public override string ToString()
        {
            return
                $"[{Id}] {Name ?? "---"} {(DateOfBirth != null ? DateOfBirth.ToString() : "---")} {Age.ToString() ?? "---"} " +
                $"[{string.Join(",", Awards.ConvertAll(a => $"{a.Id} {a.Title}"))}]";
        }
    }
}