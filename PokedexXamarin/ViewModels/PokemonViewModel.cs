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
        public int Order { get; set; }
        public string ImageURL { get; set; }
        public string Ability { get; set; }
    }
}
