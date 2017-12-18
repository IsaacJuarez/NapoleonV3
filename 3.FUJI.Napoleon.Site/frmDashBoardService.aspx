<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmDashBoardService.aspx.cs" Inherits="_3.FUJI.Napoleon.Site.frmDashBoardService" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Bootstrap -->
    <script src="<%= ResolveClientUrl("~/vendors/jquery/dist/jquery.min.js")%>" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="x_panel">
                <div class="x_title">
                    <div class="messagealert" id="alert_container">
                    </div>
                    <h2>Dasboard Servicios Napoleón</h2>
                    <ul class="nav navbar-right panel_toolbox">
                      <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                      </li>
                    </ul>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">
                    <p class="text-muted font-13 m-b-30">
                      Monitor de Servicios.
                    </p>
                    <div class="row">
                        <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1"></div>
                        <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10">
                            <asp:UpdatePanel ID="updGrid" runat="server">
                                <ContentTemplate>
                                    <asp:Panel runat="server" ID="pnlGrid">
                                        <asp:GridView ID="grvServicios" runat="server" AllowPaging="true" CssClass="table table-striped table-bordered"
                                            PageSize="20" AutoGenerateColumns="false" OnRowDataBound="grvServicios_RowDataBound" Font-Size="10px"
                                            OnPageIndexChanging="grvServicios_PageIndexChanging"
                                            EmptyDataText="No hay resultado bajo el criterio de búsqueda.">
                                            <Columns>
                                                <asp:BoundField DataField="id_sitio"  HeaderText="ID" ReadOnly="true" />
                                                <asp:BoundField DataField="vchClaveSitio"  HeaderText="Clave de Sitio" ReadOnly="true" />
                                                <asp:BoundField DataField="vchNombreSitio"  HeaderText="Sitio" ReadOnly="true" />
                                                <asp:TemplateField HeaderText="Recepción de Estudios" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <center>
                                                        <table style="width:100%" width="100%">
                                                            <tr>
                                                                <td>
                                                                    <asp:LinkButton ID="btnLOn"  Enabled="false" runat="server">
                                                                        <asp:Image ID="imgLActivo" runat="server" ImageUrl="~/Images/out.png" Height="25px" Width="25px" ToolTip="Activo"/>
                                                                    </asp:LinkButton>
                                                                </td>
                                                                <td>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label runat="server" Text="Última actividad"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lblFechaUltimaConL" runat="server" Text='<%# String.Format("{0}", Eval("datFechaSCP")) %>' ForeColor="DarkGreen" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblTiempoTransL" runat="server" Text="" ForeColor="DarkGreen" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        </center>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sincronización" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <center>
                                                        <table style="width:100%" width="100%">
                                                            <tr>
                                                                <td>
                                                                    <asp:LinkButton ID="btnSOn"  Enabled="false" runat="server">
                                                                        <asp:Image ID="imgSActivo" runat="server" ImageUrl="~/Images/out.png" Height="25px" Width="25px" ToolTip="Activo"/>
                                                                    </asp:LinkButton>
                                                                </td>
                                                                <td>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label runat="server" Text="Última actividad"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lblFechaUltimaConS" runat="server" Text='<%# String.Format("{0}", Eval("datFechaSync")) %>' ForeColor="DarkGreen" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblTiempoTransS" runat="server" Text="" ForeColor="DarkGreen" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        </center>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Envío de Estudios" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <center>
                                                        <table style="width:100%" width="100%">
                                                            <tr>
                                                                <td>
                                                                    <asp:LinkButton ID="btnHOn"  Enabled="false" runat="server">
                                                                        <asp:Image ID="imgHActivo" runat="server" ImageUrl="~/Images/out.png" Height="25px" Width="25px" ToolTip="Activo"/>
                                                                    </asp:LinkButton>
                                                                </td>
                                                                <td>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label runat="server" Text="Última actividad"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lblFechaUltimaConH" runat="server" Text='<%# String.Format("{0}", Eval("datFechaSCU")) %>' ForeColor="DarkGreen" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblTiempoTransH" runat="server" Text="" ForeColor="DarkGreen" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        </center>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerTemplate>
                                                <asp:Label ID="lblTemplate" runat="server" Text="Muestra Filas: " CssClass="Label" />
                                                <asp:DropDownList ID="ddlBandeja" runat="server" AutoPostBack="true" CausesValidation="false"
                                                    Enabled="true" OnSelectedIndexChanged="ddlBandeja_SelectedIndexChanged">
                                                        <asp:ListItem Value="20" />
                                                        <asp:ListItem Value="25" />
                                                        <asp:ListItem Value="30" />
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
                        <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1"></div>
                    </div>
                </div>
            </div>
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
            $(control).fadeTo(2000, 500).slideUp(500, function () {
                $(control).slideUp(700);
            });
        }
    </script>
</asp:Content>
