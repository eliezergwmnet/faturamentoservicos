$(function () {
    $("#buttonTituloPagesCadastro").click(function () {
        CadastrarServicosItem();
    })

    $('.i-checks').iCheck({
        checkboxClass: 'icheckbox_square-green',
        radioClass: 'iradio_square-green',
    });
});



function CadastrarServicosItem() {
    CarregarSelect("serv_id", "", null, function () { $('#serv_id').selectpicker(); }, "/SCM/_DropDown/CarregaServicos");
    $("#ModContratocli_id").val($("#cli_id").val());
    $("#ModContratocont_id").val($("#cont_id").val());

    $('#SCMCadastroContratoServico').modal({ backdrop: 'static', keyboard: true });
}


/*
Função usada fora da tela de cadasatro de seriço
passando os parametros de Contrato e Cliente
*/
function CadastrarConfiguracaoItemTelaContrato(cliente, contrato) {
    CarregarSelect("serv_id", "", null, function () { $('#serv_id').selectpicker(); }, "/SCM/_DropDown/CarregaServicos");
    $("#ModContratocli_id").val(cliente);
    $("#ModContratocont_id").val(contrato);

    $('#SCMCadastroContratoServico').modal({ backdrop: 'static', keyboard: true });
}


function SCMAlterarServicoCliente(id) {
    EnviarRequisicao("/SCM/ContratoServicos/SelectId", {servCli_id: id}, "Post", function (res) {
        $("#servCli_id").val(res.servCli_id);
        $("#cli_id").val(res.cli_id);
        $("#cont_id").val(res.cont_id);
        CarregarSelect("serv_id", "", res.serv_id, function () { $('#serv_id').selectpicker(); }, "/SCM/_DropDown/CarregaServicos");
        $("#servCli_nome").val(res.servCli_nome);
        $("#servCli_valor").val(res.servCli_valor);
        //$("#servCli_dataAtivacao").val(res.servCli_dataAtivacao);
        $("#servCli_parcelado").val(res.servCli_parcelado);
        $("#servCli_parceladoQtd").val(res.servCli_parceladoQtd);
        $("#servCli_cobrarPorpor").val(res.servCli_cobrarPorpor);
        $("#servCli_qtdDias").val(res.servCli_qtdDias);
        $("#servCli_descricao").val(res.servCli_descricao);

        var data = new Date(parseInt(res.servCli_dataAtivacao.substr(6)));
        $("#servCli_dataAtivacao").val(data.format("dd/mm/yyyy"));

        $('#SCMCadastroContratoServico').modal({ backdrop: 'static', keyboard: true });
    });
}

function CobrarProporcional() {
    var valor = $("#servCli_cobrarPorpor").val();
    if (valor == "" || valor == "0") {
        $(".servCli_qtdDiasitem").fadeOut();
    }
    else {
        $(".servCli_qtdDiasitem").fadeIn();
    }
}


function SCMCadastrarServicosCliente() {
    debugger
    if (ValidaCampos("SCMCadastroServicoCliente")) {
        var obj = {
            
            servCli_id: $("#servCli_id").val(),
            cli_id: $("#ModContratocli_id").val(),
            cont_id: $("#ModContratocont_id").val(),
            serv_id: $("#serv_id").val(),
            servCli_nome: $("#serv_id option:selected").text(),
            servCli_valor: $("#servCli_valor").val(),
            servCli_dataAtivacao: $("#servCli_dataAtivacao").val(),
            servCli_parcelado: $("#servCli_parcelado").val(),
            servCli_parceladoQtd: $("#servCli_parceladoQtd").val(),
            servCli_cobrarPorpor: $("#servCli_cobrarPorpor").val(),
            servCli_qtdDias: $("#servCli_qtdDias").val(),
            servCli_descricao: $("#servCli_descricao").val()
        }

        EnviarRequisicao("/SCM/ContratoServicos/Cadastrar", obj, "Post", function () {
            window.location.reload()
        })
    }
}


function SCMRemoverServicoCliente(id) {
    Confirmacao("Atenção", "Tem certeza que deseja remover o item ? ", function (res) {
        if (res) {
            EnviarRequisicao("/SCM/ContratoServicos/Deletar", { servCli_id: id }, "Post", function () {
                window.location.reload();
            });
        }
    });
}