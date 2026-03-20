namespace School.Core.Common
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public T? Data { get; }
        public Error? Error { get; }
    
        private Result(bool isSuccess, T? data, Error? error)
        {
            IsSuccess = isSuccess;
            Data = data;
            Error = error;
        }

        public static Result<T> Success(T data)
        {
            return new Result<T>(true, data, null);
        }

        public static Result<T> Failure(Error error)
        {
            return new Result<T>(false, default, error);
        }
    }
}
