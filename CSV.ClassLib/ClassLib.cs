namespace CSV
{
    public class CSVReader
    {
        private long _timeStamp;
        private string _instrument;
        private int _bid;
        private int _ask;

        public long Timestamp
        {
            get { return _timeStamp; }
            set { _timeStamp = value; }
        }
        public string Instrument
        {
            get { return _instrument; }
            set { _instrument = value; }
        }

        public int Bid
        {
            get { return _bid; }
            set { _bid = value; }
        }
        public int Ask
        {
            get { return _ask; }
            set { _ask = value; }
        }


        public static string GetFileName(string chars, Random rnd)
        {
            string output = "";
            for (int i = 0; i < 10; i++)
            {
                output += chars[rnd.Next(chars.Length - 1)];
            }
            return output + ".csv";
        }
        public static DateTime UnixToDateTime(long unix)
        {
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return dt.AddMilliseconds(unix);
        }
        public static long DateTimeToUnix(DateTime dt)
        {
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return (long)(dt - epoch).TotalMilliseconds;
        }

    }
    public class CSVOutput
    {
        private string _datetime;
        private string _instrument;
        private int _bid;
        private int _ask;

        public string TimeStamp
        {
            get { return _datetime; }
            set { _datetime = value; }
        }
        public string Instrument
        {
            get { return _instrument; }
            set { _instrument = value; }
        }

        public int Bid
        {
            get { return _bid; }
            set { _bid = value; }
        }
        public int Ask
        {
            get { return _ask; }
            set { _ask = value; }
        }
        public static string GetString(CSVOutput csv)
        {
            return $"\n{csv.TimeStamp},{csv.Instrument},{csv.Bid},{csv.Ask}";
        }
    }
    public class Writer
    {
        private Thread _thread;
        private CSVOutput _output;
    }

}


