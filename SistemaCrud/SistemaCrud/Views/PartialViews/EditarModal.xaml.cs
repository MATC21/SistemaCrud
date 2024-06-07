using SistemaCrud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SistemaCrud.Views.PartialViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditarModal : ContentPage
    {
        private Usuarios usuarios;
        public EditarModal(Models.Usuarios usuarios)
        {
            InitializeComponent();
            this.usuarios = usuarios;
            CargarDatosUsuario();
        }

        private void CargarDatosUsuario()
        {
            id.Text = usuarios.cedula;
            nombre.Text = usuarios.nombre;
            apellido.Text = usuarios.apellido;
            edad.Text = usuarios.edad.ToString();
        
        }

        private async void btn_Actualizar_Clicked(object sender, EventArgs e)
        {
            try
            {
                int edadUsuario;
                if (!int.TryParse(edad.Text, out edadUsuario))
                {
                    await DisplayAlert("Error", "La edad debe ser un número válido", "Aceptar");
                    return;
                }

                var usuario = new Usuarios
                {
                    cedula = id.Text,
                    nombre = nombre.Text,
                    apellido = apellido.Text,
                    edad = edadUsuario
                };

                var result = await App.DataBaseConnection.EditItem(usuario);

                if (result == 1)
                {
                    await DisplayAlert("Listo", "El usuario se actualizo correctamente", "Aceptar");
                    nombre.Text = string.Empty;
                    apellido.Text = string.Empty;
                    edad.Text = string.Empty;
                    await Navigation.PushAsync(new ListUser());
                }
                else
                {
                    await DisplayAlert("Error", "No se pudo actualizar el usuario", "Aceptar");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Aceptar");
            }
        }
        private void edad_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Verifica si la entrada está vacía
            if (string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                return;
            }

            // Verifica si la entrada es un número válido
            if (int.TryParse(e.NewTextValue, out int edadValue))
            {
                // Si el número no está en el rango de 1 a 100, restaura el valor anterior
                if (edadValue < 1 || edadValue > 100)
                {
                    ((Entry)sender).Text = e.OldTextValue;
                }
            }
            else
            {
                // Si la entrada no es un número válido, restaura el valor anterior
                ((Entry)sender).Text = e.OldTextValue;
            }
        }
    }
}