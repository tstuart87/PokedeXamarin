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
    public partial class PokemonPage : ContentPage
    {
        static HttpClient httpClient = new HttpClient();
        private readonly PokemonDataStore _pokeDataStore = new PokemonDataStore(httpClient);

        public PokemonPage()
        {
            InitializeComponent();
        }

        public async void HandleEnterPress(object sender, EventArgs args)
        {
            string nameSearch = SearchBoxInputField.Text;

            PokemonViewModel fetchedPokemon = await GetNewPokemon(nameSearch);

            Label pokeLabel = CreatePokemonLabel(fetchedPokemon);
            Image pokeImage = CreatePokemonImage(fetchedPokemon);
            Label pokeStats = CreateStatsLabel(fetchedPokemon);

            PokeBox.Children.Clear();

            PokeBox.Children.Add(pokeLabel);
            PokeImage.Children.Add(pokeImage);
            PokeBox.Children.Add(pokeStats);
        }

        private async Task<PokemonViewModel> GetNewPokemon(string name)
        {
            Pokemon result = await _pokeDataStore.GetPokemonAsync(name);

            PokemonViewModel _pokemon = new PokemonViewModel()
            {
                Name = result.Name,
                ImageURL = result.ImageURL,
                Weight = result.Weight,
                Height = result.Height,
                Experience = result.Experience,
                Ability = result.Ability
            };

            return _pokemon;
        }

        private Label CreatePokemonLabel(PokemonViewModel pokemon)
        {
            Label pokeLabel = new Label
            {
                Text = pokemon.Name,
                TextTransform = TextTransform.Uppercase,
                FontSize = 12
            };

            return pokeLabel;
        }

        private Label CreateStatsLabel(PokemonViewModel pokemon)
        {
            Label pokeLabel = new Label
            {
                Text = $"Height: {pokemon.Height}\n" +
                       $"Weight: {pokemon.Weight}\n" +
                       $"Weight: {pokemon.Ability}\n" +
                       $"XP: {pokemon.Experience}",
                FontSize = 9
            };

            return pokeLabel;
        }

        private Image CreatePokemonImage(PokemonViewModel pokemon)
        {
            Image pokeImage = new Image()
            {
                Source = pokemon.ImageURL,
                Scale = 8.0,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            return pokeImage;
        }
    }
}