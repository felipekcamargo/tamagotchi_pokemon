using Client;
using Service;
using View;

namespace Controller
{
  public class TamagotchiController
  {
    private readonly ITamagotchiView _tamagotchiView;
    private readonly IPokemonService _pokemonService;

    private List<string> _adoptedPokemons = new();

    public TamagotchiController()
    {
      _tamagotchiView = new TamagotchiView();
      _pokemonService = new PokemonService(new PokemonClient());
    }

    public async Task MainMenu()
    {
      try
      {
        Console.Clear();
        _tamagotchiView.ShowGameName();
        _tamagotchiView.GetUserName();
        Console.Clear();

        var continueGame = true;
        while (continueGame)
        {
          _tamagotchiView.MainMenuOptions();
          var selectedOption = GetOptionFromUser();
          switch (selectedOption)
          {
            case 1:
              await AdoptPokemon();
              break;
            case 2:
              Console.Clear();
              _tamagotchiView.ShowAdoptedPokemons(_adoptedPokemons);
              break;
            case 3:
              continueGame = false;
              break;
            default:
              Console.WriteLine("Invalid option");
              break;
          }
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
    }

    private async Task AdoptPokemon()
    {
      var availablePokemons = await GetAvailablePokemons();
      _tamagotchiView.ShowAvailablePokemons(availablePokemons);
      var selectedOption = GetOptionFromUser();

      var pokemonName = availablePokemons[selectedOption];

      var continueOptions = true;

      while (continueOptions)
      {
        _tamagotchiView.AdoptionOptions(pokemonName);
        selectedOption = GetOptionFromUser();

        switch (selectedOption)
        {
          case 1:
            var pokemon = await _pokemonService.GetPokemonAsync(pokemonName);
            _tamagotchiView.ShowPokemonInfoAsync(pokemon);
            break;
          case 2:
            _tamagotchiView.AdoptedPokemonMessage(pokemonName);
            _adoptedPokemons.Add(pokemonName);
            continueOptions = false;
            break;
          case 3:
            continueOptions = false;
            break;

        }
      }
    }

    private static int GetOptionFromUser()
    {
      var selectedOption = Console.ReadLine();

      if (!int.TryParse(selectedOption, out var parsedSelectedOption))
        throw new Exception("Invalid input");
      return parsedSelectedOption;
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

  }
}
