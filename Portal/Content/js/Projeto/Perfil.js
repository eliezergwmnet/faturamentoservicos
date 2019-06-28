$(function () {
    $("#buttonTituloPagesCadastro").click(function () {
        CarregaModalCadastroPerfil();
    })
});

function CarregaModalCadastroPerfil() {
    $('#modalCadastroPerfil').modal({ backdrop: 'static', keyboard: true })
}


function CadastrarPerfil() {
    var idperfil = $("#idperfil").val();
    var nomePerfil = $("#nomeperfil").val();
    if (nomePerfil == undefined || nomePerfil == null || nomePerfil == "") {
        $("#nomeperfil").addClass('is-valid');
    }
    else {
        EnviarRequisicao("/Perfil/InsertPerfil", { perm_id: idperfil, perm_nome: nomePerfil }, "GET", function () {
            location.reload();
        }, false);
    }
}

function AlterarPerfil(id, nome) {
    $("#idperfil").val(id);
    $("#nomeperfil").val(nome);

    CarregaModalCadastroPerfil();
}

function RemoverPerfil(id) {
    Confirmacao("Atenção", "Tem certeza que quer remover o perfil ", function (retorno) {
        if (retorno) {
            $.ajax({
                url: '/Perfil/DeletePeril',
                data: { perm_id: id },
                type: 'POST',
                beforeSend: function () {
                },
                success: function (retornoEndereco) {
                    location.reload();
                },
                complete: function () {
                }
            });
        }
        else
            Alerta('Cancelado', 'Operação cancelada', function () { });
    })
}