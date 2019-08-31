$(document).ready(() => {
   

});
$(document).on("click", "#btnNovo", () => {
    Adicionar("#Adicionar","#Listagem");
}); 
$(document).on("click", "#btnCancelar", () => {
    Cancelar("#Adicionar", "#Listagem");
}); 
$(document).on("click", "#btnSalvar", () => {
    let user = $("#Usuario").serializeArray();
    let Usuario = {
        id: 0,
        Nome: user[0].value,
        Login: user[1].value,
        Senha: user[2].value,
        Email: user[3].value,
        Cpf: user[4].value
    }
    let teste = JSON.stringify(Usuario).toString();
    $.ajax({
        url: "/" + GetController() + "/SalvarUsuario",
        method: 'POST',
        contentType: "application/json",
        data: { 'nome': user[0].value }
        
    });

    Cancelar("#Adicionar", "#Listagem");
}); 