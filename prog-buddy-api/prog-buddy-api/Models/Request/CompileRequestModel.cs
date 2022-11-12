using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prog_buddy_api.Models.Request
{
    public class CompileRequestModel
    {
        public string Code { get; set; }

        public string Language { get; set; }
    }
}
