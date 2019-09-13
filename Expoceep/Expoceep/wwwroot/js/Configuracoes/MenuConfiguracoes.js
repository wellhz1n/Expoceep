
$(document).ready(function () {
});
//********************************************************
//Eventos de Click
//=======================================================
$(document).on("click", "#menu-Backup", async () => {
    await BloquearTela();
    await $.post("/" + GetController() + "/GerarBackup", {}, async (e) => {
        if (e) {
            toastr.success("Backup Gerado", "Sucesso", { preventDuplicates: true, progressBar: true });
        }
        else
            toastr.error("Algo deu errado", "Opps!", { preventDuplicates: true, progressBar: true });

    });
    await DesbloquearTela();
});
$(document).on("click", "#menu-Backup-Carregar", async () => {
    await BloquearTela();
    await $.post("/" + GetController() + "/CarregarBackup", {}, async (e) => {
        if (e.resultado) 
            toastr.success("Backup Carregado", "Sucesso", { preventDuplicates: true, progressBar: true });
        else {
            toastr.error("Algo deu errado: " + e.erro, "Opps!", { preventDuplicates: true, progressBar: true, escapeHtml: true });
            ImprimirNoConsole(e.erro, "error");
        }

    });
    await DesbloquearTela();
});



