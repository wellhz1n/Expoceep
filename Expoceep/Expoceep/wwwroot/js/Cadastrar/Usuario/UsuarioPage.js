var tabela;
$(document).ready(async () => {
    Usuario = ResetarObjeto(Usuario);
    Usuario.Editando = false;
    await BloquearTela();
    tabela = await Tabela("dtUsuario", "GetUsuariosTable");
    $(".cpf").mask('000.000.000-00');
    //toastr.info('Implementar notificações', "Info", { timeOut: 2000 });
    await DesbloquearTela();

});


$(document).on("click", "#btnNovo", () => {
    Adicionar("#Adicionar", "#Listagem");
    Usuario.Novo = true;
});
$(document).on("click", "#btnCancelar", async () => {
    await Cancelar("#Adicionar", "#Listagem");
    Usuario = ResetarObjeto(Usuario);
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
        ColocarValorUsuario(user);
    }
    await BloquearTela();
    await $.post("/" + GetController() + "/SalvarUsuario", { usuario: Usuario }, async (e) => {
        if (e) {
            await $('#dtUsuario').DataTable().ajax.reload();
            toastr.success("Salvo Com Sucesso", "Sucesso", { timeOut: 2000 })
            await Cancelar("#Adicionar", "#Listagem");
            //await DesbloquearTela();
        }

        await DesbloquearTela();

    });
});
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





///POR HORA TEM QUE COLOCAR ISSO EM TODOS,NAO CONSEGUI AUTOMATIZAR ENTAO E OBRIGATORIO PARA DELETAR E EDITAR

$('#dtUsuario tbody').on('click', 'tr', function () { //

    if ($(this).hasClass('selected')) {
        $(this).removeClass('selected');
        Usuario = ResetarObjeto(Usuario);
    }
    else {
        tabela.$('tr.selected').removeClass('selected');
        $(this).addClass('selected');
        Usuario = tabela.row(this).data();
    }
});
$('#dtUsuario tbody').on('dblclick ', 'tr', function () {
    Usuario = tabela.row(this).data();
    if (!ObjetoENulo(Usuario)) {
        ValorInput(Usuario, "Usuario");
        Adicionar("#Adicionar", "#Listagem");
        Usuario.Editando = true;
        $("#btnSalvar").text("Salvar");
    }
    else
        toastr.warning("Selecione um registro", "Editar", { timeOut: 2000 });


});
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
$(document).on("click", "#btnDeletar", async () => {

    if (!ObjetoENulo(Usuario)) {

        $.post("/" + GetController() + "/DeletarUsuario", { usuario: Usuario }, async (retorno) => {
            if (retorno) {
                await $('#dtUsuario').DataTable().ajax.reload();
                Usuario = null;
                await toastr.success("Usuario Apagado", "Sucesso", { timeOut: 2000, preventDuplicates: true, progressBar: true });
            }
        });


    }
    else
        toastr.warning("Selecione um registro", "Deletar", { timeOut: 2000, progressBar: true, preventDuplicates: true });

});
$(document).on("click", "#btnEditar", async () => {
    //BloquearTela();
    if (!ObjetoENulo(Usuario)) {
        ValorInput(Usuario, "Usuario");
        Adicionar("#Adicionar", "#Listagem");
        EscondeElemento("#camposenha");
        Usuario.Editando = true;
        $("#btnSalvar").text("Salvar");

    }
    else
        toastr.warning("Selecione um registro", "Editar", { timeOut: 2000 });

    //DesbloquearTela();



});

function ColocarValorUsuario(user) {
    Usuario.id = user[0].value;
    Usuario.Nome = user[1].value;
    Usuario.Login = user[2].value;
    Usuario.Senha = user[3].value;
    Usuario.Email = user[4].value;
    Usuario.Cpf = user[5].value;
}


// Validação JS -/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/
tiraEspacoDosInputs("Usuario", false)


// -/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/