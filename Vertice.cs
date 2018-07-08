namespace CryptoCube
{
    public class Vertice
    {
        public Vertice(char data, int position)
        {
            this.Data = data;
            this.Position = position;
        }
        public char Data { get; set; }
        public int Position { get; set; }
    }
}
