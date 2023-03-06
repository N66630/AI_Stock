namespace GetDataApi.API_IO.GetData {
    public class TestApi_OO {
        public string aid { get; set; }
        public string ad_id { get; set; }
        public int order_idx { get; set; }
        public string media_type { get; set; }
        public string ad_url { get; set; }

    }

    public class TestApi_IO {
        /// <summary>
        /// Input value
        /// </summary>
        public class input : BaseInput {
            public string app_id { get; set; }
            public string ad_id { get; set; }
        }

        /// <summary>
        /// Output value
        /// </summary>
        public class output : BaseOutput<TestApi_OO> {

        }
    }

}
