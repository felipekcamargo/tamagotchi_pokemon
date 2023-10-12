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
                Console.Clear();
                await ShowOptions();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task ShowOptions()
        {
            var selectedOption = _uiService.MainMenuOptions();
            switch (selectedOption)
            {
                case 1:
                    await _uiService.AdoptAPokemon();
                    await ShowOptions();
                    break;
                case 2:
                    Console.Clear();
                    _uiService.ShowAdoptedPokemons();
                    await ShowOptions();
                    break;
                case 3:
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
        }
    }
}