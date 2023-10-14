using Controller;

namespace Main
{
    static class Program
    {
        public static async Task Main(string[] args)
        {
            var TamagotchiPokemon = new TamagotchiController();
            await TamagotchiPokemon.MainMenu();
        }
    }
}
