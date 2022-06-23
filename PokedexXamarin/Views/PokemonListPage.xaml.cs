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
            IEnumerable<PokemonViewModel> fetchedPokemons = await GetAllPokemons();

            DisplayAllPokemon(fetchedPokemons);
        }

        private void DisplayAllPokemon(IEnumerable<PokemonViewModel> fetchedPokemons)
        {
            foreach(PokemonViewModel pokemon in fetchedPokemons)
            {
                Label pokeName = CreatePokemonNameLabel(pokemon);

                PokeScrollView.Children.Add(pokeName);
            }
        }

        private async Task<List<PokemonViewModel>> GetAllPokemons()
        {
            IEnumerable<Pokemon2> _allPokemons = await _pokeDataStore.GetAllPokemonAsync(false);
            List<PokemonViewModel> _pokemons = new List<PokemonViewModel>();

            foreach (Pokemon2 pokemon in _allPokemons)
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

        private Label CreatePokemonNameLabel(PokemonViewModel pokemon)
        {
            Label pokeLabel = new Label
            {
                Text = pokemon.Name,
                TextTransform = TextTransform.Uppercase,
                FontSize = 26,
                FontAttributes = FontAttributes.Bold
            };

            return pokeLabel;
        }
    }
}