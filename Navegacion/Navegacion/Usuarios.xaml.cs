using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Navegacion.Modelos;


namespace Navegacion
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Hoy : ContentPage
    {
        public Hoy()
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
            var listaUsuarios = await App.SQLiteDB.GetAlumnos();
            if (listaUsuarios != null)
            {
                lstUsuarios.ItemsSource = listaUsuarios;
            }
        }

        public async void resultados()
        {
            Empleados emp = new Empleados
            {
                nombre = txtNombre.Text,
                apellidopaterno = txtApellidopaterno.Text,
                apellidomaterno = txtApellidomaterno.Text,
                numero = txtNumero.Text,
                direccion = txtDireccion.Text

            };

            if (string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtApellidopaterno.Text) || string.IsNullOrEmpty(txtApellidomaterno.Text) || string.IsNullOrEmpty(txtNumero.Text) || string.IsNullOrEmpty(txtDireccion.Text))
            {
                await DisplayAlert("Alerta!", "Debe introducir todos los campos", "Aceptar");
            }
            else
            {
                await DisplayAlert("Correcto", "Se a ingresado correctamente", "Aceptar");

                await App.SQLiteDB.GuardarAlumno(emp);
                txtNombre.Text = "";
                txtApellidopaterno.Text = "";
                txtApellidomaterno.Text = "";
                txtNumero.Text = "";
                txtDireccion.Text = "";
                Tabla();

                var listaUsuarios = await App.SQLiteDB.GetAlumnos();
                if (listaUsuarios != null)
                {
                    lstUsuarios.ItemsSource = listaUsuarios;
                }
            }
        }

        private async void btnBorrar_Clicked(object sender, EventArgs e)
        {
            var usuarios = await App.SQLiteDB.GetEmpleadoByIdAsync(Convert.ToInt32(txtid.Text));
            if (usuarios != null)
            {
                await App.SQLiteDB.BorrarEmpleado(usuarios);
                await DisplayAlert("Borrado", "Se a Borrado correctamente", "Aceptar");
                txtid.Text = "";
                txtNombre.Text = "";
                txtApellidopaterno.Text = "";
                txtApellidomaterno.Text = "";
                txtNumero.Text = "";
                txtDireccion.Text = "";
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
                Empleados emp = new Empleados
                {
                    matricula = Convert.ToInt32(txtid.Text),
                    nombre = txtNombre.Text,
                    apellidopaterno = txtApellidopaterno.Text,
                    apellidomaterno = txtApellidomaterno.Text,
                    numero = txtNumero.Text,
                    direccion = txtDireccion.Text

                };

                await App.SQLiteDB.GuardarAlumno(emp);
                await DisplayAlert("Modificacion", "Se a modificado correctamente", "Aceptar");
                txtid.Text = "";
                txtNombre.Text = "";
                txtApellidopaterno.Text = "";
                txtApellidomaterno.Text = "";
                txtNumero.Text = "";
                txtDireccion.Text = "";
                txtid.IsVisible = false;
                BtnModificar.IsVisible = false;
                btnRegistrar.IsVisible = true;
                btnBorrar.IsVisible = false;
                Tabla();
            }
        }

        private async void lstUsuarios_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (Empleados)e.SelectedItem;
            btnRegistrar.IsVisible = false;
            txtid.IsVisible = true;
            BtnModificar.IsVisible = true;
            btnBorrar.IsVisible = true;
            if (!string.IsNullOrEmpty(obj.matricula.ToString()))
            {
                var Usuario = await App.SQLiteDB.GetEmpleadoByIdAsync(obj.matricula);
                if (Usuario != null)
                {
                    txtid.Text = Usuario.matricula.ToString();
                    txtNombre.Text = Usuario.nombre;
                    txtApellidopaterno.Text = Usuario.apellidopaterno;
                    txtApellidomaterno.Text = Usuario.apellidomaterno;
                    txtNumero.Text = Usuario.numero;
                    txtDireccion.Text = Usuario.direccion;
                }
            }
        }
    }
}