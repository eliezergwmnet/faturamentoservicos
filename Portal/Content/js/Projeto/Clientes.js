var objCliente = {};
var objEnderec = {};
var objContato = {};

$(function () {
    $("#buttonTituloPagesCadastro").click(function () {
        window.location = "/Clientes/Insert";
    })
})

function ValidaCFPCNPJ() {
    var valor = $("#cli_CPF").val();
    if (!validate_cnpj(valor, true)) {
        if (!validate_cpf(valor, true)) {
            AlertaVermelho("Atenção", "CPF / CNPJ informado é Inválido", function () {
               // $("#cli_CPF").focus();
            });
        }
    }
}

function CarregaDadosCliente(id) {
    EnviarRequisicao("/Clientes/CarregarDados", { id: id }, "POST", function (res) {
        $("#cli_idmodal").val(res.cli_id);
        $("#cli_nomeFantasiamodal").val(res.cli_nomeFantasia);
        $("#cli_razaoSocialmodal").val(res.cli_razaoSocial);
        $("#cli_CPFmodal").val(res.cli_CPF);
        $("#cli_tipoInscricaomodal").val(res.cli_tipoInscricao);
        $("#cli_incricaoEstadualmodal").val(res.cli_incricaoEstadual);
        //$("#cli_CFOP").val(res.cli_CFOP);
        //$("#cli_tipoVencimento").val(res.cli_tipoVencimento);
        $("#cli_parametroVencimentomodal").val(res.cli_parametroVencimento);
        //$("#cli_CodCidIBGE").val(res.cli_CodCidIBGE);

        CarregarSelect("cli_CFOPmodal", "CarregaFOP", res.cli_CFOP);
        CarregarSelect("cli_tipoVencimentomodal", "CarregaTipoVencimento", res.cli_tipoVencimento);
        CarregarSelect("cli_CodCidIBGEmodal", "CarregaCodigoIBGE", res.cli_parametroVencimento);


        $(".cnpjselecionado").click(function () {
            $(".itemselecionado").html('CNPJ');
            $("#cli_CPFmodal").mask("99.999.999/9999-99", { placeholder: " " });
            $("#cli_CPFmodal").focus();
            $("#cli_tipoInscricaomodal").val("2");
        });
        $(".cpfselecionado").click(function () {
            $(".itemselecionado").html('CPF');
            $("#cli_CPFmodal").mask("999.999.999-99", { placeholder: " " });
            $("#cli_CPFmodal").focus();
            $("#cli_tipoInscricaomodal").val("1");
        });

        $('#empresaolateralModal').modal('toggle');
    });
}


function RemoveClienteCadastrado(id) {
    ConfirmacaoVermelho("Atenção", "Tem certeza que deseja remover o cliente ?", function (res) {
        if (res) {
            EnviarRequisicao("/Clientes/Deletar", { cli_id: id }, "POST", function (res) {
                window.location = "/Clientes/";
            });
        }
    })
}

function SalvarDadosCliente() {
    EfeitoBotao('.rotacionaricone > .iconebutton', '.rotacionaricone', true);

    let obj = {
        cli_id: $("#cli_idmodal").val(), cli_nomeFantasia: $("#cli_nomeFantasiamodal").val(), cli_razaoSocial: $("#cli_razaoSocialmodal").val(), cli_razaoSocial: $("#cli_razaoSocialmodal").val(), cli_CPF: $("#cli_CPFmodal").val(),
        cli_tipoInscricao: $("#cli_tipoInscricaomodal").val(), cli_incricaoEstadual: $("#cli_incricaoEstadualmodal").val(), cli_parametroVencimento: $("#cli_parametroVencimentomodal").val(),
        cli_CFOP: $("#cli_CFOPmodal").val(), cli_tipoVencimento: $("#cli_tipoVencimentomodal").val(), cli_CodCidIBGE: $("#cli_CodCidIBGEmodal").val()
    }

    EnviarRequisicao("/Clientes/Update", obj, "POST", function (res) {
        window.location.reload();
    }, false);
}

function CancelarCadastroCliente() {
    $('#empresaolateralModal').modal('toggle');
}

function CarregarLista() {
    $("#viewPlaceHolder").load("/Clientes/CarregaListaCliente?cli_nomeFantasia=" + $("#cli_nomeFantasia").val(), function () { Paginacao(); });

    $("#btnbuscadados").click(function () {

        $("#page-loader").fadeIn("slow");
        var parametro = "cli_nomeFantasia=" + encodeURIComponent($("#cli_nomeFantasia").val()) + "&cli_razaoSocial=" + encodeURIComponent($("#cli_razaoSocial").val()) + "&cli_CGC=" + $("#cli_CGC").val()
        $("#viewPlaceHolder").load("/Clientes/CarregaListaCliente?" + parametro, function () { Paginacao(); });
    })
}

function CarregaDadosCadastro() {
    CarregarSelect("cli_CFOP", "CarregaFOP");
    CarregarSelect("cli_tipoVencimento", "CarregaTipoVencimento");
    CarregarSelect("cli_CodCidIBGE", "CarregaCodigoIBGE");

    $(".cnpjselecionado").click(function () {
        $(".itemselecionado").html('CNPJ');
        $("#cli_CPF").mask("99.999.999/9999-99", { placeholder: " " });
        $("#cli_CPF").focus();
        $("#cli_tipoInscricao").val("2");
    });
    $(".cpfselecionado").click(function () {
        $(".itemselecionado").html('CPF');
        $("#cli_CPF").mask("999.999.999-99", { placeholder: " " });
        $("#cli_CPF").focus();
        $("#cli_tipoInscricao").val("1");
    });

    $(".buttonenderecobuscacep").click(function () {
        EnderecoBuscaCep();
    })


    $("#page02").fadeOut();
    $("#page03").fadeOut();
    $("#volta").fadeOut();
    $("#salvardados").fadeOut();

    $("#avancar").click(function () {

        var valor = $(this).val().toString();
        if (valor == "1") {
            if (ValidaCliente()) {
                $("#avancar").val("2");
                $("#volta").val("1");
                $("#volta").fadeIn();
                $("#page01").fadeOut();
                $("#page02").fadeIn();
                $("#page03").fadeOut();
                $("#progress").css("width", "52%");
            }
        }
        if (valor == "2") {
            if (ValidaEndereco()) {
                $("#avancar").val("3");
                $("#volta").val("2");
                $("#volta").fadeIn();
                $("#page01").fadeOut();
                $("#page02").fadeOut();
                $("#page03").fadeIn();
                $("#progress").css("width", "77%");

                $("#avancar").fadeOut();
                setTimeout(function () {
                    $("#salvardados").fadeIn();
                }, 1000);
            }
        }

    })

    $("#volta").click(function () {

        var valor = $(this).val().toString();
        if (valor == "1") {
            $("#avancar").val("1");
            $("#volta").val("1");
            $("#volta").fadeOut();
            $("#page01").fadeIn();
            $("#page02").fadeOut();
            $("#page03").fadeOut();
            $("#progress").css("width", "25%");

        }
        if (valor == "2") {
            $("#avancar").val("2");
            $("#volta").val("1");
            $("#volta").fadeIn();
            $("#page01").fadeOut();
            $("#page02").fadeIn();
            $("#page03").fadeOut();
            $("#progress").css("width", "52%");
        }

    })

    $(".one").click(function () {
        $("#avancar").val("1");
        $("#volta").val("1");
        $("#volta").fadeOut();
        $("#page01").fadeIn();
        $("#page02").fadeOut();
        $("#page03").fadeOut();
        $("#progress").css("width", "25%");
    })
    $(".two").click(function () {
        $("#avancar").val("2");
        $("#volta").val("1");
        $("#volta").fadeIn();
        $("#page01").fadeOut();
        $("#page02").fadeIn();
        $("#page03").fadeOut();
        $("#progress").css("width", "52%");
    })
    $(".three").click(function () {
        $("#avancar").val("2");
        $("#volta").val("2");
        $("#volta").fadeIn();
        $("#page01").fadeOut();
        $("#page02").fadeOut();
        $("#page03").fadeIn();
        $("#progress").css("width", "77%");
    })


    $("#salvardados").click(function () {
        if (ValidaContato()) {
            EnviarRequisicao("/Clientes/Insert", { _cliente: objCliente, cli_CFOP: $("#cli_CFOP").val(), _endereco: objEnderec, _contato: objContato }, "Post", function () {
                window.location = "/Clientes";
            }, true)
        }
    })
}


function ValidaCliente() {
    var retorno = true;
    if ($("#cli_nomeFantasia").val() == "") { retorno = false; $("#cli_nomeFantasia").addClass("is-invalid").removeClass("is-valid") } else { retorno = retorno; $("#cli_nomeFantasia").addClass("is-valid").removeClass("is-invalid") }
    if ($("#cli_razaoSocial").val() == "") { retorno = false; $("#cli_razaoSocial").addClass("is-invalid").removeClass("is-valid") } else { retorno = retorno; $("#cli_razaoSocial").addClass("is-valid").removeClass("is-invalid") }
    if ($("#cli_CPF").val() == "") { retorno = false; $("#cli_CPF").addClass("is-invalid").removeClass("is-valid") } else { retorno = retorno; $("#cli_CPF").addClass("is-valid").removeClass("is-invalid") }
    if ($("#cli_incricaoEstadual").val() == "") { retorno = false; $("#cli_incricaoEstadual").addClass("is-invalid").removeClass("is-valid") } else { retorno = retorno; $("#cli_incricaoEstadual").addClass("is-valid").removeClass("is-invalid") }
    if ($("#cli_CFOP").val() == "") { retorno = false; $("#cli_CFOP").addClass("is-invalid").removeClass("is-valid") } else { retorno = retorno; $("#cli_CFOP").addClass("is-valid").removeClass("is-invalid") }
    if ($("#cli_tipoVencimento").val() == "") { retorno = false; $("#cli_tipoVencimento").addClass("is-invalid").removeClass("is-valid") } else { retorno = retorno; $("#cli_tipoVencimento").addClass("is-valid").removeClass("is-invalid") }
    if ($("#cli_parametroVencimento").val() == "") { retorno = false; $("#cli_parametroVencimento").addClass("is-invalid").removeClass("is-valid") } else { retorno = retorno; $("#cli_parametroVencimento").addClass("is-valid").removeClass("is-invalid") }
    if ($("#cli_CodCidIBGE").val() == "") { retorno = false; $("#cli_CodCidIBGE").addClass("is-invalid").removeClass("is-valid") } else { retorno = retorno; $("#cli_CodCidIBGE").addClass("is-valid").removeClass("is-invalid") }

    if (retorno) {
        objCliente = {
            cli_nomeFantasia: $("#cli_nomeFantasia").val(), cli_razaoSocial: $("#cli_razaoSocial").val(), cli_CPF: $("#cli_CPF").val(), cli_incricaoEstadual: $("#cli_incricaoEstadual").val(),
            cli_tipoVencimento: $("#cli_tipoVencimento").val(), cli_parametroVencimento: $("#cli_parametroVencimento").val(), cli_CodCidIBGE: $("#cli_CodCidIBGE").val(), cli_tipoInscricao: $("#cli_tipoInscricao").val()
        }
    }
    return retorno;
}

function ValidaEndereco() {
    var retorno = true;
    if ($("#end_cep").val() == "") { retorno = false; $("#end_cep").addClass("is-invalid").removeClass("is-valid") } else { retorno = retorno; $("#end_cep").addClass("is-valid").removeClass("is-invalid") }
    if ($("#end_logradouro").val() == "") { retorno = false; $("#end_logradouro").addClass("is-invalid").removeClass("is-valid") } else { retorno = retorno; $("#end_logradouro").addClass("is-valid").removeClass("is-invalid") }
    if ($("#end_numero").val() == "") { retorno = false; $("#end_numero").addClass("is-invalid").removeClass("is-valid") } else { retorno = retorno; $("#end_numero").addClass("is-valid").removeClass("is-invalid") }
    if ($("#end_estado").val() == "") { retorno = false; $("#end_estado").addClass("is-invalid").removeClass("is-valid") } else { retorno = retorno; $("#end_estado").addClass("is-valid").removeClass("is-invalid") }
    if ($("#end_cidade").val() == "") { retorno = false; $("#end_cidade").addClass("is-invalid").removeClass("is-valid") } else { retorno = retorno; $("#end_cidade").addClass("is-valid").removeClass("is-invalid") }
    if ($("#end_bairro").val() == "") { retorno = false; $("#end_bairro").addClass("is-invalid").removeClass("is-valid") } else { retorno = retorno; $("#end_bairro").addClass("is-valid").removeClass("is-invalid") }

    if (retorno) {
        objEnderec = {
            end_cep: $("#end_cep").val(), end_logradouro: $("#end_logradouro").val(), end_numero: $("#end_numero").val(), end_complemento: $("#end_complemento").val(),
            end_estado: $("#end_estado").val(), end_cidade: $("#end_cidade").val(), end_bairro: $("#end_bairro").val()
        }
    }
    return retorno;
}

function ValidaContato() {
    var retorno = true;
    if ($("#cont_nome").val() == "") { retorno = false; $("#cont_nome").addClass("is-invalid").removeClass("is-valid") } else { retorno = retorno; $("#cont_nome").addClass("is-valid").removeClass("is-invalid") }
    if ($("#cont_fone").val() == "") { retorno = false; $("#cont_fone").addClass("is-invalid").removeClass("is-valid") } else { retorno = retorno; $("#cont_fone").addClass("is-valid").removeClass("is-invalid") }
    if ($("#cont_email").val() == "") { retorno = false; $("#cont_email").addClass("is-invalid").removeClass("is-valid") } else { retorno = retorno; $("#cont_email").addClass("is-valid").removeClass("is-invalid") }

    if (retorno) {
        objContato = {
            cont_nome: $("#cont_nome").val(), cont_fone: $("#cont_fone").val(),
            cont_departamento: $("#cont_departamento").val(), cont_email: $("#cont_email").val()
        }
    }
    return retorno;
}
