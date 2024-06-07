using SistemaCrud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SistemaCrud.Views.PartialViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VerModal : ContentPage
    {
        private Usuarios usuarios;
        public VerModal(Models.Usuarios usuarios)
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
    }
}