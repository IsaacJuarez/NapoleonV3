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
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
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
                                                        OnPageIndexChanging="grvBusqueda_PageIndexChanging"
                                                        OnRowCommand="grvBusqueda_RowCommand"
                                                        EmptyDataText="No hay resultado bajo el criterio de búsqueda.">
                                                        <Columns>
                                                            <asp:BoundField DataField="id_Sitio"  HeaderText="ID" ReadOnly="true" />
                                                            <asp:BoundField DataField="vchClaveSitio" HeaderText="Clave de Sitio" ReadOnly="true" />
                                                            <asp:BoundField DataField="vchNombreSitio" HeaderText="Nombre de Sitio" ReadOnly="true" />
                                                            <asp:BoundField DataField="vchIPCliente" HeaderText="IP Cliente" ReadOnly="true" />
                                                            <asp:BoundField DataField="vchMaskCliente" HeaderText="Mascara de Red" ReadOnly="true" />
                                                            <asp:BoundField DataField="intPuertoCliente" HeaderText="Puerto" ReadOnly="true" />
                                                            <asp:BoundField DataField="vchAETitle" HeaderText="AETitle" ReadOnly="true" />
                                                            <asp:BoundField DataField="vchPathLocal" HeaderText="Folder Local" ReadOnly="true" />
                                                            <asp:BoundField DataField="vchIPServidor" HeaderText="IP Servidor" ReadOnly="true" />
                                                            <asp:BoundField DataField="in_tPuertoServer" HeaderText="Puerto Servidor" ReadOnly="true" />
                                                            <asp:BoundField DataField="vchAETitleServer" HeaderText="AETitle Servidor" ReadOnly="true" />
                                                            <asp:BoundField DataField="datFechaSistema" DataFormatString="{0:MM/dd/yyyy}" HeaderText="Fecha" ReadOnly="true" />
                                                            <asp:BoundField DataField="vchUserAdmin" HeaderText="Usuario de Act." ReadOnly="true" />
                                                            <asp:TemplateField HeaderText="Editar">
                                                                <ItemTemplate>
                                                                    <center>
                                                                        <asp:LinkButton ID="btnVisualizar"  CommandName="viewEditar" CommandArgument='<%# Bind("id_Sitio") %>' runat="server">
                                                                            <asp:Image ID="ImageVisializa" runat="server" ImageUrl="~/images/ic_action_edit.png" Height="25px" Width="25px" ToolTip="Editar"/>
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
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

