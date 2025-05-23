using HandmadeMarket.Enum;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HandmadeMarket
{
    public class Result<T>
    {
        public bool IsSuccess { get; init; }
        public T? Data { get; init; }
        public ErrorCode Errorcode { get; init; }
        public string Message { get; init; }

        //public static Result<T> Success(T data) => new Result<T>() { IsSuccess = true, Data = data };
        //public static Result<T> Failure(string msg) => new Result<T>() { IsSuccess = false, Error = msg };
        public static Result<T> Success(T data, string message = "")

        {

            return new Result<T>

            {

                Data = data,

                IsSuccess = true,

                Message = message,

                Errorcode = ErrorCode.None,

            };

        }



        public static Result<T> Failure(ErrorCode errorCode, string message = "")

        {

            return new Result<T>

            {

                Data = default,

                IsSuccess = false,

                Message = message,

                Errorcode = errorCode,

            };

        }

    }
}
