namespace GetDataApi.Models.Utility {
    public class DataConvert {
        public static DateTime TaiwanYearStringToDateTime(string data) { return Convert.ToDateTime(data.Replace(",", "")).AddYears(1911); }
        public static int StringToInt(string data) { return Convert.ToInt32(data.Replace(",", "")); }
        public static float StringToSingle(string data) { return Convert.ToSingle(data.Replace(",", "")); }
    }
}
