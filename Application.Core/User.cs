using System;
using System.Globalization;

namespace Application.Core
{
    public class User
    {
        public User() { }
        public User(int id, string name, string email)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException($"'{nameof(email)}' cannot be null or whitespace.", nameof(email));
            }

            Id = id;
            Name = name;
            Email = email;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }

    }
}
