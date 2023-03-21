namespace SigefMunicipal.Domain.Responses
{

    public class ApiResponse<T>
    {

        public ApiResponse()
        { }

        public T Data { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; } = true;
        public dynamic[] Errors { get; set; }

        public static ApiResponse<T> Fail(dynamic[] Errors, string message = "")
        {
            return new ApiResponse<T>
            {
                Errors = Errors,
                IsSuccess = false,
                Message = message
            };
        }
        public static ApiResponse<T> Fail(string message = "")
        {
            return new ApiResponse<T>
            {
                IsSuccess = false,
                Message = message
            };
        }
        public static ApiResponse<T> Success(T Data, string message = "")
        {
            return new ApiResponse<T>
            {
                Data = Data,
                IsSuccess = true,
                Message = message
            };
        }

        public static ApiResponse<T> Success(string message = "")
        {
            return new ApiResponse<T>
            {
                IsSuccess = true,
                Message = message
            };
        }
    }
}