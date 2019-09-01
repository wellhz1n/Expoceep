$(document).ready(() => {

    $(".cpf").mask('000.000.000-00');
    toastr.info('Implementar notificações', "Info", { timeOut: 2000 });

});
$(document).on("click", "#btnNovo", () => {
    Adicionar("#Adicionar", "#Listagem");
});
$(document).on("click", "#btnCancelar", async() => {
   await Cancelar("#Adicionar", "#Listagem");
});
$(document).on("click", "#btnSalvar", async () => {
    let user = $("#Usuario").serializeArray();
    if (checarNulos(user)) {
        let Usuario = {
            id: 0,
            Nome: user[0].value,
            Login: user[1].value,
            Senha: user[2].value,
            Email: user[3].value,
            Cpf: user[4].value
        }
        await $.post("/" + GetController() + "/SalvarUsuario", { usuario: Usuario }, async (e) => {
            if (e) {
                toastr.success("Salvo Com Sucesso", "Sucesso", { timeOut:2000 })
                await $('#dtBasicExample').DataTable().ajax.reload();
                await Cancelar("#Adicionar", "#Listagem");
            }
        });
    }


}); 