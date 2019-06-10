( function ( $ ) {
    "use strict";


// const brandPrimary = '#20a8d8'
const brandSuccess = '#4dbd74'
const brandInfo = '#63c2de'
const brandDanger = '#f86c6b'

function convertHex (hex, opacity) {
  hex = hex.replace('#', '')
  const r = parseInt(hex.substring(0, 2), 16)
  const g = parseInt(hex.substring(2, 4), 16)
  const b = parseInt(hex.substring(4, 6), 16)

  const result = 'rgba(' + r + ',' + g + ',' + b + ',' + opacity / 100 + ')'
  return result
}

function random (min, max) {
  return Math.floor(Math.random() * (max - min + 1) + min)
}

    var elements = 27
    var data1 = []
    var data2 = []
    var data3 = []

    for (var i = 0; i <= elements; i++) {
      data1.push(random(50, 200))
      data2.push(random(80, 100))
      data3.push(65)
    }

    var ctx = document.getElementById("trafficChart");

    EnviarRequisicao("/Home/FaturamentoAnual", {}, "GET", function (res) {
        if (res != null) {
            var ano = [];
            var valor = [];

            $.each(res, function (x, y) {
                ano.push(y.ANO);
                valor.push(y.VALOR);
            })

            var maiorValor = valor.map(Number).reduce(function (a, b) {
                return Math.max(a, b);
            });

            var myChart = new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: ano,
                    datasets: [
                    {
                        label: 'Faturamento Anual',
                        backgroundColor: '#20a8d8',
                        borderColor: brandInfo,
                        pointHoverBackgroundColor: '#fff',
                        borderWidth: 2,
                        data: valor,
                    }
                    ]
                },
                options: {
                    maintainAspectRatio: true,
                    legend: {
                        display: false
                    },
                    responsive: true,
                    scales: {
                        xAxes: [{
                            gridLines: {
                                drawOnChartArea: false
                            }
                        }],
                        yAxes: [{
                            ticks: {
                                beginAtZero: true,
                                maxTicksLimit: 5,
                                stepSize: Math.ceil(maiorValor / valor.length),
                                max: maiorValor
                            },
                            gridLines: {
                                display: true
                            }
                        }]
                    },
                    elements: {
                        point: {
                            radius: 0,
                            hitRadius: 10,
                            hoverRadius: 4,
                            hoverBorderWidth: 3
                        }
                    }


                }
            });
        }
    }, false);

    EnviarRequisicao("/Home/FaturamentoMes", {}, "GET", function (res) {
        
        if (res != null) {
            var mes = [];
            var valor = [];

            if (res.length > 0) {
                for (var i = 0; i < res.length; i++) {
                    mes.push(CarregaMes(res[i].ANO));
                    valor.push(res[i].VALOR);
                }

                var item = res[res.length - 1].VALOR;

                $("#relatoriovalormensal").html("R$ " + mascaraValor(item.toFixed(2)));
            }

            var ctx = document.getElementById("widgetChart1");
            ctx.height = 150;
            var myChart = new Chart(ctx, {
                type: 'line',
                data: {
                    labels: mes,
                    type: 'line',
                    datasets: [{
                        data: valor,
                        label: 'Valor R$',
                        backgroundColor: 'transparent',
                        borderColor: 'rgba(255,255,255,.55)',
                    }, ]
                },
                options: {

                    maintainAspectRatio: false,
                    legend: {
                        display: false
                    },
                    responsive: true,
                    tooltips: {
                        mode: 'index',
                        titleFontSize: 12,
                        titleFontColor: '#000',
                        bodyFontColor: '#000',
                        backgroundColor: '#fff',
                        titleFontFamily: 'Montserrat',
                        bodyFontFamily: 'Montserrat',
                        cornerRadius: 3,
                        intersect: false,
                    },
                    scales: {
                        xAxes: [{
                            gridLines: {
                                color: 'transparent',
                                zeroLineColor: 'transparent'
                            },
                            ticks: {
                                fontSize: 2,
                                fontColor: 'transparent'
                            }
                        }],
                        yAxes: [{
                            display: false,
                            ticks: {
                                display: false,
                            }
                        }]
                    },
                    title: {
                        display: false,
                    },
                    elements: {
                        line: {
                            borderWidth: 1
                        },
                        point: {
                            radius: 4,
                            hitRadius: 10,
                            hoverRadius: 4
                        }
                    }
                }
            });
        }
    }, false)

    EnviarRequisicao("/Home/FaturamentoAnual", {}, "GET", function (res) {
        if (res != null) {
            var ano = [];
            var valor = [];

            $.each(res, function (x, y) {
                ano.push(y.ANO);
                valor.push(y.VALOR);
            })

            var maiorValor = valor.map(Number).reduce(function (a, b) {
                return Math.max(a, b);
            });

            var item = res[res.length - 1].VALOR;

            $("#relatoriovaloranual").html("R$ " + mascaraValor(item.toFixed(2)));

            var ctx = document.getElementById("widgetChart2");
            ctx.height = 150;
            var myChart = new Chart(ctx, {
                type: 'line',
                data: {
                    labels: ano,
                    type: 'line',
                    datasets: [{
                        data: valor,
                        label: 'Valor Anual',
                        backgroundColor: '#63c2de',
                        borderColor: 'rgba(255,255,255,.55)',
                    }, ]
                },
                options: {

                    maintainAspectRatio: false,
                    legend: {
                        display: false
                    },
                    responsive: true,
                    tooltips: {
                        mode: 'index',
                        titleFontSize: 12,
                        titleFontColor: '#000',
                        bodyFontColor: '#000',
                        backgroundColor: '#fff',
                        titleFontFamily: 'Montserrat',
                        bodyFontFamily: 'Montserrat',
                        cornerRadius: 3,
                        intersect: false,
                    },
                    scales: {
                        xAxes: [{
                            gridLines: {
                                color: 'transparent',
                                zeroLineColor: 'transparent'
                            },
                            ticks: {
                                fontSize: 2,
                                fontColor: 'transparent'
                            }
                        }],
                        yAxes: [{
                            display: false,
                            ticks: {
                                display: false,
                            }
                        }]
                    },
                    title: {
                        display: false,
                    },
                    elements: {
                        line: {
                            tension: 0.00001,
                            borderWidth: 1
                        },
                        point: {
                            radius: 4,
                            hitRadius: 10,
                            hoverRadius: 4
                        }
                    }
                }
            });

        }
    }, false);

    EnviarRequisicao("/Home/FaturamentoPagamento", {}, "GET", function (res) {
        if (res != null) {
            if (res.Pendente != undefined && res.Pendente.length > 0) {
                $("#relatoriovalorpendente").html("R$ " + mascaraValor(res.Pendente[0].VALOR.toFixed(2)));
                $("#relatoriovalorpendenteqtde").html(res.Pendente[0].QTDE);
                $("#totalpagamentopendentes").html("R$ " + mascaraValor(res.Pendente[0].VALOR.toFixed(2)));
            }
            else {
                $("#relatoriovalorpendente").html("R$ 0,00");
                $("#relatoriovalorpendenteqtde").html(0);
                $("#totalpagamentopendentes").html("R$ 0,00");
            }
            if (res.Atrasados != undefined && res.Atrasados.length > 0) {
                $("#relatoriovaloratrasado").html("R$ " + mascaraValor(res.Atrasados[0].VALOR.toFixed(2)));
                $("#relatoriovaloratrasadoqtde").html(res.Atrasados[0].QTDE);
                $("#totalpagamentoatrasados").html("R$ " + mascaraValor(res.Atrasados[0].VALOR.toFixed(2)));
            }
            else {
                $("#relatoriovaloratrasado").html("R$ 0,00");
                $("#relatoriovaloratrasadoqtde").html(0);
                $("#totalpagamentoatrasados").html("R$ 0,00");
            }
            if (res.Baixados != undefined && res.Baixados.length > 0) {
                $("#totalpagamentoefetuados").html("R$ " + mascaraValor(res.Baixados[0].VALOR.toFixed(2)));
            }
            else {
                $("#totalpagamentoefetuados").html("R$ 0,00");
            }
        }
    });


} )( jQuery );