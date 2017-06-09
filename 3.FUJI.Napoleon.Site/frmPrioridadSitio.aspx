<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmPrioridadSitio.aspx.cs" Inherits="_3.FUJI.Napoleon.Site.frmPrioridadSitio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="x_panel">
            <div class="messagealert" id="alert_containerPrioridad"></div>
            <p><asp:Label runat="server" Text="" ID="Label1"></asp:Label></p>
            <div class="x_title">
                <h2>Configuración de Prioridades</h2>
                <ul class="nav navbar-right panel_toolbox">
                    <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                    </li>
                </ul>
                <div class="clearfix"></div>
            </div>
            <div class="x_content">
                <div class="row">
                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                    </div>
                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                        <div class="row">
                            <div class="title_right">
                                <div class="col-md-5 col-sm-5 col-xs-12 form-group pull-right top_search">
                                    <asp:DropDownList runat="server" AutoPostBack="true" ID="ddlSitioPriridad" Width="100%" CssClass="form-control" OnSelectedIndexChanged="ddlSitioPriridad_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="panel">
                                <asp:UpdatePanel ID="updPrio" runat="server">
                                    <ContentTemplate>
                                        <asp:Panel runat="server" ID="pnlPrio">
                                            <asp:GridView ID="grvPrioridad" runat="server" AllowPaging="true" CssClass="table table-striped table-bordered"
                                                PageSize="15" AutoGenerateColumns="false" OnRowDataBound="grvPrioridad_RowDataBound" Font-Size="10px"
                                                OnPageIndexChanging="grvPrioridad_PageIndexChanging" OnRowCommand="grvPrioridad_RowCommand" DataKeyNames="intREL_SitioModID"
                                                EmptyDataText="No har resultado en la búsqueda">
                                                <Columns>
                                                    <asp:BoundField DataField="intREL_SitioModID"  HeaderText="ID" SortExpression="intREL_SitioModID" ReadOnly="true" />
                                                    <asp:BoundField DataField="vchModalidadID"  HeaderText="Modalidad" SortExpression="vchModalidadID" ReadOnly="true" />
                                                    <asp:BoundField DataField="vchSitio"  HeaderText="Sucursal" SortExpression="vchSitio" ReadOnly="true" />
                                                    <asp:BoundField DataField="intSecuencia" HeaderText="Secuencia" ItemStyle-HorizontalAlign="Center" ReadOnly="true"  SortExpression="intSecuencia" />
                                                    <asp:TemplateField HeaderText="Prioridad">
                                                        <ItemTemplate>
                                                            <div class="row text-center" style="width:100%">
                                                                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 text-left">
                                                                </div>
                                                                <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                                                                    <asp:CheckBox ID="chkRow" AutoPostBack="true" OnCheckedChanged="chkRow_CheckedChanged" CssClass="checkbox text-center" ToolTip="Agregar/Quitar prioridad" runat="server"  />
                                                                </div>
                                                                <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                                                                    <asp:LinkButton ID="btnUp"  CommandName="addPrioridad" CausesValidation="false" CommandArgument='<%# Bind("intREL_SitioModID") %>' runat="server">
                                                                        <asp:Image ID="imgUP" runat="server" ImageUrl="~/Images/ic_action_arrow_top.png" Height="20px" Width="20px" ToolTip="Aumentar Prioridad"/>
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                                                                    <asp:LinkButton ID="btnDown"  CommandName="lessPrioridad" CausesValidation="false" CommandArgument='<%# Bind("intREL_SitioModID") %>' runat="server">
                                                                        <asp:Image ID="imgDown" runat="server" ImageUrl="~/Images/ic_action_arrow_bottom.png" Height="20px" Width="20px" ToolTip="Disminuir Prioridad"/>
                                                                    </asp:LinkButton>
                                                                </div>                                                                               
                                                                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 text-left">
                                                                </div>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Estatus" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imbEstatusP" runat="server" BackColor="Transparent"  Height="25px" Width="25px" 
                                                                CommandArgument='<%#Eval("intREL_SitioModID") %>' CommandName="Estatus" ToolTip="Cambia el estatus del Usuario" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <PagerTemplate>
                                                            <asp:Label ID="lblTemplateP" runat="server" Text="Muestra Filas: " CssClass="Label" />
                                                            <asp:DropDownList ID="ddlBandejaP" runat="server" AutoPostBack="true" CausesValidation="false"
                                                                Enabled="true" OnSelectedIndexChanged="ddlBandejaP_SelectedIndexChanged">
                                                                    <asp:ListItem Value="15" />
                                                                    <asp:ListItem Value="20" />
                                                                    <asp:ListItem Value="25" />
                                                            </asp:DropDownList>
                                                            &nbsp;Página
                                                            <asp:TextBox ID="txtBandejaP" runat="server" AutoPostBack="true" OnTextChanged="txtBandejaP_TextChanged"
                                                                Width="40" MaxLength="10" />
                                                            de
                                                            <asp:Label ID="lblBandejaTotalP" runat="server" />
                                                            &nbsp;
                                                            <asp:Button ID="btnBandeja_IP" runat="server" CommandName="Page" CausesValidation="false"
                                                                ToolTip="Página Anterior" CommandArgument="Prev" CssClass="previous" />
                                                            <asp:Button ID="btnBandeja_IIP" runat="server" CommandName="Page" CausesValidation="false"
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
                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
