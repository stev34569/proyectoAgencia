$(document).ready(function () {

    var eventsArray = [];

    $.ajax({
        type: "Get",
        url: "/Paquete/ConsultarPaquetesUsuarioAjax",
        dataType: "json",
        success: function (events) {

            events.forEach(function (element, index) {

                eventsArray.push({
                    title: element.nombre,
                    start: element.fechaInicio,
                    end: element.finalizacion,
                    allDay: false,
                    backgroundColor: 'gray',
                    borderColor: '#00c0ef'
                })



            })



            InicializarCalendario(eventsArray);
        }
    });



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