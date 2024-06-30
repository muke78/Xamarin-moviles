using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Navegacion.Modelos;

namespace Navegacion
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Configuracion : ContentPage
    {
        public Configuracion()
        {
            InitializeComponent();
            Tabla();
        }

       

        private async void btnRegistrar_Clicked(object sender, EventArgs e)
        {
            resultados();
        }

        private async void Tabla()
        {
            var listaGeneros = await App.SQLiteDB.GetGeneros();
            if (listaGeneros != null)
            {
                lstGeneros.ItemsSource = listaGeneros;
            }
        }

        public async void resultados()
        {

            Generosm gen = new Generosm
            {
                genero = txtGenero.Text,


            };

            if (string.IsNullOrEmpty(txtGenero.Text))
            {
                await DisplayAlert("Alerta!", "Debe introducir todos los campos", "Aceptar");
            }
            else
            {
                await DisplayAlert("Correcto", "Se a ingresado correctamente", "Aceptar");

                await App.SQLiteDB.GuardarGenero(gen);
                txtGenero.Text = "";
                Tabla();
                


                var listaGeneros = await App.SQLiteDB.GetGeneros();
                if (listaGeneros!= null)
                {
                    lstGeneros.ItemsSource = listaGeneros;
                }
            }
        }

        private async void btnBorrar_Clicked(object sender, EventArgs e)
        {
            var generos = await App.SQLiteDB.GetGeneroByIdAsync(Convert.ToInt32(txtid.Text));
            if (generos != null)
            {
                await App.SQLiteDB.BorrarGenero(generos);
                await DisplayAlert("Borrado", "Se a Borrado correctamente", "Aceptar");
                txtid.Text = "";
                txtGenero.Text = "";
                txtid.IsVisible = false;
                BtnModificar.IsVisible = false;
                btnRegistrar.IsVisible = true;
                btnBorrar.IsVisible = false;
                Tabla();
            }

        }

        private async void BtnModificar_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtid.Text))
            {
                Generosm gen = new Generosm
                {
                    matricula = Convert.ToInt32(txtid.Text),
                    genero = txtGenero.Text,


                };

                await App.SQLiteDB.GuardarGenero(gen);
                await DisplayAlert("Modificacion", "Se a modificado correctamente", "Aceptar");
                txtid.Text = "";
                txtGenero.Text = "";
                txtid.IsVisible = false;
                BtnModificar.IsVisible = false;
                btnRegistrar.IsVisible = true;
                btnBorrar.IsVisible = false;
                Tabla();
            }
        }

        private async void lstGeneros_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (Generosm)e.SelectedItem;
            btnRegistrar.IsVisible = false;
            txtid.IsVisible = true;
            BtnModificar.IsVisible = true;
            btnBorrar.IsVisible = true;
            if (!string.IsNullOrEmpty(obj.matricula.ToString()))
            {
                var Generos = await App.SQLiteDB.GetGeneroByIdAsync(obj.matricula);
                if (Generos != null)
                {
                    txtid.Text = Generos.matricula.ToString();
                    txtGenero.Text = Generos.genero;
                    
                }
            }
        }
    }
}