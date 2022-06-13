// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    // Funcao de autocomplete de campo de Cidades
    $(function () {
        $.ajax({
            url: '/Cidade/GetCidades'
        }).done(function (cidades) {
            listaNomeValorCidade = [];
            for (var i = 0, len = cidades.length; i < len; i++){

                listaNomeValorCidade.push(
                    {
                    "label": cidades[i].nome,
                    "value": cidades[i].instituicaoID
                    }
                );
            }

            let nomeCidadeAutoCompletar = $('.nomeCidadeAutoCompletar');
            let inputNumericoDeIdCidade = $('.cidadeResidenciaID');

            if (nomeCidadeAutoCompletar != null){
                nomeCidadeAutoCompletar.autocomplete({
                    source: listaNomeValorCidade,
                    minLenght: 3,
                    select: function (event, ui){
                        event.preventDefault();
                        var selectedObj = ui.item;
                        inputNumericoDeIdCidade.val(selectedObj.value);
                        nomeCidadeAutoCompletar.val(selectedObj.label);  
                    },
                    focus: function (event, ui){
                        event.preventDefault();
                        var selectedObj = ui.item;
                        inputNumericoDeIdCidade.val(selectedObj.value);
                        nomeCidadeAutoCompletar.val(selectedObj.label);  
                    }
                });
            }
            
            let nomeCidadeGovernoAutoCompletar = $('.nomeCidadeGovernoAutoCompletar');
            let inputNumericoDeIdCidadeGoverno = $(".cidadeGovernoID");

            if (nomeCidadeGovernoAutoCompletar != null){
                nomeCidadeGovernoAutoCompletar.autocomplete({
                    source: listaNomeValorCidade,
                    minLenght: 3,
                    select: function (event, ui){
                        event.preventDefault();
                        var selectedObj = ui.item;
                        inputNumericoDeIdCidadeGoverno.val(selectedObj.value);
                        nomeCidadeGovernoAutoCompletar.val(selectedObj.label);  
                    },
                    focus: function (event, ui){
                        event.preventDefault();
                        var selectedObj = ui.item;
                        inputNumericoDeIdCidadeGoverno.val(selectedObj.value);
                        nomeCidadeGovernoAutoCompletar.val(selectedObj.label);  
                    }
                });
            }
        });
    });

    let tabela = $('#table');
    if (tabela) {
        tabela.DataTable();
    }
});
