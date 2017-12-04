<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmGeneral.aspx.cs" Inherits="_3.FUJI.Napoleon.Site.frmGeneral" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="vendors/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet"/>
    <link href="Content/daterangepicker.css" rel="stylesheet" type="text/css" media="all" />
    <script type="text/javascript" src="vendors/jquery/dist/jquery.min.js"></script>
    <script type="text/javascript" src="vendors/bootstrap/dist/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="Content/moment.js"></script>
    <script type="text/javascript" src="Content/daterangepicker.js"></script>
    <!-- external libs from cdnjs -->
    <link rel="stylesheet" type="text/css" href="https://cdnjs.cloudflare.com/ajax/libs/c3/0.4.11/c3.min.css"/>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jqueryui/1.11.4/jquery-ui.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/d3/3.5.5/d3.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jqueryui-touch-punch/0.2.3/jquery.ui.touch-punch.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/c3/0.4.11/c3.min.js"></script>

    <!-- PivotTable.js libs from ../dist -->
    <link rel="stylesheet" type="text/css" href="vendors/pivot/dist/pivot.css"/>
    <script type="text/javascript" src="vendors/pivot/dist/pivot.js"></script>
    <script type="text/javascript" src="vendors/pivot/dist/c3_renderers.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="dashboard_graph">
                 <div class="row x_title">
                    <div class="col-md-6 col-sm-6 col-xs-6">
                        <h3>Resumen de Transacciones <small> Estadísticas</small></h3>
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-6">
                        <div id="reportrange" class="pull-right" style="background: #fff; cursor: pointer; padding: 5px 10px; border: 1px solid #ccc">
                            <i class="glyphicon glyphicon-calendar fa fa-calendar"></i>
                            <span>December 30, 2014 - January 28, 2015</span> <b class="caret"></b>
                        </div>
                        <div id="DivddlSucurs" class="pull-left">
                            <div>
                                <div class="col-md-12 col-sm-12 col-xs-12">
                                    <select id="ddlSucurs" onchange="cambioSucursal()" class="form-control">
                                    </select>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-12 col-sm-12 col-xs-12">
                    <!-- top tiles -->
                    <div class="row tile_count">
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6 tile_stats_count">
                            <center>
                            <span class="count_top"><i class="fa fa-folder"></i> Total Estudios Enviados </span>
                            <div class="count text-center"><span class="count" id="lblTotalEst">0</span></div>
                            </center>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6 tile_stats_count">
                            <center>
                            <span class="count_top"><i class="fa fa-clock-o"></i> Pendientes de Envío a Central</span>
                            <div class="count"><span class="count" id="lblPromEnvioD">0</span></div>
                            </center>
                        </div>
                        <%--<div class="col-lg-2 col-md-2 col-sm-4 col-xs-6 tile_stats_count">
                            <center>
                            <span class="count_top"><i class="fa fa-clock-o"></i> Pendientes de Envío C-Sy</span>
                            <div class="count"><span class="count" id="lblPenCSY">0</span></div>
                            </center>
                        </div>--%>
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6 tile_stats_count">
                            <center>
                            <span class="count_top"><i class="fa fa-file-text"></i> Tamaño Total Archivos</span>
                            <div class="count"><span class="count" id="lblPTamanoArcD">0</span></div>
                            </center>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6 tile_stats_count">
                            <center>
                            <span class="count_top"><i class="fa fa-user"></i> Prom. Archivos</span>
                            <div class="count"><span class="count" id="lblPNumArch">0</span></div>
                            </center>
                        </div>
                    </div>
                    <div class="row"  style="text-align: center;">
                        <div class="x_panel">
                            <div class="x_title">
                                <h2>Gráficas</h2>
                                <ul class="nav navbar-right panel_toolbox">
                                    <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                                    </li>
                                </ul>
                                <div class="clearfix"></div>
                            </div>
                            <div class="x_content">
                                <div class="row">
                                    <div class="col-md-4 col-sm-4 col-xs-12">
                                        <div class="x_panel">
                                          <div class="x_title">
                                            <h2>Total de Estudios<small>(Sucursales)</small></h2>
                                            <ul class="nav navbar-right panel_toolbox">
                                              <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                                              </li>
                                              <li><a class="close-link"><i class="fa fa-close"></i></a>
                                              </li>
                                            </ul>
                                            <div class="clearfix"></div>
                                          </div>
                                          <div class="x_content">
                                            <div id="dos" class="col-md-12 col-sm-12 col-xs-12">
                                                <canvas id="canvas1i2" height="200" width="200" style="margin: 5px 10px 10px 0"></canvas>
                                            </div>
                                          </div>
                                        </div>
                                      </div>
                                    <div class="col-md-4 col-sm-4 col-xs-12">
                                    <div class="x_panel">
                                        <div class="x_title">
                                        <h2>Pendientes de Envío a Central<small>(Sucursales)</small></h2>
                                        <ul class="nav navbar-right panel_toolbox">
                                            <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                                            </li>
                                            <li><a class="close-link"><i class="fa fa-close"></i></a>
                                            </li>
                                        </ul>
                                        <div class="clearfix"></div>
                                        </div>
                                        <div class="x_content">
                                        <div id="uno" class="col-md-12 col-sm-12 col-xs-12">
                                            <canvas id="myChart" height="200" width="200" style="margin: 5px 10px 10px 0"> </canvas>
                                        </div>
                                        </div>
                                    </div>
                                </div>
                                    <div class="col-md-4 col-sm-4 col-xs-12">
                                        <div class="x_panel">
                                            <div class="x_title">
                                                <h2>Velocidad Promedio<small>(Sucursales)</small></h2>
                                                    <ul class="nav navbar-right panel_toolbox">
                                                        <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                                                        </li>
                                                        <li><a class="close-link"><i class="fa fa-close"></i></a>
                                                        </li>
                                                    </ul>
                                                <div class="clearfix"></div>
                                            </div>
                                            <div class="x_content">
                                                <div id="tres" class="col-md-12 col-sm-12 col-xs-12">
                                                    <div id="echart_guage" style="height:370px;"></div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <%--<div class="row">
                                    <div class="col-md-4 col-sm-6 col-xs-12">
                                        <div class="x_panel">
                                          <div class="x_title">
                                            <h2>Pendientes de Envío C-Sy<small>(Sucursales)</small></h2>
                                            <ul class="nav navbar-right panel_toolbox">
                                              <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                                              </li>
                                              <li><a class="close-link"><i class="fa fa-close"></i></a>
                                              </li>
                                            </ul>
                                            <div class="clearfix"></div>
                                          </div>
                                          <div class="x_content" >
                                              <div id="cuatro" class="col-md-12 col-sm-12 col-xs-12">
                                                <canvas id="lineChart3" height="200" width="200" style="margin: 5px 10px 10px 0"></canvas>
                                              </div>
                                          </div>
                                        </div>
                                    </div>
                                    
                                </div>--%>
                            </div>
                        </div>
                    </div>
                    <div class="row" style="text-align: center;">
                        <div class="x_panel">
                            <div class="x_title">
                                <h2>Tabla Dinámica</h2>
                                <ul class="nav navbar-right panel_toolbox">
                                    <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                                    </li>
                                </ul>
                                <div class="clearfix"></div>
                            </div>
                            <div class="x_content">
                                <div class="row">
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                        <div id="tableDinamic" class="row">
                                            <div id="output" style="margin: 30px; overflow:scroll;"></div>
                                        </div>
                                        <div class="pull-right"><strong><span title="Exportar CSV" aria-hidden="true" ><i style="cursor:pointer;" class="fa fa-file-excel-o" onclick="return exportExcel();"></i>Exportar a CSV</span></strong></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="clearfix"></div>
            </div>
        </div>
    </div>    
    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
              <div class="dashboard_graph">
               
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hflURL" ClientIDMode="Static" runat="server" Value=""/>
    <asp:HiddenField ID="fechIni" ClientIDMode="Static" runat="server" Value="" />
    <asp:HiddenField ID="fechFin" ClientIDMode="Static" runat="server" Value="" />
    <asp:HiddenField ID="sucOID" ClientIDMode="Static" runat="server" Value="" />
    <asp:HiddenField ID="intProyectoID" ClientIDMode="Static" runat="server" Value="" />
    <asp:HiddenField ID="intUsuarioID" ClientIDMode="Static" runat="server" Value="" />
    <asp:HiddenField ID="intTipoUsuarioID" ClientIDMode="Static" runat="server" Value="" />
    <asp:Button runat="server"  style="display:none;" ID="btnActualizar" OnClick="btnActualizar_Click" ClientIDMode="Static"/>
    <asp:Button runat="server"  style="display:none;" ID="btnExport" OnClick="btnExport_Click" ClientIDMode="Static"/>

    <script type="text/javascript">
        function ShowMessage(message, messagetype) {
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
            $('#alert_container').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert fade in ' + cssclass + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>' + messagetype + '!</strong> <span>' + message + '</span></div>');
            $("#alert_div").fadeTo(2000, 500).slideUp(500, function () {
                $("#alert_div").slideUp(500);
            });
        }
        
        function actualizar()
        {
            document.getElementById("btnExport").click();
        }

        function exportExcel() {
            var url = document.getElementById('hflURL').value;
            var sucOID = document.getElementById("ddlSucurs").value;
            var datIni = $('#reportrange').data('daterangepicker').startDate;
            var datFin = $('#reportrange').data('daterangepicker').endDate;
            var jsonDataExport = JSON.stringify({
                FechaIncio: '/Date(' + datIni + ')/',
                FechaFin: '/Date(' + datFin + ')/',
                sucOID: sucOID
            });
            $.ajax({
                type: "POST",
                url: url + "/Services/NapoleonService.svc/json/getDatosTabla",
                data: jsonDataExport,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessExport_,
                error: OnErrorCall_
            });
        }

        function JSONToCSVConvertor(JSONData, ReportTitle, ShowLabel) {
            //If JSONData is not an object then JSON.parse will parse the JSON string in an Object
            var arrData = typeof JSONData != 'object' ? JSON.parse(JSONData) : JSONData;

            var CSV = '';
            //Set Report title in first row or line

            CSV += ReportTitle + '\r\n\n';

            //This condition will generate the Label/Header
            if (ShowLabel) {
                var row = "";

                //This loop will extract the label from 1st index of on array
                for (var index in arrData[0]) {

                    //Now convert each value to string and comma-seprated
                    if (index != '__type')
                        row += index + ',';
                }

                row = row.slice(0, -1);

                //append Label row with line break
                CSV += row + '\r\n';
            }

            //1st loop is to extract each row
            for (var i = 0; i < arrData.length; i++) {
                var row = "";

                //2nd loop will extract each column and convert it in string comma-seprated
                for (var index in arrData[i]) {
                    if (index != '__type')
                    row += '"' + arrData[i][index] + '",';
                }

                row.slice(0, row.length - 1);

                //add a line break after each row
                CSV += row + '\r\n';
            }

            if (CSV == '') {
                alert("Invalid data");
                return;
            }

            //Generate a file name
            var fileName = "Report_";
            //this will remove the blank-spaces from the title and replace it with an underscore
            fileName += ReportTitle.replace(/ /g, "_");

            //Initialize file format you want csv or xls
            var uri = 'data:text/csv;charset=utf-8,' + escape(CSV);

            // Now the little tricky part.
            // you can use either>> window.open(uri);
            // but this will not work in some browsers
            // or you will not get the correct file extension    

            //this trick will generate a temp <a /> tag
            var link = document.createElement("a");
            link.href = uri;

            //set the visibility hidden so it will not effect on your web-layout
            link.style = "visibility:hidden";
            link.download = fileName + ".csv";

            //this part will append the anchor tag and remove it after automatic click
            document.body.appendChild(link);
            link.click();
            document.body.removeChild(link);
        }

        function OnSuccessExport_(response) {
            var aData = response.d;
            var js = JSON.stringify(response.d);
            var today = new Date();
            var namefile = 'Para_' + today.getFullYear() + today.getMonth() + 1 + today.getDate() + '_' + today.getHours() + today.getMinutes();
            JSONToCSVConvertor(js, namefile, true);
        }
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            var currentdate = new Date();
            var url = document.getElementById('hflURL').value
            Date.prototype.toMSJSON = function () {
                var date = '/Date(' + moment().subtract(29, 'days') + ')/';
                return date;
            };
            var date3 = new Date().toMSJSON();
            var jsonData2 = JSON.stringify({
                tipo: "Sucursal"
            });
            var grafica3 = JSON.stringify({
                tipo: "Tiempos",
                FechaIncio: '/Date(' + moment().subtract(29, 'days') + ')/',
                FechaFin: '/Date(' + moment() + ')/'
            });

            //Total Estudios enviados
            var grafica2 = JSON.stringify({
                tipo: "Totales",
                FechaIncio: '/Date(' + moment().subtract(29, 'days') + ')/',
                FechaFin: '/Date(' + moment() + ')/',
                sucOID: document.getElementById('sucOID').value
            });
            $.ajax({
                type: "POST",
                url: url + "/Services/NapoleonService.svc/json/getDatosGraficas",
                data: grafica2,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessG2_,
                error: OnErrorCall_
            });

            //Pendientes S-C
            var jsonData = JSON.stringify({
                tipo: 'PSC',
                FechaIncio: '/Date(' + moment().subtract(29, 'days') + ')/',
                FechaFin: '/Date(' + moment() + ')/',
                sucOID: document.getElementById('sucOID').value
            });
            $.ajax({
                type: "POST",
                url: url + "/Services/NapoleonService.svc/json/getDatosGraficas",
                data: jsonData,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess_,
                error: OnErrorCall_
            });

            ////Pendientes C-SY
            //var grafica4 = JSON.stringify({
            //    tipo: "PCSY",
            //    FechaIncio: '/Date(' + moment().subtract(29, 'days') + ')/',
            //    FechaFin: '/Date(' + moment() + ')/',
            //    sucOID: document.getElementById('sucOID').value
            //});
            //$.ajax({
            //    type: "POST",
            //    url: url + "/Services/NapoleonService.svc/json/getDatosGraficas",
            //    data: grafica4,
            //    contentType: "application/json; charset=utf-8",
            //    dataType: "json",
            //    success: OnSuccessG4_,
            //    error: OnErrorCall_
            //});
            
        });

        function OnSuccessG2_(response) {
            if (response.d.length > 0) {
                var aData2 = response.d;
                var dataLabel2 = Array();
                var dataValue2 = Array();
                var dataColor2 = Array();
                var dataHover2 = Array();
                for (var i = 0; i < aData2.length; i++) {
                    dataLabel2.push(aData2[i]._Nombre);
                }
                for (var i = 0; i < aData2.length; i++) {
                    dataValue2.push(aData2[i]._Valor);
                }
                for (var i = 0; i < aData2.length; i++) {
                    dataColor2.push(aData2[i]._Color);
                }
                for (var i = 0; i < aData2.length; i++) {
                    dataHover2.push(aData2[i]._hoverColor);
                }
                var ctx2 = $("#canvas1i2").get(0).getContext("2d");
                var options = {
                    legend: false,
                    responsive: false
                };
                var data2 = {
                    labels: dataLabel2,
                    datasets: [{
                        data: dataValue2,
                        backgroundColor: dataColor2,
                        hoverBackgroundColor: dataHover2
                    }]
                };

                var canvasDoughnut2 = new Chart(ctx2, {
                    type: 'doughnut',
                    tooltipFillColor: "rgba(51, 51, 51, 0.55)",
                    data: data2,
                    options: options
                });
            }
            else
                $('#dos').append('<h2 id="SinDatos2">No existen datos.</h2>');
        }

        function OnSuccess_(response) {
            if (response.d.length > 0) {
                var aData = response.d;
                var dataLabel = Array();
                var dataValue = Array();
                var dataColor = Array();
                var dataHover = Array();
                for (var i = 0; i < aData.length; i++) {
                    dataLabel.push(aData[i]._Nombre);
                }
                for (var i = 0; i < aData.length; i++) {
                    dataValue.push(aData[i]._Valor);
                }
                for (var i = 0; i < aData.length; i++) {
                    dataColor.push(aData[i]._Color);
                }
                for (var i = 0; i < aData.length; i++) {
                    dataHover.push(aData[i]._hoverColor);
                }
                var aLabels = data;
                var ctx = $("#myChart").get(0).getContext("2d");
                var data = {
                    labels: dataLabel,
                    datasets: [{
                        data: dataValue,
                        backgroundColor: dataColor,
                        hoverBackgroundColor: dataHover
                    }]
                };
                var options = {
                    legend: false,
                    responsive: false
                };
                var canvasDoughnut = new Chart(ctx, {
                    type: 'doughnut',
                    tooltipFillColor: "rgba(51, 51, 51, 0.55)",
                    data: data,
                    options: options
                });
            }
            else
                $('#uno').append('<h2 id="SinDatos">No existen datos.</h2>');
        }

        function OnSuccessG4_(response) {
            if (response.d.length > 0) {
                var aData = response.d;
                var dataLabel = Array();
                var dataValue = Array();
                var dataColor = Array();
                var dataHover = Array();
                for (var i = 0; i < aData.length; i++) {
                    dataLabel.push(aData[i]._Nombre);
                }
                for (var i = 0; i < aData.length; i++) {
                    dataValue.push(aData[i]._Valor);
                }
                for (var i = 0; i < aData.length; i++) {
                    dataColor.push(aData[i]._Color);
                }
                for (var i = 0; i < aData.length; i++) {
                    dataHover.push(aData[i]._hoverColor);
                }
                var aLabels = data;
                var ctx = $("#lineChart3").get(0).getContext("2d");
                var data = {
                    labels: dataLabel,
                    datasets: [{
                        data: dataValue,
                        backgroundColor: dataColor,
                        hoverBackgroundColor: dataHover
                    }]
                };
                var options = {
                    legend: false,
                    responsive: false
                };
                var canvasDoughnut = new Chart(ctx, {
                    type: 'doughnut',
                    tooltipFillColor: "rgba(51, 51, 51, 0.55)",
                    data: data,
                    options: options
                });
            }
            else
                $('#cuatro').append('<h2 id="SinDatos4">No existen datos.</h2>');
        }

        function OnErrorCall_(response) {
            console.log(response)
        }

        function ClearCanvas() {
            $('#myChart').remove(); // this is my <canvas> element
            $('#uno').append('<canvas id="myChart" height="200" width="200" style="margin: 5px 10px 10px 0"> </canvas>');
            $('#SinDatos').remove();

            $('#canvas1i2').remove(); // this is my <canvas> element
            $('#dos').append('<canvas id="canvas1i2" height="200" width="200" style="margin: 5px 10px 10px 0"> </canvas>');
            $('#SinDatos2').remove();

            $('#echart_guage').remove(); // this is my <canvas> element
            $('#tres').append(' <div id="echart_guage" style="height:370px;"></div>');
            $('#SinDatosT2').remove();

            $('#lineChart3').remove(); // this is my <canvas> element
            $('#cuatro').append('<canvas id="lineChart3" height="200" width="200" style="margin: 5px 10px 10px 0"> </canvas>');
            $('#SinDatos4').remove();
        }

        function ClearTable() {
            $('#output').remove(); // this is my <canvas> element
            $('#SinDatosT').remove();
            $('#tableDinamic').append('<div id="output" style="margin: 30px; overflow:scroll;"></div>');
        }

        function cambioSucursal() {
            $('#output').remove(); // this is my <canvas> element
            $('#tableDinamic').append('<div id="output" style="margin: 30px; overflow:scroll;"></div>');
            $('#SinDatosT').remove();
            $('#myChart').remove(); // this is my <canvas> element
            $('#uno').append('<canvas id="myChart" height="200" width="200" style="margin: 5px 10px 10px 0"> </canvas>');
            $('#SinDatos').remove();
            $('#canvas1i2').remove(); // this is my <canvas> element
            $('#dos').append('<canvas id="canvas1i2" height="200" width="200" style="margin: 5px 10px 10px 0"> </canvas>');
            $('#SinDatos2').remove();
            $('#lineChart3').remove(); // this is my <canvas> element
            $('#cuatro').append('<canvas id="lineChart3" height="200" width="200" style="margin: 5px 10px 10px 0"> </canvas>');
            $('#SinDatos4').remove();


            var x = document.getElementById("ddlSucurs").value;
            var datIni = $('#reportrange').data('daterangepicker').startDate;
            var datFin = $('#reportrange').data('daterangepicker').endDate;
            var derivers = $.pivotUtilities.derivers;
            var url = document.getElementById('hflURL').value
            var renderers = $.extend(
                $.pivotUtilities.renderers,
                $.pivotUtilities.c3_renderers,
                $.pivotUtilities.d3_renderers,
                $.pivotUtilities.export_renderers
                );
            //var jsonInsertData = JSON.stringify({
            //    intUsuarioID: document.getElementById("intUsuarioID").value,
            //    sucOID: x
            //});
            //$.ajax({
            //    type: "POST",
            //    url: url + "/Services/NapoleonService.svc/json/setPerfilUsuario",
            //    data: jsonInsertData,
            //    contentType: "application/json; charset=utf-8",
            //    dataType: "json",
            //    success: OnSuccessPerfil_,
            //    error: OnErrorCall_
            //});

            var jsonData3 = JSON.stringify({
                FechaIncio: '/Date(' + datIni + ')/',
                FechaFin: '/Date(' + datFin + ')/',
                sucOID: x,
                intProyectoID: document.getElementById("intProyectoID").value
            });
            $.ajax({
                type: "POST",
                url: url + "/Services/NapoleonService.svc/json/getDatosTabla",
                data: jsonData3,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessDD2_,
                error: OnErrorCall_
            });
            var jsonData4 = JSON.stringify({
                fini: '/Date(' + datIni + ')/',
                ffin: '/Date(' + datFin + ')/',
                sucOID: x,
                intProyectoID: document.getElementById("intProyectoID").value
            });
            $.ajax({
                type: "POST",
                url: url + "/Services/NapoleonService.svc/json/getDatosTop",
                data: jsonData4,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessDDTop2_,
                error: OnErrorCall_
            });

            //Totales
            var grafica2 = JSON.stringify({
                tipo: "Totales",
                FechaIncio: '/Date(' + datIni + ')/',
                FechaFin: '/Date(' + datFin + ')/',
                sucOID: x,
                intProyectoID: document.getElementById("intProyectoID").value
            });
            $.ajax({
                type: "POST",
                url: url + "/Services/NapoleonService.svc/json/getDatosGraficas",
                data: grafica2,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessG2_,
                error: OnErrorCall_
            });

            //Pendientes S-C
            var jsonData = JSON.stringify({
                tipo: 'PSC',
                FechaIncio: '/Date(' + datIni + ')/',
                FechaFin: '/Date(' + datFin + ')/',
                sucOID: x,
                intProyectoID: document.getElementById("intProyectoID").value
            });
            $.ajax({
                type: "POST",
                url: url + "/Services/NapoleonService.svc/json/getDatosGraficas",
                data: jsonData,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess_,
                error: OnErrorCall_
            });

            ////Pendientes C-Sy
            //var grafica4 = JSON.stringify({
            //    tipo: "PCSY",
            //    FechaIncio: '/Date(' + datIni + ')/',
            //    FechaFin: '/Date(' + datFin + ')/',
            //    sucOID: x,
            //    intProyectoID: document.getElementById("intProyectoID").value
            //});
            //$.ajax({
            //    type: "POST",
            //    url: url + "/Services/NapoleonService.svc/json/getDatosGraficas",
            //    data: grafica4,
            //    contentType: "application/json; charset=utf-8",
            //    dataType: "json",
            //    success: OnSuccessG4_,
            //    error: OnErrorCall_
            //});

            //function OnSuccessPerfil_(response) {
            //    var data = response.d;

            //}

            function OnSuccessDD2_(response) {
                if (response.d.length > 0) {
                    var aData = response.d;
                    for (var i = 0; i < aData.length; i++) {
                        delete aData[i]["__type"];
                    }
                    $("#output").pivotUI(aData, {
                        renderers: renderers,
                        rendererOptions: {
                            c3: {
                                size: { height: 300, width: 300 }
                            }
                        },
                        cols: ["Modalidad"], rows: ["Sucursal"],
                        rendererName: "Table"
                    });
                }
                else
                    $('#tableDinamic').append('<h2 id="SinDatosT">No existen datos.</h2>');
            }

            function OnSuccessDDTop2_(response) {
                var aData = response.d;
                document.getElementById("lblTotalEst").innerHTML = aData.TotalEstEnviados;
                document.getElementById("lblPromEnvioD").innerHTML = aData.PendientesEnvSC;
                //document.getElementById("lblPenCSY").innerHTML = aData.PendientesEnvCSy;
                document.getElementById("lblPTamanoArcD").innerHTML = aData.TamañoTotalArc;
                document.getElementById("lblPNumArch").innerHTML = aData.PromedioArchivos;
            }

            var theme = {
                color: [
                    '#26B99A', '#34495E', '#BDC3C7', '#3498DB',
                    '#9B59B6', '#8abb6f', '#759c6a', '#bfd3b7'
                ],

                title: {
                    itemGap: 8,
                    textStyle: {
                        fontWeight: 'normal',
                        color: '#408829'
                    }
                },

                dataRange: {
                    color: ['#1f610a', '#97b58d']
                },

                toolbox: {
                    color: ['#408829', '#408829', '#408829', '#408829']
                },

                tooltip: {
                    backgroundColor: 'rgba(0,0,0,0.5)',
                    axisPointer: {
                        type: 'line',
                        lineStyle: {
                            color: '#408829',
                            type: 'dashed'
                        },
                        crossStyle: {
                            color: '#408829'
                        },
                        shadowStyle: {
                            color: 'rgba(200,200,200,0.3)'
                        }
                    }
                },

                dataZoom: {
                    dataBackgroundColor: '#eee',
                    fillerColor: 'rgba(64,136,41,0.2)',
                    handleColor: '#408829'
                },
                grid: {
                    borderWidth: 0
                },

                categoryAxis: {
                    axisLine: {
                        lineStyle: {
                            color: '#408829'
                        }
                    },
                    splitLine: {
                        lineStyle: {
                            color: ['#eee']
                        }
                    }
                },

                valueAxis: {
                    axisLine: {
                        lineStyle: {
                            color: '#408829'
                        }
                    },
                    splitArea: {
                        show: true,
                        areaStyle: {
                            color: ['rgba(250,250,250,0.1)', 'rgba(200,200,200,0.1)']
                        }
                    },
                    splitLine: {
                        lineStyle: {
                            color: ['#eee']
                        }
                    }
                },
                timeline: {
                    lineStyle: {
                        color: '#408829'
                    },
                    controlStyle: {
                        normal: { color: '#408829' },
                        emphasis: { color: '#408829' }
                    }
                },

                k: {
                    itemStyle: {
                        normal: {
                            color: '#68a54a',
                            color0: '#a9cba2',
                            lineStyle: {
                                width: 1,
                                color: '#408829',
                                color0: '#86b379'
                            }
                        }
                    }
                },
                map: {
                    itemStyle: {
                        normal: {
                            areaStyle: {
                                color: '#ddd'
                            },
                            label: {
                                textStyle: {
                                    color: '#c12e34'
                                }
                            }
                        },
                        emphasis: {
                            areaStyle: {
                                color: '#99d2dd'
                            },
                            label: {
                                textStyle: {
                                    color: '#c12e34'
                                }
                            }
                        }
                    }
                },
                force: {
                    itemStyle: {
                        normal: {
                            linkStyle: {
                                strokeColor: '#408829'
                            }
                        }
                    }
                },
                chord: {
                    padding: 4,
                    itemStyle: {
                        normal: {
                            lineStyle: {
                                width: 1,
                                color: 'rgba(128, 128, 128, 0.5)'
                            },
                            chordStyle: {
                                lineStyle: {
                                    width: 1,
                                    color: 'rgba(128, 128, 128, 0.5)'
                                }
                            }
                        },
                        emphasis: {
                            lineStyle: {
                                width: 1,
                                color: 'rgba(128, 128, 128, 0.5)'
                            },
                            chordStyle: {
                                lineStyle: {
                                    width: 1,
                                    color: 'rgba(128, 128, 128, 0.5)'
                                }
                            }
                        }
                    }
                },
                gauge: {
                    startAngle: 225,
                    endAngle: -45,
                    axisLine: {
                        show: true,
                        lineStyle: {
                            color: [[0.2, '#86b379'], [0.8, '#68a54a'], [1, '#408829']],
                            width: 8
                        }
                    },
                    axisTick: {
                        splitNumber: 10,
                        length: 12,
                        lineStyle: {
                            color: 'auto'
                        }
                    },
                    axisLabel: {
                        textStyle: {
                            color: 'auto'
                        }
                    },
                    splitLine: {
                        length: 18,
                        lineStyle: {
                            color: 'auto'
                        }
                    },
                    pointer: {
                        length: '90%',
                        color: 'auto'
                    },
                    title: {
                        textStyle: {
                            color: '#333'
                        }
                    },
                    detail: {
                        textStyle: {
                            color: 'auto'
                        }
                    }
                },
                textStyle: {
                    fontFamily: 'Arial, Verdana, sans-serif'
                }
            };
            var echartGauge = echarts.init(document.getElementById('echart_guage'), theme);
            var url = document.getElementById('hflURL').value;
            var varibales = JSON.stringify({
                FechaIncio: '/Date(' + datIni + ')/',
                FechaFin: '/Date(' + datFin + ')/',
                sucOID: x,
                intProyectoID: document.getElementById("intProyectoID").value
            });
            $.ajax({
                type: "POST",
                url: url + "/Services/NapoleonService.svc/json/getPromedioEnvio",
                data: varibales,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessProm_,
                error: OnErrorCall_
            });

            function OnSuccessProm_(response) {
                if (response.d.length > 0) {
                    var aDataProm = parseFloat(response.d).toFixed(2);
                    if (aDataProm != 'NaN') {
                        var minutos = parseInt(parseInt(aDataProm) / 60);
                        var Tope = parseInt(parseInt(minutos) + (parseInt(minutos) * .4));
                        var seg1 = parseFloat(((Tope / 10) * 2)).toFixed(1);
                        var seg2 = parseFloat(((Tope / 10) * 4)).toFixed(1);
                        var seg3 = parseFloat(((Tope / 10) * 7)).toFixed(1);
                        var seg4 = parseFloat((Tope / 10) * 9).toFixed(1);
                        aDataProm = parseInt(aDataProm);
                        echartGauge.setOption({
                            tooltip: {
                                formatter: "{a} <br/>{b} : {c}m"
                            },
                            toolbox: {
                                show: true,
                                feature: {
                                    restore: {
                                        show: false,
                                        title: "Restore"
                                    }
                                }
                            },
                            series: [{
                                name: 'Desempeño',
                                type: 'gauge',
                                center: ['50%', '50%'],
                                startAngle: 140,
                                endAngle: -140,
                                min: 0,
                                max: Tope,
                                precision: 0,
                                splitNumber: 10,
                                axisLine: {
                                    show: true,
                                    lineStyle: {
                                        color: [
                                          [0.2, 'lightgreen'],
                                          [0.4, 'orange'],
                                          [0.7, 'skyblue'],
                                          [1, '#1ABB9C']
                                        ],
                                        width: 30
                                    }
                                },
                                axisTick: {
                                    show: true,
                                    splitNumber: 5,
                                    length: 8,
                                    lineStyle: {
                                        color: '#eee',
                                        width: 1,
                                        type: 'solid'
                                    }
                                },
                                axisLabel: {
                                    show: true,
                                    formatter: function (v) {
                                        switch (v + '') {
                                            case seg1+ '':
                                                return seg1;
                                            case seg2+ '':
                                                return seg2;
                                            case seg3+ '':
                                                return seg3;
                                            case seg4+ '':
                                                return seg4;
                                            default:
                                                return '';
                                        }
                                    },
                                    textStyle: {
                                        color: '#333'
                                    }
                                },
                                splitLine: {
                                    show: true,
                                    length: 30,
                                    lineStyle: {
                                        color: '#eee',
                                        width: 2,
                                        type: 'solid'
                                    }
                                },
                                pointer: {
                                    length: '80%',
                                    width: 8,
                                    color: 'auto'
                                },
                                title: {
                                    show: true,
                                    offsetCenter: ['-65%', -10],
                                    textStyle: {
                                        color: '#333',
                                        fontSize: 15
                                    }
                                },
                                detail: {
                                    show: true,
                                    backgroundColor: 'rgba(0,0,0,0)',
                                    borderWidth: 0,
                                    borderColor: '#ccc',
                                    width: 100,
                                    height: 40,
                                    offsetCenter: ['-60%', 10],
                                    formatter: '{value}m',
                                    textStyle: {
                                        color: 'auto',
                                        fontSize: 30
                                    }
                                },
                                data: [{
                                    value: minutos,
                                    name: 'Desempeño'
                                }]
                            }]
                        });
                    }
                }
            }
        }
        //*
    </script>

    <!-- bootstrap-daterangepicker -->
    <script>
      $(document).ready(function() {

        var cb = function(start, end, label) {
          console.log(start.toISOString(), end.toISOString(), label);
          $('#reportrange span').html(start.format('DD/MM/YYYY') + ' - ' + end.format('DD/MM/YYYY'));
        };

        var optionSet1 = {
          startDate: moment().subtract(29, 'days'),
          endDate: moment(),
          minDate: '01/01/2015',
          maxDate: '12/31/2018',
          dateLimit: {
            days: 10000
          },
          showDropdowns: true,
          showWeekNumbers: true,
          timePicker: false,
          timePickerIncrement: 1,
          timePicker12Hour: true,
          ranges: {
            'Hoy': [moment(), moment()],
            'Ayer': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
            'Últimos 7 días': [moment().subtract(6, 'days'), moment()],
            'Últimos 30 días': [moment().subtract(29, 'days'), moment()],
            'Este mes': [moment().startOf('month'), moment().endOf('month')],
            'Último mes': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
          },
          opens: 'left',
          buttonClasses: ['btn btn-default'],
          applyClass: 'btn-small btn-primary',
          cancelClass: 'btn-small',
          format: 'DD/MM/YYYY',
          separator: ' a ',
          locale: {
            applyLabel: 'Ir',
            cancelLabel: 'Limpiar',
            fromLabel: 'De',
            toLabel: 'Hasta',
            customRangeLabel: 'Elegir',
            daysOfWeek: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sa'],
            monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Deciembre'],
            firstDay: 1
          }
        };
        $('#reportrange span').html(moment().subtract(29, 'days').format('DD/MM/YYYY') + ' - ' + moment().format('DD/MM/YYYY'));
        $('#reportrange').daterangepicker(optionSet1, cb);
        $('#reportrange').on('show.daterangepicker', function() {
            console.log("show event fired");
        });
        $('#reportrange').on('hide.daterangepicker', function() {
          console.log("hide event fired");
        });
        $('#reportrange').on('apply.daterangepicker', function (ev, picker) {
            ClearCanvas();
            ClearTable();
            var url = document.getElementById('hflURL').value;
            var datIni = '/Date(' + picker.startDate + ')/';
            var datFin = '/Date(' + picker.endDate + ')/';
            var SucOIDVal = document.getElementById("ddlSucurs").value;

            //Totales
            var grafica2 = JSON.stringify({
                tipo: "Totales",
                FechaIncio: datIni,
                FechaFin: datFin,
                sucOID: SucOIDVal,
                intProyectoID : document.getElementById("intProyectoID").value
            });
            $.ajax({
                type: "POST",
                url: url + "/Services/NapoleonService.svc/json/getDatosGraficas",
                data: grafica2,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessG2_,
                error: OnErrorCall_
            });

            //Pendientes S-C
            var jsonData = JSON.stringify({
                tipo: 'PSC',
                FechaIncio: datIni,
                FechaFin: datFin,
                sucOID: SucOIDVal,
                intProyectoID: document.getElementById("intProyectoID").value
            });
            $.ajax({
                type: "POST",
                url: url + "/Services/NapoleonService.svc/json/getDatosGraficas",
                data: jsonData,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess_,
                error: OnErrorCall_
            });

            ////Pendientes C-Sy
            //var grafica4 = JSON.stringify({
            //    tipo: "PCSY",
            //    FechaIncio: datIni,
            //    FechaFin: datFin,
            //    sucOID: SucOIDVal,
            //    intProyectoID: document.getElementById("intProyectoID").value
            //});
            //$.ajax({
            //    type: "POST",
            //    url: url + "/Services/NapoleonService.svc/json/getDatosGraficas",
            //    data: grafica4,
            //    contentType: "application/json; charset=utf-8",
            //    dataType: "json",
            //    success: OnSuccessG4_,
            //    error: OnErrorCall_
            //});
            var derivers = $.pivotUtilities.derivers;
            var url = document.getElementById('hflURL').value
            var renderers = $.extend(
                $.pivotUtilities.renderers,
                $.pivotUtilities.c3_renderers,
                $.pivotUtilities.d3_renderers,
                $.pivotUtilities.export_renderers
                );
            var jsonData3 = JSON.stringify({
                FechaIncio: datIni,
                FechaFin: datFin,
                sucOID: SucOIDVal,
                intProyectoID: document.getElementById("intProyectoID").value
            });
            $.ajax({
                type: "POST",
                url: url + "/Services/NapoleonService.svc/json/getDatosTabla",
                data: jsonData3,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessDD2_,
                error: OnErrorCall_
            });
            var jsonData4 = JSON.stringify({
                fini: datIni,
                ffin: datFin,
                sucOID: SucOIDVal,
                intProyectoID: document.getElementById("intProyectoID").value
            });
            $.ajax({
                type: "POST",
                url: url + "/Services/NapoleonService.svc/json/getDatosTop",
                data: jsonData4,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessDDTop2_,
                error: OnErrorCall_
            });

            function OnSuccessDD2_(response) {
                if (response.d.length > 0) {
                    var aData = response.d;
                    for (var i = 0; i < aData.length; i++) {
                        delete aData[i]["__type"];
                    }
                    $("#output").pivotUI(aData, {
                        renderers: renderers,
                        rendererOptions: {
                            c3: {
                                size: { height: 300, width: 300 }
                            }
                        },
                        cols: ["Modalidad"], rows: ["Sucursal"],
                        rendererName: "Table"
                    });
                }
                else
                    $('#tableDinamic').append('<h2 id="SinDatosT">No existen datos.</h2>');
            }

            function OnSuccessDDTop2_(response) {
                var aData = response.d;
                document.getElementById("lblTotalEst").innerHTML= aData.TotalEstEnviados;
                document.getElementById("lblPromEnvioD").innerHTML= aData.PendientesEnvSC;
                //document.getElementById("lblPenCSY").innerHTML= aData.PendientesEnvCSy;
                document.getElementById("lblPTamanoArcD").innerHTML= aData.TamañoTotalArc;
                document.getElementById("lblPNumArch").innerHTML= aData.PromedioArchivos;
            }

            var theme = {
                color: [
                    '#26B99A', '#34495E', '#BDC3C7', '#3498DB',
                    '#9B59B6', '#8abb6f', '#759c6a', '#bfd3b7'
                ],

                title: {
                    itemGap: 8,
                    textStyle: {
                        fontWeight: 'normal',
                        color: '#408829'
                    }
                },

                dataRange: {
                    color: ['#1f610a', '#97b58d']
                },

                toolbox: {
                    color: ['#408829', '#408829', '#408829', '#408829']
                },

                tooltip: {
                    backgroundColor: 'rgba(0,0,0,0.5)',
                    axisPointer: {
                        type: 'line',
                        lineStyle: {
                            color: '#408829',
                            type: 'dashed'
                        },
                        crossStyle: {
                            color: '#408829'
                        },
                        shadowStyle: {
                            color: 'rgba(200,200,200,0.3)'
                        }
                    }
                },

                dataZoom: {
                    dataBackgroundColor: '#eee',
                    fillerColor: 'rgba(64,136,41,0.2)',
                    handleColor: '#408829'
                },
                grid: {
                    borderWidth: 0
                },

                categoryAxis: {
                    axisLine: {
                        lineStyle: {
                            color: '#408829'
                        }
                    },
                    splitLine: {
                        lineStyle: {
                            color: ['#eee']
                        }
                    }
                },

                valueAxis: {
                    axisLine: {
                        lineStyle: {
                            color: '#408829'
                        }
                    },
                    splitArea: {
                        show: true,
                        areaStyle: {
                            color: ['rgba(250,250,250,0.1)', 'rgba(200,200,200,0.1)']
                        }
                    },
                    splitLine: {
                        lineStyle: {
                            color: ['#eee']
                        }
                    }
                },
                timeline: {
                    lineStyle: {
                        color: '#408829'
                    },
                    controlStyle: {
                        normal: { color: '#408829' },
                        emphasis: { color: '#408829' }
                    }
                },

                k: {
                    itemStyle: {
                        normal: {
                            color: '#68a54a',
                            color0: '#a9cba2',
                            lineStyle: {
                                width: 1,
                                color: '#408829',
                                color0: '#86b379'
                            }
                        }
                    }
                },
                map: {
                    itemStyle: {
                        normal: {
                            areaStyle: {
                                color: '#ddd'
                            },
                            label: {
                                textStyle: {
                                    color: '#c12e34'
                                }
                            }
                        },
                        emphasis: {
                            areaStyle: {
                                color: '#99d2dd'
                            },
                            label: {
                                textStyle: {
                                    color: '#c12e34'
                                }
                            }
                        }
                    }
                },
                force: {
                    itemStyle: {
                        normal: {
                            linkStyle: {
                                strokeColor: '#408829'
                            }
                        }
                    }
                },
                chord: {
                    padding: 4,
                    itemStyle: {
                        normal: {
                            lineStyle: {
                                width: 1,
                                color: 'rgba(128, 128, 128, 0.5)'
                            },
                            chordStyle: {
                                lineStyle: {
                                    width: 1,
                                    color: 'rgba(128, 128, 128, 0.5)'
                                }
                            }
                        },
                        emphasis: {
                            lineStyle: {
                                width: 1,
                                color: 'rgba(128, 128, 128, 0.5)'
                            },
                            chordStyle: {
                                lineStyle: {
                                    width: 1,
                                    color: 'rgba(128, 128, 128, 0.5)'
                                }
                            }
                        }
                    }
                },
                gauge: {
                    startAngle: 225,
                    endAngle: -45,
                    axisLine: {
                        show: true,
                        lineStyle: {
                            color: [[0.2, '#86b379'], [0.8, '#68a54a'], [1, '#408829']],
                            width: 8
                        }
                    },
                    axisTick: {
                        splitNumber: 10,
                        length: 12,
                        lineStyle: {
                            color: 'auto'
                        }
                    },
                    axisLabel: {
                        textStyle: {
                            color: 'auto'
                        }
                    },
                    splitLine: {
                        length: 18,
                        lineStyle: {
                            color: 'auto'
                        }
                    },
                    pointer: {
                        length: '90%',
                        color: 'auto'
                    },
                    title: {
                        textStyle: {
                            color: '#333'
                        }
                    },
                    detail: {
                        textStyle: {
                            color: 'auto'
                        }
                    }
                },
                textStyle: {
                    fontFamily: 'Arial, Verdana, sans-serif'
                }
            };
            var echartGauge = echarts.init(document.getElementById('echart_guage'), theme);
            var url = document.getElementById('hflURL').value;
            var varibales = JSON.stringify({
                FechaIncio: datIni,
                FechaFin: datFin,
                sucOID: SucOIDVal,
                intProyectoID: document.getElementById("intProyectoID").value
            });
            $.ajax({
                type: "POST",
                url: url + "/Services/NapoleonService.svc/json/getPromedioEnvio",
                data: varibales,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessProm_,
                error: OnErrorCall_
            });

            function OnSuccessProm_(response) {
                if (response.d.length > 0) {
                    var aDataProm = parseFloat(response.d).toFixed(2);
                    if (aDataProm != 'NaN') {
                        var minutos = parseInt(parseInt(aDataProm) / 60);
                        var Tope = parseInt(parseInt(minutos) + (parseInt(minutos) * .4));
                        var seg1 = parseFloat(((Tope / 10) * 2)).toFixed(1);
                        var seg2 = parseFloat(((Tope / 10) * 4)).toFixed(1);
                        var seg3 = parseFloat(((Tope / 10) * 7)).toFixed(1);
                        var seg4 = parseFloat((Tope / 10) * 9).toFixed(1);
                        aDataProm = parseInt(aDataProm);
                        echartGauge.setOption({
                            tooltip: {
                                formatter: "{a} <br/>{b} : {c}m"
                            },
                            toolbox: {
                                show: true,
                                feature: {
                                    restore: {
                                        show: false,
                                        title: "Restore"
                                    }
                                }
                            },
                            series: [{
                                name: 'Desempeño',
                                type: 'gauge',
                                center: ['50%', '50%'],
                                startAngle: 140,
                                endAngle: -140,
                                min: 0,
                                max: Tope,
                                precision: 0,
                                splitNumber: 10,
                                axisLine: {
                                    show: true,
                                    lineStyle: {
                                        color: [
                                          [0.2, 'lightgreen'],
                                          [0.4, 'orange'],
                                          [0.7, 'skyblue'],
                                          [1, '#1ABB9C']
                                        ],
                                        width: 30
                                    }
                                },
                                axisTick: {
                                    show: true,
                                    splitNumber: 5,
                                    length: 8,
                                    lineStyle: {
                                        color: '#eee',
                                        width: 1,
                                        type: 'solid'
                                    }
                                },
                                axisLabel: {
                                    show: true,
                                    formatter: function (v) {
                                        switch (v + '') {
                                            case seg1+ '':
                                                return seg1;
                                            case seg2+ '':
                                                return seg2;
                                            case seg3+ '':
                                                return seg3;
                                            case seg4+ '':
                                                return seg4;
                                            default:
                                                return '';
                                        }
                                    },
                                    textStyle: {
                                        color: '#333'
                                    }
                                },
                                splitLine: {
                                    show: true,
                                    length: 30,
                                    lineStyle: {
                                        color: '#eee',
                                        width: 2,
                                        type: 'solid'
                                    }
                                },
                                pointer: {
                                    length: '80%',
                                    width: 8,
                                    color: 'auto'
                                },
                                title: {
                                    show: true,
                                    offsetCenter: ['-65%', -10],
                                    textStyle: {
                                        color: '#333',
                                        fontSize: 15
                                    }
                                },
                                detail: {
                                    show: true,
                                    backgroundColor: 'rgba(0,0,0,0)',
                                    borderWidth: 0,
                                    borderColor: '#ccc',
                                    width: 100,
                                    height: 40,
                                    offsetCenter: ['-60%', 10],
                                    formatter: '{value}m',
                                    textStyle: {
                                        color: 'auto',
                                        fontSize: 30
                                    }
                                },
                                data: [{
                                    value: minutos,
                                    name: 'Desempeño'
                                }]
                            }]
                        });
                    }
                }
            }
            //document.getElementById("fechIni").value = picker.startDate.format('DD/MM/YYYY');
            //document.getElementById("fechFin").value = picker.endDate.format('DD/MM/YYYY');
            //actualizar();
          console.log("apply event fired, start/end dates are " + picker.startDate.format('MMMM D, YYYY') + " a " + picker.endDate.format('MMMM D, YYYY'));
        });
        $('#reportrange').on('cancel.daterangepicker', function(ev, picker) {
          console.log("cancel event fired");
        });
        $('#options1').click(function() {
          $('#reportrange').data('daterangepicker').setOptions(optionSet1, cb);
        });
        $('#options2').click(function() {
          $('#reportrange').data('daterangepicker').setOptions(optionSet2, cb);
        });
        $('#destroy').click(function() {
          $('#reportrange').data('daterangepicker').remove();
        });
        $(function () {
            var derivers = $.pivotUtilities.derivers;
            var url = document.getElementById('hflURL').value
            var renderers = $.extend(
                $.pivotUtilities.renderers,
                $.pivotUtilities.c3_renderers,
                $.pivotUtilities.d3_renderers,
                $.pivotUtilities.export_renderers
                );
            var date = new Date().toMSJSON();
            var jsonDataX = JSON.stringify({
                FechaIncio: '/Date(' + moment().subtract(29, 'days') + ')/',
                FechaFin: '/Date(' + moment() + ')/',
                sucOID: document.getElementById('sucOID').value,
                intProyectoID : document.getElementById("intProyectoID").value
            });

            $.ajax({
                type: "POST",
                url: url + "/Services/NapoleonService.svc/json/getDatosTabla",
                data: jsonDataX,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessDD_,
                error: OnErrorCall_
            });
            var jsonDataTop = JSON.stringify({
                fini: '/Date(' + moment().subtract(29, 'days') + ')/',
                ffin: '/Date(' + moment() + ')/',
                sucOID: document.getElementById('sucOID').value,
                intProyectoID: document.getElementById("intProyectoID").value
            });
            $.ajax({
                type: "POST",
                url: url + "/Services/NapoleonService.svc/json/getDatosTop",
                data: jsonDataTop,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessDDTop_,
                error: OnErrorCall_
            });

            

            function OnSuccessDD_(response) {
                if (response.d.length > 0) {
                    var aData = response.d;
                    for (var i = 0; i < aData.length; i++) {
                        delete aData[i]["__type"];
                    }
                    $("#output").pivotUI(aData, {
                        renderers: renderers,
                        rendererOptions: {
                            c3: {
                                size: { height: 300, width: 300 }
                            }
                        },
                        cols: ["Modalidad"], rows: ["Sucursal"],
                        rendererName: "Table"
                    });
                }
                else
                    $('#tableDinamic').append('<h2 id="SinDatosT">No existen datos.</h2>');
            }

            function OnSuccessDDTop_(response) {
                var aData = response.d;
                document.getElementById("lblTotalEst").innerHTML= aData.TotalEstEnviados;
                document.getElementById("lblPromEnvioD").innerHTML= aData.PendientesEnvSC;
                //document.getElementById("lblPenCSY").innerHTML= aData.PendientesEnvCSy;
                document.getElementById("lblPTamanoArcD").innerHTML= aData.TamañoTotalArc;
                document.getElementById("lblPNumArch").innerHTML= aData.PromedioArchivos;
            }
        });
      });
    </script>
    <!-- /bootstrap-daterangepicker -->
  
    <!-- ECharts -->
    <script src="vendors/echarts/dist/echarts.min.js"></script>
    <script src="vendors/echarts/map/js/world.js"></script>
    <script type="text/javascript">
        var theme = {
            color: [
                '#26B99A', '#34495E', '#BDC3C7', '#3498DB',
                '#9B59B6', '#8abb6f', '#759c6a', '#bfd3b7'
            ],

            title: {
                itemGap: 8,
                textStyle: {
                    fontWeight: 'normal',
                    color: '#408829'
                }
            },

            dataRange: {
                color: ['#1f610a', '#97b58d']
            },

            toolbox: {
                color: ['#408829', '#408829', '#408829', '#408829']
            },

            tooltip: {
                backgroundColor: 'rgba(0,0,0,0.5)',
                axisPointer: {
                    type: 'line',
                    lineStyle: {
                        color: '#408829',
                        type: 'dashed'
                    },
                    crossStyle: {
                        color: '#408829'
                    },
                    shadowStyle: {
                        color: 'rgba(200,200,200,0.3)'
                    }
                }
            },

            dataZoom: {
                dataBackgroundColor: '#eee',
                fillerColor: 'rgba(64,136,41,0.2)',
                handleColor: '#408829'
            },
            grid: {
                borderWidth: 0
            },

            categoryAxis: {
                axisLine: {
                    lineStyle: {
                        color: '#408829'
                    }
                },
                splitLine: {
                    lineStyle: {
                        color: ['#eee']
                    }
                }
            },

            valueAxis: {
                axisLine: {
                    lineStyle: {
                        color: '#408829'
                    }
                },
                splitArea: {
                    show: true,
                    areaStyle: {
                        color: ['rgba(250,250,250,0.1)', 'rgba(200,200,200,0.1)']
                    }
                },
                splitLine: {
                    lineStyle: {
                        color: ['#eee']
                    }
                }
            },
            timeline: {
                lineStyle: {
                    color: '#408829'
                },
                controlStyle: {
                    normal: { color: '#408829' },
                    emphasis: { color: '#408829' }
                }
            },

            k: {
                itemStyle: {
                    normal: {
                        color: '#68a54a',
                        color0: '#a9cba2',
                        lineStyle: {
                            width: 1,
                            color: '#408829',
                            color0: '#86b379'
                        }
                    }
                }
            },
            map: {
                itemStyle: {
                    normal: {
                        areaStyle: {
                            color: '#ddd'
                        },
                        label: {
                            textStyle: {
                                color: '#c12e34'
                            }
                        }
                    },
                    emphasis: {
                        areaStyle: {
                            color: '#99d2dd'
                        },
                        label: {
                            textStyle: {
                                color: '#c12e34'
                            }
                        }
                    }
                }
            },
            force: {
                itemStyle: {
                    normal: {
                        linkStyle: {
                            strokeColor: '#408829'
                        }
                    }
                }
            },
            chord: {
                padding: 4,
                itemStyle: {
                    normal: {
                        lineStyle: {
                            width: 1,
                            color: 'rgba(128, 128, 128, 0.5)'
                        },
                        chordStyle: {
                            lineStyle: {
                                width: 1,
                                color: 'rgba(128, 128, 128, 0.5)'
                            }
                        }
                    },
                    emphasis: {
                        lineStyle: {
                            width: 1,
                            color: 'rgba(128, 128, 128, 0.5)'
                        },
                        chordStyle: {
                            lineStyle: {
                                width: 1,
                                color: 'rgba(128, 128, 128, 0.5)'
                            }
                        }
                    }
                }
            },
            gauge: {
                startAngle: 225,
                endAngle: -45,
                axisLine: {
                    show: true,
                    lineStyle: {
                        color: [[0.2, '#86b379'], [0.8, '#68a54a'], [1, '#408829']],
                        width: 8
                    }
                },
                axisTick: {
                    splitNumber: 10,
                    length: 12,
                    lineStyle: {
                        color: 'auto'
                    }
                },
                axisLabel: {
                    textStyle: {
                        color: 'auto'
                    }
                },
                splitLine: {
                    length: 18,
                    lineStyle: {
                        color: 'auto'
                    }
                },
                pointer: {
                    length: '90%',
                    color: 'auto'
                },
                title: {
                    textStyle: {
                        color: '#333'
                    }
                },
                detail: {
                    textStyle: {
                        color: 'auto'
                    }
                }
            },
            textStyle: {
                fontFamily: 'Arial, Verdana, sans-serif'
            }
        };
        var echartGauge = echarts.init(document.getElementById('echart_guage'), theme);
        var url = document.getElementById('hflURL').value;
        var varibales = JSON.stringify({
            FechaIncio: '/Date(' + moment().subtract(29, 'days') + ')/',
            FechaFin: '/Date(' + moment() + ')/',
            sucOID: document.getElementById('sucOID').value,
            intProyectoID: document.getElementById("intProyectoID").value
        });
        $.ajax({
            type: "POST",
            url: url + "/Services/NapoleonService.svc/json/getPromedioEnvio",
            data: varibales,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: OnSuccessProm_,
            error: OnErrorCall_
        });
        var catalogo = JSON.stringify({
            _TipoCat: "Sucursales",
            intProyecto: document.getElementById("intProyectoID").value,
            id_Sitio:  document.getElementById('sucOID').value
        });
        $.ajax({
            type: "POST",
            url: url + "/Services/NapoleonService.svc/json/getCatalogo",
            data: catalogo,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                $("#ddlSucurs").get(0).options.length = 0;
                if (document.getElementById('intTipoUsuarioID').value == "1" || document.getElementById('intTipoUsuarioID').value == "2") {
                    $("#ddlSucurs").get(0).options[0] = new Option("Todas Sucursales", "0");
                }

                $.each(msg.d, function (index, item) {
                    $("#ddlSucurs").get(0).options[$("#ddlSucurs").get(0).options.length] = new Option(item.vchDescripcion, item.vchValue);
                });
                var selected = document.getElementById('sucOID').value;
                $("#ddlSucurs").val(selected);
            },
            error: function () {
                alert("No se pudieron cargar las sucursales");
            }
        });

        function OnSuccessProm_(response) {
            if (response.d.length > 0) {
                var aDataProm = parseFloat(response.d);
                if (aDataProm != 'NaN') {
                    var minutos = parseInt(parseInt(aDataProm) / 60);
                    var Tope = parseInt(parseInt(minutos) + (parseInt(minutos) * .4));
                    var seg1 = parseFloat(((Tope / 10) * 2)).toFixed(1);
                    var seg2 = parseFloat(((Tope / 10) * 4)).toFixed(1);
                    var seg3 = parseFloat(((Tope / 10) * 7)).toFixed(1);
                    var seg4 = parseFloat((Tope / 10) * 9).toFixed(1);

                    echartGauge.setOption({
                        tooltip: {
                            formatter: "{a} <br/>{b} : {c}m"
                        },
                        toolbox: {
                            show: true,
                            feature: {
                                restore: {
                                    show: false,
                                    title: "Restore"
                                }

                            }
                        },
                        series: [{
                            name: 'Desempeño',
                            type: 'gauge',
                            center: ['50%', '50%'],
                            startAngle: 140,
                            endAngle: -140,
                            min: 0,
                            max: Tope,
                            precision: 0,
                            splitNumber: 10,
                            axisLine: {
                                show: true,
                                lineStyle: {
                                    color: [
                                      [0.2, 'lightgreen'],
                                      [0.4, 'orange'],
                                      [0.8, 'skyblue'],
                                      [1, '#1ABB9C']
                                    ],
                                    width: 30
                                }
                            },
                            axisTick: {
                                show: true,
                                splitNumber: 5,
                                length: 8,
                                lineStyle: {
                                    color: '#eee',
                                    width: 1,
                                    type: 'solid'
                                }
                            },
                            axisLabel: {
                                show: true,
                                formatter: function (v) {
                                    switch (v + '') {
                                        case seg1 + '':
                                            return seg1;
                                        case seg2 + '':
                                            return seg2;
                                        case seg3 + '':
                                            return seg3;
                                        case seg4 + '':
                                            return seg4;
                                        default:
                                            return '';
                                    }
                                },
                                textStyle: {
                                    color: '#333'
                                }
                            },
                            splitLine: {
                                show: true,
                                length: 30,
                                lineStyle: {
                                    color: '#eee',
                                    width: 2,
                                    type: 'solid'
                                }
                            },
                            pointer: {
                                length: '80%',
                                width: 8,
                                color: 'auto'
                            },
                            title: {
                                show: true,
                                offsetCenter: ['-65%', -10],
                                textStyle: {
                                    color: '#333',
                                    fontSize: 15
                                }
                            },
                            detail: {
                                show: true,
                                backgroundColor: 'rgba(0,0,0,0)',
                                borderWidth: 0,
                                borderColor: '#ccc',
                                width: 100,
                                height: 40,
                                offsetCenter: ['-60%', 10],
                                formatter: '{value}m',
                                textStyle: {
                                    color: 'auto',
                                    fontSize: 30
                                }
                            },
                            data: [{
                                value: minutos,
                                name: 'Desempeño'
                            }]
                        }]
                    });
                }
            }
        }
    </script>
</asp:Content>
