namespace AStar.Application.Dtos
{
    public class CardInfoDto
    {
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public string FullName { get; set; }
        public string CardNumber { get; set; }
        public string Cvv { get; set; }
        public int Pin { get; set; }
    }
}