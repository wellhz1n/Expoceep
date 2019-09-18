﻿var tabela;
$(document).ready(async () => {
    Cliente = ResetarObjeto(Cliente);
    Cliente.Editando = false;
    await BloquearTela();
    tabela = await Tabela("dtUsuario", "GetUsuariosTable");
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
    let user = $("#Usuario").serializeArray();

    /* Começo da validação adicional */
    var variavelValidacao = true;
    if (await validar('email', ["O email deve conter um @ e nenhum caracter especial depois do mesmo.", "Preencha o campo de e-mail corretamente."]) ||
        await validar('cpf', ["Preencha todo o campo de cpf para continuar.", "O cpf deve ser preenchido!"])) {
        variavelValidacao = false;
    }
    /* Fim da validação adicional 
     */

    if (checarNulos(user, [0]) && variavelValidacao) {
        Cliente = {
            id: user[0].value,
            Nome: user[1].value,
            Login: user[2].value,
            Senha: user[3].value,
            Email: user[4].value,
            Cpf: user[5].value
        }
        await BloquearTela();
        await $.post("/" + GetController() + "/SalvarUsuario", { usuario: Cliente}, async (e) => {
            if (e) {
                await $('#dtUsuario').DataTable().ajax.reload();
                toastr.success("Salvo Com Sucesso", "Sucesso", { timeOut: 2000 })
                await Cancelar("#Adicionar", "#Listagem");
                //await DesbloquearTela();
            }

            await DesbloquearTela();

        });
    }
    //var formAtual = $('.atualizaForm')[0].children[0].children;
    //for (let i1 = 0; i1 < formAtual.length; i1++) {
    //    //if (formAtual[i1].tagName == "INPUT") {
    //    //    //formAtual[i1].change(function() {
    //    //    //    checarNulos(user, [0])
    //    //    //});
    //    //    debugger
    //    //}
    //    $('.atualizaForm')[0].children[0].children(".form-control").change(function () {
    //        checarNulos(user, [0]);
    //    });
    //}




});
///POR HORA TEM QUE COLOCAR ISSO EM TODOS,NAO CONSEGUI AUTOMATIZAR ENTAO E OBRIGATORIO PARA DELETAR E EDITAR

$('#dtUsuario tbody').on('click', 'tr', function () { //
    
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
$('#dtUsuario tbody').on('dblclick ', 'tr', function () {
    Cliente = tabela.row(this).data();
    if (!ObjetoENulo(Cliente)) {
        ValorInput(Cliente, "Usuario");
        Adicionar("#Adicionar", "#Listagem");
        Cliente.Editando = true;
    }
    else
        toastr.warning("Selecione um registro", "Editar", { timeOut: 2000 });


});
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
$(document).on("click", "#btnDeletar", async () => {

    if (!ObjetoENulo(Cliente)) {

        $.post("/" + GetController() + "/DeletarUsuario", { usuario: Cliente }, async (retorno) => {
            if (retorno) {
                await $('#dtUsuario').DataTable().ajax.reload();
                Cliente = null;
                await toastr.success("Usuario Apagado", "Sucesso", { timeOut: 2000, preventDuplicates: true, progressBar: true });
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
        ValorInput(Cliente, "Usuario");
        Adicionar("#Adicionar", "#Listagem");
        EscondeElemento("#camposenha");
        Cliente.Editando = true;
    }
    else
        toastr.warning("Selecione um registro", "Editar", { timeOut: 2000 });

    //DesbloquearTela();



});
