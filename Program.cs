
using ui.TamagotchiPokemon;

namespace HelloWorld
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