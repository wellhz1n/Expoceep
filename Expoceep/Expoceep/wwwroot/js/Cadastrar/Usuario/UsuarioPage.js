$(document).ready(() => {

    $(".cpf").mask('000.000.000-00');

});
$(document).on("click", "#btnNovo", () => {
    Adicionar("#Adicionar", "#Listagem");
});
$(document).on("click", "#btnCancelar", () => {
    Cancelar("#Adicionar", "#Listagem");
});
$(document).on("click", "#btnSalvar", async () => {
    let user = $("#Usuario").serializeArray();
    if (checarNulos(user, [1,2])) {
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
                await $('#dtBasicExample').DataTable().ajax.reload();
                $("#Usuario")[0].reset();
                Cancelar("#Adicionar", "#Listagem");
            }
        });
    }


}); 