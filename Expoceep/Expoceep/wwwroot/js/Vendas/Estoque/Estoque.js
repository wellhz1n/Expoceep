let prodIdArr = [];
let chartData = Chartclass();
let tamanho;
let tamanhostr;
$(document).ready(() => {

    $("#Produtoselect").select2({
        placeholder: "Selecione Um Produto",
        width: "100%",
        closeOnSelect: true,
        multiple: true,
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
    });
    $("#Tamanhoselect").select2({
        placeholder: "Selecione Um Tamanho",
        width: "100%",
        closeOnSelect: true,
        allowClear: true,
    });
    $("#TipoGraficoselect").select2({
        placeholder: "Selecione Um Grafico",
        width: "100%",
        closeOnSelect: true,

    });

    $("#btnGerar").on('click', () => {
        debugger;
         tamanho = $("#Tamanhoselect").val();

        switch (tamanho) {
            case '0':
                tamanhostr = 'P'
                break;
            case '1':
                tamanhostr = 'M'
                break;
            case '2':
                tamanhostr = 'G'
                break;
            case null:
                tamanhostr = 'Todos'
                break;
            default:
                break;

        }
        debugger;
        prodIdArr = $("#Produtoselect").select2('val').length < 1 ? null : $("#Produtoselect").select2('val');
        ExecutaAjax('GetProdutosParaGrafico', { produtoId: prodIdArr, t: $("#Tamanhoselect").val() }).then((data) => {
            switch ($('#TipoGraficoselect').select2('val')) {
                case '0':
                    chartData.Tipo = TipoGrafico.BAR;
                    break;
                case '1':
                    chartData.Tipo = TipoGrafico.PIE;
                    break;
                    case'2':
                    chartData.Tipo = TipoGrafico.LINE;
                    break;

                default:
                    break;
            }
            chartData.Labels = data.labels;
            chartData.Values = data.values;
            $('#chartCont').css('display', 'block');
            GerarGraficoAnual('myChart', chartData.Tipo, chartData.Labels, 'Estoque de Produtos', chartData.Values, "Quantidade de Produtos");
        });



    });
    $('#btnSalvar').on('click', async () => {
        BloquearTela();
        debugger;
        let doc = new jsPDF();
        let img = new Image();
        let pageHeight = doc.internal.pageSize.height || doc.internal.pageSize.getHeight();
        let pageWidth = doc.internal.pageSize.width || doc.internal.pageSize.getWidth();


        debugger;
        img.src = '../../img/lOGO/Logo_reto.png';
        await setTimeout(async () => {

            doc.addImage(img, "PNG", 2, 0, 25, 15);
            doc.setFontSize(11);
            doc.text(`Grafico de  ${titulo.text()}-${new Date().toLocaleDateString()}`, pageWidth / 2, 10, 'center');
            doc.text(`Tamanho:  ${tamanhostr}`, 2, 30);
            doc.addImage(document.getElementById('myChart').toDataURL(), 'JPEG', 5, 40, 190, 80);
            doc.text(`Produtos`, 10, 130);
            doc.text(`Unidades`, pageWidth / 2 - 15, 130);
            for (var i = 0; i < chartData.Labels.length; i++) {
                doc.text(`${chartData.Labels[i]}:`, 10, 140 + i * 10);
                doc.text(` ${chartData.Values[i]} `, pageWidth / 2 - 10, 140 + i * 10);
            }

            await doc.save(`${titulo.text()}_${new Date().toLocaleTimeString()}.pdf`);



         DesbloquearTela();
        }, 500);
    });
});