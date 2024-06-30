using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Navegacion.Modelos;
using Navegacion.BASEDATOS;

namespace Navegacion
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BaseDatos : ContentPage
    {
        public BaseDatos()
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
            var listaSeries = await App.SQLiteDB.Getseries();
            if (listaSeries != null)
            {
                lstPeliculas.ItemsSource = listaSeries;
            }
        }

        public async void resultados()
        {
            Peliculasm pel = new Peliculasm
            {
                nombrePelicula = txtNombre.Text,
                usuario = txtUsuario.Text,
                genero = txtGenero.Text,
                año = txtAnio.Text

            };

            if (string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtUsuario.Text) || string.IsNullOrEmpty(txtGenero.Text) || string.IsNullOrEmpty(txtAnio.Text))
            {
                await DisplayAlert("Alerta!", "Debe introducir todos los campos", "Aceptar");
            }
            else
            {
                await DisplayAlert("Correcto", "Se a ingresado correctamente", "Aceptar");

                await App.SQLiteDB.GuardarPelicula(pel);
                txtNombre.Text = "";
                txtUsuario.Text = "";
                txtGenero.Text = "";
                txtAnio.Text = "";


                var listaArticulos = await App.SQLiteDB.GetPelicula();
                if (listaArticulos != null)
                {
                    lstPeliculas.ItemsSource = listaArticulos;
                }
            }
        }

        private async void btnBorrar_Clicked(object sender, EventArgs e)
        {
            var peliculas = await App.SQLiteDB.GetPeliculaByIdAsync(Convert.ToInt32(txtid.Text));
            if (peliculas != null)
            {
                await App.SQLiteDB.BorrarPelicula(peliculas);
                await DisplayAlert("Borrado", "Se a Borrado correctamente", "Aceptar");
                txtid.Text = "";
                txtNombre.Text = "";
                txtUsuario.Text = "";
                txtGenero.Text = "";
                txtAnio.Text = "";
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
                Peliculasm pel = new Peliculasm
                {
                    matricula = Convert.ToInt32(txtid.Text),
                    nombrePelicula = txtNombre.Text,
                    usuario = txtUsuario.Text,
                    genero = txtGenero.Text,
                    año = txtAnio.Text

                };

                await App.SQLiteDB.GuardarPelicula(pel);
                await DisplayAlert("Modificacion", "Se a modificado correctamente", "Aceptar");
                txtid.Text = "";
                txtNombre.Text = "";
                txtUsuario.Text = "";
                txtGenero.Text = "";
                txtAnio.Text = "";
                txtid.IsVisible = false;
                BtnModificar.IsVisible = false;
                btnRegistrar.IsVisible = true;
                btnBorrar.IsVisible = false;
                Tabla();
            }
        }

        private async void lstPeliculas_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (Peliculasm)e.SelectedItem;
            btnRegistrar.IsVisible = false;
            txtid.IsVisible = true;
            BtnModificar.IsVisible = true;
            btnBorrar.IsVisible = true;
            if (!string.IsNullOrEmpty(obj.matricula.ToString()))
            {
                var peliculas = await App.SQLiteDB.GetPeliculaByIdAsync(obj.matricula);
                if (peliculas != null)
                {
                    txtid.Text = peliculas.matricula.ToString();
                    txtNombre.Text = peliculas.nombrePelicula;
                    txtUsuario.Text = peliculas.usuario;
                    txtGenero.Text = peliculas.genero;
                    txtAnio.Text = peliculas.año;

                }
            }
        }
    }
}