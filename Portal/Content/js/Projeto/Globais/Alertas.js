const Cores = {
    Azul: 'nk-blue',
    AzulClaro: 'nk-light-blue',
    AzulEscuro: 'nk-indigo',
    Roxo: 'nk-deep-purple',
    Ciano: 'nk-cyan',
    Vermelho: 'nk-red'
}

function AbreModal(Title) {
    if (Title == undefined || Title == null || Title == "")
        $("#ModaLoadingDescricao").html("Carregando Dados...");
    else
        $("#ModaLoadingDescricao").html(Title);
    $('#ModaLoading').modal({ backdrop: 'static', keyboard: true });
}

function FechaModal() {
    $('#ModaLoading').modal('hide');
}

function Alerta(Title, Message, CallBack) {
    FechaModal();
    AbrirTelaModal(Title, Message, CallBack, false);
}

function AlertaAzul(Title, Message, CallBack) {
    AbrirTelaModal(Title, Message, CallBack, false, Cores.Azul);
}

function AlertaVermelho(Title, Message, CallBack) {
    AbrirTelaModal(Title, Message, CallBack, false, Cores.Vermelho);
}



function Confirmacao(Title, Message, CallBack, Cor) {
    FechaModal();
    AbrirTelaModal(Title, Message, CallBack, true);
}

function ConfirmacaoAzul(Title, Message, CallBack, Cor) {
    AbrirTelaModal(Title, Message, CallBack, true, Cores.Azul);
}

function ConfirmacaoVermelho(Title, Message, CallBack, Cor) {
    AbrirTelaModal(Title, Message, CallBack, true, Cores.Vermelho);
}


function AbrirTelaModal(Title, Message, CallBack, Confirmacao, Cor) {

    $("#ModaAlertaCor").removeClass(Cores.Azul).removeClass(Cores.AzulClaro).removeClass(Cores.AzulEscuro).removeClass(Cores.Roxo).removeClass(Cores.Ciano).removeClass(Cores.Vermelho).addClass(Cor);

    if (Title == "")
        Title = "Atenção";

    $("#ModaAlertaTitulo").html(Title);
    $("#ModaAlertaDescricao").html(Message);

    if (Confirmacao) {
        $("#ModaAlertaButtonSim").show();
        $("#ModaAlertaButtonNao").html("Não");
    }
    else {
        $("#ModaAlertaButtonSim").hide();
        $("#ModaAlertaButtonNao").html("Fechar");
    }

    if (CallBack != null) {
        if (Confirmacao) {
            $("#ModaAlertaButtonSim").click(function () {
                CallBack(true);
            })
            $("#ModaAlertaButtonNao").click(function () {
                CallBack(false);
            })
        }
        else {
            $("#ModaAlertaButtonNao").click(function () {
                CallBack();
            })
        }
    }

    $('#ModaAlerta').modal({ backdrop: 'static', keyboard: true });
}

