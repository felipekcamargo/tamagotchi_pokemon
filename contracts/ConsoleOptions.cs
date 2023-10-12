using System.Text.RegularExpressions;

namespace contracts
{
    class MainMenuOptions
    {
        private int SelectedOption { get; set; }
        public static readonly Dictionary<int, string> availableOptions = new()
        {
            {1, "Adopt a Pokémon"},
            {2, "Show all adopted Pokémons"},
            {3, "Exit" }
        };

        public MainMenuOptions(string? selectedOption)
        {
            if (!int.TryParse(selectedOption, out var parsedSelectedOption))
                throw new Exception("Invalid input");

            ValidateOptions(parsedSelectedOption);
            SelectedOption = parsedSelectedOption;
        }

        public int GetValue() => SelectedOption;

        private void ValidateOptions(int parsedSelectedOption)
        {
            if (!availableOptions.ContainsKey(parsedSelectedOption))
                throw new Exception("Option not supported");
        }

    }

    class PokemonOption
    {
        private string _nameOrId;
        public PokemonOption(string? nameOrId)
        {
            ValidateInput(nameOrId);
            _nameOrId = nameOrId!;
        }

        public string GetValue() => _nameOrId;

        private static void ValidateInput(string? nameOrId)
        {
            if(string.IsNullOrEmpty(nameOrId))
                throw new Exception("Invalid input");
        }

    }

    class YesOrNo{

        private readonly bool _yesOrNo;
        private const string _yesRegex = "^[yY]{1}[eE]?[sS]?$";
        private const string _noRegex = "^[nN]{1}[oO]?$";

        public YesOrNo(string? selectedOption)
        {
            if(string.IsNullOrWhiteSpace(selectedOption)|| Regex.IsMatch(selectedOption, _yesRegex ))
                _yesOrNo = true;
            else if (Regex.IsMatch(selectedOption, _noRegex))
                    _yesOrNo = false;
            else
                throw new Exception("Invalid option");
        }

        public bool GetValue() => _yesOrNo;
    }

}