using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.CodeAnalysis;
using prog_buddy_api.Models.Compilation;
using prog_buddy_api.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prog_buddy_api.Services
{
    public class CodeEvaluationService
    {
        public CompilationResponseModel GetDiagnostics(List<Diagnostic> diagnostics)
        {
            var result = new CompilationResponseModel();

            result.DiagnosticResponseModels = diagnostics.Select(x => new DiagnosticResponseModel { Message = x.Id}).ToList();

            return result;
        }
    }
}
