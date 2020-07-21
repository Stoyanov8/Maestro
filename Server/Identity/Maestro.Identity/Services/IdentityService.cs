using Core.Constants;

namespace Identity.Services
{
    using AutoMapper;
    using Core.Models;
    using Core.Services;
    using Data.Models;
    using Identity.Data;
    using Identity.Models;
    using Maestro.Core.Extensions;
    using Maestro.Core.Messages;
    using Maestro.Identity.Models;
    using MassTransit;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using static Roles;

    public class IdentityService : DataService<User>, IIdentityService
    {
        private const string InvalidErrorMessage = "Invalid credentials.";
        private readonly UserManager<User> _userManager;
        private readonly ITokenGeneratorService _jwtTokenGeneratorService;
        private readonly IMapper _mapper;
        private readonly IBus _bus;

        public IdentityService(
            UserManager<User> userManager,
            IMapper mapper,
            ITokenGeneratorService jwtTokenGeneratorService,
            IdentityDbContext context,
            IBus bus) : base(context)
        {
            _userManager = userManager;
            _jwtTokenGeneratorService = jwtTokenGeneratorService;
            _mapper = mapper;
            _bus = bus;
        }

        public async Task<Result<User>> Register(UserRegisterModel userInput)
        {
            var user = new User
            {
                Email = userInput.Email,
                UserName = userInput.Email,
                FirstName = userInput.FirstName,
                LastName = userInput.LastName,
                PhoneNumber = userInput.PhoneNumber
            };

            var identityResult = await _userManager.CreateAsync(user, userInput.Password);

            if (identityResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, UserRole);
            }

            var errors = identityResult.Errors.Select(e => e.Description);

            return identityResult.Succeeded
                ? Result<User>.SuccessWith(user)
                : Result<User>.Failure(errors);
        }

        public async Task<Result<UserOutputModel>> Login(UserBaseInputModel userInput)
        {
            var user = await _userManager.FindByEmailAsync(userInput.Email);

            if (user == null)
            {
                return InvalidErrorMessage;
            }

            var passwordValid = await _userManager.CheckPasswordAsync(user, userInput.Password);
            if (!passwordValid)
            {
                return InvalidErrorMessage;
            }

            var roles = await _userManager.GetRolesAsync(user);

            var token = _jwtTokenGeneratorService.GenerateToken(user, roles);

            return new UserOutputModel { Token = token };
        }

        public async Task<IEnumerable<UserListOutputModel>> GetAll()
        {
            var users = await All().ToListAsync();

            var output = _mapper.Map<IEnumerable<User>, IEnumerable<UserListOutputModel>>(users);

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                output.FirstOrDefault(x => x.Email == user.Email).Roles = roles;
            }

            return output;
        }


        public async Task<Result> PromoteToEmployee(UserRoleInputModel input)
        {
            var user = await _userManager.FindByIdAsync(input.UserId);

            if (user == null)
            {
                return Result.Failure("User doesn't exist");
            }

            await _userManager.AddToRoleAsync(user, EmployeeRole);

            await _bus.Publish(new UserPromotedMessage { UserId = user.Id });

            return Result.Success;
        }

        public async Task<Result> Delete(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return Result.Failure("User doesn't exist");
            }

            var result = await _userManager.DeleteAsync(user);

            return result.Succeeded ? Result.Success : Result.Failure(result.Errors.Select(x => x.Description));
        }

        public async Task<IEnumerable<UserInformationModel>> GetAllIn(IEnumerable<string> ids)
        {
            var model = await All().Where(u => ids.Contains(u.Id)).ToListAsync();

            return _mapper.Map<IEnumerable<User>, IEnumerable<UserInformationModel>>(model);
        }
    }
}