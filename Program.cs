// See https://aka.ms/new-console-template for more information
using System.Text;
using System.Text.Json;
//  www.jsonlint.com can be used to test if the output json file is valid.

List<string> shareList = new() { "AAPL", "GOOG", "MSFT" };

foreach (string str in shareList)
{
    Console.WriteLine(str);
    var json1 = await JSONConversion.Program.Pr1(str);
    await JSONConversion.Program.Pr2(json1, str);
}

namespace JSONConversion
{
    public class Program
    {
        async public static Task<string> Pr1(string sticker)
        {
            HttpClient client = new HttpClient();
            var jsonresponseBody = await client.GetStringAsync("https://financialmodelingprep.com/api/v3/historical-price-full/" + sticker + "?&apikey=APIKEY");
            File.WriteAllText(@"C:\Users\User1\source\repos\Data\" + sticker + ".json", jsonresponseBody);
            return jsonresponseBody;
        }

        async public static Task Pr2(string jsonresponseBody, string sticker)
        {

            // convert string to stream
            byte[] byteArray = Encoding.UTF8.GetBytes(jsonresponseBody);
            MemoryStream stream = new MemoryStream(byteArray);

            Root myDeserializedClass = JsonSerializer.Deserialize<Root>(stream);
            string str;

            StreamWriter sw = new StreamWriter(@"C:\Users\User1\source\repos\Data\" + sticker + ".txt");
            await sw.WriteLineAsync("<date>,<open>,<high>,<low>,<close>,<vol>");

            foreach (Historical historical in myDeserializedClass.historical)
            {
                str = historical.date;
                str = str.Replace("-", "");
                str = str + "," + Math.Round(historical.open, 2) + "," + Math.Round(historical.high, 2) + "," +
                Math.Round(historical.low, 2) + "," + Math.Round(historical.close, 2) + "," + historical.volume;
                await sw.WriteLineAsync(str);
            }
            sw.Close();
        }
    }

    public class Historical
    {
        public string? date { get; set; }
        public double open { get; set; }
        public double high { get; set; }
        public double low { get; set; }
        public double close { get; set; }
        public double adjClose { get; set; }
        public double volume { get; set; }
        public double unadjustedVolume { get; set; }
        public double change { get; set; }
        public double changePercent { get; set; }
        public double vwap { get; set; }
        public string? label { get; set; }
        public double changeOverTime { get; set; }
    }

    public class Root
    {
        public string? symbol { get; set; }
        public List<Historical>? historical { get; set; }
    }

}