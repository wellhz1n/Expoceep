
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
 
   
    LimpaFormulario(Teladeadicionar);
  
    AparecerElemento(Teladelistagem);
    AparecerElemento("#btnNovo");
    AparecerElemento("#btnDeletar");
    AparecerElemento("#btnEditar");
    EscondeElemento("#btnCancelar");
    EscondeElemento("#btnSalvar");
    EscondeElemento(Teladeadicionar);


}


function LimpaFormulario(TeladoForm) {
    let c = TeladoForm + " form " + " :input";
    $(c).removeClass("border-danger");
    $(c).toArray().forEach(o => o.value = null);
}

// Checa se um array tem nulos


function checarNulos(array, arrayOpcionalDeExcessoes) {
    let naoTemNulo = true;
    //Caso tiver um array de excessões:
    if (arrayOpcionalDeExcessoes !== undefined) {
        if (typeof arrayOpcionalDeExcessoes[0] == "number") {
            for (let c in array) {
                for (let itemInterno in arrayOpcionalDeExcessoes) {
                    if (c == arrayOpcionalDeExcessoes[itemInterno].toString()) {
                        $("input[name =\'" + array[c].name + "\' ]").removeClass("border-danger");

                        naoTemNulo = true;
                        break;
                    } else if (array[c].value == null || array[c].value == "") {
                        $("input[name =\'" + array[c].name + "\' ]").addClass("border-danger");
                        debugger
                        naoTemNulo = false;
                    }
                }

            }
        }

    } else { //Caso não tiver
        for (let c in array) {
            if (array[c].value == null || array[c].value == "") {
                $("input[name =\'" + array[c].name + "\' ]").addClass("border-danger");
                naoTemNulo = false;

            }
            else {
                $("input[name =\'" + array[c].name + "\' ]").removeClass("border-danger");
            }
            if (!naoTemNulo) {
                toastr.options.preventDuplicates = true;
                toastr.error("Preencha os Campos", "Ops!", { timeOut: 2000 });
            }
        }
    }
    return naoTemNulo;
}