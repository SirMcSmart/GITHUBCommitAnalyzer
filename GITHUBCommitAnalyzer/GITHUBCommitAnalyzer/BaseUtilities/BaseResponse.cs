using Microsoft.Extensions.Options;
using System.Net;

namespace GITHUBCommitAnalyzer.BaseUtilities
{
    public class BaseResponse<T> : IBaseResponse<T>
    {
        private readonly AppSettings _appSettings;
        public BaseResponse(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        public Task<string> CouldNotFetchDetails() => Task.FromResult(_appSettings.CouldNotFetchDetails);
        public Task<string> CouldNotFetchSingle() => Task.FromResult(_appSettings.CouldNotFetchSingle);
        public Task<string> CouldNotFetchAll() => Task.FromResult(_appSettings.CouldNotFetchAll);
        public Task<string> CouldNotCreate() => Task.FromResult(_appSettings.CouldNotCreate);
        public Task<string> CouldNotUpdate() => Task.FromResult(_appSettings.CouldNotUpdate);
        public Task<string> UserAlreadyExist() => Task.FromResult(_appSettings.UserAlreadyExist);
        public Task<string> CouldNotDelete() => Task.FromResult(_appSettings.CouldNotDelete);
        public Task<string> InvalidParameter() => Task.FromResult(_appSettings.InvalidParameter);
        public Task<string> ErrorOccured() => Task.FromResult(_appSettings.ErrorOccured);
        public Task<string> NoContent() => Task.FromResult(_appSettings.NoContent);
        public Task<string> Successful() => Task.FromResult(_appSettings.Successful);
        public Task<string> PermissionDenied() => Task.FromResult(_appSettings.PermissionDenied);
        public Task<BaseResponseVM<T>> Success(object response = null, string respCode = null, string msg = null)
        {
            try
            {
                var obj = new BaseResponseVM<T>
                {
                    Description = msg == null ? _appSettings.Successful : msg,
                    Success = true,
                    StatusCode = (int)HttpStatusCode.OK,
                    Code = respCode == null ? _appSettings.RcSuccess : respCode,
                    data = (T)response
                };
                return Task.FromResult(obj);
            }
            catch (Exception ex)
            {
                //ex.LogError();
                return Task.FromResult(new BaseResponseVM<T>());
            }
        }
        public Task<BaseResponseVM<T>> CustomResponse(HttpStatusCode httpStatus, object response = null, string respCode = null, string msg = null)
        {
            try
            {
                var obj = new BaseResponseVM<T>
                {
                    Description = msg == null ? _appSettings.Successful : msg,
                    Success = true,
                    StatusCode = (int)(httpStatus),
                    Code = respCode == null ? _appSettings.RcSuccess : respCode,
                    data = (T)response
                };
                return Task.FromResult(obj);
            }
            catch (Exception ex)
            {
                //ex.LogError();
                return Task.FromResult(new BaseResponseVM<T>());
            }
        }

        public Task<BaseResponseVM<T>> SuccessMessage(string msg)
        {
            try
            {
                var obj = new BaseResponseVM<T>
                {
                    Description = msg,
                    Success = true,
                    StatusCode = (int)HttpStatusCode.OK,
                    Code = _appSettings.RcSuccess ?? "00"
                };
                return Task.FromResult(obj);
            }
            catch (Exception ex)
            {
                //ex.LogError();
                return Task.FromResult(new BaseResponseVM<T>());
            }
        }

        public Task<BaseResponseVM<T>> CustomErrorMessage(string msg, HttpStatusCode StatusCode, string respCode = null)
        {
            try
            {
                var obj = new BaseResponseVM<T>
                {
                    Description = msg,
                    Success = false,
                    StatusCode = (int)StatusCode,
                    Code = respCode ?? _appSettings.RcFailed ?? "99"
                };
                return Task.FromResult(obj);
            }
            catch (Exception ex)
            {
                //ex.LogError();
                return Task.FromResult(new BaseResponseVM<T>());
            }
        }

        public Task<BaseResponseVM<T>> BadRequest(string msg = null)
        {
            try
            {
                var obj = new BaseResponseVM<T>
                {
                    Code = _appSettings.RcBadRequest ?? "99",
                    Description = _appSettings.InvalidParameter + msg,
                    Success = false,
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
                return Task.FromResult(obj);
            }
            catch (Exception ex)
            {
                //ex.LogError();
                return Task.FromResult(new BaseResponseVM<T>());
            }
        }

        public Task<BaseResponseVM<T>> InternalServerError(Exception ex)
        {
            try
            {
                var obj = new BaseResponseVM<T>
                {
                    Code = _appSettings.RcFailed ?? "99",
                    Description = $"{_appSettings.ErrorOccured}. Details {ex.Message}",
                    Success = false,
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
                return Task.FromResult(obj);
            }
            catch (Exception exp)
            {
                //ex.LogError();
                // exp.LogError();
                return Task.FromResult(new BaseResponseVM<T>());
            }
        }

        public static Task<BaseResponseVM<T>> MiddlewareUnAuthorized()
        {
            try
            {
                var obj = new BaseResponseVM<T>
                {
                    Description = "Unauthorized",
                    Success = false,
                    StatusCode = (int)HttpStatusCode.Unauthorized
                };
                return Task.FromResult(obj);
            }
            catch (Exception ex)
            {
                //ex.LogError();
                return Task.FromResult(new BaseResponseVM<T>());
            }
        }

        public static Task<BaseResponseVM<T>> PermissionDeniedMessage()
        {
            try
            {
                var obj = new BaseResponseVM<T>
                {
                    Description = "Permission Denied",
                    Success = false,
                    StatusCode = (int)HttpStatusCode.Forbidden
                };
                return Task.FromResult(obj);
            }
            catch (Exception ex)
            {
                //ex.LogError();
                return Task.FromResult(new BaseResponseVM<T>());
            }
        }
        public static Task<BaseResponseVM<T>> StaticErrorMessage(string msg, HttpStatusCode StatusCode, string respCode = null)
        {
            try
            {
                var obj = new BaseResponseVM<T>
                {
                    Description = msg,
                    Success = false,
                    StatusCode = (int)StatusCode,
                    Code = respCode ?? "99"
                };
                return Task.FromResult(obj);
            }
            catch (Exception ex)
            {
                //ex.LogError();
                return Task.FromResult(new BaseResponseVM<T>());
            }
        }
    }
}
