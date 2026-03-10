using Microsoft.AspNetCore.Mvc;
using DevFreela.Application.Models;

namespace DevFreela.API.Extensions
{
    public static class ApiBehaviorExtensions
    {
        public static IServiceCollection AddCustomValidationResponse(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .SelectMany(x => x.Value.Errors)
                        .Select(x => x.ErrorMessage)
                        .ToList();

                    var errorMessage = string.Join("; ", errors);

                    var result = Result.Failure(ErrorType.Validation, errorMessage);

                    return new BadRequestObjectResult(result);
                };
            });

            return services;
        }
    }
}