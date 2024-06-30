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
    public partial class Objetivos : ContentPage
    {
        public Objetivos()
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
                lstSeries.ItemsSource = listaSeries;
            }
        }

        public async void resultados()
        {
            Seriesm ser = new Seriesm
            {
                nombreSerie = txtNombre.Text,
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

                await App.SQLiteDB.GuardarSerie(ser);
                txtNombre.Text = "";
                txtUsuario.Text = "";
                txtGenero.Text = "";
                txtAnio.Text = "";
                Tabla();
              

                var listaSeries = await App.SQLiteDB.Getseries();
                if (listaSeries != null)
                {
                    lstSeries.ItemsSource = listaSeries;
                }
            }
        }

        private async void btnBorrar_Clicked(object sender, EventArgs e)
        {
            var series = await App.SQLiteDB.GetSerieByIdAsync(Convert.ToInt32(txtid.Text));
            if (series != null)
            {
                await App.SQLiteDB.BorrarSerie(series);
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
                Seriesm ser = new Seriesm
                {
                    matricula = Convert.ToInt32(txtid.Text),
                    nombreSerie = txtNombre.Text,
                    usuario = txtUsuario.Text,
                    genero = txtGenero.Text,
                    año = txtAnio.Text

                };

                await App.SQLiteDB.GuardarSerie(ser);
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

        private async void lstSeries_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (Seriesm)e.SelectedItem;
            btnRegistrar.IsVisible = false;
            txtid.IsVisible = true;
            BtnModificar.IsVisible = true;
            btnBorrar.IsVisible = true;
            if (!string.IsNullOrEmpty(obj.matricula.ToString()))
            {
                var series = await App.SQLiteDB.GetSerieByIdAsync(obj.matricula);
                if (series != null)
                {
                    txtid.Text = series.matricula.ToString();
                    txtNombre.Text = series.nombreSerie;
                    txtUsuario.Text = series.usuario;
                    txtGenero.Text = series.genero;
                    txtAnio.Text = series.año;
                    
                }
            }
        }
    }
}