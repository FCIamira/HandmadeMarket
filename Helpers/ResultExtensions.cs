using HandmadeMarket;
using HandmadeMarket.Enum;
using Microsoft.AspNetCore.Mvc;

namespace HandmadeMarket.Helpers
    {
        public static class ResultExtensions
        {
            public static IActionResult ToActionResult<T>(this Result<T> result)
            {
                if (result.IsSuccess)
                    return new OkObjectResult(result);

                return result.Errorcode switch
                {
                    ErrorCode.NotFound => new NotFoundObjectResult(result),
                    ErrorCode.ValidationError => new BadRequestObjectResult(result),
                    ErrorCode.Unauthorized => new UnauthorizedObjectResult(result),
                    ErrorCode.Conflict => new ConflictObjectResult(result),
                    ErrorCode.BadRequest => new BadRequestObjectResult(result),
                    ErrorCode.ServerError => new ObjectResult(result) { StatusCode = 500 },
                    _ => new BadRequestObjectResult(result)
                };
            }
        }
    }


