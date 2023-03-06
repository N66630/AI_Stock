namespace GetDataApi.Models.API.GetData
{
    public class StockValueData_OO
    {
        public string date { get; set; }
        public string price { get; set; }
    }
    public class StockValue_CallApiRtn_OO
    {
        public string stat { get; set; }
        public string date { get; set; }
        public string title { get; set; }
        public string[] fields { get; set; }
        public List<List<string>> data { get; set; }
        public string[] notes { get; set; }
    }
}
