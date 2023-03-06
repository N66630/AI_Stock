using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System;
using System.Text.Json;
using System.Reflection.Metadata;
using GetDataApi.Models;
using GetDataApi.Models.API.GetData;
using GetDataApi.API_IO.GetData;
using GetDataApi.Controllers.HttpResults;
using GetDataApi.Models.Utility;
using Microsoft.AspNetCore.Cors;
using System.Collections.Generic;

namespace GetDataApi.Controllers.API.GetData {

    //[ApiController]
    //[Route("[controller]")]
    //public class TestAPIController {
    //    [EnableCors]
    //    [HttpPost]
    //    [Route("getTestApi")]
    //    public ActionResult TestApi(TestApi_IO.input input) {
    //        var output = new TestApi_IO.output();
    //        TestApi_OO obj = new TestApi_OO();
    //        obj.aid = "Is aid";
    //        obj.order_idx = 0;
    //        output.setOutputData(ReturnCodeStatus.STATUS_OK, ReturnInfo.CM000, new List<TestApi_OO> { obj });

    //        return new ApiJsonResult<TestApi_IO.output>(output, false);
    //    }
    //}

    [ApiController]
    [Route("api/getdata/[controller]")]
    public class GetStockPriceController {
        [EnableCors]
        [HttpPost]
        //[Route("getStockValue")]
        public async Task<ActionResult> getStockValue(GetStockValue_IO.input input) {
            var output = new GetStockValue_IO.output();
            //TestApi_OO obj = new TestApi_OO();
            //obj.aid = "Is aid";
            //obj.order_idx = 0;
            //output.setOutputData(ReturnCodeStatus.STATUS_OK, ReturnInfo.CM000, new List<TestApi_OO> { obj });

            //return new ApiJsonResult<GetStockValue_IO.output>(output, false);
            try {
                // 設定欲爬取的網址
                string url = $"https://www.twse.com.tw/exchangeReport/STOCK_DAY?"+
                    $"response=json&stockNo={input.stock_id}&dateStart={input.date_start.Replace("-", "")}&dateEnd={input.date_end.Replace("-", "")}&_=1645819817083";

                // 送出 GET 請求
                using var httpClient = new HttpClient();
                using var response = await httpClient.GetAsync(url);
                using var content = response.Content;

                // 解析 JSON 格式的回應
                string jsonString = await content.ReadAsStringAsync();
                ////dynamic jsonData = JsonSerializer.Deserialize<dynamic>(jsonString);
                //dynamic jsonData = System.Text.Json.Nodes.JsonNode.Parse(jsonString);
                //decimal stockPrice = jsonData["data"][0][6].GetDecimal();

                StockValue_CallApiRtn_OO jsonData = JsonSerializer.Deserialize<StockValue_CallApiRtn_OO>(jsonString);
                //decimal stockPrice = Convert.ToDecimal(jsonData.data[0][1]);
                DateTime dStart = Convert.ToDateTime(input.date_start);
                DateTime dEnd = Convert.ToDateTime(input.date_end);
                List<GetStockValue_OO> lstData = new List<GetStockValue_OO>();
                
                for (int i=0; i< jsonData.data.Count; i++) {
                    DateTime dDate = DataConvert.TaiwanYearStringToDateTime(jsonData.data[i][Array.IndexOf(jsonData.fields, "日期1")]);
                    if (dDate >= dStart && dDate <= dEnd) {
                        GetStockValue_OO item = new GetStockValue_OO();
                        item.date = dDate;
                        item.trading_volume = DataConvert.StringToInt(jsonData.data[i][Array.IndexOf(jsonData.fields, "成交股數")]);
                        item.business_volume = DataConvert.StringToSingle( jsonData.data[i][Array.IndexOf(jsonData.fields, "成交金額")]) / 1000;
                        item.opening_price = DataConvert.StringToSingle(jsonData.data[i][Array.IndexOf(jsonData.fields, "開盤價")]) / 1000;
                        item.highest_price = DataConvert.StringToSingle(jsonData.data[i][Array.IndexOf(jsonData.fields, "最高價")]);
                        item.lowest_price = DataConvert.StringToSingle(jsonData.data[i][Array.IndexOf(jsonData.fields, "最低價")]);
                        item.closing_price = DataConvert.StringToSingle(jsonData.data[i][Array.IndexOf(jsonData.fields, "收盤價")]);
                        item.price_change = DataConvert.StringToSingle(jsonData.data[i][Array.IndexOf(jsonData.fields, "漲跌價差")]);
                        item.turnover = DataConvert.StringToInt(jsonData.data[i][Array.IndexOf(jsonData.fields, "成交筆數")]);
                        
                        lstData.Add(item);
                    }
                }

                // 回傳股價
                output.setOutputData(ReturnCodeStatus.STATUS_OK, ReturnInfo.CM000, lstData);
                return new ApiJsonResult<GetStockValue_IO.output>(output, false);
            }
            catch(Exception  ex) {
                // 若發生錯誤，回傳錯誤訊息
                output.setOutputExcption(ex);
                return new ApiJsonResult<GetStockValue_IO.output>(output, false);
            }
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class GetStockValueController : ControllerBase {
        [HttpGet]
        public async Task<ActionResult<decimal>> Get() {
            try {
                // 設定欲爬取的網址
                string url = "https://www.twse.com.tw/exchangeReport/STOCK_DAY?response=json&stockNo=2330&dateStart=20230324&dateEnd=20230324&_=1645819817083";

                // 送出 GET 請求
                using var httpClient = new HttpClient();
                using var response = await httpClient.GetAsync(url);
                using var content = response.Content;

                // 解析 JSON 格式的回應
                string jsonString = await content.ReadAsStringAsync();
                ////dynamic jsonData = JsonSerializer.Deserialize<dynamic>(jsonString);
                //dynamic jsonData = System.Text.Json.Nodes.JsonNode.Parse(jsonString);
                //decimal stockPrice = jsonData["data"][0][6].GetDecimal();
                
                StockValue_CallApiRtn_OO jsonData = JsonSerializer.Deserialize<StockValue_CallApiRtn_OO>(jsonString);
                decimal stockPrice = Convert.ToDecimal(jsonData.data[0][1]);

                // 回傳股價
                return Ok(stockPrice);
            }
            catch(Exception ex) {
                // 若發生錯誤，回傳錯誤訊息
                return BadRequest(ex.Message);
            }
        }
    }
}
