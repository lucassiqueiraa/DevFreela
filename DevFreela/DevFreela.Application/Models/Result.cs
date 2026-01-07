namespace DevFreela.Application.Models
{
    public enum ErrorType
    {
        Validation,
        NotFound,
        Conflict,
        Failure
    }

    public sealed record Error(ErrorType Code, string Description);

    public sealed record Result<T>
    {
        public bool IsSuccess { get; }
        public T? Data { get; }
        public Error? Error { get; } 

        // Private constructors
        private Result(T data)
        {
            IsSuccess = true;
            Data = data;
            Error = null;
        }

        private Result(Error error)
        {
            IsSuccess = false;
            Data = default;
            Error = error;
        }

        // (Factories)
        public static Result<T> Success(T data) => new(data);
        public static Result<T> Failure(Error error) => new(error);

        // Method overload for easier failure creation
        public static Result<T> Failure(ErrorType type, string message)
            => new(new Error(type, message));
    }

    // O Result "Void" (para comandos que não retornam nada ou só ID)
    public sealed record Result
    {
        public bool IsSuccess { get; }
        public Error? Error { get; }

        private Result(bool isSuccess, Error? error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success() => new(true, null);
        public static Result Failure(Error error) => new(false, error);

        public static Result Failure(ErrorType type, string message)
            => new(false, new Error(type, message));
    }
}