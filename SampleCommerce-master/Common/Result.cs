namespace SampleCommerce.Common
{
    public class Result
    {
        public bool Success { get; private set; }
        public string Message { get; private set; }
        public List<string> Errors { get; private set; }

        protected Result(bool success, string message, List<string>? errors = null)
        {
            Success = success;
            Message = message;
            Errors = errors ?? new List<string>();
        }

        public static Result Ok() => new (true, "The operation was successfully executed.");
        public static Result Fail(string message) => new(false, message);
        public static Result Fail(List<string> errors) => new(false, "At least one error occurred.", errors);
    }

    public class Result<T> : Result
    {
        public T? Data { get; private set; }

        protected Result(T? data, bool success, string message, List<string>? errors = null)
            : base(success, message, errors)
        {
            Data = data;
        }

        public static Result<T> Ok(T data) => new(data, true, "The operation was successfully executed.");

        public static new Result<T> Fail(string message) => new(default, false, message);

        public static new Result<T> Fail(List<string> errors) => new(default, false, "At least one error occurred.", errors);
    }
}
