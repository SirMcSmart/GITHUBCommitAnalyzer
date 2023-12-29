using System.Net;

namespace GITHUBCommitAnalyzer.BaseUtilities
{
    public interface IBaseResponse<T>
    {
        Task<string> CouldNotFetchDetails();
        Task<string> CouldNotFetchSingle();
        Task<string> CouldNotFetchAll();
        Task<string> CouldNotCreate();
        Task<string> CouldNotUpdate();
        Task<string> CouldNotDelete();
        Task<string> UserAlreadyExist();
        Task<string> InvalidParameter();
        Task<string> ErrorOccured();
        Task<string> NoContent();
        Task<string> Successful();
        Task<string> PermissionDenied();
        //Task<BaseResponseVM<T>> ReferralNotExist();
        Task<BaseResponseVM<T>> Success(object response, string? respCode = null, string? msg = null);
        Task<BaseResponseVM<T>> SuccessMessage(string msg);
        Task<BaseResponseVM<T>> CustomErrorMessage(string msg, HttpStatusCode StatusCode, string? respCode = null);
        Task<BaseResponseVM<T>> BadRequest(string? msg = null);
        Task<BaseResponseVM<T>> InternalServerError(Exception ex);
        Task<BaseResponseVM<T>> CustomResponse(HttpStatusCode httpStatus, object? response = null, string? respCode = null, string? msg = null);
    }
}
