using SistemaCrud.Models;
using SistemaCrud.Views.PartialViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SistemaCrud.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListUser : ContentPage
    {
        private ObservableCollection<Usuarios> usuarios;
        public ListUser()
        {
            InitializeComponent();
            LoadUsuarios();
        }
        private async void LoadUsuarios()
        {
            var UserList = await App.DataBaseConnection.getItem();
            usuarios = new ObservableCollection<Usuarios>(UserList);
            UsuariosListView.ItemsSource = usuarios;
        }


        private async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var button = sender as ImageButton;
            var usuario = button?.BindingContext as Usuarios;
            if (usuario != null)
            {
                bool confirm = await DisplayAlert("Confirmar eliminación", "¿Estás seguro de que deseas eliminar esta tarea?", "Sí", "No");
                if (confirm)
                {
                    await App.DataBaseConnection.DeleteItem(usuario);
                    usuarios.Remove(usuario);
                }
            }
        }
        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null; // Desmarca la tarea seleccionada
        }


        private async void Ver_Clicked(object sender, EventArgs e)
        {
            var button = sender as ImageButton;
            var usuarios = button?.BindingContext as Usuarios;
            if (usuarios != null)
            {
                // Navega a una página de edición pasando la tarea seleccionada
                await Navigation.PushAsync(new VerModal(usuarios));
            }
        }

        private async void OnEditButtonClicked(object sender, EventArgs e)
        {
            var button = sender as ImageButton;
            var usuarios = button?.BindingContext as Usuarios;
            if (usuarios != null)
            {
                // Navega a una página de edición pasando la tarea seleccionada
                await Navigation.PushAsync(new EditarModal(usuarios));
            }
        }
    }
}