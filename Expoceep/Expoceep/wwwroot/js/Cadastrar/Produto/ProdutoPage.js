var tabela;
var Editando = false;
$(document).ready(async () => {
    await BloquearTela();
    tabela = await Tabela("dtProduto", "GetProdutosTable");
    Produto = tabela[1];
    $(".cpf").mask('000.000.000-00');
    await DesbloquearTela();
    //toastr.info('Implementar notificações', "Info", { timeOut: 2000 });


});


$(document).on("click", "#btnNovo", () => {
    Adicionar("#Adicionar", "#Listagem");
    EscondeElemento("#CampoUsuarioCodigo");

});
$(document).on("click", "#btnCancelar", async () => {
    await Cancelar("#Adicionar", "#Listagem");
    Editando = false;
    Produto = null;
});
$(document).on("click", "#btnSalvar", async () => {
    let produto = $("#Produto").serializeArray();
    let produtopropriedade = SerialiazaGrupoForm($(".Produtopropriedade"));
    produtopropriedade.pop();
    let propriedadestemp = [];
    for (var i = 0; i < produtopropriedade.length; i++) {
        let copia = $.extend(true, {}, ProdutoPropriedades);
        copia.Tamanho = produtopropriedade[i][0].value;
        copia.Preco = produtopropriedade[i][1].value;
        copia.Unidades = produtopropriedade[i][2].value;
        propriedadestemp.push(copia);
    }

    debugger;
    //if (checarNulos(produto,[0, 1, 5]) || Editando) {
    Produto = {
        Id: produto[0].value,
        Codigo: produto[1].value,
        Nome: produto[2].value
        //Preco: produto[3].value,
        //Unidades: produto[4].value,
        //Tamanho: produto[5].value
        
        }
        
    await BloquearTela();
    await $.post("/" + GetController() + "/SalvarProduto", { prod: Produto, editando: Editando }, async (e) => {
        if (e) {
            toastr.success("Salvo Com Sucesso", "Sucesso", { timeOut: 2000 })
            await $('#dtProduto').DataTable().ajax.reload();
            await Cancelar("#Adicionar", "#Listagem");
            await DesbloquearTela();
        }
        else {
            await DesbloquearTela();
        }
    });
    //}


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
$('#dtProduto tbody').on('dblclick ', 'tr', function () {
    Produto = tabela.row(this).data();
    AparecerElemento("#CampoUsuarioCodigo");
    if (Produto != null) {
        ValorInput(Produto, "Produto");
        $("#Tamanhoselect option:eq(" + Produto.Tamanho.value + ")").prop('selected', true);
        Adicionar("#Adicionar", "#Listagem");
        Editando = true;
    }
    else
        toastr.warning("Selecione um registro", "Editar", { timeOut: 2000 });

});

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
$(document).on("click", "#btnDeletar", async () => {

    if (Produto != null) {
        $.post("/" + GetController() + "/DeletarProduto", { prod: Produto }, async (retorno) => {
            await BloquearTela();
            if (retorno) {
                await $('#dtProduto').DataTable().ajax.reload();
                Produto = null;
                await toastr.success("Produto Apagado", "Sucesso", { timeOut: 2000, preventDuplicates: true, progressBar: true });
            }
            await DesbloquearTela();
        });

    }
    else
        toastr.warning("Selecione um registro", "Deletar", { timeOut: 2000, preventDuplicates: true, progressBar: true });

});
$(document).on("click", "#btnEditar", async () => {
    //BloquearTela();
    AparecerElemento("#CampoUsuarioCodigo");
    if (Produto != null) {
        ValorInput(Produto, "Produto");
        $("#Tamanhoselect option:eq(" + Produto.Tamanho.value + ")").prop('selected', true);
        await Adicionar("#Adicionar", "#Listagem");
        Editando = true;
        setaSelect(Produto, '#Tamanhoselect', 'Tamanho')
    }
    else
        toastr.warning("Selecione um registro", "Editar", { timeOut: 2000 });

    //DesbloquearTela();



});

function setaSelect(obj, select) {
    var seletorCriancas = $(select).children();
    if (typeof select == 'object') {
    } else if (typeof select == 'string') {
        for (let i = 0; i < seletorCriancas.length; i++) {
            if (seletorCriancas[i].value == obj['Tamanho']) {
                seletorCriancas[i].selected = true;
            } else {
                seletorCriancas[i].selected = false;
                console.log(seletorCriancas[i])
            }
        }
    }
    
    
}