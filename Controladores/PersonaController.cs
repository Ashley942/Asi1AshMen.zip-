using MantFormPersonas.Modelos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MantFormPersonas.Controladores
{
    public class PersonaController
    {
        private string connectionString;

        public PersonaController()
        {
            connectionString = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
        }

        public List<PersonasClass> ObtenerPersonas()
        {
            List<PersonasClass> personas = new List<PersonasClass>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_ObtenerPersonas", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    personas.Add(new PersonasClass
                    {
                        Id = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Apellido = reader.GetString(2),
                        Correo = reader.GetString(3),
                        Telefono = reader.GetString(4),
                        Direccion = reader.GetString(5),
                        FechaCreacion = reader.GetDateTime(6),
                        UsuarioCreacion = reader.GetString(7),
                        FechaModificacion = reader.IsDBNull(8) ? (DateTime?)null : reader.GetDateTime(8),
                        UsuarioModificacion = reader.IsDBNull(9) ? null : reader.GetString(9)
                    });
                }
            }

            return personas;
        }
        // Método para agregar una nueva persona al archivo
        public void AgregarPersonas(string nombre,string apellido ,string correo, string telefono, string direccion, string usuarioCreacion)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_AgregarPersonas", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nombre", nombre);
                cmd.Parameters.AddWithValue("@Apellido", apellido);
                cmd.Parameters.AddWithValue("@Correo", correo);
                cmd.Parameters.AddWithValue("@Telefono", telefono);
                cmd.Parameters.AddWithValue("@Direccion", direccion);
                cmd.Parameters.AddWithValue("@UsuarioCreacion", usuarioCreacion);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public PersonasClass ObtenerPersonasPorId(int id)
        {
            PersonasClass persona = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_ObtenerPersonasPorId", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    persona = new PersonasClass
                    {
                        Id = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Apellido = reader.GetString(2),
                        Correo = reader.GetString(3),
                        Telefono = reader.GetString(4),
                        Direccion = reader.GetString(5),
                        FechaCreacion = reader.GetDateTime(6),
                        UsuarioCreacion = reader.GetString(7),
                        FechaModificacion = reader.IsDBNull(8) ? (DateTime?)null : reader.GetDateTime(8),
                        UsuarioModificacion = reader.IsDBNull(9) ? null : reader.GetString(9)
                    };
                }
            }

            return persona;
        }
        // Método para actualizar una persona existente
        public void ActualizarPersonas(int id, string nombre, string apellido, string correo, string telefono, string direccion, string usuarioModificacion)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_ActualizarPersonas", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Nombre", nombre);
                cmd.Parameters.AddWithValue("@Apellido", apellido);
                cmd.Parameters.AddWithValue("@Correo", correo);
                cmd.Parameters.AddWithValue("@Telefono", telefono);
                cmd.Parameters.AddWithValue("@Direccion", direccion);
                cmd.Parameters.AddWithValue("@UsuarioModificacion", usuarioModificacion);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        // Método para eliminar una persona por ID
        public void EliminarPersona(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_EliminarPersona", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}