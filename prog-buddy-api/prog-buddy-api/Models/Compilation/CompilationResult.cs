using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace prog_buddy_api.Models.Compilation
{
    public class CompilationResult
    {
        public bool Success { get; set; } // If code was compilable or not

        public Assembly Assembly { get; set; } // Compiled assembly

        public SyntaxNode Root { get; set; } // Null if compilation error

        public List<Diagnostic> Diagnostics { get; set; } // Diagnostic results
    }
}
