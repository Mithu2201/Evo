using Evo.API.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Evo.API.Extensions
{
    public static class ControllerExtensions
    {
        public static IActionResult ToApiResponse<T>(this ControllerBase controller, T dto, string message = "")
        {
            var response = new ApiResponse<T>
            {
                Data = dto,
                Message = message,
                Success = true
            };
            return controller.Ok(response);
        }

        public static IActionResult ToErrorResponse(this ControllerBase controller, string message, int statusCode = 400)
        {
            var response = new ApiResponse<object>
            {
                Success = false,
                Message = message,
                Data = null!
            };
            return controller.StatusCode(statusCode, response);
        }
    }
}
