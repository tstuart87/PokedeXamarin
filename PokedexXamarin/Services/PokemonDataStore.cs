using Newtonsoft.Json;
using PokedexXamarin.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace PokedexXamarin.Services
{
    public class PokemonDataStore
    {
        private readonly HttpClient _httpClient;

        public PokemonDataStore(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Pokemon> GetPokemonAsync(string name)
        {
            Pokemon result = JsonConvert.DeserializeObject<Pokemon>(await _httpClient.GetStringAsync($"https://pokeapi.co/api/v2/pokemon/{name.ToLower()}"));

            return result;
        }

        public async Task<IEnumerable<Pokemon>> GetAllPokemonAsync(bool forceRefresh = false)
        {
            List<Pokemon> _pokemons = new List<Pokemon>();

            for (int i = 1; i <= 151; i++)
            {
                Pokemon pokemon = await GetPokemonAsync(i.ToString());
                _pokemons.Add(pokemon);
            }

            return _pokemons;
        }
    }
}
