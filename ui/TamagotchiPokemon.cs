using client.PokemonClient;
using contracts.ConsoleOptions;
using service.PokemonService;
using System.Text.Json;
using static contracts.ConsoleOptions.MainMenuOptions;

namespace ui.TamagotchiPokemon
{

    public class TamagotchiPokemon
    {
        private readonly IPokemonClient _pokemonClient = new PokemonClient();
        private readonly IPokemonService _pokemonService;
        private string _userName = string.Empty;

        public TamagotchiPokemon()
        {
            _pokemonService = new PokemonService(_pokemonClient);
        }

        public async Task MainMenu()
        {
            try
            {
                Console.Clear();
                ShowGameName();
                GetUserName();
                var selectedOption = MainMenuOptions();
                switch (selectedOption)
                {
                    case 1:
                        await ShowAllPokemonsPaged();
                        break;
                    case 2:
                        await ShowPokemonInfoAsync();
                        break;
                    case 3:
                        Console.WriteLine("WIP");
                        break;
                    case 4:
                        Console.WriteLine("WIP");
                        break;
                    case 5:
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

        private void GetUserName()
        {
            Console.Write("What's your name? ");
            _userName = Console.ReadLine();
        }

        private int MainMenuOptions()
        {
            Console.Clear();
            Console.WriteLine("---------------------- MENU ----------------------");
            Console.WriteLine($"\n{_userName}, what you want to do?\n");
            foreach(var option in availableOptions)
                Console.WriteLine($"{option.Key} - {option.Value}");

            Console.Write("Option: ");
            var actionOption = new MainMenuOptions(Console.ReadLine());
            Console.Clear();
            return actionOption.GetValue();
        }

        private async Task ShowPokemonInfoAsync()
        {
            Console.Write("Please provide Pokemon name or Id: ");
            var nameOrId = new PokemonOption(Console.ReadLine()).GetValue();
            var pokemon = await _pokemonService.GetPokemonAsync(nameOrId);
            Console.Clear();
            Console.WriteLine($"Name: {pokemon.Name}");
            Console.WriteLine($"Height: {pokemon.Height}");
            Console.WriteLine($"Weight: {pokemon.Weight}");
            Console.WriteLine("Abilities");
            foreach(var abilityData in pokemon.Abilities)
                Console.WriteLine($" {abilityData.Ability.Name}");
        }

        private async Task ShowAllPokemonsPaged()
        {
            var page = 0;
            bool shouldContinue;
            do
            {
                var pokemons = await _pokemonService.GetAllPokemonsPagedAsync(page);
                Console.WriteLine(JsonSerializer.Serialize(pokemons));
                Console.WriteLine($"\nPage {page + 1}");
                Console.Write("\nShow next page? [Y,n] ");
                shouldContinue = new YesOrNo(Console.ReadLine()).GetValue();
                page++;
            } while (shouldContinue);
        }

        private static void ShowGameName(){
            Console.WriteLine(@" _____                                         _          _      _  ______         _                                    
|_   _|                                       | |        | |    (_) | ___ \       | |                                   
  | |    __ _  _ __ ___    __ _   __ _   ___  | |_   ___ | |__   _  | |_/ /  ___  | | __  ___  _ __ ___    ___   _ __   
  | |   / _` || '_ ` _ \  / _` | / _` | / _ \ | __| / __|| '_ \ | | |  __/  / _ \ | |/ / / _ \| '_ ` _ \  / _ \ | '_ \  
  | |  | (_| || | | | | || (_| || (_| || (_) || |_ | (__ | | | || | | |    | (_) ||   < |  __/| | | | | || (_) || | | | 
  \_/   \__,_||_| |_| |_| \__,_| \__, | \___/  \__| \___||_| |_||_| \_|     \___/ |_|\_\ \___||_| |_| |_| \___/ |_| |_| 
                                  __/ |                                                                                 
                                 |___/                                                                                  
");
        }

    }
}