

    function onSelect(calendar, date) {
        // Beware that this function is called even if the end-user only
        // changed the month/year. In order to determine if a date was
        // clicked you can use the dateClicked property of the calendar:
        if (calendar.dateClicked) {
            var msg =
                "<br/>Persian: Year: " + calendar.date.getJalaliFullYear() +
                    ", Month: " + (calendar.date.getJalaliMonth() + 1) +
                    ", Day: " + calendar.date.getJalaliDate() +
                    "<br/>Gregorian: Year: " + calendar.date.getFullYear() +
                    ", Month: " + calendar.date.getMonth() +
                    ", Day: " + calendar.date.getDate();

            $("#<%= DatePicker1.ClientID %>").val(date);
            logEvent("onSelect Event: <br> Selected Date: " + date + msg);
            calendar.hide();
            //calendar.callCloseHandler(); // this calls "onClose"
        }
    };

function onUpdate(calendar) {
    var msg =
        "<br/>Persian: Year: " + calendar.date.getJalaliFullYear() +
            ", Month: " + (calendar.date.getJalaliMonth() + 1) +
            ", Day: " + calendar.date.getJalaliDate() +
            "<br/>Gregorian: Year: " + calendar.date.getFullYear() +
            ", Month: " + calendar.date.getMonth() +
            ", Day: " + calendar.date.getDate();

    logEvent("onUpdate Event: <br> Selected Date: " + calendar.date.print('%Y/%m/%d', 'jalali') + msg);
};

function onClose(calendar) {
    logEvent("onClose Event");
    calendar.hide();
};

function logEvent(str) {
    $("#log").append("<li>" + str + "</li>");
}
