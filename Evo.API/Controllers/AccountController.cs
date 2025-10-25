using AutoMapper;
using Evo.API.Extensions;
using Evo.API.Models.Requests.Accounts;
using Evo.Application.Features.Accounts.Commands.GoogleLogin;
using Evo.Application.Features.Accounts.Commands.LoginUser;
using Evo.Application.Features.Accounts.Commands.RegisterAdminUser;
using Evo.Application.Features.Accounts.Commands.RegisterCustomerUser;
using Evo.Application.Features.Accounts.Commands.RegisterServiceProviderUser;
using Evo.Application.Features.Accounts.Commands.RegisterStaffUser;
using Evo.Application.Features.Accounts.Dtos;
using Evo.Application.Features.Accounts.Requests;
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

        [HttpPost("registerserviceprovider")] // http://localhost:5077/api/account/registerserviceprovider
        public async Task<IActionResult> RegisterServiceProvider(RegisterServiceProviderApiRequest request)
        {
            var command = _mapper.Map<RegisterServiceProviderUserCommand>(request);
            var userDto = await _mediator.Send(command);
            return this.ToApiResponse(userDto, "Service provider registered successfully");
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login([FromBody] LoginRequest request)  //http://localhost:5077/api/account/login
        {
            var command = _mapper.Map<LoginUserCommand>(request);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
