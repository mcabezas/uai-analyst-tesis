using System.Numerics;

namespace Security.Model
{
    public class User
    {
        public User(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public BigInteger Id { get; set; }
        public string FirstName { get; }
        public string LastName { get; }
    }
}