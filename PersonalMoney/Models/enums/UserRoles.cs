using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace PersonalMoney.Models.enums
{
    public enum UserRoles
    {
        [EnumMember(Value = "ADMIN")]
        ADMIN,
        [EnumMember(Value = "USER")]
        USER
    }
}