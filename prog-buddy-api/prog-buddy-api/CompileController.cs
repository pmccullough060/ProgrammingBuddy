using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using prog_buddy_api.Models.Request;
using prog_buddy_api.Services;
using prog_buddy_api.Models.Compilation;
using prog_buddy_api.Models.Response;

namespace prog_buddy_api
{
    public static class CompileController
    {
        [FunctionName("Compile")]
        public static async Task<CompilationResponseModel> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", "post", Route = "Compile")] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var compileRequestModel = JsonConvert.DeserializeObject<CompileRequestModel>(requestBody);

            var compiler = new CompilerService();

            // Compile the code and create a dll:
            var result = compiler.CompileSource(compileRequestModel.Code);

            if (result.Success)
            {
                var method = result.Assembly.GetType("Program").GetMethod("Main");

                method.Invoke(null, null);
            }

            var codeEvaluationService = new CodeEvaluationService();

            return codeEvaluationService.GetDiagnostics(result.Diagnostics);
        }
    }
}
