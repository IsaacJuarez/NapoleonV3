<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmLogin.aspx.cs" Inherits="_3.FUJI.Napoleon.Site.frmLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>LOG IN FEED2CLOUD</title>
    <link rel="stylesheet" href="Content/style.css"/>
    <link rel="stylesheet" href="vendors/bootstrap/dist/css/bootstrap.min.css"/>
    <script src="vendors/jquery/dist/jquery.min.js"></script>
    <style type="text/css">
        .imagen{
            -webkit-background-size: cover;
           -moz-background-size: cover;
           -o-background-size: cover;
           background-size: cover;
           height: 90%;
           width: 90% ;
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
                        <form id="form" runat="server" class="form">
                            <div>
                                <asp:TextBox ID="txtUsuarioUser" placeholder="Usuario" runat="server" ValidationGroup="vgLogin" ></asp:TextBox>
		                        <asp:TextBox ID="txtContrasenaUser" TextMode="Password" placeholder="Contraseña" runat="server" ValidationGroup="vgLogin"></asp:TextBox>
		                        <asp:Button type="submit" id="btnLogin" runat="server" Text="Entrar" OnClick="btnLogin_Click" ValidationGroup="vgLogin"></asp:Button>
                                <div>
                                    <asp:RequiredFieldValidator ErrorMessage="Usuario requerido." ForeColor="#61210B" Font-Bold="true" runat="server" ID="rfvUser" ControlToValidate="txtUsuarioUser" ValidationGroup="vgLogin"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ErrorMessage="Contraseña requerida" ForeColor="#61210B" Font-Bold="true" runat="server" ID="rfvPass" ControlToValidate="txtContrasenaUser" ValidationGroup="vgLogin"></asp:RequiredFieldValidator>
                                </div>
                                <br />
                                <div>
                                    <asp:Label runat="server" ID="lblLogin" Text=""></asp:Label>
                                </div>
                            </div>
                            <img src="Images/header_taglinelogo.gif" class="imagenFuji"/>
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
