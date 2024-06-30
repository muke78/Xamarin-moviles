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
    public partial class Usuarios : ContentPage
    {
        public Usuarios()
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
            var listaArticulos = await App.SQLiteDB.GetAlumnos();
            if (listaArticulos != null)
            {
                lstAlumnos.ItemsSource = listaArticulos;
            }
        }

        public async void resultados()
        {
            Alumnos alum = new Alumnos
            {
                nombre = txtNombre.Text,
                apellidoPaterno = txtApellidoPaterno.Text,
                apellidoMaterno = txtApellidoMaterno.Text,
                correo = txtCorreo.Text
            };

            if (string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtCorreo.Text) || string.IsNullOrEmpty(txtApellidoPaterno.Text) || string.IsNullOrEmpty(txtApellidoMaterno.Text))
            {
                await DisplayAlert("Alerta!", "Debe introducir todos los campos", "Aceptar");
            }
            else
            {
                await DisplayAlert("Correcto", "Se a ingresado correctamente", "Aceptar");

                await App.SQLiteDB.GuardarAlumno(alum);
                txtNombre.Text = "";
                txtApellidoPaterno.Text = "";
                txtApellidoMaterno.Text = "";
                txtCorreo.Text = "";
                Tabla();
            }
        }

        private  async void btnBorrar_Clicked(object sender, EventArgs e)
        {
            var alumno = await App.SQLiteDB.GetAlumnoByIdAsync(Convert.ToInt32(txtmatricula.Text));
            if (alumno != null)
            {
                await App.SQLiteDB.BorrarAlumno(alumno);
                await DisplayAlert("Borrado", "Se a Borrado correctamente", "Aceptar");
                txtmatricula.Text = "";
                txtNombre.Text = "";
                txtApellidoPaterno.Text = "";
                txtApellidoMaterno.Text = "";
                txtCorreo.Text = "";
                txtmatricula.IsVisible = false;
                BtnModificar.IsVisible = false;
                btnRegistrar.IsVisible = true;
                btnBorrar.IsVisible = false;                
                Tabla();
            }                
            
        }

        private async void BtnModificar_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtmatricula.Text))
            {
                Alumnos alum = new Alumnos
                {
                    matricula = Convert.ToInt32(txtmatricula.Text),
                    nombre = txtNombre.Text,
                    apellidoPaterno = txtApellidoPaterno.Text,
                    apellidoMaterno = txtApellidoMaterno.Text,
                    correo = txtCorreo.Text
                };

                await App.SQLiteDB.GuardarAlumno(alum);
                await DisplayAlert("Modificacion", "Se a modificado correctamente", "Aceptar");
                txtmatricula.Text = "";
                txtNombre.Text = "";
                txtApellidoPaterno.Text = "";
                txtApellidoMaterno.Text = "";
                txtCorreo.Text = "";
                txtmatricula.IsVisible = false;
                BtnModificar.IsVisible = false;
                btnRegistrar.IsVisible = true;
                btnBorrar.IsVisible = false;                
                Tabla();
            }
        }

        private async void lstAlumnos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (Alumnos)e.SelectedItem;
            btnRegistrar.IsVisible = false;
            txtmatricula.IsVisible = true;
            BtnModificar.IsVisible = true;
            btnBorrar.IsVisible = true;
            if (!string.IsNullOrEmpty(obj.matricula.ToString()))
            {
                var alumno = await App.SQLiteDB.GetAlumnoByIdAsync(obj.matricula);
                if(alumno != null)
                {
                    txtmatricula.Text = alumno.matricula.ToString();
                    txtNombre.Text = alumno.nombre;
                    txtApellidoPaterno.Text = alumno.apellidoPaterno;
                    txtApellidoMaterno.Text = alumno.apellidoMaterno;
                    txtCorreo.Text = alumno.correo;
                }
            }
        }
    }
}