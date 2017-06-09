<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmConfiguraciones.aspx.cs" Inherits="_3.FUJI.Napoleon.Site.frmConfiguraciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Datatables -->
    <link href="<%= ResolveClientUrl("~/vendors/datatables.net-bs/css/dataTables.bootstrap.min.css")%>" rel="stylesheet"/>
    <link href="<%= ResolveClientUrl("~/vendors/datatables.net-buttons-bs/css/buttons.bootstrap.min.css")%>" rel="stylesheet"/>
    <link href="<%= ResolveClientUrl("~/vendors/datatables.net-fixedheader-bs/css/fixedHeader.bootstrap.min.css")%>" rel="stylesheet"/>
    <link href="<%= ResolveClientUrl("~/vendors/datatables.net-responsive-bs/css/responsive.bootstrap.min.css")%>" rel="stylesheet"/>
    <link href="<%= ResolveClientUrl("~/vendors/datatables.net-scroller-bs/css/scroller.bootstrap.min.css")%>" rel="stylesheet"/>
    <script src="<%= ResolveClientUrl("~/vendors/jquery/dist/jquery.min.js")%>" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <p><asp:Label runat="server" Text="" ID="lblMensajeConf"></asp:Label></p>
    </div>
    <div class="row">
        <div class="x_panel">
            <div class="messagealert" id="alert_containerSites"></div>
            <div class="x_title">
                <h2>Sitios</h2>
                <ul class="nav navbar-right panel_toolbox">
                    <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                    </li>
                </ul>
                <div class="clearfix"></div>
            </div>
            <div class="x_content">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="row">
                        <div class="col-lg-4 col-md-12 col-sm-12 col-xs-12">
                            <div class="x_panel" runat="server" id="divSites">
                                <div class="x_title">
                                    <h2>Sitio <small></small></h2>
                                    <ul class="nav navbar-right panel_toolbox">
                                      <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                                      </li>
                                    </ul>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content">
                                    <div class="row">
                                        <div class="title_right">
                                            <div class="col-md-9 col-sm-9 col-xs-10 form-group pull-right top_search">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtBusquedaSite" CssClass="form-control" placeholder="Búsqueda..." runat="server" Width="80%" AutoPostBack="true" OnTextChanged="txtBusquedaSite_TextChanged"></asp:TextBox>
                                                    <asp:Button CssClass="btn btn-default" runat="server" ID="btnBusquedaSite" OnClick="btnBusquedaSite_Click" Text="Ir" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <asp:UpdatePanel runat="server" ID="updGridSites">
                                            <ContentTemplate>
                                                <asp:Panel runat="server" ID="pnlSites">
                                                    <asp:GridView ID="grvSites" runat="server" AllowPaging="true" CssClass="table table-striped table-bordered" 
                                                        PageSize="10" AutoGenerateColumns="false" OnRowDataBound="grvSites_RowDataBound" Font-Size="10px" OnPageIndexChanging="grvSites_PageIndexChanged"
                                                         OnRowCommand="grvSites_RowCommand" EmptyDataText="No hay resultado bajo el criterio de búsqueda">
                                                        <Columns>
                                                            <asp:BoundField DataField="id_Sitio"  HeaderText="ID" ReadOnly="true" />
                                                            <asp:BoundField DataField="vchClaveSitio" HeaderText="Clave de Sitio" ReadOnly="true" />
                                                            <asp:BoundField DataField="vchNombreSitio" HeaderText="Nombre de Sitio" ReadOnly="true" />
                                                            <asp:TemplateField HeaderText="Editar"  ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                        <asp:ImageButton ID="btnVisualizar" ImageUrl="~/images/ic_action_edit.png" CausesValidation="false" CommandName="viewEditar" Height="25px" Width="25px" ToolTip="Editar" CommandArgument='<%# Bind("id_Sitio") %>' runat="server" >
                                                                        </asp:ImageButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Estatus" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imbEstatus" runat="server" BackColor="Transparent"  Height="25px" Width="25px" 
                                                                            CommandArgument='<%#Eval("id_Sitio") %>' CommandName="Estatus" ToolTip="Cambia el estatus del Sitio" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                        </Columns>
                                                        <PagerTemplate>
                                                            <asp:Label ID="lblTemplate" runat="server" Text="Muestra Filas: " CssClass="Label" />
                                                            <asp:DropDownList ID="ddlBandejaSitio" runat="server" AutoPostBack="true" CausesValidation="false"
                                                                Enabled="true" OnSelectedIndexChanged="ddlBandejaSitio_SelectedIndexChanged">
                                                                    <asp:ListItem Value="10" />
                                                                    <asp:ListItem Value="15" />
                                                                    <asp:ListItem Value="20" />
                                                                    <asp:ListItem Value="25" />
                                                            </asp:DropDownList>
                                                            &nbsp;Página
                                                            <asp:TextBox ID="txtBandejaSitio" runat="server" AutoPostBack="true" OnTextChanged="txtBandejaSitio_TextChanged"
                                                                Width="40" MaxLength="10" />
                                                            de
                                                            <asp:Label ID="lblBandejaTotalSitio" runat="server" />
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
                        <div class="col-lg-4 col-md-12 col-sm-12 col-xs-12">
                            <div class="x_panel" runat="server" id="divProyec">
                                <div class="x_title">
                                    <h2>Proyectos <small></small></h2>
                                    <ul class="nav navbar-right panel_toolbox">
                                      <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                                      </li>
                                    </ul>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content">
                                    <div class="row">
                                        <div class="title_right">
                                            <div class="col-md-9 col-sm-9 col-xs-10 form-group pull-right top_search">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtProeyct" CssClass="form-control" placeholder="Búsqueda..." runat="server" Width="80%" AutoPostBack="true" OnTextChanged="txtProeyct_TextChanged" ></asp:TextBox>
                                                    <span class="input-group-btn">
                                                        <asp:Button CssClass="btn btn-default" runat="server" ID="btnProyect" OnClick="btnProyect_Click" Text="Ir"/>
                                                    </span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <asp:UpdatePanel runat="server" ID="updProyect">
                                            <ContentTemplate>
                                                <asp:Panel runat="server" ID="pnlProyect">
                                                    <asp:GridView runat="server" ID="grvProyectos" AllowPaging="true" CssClass="table table-striped table-bordered" 
                                                        ageSize="5" AutoGenerateColumns="false" OnRowDataBound="grvProyectos_RowDataBound" Font-Size="10px" OnPageIndexChanging="grvProyectos_PageIndexChanged"
                                                         OnRowCommand="grvProyectos_RowCommand"  EmptyDataText="No hay resultado bajo el criterio de búsqueda">
                                                        <Columns>
                                                            <asp:BoundField DataField="intProyectoID"  HeaderText="ID" ReadOnly="true" />
                                                            <asp:BoundField DataField="vchProyectoDesc" HeaderText="Clave de Sitio" ReadOnly="true" />
                                                            <asp:TemplateField HeaderText="Editar">
                                                                <ItemTemplate>
                                                                    <center>
                                                                        <asp:ImageButton ID="btnVisualizar" CausesValidation="false" CommandName="viewEditar" CommandArgument='<%# Bind("intProyectoID") %>' runat="server" ImageUrl="~/images/ic_action_edit.png" Height="25px" Width="25px" ToolTip="Editar">
                                                                        </asp:ImageButton>
                                                                    </center>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Estatus" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="imbEstatus" runat="server" BackColor="Transparent"  Height="25px" Width="25px" 
                                                                        CommandArgument='<%#Eval("intProyectoID") %>' CommandName="Estatus" ToolTip="Cambia el estatus del Proyecto" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <PagerTemplate>
                                                            <asp:Label ID="lblTemplate" runat="server" Text="Muestra Filas: " CssClass="Label" />
                                                            <asp:DropDownList ID="ddlBandejaProy" runat="server" AutoPostBack="true" CausesValidation="false"
                                                                Enabled="true" OnSelectedIndexChanged="ddlBandejaProy_SelectedIndexChanged">
                                                                    <asp:ListItem Value="10" />
                                                                    <asp:ListItem Value="15" />
                                                                    <asp:ListItem Value="20" />
                                                                    <asp:ListItem Value="25" />
                                                            </asp:DropDownList>
                                                            &nbsp;Página
                                                            <asp:TextBox ID="txtBandejaProyec" runat="server" AutoPostBack="true" OnTextChanged="txtBandejaProyec_TextChanged"
                                                                Width="40" MaxLength="10" />
                                                            de
                                                            <asp:Label ID="lblBandejaTotalP" runat="server" />
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
                        <div class="col-lg-4 col-md-12 col-sm-12 col-xs-12">
                            <div class="x_panel" runat="server" id="divProyecAdd">
                                <div class="x_title">
                                    <h2>Agregar Proyecto <small></small></h2>
                                    <ul class="nav navbar-right panel_toolbox">
                                      <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                                      </li>
                                    </ul>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                <div class="row">
                                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                                        <asp:Label runat="server" Text="Nombre del Proyecto" ></asp:Label>
                                                        <asp:RequiredFieldValidator ID="rfvNombreProyect" runat="server" ErrorMessage="Capturar un nombre del proyecto" Text="* " ControlToValidate="txtNombreProyect" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                                        <asp:TextBox runat="server" ID="txtNombreProyect" Width="100%" CssClass="form-control" ValidationGroup="vgAddProyecto"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                        <br />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                        <asp:UpdatePanel runat="server">
                                                            <ContentTemplate>
                                                                <asp:GridView ID="gridSitiosProyec" runat="server" AllowPaging="true" CssClass="table table-striped table-bordered" DataKeyNames="id_Sitio"
                                                                    PageSize="10" AutoGenerateColumns="false" OnRowDataBound="gridSitiosProyec_RowDataBound" Font-Size="10px" OnPageIndexChanging="gridSitiosProyec_PageIndexChanging"
                                                                    OnRowCommand="gridSitiosProyec_RowCommand"  EmptyDataText="No hay resultado bajo el criterio de búsqueda">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="id_Sitio"  HeaderText="ID" ReadOnly="true" />
                                                                        <asp:BoundField DataField="vchClaveSitio" HeaderText="Clave de Sitio" ReadOnly="true" />
                                                                        <asp:BoundField DataField="vchNombreSitio" HeaderText="Nombre de Sitio" ReadOnly="true" />
                                                                        <asp:TemplateField HeaderText="Seleccionar" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="chkSelect" runat="server" BackColor="Transparent"  Height="25px" Width="25px"
                                                                                    CommandArgument='<%#Eval("id_Sitio") %>' CommandName="Sitio" ToolTip="Agregar Sitio al Proyecto" />
                                                                            </ItemTemplate>
                                                                       </asp:TemplateField>
                                                                    </Columns>
                                                                    <PagerTemplate>
                                                                        <asp:Label ID="lblTemplate" runat="server" Text="Muestra Filas: " CssClass="Label" />
                                                                        <asp:DropDownList ID="ddlBandejaPS" runat="server" AutoPostBack="true" CausesValidation="false"
                                                                            Enabled="true" OnSelectedIndexChanged="ddlBandejaPS_SelectedIndexChanged">
                                                                                <asp:ListItem Value="10" />
                                                                                <asp:ListItem Value="15" />
                                                                                <asp:ListItem Value="20" />
                                                                                <asp:ListItem Value="25" />
                                                                        </asp:DropDownList>
                                                                        &nbsp;Página
                                                                        <asp:TextBox ID="txtBandejaPS" runat="server" AutoPostBack="true" OnTextChanged="txtBandejaPS_TextChanged"
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
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-right">
                                                        <asp:Button ID="btnLimpiar" runat="server" Text="Cancelar" class="btn btn-success" OnClick="btnLimpiar_Click" />
                                                        <asp:Button ID="btnAddProyecto" runat="server" Text="Agregar/Actualizar" ValidationGroup="vgAddProyecto" class="btn btn-success" OnClick="btnAddProyecto_Click" />
                                                    </div>
                                                </div>
                                            </div>
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
    <div class="row">
        <div class="x_panel">
            <div class="messagealert" id="alert_container"></div>
            <div class="x_title">
                <h2>Usuarios</h2>
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
                            <div class="col-lg-6 col-md-12 col-sm-12 col-xs-12">
                                <div class="x_panel" runat="server"  id="divUser">
                                    <div class="x_title">
                                        <h2>Usuarios<small></small></h2>
                                        <ul class="nav navbar-right panel_toolbox">
                                            <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                                            </li>
                                        </ul>
                                        <div class="clearfix"></div>
                                    </div>
                                    <div class="x_content">
                                         <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                             <div class="row">
                                                <div class="title_right">
                                                    <div class="col-md-5 col-sm-5 col-xs-12 form-group pull-right top_search">
                                                        <div class="input-group">
                                                            <asp:TextBox ID="txtBusqueda" CssClass="form-control" placeholder="Búsqueda..." runat="server" Width="100%" AutoPostBack="true" OnTextChanged="txtBusqueda_TextChanged" ></asp:TextBox>
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
                                                                    <asp:TemplateField ControlStyle-Width="5%">
                                                                        <ItemTemplate>
                                                                            <%# Convert.ToInt32(DataBinder.Eval(Container, "DataItemIndex")) + 1 %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="vchUsuario"  HeaderText="Usuario" ReadOnly="true" />
                                                                    <asp:BoundField DataField="vchNombre" HeaderText="Nombre" ReadOnly="true" />
                                                                    <asp:BoundField DataField="vchApellido" HeaderText="Apellidos" ReadOnly="true" />
                                                                    <asp:BoundField DataField="vchProyectoID" HeaderText="Proyecto" ReadOnly="true" />
                                                                    <asp:BoundField DataField="vchSitio" HeaderText="Sitio" ReadOnly="true" />
                                                                    <asp:TemplateField HeaderText="Editar">
                                                                        <ItemTemplate>
                                                                           
                                                                                <asp:LinkButton ID="btnVisualizar" CausesValidation="false" CommandName="viewEditar" CommandArgument='<%# Bind("intUsuarioID") %>' runat="server">
                                                                                    <asp:Image ID="ImageVisializa" runat="server" ImageUrl="~/images/ic_action_edit.png" Height="25px" Width="25px" ToolTip="Editar"/>
                                                                                </asp:LinkButton>
                                                                            
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Estatus" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="imbEstatus" runat="server" BackColor="Transparent"  Height="25px" Width="25px" 
                                                                                CommandArgument='<%#Eval("intUsuarioID") %>' CommandName="Estatus" ToolTip="Cambia el estatus del Usuario" />
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
                            <div class="col-lg-6 col-md-12 col-sm-12 col-xs-12">
                                <div class="x_panel" runat="server" id="divAddUser">
                                    <div class="x_title">
                                        <h2>Agregar/Editar Usuarios<small></small></h2>
                                        <ul class="nav navbar-right panel_toolbox">
                                            <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                                            </li>
                                        </ul>
                                        <div class="clearfix"></div>
                                    </div>
                                    <div class="x_content">
                                         <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                             <div class="row">
                                                <asp:UpdatePanel runat="server">
                                                    <ContentTemplate>
                                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                            <div class="row">
                                                                <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                                                                    <div class="row">
                                                                        <div class="col-md-10">
                                                                            <asp:Label runat="server" class="control-label  col-md-3" AssociatedControlID="txtNombre" Text="Nombre"></asp:Label>
                                                                        </div>
                                                                        <div class="col-md-2">
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="col-md-1" ValidationGroup="vdgUserCaptura" runat="server" ControlToValidate="txtNombre" Text="*" ForeColor="Red" ErrorMessage="Nombre requerido"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                                                    <asp:TextBox runat="server" type="text" id="txtNombre" class="form-control" TabIndex="1" ></asp:TextBox>
                                                                </div>
                                                                <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                                                                    <div class="row">
                                                                        <div class="col-md-10">
                                                                            <asp:Label runat="server" class="control-label col-md-4" AssociatedControlID="txtApePat" Text="Apellidos"></asp:Label>
                                                                        </div>
                                                                        <div class="col-md-2">
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="col-md-1" ValidationGroup="vdgUserCaptura" runat="server" ControlToValidate="txtApePat" Text="*" ForeColor="Red" ErrorMessage="Apellidos requerido"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                                                    <asp:Textbox type="text" ID="txtApePat" name="txtApePat" runat="server" class="form-control" TabIndex="2"></asp:Textbox>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                                                                    <div class="row">
                                                                        <div class="col-md-10">
                                                                            <asp:Label runat="server"  class="control-label col-md-12" Text="Tipo de Usuario" AssociatedControlID="ddlTipoUsuario"></asp:Label>
                                                                        </div>
                                                                        <div class="col-md-2">
                                                                            <asp:RequiredFieldValidator ID="rfvTipUser" runat="server" ControlToValidate="ddlTipoUsuario" Enabled="false"
                                                                            InitialValue="0" Text="*" ForeColor="Red" ErrorMessage="Seleccione un Tipo de Usuario"
                                                                            ValidationGroup="vdgUserCaptura">
                                                                            </asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                                                    <asp:DropDownList ID="ddlTipoUsuario" runat="server" CssClass="form-control col-md-12" AutoPostBack="true" TabIndex="3" OnSelectedIndexChanged="ddlTipoUsuario_SelectedIndexChanged"></asp:DropDownList>
                                                                </div>
                                                                <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                                                                    <div class="row">
                                                                        <div class="col-md-10">
                                                                            <asp:Label runat="server"  class="control-label col-md-12" Text="Proyecto" AssociatedControlID="ddlProyecto"></asp:Label>
                                                                        </div>
                                                                        <div class="col-md-2">
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlProyecto"
                                                                            InitialValue="0" Text="*" ForeColor="Red" ErrorMessage="Seleccione un Proyecto" Enabled="false"
                                                                            ValidationGroup="vdgUserCaptura">
                                                                            </asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                                                    <asp:DropDownList ID="ddlProyecto" runat="server" CssClass="form-control col-md-12" TabIndex="4" Enabled="false"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                                                                </div>
                                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                                                </div>
                                                                <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                                                                    <div class="row">
                                                                        <div class="col-md-10">
                                                                            <asp:Label runat="server"  class="control-label col-md-12" Text="Sitio" AssociatedControlID="ddlSitio"></asp:Label>
                                                                        </div>
                                                                        <div class="col-md-2">
                                                                            <asp:RequiredFieldValidator ID="rfvSitio" runat="server" ControlToValidate="ddlSitio"
                                                                            InitialValue="0" Text="*" ForeColor="Red" ErrorMessage="Seleccione un Proyecto" Enabled="false"
                                                                            ValidationGroup="vdgUserCaptura">
                                                                            </asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                                                    <asp:DropDownList ID="ddlSitio" runat="server" CssClass="form-control col-md-12" TabIndex="4" Enabled="false"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                                                                    <div class="row">
                                                                        <div class="col-md-10">
                                                                            <asp:Label runat="server"  class="control-label col-md-12" Text="Usuario" AssociatedControlID="txtUsuario"></asp:Label> 
                                                                        </div>
                                                                        <div class="col-md-2">
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="vdgUserCaptura" runat="server" ControlToValidate="txtUsuario" Text="*" ForeColor="Red" ErrorMessage="Usuario requerida"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                                                    <asp:TextBox id="txtUsuario" class="date-picker form-control col-md-12" runat="server" type="text" TabIndex="5"></asp:TextBox>
                                                                </div>
                                                                <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                                                                    <div class="row">
                                                                        <div class="col-md-10">
                                                                            <asp:Label runat="server" class="control-label col-md-12" Text="Contraseña" AssociatedControlID="txtPassword1" ></asp:Label>
                                                                        </div>
                                                                        <div class="col-md-2">
                                                                            
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                                                    <asp:TextBox ID="txtPassword1" TextMode="Password" class="form-control col-md-12" runat="server" type="text" MaxLength="20" TabIndex="6"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                                    <asp:RequiredFieldValidator ID="rfvPass" runat="server" ValidationGroup="vdgUserCaptura" ControlToValidate="txtPassword1" Text="*" ForeColor="Red" ErrorMessage="Contraseña requerida"></asp:RequiredFieldValidator>
                                                                    <asp:RegularExpressionValidator ID="rgVPass" Font-Bold="true" runat="server" ControlToValidate="txtPassword1" ValidationExpression=".{8}.*" ErrorMessage="Capturar al menos 8 caracteres" Text="* Capturar al menos 8 caracteres" ForeColor="Red" ValidationGroup="vdgUserCaptura"></asp:RegularExpressionValidator>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-right">
                                                                    <div class="row" >
                                                                        <asp:Button runat="server" ID="btnCancel" OnClick="btnCancel_Click" type="submit" Text="Cancelar" class="btn btn-primary" CausesValidation="false"></asp:Button>
                                                                        <asp:Button runat="server" ID="btnAddUser" OnClick="btnAddUser_Click" type="submit" Text="Agregar" ValidationGroup="vdgUserCaptura" class="btn btn-success"></asp:Button>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
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
                                        <h2><asp:Label Text="Sitio:" runat="server"></asp:Label><small><asp:Label runat="server" ID="lblIDSitio" ForeColor="Green"></asp:Label></small></h2>
                                     </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-body">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <div class="row">
                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                                        <asp:Label runat="server" ID="lblClave" Text="Clave de Sitio" AssociatedControlID="txtClaveSit"></asp:Label>
                                    </div>
                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 text-left">
                                        <asp:Label runat="server" ForeColor="Green" Font-Bold="true"  Text="" id="txtClaveSit" ></asp:Label>
                                    </div>
                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">

                                    </div>
                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 text-left">
                                        
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                                        <asp:Label runat="server" Text="Nombre del Sitio"></asp:Label>
                                    </div>
                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 text-left">
                                        <asp:Label runat="server" ID="txtNomSite" ForeColor="Green" Font-Bold="true"  Width="100%" Text="" ></asp:Label>
                                    </div>
                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                                        <asp:Label runat="server" Text="AETitle" ></asp:Label>
                                    </div>
                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 text-left">
                                        <asp:Label runat="server" ForeColor="Green" Font-Bold="true" Text="" Width="100%" ID="txtAETitle" ></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                                        <asp:Label runat="server" Text="IP Sitio"></asp:Label>
                                    </div>
                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 text-left">
                                        <asp:Label runat="server" ID="txtIPSite" Text="" Width="100%" ForeColor="Green" Font-Bold="true" ></asp:Label>
                                    </div>
                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                                        <asp:Label runat="server" Text="Mascara de Red"></asp:Label>
                                    </div>
                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 text-left">
                                        <asp:Label runat="server" ID="txtMaskSite" Text="" Width="100%" ForeColor="Green" Font-Bold="true" ></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                                        <asp:Label runat="server" Text="Puerto"></asp:Label>
                                    </div>
                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 text-left">
                                        <asp:Label runat="server" ID="txtPortSite" Width="100%" Text="" ForeColor="Green" Font-Bold="true" ></asp:Label>
                                    </div>
                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                                        <asp:Label runat="server" Text="Path"></asp:Label>
                                    </div>
                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 text-left">
                                        <asp:Label runat="server" ID="txtPath" Text="" Width="100%" ForeColor="Green" Font-Bold="true" ></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                        <h2><asp:Label Text="Servidor:" runat="server"></asp:Label></h2>
                                     </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                                        <asp:Label runat="server" Text="IP" ></asp:Label>
                                    </div>
                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 text-left">
                                        <asp:Label runat="server" ID="txtIPServer" Text="" Width="100%" ForeColor="Green" Font-Bold="true" ></asp:Label>
                                    </div>
                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                                        <asp:Label runat="server" Text="Puerto"></asp:Label>
                                    </div>
                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 text-left">
                                        <asp:Label runat="server" ID="txtPortServer" Text="" Width="100%" ForeColor="Green" Font-Bold="true" ></asp:Label>
                                    </div>
                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                                        <asp:Label runat="server" Text="AETitle"></asp:Label>
                                    </div>
                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 text-left">
                                        <asp:Label runat="server" ID="txtAETitleServer" Text="" Width="100%" ForeColor="Green" Font-Bold="true" ></asp:Label>
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


    <asp:HiddenField ID="hflURL" ClientIDMode="Static" runat="server" Value=""/>

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

        function openPopover() {
            try {
                $('[data-toggle="popover"]').popover();
            }
            catch (ew) {
            }
        }

        $(document).ready(function () {
            $('[data-toggle="popover"]').popover();
        });

    </script>

</asp:Content>
