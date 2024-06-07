using SistemaCrud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SistemaCrud.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AñadirUser : ContentPage
    {
        public AñadirUser()
        {
            InitializeComponent();
        }

        private async void btn_guardar_Clicked(object sender, EventArgs e)
        {
            try
            {
                // Validar que la edad sea un número válido
                if (!int.TryParse(edad.Text, out int edadUsuario))
                {
                    await DisplayAlert("Error", "La edad debe ser un número válido", "Aceptar");
                    return;
                }

                // Verificar si la cédula ya existe en la base de datos
                var existingUser = await App.DataBaseConnection.ObtenerCI(id.Text);
                if (existingUser != 0 )
                {
                    await DisplayAlert("Error", "La cédula ya existe, por favor ingrese otra", "Aceptar");
                    return;
                }

                // Crear el objeto de usuario
                var usuario = new Usuarios
                {
                    cedula = id.Text,
                    nombre = nombre.Text,
                    apellido = apellido.Text,
                    edad = edadUsuario
                };

                // Insertar el usuario en la base de datos
                var result = await App.DataBaseConnection.InsertItem(usuario);

                if (result == 1)
                {
                    await DisplayAlert("Listo", "El usuario se registró correctamente", "Aceptar");
                    id.Text = string.Empty;
                    nombre.Text = string.Empty;
                    apellido.Text = string.Empty;
                    edad.Text = string.Empty;
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Error", "No se pudo guardar el usuario", "Aceptar");
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