$(document).ready(function () {

    var eventsArray = [];
    InicializarCalendario(eventsArray);
});

function InicializarCalendario(eventos) {
    $('#calendar').fullCalendar({
        header: {
            left: 'prev,next today',
            center: 'title',
            right: 'month,agendaWeek,agendaDay'
        },
        buttonText: {
            today: 'Hoy',
            month: 'Mes',
            week: 'Semana',
            day: 'Día'
        },
        editable: false,
        droppable: false,
        events: eventos,
        timeFormat: 'hh:mm A'
    });
}