using System.Collections.Generic;

namespace ShopBridge.Common
{
    public class ReturnResult 
    {
        public static Dictionary<string, object> SuccessResponse<T>(T value, string message) where T : class
        {
            Dictionary<string, object> SuccessResponse = new Dictionary<string, object>();
            SuccessResponse.Add("success", true);
            SuccessResponse.Add("message", message);
            SuccessResponse.Add("value", value);
            return SuccessResponse;
        }
        public static Dictionary<string, object> SuccessResponse(string message)  
        {
            Dictionary<string, object> SuccessResponse = new Dictionary<string, object>();
            SuccessResponse.Add("success", true);
            SuccessResponse.Add("message", message);
            return SuccessResponse;
        }
        public static Dictionary<string, object> FailureResponse<T>(T value, string message) where T : class
        {
            Dictionary<string, object> SuccessResponse = new Dictionary<string, object>();
            SuccessResponse.Add("success", false);
            SuccessResponse.Add("message", message);
            SuccessResponse.Add("value", value);
            return SuccessResponse;
        }
        public static Dictionary<string, object> FailureResponse(string message) 
        {
            Dictionary<string, object> SuccessResponse = new Dictionary<string, object>();
            SuccessResponse.Add("success", false);
            SuccessResponse.Add("message", message);
            return SuccessResponse;
        }
    }
}
