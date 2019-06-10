$(function () {
    $("#buttonTituloPagesCadastro").click(function () {
        window.location = "/Usuarios/Insert";
    })

    CarregarSelect("perm_id", "CarregaPerfil");
})

function SalvarCliente() {
    var retorno = true;
    var retornoEnd = true;
    if ($("#usu_nome").val() == "") { retorno = false; $("#usu_nome").addClass("is-invalid").removeClass("is-valid") } else { retorno = retorno; $("#usu_nome").addClass("is-valid").removeClass("is-invalid") }
    if ($("#usu_telefone").val() == "") { retorno = false; $("#usu_telefone").addClass("is-invalid").removeClass("is-valid") } else { retorno = retorno; $("#usu_telefone").addClass("is-valid").removeClass("is-invalid") }
    if ($("#usu_celular").val() == "") { retorno = false; $("#usu_celular").addClass("is-invalid").removeClass("is-valid") } else { retorno = retorno; $("#usu_celular").addClass("is-valid").removeClass("is-invalid") }
    if ($("#perm_id").val() == "") { retorno = false; $("#perm_id").addClass("is-invalid").removeClass("is-valid") } else { retorno = retorno; $("#perm_id").addClass("is-valid").removeClass("is-invalid") }
    if ($("#usu_email").val() == "") { retorno = false; $("#usu_email").addClass("is-invalid").removeClass("is-valid") } else { retorno = retorno; $("#usu_email").addClass("is-valid").removeClass("is-invalid") }
    if ($("#usu_senha").val() == "") { retorno = false; $("#usu_senha").addClass("is-invalid").removeClass("is-valid") } else { retorno = retorno; $("#usu_senha").addClass("is-valid").removeClass("is-invalid") }

    if ($("#end_cep").val() == "") { retornoEnd = false; $("#end_cep").addClass("is-invalid").removeClass("is-valid") } else { retornoEnd = retornoEnd; $("#end_cep").addClass("is-valid").removeClass("is-invalid") }
    if ($("#end_logradouro").val() == "") { retornoEnd = false; $("#end_logradouro").addClass("is-invalid").removeClass("is-valid") } else { retornoEnd = retornoEnd; $("#end_logradouro").addClass("is-valid").removeClass("is-invalid") }
    if ($("#end_numero").val() == "") { retornoEnd = false; $("#end_numero").addClass("is-invalid").removeClass("is-valid") } else { retornoEnd = retornoEnd; $("#end_numero").addClass("is-valid").removeClass("is-invalid") }
    if ($("#end_estado").val() == "") { retornoEnd = false; $("#end_estado").addClass("is-invalid").removeClass("is-valid") } else { retornoEnd = retornoEnd; $("#end_estado").addClass("is-valid").removeClass("is-invalid") }
    if ($("#end_cidade").val() == "") { retornoEnd = false; $("#end_cidade").addClass("is-invalid").removeClass("is-valid") } else { retornoEnd = retornoEnd; $("#end_cidade").addClass("is-valid").removeClass("is-invalid") }
    if ($("#end_bairro").val() == "") { retornoEnd = false; $("#end_bairro").addClass("is-invalid").removeClass("is-valid") } else { retornoEnd = retornoEnd; $("#end_bairro").addClass("is-valid").removeClass("is-invalid") }

    if (retornoEnd) {
        objEnderec = {
            end_cep: $("#end_cep").val(), end_logradouro: $("#end_logradouro").val(), end_numero: $("#end_numero").val(), end_complemento: $("#end_complemento").val(),
            end_estado: $("#end_estado").val(), end_cidade: $("#end_cidade").val(), end_bairro: $("#end_bairro").val()
        }
    }

    if (retorno) {
        if (ValidaSenha($("#usu_senha").val(), $("#usu_senhaconfirmacao").val())) {
            obj = {
                usu_nome: $("#usu_nome").val(), usu_telefone: $("#usu_telefone").val(), usu_celular: $("#usu_celular").val(), perm_id: $("#perm_id").val(),
                usu_email: $("#usu_email").val(), usu_senha: $("#usu_senha").val(), usu_imagem: $("#usu_imagem").val()
            }
        }
        else {
            retorno = false;
        }
    }

    if(retorno && retornoEnd){
        EnviarRequisicao("/Usuarios/Insert", { _usuario: obj,  _endereco: objEnderec }, "Post", function () {
            Alerta("Cadastro", "Usuário cadastrado com sucesso", function () {
                window.location = "/Usuarios";
            })
        }, true)
    }
    return retorno;
}

function AlterarSenha(id) {
    $("#usu_idmodal").val(id);
    $('#usuarioalterarSenha').modal({ backdrop: 'static', keyboard: true });
}

function AlterarUsuario(id) {
    EnviarRequisicao("/Usuarios/SelecId", { id: id }, "Post", function (item) {
        $("#usu_id_modalalt").val(id);
        $("#usu_nomemodalalt").val(item.usu_nome);
        $("#usu_telefonemodalat").val(item.usu_telefone);
        $("#usu_celularmodalalt").val(item.usu_celular);
        $("#usu_imagemodalalt").val(item.usu_imagem);
        $("#imageusu_imagemodalalt").attr('src', item.usu_imagem);

        $('#usuarioalterarDados').modal({ backdrop: 'static', keyboard: true });
    });
}

function AlterarDadosUsuario() {
    var retorno = true;
    if ($("#usu_nomemodalalt").val() == "") { retorno = false; $("#usu_nomemodalalt").addClass("is-invalid").removeClass("is-valid") } else { retorno = retorno; $("#usu_nomemodalalt").addClass("is-valid").removeClass("is-invalid") }
    if ($("#usu_telefonemodalat").val() == "") { retorno = false; $("#usu_telefonemodalat").addClass("is-invalid").removeClass("is-valid") } else { retorno = retorno; $("#usu_telefonemodalat").addClass("is-valid").removeClass("is-invalid") }
    if ($("#usu_celularmodalalt").val() == "") { retorno = false; $("#usu_celularmodalalt").addClass("is-invalid").removeClass("is-valid") } else { retorno = retorno; $("#usu_celularmodalalt").addClass("is-valid").removeClass("is-invalid") }

    if (retorno) {
        EnviarRequisicao("/Usuarios/Update", { usu_id: $("#usu_id_modalalt").val(), usu_nome: $("#usu_nomemodalalt").val(), usu_telefone: $("#usu_telefonemodalat").val(), usu_celular: $("#usu_celularmodalalt").val(), usu_imagem: $("#usu_imagemodalalt").val() }, "Post", function (item) {
            Alerta("Alteração", "Usuário alterado com sucesso", function () {
                window.location = "/Usuarios";
            })
        });
    }
}

function AlterarSenhaUsuario() {
    if (ValidaSenha($("#usu_senhamodal").val(), $("#usu_senhaconfirmacaomodal").val())) {
        EnviarRequisicao("/Usuarios/AlterarResetar", { id: $("#usu_idmodal").val(), senha: $("#usu_senhamodal").val(), senhanova: $("#usu_senhaconfirmacaomodal").val() }, "Post", function () {
            Alerta("Alteração de Senha", "Senha alterada com sucesso!", function () { });
        });
    }
}

function RemoveUsuarioCadastrado(id) {
    ConfirmacaoVermelho("Atenção", "Tem certeza que deseja remover o usuário ?", function (res) {
        if (res) {
            EnviarRequisicao("/Usuarios/Delete", { usu_id: id }, "POST", function (res) {
                window.location = "/Usuarios/";
            });
        }
    })
}

function ValidaSenha(senha, confimacao) {
    if (senha.length < 6) {
        AlertaVermelho("Atenção", "Senha precisa ter pelo menos 6 digitos.");
        return false;
    }
    else {
        if (senha == confimacao) {
            return true;
        }
        else {
            AlertaVermelho("Atenção", "Senha diferente de confirmação.");
        }
    }
}