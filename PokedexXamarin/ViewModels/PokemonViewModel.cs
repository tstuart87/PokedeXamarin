using System;
using System.Collections.Generic;
using System.Text;

namespace PokedexXamarin.ViewModels
{
    public class PokemonViewModel : BaseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }
        public int Experience { get; set; }
        public Uri ImageURL { get; set; }
        public string Move { get; set; }
        public string Type { get; set; }
    }
}
