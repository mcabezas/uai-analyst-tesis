using static Security.Model.Idiom;

namespace Security.Model
{
    public class User : Entity
    {
        public static readonly User NullUser = new User();
        public User()
        {
            Id = NullId;
            FirstName = "";
            LastName = "";
            Email = "";
            Idiom = NullIdiom;
        }

        public User(string firstName, string lastName, Idiom idiom)
        {
            FirstName = firstName;
            LastName = lastName;
            Idiom = idiom;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Idiom Idiom { get; set; }
    }
}