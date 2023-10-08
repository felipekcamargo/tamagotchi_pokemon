using System.Net.Http.Json;
using contracts.Pokemons;
using contracts.Pokemon;

namespace client.PokemonClient
{
    public interface IPokemonClient
    {
        Task<Pokemon?> GetPokemon(string nameOrId);
        Task<Pokemons?> GetPokemons(int offset = 0, int limit = 20);
    }

    public class PokemonClient : IPokemonClient
    {
        private readonly HttpClient _httpClient;
        private const string _basePokeUrl = "https://pokeapi.co/api/v2/pokemon/";

        public PokemonClient()
        {
            _httpClient = new HttpClient();
        }

        public async Task<Pokemon?> GetPokemon(string nameOrId)
        {
            var response = await _httpClient.GetAsync(_basePokeUrl + nameOrId);
            response.EnsureSuccessStatusCode();
            var pokemon = await response.Content.ReadFromJsonAsync<Pokemon>();
            return pokemon;
        }

        public async Task<Pokemons?> GetPokemons(int offset = 0, int limit = 20)
        {            
            var response = await _httpClient.GetAsync(_basePokeUrl+ $"?offset={offset}&limit={limit}");
            response.EnsureSuccessStatusCode();
            var pokemons = await response.Content.ReadFromJsonAsync<Pokemons>();
            return pokemons;
        }
    }
}