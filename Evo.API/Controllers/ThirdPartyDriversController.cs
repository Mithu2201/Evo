using Evo.API.Extensions;
using Evo.Application.Features.ThirdPartyDrivers.Commands.Create;
using Evo.Application.Features.ThirdPartyDrivers.Commands.Update;
using Evo.Application.Features.ThirdPartyDrivers.Commands.Delete;
using Evo.Application.Features.ThirdPartyDrivers.Dtos;
using Evo.Application.Features.ThirdPartyDrivers.Queries.GetById;
using Evo.Application.Features.ThirdPartyDrivers.Queries.List;
using Evo.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Evo.API.Controllers
{
    [ApiController]
    [Route("api/thirdparty-drivers")]
    public class ThirdPartyDriversController : ControllerBase  
    {
        private readonly IMediator _mediator;
        public ThirdPartyDriversController(IMediator mediator) => _mediator = mediator;

        [HttpPost("createthirdpartydriver")]
        public async Task<IActionResult> Create([FromBody] CreateThirdPartyDriverDto dto, CancellationToken ct)    //http://localhost:5077/api/thirdparty-drivers/createthirdpartydriver
        {
            var created = await _mediator.Send(new CreateThirdPartyDriverCommand(dto), ct);
            return this.ToApiResponse(created, "Driver created");
        }

        [HttpGet("{driverId:guid}")]                                                                               //http://localhost:5077/api/thirdparty-drivers/{driverId:guid}
		public async Task<IActionResult> GetById([FromRoute] Guid driverId, CancellationToken ct)
        {
            var dto = await _mediator.Send(new GetThirdPartyDriverByIdQuery(driverId), ct);
            if (dto is null) return this.ToErrorResponse("Not found", 404);
            return this.ToApiResponse(dto);
        }

        [HttpGet]
        public async Task<IActionResult> List([FromQuery] DriverStatus? status, [FromQuery] DriverVerificationStatus? verificationStatus, [FromQuery] bool? isAvailable, CancellationToken ct)
        {
            var list = await _mediator.Send(new ListThirdPartyDriversQuery(status, verificationStatus, isAvailable), ct);
            return this.ToApiResponse(list);
        }

        [HttpPut("{driverId:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid driverId, [FromBody] UpdateThirdPartyDriverDto dto, CancellationToken ct)
        {
            var ok = await _mediator.Send(new UpdateThirdPartyDriverCommand(driverId, dto), ct);
            if (!ok) return this.ToErrorResponse("Not found", 404);
            return this.ToApiResponse(true, "Updated");
        }

        [HttpDelete("{driverId:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid driverId, CancellationToken ct)
        {
            var ok = await _mediator.Send(new DeleteThirdPartyDriverCommand(driverId), ct);
            if (!ok) return this.ToErrorResponse("Not found", 404);
            return this.ToApiResponse(true, "Deleted");
        }
    }
}
