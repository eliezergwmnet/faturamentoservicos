
jQuery(document).ready(function($) {
	"use strict";

	$(".nk-int-st")[0] && ($("body").on("focus", ".nk-int-st .form-control", function () {
	    $(this).closest(".nk-int-st").addClass("nk-toggled")
	}), $("body").on("blur", ".form-control", function () {
	    var p = $(this).closest(".form-group, .input-group"),
            i = p.find(".form-control").val();
	    p.hasClass("fg-float") ? 0 == i.length && $(this).closest(".nk-int-st").removeClass("nk-toggled") : $(this).closest(".nk-int-st").removeClass("nk-toggled")
	})), $(".fg-float")[0] && $(".fg-float .form-control").each(function () {
	    var i = $(this).val();
	    0 == !i.length && $(this).closest(".nk-int-st").addClass("nk-toggled")
	});



	MarcarasInput();


	[].slice.call( document.querySelectorAll( 'select.cs-select' ) ).forEach( function(el) {
		new SelectFx(el);
	} );

	jQuery('.selectpicker').selectpicker;

    $('body').toggleClass('open');
	$('#menuToggle').on('click', function(event) {
		$('body').toggleClass('open');
	});

	$('.search-trigger').on('click', function(event) {
		event.preventDefault();
		event.stopPropagation();
		$('.search-trigger').parent('.header-left').addClass('open');
	});

	$('.search-close').on('click', function(event) {
		event.preventDefault();
		event.stopPropagation();
		$('.search-trigger').parent('.header-left').removeClass('open');
	});



    //Submit
    //CADASTRA Comentario	
	$('form[name="adcomentario"]').submit(function () {
	    var forma = $(this);
	    $(this).ajaxSubmit({
	        url: urlaction,
	        data: { acao: 'send_comentario' },
	        beforeSubmit: function () {
	            forma.find('.load img').fadeIn("fast");
	        },
	        success: function (dados) {
	            if (dados == 'errempty') {
	                myDial('alert', '<p>Para registrarmos seu comentario é necessário o preenchimento de todos os campos. </p><p><strong>Obrigado!</strong></p>');
	            } else if (dados == 'errmail') {
	                myDial('error', '<p>Oppssss. O e-mail que você informou não tem um formato válido, assim não poderemos responder a você!</p><p><strong>Informe seu e-mail corretamente!</strong></p>');
	            } else {
	                myDial('accept', '<p> Olá <strong>' + dados + '</strong>, seu comentario foi cadastrado com sucesso, logo mais ele será publicado no site.</p>');
	                $('.dialog').one('click', '.closedial', function () {

	                    $('.msg').fadeOut("fast", function () {
	                        $(this).empty();
	                    });
	                    forma.find('input[type="text"]').val('');
	                    forma.find('input[type="file"]').val('');
	                    forma.find('textarea').val('');

	                    return false;
	                });
	            }
	        },
	        complete: function () {
	            forma.find('.load img').fadeOut("slow");
	        }
	    });
	    return false;
	});

	$('.material-button-toggle').on("click", function () {
	    $(this).toggleClass('open');
	    $('.option').toggleClass('scale-on');
	});


	$('.newmsg').fadeIn("fast");
    //Mascaras
	

	//$(".formVal").maskMoney();
	//jQuery("#end_estado").chosen({
	//    disable_search_threshold: 10,
	//    no_results_text: "Oops, nothing found!",
	//    width: "100%"
	//});

	//CarrgaMapaEndereco();
	
	ValidaCamposObrigatorios();
});

var myVar;
function myFunction() {
}

//Input File Upload
//Tipo de imagem para salvar na pasta
//Imagem que vai receber o retorno
//text input que vai receber o valor para salvar no banco
function SalvarImagemServidor(input, tipo, imagem, text) {

    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#' + imagem).attr('src', "http://upload.wikimedia.org/wikipedia/commons/d/de/Ajax-loader.gif");

            EnviarRequisicao("/Empresa/Base64ToImage", { base64String: e.target.result, tipo: tipo }, "Post", function (imagemUrl) {
                $('#' + imagem).attr('src', e.target.result);
                $('#' + text).val(imagemUrl);
            });
        }
        reader.readAsDataURL(input.files[0]);
    }
}




function MarcarasInput() {
    $(".formDate").mask("99/99/9999", { placeholder: " " });
    $(".formDateMes").mask("99/9999", { placeholder: " " });
    $(".formTime").mask("99:99", { placeholder: " " });
    $(".formFone").mask("99 9999-9999", { placeholder: " " });
    $(".formCep").mask("99999-999", { placeholder: " " });
    $(".formCel").mask("(99) 9999-9999?9", { placeholder: " " });
    $(".formInteiro").mask("9?99999", { placeholder: " " });
    $(".formCpf").mask("999.999.999-99", { placeholder: " " });
    $(".formCnpj").mask("99.999.999/9999-99", { placeholder: " " });
    $(".formDiasVencimento").mask("99", { placeholder: " " });

    $(".money").maskMoney({ prefix: 'R$ ', allowNegative: true, thousands: '.', decimal: ',', affixesStay: false });
  /*  $(".porcento").maskMoney({ prefix: '% ', allowNegative: true, thousands: '.', decimal: ',', affixesStay: false });*/
}


function EfeitoBotao(classe, botao,  ativar) {
    if (ativar) {
        $(classe).addClass('square');
        $(botao).prop("disabled", true);
    }
    else {
        $(classe).removeClass('square');
        $(botao).prop("disabled", false);
    }
}


jQuery(window).load(function () {
    /*$("#page-loader").fadeOut("slow");
    document.getElementById("myDiv").style.display = "block";*/
});

function CarrgaMapaEndereco() {
    map = new GMaps({
        el: '#basic-map',
        //lat: -12.043333,
        //lng: -77.028333,
        address:'rua dr joao aranha neto, 1120, apartamento 14, mauá, sp',
        zoomControl: true,
        zoomControlOpt: {
            style: 'SMALL',
            position: 'TOP_LEFT'
        },
        panControl: false,
        streetViewControl: false,
        mapTypeControl: false,
        overviewMapControl: false
    });
}

function myModal(id, tipo, content) {
    var title = (tipo == 'accept' ? 'Sucesso:' : (tipo == 'error' ? 'Oppsss:' : (tipo == 'alert' ? 'Atenção:' : 'null')));
    if (title == 'null') {
        alert('Tipo deve ser: accept | error | alert');
    } else {
        $('.dialog').fadeIn('fast', function () {
            $('.ajaxmsg').addClass(id).addClass(tipo).html(
                '<strong class="tt">' + title + '</strong>' +
                '<p>' + content + '</p>' +
                '<a href="#" class="closedial j_ajaxclose" id="' + id + '">X FECHAR</a>'
            ).fadeIn("slow");
        });
    }
}

function ValidaCampos(idFormValida) {
    var retorno = true;
    $.each($('form#' + idFormValida + ' input'), function (x, item) {
        if (item.classList.value.indexOf("obrigatorio") != -1)
        {
            if (item.value == undefined || item.value == null || item.value == "") {
                $(this).removeClass('is-valid');
                $(this).addClass('is-invalid');
                retorno = false;
            }
            else {
                $(this).removeClass('is-invalid');
                $(this).addClass('is-valid');
            }
        }
        else {
            $(this).removeClass('is-invalid');
            $(this).addClass('is-valid');
        }
    });

    return retorno;
}

function ValidaCamposObrigatorios() {
    $('.input-group input[required], .input-group textarea[required], .input-group select[required]').on('keyup change', function () {
        var $form = $(this).closest('form'),
            $group = $(this).closest('.input-group'),
			$addon = $group.find('.input-group-addon'),
			$icon = $addon.find('span'),
			state = false;

        if (!$group.data('validate')) {
            state = $(this).val() ? true : false;
        } else if ($group.data('validate') == "email") {
            state = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/.test($(this).val())
        } else if ($group.data('validate') == 'phone') {
            state = /^[(]{0,1}[0-9]{3}[)]{0,1}[-\s\.]{0,1}[0-9]{3}[-\s\.]{0,1}[0-9]{4}$/.test($(this).val())
        } else if ($group.data('validate') == "length") {
            state = $(this).val().length >= $group.data('length') ? true : false;
        } else if ($group.data('validate') == "number") {
            state = !isNaN(parseFloat($(this).val())) && isFinite($(this).val());
        }

        if (state) {
            //$addon.removeClass('is-invalid');
            //$addon.addClass('success');
            //$icon.attr('class', 'fa fa-ok');
            $(this).removeClass('is-invalid');
            $(this).addClass('is-valid');
            //$icon.attr('class', 'fa fa-ok');
        } else {
            //$addon.removeClass('success');
            //$addon.addClass('danger');
            //$icon.attr('class', 'fa fa-exclamation-circle');

            $(this).removeClass('is-valid');
            $(this).addClass('is-invalid');
            //$icon.attr('class', 'fa fa-exclamation-circle');
        }

        if ($('form[name="submitenviar"]').find('.is-invalid').length == 0) {
            $form.find('[type="submit"]').prop('disabled', false);
        } else {
            $form.find('[type="submit"]').prop('disabled', true);
        }
    });

    $('.input-group input[required], .input-group textarea[required], .input-group select[required]').trigger('change');

}

function onClicPaginacao(item, parametro) {
    $(item).click(function () {
        $("#page-loader").fadeIn("slow");
        var i = $(this).attr('name');
        $("#viewPlaceHolder").load(i, function () { Paginacao(parametro); $("#page-loader").fadeOut("slow"); });
    })
}

function Paginacao(parametro) {
    //Quando usar a paginação com parametros passar com o separador ex: &tipo=123
    if (parametro == undefined || parametro == null)
    {
        parametro = "";
    }
    setTimeout(function () {
        var colunas = $('.Grid-Footer > td > a');
        colunas.each(function (x, y) {
            link = y.href + parametro;
            y.href = 'javascript:';
            y.onclick = onClicPaginacao(y, parametro);
            y.name = link;
        });
        $("#page-loader").fadeOut("slow");
    }, 200)
}
/*
function Alerta(titulo, mensagem, callback, iconAviso) {
    if (iconAviso == undefined || iconAviso == null || iconAviso == "")
        iconAviso = "success";

    $('#sa-success').on('click', function () {
        swal(titulo, mensagem, "success").then(function () {
            debugger
            callback();
        })
    });
}*/
/*
function Confirmacao(titulo, mensagem, callback, iconAviso) {
    if(iconAviso == undefined || iconAviso == null || iconAviso == "")
        iconAviso = "warning";

    swal({
        title: titulo,
        text: mensagem,
        type: iconAviso,
        showCancelButton: true,
        confirmButtonText: "Sim",
        cancelButtonText: "Cancelar"
    }).then(function (res) {
        callback(res);
    });
}*/

function CarregarSelect(idCampo, url, idSelected, CallBack, url2) {
    if (url2 != undefined && url2 != null && url2 != "")
        url = url2;
    else
        url = '/_DropDown/' + url;

    EnviarRequisicao(url, {}, 'POST', function (resultCombo) {
        var retorno = '';
        var selecionado = '';
        var selecionar = true;

        if (idSelected == undefined || idSelected == null || idSelected == 0) {
            retorno = '<option value="">Selecione</option>';
            selecionar = false;
        }

        if (resultCombo.length > 0) {
            $.each(resultCombo, function (linha, item) {
                if(selecionar){
                    if (idSelected.toString() == item.Id) {
                        selecionado = 'selected = "selected"';
                    }
                    else {
                        selecionado = '';
                    }
                }
                retorno += '<option value="' + item.Id + '" ' + selecionado + '>' + item.Nome + '</option>';
            })
        }

        $("#" + idCampo).html(retorno);
        if (CallBack != undefined && CallBack != null) {
            CallBack();
        }
        else {
            return retorno;
        }
    }, true);
}

function LoadingPage(Open) {
    if(Open)
        $("#page-loader").fadeIn("slow");
    else
        $("#page-loader").fadeOut("slow");
}

function EnviarRequisicao(url, data, metodo, callback, modal) {
    if (modal != true)
        modal = false;
    $.ajax({
        url: url,
        data: data,
        type: metodo,
        beforeSend: function () {
            if (modal) {
                LoadingPage(true);
            }
        },
        success: function (retornoRequisicao) {
            callback(retornoRequisicao);
        },
        error:function(ex){
            callback(retornoRequisicao);
        },
        complete: function () {
            if (modal) {
                LoadingPage(false);
            }
        }
    });
}

function maiuscula(z) {
    v = z.value.toUpperCase();
    z.value = v;
}

function mascaraValor(valor) {
    valor = valor.toString().replace(/\D/g, "");
    valor = valor.toString().replace(/(\d)(\d{8})$/, "$1.$2");
    valor = valor.toString().replace(/(\d)(\d{5})$/, "$1.$2");
    valor = valor.toString().replace(/(\d)(\d{2})$/, "$1,$2");
    return valor;
    //return mascaraValor(valor.toFixed(2));
}

function numDiasMes(mes) {
    var objData = new Date(),
        numAno = objData.getFullYear(),
        numMes = mes,
        numDias = new Date(numAno, numMes, 0).getDate();

    return numDias;
}

function validateDate(id) {
    var RegExPattern = /^((((0?[1-9]|[12]\d|3[01])[\.\-\/](0?[13578]|1[02])      [\.\-\/]((1[6-9]|[2-9]\d)?\d{2}))|((0?[1-9]|[12]\d|30)[\.\-\/](0?[13456789]|1[012])[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}))|((0?[1-9]|1\d|2[0-8])[\.\-\/]0?2[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}))|(29[\.\-\/]0?2[\.\-\/]((1[6-9]|[2-9]\d)?(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)|00)))|(((0[1-9]|[12]\d|3[01])(0[13578]|1[02])((1[6-9]|[2-9]\d)?\d{2}))|((0[1-9]|[12]\d|30)(0[13456789]|1[012])((1[6-9]|[2-9]\d)?\d{2}))|((0[1-9]|1\d|2[0-8])02((1[6-9]|[2-9]\d)?\d{2}))|(2902((1[6-9]|[2-9]\d)?(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)|00))))$/;

    if (!((id.value.match(RegExPattern)) && (id.value != ''))) {
        return false;
    }
    else
        return true;
}


function CarregaMes(mes) {
    var mesRetorno = "";
    mes = parseInt(mes);
    switch (mes) {
        case 1:
            mesRetorno = "Janeiro";
            break;
        case 2:
            mesRetorno = "Fevereiro";
            break;
        case 3:
            mesRetorno = "MarCo";
            break;
        case 4:
            mesRetorno = "Abril";
            break;
        case 5:
            mesRetorno = "Maio";
            break;
        case 6:
            mesRetorno = "Junho";
            break;
        case 7:
            mesRetorno = "Julho";
            break;
        case 8:
            mesRetorno = "Agosto";
            break;
        case 9:
            mesRetorno = "Setembro";
            break;
        case 10:
            mesRetorno = "Outubro";
            break;
        case 11:
            mesRetorno = "Novembro";
            break;
        case 12:
            mesRetorno = "Dezembro";
            break;
    }

    return mesRetorno;
}

function validate_cnpj(val, msk) {
    val = val.replace(/[^\d]+/g, '');

    if (val == '') return false;

    if (val.length != 14)
        return false;

    // Elimina CNPJs invalidos conhecidos
    if (val == "00000000000000" ||
        val == "11111111111111" ||
        val == "22222222222222" ||
        val == "33333333333333" ||
        val == "44444444444444" ||
        val == "55555555555555" ||
        val == "66666666666666" ||
        val == "77777777777777" ||
        val == "88888888888888" ||
        val == "99999999999999")
        return false;

    // Valida DVs
    tamanho = val.length - 2
    numeros = val.substring(0, tamanho);
    digitos = val.substring(tamanho);
    soma = 0;
    pos = tamanho - 7;
    for (i = tamanho; i >= 1; i--) {
        soma += numeros.charAt(tamanho - i) * pos--;
        if (pos < 2)
            pos = 9;
    }
    resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
    if (resultado != digitos.charAt(0))
        return false;

    tamanho = tamanho + 1;
    numeros = val.substring(0, tamanho);
    soma = 0;
    pos = tamanho - 7;
    for (i = tamanho; i >= 1; i--) {
        soma += numeros.charAt(tamanho - i) * pos--;
        if (pos < 2)
            pos = 9;
    }
    resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
    if (resultado != digitos.charAt(1))
        return false;

    return true;
}

function validate_cpf(val, msk) {
    var regex = msk != undefined && msk ? /^\d{3}\.\d{3}\.\d{3}\-\d{2}$/ : /^[0-9]{11}$/;

    if (val.match(regex) != null) {
        //check all same numbers
        if (val.match(/\b(.+).*(\1.*){10,}\b/g) != null)
            return false;

        var strCPF = val.replace(/\D/g, '');
        var sum;
        var rest;
        sum = 0;

        for (i = 1; i <= 9; i++)
            sum = sum + parseInt(strCPF.substring(i - 1, i)) * (11 - i);

        rest = (sum * 10) % 11;

        if ((rest == 10) || (rest == 11))
            rest = 0;

        if (rest != parseInt(strCPF.substring(9, 10)))
            return false;

        sum = 0;
        for (i = 1; i <= 10; i++)
            sum = sum + parseInt(strCPF.substring(i - 1, i)) * (12 - i);

        rest = (sum * 10) % 11;

        if ((rest == 10) || (rest == 11))
            rest = 0;
        if (rest != parseInt(strCPF.substring(10, 11)))
            return false;

        return true;
    }

    return false;
}


