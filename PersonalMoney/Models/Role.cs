using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace PersonalMoney.Models
{
    public class Role : IdentityRole
    {
        public Role():base()
        {
        }

        public Role(string roleName) : base(roleName)
        {
        }

        public virtual ICollection<User> Users { get; set; }

    }
}