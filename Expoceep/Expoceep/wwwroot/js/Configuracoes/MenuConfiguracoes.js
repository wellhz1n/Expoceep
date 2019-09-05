$(document).on("click", "#menu-Backup", async () => {
    await BloquearTela();
    await $.post("/" + GetController() + "/GerarBackup", {}, async (e) => {
        if (e) {
            toastr.success("Backup Gerado", "Sucesso");
        }
    });
    await DesbloquearTela();
});

//USUARIOTABLE
$(document).ready(function () {
});



