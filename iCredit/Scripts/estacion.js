$(document).ready(function () {
    // $("#EmpCombo").hide("slow");
    //$('form:first *:input[type!=hidden]:first').focus();
    //alert('jQuery has loaded!');
    $("#Est_CodigoSicom").focus();
   // $("#Est_CodigoSicom").attr('maxlength', $("#LONG_CODIGO_SICOM").val());
    $.ajaxSetup({
        async: false
    });
   // var escogerDeptoyCiudad = 0;
    var Path = location.host;
    var VirtualDirectory;
    if (Path.indexOf("localhost") >= 0 && Path.indexOf(":") >= 0) { VirtualDirectory = ""; }
    else {
        var pathname = window.location.pathname; var VirtualDir = pathname.split('/');
        VirtualDirectory = VirtualDir[1]; VirtualDirectory = '/' + VirtualDirectory;
    }
   
      

      $("#Est_Departamento").change(function () {

        $("#Est_Ciudad").empty();
        var Param = { DepartamentoId: $("#Est_Departamento > option:selected").attr("value") };
        $.getJSON(VirtualDirectory + "/Estacion/llenarCiudadxDepto/", Param, function (data) {
            var items = "<option></option>";
            $.each(data, function (i, item) {
                items += "<option value=" + item.Value + ">" + item.Text + "</option>";
                
            });
            $("#Est_Ciudad").html(items);
        });
      });

      //$('#Est_Departamento').blur(function () {
      //    $("#estDeptoId").val($("#Est_Departamento > option:selected").attr("value"));
      //});

      //$('#Est_Ciudad').blur(function () {
      //    $("#estCiudadId").val($("#Est_Ciudad > option:selected").attr("value"));
      //});


      $("#form [name='Est_CodigoSicom']").focus();

});

