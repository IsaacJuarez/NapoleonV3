<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmLogin.aspx.cs" Inherits="_3.FUJI.Napoleon.Site.frmLogin" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>LOG IN FEED2CLOUD</title>
    <link rel="stylesheet" href="Content/style.css"/>
    <link rel="stylesheet" href="vendors/bootstrap/dist/css/bootstrap.min.css"/>
    <script src="vendors/jquery/dist/jquery.min.js"></script>
    <script src="vendors/bootstrap/dist/js/bootstrap.min.js"></script>
    <style type="text/css">
        .imagen{
            -webkit-background-size: cover;
           -moz-background-size: cover;
           -o-background-size: cover;
           background-size: cover;
           height: 80%;
           width: 80% ;
           text-align: center;
        }
        .imagenFuji{
            -webkit-background-size: cover;
           -moz-background-size: cover;
           -o-background-size: cover;
           background-size: cover;
           height: 50%;
           width: 50% ;
           text-align: center;
        }
        .btn-info {
          color: #fff;
          background-color: #5bc0de;
          border-color: #46b8da;
        }
    </style>
</head>
<body>
    <div class="bg-bubbles">
        <div class="wrapper">
            <center>
                <div class="container">
                    <h1>Bienvenido</h1>
                    <div class="col-md-12 col-sm-12 col-xs-12">
                        <img src="Images/HorseNapoleon.png" class="imagen"/>
                        <form id="form" runat="server" >
                            <asp:ScriptManager runat="server" ></asp:ScriptManager>
                            <div>
                                <asp:TextBox ID="txtUsuarioUser" placeholder="Usuario" runat="server" ValidationGroup="vgLogin" class="form" ></asp:TextBox>
		                        <asp:TextBox ID="txtContrasenaUser" TextMode="Password" placeholder="Contraseña" runat="server" ValidationGroup="vgLogin" class="form"></asp:TextBox>
                                <asp:Button type="submit" id="btnLogin" runat="server" Text="Entrar" OnClick="btnLogin_Click" ValidationGroup="vgLogin" class="form"></asp:Button>
                                <asp:Button runat="server" ID="btnRecPass" Text="Recuperar Contraseña" CssClass="btn btn-primary" OnClick="btnRecPass_Click" />
                                <div>
                                    <asp:RequiredFieldValidator ErrorMessage="Usuario requerido." ForeColor="#61210B" Font-Bold="true" runat="server" ID="rfvUser" ControlToValidate="txtUsuarioUser" ValidationGroup="vgLogin"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ErrorMessage="Contraseña requerida" ForeColor="#61210B" Font-Bold="true" runat="server" ID="rfvPass" ControlToValidate="txtContrasenaUser" ValidationGroup="vgLogin"></asp:RequiredFieldValidator>
                                </div>
                                <br />
                                <div>
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:Label runat="server" ID="lblLogin" Text="" class="form"></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <img src="Images/header_taglinelogo.gif" class="imagenFuji"/>

                            <!-- ModalPopupExtender -->
                            <ajaxToolkit:ModalPopupExtender ID="mdlRecPass" runat="server" PopupControlID="Panel1" TargetControlID="btnRecPass"
                                CancelControlID="btnClose" BackgroundCssClass="modalBackground">
                            </ajaxToolkit:ModalPopupExtender>
                            <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" align="center" Style="display: none">
                                <div style="height: 60px">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <div class="panel panel-default panel-success">
                                                <div class="panel-heading">
                                                    Solicitud recuperación de contraseña
                                                </div>
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-lg-12">
                                                            <small>Correo electrónico</small>
                                                            <asp:TextBox ID="txtRecPass" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" Text="* Capturar correo electrónico de la cuenta" ForeColor="Red" ControlToValidate="txtRecPass" ValidationGroup="vdgRecPass"></asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Text="* Formato Email incorrecto" ValidationGroup="vdgRecPass" ForeColor="red"
                                                                    ErrorMessage="Invalid Email" ControlToValidate="txtRecPass" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-12">
                                                            <asp:Label ID="lblMesaje" runat="server" Text="" ForeColor="Red" Font-Bold="true"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-12">
                                                            <asp:Button ID="btnClose" runat="server" Text="Cancelar" CssClass="btn btn-danger" OnClick="btnClose_Click"/>
                                                            <asp:Button runat="server" Text="Enviar" CssClass="btn btn-success" ID="btnEnviar" OnClick="btnEnviar_Click" ValidationGroup="vdgRecPass"/>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </asp:Panel>
                            <!-- ModalPopupExtender -->

                            <!-- ModalPopupExtender1 -->
                            <asp:Button runat="server" ID="btnChangePass" Style="display: none"/>
                            <ajaxToolkit:ModalPopupExtender ID="mdlChangePass" runat="server" PopupControlID="Panel2" TargetControlID="btnChangePass"
                                CancelControlID="btnSalir" BackgroundCssClass="modalBackground">
                            </ajaxToolkit:ModalPopupExtender>
                            <asp:Panel ID="Panel2" runat="server" CssClass="modalPopup" align="center" Style="display: none">
                                <div style="height: 60px">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <div class="panel panel-default panel-success">
                                                <div class="panel-heading">
                                                    Cambiar contraseña
                                                </div>
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-lg-12 col-md-12 col-sm-12">
                                                            <asp:Label Text="Contraseña actual" runat="server" AssociatedControlID="txtPassActual"></asp:Label>
                                                            <asp:TextBox runat="server" ID="txtPassActual" TextMode="Password" placeholder="Contraseña Actual" Text="" CssClass="form-control" Width="80%"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvPassActual" runat="server" ErrorMessage="* Campo requerido" ForeColor="Red" ControlToValidate="txtPassActual" ValidationGroup="vgChangePass"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-12 col-md-12 col-sm-12">
                                                            <asp:Label Text="Nueva contraseña" runat="server" AssociatedControlID="txtPassNueva"></asp:Label>
                                                            <asp:TextBox runat="server" ID="txtPassNueva" Text=""  TextMode="Password" placeholder=" Nueva Contraseña" CssClass="form-control" Width="80%" ValidationGroup="vgChangePass"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvNewPass" runat="server" ErrorMessage="* Campo requerido" ForeColor="Red" ControlToValidate="txtPassNueva" ValidationGroup="vgChangePass"></asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="rgVPass" Font-Bold="true" runat="server" ControlToValidate="txtPassNueva" ValidationExpression=".{8}.*" 
                                                                ErrorMessage="Capturar al menos 8 caracteres" Text="* Capturar al menos 8 caracteres" ForeColor="Red" ValidationGroup="vgChangePass"></asp:RegularExpressionValidator>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-12 col-md-12 col-sm-12">
                                                            <asp:Label Text="Confirmar Contraseña" runat="server" AssociatedControlID="txtPassNueva1"></asp:Label>
                                                            <asp:TextBox runat="server" ID="txtPassNueva1" Text=""  TextMode="Password" placeholder="Confirmar Contraseña" CssClass="form-control" Width="80%" ValidationGroup="vgChangePass"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvNewPass1" runat="server" ErrorMessage="* Campo requerido" ForeColor="Red" ControlToValidate="txtPassNueva1" ValidationGroup="vgChangePass"></asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="rgVPass1" Font-Bold="true" runat="server" ControlToValidate="txtPassNueva1" ValidationExpression=".{8}.*" 
                                                                ErrorMessage="Capturar al menos 8 caracteres" Text="* Capturar al menos 8 caracteres" ForeColor="Red" ValidationGroup="vgChangePass"></asp:RegularExpressionValidator>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-12 col-md-12 col-sm-12">
                                                            <asp:Label ID="lblMensajePass" runat="server" Text="" ForeColor="Red" Font-Bold="true"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-12 col-md-12 col-sm-12">
                                                            <asp:Button ID="btnSalir" runat="server" Text="Cancelar" CssClass="btn btn-danger" OnClick="btnSalir_Click"/>
                                                            <asp:Button runat="server" Text="Cambiar" CssClass="btn btn-success" ID="btnChange" OnClick="btnChange_Click" ValidationGroup="vgChangePass"/>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </asp:Panel>
                            <!-- ModalPopupExtender1 -->
                        </form>
                    </div>
                    <!-- footer content -->
                    <footer>
                        <div class="pull-center">
                            <p><%: DateTime.Now.Year %> - Feed2Cloud - Versión 1.0    </p>
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
