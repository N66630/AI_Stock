namespace GetDataApi.Models {
    public static class ReturnCodeStatus {
        // API Output status
        //*****************************************************************************************
        /// <summary>
        /// Output status (OK)
        /// </summary>
        public static string STATUS_OK = "OK";
        /// <summary>
        /// Output status (NG)
        /// </summary>
        public static string STATUS_NG = "NG";
        /// <summary>
        /// Output status (Warning)
        /// </summary>
        public static string STATUS_WR = "WARN";
    }


    public class ReturnCode {
        private string _code;
        private string _msg;

        public ReturnCode(string code, string msg) {
            this._code = code;
            this._msg = msg;
        }


        public String Code { get { return this._code; } }

        public String Msg { get { return this._msg; } }
    }


    public class ReturnInfo {
        public static ReturnCode CM999 { get { return new ReturnCode("CM999", "一般錯誤"); } }
        public static ReturnCode CM000 { get { return new ReturnCode("CM000", "作業成功"); } }
    }

}
