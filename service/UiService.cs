using client;
using contracts;
using static contracts.MainMenuOptions;

namespace service
{

    public interface IUiService
    {
        public void GetUserName();
        public int MainMenuOptions();
        public Task AdoptAPokemon();
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
            var selectedOption = new MainMenuOptions(ShowMenuAndGetUserOption("Menu", $"{_userName}, what you want to do?", availableOptions));

            return selectedOption.GetValue();
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

        public async Task AdoptAPokemon()
        {
            var availablePokemons = await GetAvailablePokemons();
            var selectedPokemon = ShowMenuAndGetUserOption("Adopt a Pokémon", "Select a Pokémon:", availablePokemons);
            var selectedPokemonName = availablePokemons[selectedPokemon];
            await AdoptionOptions(selectedPokemonName);
        }

        private async Task AdoptionOptions(string pokemonName)
        {
            var adoptionOptions = new Dictionary<int, string>{
                {1,$"More info about {pokemonName}"},
                {2,$"Adopt {pokemonName}"},
                {3,"Back"},
            };

            var selectedOption = ShowMenuAndGetUserOption(
                "Adoption Options",
                $"{_userName}, what you want to do?",
                adoptionOptions
            );

            switch (selectedOption)
            {
                case 1:
                    await ShowPokemonInfoAsync(pokemonName);
                    await AdoptionOptions(pokemonName);
                    break;
                case 2:
                    AdoptedPokemonMessage(pokemonName);
                    _adoptedPokemons.Add(pokemonName);
                    break;
                case 3:
                    break;
            }
        }

        private async Task<Dictionary<int, string>> GetAvailablePokemons()
        {
            int randomPage = new Random().Next(63);
            var pokemons = await _pokemonService.GetAllPokemonsPagedAsync(randomPage);
            var pokemonsAvailable = new Dictionary<int, string>();

            for (int index = 0; index < 3; index++)
            {
                int randomPokemon = new Random().Next(19);
                pokemonsAvailable.Add(index + 1, pokemons!.Results![randomPokemon].Name!);
            }

            return pokemonsAvailable;
        }

        public void ShowAdoptedPokemons()
        {
            if (_adoptedPokemons.Count == 0){
                Console.WriteLine("\n\nThere is no Pokémon adopted\n\n");
                return;
            }

            Console.WriteLine("\n\n---------------------- ADOPTED A POKÉMONS ----------------------\n\n");
            foreach (var pokemon in _adoptedPokemons)
                Console.WriteLine(pokemon);
            
        }

        private static void AdoptedPokemonMessage(string selectedPokemon)
        {
            Console.Clear();
            Console.WriteLine($"{selectedPokemon} adopted!");
            Console.WriteLine(@"
                              ████                              
                            ██░░░░██                            
                          ██░░░░░░░░██                          
                          ██░░░░░░░░██                          
                        ██░░░░░░░░░░░░██                        
                        ██░░░░░░░░░░░░██                        
                        ██░░░░░░░░░░░░██                        
                          ██░░░░░░░░██                          
                            ████████                            ");
        }

        private async Task ShowPokemonInfoAsync(string? pokemonSelected)
        {
            var pokemonName = new PokemonOption(pokemonSelected).GetValue();
            var pokemon = await _pokemonService.GetPokemonAsync(pokemonName);
            Console.Clear();
            Console.WriteLine($"---------------------- INFO ABOUT {pokemon.Name.ToUpper()} ----------------------");
            Console.WriteLine($"Name: {pokemon.Name}");
            Console.WriteLine($"Height: {pokemon.Height}");
            Console.WriteLine($"Weight: {pokemon.Weight}");
            Console.WriteLine("Abilities");
            foreach (var abilityData in pokemon.Abilities)
                Console.WriteLine($" {abilityData.Ability.Name}");
        }

        private static int ShowMenuAndGetUserOption(string title, string description, Dictionary<int, string> options)
        {
            Console.WriteLine($"---------------------- {title.ToUpper()} ----------------------");
            Console.WriteLine($"\n{description}\n");

            foreach (var option in options)
                Console.WriteLine($"{option.Key} - {option.Value}");

            Console.Write("\nOption: ");

            var selectedOption = Console.ReadLine();

            if (!int.TryParse(selectedOption, out var parsedSelectedOption))
                throw new Exception("Invalid input");

            if (!options.ContainsKey(parsedSelectedOption))
                throw new Exception("Option not available :(");

                return parsedSelectedOption;
        }
    }
}
