let template = '<div class="col-12"id="bloco"><div><form method="post" class="row Produtopropriedade" id="Produtopropriedade">' +
    '<div class="col-3" >' +
    '<label for="Tamanho">Tamanho</label>' +
    '<select name="Tamanho" class="form-control  " id="Tamanhoselect" >' +
    '<option value="0">P</option>' +
    '<option value="1" >M</option>' +
    '<option value="2">G</option>' +
    '</select>' +
    '</div >' +
    ' <div class="col-3">' +
    '<label for="Preco">Preço</label>' +
    '<input type="text" name="Preco" class="form-control" />' +
    '</div>' +
    '<div class="col-3">' +
    '<label for="Unidades">Unidades</label>' +
    ' <input type="number" name="Unidades" class="form-control" />' +
    '</div><a href="#" class="delete p-4 my-3"><i class="fa fa-trash"></i></a>' +
    '</form >' + '</div>';
let listaProduto = []
let VProduto = Produto;
$(document).ready(function () {

    $("#Produtoselect").select2({
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
        if (e.target.selectedOptions[0].value != null) {
            VProduto = await ExecutaAjax("GetProdutoCompleto", { idproduto: e.target.selectedOptions[0].value });
        }
    });

    $("#Produtoselecttamanho").select2({
        placeholder: "Selecione Um Tamanho",
        width: "30%",
        data: () => {
            return { results: " { results: " +  + "}" }
        },
    });
    var max_fields = 10;
    var wrapper = $(".container1");
    var add_button = $(".add_form_field");

    var x = 0;
    $(add_button).click(function (e) {
        e.preventDefault();
        if (x < max_fields) {
            x++;
            $(wrapper).append(template); //add input box
        } else {
            alert('You Reached the limits')
        }
    });

    $(wrapper).on("click", ".delete", function (e) {
        e.preventDefault();
        $(this).parent($('#bloco')).remove();
        x--;
    })
});
function ProdutoTamanhosSelectData() {
    let pr = VProduto.propriedades;
    let data = {
        id: null,
        text: null
    }
    let arry = [];
    for (var i = 0; i < pr.length; i++) {
        if (pr[i].tamanho == 0)
            arry.push(data.id = pr[i].id, data.text = "P");
        if (pr[i].tamanho == 1)
            arry.push(data.id = pr[i].id, data.text = "M");
        if (pr[i].tamanho == 2)
            arry.push(data.id = pr[i].id, data.text = "G");
    }
    return JSON.stringify(arry);
}