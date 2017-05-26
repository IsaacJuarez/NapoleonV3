<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgregarSitio.aspx.cs" Inherits="_3.FUJI.Napoleon.Site.AgregarSitio" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Agregar Sitio</title>
    <link href="../Content/style.css" rel="stylesheet" />
    <link rel="stylesheet" href="../vendors/bootstrap/dist/css/bootstrap.min.css"/>
    <script src="../vendors/jquery/dist/jquery.min.js"></script>
    <script type="text/javascript">
        function ShowMessage(message, messagetype, idControl) {
            var cssclass;
            switch (messagetype) {
                case 'Success':
                    cssclass = 'alert-success'
                    break;
                case 'Error':
                    cssclass = 'alert-danger'
                    break;
                case 'Warning':
                    cssclass = 'alert-warning'
                    break;
                default:
                    cssclass = 'alert-info'
            }
            var control = "#" + idControl;
            $(control).append('<div id="' + idControl + '" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert fade in ' + cssclass + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>' + messagetype + '!</strong> <span>' + message + '</span></div>');
            $(control).fadeTo(2000, 700).slideUp(700, function () {
                $(control).slideUp(700);
            });
        }
        function validarNum(e) {
            tecla = (document.all) ? e.keyCode : e.which;
            if (tecla == 8) return true;
            patron = /\d/;
            te = String.fromCharCode(tecla);
            return patron.test(te);
        }
        function quitaNoNumero(obj) {
            patron = /\d/;
            if (!patron.test(obj.value)) {
                obj.value = "";
            }
        }
   </script>
</head>
<body >
    <div class="bg-bubblesSit">
        
        <div class="wrapper">
            <center>
                <div class="containerSit">
                    <div class="messagealert" id="alert_container"></div>
                    <h1>Agregar Sitio</h1>
                    <div class="col-md-12 col-sm-12 col-xs-12">
                        <form id="form1" runat="server">
                            <div class="row">
                                <div class="col-md-5  col-sm-5 col-xs-5 text-left">
                                    <asp:Label runat="server" ID="lblNombreSitio" Text="Nombre del Sitio"></asp:Label>
                                    <asp:RequiredFieldValidator runat="server" ID="rfvNombreSitio" Text="*" ForeColor="Red" ControlToValidate="txtNombreSitio" ValidationGroup="vgAgregarSitio"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-7  col-sm-7 col-xs-7">
                                    <asp:TextBox runat="server" ID="txtNombreSitio" onkeyup="this.value=this.value.toUpperCase()" CssClass="form-control upper-case" Width="100%"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-5  col-sm-5 col-xs-5 text-left">
                                    <asp:Label runat="server" ID="lblClaveSito" Text="Clave del Sitio"></asp:Label>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" Text="*" ForeColor="Red" ControlToValidate="txtClaveSitio" ValidationGroup="vgAgregarSitio"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-7  col-sm-7 col-xs-7">
                                    <asp:TextBox runat="server" ID="txtClaveSitio" onkeyup="this.value=this.value.toUpperCase()" CssClass="form-control upper-case" MaxLength="5"  Width="100%"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-5  col-sm-5 col-xs-5 text-left">
                                    <asp:Label runat="server" ID="lblNombreCliente" Text="Nombre de Contacto"></asp:Label>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" Text="*" ForeColor="Red" ControlToValidate="txtNombreContacto" ValidationGroup="vgAgregarSitio"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-7  col-sm-7 col-xs-7">
                                    <asp:TextBox runat="server" ID="txtNombreContacto" onkeyup="this.value=this.value.toUpperCase()" CssClass="form-control upper-case" Width="100%"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-5  col-sm-5 col-xs-5 text-left">
                                    <asp:Label runat="server" ID="lblEmail" Text="Email"></asp:Label>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" Text="*" ForeColor="Red" ControlToValidate="txtEmail" ValidationGroup="vgAgregarSitio"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Text="* Formato Email incorrecto" ValidationGroup="vgAgregarSitio" ForeColor="red"
                                                ErrorMessage="Invalid Email" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                </div>
                                <div class="col-md-7  col-sm-7 col-xs-7">
                                    <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" Width="100%"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-5  col-sm-5 col-xs-5 text-left">
                                    <asp:Label runat="server" ID="lblNumContacto" Text="Numero de Contacto"></asp:Label>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" Text="*" ForeColor="Red" ControlToValidate="txtNumContacto" ValidationGroup="vgAgregarSitio"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-7  col-sm-7 col-xs-7 text-left">
                                    <asp:TextBox runat="server" ID="txtNumContacto" onkeypress="return validarNum(event)" onchange="quitaNoNumero(this)" CssClass="form-control" MaxLength="12" Width="100%"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-5  col-sm-5 col-xs-5  text-left">
                                    <asp:Label runat="server" ID="lblVendedor" Text="Vendedor"></asp:Label>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" Text="*" ForeColor="Red" ControlToValidate="txtVendedor" ValidationGroup="vgAgregarSitio"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-7  col-sm-7 col-xs-7">
                                    <asp:TextBox runat="server" ID="txtVendedor" onkeyup="this.value=this.value.toUpperCase()" CssClass="form-control upper-case" Width="100%"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12 col-sm-12 col-xs-12">
                                    <asp:Button runat="server" ID="btnCrearSitio" Width="50%" Text="Crear Sitio" OnClick="btnCrearSitio_Click" CssClass="form-control" ValidationGroup="vgAgregarSitio"/>
                                </div>
                            </div>
                        </form>
                    </div>
                    <!-- footer content -->
                    <footer>
                        <div class="pull-center">
                            <p><%: DateTime.Now.Year %> - FEED2CLOUD - Versión 1.0    </p>
                        </div>
                    </footer>
                    <!-- /footer content -->                    
                </div>
            </center>
        </div>
    </div>
    <script src='http://cdnjs.cloudflare.com/ajax/libs/jquery/2.1.3/jquery.min.js'></script>    
</body>
</html>

