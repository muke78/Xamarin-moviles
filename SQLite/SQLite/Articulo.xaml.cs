using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite.Modelos;

namespace SQLite
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Articulo : ContentPage
    {
        public Articulo()
        {
            InitializeComponent();
            Tabla();
        }
        private async void Tabla()
        {
            var listaArticulos = await App.SQLiteDB.GetArticulo();
            if (listaArticulos != null)
            {
                lstArticulos.ItemsSource = listaArticulos;
            }
        }

        private async void BtnRegistrar_Clicked(object sender, EventArgs e)
        {
            resultados();

        }

        public async void resultados()
        {

            Articulos art = new Articulos
            {
                titulo = txtTitulo.Text,
                usuario = txtUsuario.Text,
                fecha = txtFecha.Text,
                informacion = txtInformacion.Text
            };

            if (string.IsNullOrEmpty(txtTitulo.Text) || string.IsNullOrEmpty(txtUsuario.Text) || string.IsNullOrEmpty(txtFecha.Text) || string.IsNullOrEmpty(txtInformacion.Text))
            {
                await DisplayAlert("Alerta!", "Debe introducir todos los campos", "Aceptar");
            }
            else
            {
                await DisplayAlert("Correcto", "Se a ingresado correctamente", "Aceptar");

                await App.SQLiteDB.GuardarArticulo(art);
                txtTitulo.Text = "";
                txtUsuario.Text = "";
                txtFecha.Text = "";
                txtInformacion.Text = "";
                Tabla();                
            }
        }

        private async void lstArticulo_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (Articulos)e.SelectedItem;
            btnRegistrar.IsVisible = false;
            txtid.IsVisible = true;
            BtnModificar.IsVisible = true;
            btnBorrar.IsVisible = true;
            if (!string.IsNullOrEmpty(obj.idarticulo.ToString()))
            {
                var articulo = await App.SQLiteDB.GetArticuloByIdAsync(obj.idarticulo);
                if (articulo != null)
                {
                    txtid.Text = articulo.idarticulo.ToString();
                    txtTitulo.Text = articulo.titulo;
                    txtUsuario.Text = articulo.usuario;
                    txtFecha.Text = articulo.fecha;
                    txtInformacion.Text = articulo.informacion;
                }
            }
        }
        private async void btnBorrar_Clicked(object sender, EventArgs e)
        {
            var articulo = await App.SQLiteDB.GetArticuloByIdAsync(Convert.ToInt32(txtid.Text));
            if (articulo != null)
            {
                await App.SQLiteDB.BorrarArticulo(articulo);
                await DisplayAlert("Borrado", "Se a Borrado correctamente", "Aceptar");
                txtid.Text = "";
                txtTitulo.Text = "";
                txtUsuario.Text = "";
                txtFecha.Text = "";
                txtInformacion.Text = "";
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
                Articulos art = new Articulos
                {
                    idarticulo = Convert.ToInt32(txtid.Text),
                    titulo = txtTitulo.Text,
                    usuario = txtUsuario.Text,
                    fecha = txtFecha.Text,
                    informacion = txtInformacion.Text
                };

                await App.SQLiteDB.GuardarArticulo(art);
                await DisplayAlert("Modificacion", "Se a modificado correctamente", "Aceptar");
                txtid.Text = "";
                txtTitulo.Text = "";
                txtUsuario.Text = "";
                txtFecha.Text = "";
                txtInformacion.Text = "";
                txtid.IsVisible = false;
                BtnModificar.IsVisible = false;
                btnRegistrar.IsVisible = true;
                btnBorrar.IsVisible = false;
                Tabla();
            }
        }

    }
}