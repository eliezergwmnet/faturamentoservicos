
function BuscaConfiguracaoItensFiltro() {
    window.location = "/ConfiguracaoItens?tipo=" + $("#tipo").val() + "&descricao=" + $("#descricao").val();
}

function CadastrarConfiguracaoItemSalvar() {
    var Con_id = $("#Con_id").val();
    var Con_Tipo = $("#Con_Tipo").val();
    var Con_IdItem = $("#Con_IdItem").val();
    var Con_Descricao = $("#Con_Descricao").val();

    EnviarRequisicao("/ConfiguracaoItens/Insert", { Con_id: Con_id, Con_Tipo: Con_Tipo, Con_IdItem: Con_IdItem, Con_Descricao: Con_Descricao }, "Post", function (imagemUrl) {
        location.reload();
    });

}

function CadastrarConfiguracaoItem() {
    $("#Con_Tipo").val($("#tipo").val());
    $('#modalCadastroConfiguracaoItem').modal({ backdrop: 'static', keyboard: true });
}

function AlterarConfiguracaoItem(id) {
    EnviarRequisicao("/ConfiguracaoItens/SelectId", { id: id }, "Post", function (item) {
        $("#Con_id").val(item.Con_id);
        $("#Con_Tipo").val(item.Con_Tipo);
        $("#Con_IdItem").val(item.Con_IdItem);
        $("#Con_Descricao").val(item.Con_Descricao);

        $('#modalCadastroConfiguracaoItem').modal({ backdrop: 'static', keyboard: true })
    });
}

function DeletarConfiguracaoItem(id) {
    Confirmacao("Atenção", "Tem certeza que quer remover o item ", function (retorno) {
        EnviarRequisicao("/ConfiguracaoItens/Delete", { Con_id: id }, "Post", function (item) {
            location.reload();
        });
    });
}


$(document).ready(function () {
       /* $("#viewPlaceHolder").load("/Configuracao/CarregaLista?tipo=" + $("#tipo").val(), function () { Paginacao(); });

        $("#btnbuscadados").click(function () {
            $("#page-loader").fadeIn("slow");
            var parametro = "tipo=" + $("#tipo").val() + "&descricao=" + $("#descricao").val();
            $("#viewPlaceHolder").load("/Configuracao/CarregaLista?" + parametro, function () { Paginacao(); });
        })
        */


        $("#adicionaritens").click(function () {
            $.confirm({
                title: 'Adicionar Item',
                content: '' +
                '<form action="" class="formName">' +
                '<div class="form-group">' +
                '<label>Valor</label>' +
                '<input type="text" placeholder="Valor do Parametro" id="configuracaovalor" class="valor form-control" required />' +
                '</div>' +
                '<div class="form-group">' +
                '<label>Descrição</label>' +
                '<input type="text" placeholder="Descrição do Parametro" id="configuracaodescricao" class="descricao form-control" required />' +
                '</div>' +
                '</form>',
                buttons: {
                    formSubmit: {
                        text: 'Enviar',
                        btnClass: 'btn-blue',
                        action: function () {
                            var valor = this.$content.find('.valor').val();
                            var descricao = this.$content.find('.descricao').val();
                            if (!valor) {
                                $.alert('Campo valor obrigatório');
                                return false;
                            }
                            else if (!descricao) {
                                $.alert('Campo Descrição obrigatório');
                                return false;
                            }
                            $.ajax({
                                url: '/Configuracao/Insert',
                                data: { Conf_IdItem: $("#configuracaovalor").val(), Conf_Descricao: $("#configuracaodescricao").val(), Conf_Tipo: $("#tipo").val() },
                                type: 'POST',
                                beforeSend: function () {
                                },
                                success: function (retornoEndereco) {
                                    Alerta("Cadastro efetuado com sucesso", function () {
                                        $("#page-loader").fadeIn("slow");
                                        var parametro = "tipo=" + $("#tipo").val() + "&descricao=" + $("#descricao").val();
                                        $("#viewPlaceHolder").load("/Configuracao/CarregaLista?" + parametro, function () { Paginacao(); });

                                    })
                                },
                                complete: function () {
                                }
                            });
                        }
                    },
                    Cancelar: function () {
                    },
                },
                onContentReady: function () {
                    var jc = this;
                    this.$content.find('form').on('submit', function (e) {
                        e.preventDefault();
                        jc.$$formSubmit.trigger('click');
                    });
                }
            });
        });

    });
    $(".editarconfiguracao").click(function () {
        var nome = $(this).attr('name');
        var id = $(this).attr('id');
        var valor = $(this).attr('valor');
        var referencia = $(this).attr('referencia');


        $.confirm({
            title: 'Adicionar Item',
            content: '' +
            '<form action="" class="formName">' +
            '<div class="form-group">' +
            '<label>Valor</label>' +
            '<input type="text" placeholder="Valor do Parametro" value="' + valor + '" id="configuracaovalor" class="valor form-control" required />' +
            '</div>' +
            '<div class="form-group">' +
            '<label>Descrição</label>' +
            '<input type="text" placeholder="Descrição do Parametro" value="' + nome + '" id="configuracaodescricao" class="descricao form-control" required />' +
            '</div>' +
            '</form>',
            buttons: {
                formSubmit: {
                    text: 'Enviar',
                    btnClass: 'btn-blue',
                    action: function () {
                        var valor = this.$content.find('.valor').val();
                        var descricao = this.$content.find('.descricao').val();
                        if (!valor) {
                            $.alert('Campo valor obrigatório');
                            return false;
                        }
                        else if (!descricao) {
                            $.alert('Campo Descrição obrigatório');
                            return false;
                        }
                        $.ajax({
                            url: '/Configuracao/Update',
                            data: { Conf_IdItem: $("#configuracaovalor").val(), Conf_Descricao: $("#configuracaodescricao").val(), Conf_Tipo: $("#tipo").val(), Conf_id: id },
                            type: 'POST',
                            beforeSend: function () {
                            },
                            success: function (retornoEndereco) {
                                Alerta("Alteração efetuado com sucesso", function () {
                                    $("#page-loader").fadeIn("slow");
                                    var parametro = "tipo=" + $("#tipo").val() + "&descricao=" + $("#descricao").val();
                                    $("#viewPlaceHolder").load("/Configuracao/CarregaLista?" + parametro, function () { Paginacao(); });

                                })
                            },
                            complete: function () {
                            }
                        });
                    }
                },
                Cancelar: function () {
                },
            },
            onContentReady: function () {
                var jc = this;
                this.$content.find('form').on('submit', function (e) {
                    e.preventDefault();
                    jc.$$formSubmit.trigger('click');
                });
            }
        });
    });

    $(".deletarconfiguracao").click(function () {
        var id = $(this).attr('id');
        var name = $(this).attr('name');
        Confirmacao("Atenção", "Tem certeza que quer remover o item " + name, function (retorno) {
            if (retorno) {
                $.ajax({
                    url: '/Configuracao/Delete',
                    data: { Conf_id: id },
                    type: 'POST',
                    beforeSend: function () {
                    },
                    success: function (retornoEndereco) {
                        Alerta("Delete efetuado com sucesso", function () {
                            $("#page-loader").fadeIn("slow");
                            var parametro = "tipo=" + $("#tipo").val() + "&descricao=" + $("#descricao").val();
                            $("#viewPlaceHolder").load("/Configuracao/CarregaLista?" + parametro, function () { Paginacao(); });

                        })
                    },
                    complete: function () {
                    }
                });
            }
            else
                Alerta('Cancelado', 'Operação cancelada', function () { });
        })
    })