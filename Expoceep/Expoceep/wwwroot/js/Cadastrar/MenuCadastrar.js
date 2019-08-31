$(document).on("click", "#menu-produto", () => {

    MudaUrl(null, "ProdutoCadastrar");

});
$(document).on("click", "#menu-usuario", () => {

    MudaUrl(null, "UsuarioCadastrar");

});
$(document).on("click", "#menu-materiais", () => {

    MudaUrl(null, "MateriaisCadastrar");

});
$(document).on("click", "#menu-clientes", () => {

    MudaUrl(null, "ClientesCadastrar");

});
$(document).on("click", "#menu-categoriaproduto", () => {

    MudaUrl(null, "CategoriaProdutoCadastrar");

});
//USUARIOTABLE
$(document).ready(function () {
    $('#dtBasicExample').DataTable({
        "language": {
            "lengthMenu": "Exibe _MENU_ Registros por página",
            "search": "Procurar",
            "paginate": { "previous": "Retorna", "next": "Avança" },
            "zeroRecords": "Nada foi encontrado",
            "info": "Exibindo página _PAGE_ de _PAGES_",
            "infoEmpty": "Sem registros",
            "infoFiltered": "(filtrado de _MAX_ regitros totais)"
        },
        "processing": true,
        "serverSide": true,
        "filter": true, // habilita o filtro(search box)
        "lengthMenu": [[3, 5, 10, 25, 50, -1], [3, 5, 10, 25, 50, "Todos"]],
        "pageLength": 3,
        "ajax": {
            "url": "/"+GetController() + "/GetUsuariosTable",
            "type": "POST",
            "datatype": "json"
        },
        "columns": [
            { "data": "Nome", "title": "Nome", "autowidth": true },
            { "data": "Login", "title": "Login", "autowidth": true }
        ]
    });
    $('.dataTables_length').addClass('bs-select');
});

