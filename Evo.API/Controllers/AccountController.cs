using AutoMapper;
using Evo.API.Extensions;
using Evo.API.Models.Requests.Accounts;
using Evo.Application.Features.Accounts.Commands.RegisterCustomerUser;
using Evo.Application.Features.Accounts.Commands.GoogleLogin;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace Evo.API.Controllers
{
    public class AccountController(IMapper _mapper,IMediator _mediator) : BaseAPIController
    {
        [HttpPost("registercustomer")]
        public async Task<IActionResult> RegisterCustomer(RegisterCustomerApiRequest request)   //http://localhost:5077/api/account/registercustomer
        {
            var command = _mapper.Map<RegisterCustomerUserCommand>(request);
            var userDto = await _mediator.Send(command);

            // Map UserDto to ApiResponse<UserDto> using extension
            return this.ToApiResponse(userDto, "User registered successfully");
        }

        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin(GoogleLoginApiRequest request) // POST /api/account/google-login //http://localhost:5077/api/account/google-login
        {
            var command = _mapper.Map<GoogleLoginCommand>(request);
            var userDto = await _mediator.Send(command);
            return this.ToApiResponse(userDto, "Google sign-in successful");
        }
    }
}
