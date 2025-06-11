using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;


namespace CSV
{
    class Program
    {
        static void Main(string[] args)
        {
            string outputPath = @"D:\Output";

            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }

            var instruments = new[] { "BTCUSDT", "ETHUSDT", "SOLUSDT", "BTCUSDC", "XRPUSDT",  "ETHUSDC", "DOGEUSDT", "BNBUSDT", "SUIUSDT", "AAVEUSDT",
                "ADAUSDT", "LTCUSDT", "TRUMPUSDT", "ENAUSDT", "BCHUSDT", "AVAXUSDT", "UNIUSDT", "LINKUSDT", "TRXUSDT", "FARTCOINUSDT",
                "WIFUSDT", "DOTUSDT", "ONDOUSDT", "SOLUSDC", "WLDUSDT", "TAOUSDT", "FILUSDT", "VIRTUALUSDT", "TONUSDT", "FORMUSDT",
                "NEARUSDR", "ALCHUSDT", "TIAUSDT", "APTUSDT", "ARBUSDT", "XLMUSDT", "OPUSDT", "CRVUSDT", "WCTUSDT", "BUSDT", "HBARUSDT",
                "MASKUSDT", "JUPUSDT", "ETCUSDT", "PNUTUSDT", "LPTUSDT", "FETUSDT", "LDOUSDT", "INJUSDT", "KAITOUSDT", "KAVAUSDT", "SEIUSDT", "ATOMUSDT", "TRBUSDT", "PAXUSDT",
                "OMUSDT", "XRPUSDC", "ETHFIUSDT", "PENDLEUSDT", "MOODENGUSDT", "ORDIUSDT", "NEIROUSDT", "BNBUSDC", "TSTUSDT", "BTCDOMUSDT", "SUSDT", "MKRUSDT", "BERAUSDT", "ZROUSDT", "GALAUSDT",
                "NXPCUSDT", "GRASSUSDT", "SOLVUSDT", "ICPUSDT", "PENGUUSDT", "HMSTRUSDT", "SANDUSDT", "LAYERUSDT", "ATHUSDT", "INITUSD","CAKEUSDT",
                "DYDXUSDT", "POLUSDT", "DEGOUSDT", "RENDERUSDT", "POPCATUSDT", "EIGENUSDT", "ALGOUSDT", "APEUSDT", "NILUSDT", "ENSUSDT", "COOKIEUSDT", "DEXEUSDT",
                "DOGEUSDC", "VETUSDT", "MOVEUSDT", "RUNEUSDT", "XMRUSDT", "GOATUSDT", "SOPHUSDT" };

            Random rand = new Random();
            string chars = "qwertyuioplkjhgfdsazxcvbnm0123456789_";

            DateTime date = new DateTime(2025, 1, 1, 0, 0, 0, 0);
            DateTime expectedTime = DateTime.Now;

            string filePath = CSVReader.GetFileName(chars, rand);
            string fullPath = Path.Combine(outputPath, filePath);

            using (var writer = new StreamWriter(fullPath))
            {
                writer.WriteLine("Timestamp,Instrument, Bid, Ask");
                for (int i = 0; i < 10; i++)
                {
                    writer.WriteLine($"{rand.NextInt64(CSVReader.DateTimeToUnix(date),
                        CSVReader.DateTimeToUnix(expectedTime))}, {instruments[rand.Next(100)]}, {rand.Next(1, 100)}, {rand.Next(1, 500)}");
                }
            }



            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                PrepareHeaderForMatch = args => args.Header.Trim(),
            };

            List<CSVOutput> output;
            List<string> content = new List<string>();

            using (var reader = new StreamReader(@"D:\Output\gveupvbhce.csv"))
            using (var csvReader = new CsvReader(reader, config))
            {
                var records = csvReader.GetRecords<CSVReader>();
                output = (from record in records
                          orderby record.Timestamp
                          select new CSVOutput
                          {
                              TimeStamp = CSVReader.UnixToDateTime(record.Timestamp).ToString("yyyy-MM-dd hh:mm:ss.fff"),
                              Instrument = record.Instrument.Trim(),
                              Bid = record.Bid,
                              Ask = record.Ask
                          }).ToList();


            }
            Thread myThread = new Thread(() =>
            {

                foreach (var record in output)
                {
                    var dateTime = DateTime.Parse(record.TimeStamp);

                    var path = @$"D:\Yield\{dateTime.Year}-{dateTime.Month}-{dateTime.Day}-{dateTime.Hour}_{record.Instrument.Trim()}.csv";
                    if (!File.Exists(path))
                    {
                        using (File.Create(path)) { };
                        File.AppendAllText(path, "TimeStamp,Instrument,Bid,Ask");
                        File.AppendAllText(path, CSVOutput.GetString(record));
                    }
                    else
                    {
                        File.AppendAllText(path, CSVOutput.GetString(record));
                    }
                }
            });
            myThread.Start();


        }
    }
}



