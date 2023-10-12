using client;
using contracts;

namespace service
{
    public interface IPokemonService {
        Task<Pokemons?> GetAllPokemonsPagedAsync(int page = 0);
        Task<Pokemon?> GetPokemonAsync(string nameOrId);
    }

    class PokemonService: IPokemonService
    {
        private readonly IPokemonClient _pokemonClient;
        public PokemonService(IPokemonClient pokemonClient)
        {
            _pokemonClient = pokemonClient;
        }
        public async Task<Pokemons?> GetAllPokemonsPagedAsync(int page = 0) {
            
            var limit = 20;
            var offset = limit*page;
            var pokemons = await _pokemonClient.GetPokemons(offset, limit);
            return pokemons;
        }

        public async Task<Pokemon?> GetPokemonAsync(string nameOrId)
        {
            var pokemon = await _pokemonClient.GetPokemon(nameOrId);
            return pokemon;
        }
        
    }


}