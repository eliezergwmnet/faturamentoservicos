/*
********************************************
********************************************
********************************************
*** DESENVOLVIDO POR LEANDRO MARTINS *******
*** PLATAFORMA DE CRIAÇÃO DE TELAS**********
********************************************
********************************************
********************************************
*/


/*EXEMPLO DE FUNÇÕES BASICAS DO SISTEMA

CARREGAR CHECKBOX
$('.i-checks').iCheck({
	checkboxClass: 'icheckbox_square-green',
	radioClass: 'iradio_square-green',
});

ATIVAR CHECKBOX
$("#cont_avulso").iCheck('check');
DESATIVAR CHECKBOX
$("#cont_avulso").iCheck('uncheck');

//CARREGAR COMBO
CarregarSelect("cli_id", "CarregaClientes");
*/

$(function () {
    $("#buttonTituloPagesCadastro").click(function () {
        CadastrarConfiguracaoItem();
    })

    CarregarSelect("cli_id", "CarregaClientes", null, function () { $('#cli_id').selectpicker(); });

});

function CadastrarConfiguracaoItem() {

    CarregarSelect("log_id", "CarregaClientes");
    CarregarSelect("not_numero", "CarregaClientes");
    CarregarSelect("cli_id", "CarregaClientes");
    CarregarSelect("cont_id", "CarregaClientes");
    CarregarSelect("conf_id", "CarregaClientes");
    CarregarSelect("not_preenchida", "CarregaClientes");
    $('#ModalCadastroNota').modal({ backdrop: 'static', keyboard: true });
}

function SCMCadastrarServicos() {
    if (ValidaCampos("SCMCadastroContrato")) {
        var obj = {
            not_id: $("#not_id").val(),
            log_id: $("#log_id").val(),
            not_numero: $("#not_numero").val(),
            cli_id: $("#cli_id").val(),
            cont_id: $("#cont_id").val(),
            conf_id: $("#conf_id").val(),
            not_tipoReceita: $("#not_tipoReceita").val(),
            not_codReceita: $("#not_codReceita").val(),
            not_dtEmissao: $("#not_dtEmissao").val(),
            not_dtVencimento: $("#not_dtVencimento").val(),
            not_pis: $("#not_pis").val(),
            not_confins: $("#not_confins").val(),
            not_cssl: $("#not_cssl").val(),
            not_irrf: $("#not_irrf").val(),
            not_totalbruto: $("#not_totalbruto").val(),
            not_totalliquido: $("#not_totalliquido").val(),
            not_preenchida: $("#not_preenchida").val(),
            not_emitida: $("#not_emitida").val(),
            not_situacao: $("#not_situacao").val(),
            not_chave: $("#not_chave").val(),
            not_tipopagamento: $("#not_tipopagamento").val(),
        }

        EnviarRequisicao("/SCM/Home/Cadastrar", obj, "Post", function (res) {
            window.location.href = "/SCM/ContratoServicos?contrato=" + res;
        })
    }
}

function SCMAlterarContrato(id) {
    EnviarRequisicao("/SCM/Home/SelectId", { not_id: id }, "Post", function (res) {
        $("#not_id").val(res.not_id);
        $("#log_id").val(res.log_id);
        $("#not_numero").val(res.not_numero);
        $("#cli_id").val(res.cli_id);
        $("#cont_id").val(res.cont_id);
        $("#conf_id").val(res.conf_id);
        $("#not_tipoReceita").val(res.not_tipoReceita);
        $("#not_codReceita").val(res.not_codReceita);
        $("#not_dtEmissao").val(res.not_dtEmissao);
        $("#not_dtVencimento").val(res.not_dtVencimento);
        $("#not_pis").val(res.not_pis);
        $("#not_confins").val(res.not_confins);
        $("#not_cssl").val(res.not_cssl);
        $("#not_irrf").val(res.not_irrf);
        $("#not_totalbruto").val(res.not_totalbruto);
        $("#not_totalliquido").val(res.not_totalliquido);
        $("#not_preenchida").val(res.not_preenchida);
        $("#not_emitida").val(res.not_emitida);
        $("#not_situacao").val(res.not_situacao);
        $("#not_chave").val(res.not_chave);
        $("#not_tipopagamento").val(res.not_tipopagamento);

        $('#SCMCadastroContrato').modal({ backdrop: 'static', keyboard: true });
    });
}

function SCMRemoverContrato(id) {
    Confirmacao("Atenção", "Tem certeza que deseja remover o servço ? ", function (res) {
        if (res) {
            EnviarRequisicao("/SCM/Home/Deletar", { not_id: id }, "Post", function () {
                window.location.reload();
            });
        }
    });
}
