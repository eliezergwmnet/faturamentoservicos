
$(function () {
    $("#buttonTituloPagesCadastro").click(function () {
        window.location = "/Empresa/Insert";
    })
})

function salvarImagemPequena(input) {
    
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#image_pequena').attr('src', "http://upload.wikimedia.org/wikipedia/commons/d/de/Ajax-loader.gif");
            
            EnviarRequisicao("/Empresa/Base64ToImage", { base64String: e.target.result, tipo: "Empresa" }, "Post", function (imagemUrl) {
                $('#image_pequena').attr('src', e.target.result);
                $('#conf_imagempequena').val(imagemUrl);
            });
        }
        reader.readAsDataURL(input.files[0]);
    }
}

function salvarImagemGrande(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#image_grande').attr('src', "http://upload.wikimedia.org/wikipedia/commons/d/de/Ajax-loader.gif");

            EnviarRequisicao("/Empresa/Base64ToImage", { base64String: e.target.result, tipo: "Empresa" }, "Post", function (imagemUrl) {
                $('#image_grande').attr('src', e.target.result);
                $('#conf_imagemgrande').val(imagemUrl);
            });
        }
        reader.readAsDataURL(input.files[0]);
    }
}



function AlterarTipoCampo(select) {
    if (select.value == 'formCpf') {
        $("#conf_cnpj").mask("999.999.999-99", { placeholder: " " });
        $("#tipocadastro").html("CPF");
    }
    else {
        $("#conf_cnpj").mask("99.999.999/9999-99", { placeholder: " " });
        $("#tipocadastro").html("CNPJ");
    }
}


function CadastrarModulosEmpresa(id) {
    $("#conf_id").val(id);
    $('#modalCadastroModuloEmpresa').modal({ backdrop: 'static', keyboard: true });
    CarregarSelect("mod_id", "/CarregaModulos", null, null);
}

function CadastrarModuloEmpresa() {

    EnviarRequisicao("/Empresa/CadastroModuloEmpresa", { mod_id: $("#mod_id").val(), conf_id: $("#conf_id").val() }, "Post", function () {
        location.reload();
    });
}