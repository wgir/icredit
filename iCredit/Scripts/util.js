$(document).ready(function () {
    // $("#EmpCombo").hide("slow");
    //$('form:first *:input[type!=hidden]:first').focus();
    //alert('jQuery has loaded!');
    $("#Est_CodigoSicom").focus();
    $("#Est_CodigoSicom").attr('maxlength', $("#LONG_CODIGO_SICOM").val());
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
   


    

      //$("#Cliente_Cli_Nit").keypress(function (event) {
      //    var keycode = (event.keyCode ? event.keyCode : event.which);
      //    if (keycode == 13 || keycode == 9) {
      //        $("#Cliente_Cli_Nombre").val("");
      //        if ($("#Cliente_Cli_Nit").val().trim() != "") {
      //            $.getJSON(VirtualDirectory + '/Cliente/buscarCliente?cliNit=' + $('#Cliente_Cli_Nit').val(), function (data) {
      //                $.each(data, function (i, dataCliente) {
      //                    $("#Cliente_Cli_Nombre").val(dataCliente.Nombre + ' ' + dataCliente.Apellido);
      //                });
      //            });
      //            if ($("#Cliente_Cli_Nombre").val().trim() == "") {
      //                $('#tallModal').modal('show');
      //                $("#Cli_Nit").val($("#Cliente_Cli_Nit").val());
      //                $('#tallModal').find('.modal-title').text("Crear Cliente");
      //                var height = $(window).height() - 200;
      //                $('#tallModal').find(".modal-body").css("max-height", height);

                      
      //            }
      //        }
      //    }
      //});

      //$('#tallModal').on('hidden.bs.modal', function () {
      //   // $("#form [name='Cliente_Cli_Nit']").focus();
      //    $("[id$=Sol_Observacion]").focus();
      //    $("[id$=Cliente_Cli_Nit]").focus();
      //})

   //   $('#tallModal').on('shown.bs.modal', function () {
   //       $('#Cli_Nombre').focus();
   //   });


   //   $("#Est_Departamento").change(function () {

   //     $("#Est_Ciudad").empty();
   //     var Param = { DepartamentoId: $("#Est_Departamento > option:selected").attr("value") };
   //     $.getJSON(VirtualDirectory + "/Cliente/llenarCiudadxDepto/", Param, function (data) {
   //         var items = "<option></option>";
   //         $.each(data, function (i, item) {
   //             items += "<option value=" + item.Value + ">" + item.Text + "</option>";
                
   //         });
   //         $("#Est_Ciudad").html(items);
   //     });
   //   });


      
       

     

   
   //$("#DepartamentoId").change(function () {
   //     $("#CiudadId").empty();
   //     var Param = { DepartamentoId: $("#DepartamentoId > option:selected").attr("value") };
   //     $.getJSON(VirtualDirectory + "/Cliente/llenarCiudadxDepto/", Param, function (data) {
   //         var items = "<option></option>";
   //         $.each(data, function (i, item) {
   //             items += "<option value=" + item.Value + ">" + item.Text + "</option>";
                
   //         });
   //         $("#CiudadId").html(items);
   //     });
   // });

  
   
   //function limpiarCamposmodal() {
   //    $("#Cli_Nit").val("");
   //    $('#Cli_Nombre').val("");
   //    $('#Cli_Apellido').val("");
   //    $('#Cli_Telefono').val("");
   //    $('#Cli_Celular').val("");
   //    $('#Cli_Email').val("");
   //    $('#PerfilId').val("");
   //    $('#Cli_Institucion').val("");
   //    $('#DepartamentoId').val("");
   //    $('#CiudadId').val("");
   //}   

        //validar = function () {
        //    for (var i = 0; i < arguments.length; i++) {
        //        if (arguments[i].val() == "" || arguments[i].val() == null) {
        //           // alert("Falta digitar informacion");
        //            arguments[i].css("border", "1px solid red");
        //            arguments[i].focus();
        //            return false;
        //        }
        //    }
        //    return true;
        //};
    

    //$('#Est_Departamento').blur(function () {
    //    $("#estDeptoId").val($("#Est_Departamento > option:selected").attr("value"));
    //});

    //$('#Est_Ciudad').blur(function () {
    //    $("#estCiudadId").val($("#Est_Ciudad > option:selected").attr("value"));
    //});



    //$("#Cli_Nit").focus(function () {
    //    $("#btnHistorial").addClass("disabled")
    //    $('[type="submit"]').prop('disabled', 'disabled');
    //});

    //$('#Cli_Nit').blur(function () {
    //    $('[type="submit"]').removeAttr("disabled");
    //    $('form').unbind('submit');
    //});


    // $("#frm").on('submit', function (event) {
    //    event.preventDefault();
    //    event.stopImmediatePropagation();
    //    $('[type="submit"]').prop('disabled', 'disabled');
    //});


    //$("#Cli_Nit").keypress(function (event) {
    //    var keycode = (event.keyCode ? event.keyCode : event.which);
    //    //alert(keycode);
    //    if (keycode == 13 || keycode==9)
    //    {
    //        $("#Cli_Nombre").val("");
    //        $("#Cli_Apellido").val("");
    //        $("#DepartamentoId").val($("#target option:first").val());
    //        $("#CiudadId").empty();
    //        if ($("#Cli_Nit").val().trim() != "")
    //        {
    //                $.getJSON(VirtualDirectory + '/Cliente/buscarCliente?cliNit=' + $('#Cli_Nit').val(), function (data) {
    //                    $.each(data, function (i, dataCliente) {
    //                        $("#Cli_Nombre").val(dataCliente.Nombre);
    //                        $("#Cli_Apellido").val(dataCliente.razonSocialP);
    //                        $("#DepartamentoId").val(dataCliente.Depto);
    //                        $("#DepartamentoId").trigger('change');
    //                        $("#CiudadId").val(dataCliente.Ciudad);
    //                        $("#btnHistorial").removeClass("disabled")
    //                    });
    //                });
    //        }
    //    }
    //});

   // hablitarCampos($('#HidetipoCliente').val());
    //$("#form [name='Est_CodigoSicom']").focus();

    //  $("form input:radio").change(function () {
    //      hablitarCampos($(this).val());
    //      $("#HidetipoCliente").val($(this).val());
    //});

    //hablitarCampos = function (radioButonVal)
    //{
    //    $("#HidetipoCliente").hide();
    //    if (radioButonVal.trim() == "Estacion")
    //    {
    //        $("#frmBuscarEstacion").show();
    //        if ($('#Est_CodigoSicom').val() == "")
    //        {
    //          $('#Est_RazonSocialPropietario').prop('readonly', true);
    //          $('#Est_Nombre').prop('readonly', true);
    //          $('#Est_Departamento').attr("disabled", true);
    //          $('#Est_Ciudad').attr("disabled", true);
    //        }
    //    } else {
    //                $("#frmBuscarEstacion").hide();
    //          }
    //};



    //  $("#Est_CodigoSicom").focus(function () {
    //      // $("#btnHistorial").addClass("disabled")
    //      $("#estDeptoId").val($("#Est_Departamento > option:selected").attr("value"));
    //      $("#estCiudadId").val($("#Est_Ciudad > option:selected").attr("value"));
    //      $('[type="submit"]').prop('disabled', 'disabled');
    //  });

    //  $('#Est_CodigoSicom').blur(function () {
    //      $('[type="submit"]').removeAttr("disabled");
    //      $('form').unbind('submit');

    //  });

    //$("#TemaId").change(function () {
    //    //alert("selecciona Tema");
    //    $("#SubtemaId").val($("#target option:first").val());
    //    $("#SubtemaId").empty();
    //    var Param = { TemaId: $("#TemaId > option:selected").attr("value") };
    //    $.getJSON(VirtualDirectory + "/Cliente/llenarSubtemaxTema/", Param, function (data) {
    //        var items = "<option></option>";
    //        $.each(data, function (i, item) {
    //            items += "<option value=" + item.Value + ">" + item.Text + "</option>";
    //        });
    //        $("#SubtemaId").html(items);
    //    });
    //});




    //$(".modal-wide").on("show.bs.modal", function (event) {

    //    var button = $(event.relatedTarget) // Button that triggered the modal
    //    var recipient = button.data('whatever') // Extract info from data-* attributes
    //    //// If necessary, you could initiate an AJAX request here (and then do the updating in a callback).
    //    //// Update the modal's content. We'll use jQuery here, but you could use a data binding library or other methods instead.
    //    var modal = $(this)
    //    modal.find('.modal-title').text(recipient)

    //    var height = $(window).height() - 200;
    //    $(this).find(".modal-body").css("max-height", height);
    //    $("#cargarURL").load(VirtualDirectory+"/Cliente/RegistroCliente?documentoCliente="+$("#Cli_Nit").val());
    //});


    //$('#crear').on('click', function (event) {
    //   // event.preventDefault();
    //   // alert('submitting');
    //    if ($('#acw').val()=="0")
    //    {
    //        $('#confirm-submit').modal('show');
    //    }else
    //    {
    //        $('#frm').submit();
    //    }
    //});
    //$('createUnoForm').submit(function () {
    //    event.preventDefault();
    //    alert('submitting');
    //});

});

