using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using SistemaCrud.Models;
using System.ComponentModel;

namespace SistemaCrud.Controllers
{
    public class DataBaseConnection
    {
        public SQLiteAsyncConnection Connection { get; set; }

        public DataBaseConnection(string path)
        {
            Connection = new SQLiteAsyncConnection(path);
            //Creamos una tabla Tarea
            Connection.CreateTableAsync<Usuarios>().Wait();
        }
        public async Task<int> InsertItem(Usuarios item)
        {
            return await Connection.InsertAsync(item);
        }
        public async Task<List<Usuarios>> getItem()
        {
            return await Connection.Table<Usuarios>().ToListAsync();
        }
        public async Task<int> DeleteItem(Usuarios item)
        {
            return await Connection.DeleteAsync(item);
        }
        public async Task<int> EditItem(Usuarios item)
        {
            return await Connection.UpdateAsync(item);
        }

        public async Task<int> ObtenerCI(string id)
        {
            var usuario = await Connection.Table<Usuarios>()
                                       .FirstOrDefaultAsync(x => x.cedula == id);

            if (usuario != null)
            {
                int ci;
                int.TryParse(usuario.cedula, out ci);
                return ci;
            }
            else
            {
                return 0;
            }
            
            
            
        }

    }
}
