$(function () {
    $("#buttonTituloPagesCadastro").click(function () {
        CadastrarConfiguracaoItem();
    })

});

function CadastrarConfiguracaoItem() {
    //$("#Con_Tipo").val($("#tipo").val());
    $('#SCMCadastroServico').modal({ backdrop: 'static', keyboard: true });
}

function SCMCadastrarServicos() {
    debugger
    if (ValidaCampos("SCMCadastroServico")) {
        var obj = { ser_id: $("#ser_id").val(), ser_codigoServico: $("#ser_codigoServico").val(), ser_codigoAtividade: $("#ser_codigoAtividade").val(), ser_descricaoServico: $("#ser_descricaoServico").val(), ser_aliquota: $("#ser_aliquota").val(), }
        EnviarRequisicao("/SCM/Servicos/Cadastrar", obj, "Post", function () {
            window.location.reload()
        })
    }
}

function SCMAlterarServicos(id) {
    EnviarRequisicao("/SCM/Servicos/SelectId", { ser_id: id }, "Post", function (res) {
        $("#ser_id").val(res.ser_id);
        $("#ser_codigoServico").val(res.ser_codigoServico);
        $("#ser_codigoAtividade").val(res.ser_codigoAtividade);
        $("#ser_descricaoServico").val(res.ser_descricaoServico);
        $("#ser_aliquota").val(res.ser_aliquota);

        $('#SCMCadastroServico').modal({ backdrop: 'static', keyboard: true });

    });
}

function SCMRemoverServicos(id) {
    Confirmacao("Atenção", "Tem certeza que deseja remover o servço ? ", function (res) {
        if (res) {
            EnviarRequisicao("/SCM/Servicos/Deletar", { ser_id: id }, "Post", function () {
                window.location.reload();
            });
        }
    });
}