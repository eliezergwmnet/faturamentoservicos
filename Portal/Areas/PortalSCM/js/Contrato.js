$(document).ready(function () {
    $("#buttonTituloPagesCadastro").click(function () {
        CadastrarConfiguracaoItem();
    })

    $('.i-checks').iCheck({
        checkboxClass: 'icheckbox_square-green',
        radioClass: 'iradio_square-green',
    });
});

function CadastrarConfiguracaoItem() {
    CarregarSelect("cli_id", "CarregaClientes");
    $('#SCMCadastroContrato').modal({ backdrop: 'static', keyboard: true });
}

function SCMCadastrarServicos() {
    if (ValidaCampos("SCMCadastroContrato")) {
        var obj = {
            cont_id: $("#cont_id").val(),
            cli_id: $("#cli_id").val(),
            cont_numero: $("#cont_numero").val(),
            cont_nome: $("#cont_nome").val(),
            cont_descricao: $("#cont_descricao").val(),
            cont_avulso: $("#cont_avulso").val() == "on" ? true : false
        }

        EnviarRequisicao("/SCM/Home/Cadastrar", obj, "Post", function (res) {
            window.location.href = "/SCM/ContratoServicos?contrato=" + res;
        })
    }
}

function SCMAlterarContrato(id) {
    EnviarRequisicao("/SCM/Home/SelectId", { cont_id: id }, "Post", function (res) {
        $("#cont_id").val(res.cont_id);
        $("#cont_numero").val(res.cont_numero);
        $("#cont_nome").val(res.cont_nome);
        $("#cont_descricao").val(res.cont_descricao);

        if (res.cont_avulso) {
            $("#cont_avulso").iCheck('check');
        }
        else {
            $("#cont_avulso").iCheck('uncheck');
        }
        CarregarSelect("cli_id", "CarregaClientes", res.cli_id);

        $('#SCMCadastroContrato').modal({ backdrop: 'static', keyboard: true });

    });
}

function SCMRemoverContrato(id) {
    Confirmacao("Atenção", "Tem certeza que deseja remover o serviço ? ", function (res) {
        if (res) {
            EnviarRequisicao("/SCM/Home/Deletar", { cont_id: id }, "Post", function () {
                window.location.reload();
            });
        }
    });
}