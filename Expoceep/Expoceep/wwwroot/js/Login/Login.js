$(document).on("click", "#login", () => {

    let login = $("#form").serializeArray();
    if (login[0].value !== "" && login[1].value !== "") {
        $(".caixa-login").addClass("anim");
        $("#form").submit();
    }
    else
        alert("Preencha os campos");

    //$.post("Login/Login", { login: login[0].value, senha: login[1].value }, function (data) {
    //    if (!data) {
    //        alert("Login invalido");
    //    }
    //});

});