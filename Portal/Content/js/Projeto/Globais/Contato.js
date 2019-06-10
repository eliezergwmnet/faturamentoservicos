$(function () {
    $("#btnCancelarItemContato").click(function () {
        $('#contatoModal').modal('toggle');
    })


    $("#btnSalvarItemContato").click(function () {
        EfeitoBotao('#btnSalvarItemContato > .iconebutton', '#btnSalvarItem', true);

        let obj = { cli_id: $("#cli_idContatoModal").val(), cont_id: $("#cont_idmodal").val(), cont_nome: $("#cont_nome").val(), cont_fone: $("#cont_fone").val(), cont_departamento: $("#cont_departamento").val(), cont_email: $("#cont_email").val(), }

        EnviarRequisicao("/Clientes/CadastroContato", obj, "POST", function (res) {
            window.location = '/Clientes';
        }, false);
    });
})




function CadastrarNovoContato(id) {
    $("#contatoModal").modal();
    $("#cli_idContatoModal").val(id);
    $("#cont_idmodal").val('');

    $("#cont_nome").val('');
    $("#cont_fone").val('');
    $("#cont_departamento").val('');
    $("#cont_email").val('');
}

function CarregarContato(id) {
    $.ajax({
        url: '/Clientes/CarregaClienteContatos',
        data: { id: id },
        type: 'POST',
        beforeSend: function () {
        },
        success: function (retornoContato) {
            $('#contatoModal').modal('show');

            $("#cli_idContatoModal").val(retornoContato.cli_id);
            $("#cont_idmodal").val(retornoContato.cont_id);
            $("#cont_nome").val(retornoContato.cont_nome);
            $("#cont_fone").val(retornoContato.cont_fone);
            $("#cont_departamento").val(retornoContato.cont_departamento);
            $("#cont_email").val(retornoContato.cont_email);
        },
        complete: function () {
        }
    });
}


function DeletarContatoCliente(cont_id) {
    Confirmacao("Atenção", "Tem certeza que deseja deletear o item ? ", function (res) {
        if (res) {
            let data = { cont_id: cont_id }
            EnviarRequisicao("/Clientes/DeleteClienteContato", data, "POST", function () {
                $("#" + cont_id).fadeOut();
            }, true);
        }
    })

    //deletarEnderecocliente
}