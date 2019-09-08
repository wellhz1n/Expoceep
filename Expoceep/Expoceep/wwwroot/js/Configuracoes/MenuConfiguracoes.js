
$(document).ready(function () {
});
//********************************************************
//Eventos de Click
//=======================================================
$(document).on("click", "#menu-Backup", async () => {
    await BloquearTela();
    await $.post("/" + GetController() + "/GerarBackup", {}, async (e) => {
        if (e) {
            toastr.success("Backup Gerado", "Sucesso");
        }
        else
            toastr.error("Algo deu errado", "Opps!");

    });
    await DesbloquearTela();
});
$(document).on("click", "#menu-Backup-Carregar", async () => {
    await BloquearTela();
    await $.post("/" + GetController() + "/CarregarBackup", {}, async (e) => {
        if (e) {
            toastr.success("Backup Carregado", "Sucesso");
        }
        else
            toastr.error("Algo deu errado", "Opps!");

    });
    await DesbloquearTela();
});



