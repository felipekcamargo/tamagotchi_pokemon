using client.PokemonClient;
using System.Text.Json;

namespace HelloWorld
{
    static class Program
    {
        public static async Task Main(string[] args)
        {
            await ShowPokemons();
        }

        private static async Task ShowPokemons(){
            var pokemonClient = new PokemonClient();

            try {
                // var pokemons = await pokemonClient.GetPokemons();
                // Console.WriteLine(JsonSerializer.Serialize(pokemons));  
                var pokemon = await pokemonClient.GetPokemon("21");
                Console.WriteLine(pokemon);  
            }
            catch(Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}