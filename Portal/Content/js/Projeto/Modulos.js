$(function () {
    //Fun~ção do botão padrão de cadastro no titulo da pagina
    $("#buttonTituloPagesCadastro").click(function () {
        $('#modalCadastroModulo').modal({ backdrop: 'static', keyboard: true })
    })
})

function salvarImagemModulo(input) {

    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#image_pequena').attr('src', "http://upload.wikimedia.org/wikipedia/commons/d/de/Ajax-loader.gif");

            EnviarRequisicao("/ConfiguracaoModulos/Base64ToImage", { base64String: e.target.result, tipo: "Modulos" }, "Post", function (imagemUrl) {
                $('#image_pequena').attr('src', e.target.result);
                $('#conf_imagempequena').val(imagemUrl);
            });
        }
        reader.readAsDataURL(input.files[0]);
    }
}

function CadastrarModulo() {
    var id = $("#mod_id").val();
    var nome = $("#mod_nome").val();
    var descricao = $("#mod_descricao").val();
    var imagem = $("#conf_imagempequena").val();

    EnviarRequisicao("/ConfiguracaoModulos/Cadastro", { mod_id: id, mod_nome    : nome, mod_descricao: descricao, mod_image: imagem }, "Post", function (imagemUrl) {
        location.reload();
    });
}

function AlterarModuloCadastrado(id) {
    EnviarRequisicao("/ConfiguracaoModulos/SelectId", { id: id }, "Post", function (item) {
        $("#mod_id").val(item.mod_id);
        $("#mod_nome").val(item.mod_nome);
        $("#mod_descricao").val(item.mod_descricao);
        $("#conf_imagempequena").val(item.mod_image);
        $("#image_pequena").attr('src', item.mod_image.replace("~",""));
        
        $('#modalCadastroModulo').modal({ backdrop: 'static', keyboard: true })
    });
}


function DeletarModuloCadastrado(id) {
    Confirmacao("Atenção", "Tem certeza que quer remover o modulo ", function (retorno) {
        EnviarRequisicao("/ConfiguracaoModulos/Deletar", { mod_id: id }, "Post", function (item) {
            location.reload();
        });
    });
}


function CadastrarModulosPages(idModulo) {
    $("#mod_id").val(idModulo);
    $('#modalCadastroModuloPages').modal({ backdrop: 'static', keyboard: true });
}


function CadastrarModuloPages() {
    var modPage_id = $("#modPage_id").val();
    var mod_id = $("#mod_id").val();
    var modPage_nome = $("#modPage_nome").val();
    var modPage_descricao = $("#modPage_descricao").val();
    var modPage_icone = $("#modPage_icone").val();
    var modPage_cadTitulo = $("#modPage_cadTitulo").val();
    var modPage_url = $("#modPage_url").val();

    EnviarRequisicao("/ConfiguracaoModulos/CadastroPage", { modPage_id: modPage_id, mod_id: mod_id, modPage_nome: modPage_nome, modPage_descricao: modPage_descricao, modPage_icone: modPage_icone, modPage_cadTitulo: modPage_cadTitulo, modPage_url: modPage_url }, "Post", function (imagemUrl) {
        location.reload();
    });
}

function DeletarPageModulo(id) {
    Confirmacao("Atenção", "Tem certeza que quer remover a pagina ", function (retorno) {
        EnviarRequisicao("/ConfiguracaoModulos/DeletarPagina", { modPage_id: id }, "Post", function (item) {
            location.reload();
        });
    });
}