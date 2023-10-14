using System.Text.RegularExpressions;

namespace Model
{
    class PokemonOption
    {
        private readonly string _nameOrId;
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