$(document).ready(() => {

    $(".cpf").mask('000.000.000-00');
toastr.info('Implementar notificações');
//TODO
//https://github.com/CodeSeven/toastr
 //FAZER SISTEMA DE NOTIFICACOES PARA USAR NO JS E NO C# 
//https://chrissainty.com/blazor-toast-notifications-using-only-csharp-html-css/
});
$(document).on("click", "#btnNovo", () => {
    Adicionar("#Adicionar","#Listagem");
}); 
$(document).on("click", "#btnCancelar", () => {
    Cancelar("#Adicionar", "#Listagem");
});
$(document).on("click", "#btnSalvar", async () => {
    let user = $("#Usuario").serializeArray();
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
   
   
}); 