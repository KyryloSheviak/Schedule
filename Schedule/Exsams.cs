namespace Schedule
{
    public class Exsams
    {
        public string Name { get; set; }
        public string Less { get; set; }
        public Exsams(){}
        public Exsams(string name, string less)
        {
            this.Name = name;
            this.Less = less;
        }
    }
}
