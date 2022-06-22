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

        public async void FetchPokemon(object sender, EventArgs args)
        {
            string nameSearched = SearchBoxInputField.Text;

            PokemonViewModel fetchedPokemon = await GetNewPokemon(nameSearched);

            DisplayPokemon(fetchedPokemon);
        }

        public async void FetchNextPokemon(object sender, EventArgs args)
        {
            string pokeId = (Convert.ToInt32(HiddenValues.Text) + 1).ToString();

            PokemonViewModel pokemon = await GetNewPokemon(pokeId);

            DisplayPokemon(pokemon);
        }

        public async void FetchPrevPokemon(object sender, EventArgs args)
        {
            string pokeId = (Convert.ToInt32(HiddenValues.Text) - 1).ToString();

            PokemonViewModel pokemon = await GetNewPokemon(pokeId);

            DisplayPokemon(pokemon);
        }

        public async void FetchRandomPokemon(object sender, EventArgs args)
        {
            string pokeId = (new Random().Next(1, 151)).ToString();

            PokemonViewModel pokemon = await GetNewPokemon(pokeId);

            DisplayPokemon(pokemon);
        }

        private void DisplayPokemon(PokemonViewModel pokemon)
        {
            Label pokeName = CreatePokemonNameLabel(pokemon);
            Image pokeImage = CreatePokemonImage(pokemon);
            Label pokePhysicalStats = CreatePhysicalStatsLabel(pokemon);
            Label pokeMoveTypeStats = CreateMoveTypeLabel(pokemon);

            ClearContent();

            HiddenValues.Text = pokemon.Id.ToString();
            PokeName.Children.Add(pokeName);
            PokeImage.Children.Add(pokeImage);
            PokePhysicalStats.Children.Add(pokePhysicalStats);
            PokeMoveTypeStats.Children.Add(pokeMoveTypeStats);
        }

        private async Task<PokemonViewModel> GetNewPokemon(string name)
        {
            Pokemon2 result = await _pokeDataStore.GetPokemonAsync(name);

            PokemonViewModel _pokemon = new PokemonViewModel()
            {
                Id = result.Id,
                Name = result.Name,
                Weight = result.Weight,
                Height = result.Height,
                Experience = result.BaseExperience,
                Order = result.Order,
                ImageURL = result.Sprites.FrontDefault,
                Move = result.Moves[0].Move.Name,
                Type = result.Types[0].Type.Name
            };

            return _pokemon;
        }

        private Label CreatePokemonNameLabel(PokemonViewModel pokemon)
        {
            Label pokeLabel = new Label
            {
                Text = pokemon.Name,
                TextTransform = TextTransform.Uppercase,
                HorizontalOptions = LayoutOptions.Start,
                VerticalTextAlignment = TextAlignment.Center,
                FontSize = 30,
                FontAttributes = FontAttributes.Bold
            };

            return pokeLabel;
        }

        private Label CreatePhysicalStatsLabel(PokemonViewModel pokemon)
        {
            Label pokeLabel = new Label
            {
                Text = $"Height: {pokemon.Height}\n" +
                       $"Weight: {pokemon.Weight}",
                FontSize = 24,
                TextColor = Color.White,
                Padding = 5
                
            };

            return pokeLabel;
        }

        private Label CreateMoveTypeLabel(PokemonViewModel pokemon)
        {
            Label pokeLabel = new Label
            {
                Text = $"Type: {pokemon.Type}\n" +
                       $"Move: {pokemon.Move}\n" +
                       $"XP: {pokemon.Experience}",
                FontSize = 18,
                Padding = 5,
                TextColor = Color.White,
                HorizontalTextAlignment = TextAlignment.End
            };

            return pokeLabel;
        }

        private Image CreatePokemonImage(PokemonViewModel pokemon)
        {
            Image pokeImage = new Image()
            {
                Source = pokemon.ImageURL,
                Scale = 10.0,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            return pokeImage;
        }

        private void ClearContent()
        {
            PokeName.Children.Clear();
            PokeImage.Children.Clear();
            PokePhysicalStats.Children.Clear();
            PokeMoveTypeStats.Children.Clear();
        }
    }
}