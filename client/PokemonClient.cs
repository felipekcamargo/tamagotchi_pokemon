using System.Net.Http.Json;
using contracts.Pokemons;

namespace client.PokemonClient
{
    public interface IPokemonClient
    {
        Task<string> GetPokemon(string nameOrId);
        Task<Pokemons?> GetPokemons();
    }

    public class PokemonClient : IPokemonClient
    {
        private readonly HttpClient _httpClient;
        private const string _basePokeUrl = "https://pokeapi.co/api/v2/pokemon/";

        public PokemonClient()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> GetPokemon(string nameOrId) //TODO: Arrumar o contrato
        {
            var response = await _httpClient.GetAsync(_basePokeUrl + nameOrId);
            response.EnsureSuccessStatusCode();
            var pokemon = await response.Content.ReadAsStringAsync();
            return pokemon;
        }

        public async Task<Pokemons?> GetPokemons()
        {
            var response = await _httpClient.GetAsync(_basePokeUrl);
            response.EnsureSuccessStatusCode();
            var pokemons = await response.Content.ReadFromJsonAsync<Pokemons>();
            return pokemons;
        }
    }
}