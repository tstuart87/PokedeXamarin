using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PokedexXamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PokedexView : ContentView
    {
        public PokedexView()
        {
            InitializeComponent();
        }
    }
}