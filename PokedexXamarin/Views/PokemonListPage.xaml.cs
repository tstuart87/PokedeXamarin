using PokedexXamarin.ViewModels;
using PokedexXamarin.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using PokedexXamarin.Models;

namespace PokedexXamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PokemonListPage : ContentPage
    {
        static HttpClient httpClient = new HttpClient();
        private readonly PokemonDataStore _pokeDataStore = new PokemonDataStore(httpClient);


        public PokemonListPage()
        {
            InitializeComponent();
            ListAllPokemon();
        }

        public async void ListAllPokemon()
        {
            List<PokemonViewModel> fetchedPokemons = await GetAllPokemons();
            Dictionary<string, Image> _pokemonDict = CreateNameAndImageXamlElements(fetchedPokemons);

            DisplayAllPokemon(_pokemonDict);
        }

        private void DisplayAllPokemon(Dictionary<string, Image> _pokemonDict)
        {
            foreach(KeyValuePair<string, Image> pokemon in _pokemonDict)
            {
                Label pokeName = CreatePokemonNameLabel(pokemon.Key);

                PokeScrollView.Children.Add(pokeName);
                PokeScrollView.Children.Add(pokemon.Value);
            }
        }

        private async Task<List<PokemonViewModel>> GetAllPokemons()
        {
            IEnumerable<Pokemon> _allPokemons = await _pokeDataStore.GetAllPokemonAsync(false);
            List<PokemonViewModel> _pokemons = new List<PokemonViewModel>();

            foreach (Pokemon pokemon in _allPokemons)
            {
                PokemonViewModel pokeModel = new PokemonViewModel()
                {
                    Id = pokemon.Id,
                    Name = pokemon.Name,
                    Weight = pokemon.Weight,
                    Height = pokemon.Height,
                    Experience = pokemon.BaseExperience,
                    Order = pokemon.Order,
                    ImageURL = pokemon.Sprites.FrontDefault,
                    Move = pokemon.Moves[0].Move.Name,
                    Type = pokemon.Types[0].Type.Name
                };

                _pokemons.Add(pokeModel);
            }

            return _pokemons;
        }

        private Dictionary<string, Image> CreateNameAndImageXamlElements(List<PokemonViewModel> allPokemon)
        {
            Dictionary<string, Image> pokeDictionary = new Dictionary<string, Image>();

            foreach(PokemonViewModel pokemon in allPokemon)
            {
                Image image = new Image
                {
                    Source = pokemon.ImageURL,
                    Scale = 3.0
                };

                pokeDictionary.Add(pokemon.Name, image);
            }

            return pokeDictionary;
        }

        private Label CreatePokemonNameLabel(string pokemonName)
        {
            Label pokeLabel = new Label
            {
                Text = pokemonName,
                TextTransform = TextTransform.Uppercase,
                FontSize = 26,
                FontAttributes = FontAttributes.Bold
            };

            return pokeLabel;
        }
    }
}