using System.Text.Json.Serialization;

namespace HRCom.Domain.BaseTypes
{
    public class SuccessResponse
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; } = false;
    }

    public class ErrorResponse
    {
        [JsonPropertyName("error")]
        public Error Error { get; set; }
    }

    public class ErrorResponseWithCode
    {
        [JsonPropertyName("error")]
        public ErrorWithCode Error { get; set; }
    }



    public class ErrorRequiredFieldResponse
    {
        [JsonPropertyName("error")]
        public ErrorRequiredField Error { get; set; }
    }
    public class ErrorRequiredField
    {
        [JsonPropertyName("requiredfieldsar")]
        public string RequiredFieldsAr { get; set; }


        [JsonPropertyName("requiredfieldsen")]
        public string RequiredFieldsEn { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }
        [JsonPropertyName("code")]
        public int Code { get; set; }
    }

    public class Error
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }

    public class ErrorWithCode : Error
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }
    }

    public class DataResponse<T>
    {
        [JsonPropertyName("data")]
        public T Data { get; set; }
    }

    public class DataResponseWithPaging<T> : DataResponse<T>
    {
        [JsonPropertyName("paging")]
        public PagingResponse Paging { get; set; }
    }

    public class PagingResponse
    {
        [JsonPropertyName("prev")]
        public bool Previous { get; set; }

        [JsonPropertyName("next")]
        public bool Next { get; set; }

        [JsonPropertyName("total_pages")]
        public int TotalPages { get; set; }

        [JsonPropertyName("total_records")]
        public long TotalRecords { get; set; }
    }

    public class PaginatedResult<T>
    {
        [JsonPropertyName("pagination")]
        public PagingResponse Pagination { get; }

        [JsonPropertyName("data")]
        public IEnumerable<T> Data { get; }

        public PaginatedResult(IEnumerable<T> data, long count, int pageNumber, int pageSize)
        {
            Data = data;
            var totalNumberOfPages = Math.Ceiling((decimal)count / pageSize);
            Pagination = new PagingResponse
            {
                Next = pageNumber < totalNumberOfPages,
                Previous = pageNumber > 1,
                TotalPages = (int)totalNumberOfPages,
                TotalRecords = count
            };
        }
    }
}
