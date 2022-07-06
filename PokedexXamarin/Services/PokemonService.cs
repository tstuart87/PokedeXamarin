using Newtonsoft.Json;
using PokedexXamarin.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PokedexXamarin.Services
{
    public class PokemonService
    {
        private readonly HttpClient _httpClient = new HttpClient();

        //private PokemonService(HttpClient httpClient)
        //{
        //    _httpClient = httpClient;
        //}

        public async Task<List<Pokemon>> GetAllPokemonAsync()
        {
            List<Pokemon> allPokemon = new List<Pokemon>();

            for (int i = 1; i <= 151; i++)
            {
                Pokemon pokemon = await GetPokemonAsync(i);
                allPokemon.Add(pokemon);
            }

            return allPokemon;
        }

        private async Task<Pokemon> GetPokemonAsync(int id)
        {
            Pokemon result = JsonConvert.DeserializeObject<Pokemon>(await _httpClient.GetStringAsync($"https://pokeapi.co/api/v2/pokemon/{id}"));

            return result;
        }
    }
}
