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

        public async Task<Pokemon2> GetPokemonAsync(string name)
        {
            Pokemon2 result = JsonConvert.DeserializeObject<Pokemon2>(await _httpClient.GetStringAsync($"https://pokeapi.co/api/v2/pokemon/{name.ToLower()}"));

            return result;
        }

        public async Task<IEnumerable<Pokemon2>> GetAllPokemonAsync(bool forceRefresh = false)
        {
            List<Pokemon2> _pokemons = new List<Pokemon2>();

            for (int i = 0; i <= 151; i++)
            {
                Pokemon2 pokemon = await GetPokemonAsync(i.ToString());
                _pokemons.Add(pokemon);
            }

            return _pokemons;
        }
    }
}
