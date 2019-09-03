var tabela;
var Editando = false;
$(document).ready(async () => {
    tabela = await Tabela("dtProduto", "GetProdutosTable");
    Produto = tabela[1];
    $(".cpf").mask('000.000.000-00');
    //toastr.info('Implementar notificações', "Info", { timeOut: 2000 });


});


$(document).on("click", "#btnNovo", () => {
    Adicionar("#Adicionar", "#Listagem");
});
$(document).on("click", "#btnCancelar", async () => {
    await Cancelar("#Adicionar", "#Listagem");
    Editando = false;
});
$(document).on("click", "#btnSalvar", async () => {
    let produto = $("#Produto").serializeArray();
    if (checarNulos(produto, [0,5]) || Editando) {
        Produto = {
            Id: produto[0].value,
            Codigo: null,
            Nome: produto[1].value,
            Preco: produto[2].value,
            Unidades: produto[3].value,
            Tamanho: produto[4].value
        }
        await BloquearTela
        await $.post("/" + GetController() + "/SalvarProduto", { prod:Produto, editando: Editando }, async (e) => {
            if (e) {
                toastr.success("Salvo Com Sucesso", "Sucesso", { timeOut: 2000 })
                await $('#dtProduto').DataTable().ajax.reload();
                await Cancelar("#Adicionar", "#Listagem");
                await DesbloquearTela();
            }
        });
    }


});
///POR HORA TEM QUE COLOCAR ISSO EM TODOS,NAO CONSEGUI AUTOMATIZAR ENTAO E OBRIGATORIO PARA DELETAR E EDITAR
$('#dtProduto tbody').on('click', 'tr', function () {
    if ($(this).hasClass('selected')) {
        $(this).removeClass('selected');
        Produto = null;
    }
    else {
        tabela.$('tr.selected').removeClass('selected');
        $(this).addClass('selected');
        Produto = tabela.row(this).data();
    }
});
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
$(document).on("click", "#btnDeletar", async () => {

    if (Produto != null) {

        $.post("/" + GetController() + "/DeletarProduto", { prod: Produto }, async (retorno) => {
            if (retorno) {
                await $('#dtProduto').DataTable().ajax.reload();
                Produto = null;
                await toastr.success("Produto Apagado", "Sucesso", { timeOut: 2000 });
            }
        });


    }
    else
        toastr.warning("Selecione um registro", "Deletar", { timeOut: 2000 });

});
$(document).on("click", "#btnEditar", async () => {
    BloquearTela();
    if (Produto != null) {
        ValorInput(Produto, "Produto");
        Adicionar("#Adicionar", "#Listagem");
        Editando = true;
    }
    else
        toastr.warning("Selecione um registro", "Editar", { timeOut: 2000 });

    DesbloquearTela();



});
