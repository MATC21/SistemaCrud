using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaCrud.Models
{
    public class Usuarios
    {
        [PrimaryKey]
        public string cedula { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public int edad { get; set; }
        
    }
}
