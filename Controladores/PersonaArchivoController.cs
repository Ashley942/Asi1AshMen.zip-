using MantFormPersonas.Modelos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

// Método para obtener la lista de personas desde el archivo CSV

namespace MantFormPersonas.Controladores
{
    public class PersonaArchivoController
    {
        private string ruta;

        public PersonaArchivoController()
        {
            ruta = HttpContext.Current.Server.MapPath("~/App_Data/personas.csv");

            // Crear el archivo en caso que no exista con todas las columnas
            if (!File.Exists(ruta))
            {
                File.WriteAllText(ruta,
                    "Id,Nombre,Apellido,Correo,Telefono,Direccion,FechaCreacion,UsuarioCreacion,FechaModificacion,UsuarioModificacion");
            }
        }

        public List<PersonasClass> ObtenerPersonas()
        {
            //Manejo de Excepciones
            try
            {
                List<PersonasClass> lista = new List<PersonasClass>();
                var lineas = File.ReadAllLines(ruta);

                for (int i = 1; i < lineas.Length; i++)
                {
                    if (string.IsNullOrWhiteSpace(lineas[i]))
                        continue;

                    var datos = lineas[i].Split(',');

                    if (datos.Length < 10)
                        continue;

                    int id;
                    int.TryParse(datos[0], out id);

                    DateTime fechaCreacion;
                    DateTime.TryParse(datos[6], out fechaCreacion);

                    DateTime fechaModificacion;
                    DateTime? fechaMod = null;
                    if (DateTime.TryParse(datos[8], out fechaModificacion))
                        fechaMod = fechaModificacion;

                    lista.Add(new PersonasClass
                    {
                        Id = id,
                        Nombre = datos[1],
                        Apellido = datos[2],
                        Correo = datos[3],
                        Telefono = datos[4],
                        Direccion = datos[5],
                        FechaCreacion = fechaCreacion,
                        UsuarioCreacion = datos[7],
                        FechaModificacion = fechaMod,
                        UsuarioModificacion = string.IsNullOrEmpty(datos[9]) ? null : datos[9]
                    });
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al leer el archivo de personas: " + ex.Message);
            }

        }
        // Método para agregar una nueva persona al archivo
        public void AgregarPersona(PersonasClass persona)
        {
            //Manejo de excepciones
            try
            {
                var lista = ObtenerPersonas();
                int nuevoId = lista.Count > 0 ? lista.Max(p => p.Id) + 1 : 1;
                persona.Id = nuevoId;
                persona.FechaCreacion = DateTime.Now;

                string nuevaLinea =
                    $"{persona.Id}," +
                    $"{persona.Nombre}," +
                    $"{persona.Apellido}," +
                    $"{persona.Correo}," +
                    $"{persona.Telefono}," +
                    $"{persona.Direccion}," +
                    $"{persona.FechaCreacion}," +
                    $"{persona.UsuarioCreacion},,";

                File.AppendAllText(ruta, Environment.NewLine + nuevaLinea);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar la persona: " + ex.Message);
            }
        }
        // Método para actualizar una persona existente
        public void ActualizarPersona(PersonasClass persona)
        {
            var lista = ObtenerPersonas();

            var personaExistente = lista.FirstOrDefault(p => p.Id == persona.Id);

            if (personaExistente != null)
            {
                personaExistente.Nombre = persona.Nombre;
                personaExistente.Apellido = persona.Apellido;
                personaExistente.Correo = persona.Correo;
                personaExistente.Telefono = persona.Telefono;
                personaExistente.Direccion = persona.Direccion;
                personaExistente.FechaModificacion = DateTime.Now;
                personaExistente.UsuarioModificacion = persona.UsuarioModificacion;
            }

            GuardarArchivoCompleto(lista);
        }
        // Método para eliminar una persona por ID
        public void EliminarPersona(int id)
        {
            var lista = ObtenerPersonas();
            lista.RemoveAll(p => p.Id == id);

            GuardarArchivoCompleto(lista);
        }

        private void GuardarArchivoCompleto(List<PersonasClass> lista)
        {
            //Manejo de excepciones
            try
            {
                List<string> lineas = new List<string>();

                lineas.Add("Id,Nombre,Apellido,Correo,Telefono,Direccion,FechaCreacion,UsuarioCreacion,FechaModificacion,UsuarioModificacion");

                foreach (var p in lista)
                {
                    lineas.Add(
                        $"{p.Id}," +
                        $"{p.Nombre}," +
                        $"{p.Apellido}," +
                        $"{p.Correo}," +
                        $"{p.Telefono}," +
                        $"{p.Direccion}," +
                        $"{p.FechaCreacion}," +
                        $"{p.UsuarioCreacion}," +
                        $"{p.FechaModificacion}," +
                        $"{p.UsuarioModificacion}");
                }

                File.WriteAllLines(ruta, lineas);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar el archivo: " + ex.Message);
            }
        }
    }
}