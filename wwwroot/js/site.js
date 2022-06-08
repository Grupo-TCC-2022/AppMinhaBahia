// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// Completar o autocomplete de cidade
$(document).ready(function () {
    $(function () {
        $.ajax({
            url: 'todos_usuarios'
        }).done(function (data) {
            $('#apelido_autocomplete').autocomplete({
                source: data,
                minLenght: 2
            })
        });
    });
});