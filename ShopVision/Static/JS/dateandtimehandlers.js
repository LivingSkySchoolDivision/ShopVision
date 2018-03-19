var monthNames = [ "January", "February", "March", "April", "May", "June",
    "July", "August", "September", "October", "November", "December" ];
var dayNames= ["Sunday","Monday","Tuesday","Wednesday","Thursday","Friday","Saturday"];

function getDateString(date) {
    return dayNames[date.getDay()] + ",&nbsp;" + monthNames[date.getMonth()] + ' ' + date.getDate() + ',&nbsp;' + date.getFullYear()
}

function getTimeString(time) {
    ampm= 'AM';
    h= time.getHours();
    m= time.getMinutes();
    s= time.getSeconds();
    if(h>= 12){
        if(h>12)h-= 12;
        ampm= 'PM';
    }
    //if(h<10) h= '0'+h;
    if(m<10) m= '0'+m;
    if(s<10) s= '0'+s;
    return '<div id=\"time_main\">' + h + ':' + m + '</div><div id=\"time_second\">:' + s + '</div>';
}

var currentServerDateTime = null;
var offset_year = 0;
var offset_month = 0;
var offset_day = 0;
var offset_hour = 0;
var offset_minute = 0;
var offset_second = 0;
function updateDateAndTime(JSONPath) {
    var JSONPath = "/JSON/JSONTime.aspx";
    var currentSystemDateTime = new Date();

    // Get the current time from the server
    if (currentServerDateTime == null) {
        $.getJSON(JSONPath, function(data) {
            // Javascript uses zero based month numbers, and the server uses 1 based, so subtract 1 from the month
            currentServerDateTime = new Date(data.Year, (data.Month - 1), data.Day, data.Hour, data.Minute, data.Second, data.Millisecond);
            offset_year = currentSystemDateTime.getFullYear() - currentServerDateTime.getFullYear();
            offset_month = currentSystemDateTime.getMonth() - currentServerDateTime.getMonth();
            offset_day = currentSystemDateTime.getDate() - currentServerDateTime.getDate();
            offset_hour = currentSystemDateTime.getHours() - currentServerDateTime.getHours();
            offset_minute = currentSystemDateTime.getMinutes() - currentServerDateTime.getMinutes();
            offset_second = currentSystemDateTime.getSeconds() - currentServerDateTime.getSeconds();
        });
    }

    // Figure out the offset for each increment
    var displayDate = new Date(
        (currentSystemDateTime.getFullYear() - offset_year),
        (currentSystemDateTime.getMonth() - offset_month),
        (currentSystemDateTime.getDate() - offset_day),
        (currentSystemDateTime.getHours() - offset_hour),
        (currentSystemDateTime.getMinutes() - offset_minute),
        (currentSystemDateTime.getSeconds() - offset_second));

    $('#current_date').html(getDateString(displayDate));
    $('#current_time').html(getTimeString(displayDate));
}