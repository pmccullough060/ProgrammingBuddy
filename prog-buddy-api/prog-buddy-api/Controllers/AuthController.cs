using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using prog_buddy_api.Models.Request;
using prog_buddy_api.Models.Response;
using prog_buddy_api.Services;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using prog_buddy_api.Models.DTO;
using Data;
using Microsoft.AspNetCore.Mvc;
using prog_buddy_api.AuthHelpers;
using Data.Enums;
using Microsoft.EntityFrameworkCore;
using Data.Tables;

namespace prog_buddy_api.Controllers
{
    public class AuthController
    {
        private readonly Context _context;

        public AuthController(Context context)
        {
            _context = context;
        }

        [FunctionName("Login")]
        public async Task<IActionResult> Login(
        [HttpTrigger(AuthorizationLevel.Anonymous, "put", "post", Route = "Login")] HttpRequest req, ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var loginRequestModel = JsonConvert.DeserializeObject<LoginRequestModel>(requestBody);

            // Check is there is an email associated with this account:
            var existingUser = _context.Users.Include(user => user.HashedPassword)
                                             .FirstOrDefault(user => user.Email == loginRequestModel.Email.ToLower());

            if (existingUser is null)
            {
                return new UnauthorizedResult();
            }

            // Hash the password:
            var correctPassword = loginRequestModel.Password.VerifyPassword(existingUser.HashedPassword.HashedPassword, existingUser.HashedPassword.Salt);


            // DB call to validate the user etc...
            var user = new UserDTO
            {
                UserName = loginRequestModel.Email,
                Role = UserRoles.User,
            };

            var token = AuthenticationService.IssueJwtToken(user);

            var responseModel = new LoginResponseModel
            {
                Token = token,
                Expiry = DateTime.Now,
            };

            return new JsonResult(responseModel);
        }

        [FunctionName("Register")]
        public async Task<IActionResult> Register(
        [HttpTrigger(AuthorizationLevel.Anonymous, "put", "post", Route = "Register")] HttpRequest req, ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var registerRequestModel = JsonConvert.DeserializeObject<RegisterRequestModel>(requestBody);

            // Check that the email hasn't already been registered:
            var emailAlreadyRegistered = _context.Users.Any(user => user.Email == registerRequestModel.Email.ToLower());

            if (emailAlreadyRegistered)
            {
                return new ConflictObjectResult("This emial address has already been registered");
            }

            // TODO: Move this to the service layer!

            // Hash the password and generate salt:

            byte[] salt;

            var hashedPassword = registerRequestModel.Password.HashPassword(out salt);

            // Build the user entity and save to context:
            var user = new User
            {
                Email = registerRequestModel.Email.ToLower(),
                FirstName = registerRequestModel.FirstName,
                LastName = registerRequestModel.LastName,
                Role = UserRoles.User,
                HashedPassword = new PasswordHash
                {
                    HashedPassword = hashedPassword,
                    Salt = salt,
                },
            };

            // Add the user to the database:
            _context.Users.Add(user);

            // Persist the tracked changes to the db:
            _context.SaveChanges();

            // Issue a jwt token:
            var userDTO = new UserDTO
            {
                UserName = registerRequestModel.Email,
                Role = UserRoles.User,
            };

            // Issue a jet token with the response
            var token = AuthenticationService.IssueJwtToken(userDTO);

            var responseModel = new LoginResponseModel
            {
                Token = token,
                Expiry = DateTime.Now,
            };

            return new JsonResult(responseModel);
        }
    }
}
