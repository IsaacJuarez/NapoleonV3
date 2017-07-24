<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmUpdateUser.aspx.cs" Inherits="_3.FUJI.Napoleon.Site.frmUpdateUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="vendors/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet"/>
    <script src="vendors/jquery/dist/jquery.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <center>
        <div class="row">
            <div class="messagealert" id="alert_container">
            </div>
            <div class="col-md-3 col-sm-3 col-xs-3">
            </div>
            <div class="col-md-6 col-sm-6 col-xs-6">
                <div class="x_panel">
                    <div class="x_title">
                        <h2>Cambio de Contraseña</h2>
                        <ul class="nav navbar-right panel_toolbox">
                          <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                          </li>
                        </ul>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content">
                        <p class="text-muted font-13 m-b-30">
                          * El usuario puede actualizar la contraseña de inicio de sesión.
                        </p>
                        <div class="row">
                            <div class="col-lg-12 col-mg-12 col-sm-12">
                                <div class="row">
                                    <div id="demo-form2"  class="form-horizontal form-label-left input_mask">
                                      <div class="col-md-6 col-sm-6 col-xs-12 form-group has-feedback">
                                            <asp:TextBox type="text" runat="server" CssClass="form-control has-feedback-left" ID="txtNombre" placeholder="Nombre" style="height: 34px;"></asp:TextBox>
                                            <span class="fa fa-user form-control-feedback left" aria-hidden="true"></span>
                                            <asp:RequiredFieldValidator ID="rfvNombre" runat="server" Text="* Capturar un nombre." ForeColor="Red" ControlToValidate="txtNombre" ValidationGroup="vgUserEdit"></asp:RequiredFieldValidator>
                                      </div>

                                      <div class="col-md-6 col-sm-6 col-xs-12 form-group has-feedback">
                                            <asp:TextBox type="text" runat="server" CssClass="form-control" ID="txtApellido" placeholder="Apellidos"  style="height: 34px;"></asp:TextBox>
                                            <span class="fa fa-user form-control-feedback right" aria-hidden="true"></span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Text="* Capturar Apellidos." ForeColor="Red" ControlToValidate="txtApellido" ValidationGroup="vgUserEdit"></asp:RequiredFieldValidator>
                                      </div>

                                      <div class="col-md-6 col-sm-6 col-xs-12 form-group has-feedback">
                                            <asp:TextBox  type="text" runat="server" CssClass="form-control has-feedback-left" ID="txtEmail" placeholder="Email"  style="height: 34px;"></asp:TextBox>
                                            <span class="fa fa-envelope form-control-feedback left" aria-hidden="true"></span>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" Text="* Capturar correo electrónico de la cuenta" ForeColor="Red" ControlToValidate="txtEmail" ValidationGroup="vgUserEdit"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Text="* Formato Email incorrecto" ValidationGroup="vgUserEdit" ForeColor="red"
                                                ErrorMessage="Invalid Email" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                      </div>
                                      <div class="col-md-6 col-sm-6 col-xs-12 form-group has-feedback">
                                            <asp:CheckBox ID="btnChangePass" runat="server" AutoPostBack="true" Text="Cambiar contraseña" Checked="false" OnCheckedChanged="btnChangePass_CheckedChanged" />
                                      </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div id="demo-form3"  class="form-horizontal form-label-left input_mask">
                                          <div class="col-md-6 col-sm-6 col-xs-12 form-group has-feedback">
                                                <asp:TextBox  type="text" TextMode="Password" runat="server" Enabled="false" CssClass="form-control has-feedback-left" ID="txtPass" placeholder="Constraseña"  style="height: 34px;"></asp:TextBox>
                                                <span class="fa fa-key form-control-feedback left" aria-hidden="true"></span>
                                                <asp:RequiredFieldValidator ID="RequiredcFieldValidator2" runat="server" Enabled="false" Text="* Capturar contraseña." ForeColor="Red"  ControlToValidate="txtPass" ValidationGroup="vgUserEdit"></asp:RequiredFieldValidator>
                                          </div>

                                          <div class="col-md-6 col-sm-6 col-xs-12 form-group has-feedback">
                                                <asp:TextBox  type="text" TextMode="Password" runat="server" Enabled="false" CssClass="form-control" ID="txtRePass" placeholder="Confirmar Contraseña"  style="height: 34px;"></asp:TextBox>
                                                <span class="fa fa-key form-control-feedback right" aria-hidden="true"></span>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Enabled="false" Text="* Confirmar contraseña." ForeColor="Red" ControlToValidate="txtRePass" ValidationGroup="vgUserEdit"></asp:RequiredFieldValidator>
                                          </div>
                                        <div class="form-group">
                                            <div class="col-md-9 col-sm-9 col-xs-12 col-md-offset-3">
                                              <asp:Button type="submit" runat="server" ID="btnGuardar" CssClass="btn btn-success" OnClick="btnGuardar_Click" Text="Guardar" ValidationGroup="vgUserEdit"></asp:Button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                   </div>
                </div>
            </div>
            <div class="col-md-3 col-sm-3 col-xs-3">
            </div>  
        </div>
    </center>

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

        function openPopover() {
            try {
                $('[data-toggle="popover"]').popover();
            }
            catch (ew) {
            }
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

        $(document).ready(function () {
            $('[data-toggle="popover"]').popover();
        });

    </script>
</asp:Content>
