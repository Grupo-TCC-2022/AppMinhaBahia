// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// Completar o autocomplete de cidade
$(document).ready(function () {
    $(function () {
        $.ajax({
            url: '/Cidade/GetCidades'
        }).done(function (data) {
            listaNomeCidade = [];
            for (var i = 0, len = data.length; i < len; i++){
                listaNomeCidade.push(data[i].nome);
            }
            console.log(listaNomeCidade);
            $('#NomeCidadeAutoCompletar').autocomplete({
                source: listaNomeCidade,
                minLenght: 3
            })
        });
    });
});