<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonasForm.aspx.cs" Inherits="MantFormPersonas.Vistas.PersonasForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Mantenimiento de Personas</title>
<link href="../Estilos/PersonaStyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
 <div class="container">
            <h2 id="titulo">Mantenimiento de Personas</h2>
            <asp:Label ID="lblMensajeError" runat="server" CssClass="texto-error"></asp:Label> <br />  <br />
            
            <asp:GridView ID="gvPersonas" runat="server" AutoGenerateColumns="False" CssClass="gridview-persona"
                OnRowCommand="gvPersonas_RowCommand" OnRowDeleting="gvPersonas_RowDeleting"
                DataKeyNames="Id">
                
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="ID" ReadOnly="True" />
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
                    <asp:BoundField DataField="Correo" HeaderText="Correo" />
                    <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
                    <asp:BoundField DataField="Direccion" HeaderText="Dirección" />
                    <asp:BoundField DataField="FechaCreacion" HeaderText="Fecha Creación" />
                    <asp:BoundField DataField="UsuarioCreacion" HeaderText="Usuario Creación" />
                    <asp:BoundField DataField="FechaModificacion" HeaderText="Fecha Modificación" />
                    <asp:BoundField DataField="UsuarioModificacion" HeaderText="Usuario Modificación" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnSelect" runat="server" CommandName="Select" 
                                CommandArgument='<%# Eval("Id") %>' Text="Editar" CssClass="btn-editar"   CausesValidation="false" />
                            <asp:Button ID="btnDelete" runat="server" CommandName="Delete" 
                                CommandArgument='<%# Eval("Id") %>' Text="Eliminar" CssClass="btn-eliminar"  CausesValidation="false"  
                                OnClientClick="return confirm('¿Está seguro que desea eliminar este registro?');"  /><!-- Mensaje antes de eliminar -->
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <h3>Agregar/Editar Persona</h3>
              <!-- Indicaciones de los campos obligatorios -->
            <asp:ValidationSummary 
                ID="ValidationSummary1" runat="server" ForeColor="Red"
                HeaderText="Por favor corrija los siguientes errores:"   DisplayMode="BulletList">
            </asp:ValidationSummary>

            <asp:Label ID="lblId" runat="server" Visible="False"></asp:Label>

            <asp:Label ID="lblNombre" runat="server" Text="Nombre:"></asp:Label>
            <asp:TextBox ID="txtNombre" runat="server" CssClass="input-field"></asp:TextBox>
            <!-- Validación de obligatorio -->
            <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txtNombre"
                ErrorMessage="El nombre es obligatorio" ForeColor="Red" Display="Dynamic" /> <br />

            <asp:Label ID="lblApellido" runat="server" Text="Apellido:"></asp:Label>
            <asp:TextBox ID="txtApellido" runat="server" CssClass="input-field"></asp:TextBox>
            <!-- Validación de obligatorio -->
            <asp:RequiredFieldValidator ID="rfvApellido" runat="server" ControlToValidate="txtApellido" 
                ErrorMessage="El apellido es obligatorio" ForeColor="Red" Display="Dynamic" /> <br />
          
            <asp:Label ID="lblCorreo" runat="server" Text="Correo:"></asp:Label>
            <asp:TextBox ID="txtCorreo" runat="server" CssClass="input-field"></asp:TextBox>
            <!-- Validaciones de obligatorio y formato -->
            <asp:RequiredFieldValidator
                ID="rfvCorreo" runat="server" ControlToValidate="txtCorreo"
                ErrorMessage="El correo es obligatorio" ForeColor="Red" Display="Dynamic" />
            <asp:RegularExpressionValidator
                ID="revCorreo" runat="server" ControlToValidate="txtCorreo" ErrorMessage="Formato de correo inválido"
                ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$" ForeColor="Red" Display="Dynamic" />
            <br />

            <asp:Label ID="lblTelefono" runat="server" Text="Teléfono:"></asp:Label>
            <asp:TextBox ID="txtTelefono" runat="server" CssClass="input-field"></asp:TextBox>
            <!-- Validaciones de obligatorio y formato -->
            <asp:RequiredFieldValidator
                ID="rfvTelefono" runat="server" ControlToValidate="txtTelefono"
                ErrorMessage="El teléfono es obligatorio" ForeColor="Red" Display="Dynamic" />
            <asp:RegularExpressionValidator
                ID="revTelefono" runat="server" ControlToValidate="txtTelefono"
                ErrorMessage="Solo números permitidos" ValidationExpression="^\d+$" ForeColor="Red" Display="Dynamic" />
            <br />

            <asp:Label ID="lblDireccion" runat="server" Text="Dirección:"></asp:Label>
            <asp:TextBox ID="txtDireccion" runat="server" CssClass="input-field"></asp:TextBox>
            <!-- Validación de obligatorio -->
            <asp:RequiredFieldValidator
                ID="rfvDireccion" runat="server" ControlToValidate="txtDireccion"
                ErrorMessage="La dirección es obligatoria" ForeColor="Red" Display="Dynamic" />
            <br />

            <asp:Label ID="lblMensajeExito" runat="server" CssClass="texto-exito"></asp:Label><br /><br />
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" CssClass="btn-agregar" />
            <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click" CssClass="btn-limpiar"  CausesValidation="false" />
        </div>
    </form>
</body>
</html>
