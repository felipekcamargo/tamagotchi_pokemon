using Model;

namespace View
{
  public interface ITamagotchiView
  {
    public void GetUserName();
    public void ShowGameName();
    public void MainMenuOptions();
    public void ShowAdoptedPokemons(List<string> adoptedPokemons);
    public void ShowAvailablePokemons(Dictionary<int, string> options);
    public void ShowPokemonInfoAsync(Pokemon pokemon);
    public void AdoptedPokemonMessage(string selectedPokemon);
    public void AdoptionOptions(string pokemonName);
  }

  class TamagotchiView : ITamagotchiView
  {
    private string _userName = string.Empty;

    public void GetUserName()
    {
      Console.Write("What's your name? ");
      _userName = Console.ReadLine();
    }

    public void MainMenuOptions()
    {
      var availableOptions = new Dictionary<int, string>()
            {
                {1, "Adopt a Pokémon"},
                {2, "Show all adopted Pokémons"},
                {3, "Exit" }
            };

      PrintOptionsFromDictionary("Menu", $"{_userName}, what you want to do?", availableOptions);
    }

    public void ShowGameName()
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

    public void ShowAvailablePokemons(Dictionary<int, string> options)
    {
      PrintOptionsFromDictionary("Adopt a Pokémon", "Select a Pokémon:", options);
    }

    public void AdoptionOptions(string pokemonName)
    {
      var adoptionOptions = new Dictionary<int, string>{
                {1,$"More info about {pokemonName}"},
                {2,$"Adopt {pokemonName}"},
                {3,"Back"},
            };

      PrintOptionsFromDictionary(
          "Adoption Options",
          $"{_userName}, what you want to do?",
          adoptionOptions
      );
    }

    public void ShowAdoptedPokemons(List<string> adoptedPokemons)
    {
      if (adoptedPokemons.Count == 0)
      {
        Console.WriteLine("\n\nThere is no Pokémon adopted\n\n");
        return;
      }

      Console.WriteLine("\n\n---------------------- ADOPTED A POKÉMONS ----------------------\n\n");
      foreach (var pokemon in adoptedPokemons)
        Console.WriteLine(pokemon);

    }

    public void AdoptedPokemonMessage(string selectedPokemon)
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

    public void ShowPokemonInfoAsync(Pokemon pokemon)
    {
      Console.Clear();
      Console.WriteLine($"---------------------- INFO ABOUT {pokemon.Name.ToUpper()} ----------------------");
      Console.WriteLine($"Name: {pokemon.Name}");
      Console.WriteLine($"Height: {pokemon.Height}");
      Console.WriteLine($"Weight: {pokemon.Weight}");
      Console.WriteLine("Abilities");
      foreach (var abilityData in pokemon.Abilities)
        Console.WriteLine($" {abilityData.Ability.Name}");
    }

    private static void PrintOptionsFromDictionary(string title, string description, Dictionary<int, string> options)
    {
      Console.WriteLine($"---------------------- {title.ToUpper()} ----------------------");
      Console.WriteLine($"\n{description}\n");

      foreach (var option in options)
        Console.WriteLine($"{option.Key} - {option.Value}");

      Console.Write("\nOption: ");
    }
  }
}
