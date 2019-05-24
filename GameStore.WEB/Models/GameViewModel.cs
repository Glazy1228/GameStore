namespace GameStore.WEB.Models
{
    public class GameViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string Platform { get; set; }
        public string SystemRequirement { get; set; }
    }
}