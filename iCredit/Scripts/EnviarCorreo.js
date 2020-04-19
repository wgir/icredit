$(document).ready(function () {

    var Path = location.host;
    var VirtualDirectory;
    if (Path.indexOf("localhost") >= 0 && Path.indexOf(":") >= 0) { VirtualDirectory = ""; }
    else {
        var pathname = window.location.pathname; var VirtualDir = pathname.split('/');
        VirtualDirectory = VirtualDir[1]; VirtualDirectory = '/' + VirtualDirectory;
    }

    $('#btnEnviar').click(function (event) {
        // $("#createUnoForm").submit(function (event) {
        /* when the submit button in the modal is clicked, submit the form */
        alert('submitting en enviar correo');
        //  event.preventDefault();
        if (validar($("#Destinatario"), $('#Asunto'), $('#Mensaje'))) {


            $.ajax({
                //para validar que la fecha que esta ingesando los asistente sea mayor a la fecha actual de nomina
                url: VirtualDirectory + "/EnviarCorreo/EnviarAbono/",
                type: "POST",
                // data: JSON.stringify({ cliNit: $('#Cliente_Cli_Nit').val(), Cli_Nombre: $('#Cli_Nombre').val(), Cli_Apellido: $('#Cli_Apellido').val() }),
                data: JSON.stringify({
                    Servidor: $("#Servidor").val(), Puerto: $('#Puerto').val(),
                    Usuario: $('#Usuario').val(),
                    Empresa: $('#Empresa').val(),
                    Destinatario: $('#Destinatario').val(),
                    Password: $('#Password').val(),
                    Asunto: $('#Asunto').val(),
                    Mensaje: $('#Mensaje').val(),
                    Adjunto: $('#Adjunto').val()
                }),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    if (data.success) {
                        $('#tallModal').modal('hide');
                        // $("#CanalId").focus();
                        // $("#Cliente_Cli_Nit").focus();
                        // limpiarCamposmodal();
                    }
                },

                error: function () {
                    //$('#message').html('Error Occurred').fadeIn();
                }
            });
        } else {

        }
        //alert('submitting');
        //$('#createUnoForm').submit();
        event.preventDefault();

    });




});