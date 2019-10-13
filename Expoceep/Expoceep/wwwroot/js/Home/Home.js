$(document).ready(() => {
    Clock();

});
function Clock() {
    let date = new Date();
    let relogio = {
        horas: date.getHours(),
        minutos: date.getMinutes(),
        segundos: date.getSeconds()
    };
    relogio.minutos = Check(relogio.minutos);
    relogio.segundos = Check(relogio.segundos);

    document.getElementById('clockletter').innerHTML = `${relogio.horas}:${relogio.minutos}:${relogio.segundos}`;
    function Check(i) {
        if (i < 10) {
            i = "0" + i;
        }
        return i;
    }
    setTimeout(Clock, 500);
}
