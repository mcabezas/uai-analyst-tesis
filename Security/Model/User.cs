using System.Numerics;

namespace Security.Model
{
    public class User
    {
        public User(string firstName, string lastName, BigInteger userId)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
        }

        public BigInteger UserId { get; }
        public string FirstName { get; }
        public string LastName { get; }
    }
}