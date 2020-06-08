using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Model
{
    public class Award
    {
        public int Id { get; set; }
        public string Title { get; set; }
        
        [JsonIgnore]
        public List<User> Users { get; set; }
        
        public Award(){}

        public Award(string title)
        {
            Title = title;
        }

        public Award(int id, string title)
        {
            Id = id;
            Title = title;
        }

        public override string ToString()
        {
            return $"[{Id}] {Title ?? "---"}" +
                   $"[{string.Join(",", Users.ConvertAll(a => $"{a.Id} {a.Name} {a.Age} {a.DateOfBirth}"))}]";
        }
    }
}