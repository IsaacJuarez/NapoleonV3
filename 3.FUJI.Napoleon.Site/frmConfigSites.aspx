<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmConfigSites.aspx.cs" Inherits="_3.FUJI.Napoleon.Site.frmConfigSites" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <p><asp:Label runat="server" Text="" ID="lblMensajeConf"></asp:Label></p>
    </div>
    <div class="row">
        <div class="x_panel">
            <div class="messagealert" id="alert_container"></div>
            <div class="x_title">
                <h2>Configuración de Sitios</h2>
                <ul class="nav navbar-right panel_toolbox">
                    <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                    </li>
                </ul>
                <div class="clearfix"></div>
            </div>
            <div class="x_content">
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="row">
                            <div class="col-lg-2 col-md-1 col-sm-1 col-xs-1">

                            </div>
                            <div class="col-lg-8 col-md-10 col-sm-10 col-xs-10">
                                <div class="panel" runat="server"  id="divSitios">
                                    <div class="row">
                                        <div class="title_right">
                                            <div class="col-md-5 col-sm-5 col-xs-12 form-group pull-right top_search">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtBusqueda" CssClass="form-control" placeholder="Búsqueda Sitio..." runat="server" Width="100%" AutoPostBack="true" OnTextChanged="txtBusqueda_TextChanged" ></asp:TextBox>
                                                    <span class="input-group-btn">
                                                        <asp:Button CssClass="btn btn-default" runat="server" ID="btnBusqueda" OnClick="btnBusqueda_Click" Text="Ir"/>
                                                    </span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <asp:UpdatePanel ID="updGrid" runat="server">
                                            <ContentTemplate>
                                                <asp:Panel runat="server" ID="pnlGrid">
                                                    <asp:GridView ID="grvBusqueda" runat="server" AllowPaging="true" CssClass="table table-striped table-bordered"
                                                        PageSize="5" AutoGenerateColumns="false" OnRowDataBound="grvBusqueda_RowDataBound" Font-Size="10px"
                                                        OnPageIndexChanging="grvBusqueda_PageIndexChanging" DataKeyNames="vchNombreSitio"
                                                        OnRowCommand="grvBusqueda_RowCommand"
                                                        EmptyDataText="No hay resultado bajo el criterio de búsqueda.">
                                                        <Columns>
                                                            <asp:BoundField DataField="id_Sitio"  HeaderText="ID" ReadOnly="true" />
                                                            <asp:BoundField DataField="vchClaveSitio" HeaderText="Clave de Sitio" ReadOnly="true" />
                                                            <asp:BoundField DataField="vchNombreSitio" HeaderText="Nombre de Sitio" ReadOnly="true" />
                                                            <asp:BoundField DataField="vchIPCliente" HeaderText="IP Cliente" ReadOnly="true" />
                                                            <asp:BoundField DataField="vchMaskCliente" HeaderText="Mascara de Red" ReadOnly="true" />
                                                            <asp:BoundField DataField="intPuertoCliente" HeaderText="Puerto" ReadOnly="true" HeaderStyle-CssClass=" hidden-xs" ItemStyle-CssClass=" hidden-xs"/>
                                                            <asp:BoundField DataField="vchAETitle" HeaderText="AETitle" ReadOnly="true" HeaderStyle-CssClass=" hidden-xs" ItemStyle-CssClass=" hidden-xs"/>
                                                            <asp:BoundField DataField="vchPathLocal" HeaderText="Folder Local" ReadOnly="true" HeaderStyle-CssClass="hidden-md hidden-xs" ItemStyle-CssClass="hidden-md hidden-xs"/>
                                                            <%--<asp:BoundField DataField="vchIPServidor" HeaderText="IP Servidor" ReadOnly="true" HeaderStyle-CssClass="hidden-md hidden-xs" ItemStyle-CssClass="hidden-md hidden-xs"/>
                                                            <asp:BoundField DataField="in_tPuertoServer" HeaderText="Puerto Servidor" ReadOnly="true"  HeaderStyle-CssClass="hidden-md hidden-xs" ItemStyle-CssClass="hidden-md hidden-xs"/>
                                                            <asp:BoundField DataField="vchAETitleServer" HeaderText="AETitle Servidor" ReadOnly="true"  HeaderStyle-CssClass="hidden-md hidden-xs" ItemStyle-CssClass="hidden-md hidden-xs"/>
                                                            <asp:BoundField DataField="datFechaSistema" DataFormatString="{0:MM/dd/yyyy}" HeaderText="Fecha" ReadOnly="true"  HeaderStyle-CssClass="hidden-md hidden-xs" ItemStyle-CssClass="hidden-md hidden-xs"/>
                                                            <asp:BoundField DataField="vchUserAdmin" HeaderText="Usuario de Act." ReadOnly="true"  HeaderStyle-CssClass="hidden-md hidden-xs" ItemStyle-CssClass="hidden-md hidden-xs"/>--%>
                                                            <asp:TemplateField HeaderText="Configuraciones" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnConfig" CausesValidation="false" CommandName="Configuracion" CommandArgument='<%#Eval("id_Sitio") %>' runat="server">
                                                                        <i class="fa fa-cogs green" aria-hidden="true" title="Editar las Configuraciones" style="font-size:25px;"></i>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Contacto">
                                                                <ItemTemplate>
                                                                    <center>
                                                                        <asp:LinkButton ID="btnVisualizar"  CommandName="viewEditar" CommandArgument='<%# Bind("id_Sitio") %>' runat="server">
                                                                            <asp:Image ID="ImageVisializa" runat="server" ImageUrl="~/images/ic_action_user.png" Height="25px" Width="25px" ToolTip="Editar"/>
                                                                        </asp:LinkButton>
                                                                    </center>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <PagerTemplate>
                                                            <asp:Label ID="lblTemplate" runat="server" Text="Muestra Filas: " CssClass="Label" />
                                                            <asp:DropDownList ID="ddlBandeja" runat="server" AutoPostBack="true" CausesValidation="false"
                                                                Enabled="true" OnSelectedIndexChanged="ddlBandeja_SelectedIndexChanged">
                                                                    <asp:ListItem Value="5" />
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
                            <div class="col-lg-2 col-md-1 col-sm-1 col-xs-1">

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Bootstrap Modal Dialog -->
    <div class="modal fade" id="myModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <div class="row">
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                        <h2><asp:Label Text="Sitio: " runat="server"></asp:Label><small><asp:Label runat="server" ID="lblIDSitio" ForeColor="Green"></asp:Label></small></h2>
                                     </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-body">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <div class="row">
                                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                        <asp:Label runat="server" ID="lblClave" Text="Nombre Contacto" AssociatedControlID="txtNombreContacto"></asp:Label>
                                    </div>
                                    <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9 text-left">
                                        <asp:Label runat="server" ForeColor="Green" Font-Bold="true"  Text="" id="txtNombreContacto" ></asp:Label>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                                        <asp:Label runat="server" Text="Correo Electrónico:"></asp:Label>
                                    </div>
                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 text-left">
                                        <asp:Label runat="server" ID="txtEmailContacto" ForeColor="Green" Font-Bold="true"  Width="100%" Text="" ></asp:Label>
                                    </div>
                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                                        <asp:Label runat="server" Text="Telefono:" ></asp:Label>
                                    </div>
                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 text-left">
                                        <asp:Label runat="server" ForeColor="Green" Font-Bold="true" Text="" Width="100%" ID="txtTelefono" ></asp:Label>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                        <asp:Label runat="server" Text="Vendedor:"></asp:Label>
                                    </div>
                                    <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9 text-left">
                                        <asp:Label runat="server" ID="txtVendedor" Text="" Width="100%" ForeColor="Green" Font-Bold="true" ></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button class="btn btn-info" data-dismiss="modal" aria-hidden="true">Cerrar</button>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <div class="modal fade" id="ConfigModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <h1>Configuraciones</h1>
            <asp:UpdatePanel ID="upConfigModal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <div class="row">
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                        <h2><asp:Label Text="Sitio: " runat="server"></asp:Label><small><asp:Label runat="server" ID="Label1" ForeColor="Green"></asp:Label></small></h2>
                                        <asp:Label runat="server" ID="Label2" ForeColor="Green" Visible="false"></asp:Label>
                                        <asp:Label runat="server" ID="lblIDSite" ForeColor="Green" Visible="false"></asp:Label>
                                     </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12">
                                    <div class="row">
                                        <div class="col-lg-6 col-md-6 col-sm-6">
                                            <div class="row">
                                                <div class="col-lg-4 col-md-12 col-sm-12">
                                                    <asp:Label runat="server" Text="AETitle Local (SCU)" ForeColor="DarkGreen" ></asp:Label>
                                                </div>
                                                <div class="col-lg-8 col-md-12 col-sm-12">
                                                    <asp:TextBox ID="txtSCULocal" runat="server" Text="" CssClass="form-control" placeholder="AETitle Local (SCU)" ></asp:TextBox>
                                                    <asp:RequiredFieldValidator runat="server" ID="rfvSCULocal" ControlToValidate="txtSCULocal" ErrorMessage="*" Text="*" ValidationGroup="vgServerConfig" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-6 col-md-6 col-sm-6">
                                            <div class="row">
                                                <div class="col-lg-6 col-md-12 col-sm-12">
                                                    <asp:Label runat="server" Text="AETitle VNA (SCP)" ForeColor="DarkGreen" ></asp:Label>
                                                </div>
                                                <div class="col-lg-6 col-md-12 col-sm-12">
                                                    <asp:TextBox ID="txtSCPVNA" runat="server" Text="" CssClass="form-control" placeholder="AETitle VNA (SCP)" ></asp:TextBox>
                                                    <asp:RequiredFieldValidator runat="server" ID="rfvSCPVNA" ControlToValidate="txtSCPVNA" ErrorMessage="*" Text="*" ValidationGroup="vgServerConfig" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-lg-6 col-md-12 col-sm-12">
                                                    <asp:Label runat="server" Text="IP VNA (SCP)" ForeColor="DarkGreen" ></asp:Label>
                                                </div>
                                                <div class="col-lg-6 col-md-12 col-sm-12">
                                                    <div class="row">
                                                        <div class="col-lg-3 col-md-3 col-sm-3">
                                                            <asp:TextBox ID="txtIPVNA1" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                                                            <asp:RequiredFieldValidator runat="server" ID="rfvIPVNA1" ControlToValidate="txtIPVNA1" ErrorMessage="*" Text="*" ValidationGroup="vgServerConfig" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="col-lg-3 col-md-3 col-sm-3">
                                                            <asp:TextBox ID="txtIPVNA2" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                                                            <asp:RequiredFieldValidator runat="server" ID="rfvIPVNA2" ControlToValidate="txtIPVNA2" ErrorMessage="*" Text="*" ValidationGroup="vgServerConfig" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="col-lg-3 col-md-3 col-sm-3">
                                                            <asp:TextBox ID="txtIPVNA3" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                                                            <asp:RequiredFieldValidator runat="server" ID="rfvIPVNA3" ControlToValidate="txtIPVNA3" ErrorMessage="*" Text="*" ValidationGroup="vgServerConfig" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="col-lg-3 col-md-3 col-sm-3">
                                                            <asp:TextBox ID="txtIPVNA4" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                                                            <asp:RequiredFieldValidator runat="server" ID="rfvIPVNA4" ControlToValidate="txtIPVNA4" ErrorMessage="*" Text="*" ValidationGroup="vgServerConfig" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-lg-6 col-md-12 col-sm-12">
                                                    <asp:Label runat="server" Text="Puerto VNA (SCP)" ForeColor="DarkGreen" ></asp:Label>
                                                </div>
                                                <div class="col-lg-6 col-md-12 col-sm-12">
                                                    <asp:TextBox ID="txtPuertoVNA" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtPuertoVNA" runat="server" ErrorMessage="Solo numeros" ValidationExpression="\d+">
                                                    </asp:RegularExpressionValidator>
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtPuertoVNA" ErrorMessage="*" Text="*" ValidationGroup="vgServerConfig" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button runat="server" ID="btnCancelDetalle" class="btn btn-info" data-dismiss="modal" aria-hidden="true" OnClick="btnCancelDetalle_Click" Text="Cerrar"></asp:Button>
                            <asp:Button runat="server" ID="btnGuardarDetalle" OnClick="btnGuardarDetalle_Click" CssClass="btn btn-success" Text="Guardar" ValidationGroup="vgServerConfig" />
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

