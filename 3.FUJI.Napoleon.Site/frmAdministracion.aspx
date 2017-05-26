<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmAdministracion.aspx.cs" Inherits="_3.FUJI.Napoleon.Site.frmAdministracion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Bootstrap -->
    <script src="<%= ResolveClientUrl("~/vendors/jquery/dist/jquery.min.js")%>" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Timer ID="Timer1" runat="server" Interval="60000" OnTick="Timer1_Tick"></asp:Timer>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="x_panel">
                <div class="x_title">
                    <div class="messagealert" id="alert_container">
                    </div>
                    <h2>Estudios proceso Napoleón</h2>
                    <ul class="nav navbar-right panel_toolbox">
                      <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                      </li>
                    </ul>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">
                    <p class="text-muted font-13 m-b-30">
                      Administración de prioridades e historial de los estudios del proceso Napoleón.
                    </p>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    Búsqueda
                                </div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <asp:Label runat="server" ID="lblBusPrioridad" Text="Prioridad" AssociatedControlID="ddlBusPrioridad"></asp:Label>
                                            <asp:DropDownList ID="ddlBusPrioridad" runat="server" CssClass="form-control " >
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:Label runat="server" ID="lblBusEstatus" Text="Estatus" AssociatedControlID="ddlBusEstatus"></asp:Label>
                                            <asp:DropDownList ID="ddlBusEstatus" runat="server" CssClass="form-control " >
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:Label runat="server" ID="lblBusSucursal" Text="Sitio" AssociatedControlID="ddlBusSucursal"></asp:Label>
                                            <asp:DropDownList ID="ddlBusSucursal" runat="server" CssClass="form-control " >
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <asp:Label runat="server" ID="lblBusNumEstudio" Text="Num. Estudio" AssociatedControlID="txtBusNumEstudio"></asp:Label>
                                            <asp:TextBox ID="txtBusNumEstudio" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:Label runat="server" ID="lblBusNombre" Text="Nombre" AssociatedControlID="txtBusNombre"></asp:Label>
                                            <asp:TextBox ID="txtBusNombre" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:Label runat="server" ID="lblModalidad" Text="Modalidad" AssociatedControlID="ddlBusModalidad"></asp:Label>
                                            <asp:DropDownList ID="ddlBusModalidad" runat="server" CssClass="form-control" >
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-1">
                                            <div class="row text-right">
                                                <asp:Button runat="server" ID="btnBusquedaEst" Text="Buscar" CssClass="btn btnCenter btn-primary" OnClick="btnBusquedaEst_Click"/>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12  col-sm-12 col-xs-12">
                            <div class="row">
                                <div class="col-md-6">

                                </div>
                                <div class="col-md-6">
                                    <div class="pull-right">
                                        <asp:UpdatePanel runat="server" ID="updChecked" >
                                            <ContentTemplate>
                                                <asp:CheckBox runat="server" Checked="false" ID="chkAutomatico" Text="Autoguardado" AutoPostBack="true" OnCheckedChanged="chkAutomatico_CheckedChanged" />
                                                <asp:Button runat="server" ID="btnGuardarCambios" Text="Guardar Cambios" CssClass="btn btn-success btn-sm" OnClick="btnGuardarCambios_Click" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                            <asp:UpdatePanel ID="updGrid" runat="server">
                                <ContentTemplate>
                                    <asp:Panel runat="server" ID="pnlGrid">
                                        <asp:GridView ID="grvBusqueda" runat="server" AllowPaging="true" CssClass="table table-striped table-bordered"
                                            PageSize="10" AutoGenerateColumns="false" OnRowDataBound="grvBusqueda_RowDataBound" Font-Size="10px"
                                            OnPageIndexChanging="grvBusqueda_PageIndexChanging" DataKeyNames="vchAccessionNumber, intEstudioID"
                                            OnRowCommand="grvBusqueda_RowCommand"
                                            EmptyDataText="No hay resultado bajo el criterio de búsqueda.">
                                            <Columns>
                                                <asp:TemplateField ControlStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <%# Convert.ToInt32(DataBinder.Eval(Container, "DataItemIndex")) + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="intEstudioID" HeaderText="ID Estudio" ReadOnly="true" ItemStyle-CssClass="hidden-md" HeaderStyle-CssClass="hidden-md"/>
                                                <asp:BoundField DataField="vchAccessionNumber"  HeaderText="Num. de Estudio" ReadOnly="true" />
                                                <asp:BoundField DataField="vchModalidadID" HeaderText="Modalidades" ReadOnly="true" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                <asp:BoundField DataField="vchClaveSitio" HeaderText="Sitio" ReadOnly="true" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg"/>
                                                <asp:BoundField DataField="vchPatientBirthDate" HeaderText="Fecha Nacimiento" ReadOnly="true" ItemStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs" />
                                                <asp:BoundField DataField="PatientID" HeaderText="Folio Paciente" ReadOnly="true" HeaderStyle-CssClass="visible-md" ItemStyle-CssClass="visible-md"/>
                                                <asp:BoundField DataField="PatientName" HeaderText="Nombre" ReadOnly="true" />
                                                <asp:BoundField DataField="PacienteGenero" HeaderText="Genero" ReadOnly="true" HeaderStyle-CssClass="visible-md" ItemStyle-CssClass="visible-md"/>
                                                <asp:BoundField DataField="intNumeroArchivo" HeaderText="Num. de Archivos" ReadOnly="true" HeaderStyle-CssClass="hidden-md hidden-xs" ItemStyle-CssClass="hidden-md hidden-xs"/>
                                                <asp:BoundField DataField="intTamanoTotal" HeaderText="Tamaño Total Arc." ReadOnly="true" HeaderStyle-CssClass="hidden-md hidden-xs" ItemStyle-CssClass="hidden-md hidden-xs"/>
                                                
                                                <%--<asp:BoundField DataField="prioDescripcion" HeaderText="Prioridad" ReadOnly="true" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                <asp:BoundField DataField="priSecuencia" HeaderText="Secuencia" ReadOnly="true" HeaderStyle-CssClass="visible-lg" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="visible-lg" />
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                            <asp:LinkButton ID="btnUp"  CommandName="addPrioridad" CommandArgument='<%# Bind("intEstudioID") %>' runat="server">
                                                                <asp:Image ID="imgUP" runat="server" ImageUrl="~/Images/ic_action_arrow_top.png" Height="20px" Width="20px" ToolTip="Aumentar Prioridad"/>
                                                            </asp:LinkButton>
                                                            <asp:LinkButton ID="btnDown"  CommandName="lessPrioridad" CommandArgument='<%# Bind("intEstudioID") %>' runat="server">
                                                                <asp:Image ID="imgDown" runat="server" ImageUrl="~/Images/ic_action_arrow_bottom.png" Height="20px" Width="20px" ToolTip="Disminuir Prioridad"/>
                                                            </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Estatus">
                                                    <ItemTemplate>
                                                        <a runat="server" id="lblTooltip" title="Estatus Archivos" data-toggle="popover" data-trigger="hover" data-content="Some content" data-html="true"><asp:LinkButton CommandArgument='<%# Bind("vchAccessionNumber") %>' CommandName="EstatusArchivos" runat="server" ID="lblEstatus"></asp:LinkButton></a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Urgente">
                                                    <ItemTemplate>
                                                        <ceneter>
                                                            <asp:CheckBox runat="server" ID="ckhPrioridad" AutoPostBack="true" class="btn" Checked='<%# Eval("bitUrgente") %>' OnCheckedChanged="ckhPrioridad_CheckedChanged"/>
                                                        </ceneter>
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
            $(control).fadeTo(2000, 500).slideUp(500, function () {
                $(control).slideUp(700);
            });
        }

        $('input[type=radio]').change(function () {
            alert("test");
        });

        function openPopover() {
            try{
                $('[data-toggle="popover"]').popover();}
            catch(ew){
            }
        }

        $(document).ready(function(){
            $('[data-toggle="popover"]').popover();   
        });
    </script>

</asp:Content>
