using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3.Domain.Core
{
    public class User
    {
        public Guid UserId { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }

        public User(Guid userId, string name, string email)
        {
            UserId = userId;
            Name = name;
            Email = email;
        }
    }
}
