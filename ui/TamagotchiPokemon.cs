using service;

namespace ui
{
    public class TamagotchiPokemon
    {
        private readonly IUiService _uiService;

        public TamagotchiPokemon()
        {
            _uiService = new UiService();
        }

        public async Task MainMenu()
        {
            try
            {
                Console.Clear();
                UiService.ShowGameName();
                _uiService.GetUserName();
                var selectedOption = _uiService.MainMenuOptions();
                switch (selectedOption)
                {
                    case 1:
                        await _uiService.AdoptPokemon();
                        break;
                    case 2:
                        _uiService.ShowAdoptedPokemons();
                        break;
                    case 3:
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
    }
}