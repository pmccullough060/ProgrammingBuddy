using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prog_buddy_api.Models.Compilation
{
    public record Position
    {
        public int Line { get; init; }

        public int Start { get; init; }

        public int End { get; init; }
    }
}
