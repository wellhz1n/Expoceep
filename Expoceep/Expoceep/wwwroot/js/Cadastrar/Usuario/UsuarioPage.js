var Usuario = {
    id: null,
    Nome: null,
    Login: null,
    Senha: null,
    Email: null,
    Cpf: null
}
var tabela;
$(document).ready(async () => {
    tabela = await Tabela("dtUsuario", "GetUsuariosTable");
    Usuario = tabela[1];
    $(".cpf").mask('000.000.000-00');
    toastr.info('Implementar notificações', "Info", { timeOut: 2000 });

});


$(document).on("click", "#btnNovo", () => {
    Adicionar("#Adicionar", "#Listagem");
});
$(document).on("click", "#btnCancelar", async () => {
    await Cancelar("#Adicionar", "#Listagem");
});
$(document).on("click", "#btnSalvar", async () => {
    let user = $("#Usuario").serializeArray();
    if (checarNulos(user)) {
        Usuario = {
            id: 0,
            Nome: user[0].value,
            Login: user[1].value,
            Senha: user[2].value,
            Email: user[3].value,
            Cpf: user[4].value
        }
        await $.post("/" + GetController() + "/SalvarUsuario", { usuario: Usuario }, async (e) => {
            if (e) {
                toastr.success("Salvo Com Sucesso", "Sucesso", { timeOut: 2000 })
                await $('#dtUsuario').DataTable().ajax.reload();
                await Cancelar("#Adicionar", "#Listagem");
            }
        });
    }


});
///POR HORA TEM QUE COLOCAR ISSO EM TODOS,NAO CONSEGUI AUTOMATIZAR ENTAO E OBRIGATORIO PARA DELETAR E EDITAR
$('#dtUsuario tbody').on('click', 'tr', function () {
    if ($(this).hasClass('selected')) {
        $(this).removeClass('selected');
        Usuario = null;
    }
    else {
        tabela.$('tr.selected').removeClass('selected');
        $(this).addClass('selected');
        Usuario = tabela.row(this).data();
    }
});
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
$(document).on("click", "#btnDeletar", async () => {

    if (Usuario != null) {

        $.post("/" + GetController() +"/DeletarUsuario", { usuario: Usuario }, async (retorno) => {
            if (retorno) {
                await $('#dtUsuario').DataTable().ajax.reload();
                await toastr.success("Usuario Apagado", "Sucesso", { timeOut: 2000 });
            }
        });


    }
    else
        toastr.warning("Selecione um registro", "Deletar", { timeOut: 2000 });

});
