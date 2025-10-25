using AutoMapper;
using Evo.API.Extensions;
using Evo.API.Models.Requests.Customers;
using Evo.Application.Features.Customers.Commands;
using Evo.Application.Features.Customers.Dtos;
using Evo.Application.Features.Customers.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Evo.API.Controllers
{
    public class CustomerController(IMediator _mediator, IMapper _mapper) : BaseAPIController
    {

        // GET: http://localhost:5077/api/customer
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _mediator.Send(new GetAllCustomersQuery());
            return this.ToApiResponse(customers, "Customers retrieved successfully");
        }

        // GET: http://localhost:5077/api/customer/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var customer = await _mediator.Send(new GetCustomerByIdQuery { Id = id });
            return this.ToApiResponse(customer, "Customer retrieved successfully");
        }

        // PUT: http://localhost:5077/api/customer
        [HttpPut]
        public async Task<IActionResult> UpdateCustomer(UpdateCustomerApiRequest request)
        {
            var command = _mapper.Map<UpdateCustomerCommand>(request);
            var updatedCustomer = await _mediator.Send(command);
            return this.ToApiResponse(updatedCustomer, "Customer updated successfully");
        }

        // DELETE: http://localhost:5077/api/customer
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(string id)
        {
            var command = new DeleteCustomerCommand { Id = id };
            await _mediator.Send(command);
            return this.ToApiResponse<object>(null, "Customer deleted successfully");
        }
    }
}
