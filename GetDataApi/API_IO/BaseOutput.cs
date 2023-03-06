using GetDataApi.Models;

namespace GetDataApi.API_IO {
    public abstract class BaseOutput<T> {

        /// <summary>
        /// Output Data Value Object
        /// </summary>
        public class OutputDataVo {
            /// <summary>
            /// Total data count
            /// </summary>
            public int total { get; set; }
            /// <summary>
            /// Rows data dictionary
            /// </summary>
            public List<T> rows { get; set; }
        }


        /// <summary>
        /// System status (OK/NG/WARN)
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// System error code
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// System message
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// Output data 
        /// </summary>
        public OutputDataVo data { get; set; }


        /// <summary>
        /// To set output data while Exception
        /// </summary>
        /// <param name="status"></param>
        /// <param name="ex"></param>
        public void setOutputExcption(Exception ex) {
            this.status = ReturnCodeStatus.STATUS_NG;
            this.code = ReturnInfo.CM999.Code;
            this.message = ex.Message ;
            // Initial output data object
            OutputDataVo outputData = new OutputDataVo();
            outputData.total = 0;
            outputData.rows = new List<T>();
            // Set output data
            this.data = outputData;
        }

        /// <summary>
        /// To set object value with list object into output data
        /// </summary>
        /// <param name="status"></param>
        /// <param name="code"></param>
        /// <param name="sourceList"></param>
        public void setOutputData(string status, ReturnCode code, List<T> dataList) {
            this.status = status;
            this.code = code.Code;
            this.message = code.Msg;
            // Initial output data object
            OutputDataVo outputData = new OutputDataVo();
            outputData.total = dataList.Count;
            outputData.rows = dataList;
            // Set output data
            this.data = outputData;
        }
    }

}
