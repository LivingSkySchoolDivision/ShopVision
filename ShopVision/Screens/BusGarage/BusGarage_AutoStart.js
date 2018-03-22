/* ******************************************** */
/* * Document onLoad stuff                    * */
/* ******************************************** */
$(document).ready(function () {
    updateWeather();
    updateDateAndTime()
    updateWorkOrderLists();
    initPages();
    updateSGIInspections();
    updateShopMessages();
    // Hide the mouse cursor
    $('body').css('cursor', 'none');

});


/* ******************************************** */
/* * Interval stuff                           * */
/* ******************************************** */

/*
 1000     1 second
 10000     10 seconds
 60000     1 minute
 300000     5 minutes
 600000     10 minutes
 1800000     30 mins
 3600000     1 hour
 */


/* Refresh the page every hour ish */
setInterval(function () {
    location.reload();
}, 4500000);

// Flip the pages
setInterval(function () {
    cyclePages();
    updateShopMessages();
}, 10000);

/* Update weather */
setInterval(function () {
    updateWeather();
}, 60000);

/* Update the date and time displays */
setInterval(function () {
    updateDateAndTime();
}, 500);

setInterval(function () {
    updateWorkOrderLists();
}, 10000);

setInterval(function () {
    updateSGIInspections();
}, 60000);
