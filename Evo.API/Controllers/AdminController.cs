using AutoMapper;
using Evo.API.Extensions;
using Evo.API.Models.Requests.Accounts;
using Evo.Application.Features.Accounts.Commands.RegisterAdminUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Evo.API.Controllers
{
    public class AdminController(IMapper _mapper,IMediator _mediator) : BaseAPIController
    {
        [HttpPost("registeradmin")]
        public async Task<IActionResult> RegisterAdmin(RegisterAdminApiRequest request)   //http://localhost:5077/api/Admin/registeradmin
        {
            var command = _mapper.Map<RegisterAdminUserCommand>(request);
            var userDto = await _mediator.Send(command);

            // Map UserDto to ApiResponse<UserDto> using extension
            return this.ToApiResponse(userDto, "User registered successfully");
        }
    }
}
