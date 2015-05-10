$(function () {
    var calendar = $('#calendar');
    var saveModal = $('#saveModal');
    var confirmModal = $('#confirmModal');

    calendar.fullCalendar({
        header: {
            left: 'title',
            right: 'prev,next today'
        },
        firstDay: 1, // Monday
        height: 600,
        dayClick: dayClick,
        viewRender: viewRender,
        eventRender: eventRender,
        eventClick: eventClick,
        defaultDate: new Date()
    });

    function viewRender(view, element) {
        var start = view.intervalStart;
        var startDate = start.year() + '-' + (start.month() + 1) + '-' + start.date();
        var end = view.intervalEnd;
        var endDate = end.year() + '-' + (end.month() + 1) + '-' + end.date();

        $.post('/Calendar/Index', {
            startDate: startDate,
            endDate: endDate
        })
        .done(initEvents);
    }

    function eventRender(event, element, jsEvent) {
        var html = '<img src="/Content/Images/training.png" width="30" />';

        if (event.data.Supplements) {
            html += '<img src="/Content/Images/suplements.png" width="30" />';
        }

        if (event.data.Food) {
            html += '<img src="/Content/Images/food.png" width="30" />';
        }

        element.find(".fc-title").after('<span class="fc-event-remove" style="float:right;"><a href="javascript:void(0)"><i class="glyphicon glyphicon-remove-sign"></i></a></span>');
        element.find(".fc-event-remove").after($("<div class=\"fc-event-icons text-center\"></div>").html(html));
    }

    function initEvents(data) {
        if (!data.success) {
            alert('An error has occured.');
            return;
        }
        calendar.fullCalendar('removeEvents');
        var events = [];
        for (var i in data.data) {
            var evt = data.data[i];
            events.push(mapEvent(evt));
        }

        calendar.fullCalendar('addEventSource', events);
    }

    function mapEvent(evt) {
        var milliseconds = evt.Date.replace(/\/Date\((-?\d+)\)\//, '$1');
        var date = new Date(parseInt(milliseconds));
        return {
            id: evt.Id,
            allDay: true,
            start: date.getFullYear() + '-' + (date.getMonth() + 1) + '-' + date.getDate(),
            backgroundColor: '#fff',
            textColor: '#3a87ad',
            title: evt.WorkoutName,
            data: evt
        }
    }

    function dayClick(date, jsEvent, view) {
        var dateText = date.year() + '-' + (date.month() + 1) + '-' + date.date();
        var eventsForTheDay = calendar.fullCalendar('clientEvents', function (event) {
            var startDate = event.start.year() + '-' + (event.start.month() + 1) + '-' + event.start.date();
            return startDate == dateText;
        });

        if (eventsForTheDay.length > 0) {
            return;
        }

        $.get('/Calendar/Create')
        .done(function (view) {
            saveModal.find('.modal-body').html(view);
            saveModal.find('#Date').val(dateText);
            saveModal.modal('show');
        });
    }

    function eventClick(event, jsEvent, view) {
        if ($(jsEvent.target).hasClass('glyphicon-remove-sign')) {
            confirmModal.data('id', event.id);
            confirmModal.modal('show');

            return;
        }

        var date = event.start;
        var dateText = date.year() + '-' + (date.month() + 1) + '-' + date.date();

        $.get('/Calendar/Edit/' + event.id)
        .done(function (view) {
            saveModal.find('.modal-body').html(view);
            saveModal.find('#Date').val(dateText);
            saveModal.modal('show');
        });
    }

    saveModal.find('.btn-primary').on('click', function () {
        var formData = $("form").serializeObject();
        $.post('/Calendar/Save', formData).done(function (data) {
            if (!data.success) {
                alert(data.message);
                return;
            }

            var evt = mapEvent(data.data);
            calendar.fullCalendar('removeEvents', evt.id);
            calendar.fullCalendar('addEventSource', [evt]);
            saveModal.modal('hide');
        });
    });

    confirmModal.find('.btn-primary').on('click', function () {
        var id = confirmModal.data('id');

        $.post('/Calendar/Delete', { id: id }).done(function (data) {
            if (!data.success) {
                alert(data.message);
                return;
            }

            calendar.fullCalendar('removeEvents', id);
            confirmModal.modal('hide');
        });
    });
});