using GetDataApi.Models.API.GetData;
using System.ComponentModel;

namespace GetDataApi.API_IO.GetData {
    public class GetStockValue_OO {
        // 日期
        public DateTime date { get; set; }
        // 成交股數
        public int trading_volume { get; set; }
        // 成交金額
        public float business_volume { get; set; }
        // 開盤價
        public float opening_price { get; set; }
        // 最高價
        public float highest_price { get; set; }
        // 最低價
        public float lowest_price { get; set; }
        // 收盤價
        public float closing_price { get; set; }
        // 漲跌價差
        public float price_change { get; set; }
        // 成交筆數
        public int turnover { get; set; }
    }

    public class GetStockValue_IO {
        /// <summary>
        /// Input value
        /// </summary>
        public class input : BaseInput {
            [DefaultValue("2330")]
            public string stock_id { get; set; } = "2330";
            [DefaultValue("2023-03-06")]
            public string date_start { get; set; } = "20230306";
            [DefaultValue("2023-03-06")]
            public string date_end { get; set; } = "20230306";
        }

        /// <summary>
        /// Output value
        /// </summary>
        public class output : BaseOutput<GetStockValue_OO> {

        }
    }

}
