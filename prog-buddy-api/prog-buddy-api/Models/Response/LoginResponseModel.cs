using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prog_buddy_api.Models.Response
{
    public class LoginResponseModel
    {
        public string Token { get; set; }

        public DateTime Expiry { get; set; }
    }
}
