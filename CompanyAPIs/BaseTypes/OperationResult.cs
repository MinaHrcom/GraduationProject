using HRCom.Domain.Localization;
using System.Net;

namespace HRCom.Domain.BaseTypes
{
    public class OperationResult
    {
        public bool IsSuccess { get; protected init; }
        public bool IsFailed => !IsSuccess;
        public bool IsBadReuqest => StatusCode == HttpStatusCode.BadRequest;
        public bool IsNotFound => StatusCode == HttpStatusCode.NotFound;
        public HttpStatusCode StatusCode { get; set; }
        public string ErrorMessageKey { get; set; }
        public string[] ErrorMessagePlaceholderValues { get; private set; }
        public int ErrorCode { get; set; }

        public static OperationResult BadRequest(string message, int errorCode = 0, string[] errorMessagePlaceholderValues = null)
        => new()
        {
            IsSuccess = false,
            StatusCode = HttpStatusCode.BadRequest,
            ErrorMessageKey = message,
            ErrorCode = errorCode,
            ErrorMessagePlaceholderValues = errorMessagePlaceholderValues
        };

        public static OperationResult<T> BadRequest<T>(string message, T data = default, int errorCode = 0,
            string[] errorMessagePlaceholderValues = null)
            => new()
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest,
                ErrorMessageKey = message,
                ErrorCode = errorCode,
                Data = data,
                ErrorMessagePlaceholderValues = errorMessagePlaceholderValues
            };

        public static OperationResult NotFound(string message = LocalizationKeys.DataNotFound, string[] errorMessagePlaceholderValues = null)
            => new()
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.NotFound,
                ErrorMessageKey = message,
                ErrorMessagePlaceholderValues = errorMessagePlaceholderValues
            };

        public static OperationResult<T> NotFound<T>(string message = LocalizationKeys.DataNotFound,
            string[] errorMessagePlaceholderValues = null)
            => new()
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.NotFound,
                ErrorMessageKey = message,
                Data = default,
                ErrorMessagePlaceholderValues = errorMessagePlaceholderValues
            };

        public static OperationResult Unauthorized()
            => new()
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.Unauthorized,
            };

        public static OperationResult<T> Success<T>(T data)
            => new()
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                Data = data
            };

        public static OperationResult<T> Success<T>(T data, PagingResponse paging)
            => new()
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                Data = data,
                Paging = paging
            };

        public static OperationResult Success()
            => new()
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK
            };

        public static OperationResult<T> Created<T>(T data, string redirectUrl, string message = null)
            => new()
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.Created,
                ErrorMessageKey = message,
                Data = data,
                RedirectUrl = redirectUrl,
            };
    }

    public class OperationResult<T> : OperationResult
    {
        public T Data { get; set; }
        public PagingResponse Paging { get; set; }
        public string RedirectUrl { get; set; }
        public string RequiredFieldsEnString { get; set; }
        public string RequiredFieldsArString { get; set; }

        public static implicit operator OperationResult<T>(T data) => Success(data);

        public static implicit operator OperationResult<T>((T data, PagingResponse paging) dataWithPaging)
            => Success(dataWithPaging.data, dataWithPaging.paging);
    }
}
