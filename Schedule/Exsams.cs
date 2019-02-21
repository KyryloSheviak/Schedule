namespace Schedule
{
    public class Exsams
    {
        public string Date { get; set; }
        public string Cabinet { get; set; }
        public string Teacher { get; set; }
        public string Time { get; set; }
        public string Type { get; set; }
        public string Subject { get; set; }

        public Exsams(){}
        public Exsams(string date, string cabinet)
        {
            this.Date = date;
            this.Cabinet = cabinet;
        }
    }
}
