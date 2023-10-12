using System.Text.Json;
using client;
using contracts;
using static contracts.MainMenuOptions;

namespace service
{

    public interface IUiService
    {
        public void GetUserName();
        public int MainMenuOptions();
        public Task AdoptPokemon();
        public void ShowAdoptedPokemons();
    }

    class UiService : IUiService
    {
        private readonly IPokemonService _pokemonService;
        private string _userName = string.Empty;
        private List<string> _adoptedPokemons = new();

        public UiService()
        {
            _pokemonService = new PokemonService(new PokemonClient());
        }

        public void GetUserName()
        {
            Console.Write("What's your name? ");
            _userName = Console.ReadLine();
        }

        public int MainMenuOptions()
        {
            Console.Clear();
            Console.WriteLine("---------------------- MENU ----------------------");
            Console.WriteLine($"\n{_userName}, what you want to do?\n");
            foreach (var option in availableOptions)
                Console.WriteLine($"{option.Key} - {option.Value}");

            Console.Write("Option: ");
            var actionOption = new MainMenuOptions(Console.ReadLine());
            Console.Clear();
            return actionOption.GetValue();
        }

        public static void ShowGameName()
        {
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

        public async Task AdoptPokemon()
        {
            Console.Clear();
            Console.WriteLine("---------------------- ADOPT A POKÉMON ----------------------");
            Console.WriteLine("Select a Pokémon:");
            int randomPage = new Random().Next(63);
            var pokemons = await _pokemonService.GetAllPokemonsPagedAsync(randomPage);
            for (int index = 0; index < 3; index++)
            {
                int randomPokemon = new Random().Next(19);
                Console.WriteLine($" {pokemons.Results[randomPokemon].Name}");
            }
            Console.Write("Selected Pokémon: ");
            var selectedPokemon = Console.ReadLine();

            Console.WriteLine("\n\n -------------------------------------------------------------");
            Console.WriteLine($"\n {_userName}, what you want to do?");
            Console.WriteLine($"1 - More info about {selectedPokemon}");
            Console.WriteLine($"2 - Adopt {selectedPokemon}");
            Console.WriteLine($"3 - Back");

            var selectedOption = Console.ReadLine();
            int.TryParse(selectedOption, out int selectedOptionParsed);

            switch (selectedOptionParsed)
            {
                case 1:
                    await ShowPokemonInfoAsync(selectedPokemon);
                    break;
                case 2:
                    Console.WriteLine($" {selectedPokemon} adopted!");
                    _adoptedPokemons.Add(selectedPokemon);
                    break;
                case 3:
                    await AdoptPokemon();
                    break;
            }
        }

        public void ShowAdoptedPokemons()
        {
            if (_adoptedPokemons.Count == 0)
                throw new Exception("There is no pokémon selected");

            Console.WriteLine("---------------------- ADOPTED A POKÉMONS ----------------------");
            foreach (var pokemon in _adoptedPokemons)
            {
                Console.WriteLine(pokemon);
            }
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

        private async Task ShowPokemonInfoAsync(string? pokemonSelected)
        {
            var pokemonName = new PokemonOption(pokemonSelected).GetValue();
            var pokemon = await _pokemonService.GetPokemonAsync(pokemonName);
            Console.Clear();
            Console.WriteLine($"Name: {pokemon.Name}");
            Console.WriteLine($"Height: {pokemon.Height}");
            Console.WriteLine($"Weight: {pokemon.Weight}");
            Console.WriteLine("Abilities");
            foreach (var abilityData in pokemon.Abilities)
                Console.WriteLine($" {abilityData.Ability.Name}");
        }
    }

}
