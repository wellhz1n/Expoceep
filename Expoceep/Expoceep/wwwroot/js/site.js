//OBJECTS
var Usuario = {
    Novo: null,
    Editando: null,
    id: null,
    Nome: null,
    Login: null,
    Senha: null,
    Email: null,
    Cpf: null
}
var Cliente = {
    Novo: null,
    Editando: null,
    id: null,
    Nome: null,
    Sobrenome: null,
    Email: null,
    Cpf: null
}
var Produto = {
    Novo: null,
    Editando: null,
    Id: null,
    Codigo: null,
    Nome: null,
    propriedades: []

}
var ProdutoPropriedades = {
    Tamanho: null,
    Preco: null,
    Unidades: null

}
var Resultado = {
    resultado: null,
    erro: null
}
//FIM OBJECTS
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
                $("input[name =\'" + array[c].name + "\' ]").removeClass("erroNoInput");
                for (let itemInterno in arrayOpcionalDeExcessoes) {
                    if (c == arrayOpcionalDeExcessoes[itemInterno].toString()) {
                        $("input[name =\'" + array[c].name + "\' ]").removeClass("erroNoInput");

                        break;
                    } else if ((array[c].value == null || array[c].value == "") && $("input[name =\'" + array[c].name + "\' ]").name != "__RequestVerificationToken") {
                        $("input[name =\'" + array[c].name + "\' ]").addClass("erroNoInput");
                        naoTemNulo = false;
                    }
                }

            }

        }

    } else { //Caso não tiver
        for (let c in array) {
            $("input[name =\'" + array[c].name + "\' ]").removeClass("erroNoInput");
            if ((array[c].value == null || array[c].value == "") && $("input[name =\'" + array[c].name + "\' ]").name != "__RequestVerificationToken") {
                $("input[name =\'" + array[c].name + "\' ]").addClass("erroNoInput");
                naoTemNulo = false;
            }


            if (!naoTemNulo) {
                toastr.options.preventDuplicates = true;
                toastr.error("Preencha os Campos", "Ops!", { timeOut: 2000 });
            }
        }
    }

    return naoTemNulo;

}
////
////

async function Tabela(idtabela, action, controller) {
    //Exemplo de coluna
    //{ "data": "Nome", "title": "Nome", "autowidth": true }
    if (controller == null)
        controller = GetController();
    let colunas = TableGetColuns(idtabela);
    let table = await $('#' + idtabela).DataTable({
        language: {
            "lengthMenu": "Exibe _MENU_ Registros por página",
            "search": "Procurar",
            "paginate": { "previous": "Retorna", "next": "Avança" },
            "zeroRecords": "Nada foi encontrado",
            "info": "Exibindo página _PAGE_ de _PAGES_",
            "infoEmpty": "Sem registros",
            "infoFiltered": "(filtrado de _MAX_ regitros totais)"
        },
        //"processing": true,
        //"serverSide": true,
        //"filter": true, // habilita o filtro(search box)
        //"lengthMenu": [[3, 5, 10, 25, 50, -1], [3, 5, 10, 25, 50, "Todos"]],
        pageLength: 10,
        destroy: true,
        responsive: true,
        ajax: {
            url: "/" + controller + "/" + action,
            type: "POST",
            datatype: "json",
            cache: true,
            complete: function (e) {
                ImprimirNoConsole(e.responseText, "default");
            }
        },
        columns: colunas
    });
    //$('.dataTables_length').addClass('bs-select');

    return table;
}

//function RowValueGet(objeto, table,linha) {
//        if ($(row).hasClass('selected')) {
//            $(row).removeClass('selected');
//            objeto = objeto;
//        }
//        else {
//            table.$('tr.selected').removeClass('selected');
//            $(row).addClass('selected');
//            let t = tabela.row(row).data();
//            objeto = t;
//        }
//    console.log(objeto);
//    return objeto;
//    };


function TableGetColuns(idtabela) {
    let tab = $("#" + idtabela + "> thead > tr > th");
    let colunas = [];
    for (var i = 0; i < tab.length; i++) {
        colunas.push({ "data": tab[i].attributes.name.value, "title": tab[i].innerText != "" ? tab[i].innerText : tab[i].attributes.name.value, "autowidth": true });
    }
    return colunas;
}
function ValorInput(obj, form) {
    form = $("#" + form).serializeArray();
    let objarray = []
    objarray = Object.values(obj);

    for (var i = 0; i < form.length; i++) {


        $("input[name =\'" + form[i].name + "\' ]").val(objarray[i]);

    }


}
function BloquearTela() {
    $("#loaderpage").css("display", "block");
}
function DesbloquearTela() {
    $("#loaderpage").css("display", "none");

}

function SerialiazaGrupoForm(grupoform) {
    let formserialized = [];
    for (var i = 0; i <= grupoform.length; i++) {
        let grupoatual = grupoform[i];
        formserialized.push($(grupoatual).serializeArray());
    }
    return formserialized;
}
function ResetaGrupoFormulario(grupoform) {
    for (var i = 0; i < grupoform.length; i++) {
        $(grupoform)[i].reset();

    }
}
function ImprimirNoConsole(msg, tipo = "error") {
    switch (tipo) {
        case "error":
            console.error("❌ ERRO NO C#: " + msg);
            break;
        case "warn":
            console.warn("⚠️ Ops: " + msg);
            break;
        case "default":
            console.log("✌️ Console: " + msg);
            break;

        default:
    }
}

function validar(seletor, arrayMensagemErro) {

    var seletor = $('#' + seletor);
    if (!seletor.parsley().isValid()) {
        seletor.addClass("erroNoInput2");
        toastr.error(arrayMensagemErro[0], arrayMensagemErro[1]);
        return true;
    } else {
        seletor.removeClass("erroNoInput2");
    }
}
function ResetarObjeto(obj) {
    let objarray = Object.entries(obj);
    let newobj = {};
    for (var i = 0; i < objarray.length; i++) {
        objarray[i][1] = null;
        newobj[objarray[i][0]] = objarray[i][1];
    }
    //= JSON.stringify(Object.assign({}, objarray));
    return newobj;


}
function ObjetoENulo(obj) {
    var state = true;
    for (var key in obj) {
        if (!(obj[key] === null || obj[key] === "")) {
            state = false;
            break;
        }
    }
    return state;
}
function CopiaEntidade(obj) {
    let o = $.extend(true, {}, obj);
    return o;
}
 async function ExecutaAjax(metodo, dados) {
    let resultado;
    await $.post('/' + GetController() + "/" + metodo, dados, (result) => { resultado =  result; });
    return resultado;
}