function SalvarDadosEndereco() {
    EfeitoBotao('.rotacionaricone > .iconebutton', '.rotacionaricone', true);

    if ($("#cli_idenderecomodal").val() == "") {
        let obj = { end_id: $("#end_id").val(), end_cep: $("#end_cep").val(), end_logradouro: $("#end_logradouro").val(), end_numero: $("#end_numero").val(), end_complemento: $("#end_complemento").val(), end_bairro: $("#end_bairro").val(), end_cidade: $("#end_cidade").val(), end_estado: $("#end_estado").val(), end_latitude: $("#end_latitude").val(), end_longetitude: $("#end_longetitude").val() }

        EnviarRequisicao("/Home/CadastroEndereco", obj, "POST", function (res) {
            window.location.reload();
        }, false);
    }
    else {
        let obj = { cliEnd_Tipo: "EP", cli_id: $("#cli_idenderecomodal").val(), end_id: $("#end_id").val(), end_cep: $("#end_cep").val(), end_logradouro: $("#end_logradouro").val(), end_numero: $("#end_numero").val(), end_complemento: $("#end_complemento").val(), end_bairro: $("#end_bairro").val(), end_cidade: $("#end_cidade").val(), end_estado: $("#end_estado").val(), end_latitude: $("#end_latitude").val(), end_longetitude: $("#end_longetitude").val() }

        EnviarRequisicao("/Home/InsertClienteEndereco", obj, "POST", function (res) {
            window.location.reload();
        }, false);
    }
}

function CancelarCadastroEndereco(){
    $('#enderecolateralModal').modal('toggle');
}

function EnderecoBuscaCep() {

    EfeitoBotao('.buttonenderecobuscacep > .iconebutton', '.buttonenderecobuscacep', true);

    if ($("#end_cep").val() != "") {
        $.ajax({
            url: '/Home/BuscaCepEndereco',
            data: { end_cep: $("#end_cep").val() },
            type: 'GET',
            beforeSend: function () {
                //forma.find('.load').fadeIn("fast");
            },
            success: function (retornoEndereco) {
                if (retornoEndereco != undefined && retornoEndereco != null && retornoEndereco != "") {
                    $("#end_logradouro").val(retornoEndereco.logradouro);
                    $("#end_bairro").val(retornoEndereco.bairro);
                    $("#end_cidade").val(retornoEndereco.localidade);
                    $('#end_estado option[value="' + retornoEndereco.uf + '"]').attr({ selected: "selected" });
                    EfeitoBotao('.buttonenderecobuscacep > .iconebutton', '.buttonenderecobuscacep', false);
                }
                else {
                    $("#end_logradouro").val('');
                    $("#end_bairro").val('');
                    $("#end_cidade").val('');
                    $('#end_estado option[value=""]').attr({ selected: "selected" });
                    EfeitoBotao('.buttonenderecobuscacep > .iconebutton', '.buttonenderecobuscacep', false);
                }
            },
            complete: function () {
                //forma.find('.load').fadeOut("fast");
            }
        });
    }
    else
        EfeitoBotao('.buttonenderecobuscacep > .iconebutton', '.buttonenderecobuscacep', false);
}

function CadastrarNovoCliente(id) {
    $("#enderecolateralModal").modal();
    $("#cli_idenderecomodal").val(id);
}

function CarregarEndereco(id) {
    $.ajax({
        url: '/Home/CarregaEndereco',
        data: { id: id },
        type: 'POST',
        beforeSend: function () {
        },
        success: function (retornoContato) {
            $('#enderecoModal').modal('show');
            retornoContato.end_cep = retornoContato.end_cep.length < 8 ? '0' + retornoContato.end_cep : retornoContato.end_cep;
            $("#end_cep").val(retornoContato.end_cep);
            $("#end_logradouro").val(retornoContato.end_logradouro);
            $("#end_numero").val(retornoContato.end_numero);
            $("#end_complemento").val(retornoContato.end_complemento);
            $("#end_estado").val(retornoContato.end_estado);
            $("#end_cidade").val(retornoContato.end_cidade);
            $("#end_bairro").val(retornoContato.end_bairro);
            $("#end_id").val(retornoContato.end_id);
            $("#end_latitude").val(retornoContato.end_latitude);
            $("#end_longetitude").val(retornoContato.end_longetitude);
            $('#cliEnd_Tipo option[value=' + retornoContato.cliEnd_Tipo + ']').attr('selected', 'selected');
            $("#cliEnd_id").val(retornoContato.cliEnd_id);

            $("#enderecolateralModal").modal();
        },
        complete: function () {
        }
    });
}


function DeletarEnderecoCliente(endId, cliEndId) {
    Confirmacao("Atenção", "Tem certeza que deseja deletear o item ? ", function (res) {
        if (res) {
            let data = {cliEnd_id: cliEndId}
            EnviarRequisicao("/Home/DeleteClienteEndereco", data, "POST", function () {
                $("#" + cliEndId).fadeOut();
            }, true);
        }
    })
    
    //deletarEnderecocliente
}