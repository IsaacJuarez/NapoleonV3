<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmAgregarSitio.aspx.cs" Inherits="_3.FUJI.Napoleon.Site.frmAgregarSitio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="vendors/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet"/>
    <script src="vendors/jquery/dist/jquery.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-3 col-sm-3 col-xs-3">
        </div>
        <div class="col-md-6 col-sm-6 col-xs-6">
            <div class="x_panel">
                <div class="x_title">
                    <div class="messagealert" id="alert_container">
                    </div>
                    <h2>Agregar Sitios </h2>
                    <ul class="nav navbar-right panel_toolbox">
                      <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                      </li>
                    </ul>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">
                    <p class="text-muted font-13 m-b-30">
                      * Se debe notificar al área de tecnología para crear la configuración del sitio.
                    </p>
                    <div class="row">
                        <div class="col-lg-2 col-mg-2 col-sm-2">
                        </div>
                        <div class="col-lg-12 col-mg-12 col-sm-12">
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
                                    <asp:TextBox runat="server" ID="txtClaveSitio" onkeyup="this.value=this.value.toUpperCase()" onkeypress="return quitaEspacio(this)" CssClass="form-control upper-case" MaxLength="5"  Width="100%"></asp:TextBox>
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
                                    <asp:Label runat="server" ID="lblNumContacto" Text="Teléfono"></asp:Label>
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
                                <br />
                            </div>
                            <div class="row text-right">
                                <div class="col-md-12 col-sm-12 col-xs-12">
                                    <asp:Button runat="server" ID="btnLimpiar" Text="Limpiar" OnClick="btnLimpiar_Click" CssClass="btn btn-danger" />
                                    <asp:Button runat="server" ID="btnCrearSitio" Text="Crear Sitio" OnClick="btnCrearSitio_Click" CssClass="btn btn-success" ValidationGroup="vgAgregarSitio"/>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2 col-mg-2 col-sm-2">
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-3 col-sm-3 col-xs-3">
        </div>
    </div>



    <script type="text/javascript">
        function ShowMessage(message, messagetype, idControl) {
            var cssclass;
            switch (messagetype) {
                case 'Correcto':
                    cssclass = 'alert-success'
                    break;
                case 'Error':
                    cssclass = 'alert-danger'
                    break;
                case 'Advertencia':
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
        function quitaEspacio(v) {
            v.value = v.value.replace(/^\s+/i, '').replace(/\s+$/i, '');
        }
   </script>
</asp:Content>
