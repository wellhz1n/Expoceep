var tabela;
$(document).ready(async () => {
    Cliente = ResetarObjeto(Cliente);
    Cliente.Editando = false;
    await BloquearTela();
    tabela = await Tabela("dtCliente", "GetClientesTable");
    Cliente = tabela[1];
    $(".cpf").mask('000.000.000-00');
    //toastr.info('Implementar notificações', "Info", { timeOut: 2000 });
    await DesbloquearTela();

});


$(document).on("click", "#btnNovo", () => {
    Adicionar("#Adicionar", "#Listagem");
    Cliente.Novo = true;
});
$(document).on("click", "#btnCancelar", async () => {
    await Cancelar("#Adicionar", "#Listagem");
    Cliente = ResetarObjeto(Cliente);
    AparecerElemento("#camposenha");

});
$(document).on("click", "#btnSalvar", async () => {
    let client = $("#Cliente").serializeArray();

    /* Começo da validação adicional */
    var variavelValidacao = true;
    if (await validar('email', ["O email deve conter um @ e nenhum caracter especial depois do mesmo.", "Preencha o campo de e-mail corretamente."]) ||
        await validar('cpf', ["Preencha todo o campo de cpf para continuar.", "O cpf deve ser preenchido!"])) {
        variavelValidacao = false;
    }
    /* Fim da validação adicional 
     */

    if (checarNulos(client, [0]) && variavelValidacao) {
        Cliente = {
            id: client[0].value,
            Nome: client[1].value,
            Sobrenome: client[2].value,
            Email: client[3].value,
            Cpf: client[4].value
        }
        await BloquearTela();
        await $.post("/" + GetController() + "/SalvarCliente", { cliente: Cliente }, async (e) => {
            if (e) {
                await $('#dtCliente').DataTable().ajax.reload();
                toastr.success("Salvo Com Sucesso", "Sucesso", { timeOut: 2000 })
                await Cancelar("#Adicionar", "#Listagem");
            }
            await DesbloquearTela();
        });
    }
});
///POR HORA TEM QUE COLOCAR ISSO EM TODOS,NAO CONSEGUI AUTOMATIZAR ENTAO E OBRIGATORIO PARA DELETAR E EDITAR

$('#dtCliente tbody').on('click', 'tr', function () { //

    if ($(this).hasClass('selected')) {
        $(this).removeClass('selected');
        Cliente = ResetarObjeto(Cliente);
    }
    else {
        tabela.$('tr.selected').removeClass('selected');
        $(this).addClass('selected');
        Cliente = tabela.row(this).data();
    }
});
$('#dtCliente tbody').on('dblclick ', 'tr', function () {
    Cliente = tabela.row(this).data();
    if (!ObjetoENulo(Cliente)) {
        ValorInput(Cliente, "Cliente");
        Adicionar("#Adicionar", "#Listagem");
        Cliente.Editando = true;
    }
    else
        toastr.warning("Selecione um registro", "Editar", { timeOut: 2000 });


});
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
$(document).on("click", "#btnDeletar", async () => {

    if (!ObjetoENulo(Cliente)) {

        $.post("/" + GetController() + "/DeletarCliente", { cliente: Cliente }, async (retorno) => {
            if (retorno) {
                await $('#dtCliente').DataTable().ajax.reload();
                Cliente = null;
                await toastr.success("Cliente Apagado", "Sucesso", { timeOut: 2000, preventDuplicates: true, progressBar: true });
            }
        });


    }
    else
        toastr.warning("Selecione um registro", "Deletar", { timeOut: 2000, progressBar: true, preventDuplicates: true });

});
$(document).on("click", "#btnEditar", async () => {
    //BloquearTela();
    debugger
    if (!ObjetoENulo(Cliente)) {
        ValorInput(Cliente, "Cliente");
        Adicionar("#Adicionar", "#Listagem");
        EscondeElemento("#camposenha");
        Cliente.Editando = true;
    }
    else
        toastr.warning("Selecione um registro", "Editar", { timeOut: 2000 });

    //DesbloquearTela();



});
