using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLS.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public Guid? ProvinceId { get; set; }

        public byte IsActive { get; set; }
    }
}
