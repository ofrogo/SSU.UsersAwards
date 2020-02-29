namespace SSU.UsersAwards
{
    public class User
    {
        public long Id { get; private set; }
        public string Name { get; private set; }
        public DateOfBirth DateOfBirth { get; private set; }

        public int Age { get; private set; }


        public User()
        {
        }

        public User(long id, string name, DateOfBirth dateOfBirth, int age)
        {
            Id = id;
            Name = name;
            DateOfBirth = dateOfBirth;
            Age = age;
        }
        
        
    }
}