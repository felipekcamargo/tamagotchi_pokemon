using client.PokemonClient;
using ConsoleOptions;
using System.Text.Json;

namespace HelloWorld
{
    static class Program
    {
        public static async Task Main(string[] args)
        {
            await ShowPokemons();
        }

        private static async Task ShowPokemons()
        {
            var pokemonClient = new PokemonClient();

            try
            {
                var selectedOption = GetOptionFromUser();
                switch (selectedOption)
                {
                    case 1:
                        await ShowAllPokemonsPaged(pokemonClient);
                        break;
                    case 2:
                        await ShowPokemonInfo(pokemonClient);
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static async Task ShowPokemonInfo(PokemonClient pokemonClient)
        {
            string nameOrId = GetPokemonNameOrIdFromUser();
            var pokemon = await pokemonClient.GetPokemon(nameOrId);
            Console.WriteLine(pokemon);
        }

        private static string GetPokemonNameOrIdFromUser()
        {
            Console.Write("Please provide Pokemon name or Id: ");
            var nameOrId = new PokemonOption(Console.ReadLine());
            return nameOrId.GetValue();
        }

        private static async Task ShowAllPokemonsPaged(PokemonClient pokemonClient)
        {
            var pokemons = await pokemonClient.GetPokemons();
            Console.WriteLine(JsonSerializer.Serialize(pokemons));
        }

        private static int GetOptionFromUser()
        {
            Console.Clear();
            Console.WriteLine("--- Tamagotchi Pokemon ---");
            Console.WriteLine("1 - Show all pokemons");
            Console.WriteLine("2 - Pokemon info");
            Console.Write("Option: ");
            var actionOption = new MainMenuOptions(Console.ReadLine());
            Console.Clear();
            return actionOption.GetValue();
        }
    }
}