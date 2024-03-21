using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalMoney.dto.RoleDto
{
    public class RoleDTO
    {
        [Required(ErrorMessage = "{0} cannot be empty")]
        [StringLength(256, MinimumLength = 3, ErrorMessage = "{0} must has from {2} to {1} character length")]
        public string Name { set; get; }

        public class Update
        {
            [Required(ErrorMessage = "{0} cannot be empty")]
            public string Id { set; get; }
            [Required(ErrorMessage = "{0} cannot be empty")]
            [StringLength(256, MinimumLength = 3, ErrorMessage = "{0} must has from {2} to {1} character length")]
            public string Name { set; get; }
        }
    }
}