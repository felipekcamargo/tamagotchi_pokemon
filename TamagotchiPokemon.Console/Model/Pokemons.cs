namespace Model
{
    public class Pokemons
    {
        public int? Count { get; set; }
        public string? Next { get; set; }
        public string? Previous { get; set; }
        public Results[]? Results { get; set; }
    }

    public class Results
    {
        public string? Name { get; set; }
        public string? Url { get; set; }
    }
}