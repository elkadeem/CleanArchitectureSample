using System;
using System.Collections.Generic;

namespace Application.Core
{
    public class User
    {        
        public User(int id, string userName, string name, string email)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException($"'{nameof(userName)}' cannot be null or whitespace.", nameof(userName));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException($"'{nameof(email)}' cannot be null or whitespace.", nameof(email));
            }

            Id = id;
            UserName = userName;
            Name = name;
            Email = email;
            Active = true;
        }
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string? Country { get; set; }
        public bool Active { get; set; }
        public List<string> Roles { get; private set; } = new List<string>();
        public void UpdateUser(string userName, string name, string email)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException($"'{nameof(userName)}' cannot be null or whitespace.", nameof(userName));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException($"'{nameof(email)}' cannot be null or whitespace.", nameof(email));
            }

            UserName = userName;
            Name = name;
            Email = email;            
        }

    }
}
