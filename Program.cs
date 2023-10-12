using ui;

namespace Main
{
    static class Program
    {
        public static async Task Main(string[] args)
        {
            var TamagotchiPokemon = new TamagotchiPokemon();
            await TamagotchiPokemon.MainMenu();
        }
    }
}
