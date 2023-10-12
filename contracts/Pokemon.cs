namespace contracts
{
    public class Pokemon
    {
        public string Name { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public AbilityData[] Abilities { get; set; }
    }

    public class AbilityData
    {
        public NameUrl Ability { get; set; }
    }

    public class NameUrl
    {
        public string Name { get; set; }
    }
}
