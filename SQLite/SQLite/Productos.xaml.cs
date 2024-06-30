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
    public partial class Productos : ContentPage
    {
        public Productos()
        {
            InitializeComponent();
            Tabla();
        }

        private async void BtnRegistrar_Clicked(object sender, EventArgs e)
        {
            resultados();
        }
        private async void Tabla()
        {
            var listaProductos = await App.SQLiteDB.GetProductos();
            if (listaProductos != null)
            {
                lstProductos.ItemsSource = listaProductos;
            }
        }

        public async void resultados()
        {
            ProductosM prod = new ProductosM
            {
                nombreproducto = txtNombreproducto.Text,
                compañia = txtCompañia.Text,
                usuario = txtUsuario.Text,
                Fecha = txtFecha.Text
            };

            if (string.IsNullOrEmpty(txtNombreproducto.Text) || string.IsNullOrEmpty(txtCompañia.Text) || string.IsNullOrEmpty(txtUsuario.Text) || string.IsNullOrEmpty(txtFecha.Text))
            {
                await DisplayAlert("Alerta!", "Debe introducir todos los campos", "Aceptar");
            }
            else
            {
                await DisplayAlert("Correcto", "Se a ingresado correctamente", "Aceptar");

                await App.SQLiteDB.GuardarProductos(prod);
                txtNombreproducto.Text = "";
                txtCompañia.Text = "";
                txtUsuario.Text = "";
                txtFecha.Text = "";
                Tabla();
            }
        }

        private async void btnBorrar_Clicked(object sender, EventArgs e)
        {
            var product = await App.SQLiteDB.GetProductosByIdAsync(Convert.ToInt32(txtidproducto.Text));
            if (product != null)
            {
                await App.SQLiteDB.BorrarProductos(product);
                await DisplayAlert("Borrado", "Se a Borrado correctamente", "Aceptar");
                txtidproducto.Text = "";
                txtNombreproducto.Text = "";
                txtCompañia.Text = "";
                txtUsuario.Text = "";
                txtFecha.Text = "";
                txtidproducto.IsVisible = false;
                BtnModificar.IsVisible = false;
                btnRegistrar.IsVisible = true;
                btnBorrar.IsVisible = false;
                Tabla();
            }

        }

        private async void BtnModificar_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtidproducto.Text))
            {
                ProductosM prod = new ProductosM
                {
                    idproducto = Convert.ToInt32(txtidproducto.Text),
                    nombreproducto = txtNombreproducto.Text,
                    compañia = txtCompañia.Text,
                    usuario = txtUsuario.Text,
                    Fecha = txtFecha.Text
                };

                await App.SQLiteDB.GuardarProductos(prod);
                await DisplayAlert("Modificacion", "Se a modificado correctamente", "Aceptar");
                txtidproducto.Text = "";
                txtNombreproducto.Text = "";
                txtCompañia.Text = "";
                txtUsuario.Text = "";
                txtFecha.Text = "";
                txtidproducto.IsVisible = false;
                BtnModificar.IsVisible = false;
                btnRegistrar.IsVisible = true;
                btnBorrar.IsVisible = false;
                Tabla();
            }
        }

        private async void lstProductos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (ProductosM)e.SelectedItem;
            btnRegistrar.IsVisible = false;
            txtidproducto.IsVisible = true;
            BtnModificar.IsVisible = true;
            btnBorrar.IsVisible = true;
            if (!string.IsNullOrEmpty(obj.idproducto.ToString()))
            {
                var Producto = await App.SQLiteDB.GetProductosByIdAsync(obj.idproducto);
                if (Producto != null)
                {
                    txtidproducto.Text = Producto.idproducto.ToString();
                    txtNombreproducto.Text = Producto.nombreproducto;
                    txtCompañia.Text = Producto.compañia;
                    txtUsuario.Text = Producto.usuario;
                    txtFecha.Text = Producto.Fecha;
                }
            }
        }
    }
}