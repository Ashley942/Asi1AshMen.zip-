using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MantFormPersonas.Modelos
{
    public class PersonasClass
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; }

        public PersonasClass() { }

        public PersonasClass(int id, string nombre, string apellido,string correo, string telefono, string direccion)
        {
            Id = id;
            Nombre = nombre;
            Apellido = apellido;
            Correo = correo;
            Telefono = telefono;
            Direccion = direccion;

        }
    }
}