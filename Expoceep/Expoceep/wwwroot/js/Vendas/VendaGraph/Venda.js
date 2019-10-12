let meseschart = [];
let datachart = [];

$(document).ready(() => {

    CarregarSeletoresData();
    $("#btnGerar").on('click', () => {
        let mesinicio = new Date(Date.parse($("#ov_datainicio").val() + "T16:00")).getMonth();
        let mesfim = new Date(Date.parse($("#ov_datafim").val() + "T16:00")).getMonth();
        ExecutaAjax('GetVendasParaGrafico', {
            datainicio: new Date(Date.parse($("#ov_datainicio").val() + "T16:00")).toISOString(),
            datafim: new Date(Date.parse($("#ov_datafim").val() + "T16:00")).toISOString()
        }).then((data) => {
            $('#chartCont').css('display', 'block');
            GerarGraficoAnual('myChart', TipoGrafico.BAR, GetMesesEntre([mesinicio, mesfim]), 'Vendas', data, "Quantidade de Vendas");
        });
       
       


    });

});

function CarregarSeletoresData() {
    $("#ov_datainicio").prop('min', new Date().getFullYear() + '-01-01');
    $("#ov_datainicio").prop('max', new Date().getFullYear() + '-' + (new Date().getMonth() + 1) + '-' + new Date().getDate());
    $("#ov_datafim").prop('min', new Date().getFullYear() + '-' + (new Date().getMonth() + 1) + '-' + new Date().getDate());
    $("#ov_datafim").prop('max', new Date().getFullYear() + '-12-31');
    $("#ov_datainicio").val(new Date().toISOString().split('T')[0]);
    $("#ov_datafim").val(new Date().toISOString().split('T')[0]);
}
