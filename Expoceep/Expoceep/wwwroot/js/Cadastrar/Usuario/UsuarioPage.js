var Usuario = {
    id: null,
    Nome: null,
    Login: null,
    Senha: null,
    Email: null,
    Cpf: null
}
var tabela;
var Editando = false;
$(document).ready(async () => {
    tabela = await Tabela("dtUsuario", "GetUsuariosTable");
    Usuario = tabela[1];
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
    let user = $("#Usuario").serializeArray();
    if (checarNulos(user,[0]) || Editando) {
        Usuario = {
            id: user[0].value,
            Nome: user[1].value,
            Login: user[2].value,
            Senha: user[3].value,
            Email: user[4].value,
            Cpf: user[5].value
        }
        await BloquearTela
        await $.post("/" + GetController() + "/SalvarUsuario", { usuario: Usuario, editando: Editando }, async (e) => {
            if (e) {
                toastr.success("Salvo Com Sucesso", "Sucesso", { timeOut: 2000 })
                await $('#dtUsuario').DataTable().ajax.reload();
                await Cancelar("#Adicionar", "#Listagem");
                await DesbloquearTela();
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

        $.post("/" + GetController() + "/DeletarUsuario", { usuario: Usuario }, async (retorno) => {
            if (retorno) {
                await $('#dtUsuario').DataTable().ajax.reload();
                Usuario = null;
                await toastr.success("Usuario Apagado", "Sucesso", { timeOut: 2000 });
            }
        });


    }
    else
        toastr.warning("Selecione um registro", "Deletar", { timeOut: 2000 });

});
$(document).on("click", "#btnEditar", async () => {
     BloquearTela();
    if (Usuario != null) {
        ValorInput(Usuario, "Usuario");
        Adicionar("#Adicionar", "#Listagem");
        Editando = true;
    }
    else
        toastr.warning("Selecione um registro", "Editar", { timeOut: 2000 });

     DesbloquearTela();



});
