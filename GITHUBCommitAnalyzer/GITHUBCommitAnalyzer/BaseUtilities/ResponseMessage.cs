namespace GITHUBCommitAnalyzer.BaseUtilities
{
    public class ResponseBase
    {
        public string Code { get; set; }
        public bool Success { get; set; }
        public string Description { get; set; }
    }

    public class Response
    {
        public string message { get; set; }
        public string status { get; set; }
    }

    public class Response<T> : ResponseBase
    {
        public T Data { get; set; }
    }


    public class SaveResponse
    {
        public object Data { get; set; }
        public string Code { get; set; }
        public bool Success { get; set; }
        public string Description { get; set; }


    }
    public class DeactivateResponse
    {
        public string Id { get; set; }
        public bool Deactivated { get; set; }
        public string ResponseMessage { get; set; }

    }

    public class ReactivateResponse
    {
        public string Id { get; set; }
        public bool Reactivated { get; set; }
        public string ResponseMessage { get; set; }

    }

    public class DeleteResponse
    {
        public string Id { get; set; }
        public bool Deleted { get; set; }
    }
    public class SearchResponse<T> where T : class
    {
        public List<T> Results { get; set; }
        public long TotalCount { get; set; }
        public long Took { get; set; }
    }
    public class SearchCall<T> where T : class
    {
        public T Parameter { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }

    public class GetResponse<T>
    {
        public T Source { get; set; }
    }

    public class ResponseData<T>
    {
        public ResponseData() { }
        public ResponseData(T response)
        {
            Data = response;
        }

        public T Data { get; set; }
    }

    public class PageListResponseData<T>
    {
        public PageListResponseData() { }
        public PageListResponseData(IEnumerable<T> response)
        {
            Data = response;
        }

        public IEnumerable<T> Data { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

    }


    public class ResponseCode
    {
        public const string Success = "00";
        public const string Exist = "01";
        public const string Empty = "02";
        public const string Error = "03";
        public const string Invalid = "04";
        public const string Others = "05";
        public const string DoesNotExist = "06";
        public const string DuplicateRecord = "07";
        public const string CompletedWithError = "11";
        public const string ValidationError = "09";
        public const string Created = "10";
        public const string Updated = "11";
        public const string Deleted = "12";
        public const string Deactivated = "13";
        public const string Activated = "14";
        public const string InternalServerError = "500";
        public const string ForgotPassword = "15";
        public const string ChangePassword = "16";
    }
    public class ResponseDescription
    {
        public const string Success = "Success";
        public const string Exist = "Record Exist";
        public const string Empty = "Record cannot be Empty";
        public const string Error = "Error(s)";
        public const string Invalid = "Invalid Record";
        public const string Others = "Other Errors";
        public const string DoesNotExist = "Record Does not Exist";
        public const string DuplicateRecord = "Duplicated Record";
        public const string CompletedWithError = "Completed With Error(s)";
        public const string ValidationError = "Validation Error";
        public const string Updated = "Record Updated";
        public const string Deleted = "Record Deleted";
        public const string Deactivated = "Record Deactivated";
        public const string Activated = "Record Activated";
        public const string Created = "Record Created";
        public const string InternalServerError = "Sorry could not process your request.";
    }

    public static class ResponseMessage
    {
        public static ResponseBase Response(string code, string message, bool success = false)
        {
            var response = new ResponseBase
            {
                Code = code,
                Description = message,
                Success = success
            };
            return response;
        }

        public static Response<object> Response(string code, string message, object data, bool success = false)
        {
            var response = new Response<object>
            {
                Code = code,
                Data = data,
                Description = message,
                Success = success
            };
            return response;
        }




    }
}
