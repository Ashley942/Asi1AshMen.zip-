using MantFormPersonas.Controladores;
using MantFormPersonas.Modelos;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MantFormPersonas.Vistas
{
    public partial class PersonasForm : System.Web.UI.Page
    {
        //Se agregó para el manejo de archivos planos nuevo controlador.  
        private PersonaController personaController = new PersonaController();
        private PersonaArchivoController archivoController = new PersonaArchivoController();

        protected void Page_Load(object sender, EventArgs e)
        {
            UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;

            if (!IsPostBack)
            {
                CargarPersonas();
            }
        }

        private void CargarPersonas()
        {
            List<PersonasClass> personas = personaController.ObtenerPersonas();
            gvPersonas.DataSource = personas;
            gvPersonas.DataBind();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            lblId.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtCorreo.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtDireccion.Text = string.Empty;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {    
            //Para ejecutar las validaciones
            if (!Page.IsValid)
                return;
            try
            {
                string usuarioActual = "amenap";

                PersonasClass persona = new PersonasClass
                {
                    Nombre = txtNombre.Text.Trim(),
                    Apellido = txtApellido.Text.Trim(),
                    Correo = txtCorreo.Text.Trim(),
                    Telefono = txtTelefono.Text.Trim(),
                    Direccion = txtDireccion.Text.Trim(),
                    UsuarioCreacion = usuarioActual,
                    UsuarioModificacion = usuarioActual
                };

                if (string.IsNullOrEmpty(lblId.Text))
                {
                    // SQL
                    personaController.AgregarPersonas(
                        persona.Nombre,
                        persona.Apellido,
                        persona.Correo,
                        persona.Telefono,
                        persona.Direccion,
                        usuarioActual);

                    // CSV
                    archivoController.AgregarPersona(persona);

                    lblMensajeExito.Text = "Guardado exitosamente!";
                }
                else
                {
                    persona.Id = int.Parse(lblId.Text);

                    // SQL
                    personaController.ActualizarPersonas(
                        persona.Id,
                        persona.Nombre,
                        persona.Apellido,
                        persona.Correo,
                        persona.Telefono,
                        persona.Direccion,
                        usuarioActual);

                    // CSV
                    archivoController.ActualizarPersona(persona);

                    lblMensajeExito.Text = "Actualizado exitosamente!";
                }

                LimpiarCampos();
                CargarPersonas();
            }
            catch (Exception ex)
            {
                lblMensajeError.Text = "Error: " + ex.Message;
            }
        }

        protected void gvPersonas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                try
                {
                    int id = Convert.ToInt32(e.CommandArgument);
                    PersonasClass persona = personaController.ObtenerPersonasPorId(id);

                    if (persona != null)
                    {
                        lblId.Text = persona.Id.ToString();
                        txtNombre.Text = persona.Nombre;
                        txtApellido.Text = persona.Apellido;
                        txtCorreo.Text = persona.Correo;
                        txtTelefono.Text = persona.Telefono;
                        txtDireccion.Text = persona.Direccion;
                    }
                }
                catch (FormatException ex)
                {
                    lblMensajeError.Text = "Error: " + ex.Message;
                }
            }
        }

        protected void gvPersonas_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(gvPersonas.DataKeys[e.RowIndex].Value);

                // SQL
                personaController.EliminarPersona(id);

                // CSV
                archivoController.EliminarPersona(id);

                lblMensajeExito.Text = "Eliminado exitosamente!";
                CargarPersonas();
            }
            catch (Exception ex)
            {
                lblMensajeError.Text = "Error al eliminar la persona: " + ex.Message;
            }
        }

        protected void gvPersonas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
        }
    }
}