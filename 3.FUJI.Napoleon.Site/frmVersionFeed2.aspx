<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmVersionFeed2.aspx.cs" Inherits="_3.FUJI.Napoleon.Site.frmVersionFeed2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="vendors/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet"/>
    <script src="vendors/jquery/dist/jquery.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="messagealert" id="alert_container">
        </div>
        <div class="col-lg-2 col-md-1 col-sm-1 col-xs-1">
        </div>
        <div class="col-lg-8 col-md-10 col-sm-8 col-xs-8">
            <div class="x_panel">
                <div class="x_title">
                    <h2>Feed2Cloud Desktop</h2>
                    <ul class="nav navbar-right panel_toolbox">
                        <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                        </li>
                    </ul>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">
                    <p class="text-muted font-13 m-b-30">
                        * Versiones de la aplicación de Feed2Cloud de Escritorio.
                    </p>
                    <div class="row">
                        <div class="col-lg-12 col-mg-12 col-sm-12">
                            <div class="row">
                                <div class="col-lg-4 col-md-12 col-sm-12 col-xs-12">
                                    <div class="x_panel" runat="server" id="divVersion">
                                        <div class="x_title">
                                            <h2>Feed2Cloud de Escritorio <small></small></h2>
                                            <ul class="nav navbar-right panel_toolbox">
                                              <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                                              </li>
                                            </ul>
                                            <div class="clearfix"></div>
                                        </div>
                                        <div class="x_content">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <div class="row">
                                                        <div class="col-lg-12 col-md-12 col-sm-12">
                                                            <asp:Label runat="server" Text="Nombre de Version" AssociatedControlID="txtVersion"></asp:Label>
                                                            <asp:TextBox ID="txtVersion" runat="server" Text="" CssClass="form-control" ></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"  ControlToValidate="txtVersion"
                                                                ErrorMessage="* Campo requerido" Text="* Campo requerido" Display="Dynamic" ValidationGroup="Save" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-12 col-md-12 col-sm-12">
                                                            <asp:Label runat="server" Text="Archivo" AssociatedControlID="fuFile"></asp:Label>
                                                            <asp:FileUpload runat="server" ID="fuFile" ForeColor="DarkBlue" Font-Size="Medium"/>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="fuFile"  ValidationGroup="Save"
                                                                ErrorMessage="* Se requiere un archivo." Text="* Se requiere un archivo." ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-12 col-md-12 col-sm-12">
                                                            <asp:Label runat="server" Text="Archivo" AssociatedControlID="txtComentarios"></asp:Label>
                                                            <asp:TextBox TextMode="MultiLine" runat="server" Text="" ID="txtComentarios" Height="100" Width="100%"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-lg-12 col-md-12 col-sm-12 text-right">
                                                            <asp:Button runat="server" ID="btnLimpiar" Text="Cancelar" OnClick="btnLimpiar_Click" CssClass="btn btn-danger" />
                                                            <asp:Button runat="server" ID="btnAddFile" Text="Guardar" OnClick="btnAddFile_Click"  CssClass="btn btn-success" ValidationGroup="Save"/>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                   <asp:PostBackTrigger ControlID="btnAddFile"  />
                                               </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-8 col-md-12 col-sm-12 col-xs-12">
                                    <div class="row">
                                        <asp:UpdatePanel runat="server" ID="updGridSites">
                                            <ContentTemplate>
                                                <asp:Panel runat="server" ID="pnlFiles">
                                                    <asp:GridView ID="grvFiles" runat="server" AllowPaging="true" CssClass="table table-striped table-bordered" 
                                                        PageSize="10" AutoGenerateColumns="false" OnRowDataBound="grvFiles_RowDataBound" Font-Size="10px" OnPageIndexChanging="grvFiles_PageIndexChanging"
                                                         OnRowCommand="grvFiles_RowCommand" EmptyDataText="No hay resultado bajo el criterio de búsqueda">
                                                        <Columns>
                                                            <asp:BoundField DataField="intVersionID"  HeaderText="ID" ReadOnly="true" />
                                                            <asp:BoundField DataField="vchVersion" HeaderText="Versión" ReadOnly="true" />
                                                            <asp:BoundField DataField="datFecha" HeaderText="Fecha" DataFormatString="{0:MM/dd/yyyy HH:mm}" ReadOnly="true" />
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="lnkDownload" runat="server" BackColor="Transparent"  Height="25px" Width="25px"  ImageUrl="~/Images/ic-action-download.png"
                                                                        CommandArgument='<%# Eval("intVersionID") %>' CommandName="Descargar" ToolTip="Descargar" ></asp:ImageButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Estatus" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="imbEstatus" runat="server" BackColor="Transparent"  Height="25px" Width="25px" 
                                                                        CommandArgument='<%#Eval("intVersionID") %>' CommandName="Estatus" ToolTip="Cambia el estatus del Archivo" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <PagerTemplate>
                                                            <asp:Label ID="lblTemplate" runat="server" Text="Muestra Filas: " CssClass="Label" />
                                                            <asp:DropDownList ID="ddlBandeja" runat="server" AutoPostBack="true" CausesValidation="false"
                                                                Enabled="true" OnSelectedIndexChanged="ddlBandeja_SelectedIndexChanged">
                                                                    <asp:ListItem Value="10" />
                                                                    <asp:ListItem Value="15" />
                                                                    <asp:ListItem Value="20" />
                                                                    <asp:ListItem Value="25" />
                                                            </asp:DropDownList>
                                                            &nbsp;Página
                                                            <asp:TextBox ID="txtBandeja" runat="server" AutoPostBack="true" OnTextChanged="txtBandeja_TextChanged"
                                                                Width="40" MaxLength="10" />
                                                            de
                                                            <asp:Label ID="lblBandejaTotal" runat="server" />
                                                            &nbsp;
                                                            <asp:Button ID="btnBandeja_I" runat="server" CommandName="Page" CausesValidation="false"
                                                                ToolTip="Página Anterior" CommandArgument="Prev" CssClass="previous" />
                                                            <asp:Button ID="btnBandeja_II" runat="server" CommandName="Page" CausesValidation="false"
                                                                ToolTip="Página Siguiente" CommandArgument="Next" CssClass="next" />
                                                        </PagerTemplate>
                                                        <HeaderStyle CssClass="headerstyle" />
                                                        <FooterStyle CssClass="text-center" />
                                                        <PagerStyle CssClass="text-center" />
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                    <h2>Manuales de Usuario <small></small></h2>
                                </div>
                                <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12 text-right">
                                    <asp:LinkButton ID="btnPreRequisitos" OnClick="btnPreRequisitos_Click" runat="server">
                                        <i class="fa fa-book green" aria-hidden="true" title="Manual de Pre-requisitos" style="font-size:25px;"></i>
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnFeed2" OnClick="btnFeed2_Click" runat="server">
                                        <i class="fa fa-book blue" aria-hidden="true" title="Manual de Feed2" style="font-size:25px;"></i>
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-lg-2 col-md-1 col-sm-1 col-xs-1">
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

        function Redirecciona(strRuta) {
            var sID = Math.round(Math.random() * 10000000000);
            var winX = screen.availWidth;
            var winY = screen.availHeight;
            sID = "E" + sID;
            window.open(strRuta, sID,
                "menubar=yes,toolbar=yes,location=yes,directories=yes,status=yes,resizable=yes" +
                ",scrollbars=yes,top=0,left=0,screenX=0,screenY=0,Width=" +
                winX + ",Height=" + winY);
        }
    </script>
</asp:Content>
