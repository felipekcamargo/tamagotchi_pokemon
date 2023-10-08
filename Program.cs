using client.PokemonClient;
using contracts.ConsoleOptions;
using service.PokemonService;
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
            var pokemonService = new PokemonService(new PokemonClient());

            try
            {
                var selectedOption = GetOptionFromUser();
                switch (selectedOption)
                {
                    case 1:
                        await ShowAllPokemonsPaged(pokemonService);
                        break;
                    case 2:
                        await ShowPokemonInfoAsync(pokemonService);
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

        private static async Task ShowPokemonInfoAsync(IPokemonService pokemonService)
        {
            string nameOrId = GetPokemonNameOrIdFromUser();
            var pokemon = await pokemonService.GetPokemonAsync(nameOrId);
            Console.Clear();
            Console.WriteLine($"Name: {pokemon.Name}");
            Console.WriteLine($"Height: {pokemon.Height}");
            Console.WriteLine($"Weight: {pokemon.Weight}");
            Console.WriteLine("Abilities");
            foreach(var abilityData in pokemon.Abilities)
                Console.WriteLine($" {abilityData.Ability.Name}");
        }

        private static string GetPokemonNameOrIdFromUser()
        {
            Console.Write("Please provide Pokemon name or Id: ");
            var nameOrId = new PokemonOption(Console.ReadLine()).GetValue();
            return nameOrId;
        }

        private static async Task ShowAllPokemonsPaged(IPokemonService pokemonService)
        {
            var page = 0;
            bool shouldContinue;
            do
            {
                var pokemons = await pokemonService.GetAllPokemonsPagedAsync(page);
                Console.WriteLine(JsonSerializer.Serialize(pokemons));
                Console.WriteLine($"\nPage {page + 1}");
                Console.Write("\nShow next page? [Y,n] ");
                shouldContinue = new YesOrNo(Console.ReadLine()).GetValue();
                page++;
            } while (shouldContinue);
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