function GetController() {
    return window.location.href.split(window.location.host)[1].split("/")[1];
}
function MudaUrl(controller, action) {
    if (controller == null)
        controller = GetController();
    let href = "https://" + window.location.host + "/" + controller + "/" + action;
    window.location.href = href;
}

$(document).on("click", "#Logout", () => {

    $.post("/Login/Logout", (data) => {

        if (data)
            window.location.reload();
    });

});

//Funcoes de grid
function EscondeElemento(elemento) {
    $(elemento).fadeOut('slow');
    $(elemento).prop("hidden", true);
}
function AparecerElemento(elemento) {
    $(elemento).fadeIn('slow');
    $(elemento).prop("hidden", false);

}
function Adicionar(Teladeadicionar, Teladelistagem) {
    EscondeElemento(Teladelistagem);
    EscondeElemento("#btnNovo");
    EscondeElemento("#btnDeletar");
    EscondeElemento("#btnEditar");
    AparecerElemento("#btnCancelar");
    AparecerElemento("#btnSalvar");
    AparecerElemento(Teladeadicionar);

}
function Cancelar(Teladeadicionar, Teladelistagem) {
    AparecerElemento(Teladelistagem);
    AparecerElemento("#btnNovo");
    AparecerElemento("#btnDeletar");
    AparecerElemento("#btnEditar");
    EscondeElemento("#btnCancelar");
    EscondeElemento("#btnSalvar");
    EscondeElemento(Teladeadicionar);

}