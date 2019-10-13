$(document).on("click", "#menu-produto", () => {

    MudaUrl(null, "ProdutoCadastrar");

});
$(document).on("click", "#menu-usuario", () => {

    MudaUrl(null, "UsuarioCadastrar");

});
$(document).on("click", "#menu-materiais", () => {

    MudaUrl(null, "MateriaisCadastrar");

});
$(document).on("click", "#menu-clientes", () => {

    MudaUrl(null, "ClientesCadastrar");

});
$(document).on("click", "#menu-categoriaproduto", () => {

    MudaUrl(null, "CategoriaProdutoCadastrar");

});
//USUARIOTABLE
$(document).ready(async function () {
    await BloquearTela();
   await $("#menucont >div >p[id='construcao']").parent().each(async (d, c) => {
       await $(c).addClass('emconstrucao');
   });
    await DesbloquearTela();
});



