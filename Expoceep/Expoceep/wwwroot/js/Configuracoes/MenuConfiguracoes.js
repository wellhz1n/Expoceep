$(document).on("click", "#menu-Backup", async () => {
    await BloquearTela();
    $.post("/" + GetController() + "/GerarBackup", {}, async (e) => {
        if (e) {
            await DesbloquearTela();
            toastr.success("Backup Gerado", "Sucesso");
        }
    });
    await DesbloquearTela();
});

//USUARIOTABLE
$(document).ready(function () {
});



