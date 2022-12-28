using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prog_buddy_api.Models.Request
{
    public class LoginRequestModel
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
