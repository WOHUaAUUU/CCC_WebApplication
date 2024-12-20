using Model.Other;

namespace Shoping_WebAPI.Config
{
    public class ResultHelper
    {
        public static ApiResult Succes(object data)
        {
            return new ApiResult() { IsSuccess = true, Result = data, Msg = "调用成功" };
        }
        public static ApiResult Error(string message)
        {
            return new ApiResult() { IsSuccess = false, Result = null, Msg = message };
        }
    }
}
