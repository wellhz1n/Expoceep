﻿//Template========================================================================================================================
let template = '<div class="col-12"id="bloco"><div><form method="post" class="row Produtopropriedade" id="Produtopropriedade">' +
    '<div class="col-2" >' +
    '<label for="Nome">Produto:</label>' +
    '<input type="hidden" name="ProdutoId" class="form-control  " disabled="disabled" />' +
    '<input name ="Produto" class="form-control  " disabled ="disabled" /> ' +
    '</div >' +
    '<div class="col-2" >' +
    '<label for="Tamanho">Tamanho</label>' +
    '<input name="Tamanho" class="form-control  " id="Tamanhoselect" disabled="disabled" />' +
    '</div >' +
    ' <div class="col-2 px-2">' +
    '<label for="Preco">Preço</label>' +
    '<input type="text" name="Preco" class="form-control" disabled="disabled" />' +
    '</div>' +
    '<div class="col-2">' +
    '<label for="Unidades">Unidades</label>' +
    ' <input type="number" name="Unidades" class="form-control" disabled="disabled" />' +
    '</div>'
    + ' <div class="col-3">' +
    '<label for="Total">Total:</label>' +
    '<input type="text" name="Total" class="form-control" style="color:red;" disabled="disabled" />' +
    '</div>' + '<a href = "#" class="delete btn btn-danger text-center " style="margin-top:4%;"><i class="fa fa-trash"></i></a > ' +
    '</form >' + '</div>';

//===================================================================================================================================
let totalfinal = $("#total");
let totalitens = $("#itens");
//Listas=============================================================================================================================
let listaProduto = []
let VProduto = Produto;
let VendasProdutos = [];
var prop;
//====================================================================================================================================
$(document).ready(async function () {
    await BloquearTela();
    $("#TrocoInput").maskMoney({
        prefix: "R$:",
        decimal: ",",
        thousands: "."
    });

    //SELETORES=======================================================================================================================
    $("#UnidadesId").on("change", (e) => {
        debugger
        ImprimirNoConsole("Unidades: " + e.target.value, "default");
        if (e.target.value != '') {
            $(e.target).removeClass("border-error");
        }
    });
    await $("#Produtoselect").select2({
        placeholder: "Selecione Um Produto",
        width: "30%",
        closeOnSelect: true,
        allowClear: true,
        ajax: {
            type: "POST",
            url: "/" + GetController() + "/GetProdutos",
            dataType: 'json',
            params: {
                contentType: 'application/json; charset=utf-8'
            },
            quietMillis: 100,
            data: function (q) {
                return {
                    q: q.term
                };
            },
            results: function (data, page) {
                return { results: data.d };
            }
        }

    }).on("change", async (e) => {
        debugger
        $("#Produtoselect").select2("close");
        if (e.target.selectedOptions[0] != undefined) {
            if (e.target.selectedOptions[0].value != null) {
                $("#Produtoselect").next().removeClass('border-error');
                VProduto = await ExecutaAjax("GetProdutoCompleto", { idproduto: e.target.selectedOptions[0].value });
                $("#Produtoselecttamanho").html('').select2('destroy');
                $("#Produtoselecttamanho").html('').select2({
                    placeholder: "Selecione Um Tamanho",
                    width: "30%",
                    allowClear: true,
                    data: ProdutoTamanhosSelectData(),
                });
                $("#UnidadesId").val(1);
                $("#UnidadesId").removeClass("border-error");
            }
        } else if (e.target.selectedOptions[0] == undefined) {
            debugger
            $("#UnidadesId").val('');
            VProduto = Produto;
            ResetaSelect();
            $("#Produtoselecttamanho").html('').select2(seletortamanho);
            $("#Produtoform")[0].reset();


        }

    });

    await $("#Produtoselecttamanho").select2({
        placeholder: "Selecione Um Tamanho",
        width: "30%",
        allowClear: true,
        data: ProdutoTamanhosSelectData(),
    }).on("change", (e) => {

        $("#Produtoselecttamanho").select2("close");
        if (e.target.selectedOptions[0] != undefined) {
            if (e.target.selectedOptions[0].value != null) {
                $("#Produtoselecttamanho").next().removeClass('border-error');
                $("#UnidadesId").val(1);
                $("#UnidadesId").removeClass("border-error");

            }
        }
        else if (e.target.selectedOptions[0] == undefined) {
            $("#UnidadesId").val('');
        }
    });
    //FIM========================================================================================================================
    //SISTEMa-DINAMICO_DE_PRODUTO================================================================================================
    var max_fields = 10;
    var wrapper = $("#conteudo");
    var add_button = $(".add_form_field");

    var x = 0;
    $(add_button).click(async function (e) {
        e.preventDefault();
        if (ValidaSeletores($("#Produtoform")[0]) && verificaEstoque() && VerificaSeElementoJaestaAdicionado()) {
            if (x < max_fields) {
                $(wrapper).append(template);
                x++;
                totalitens.html(x);
                prop = $(".Produtopropriedade")[x - 1];

                for (var i = 0; i < prop.length; i++) {
                    prop[0].value = VProduto.id;
                    prop[1].value = VProduto.nome;
                    prop[2].value = $("#Produtoselecttamanho").select2('data')[0].text;
                    prop[4].value = $("#Produtoform")[0][2].value;
                    if (VProduto.propriedades[i].id == $("#Produtoselecttamanho").select2('data')[0].id) {
                        debugger;

                        prop[3].value = "R$:" + VProduto.propriedades[i].preco;
                        let tot = (Number(VProduto.propriedades[i].preco.replace(',', '.').replace(/[^0-9\.-]+/g, "") * $("#Produtoform")[0][2].value)).toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,');
                        let subtot = tot.substr(tot.length - (tot.length + 3)).replace('.', ',');
                        let totalformated = tot.substr(0, tot.length - 3).replace(',', '.') + subtot;
                        prop[5].value = "R$:" + totalformated;
                        VendasProdutos.push({ id: prop[0].value, nome: prop[1].value, tamanho: prop[2].value, preco: prop[3].value, unidade: prop[4].value, precototal: prop[5].value });
                        totalfinal.html(PrecoTotal());
                    }
                    debugger;

                }
                debugger
                debugger

                //add input box
            } else {
                alert('You Reached the limits')
            }
        }
    });

    $(wrapper).on("click", ".delete", function (e) {
        e.preventDefault();
        prop = $(this).parent($('.Produtopropriedade'))[0];
        debugger;
        for (var i = 0; i < VendasProdutos.length; i++) {
            if (VendasProdutos[i].id == prop[0].value && VendasProdutos[i].tamanho == prop[2].value) {
                ImprimirNoConsole(VendasProdutos[i].id, "default");
                VendasProdutos.splice(i, 1);

            }
            ImprimirNoConsole(VendasProdutos, "default");
        }
        $(this).parent($('#bloco')).remove();
        x--;
        totalitens.html(x);

        totalfinal.html(PrecoTotal());
    });
    //FIM====================================================================================================================
    $("#title").text("Nova Venda");
    totalitens.html('0');
    totalfinal.html('0');
    await DesbloquearTela();
    //CamposMONETARIOS
    $("#TrocoInput").on('change', () => {
        let value = $("#TrocoInput").val().replace(/Rp\s|[.,]/g, '').substr(3);
        if (value >= totalfinal.html()) {
            $('#Troco').maskMoney('mask', parseInt(value) - parseInt(totalfinal));
        }
    });
});

function ProdutoTamanhosSelectData() {
    let pr = VProduto.propriedades;
    let data = {
        id: null,
        text: null
    }
    let arry = [];
    for (var i = 0; i < pr.length; i++) {
        if (pr[i].preco != null && pr[i].unidades > 0) {
            debugger
            if (pr[i].tamanho == 0)
                arry.push({ id: pr[i].id, text: "P" });
            if (pr[i].tamanho == 1)
                arry.push({ id: pr[i].id, text: "M" });
            if (pr[i].tamanho == 2)
                arry.push({ id: pr[i].id, text: "G" });
        }
    }
    return arry;
}
//function ResetaSelect(execao = [],model = []) {
//    debugger
//    let s = $("select");
//    for (var i = 0; i < s.length; i++) {
//        if (execao[i] != i) {

//            var self = $(s[i]);
//            var select2Instance = self.data("select2");
//            var resetOptions = select2Instance.options.options;
//            self.select2("destroy")
//            self.select2(resetOptions);
//            self.data("select2").options.options.data = ProdutoTamanhosSelectData();
//            debugger;
//        }
//    }
//    debugger;


//    debugger
//}
function ResetaSelect(execao = []) {
    debugger
    let s = $("select");
    for (var i = 0; i < s.length; i++) {
        if (execao[i] != i) {

            var self = $(s[i]);
            self.select2('val', '');
            debugger;
        }
    }
    debugger;


    debugger
}
function verificaEstoque() {
    let prod = VProduto.propriedades;
    for (var i = 0; i < prod.length; i++) {
        if (prod[i].id == $("#Produtoform")[0][1].value) {
            if (prod[i].unidades < $("#Produtoform")[0][2].value) {
                toastr.warning("Insira um numero Inferior ou Igual a " + prod[i].unidades, titulo.text() + ", Estoque Insuficiente", { preventDuplicates: true, progressBar: true, timeOut: 2500 });
                return false;
            }
            else
                return true;

        }
    }


}
function PrecoTotal() {
    let total = 0;
    for (var i = 0; i < VendasProdutos.length; i++) {
        let tot = Number((VendasProdutos[i].precototal.substr(3).replace(',', '.').replace('.','')).replace(/[^0-9\.-]+/g, ""));
        total += tot;
        ImprimirNoConsole(total, 'default');
        debugger;
    }
    let tota = (Number(total.toString().replace(/[^0-9\.-]+/g, ""))).toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,');
    let subtot = tota.substr(tota.length - (tota.length + 3)).replace('.', ',');
    let totalformated = tota.substr(0, tota.length - 3).replace(',', '.') + subtot;
    return totalformated;

}
function VerificaSeElementoJaestaAdicionado() {
    let prod = VProduto.propriedades;
    for (var i = 0; i < VendasProdutos.length; i++) {
        debugger
        if (VendasProdutos[i].id == VProduto.id && VendasProdutos[i].tamanho == $("#Produtoselecttamanho")[0].selectedOptions[0].text ) {
            toastr.warning("Produto ja Inserido", titulo.text() + ", Produto ja esta na lista", { preventDuplicates: true, progressBar: true, timeOut: 2500 });

            return false;
        }
    }
    return true;
}