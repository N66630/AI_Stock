using GetDataApi.Models;
using System.Runtime.Serialization;

namespace GetDataApi.Models {

    [Serializable]
    public class WebApiException: Exception {
        private string _Code = "";
        public string Code {
            get { return _Code; }
        }

        public WebApiException() { }
        public WebApiException(ReturnCode code) { }
        public WebApiException(ReturnCode code, string extendMsg) : base(string.Format($"[{0}-{1}:{2}]", code.Code, code.Msg, extendMsg)) {
            this._Code = code.Code;
        }
        public WebApiException(ReturnCode code, string extendMsg, Exception innerException) : base(string.Format($"[{0}-{1}:{2}]", code.Code, code.Msg, extendMsg), innerException) {
            this._Code = code.Code;
        }
        public WebApiException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
