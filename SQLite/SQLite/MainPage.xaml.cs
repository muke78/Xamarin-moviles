using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SQLite.Modelos;

namespace SQLite
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

        }

        private async void Btn_articulos_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Articulo());
        }

        private async void Btn_Usuarios_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Usuarios());
        }

        private async void Btn_productos_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Productos());
        }
    }
}