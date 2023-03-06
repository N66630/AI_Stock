using Microsoft.AspNetCore.Mvc;

namespace GetDataApi.Controllers.HttpResults {
    public class ApiJsonResult<T> : JsonResult {
        private string _errorcode = "";
        private bool _catch_in_apigateway = true;

        public ApiJsonResult(object value, bool catch_in_apigateway = true) : base(value) {
            var t = value.GetType().GetProperty("code").GetValue(value, null);
            this._errorcode = (string)t;
            this._catch_in_apigateway = catch_in_apigateway;
        }
        public ApiJsonResult(object value, object serializerSettings) : base(value, serializerSettings) {
        }
        public override void ExecuteResult(ActionContext context) {
            base.ExecuteResult(context);
        }

        public override Task ExecuteResultAsync(ActionContext context) {
            if(this._catch_in_apigateway) {
                context.HttpContext.Response.Headers.Add("X-Catch-In-Api-Gateway", "Catch");
            }
            else {
                context.HttpContext.Response.Headers.Add("X-Catch-In-Api-Gateway", "");
            }

            if(this._errorcode != "CM000") {
                context.HttpContext.Response.StatusCode = 500;
            }
            return base.ExecuteResultAsync(context);
        }
    }

}
