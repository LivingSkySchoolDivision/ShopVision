var inspectionsThisMonthcount = 0;
var inspectionsNextMonthCount = 0;


var workOrderTables = [];
workOrderTables[0] = "workOrders_1";
workOrderTables[1] = "workOrders_2";

workOrderTables[2] = "workOrders_3";
workOrderTables[3] = "workOrders_4";

workOrderTables[4] = "workOrders_5";
workOrderTables[5] = "workOrders_6";

var pagesUsed = 1;
var maxWorkOrdersPerColumn = 6;

function updateWorkOrderLists() {
    var JSONPath = "/JSON/FleetVision/NewestWorkOrders.aspx";

    var displayedWorkOrders = 0;
    var currentWorkOrderColumn = 0;

    $.getJSON(JSONPath, function (data) {

        for (x = 0; x <= workOrderTables.length; x++) {
            $("#" + workOrderTables[x]).empty();
            $("#" + workOrderTables[x]).append("<tbody></tbody>");
        }

        $.each(data.WorkOrders, function (categoryIndex, workorder) {
            if (currentWorkOrderColumn < workOrderTables.length) {

                var wo_content_font_size_style = "wo_content_font_normal";

                if (workorder.workrequested.length > 64) {
                    wo_content_font_size_style = "wo_content_font_small";
                }

                $("#" + workOrderTables[currentWorkOrderColumn] + " > tbody:last").append("<tr><td align=\"left\" style='vertical-align: top;'><div class='wo_ID'>" + workorder.vehicle + "</div><div class='wo_Priority'>" + workorder.priority + "</div></td><td align=\"left\" style='vertical-align: top;'><div class='wo_Content " + wo_content_font_size_style + "'>" + workorder.workrequested + "</div></td></tr>");

            }

            displayedWorkOrders++;
            if (displayedWorkOrders >= maxWorkOrdersPerColumn) {
                displayedWorkOrders = 0;
                currentWorkOrderColumn++;
            }
        });

        // How many pages did we use up
        pagesUsed = Math.ceil((currentWorkOrderColumn + 1) / 2);
    });
}

var pages = [];
var currentPage = 0;
pages[0] = "workorders_page_1";
pages[1] = "workorders_page_2";
pages[2] = "workorders_page_3";
pages[3] = "inspections_page";
pages[4] = "inspections_page_2";
//pages[4] = "text_page";

function cyclePages() {
    var skipEmptyPages = true;
    console.log("Pages used: " + pagesUsed);
    console.log("Fading out page: " + pages[currentPage]);
    $("#" + pages[currentPage]).fadeOut('500', function () {

        currentPage++;

        if (skipEmptyPages) {
            // Skip blank work order pages
            if (currentPage == 1) {
                if (pagesUsed < 2) {
                    console.log("Skipping empty page " + currentPage);
                    currentPage++;
                }
            }

            // Skip workorders page 3 if there are no workorders on it
            if (currentPage == 2) {
                if (pagesUsed < 3) {
                    console.log("Skipping empty page " + currentPage);
                    currentPage++;
                }
            }

            // Skip SGI page if there are no inspections left this month

            if (currentPage == 3) {
                if (inspectionsThisMonthcount <= 0) {
                    console.log("No inspections this month - skipping page");
                    currentPage++;
                }
            }

            if (currentPage == 4) {
                if (inspectionsNextMonthCount <= 0) {
                    console.log("No inspections next month - skipping page")
                    currentPage++;
                }
            }
        }
        // Wrap around
        if (currentPage >= pages.length) {
            currentPage = 0;
        }

        $("#" + pages[currentPage]).fadeIn();
        console.log("Now showing page: " + pages[currentPage]);
        $("#page_name").html("[" + pages[currentPage] + "]");
    });
}

// Only show page 1
function initPages() {

    for (x = 1; x <= pages.length; x++) {
        console.log("Hiding " + pages[x])
        $("#" + pages[x]).fadeOut(1);
    }

    $("#curtain_black").delay(1000).fadeOut(500);
}

function dim() {
    $("#curtain_dim").fadeIn(5000);
}

function undim() {
    $("#curtain_dim").fadeOut(5000);
}


function updateSGIInspections() {
    var JSONPath = "/JSON/Versatrans/UpcomingBusInspections.aspx";

    console.log("Loading SGI inspections");

    $.getJSON(JSONPath, function (data) {
        inspectionsThisMonthcount = 0;
        inspectionsNextMonthCount = 0;

        console.log("Loaded SGI inspections");

        var font_style = "sgi_medium_text";
        if ((data.TotalThisMonth + data.TotalOverdue) < 15) {
            font_style = "sgi_big_text";
        }

        $("#month_name").html(" in " + data.ThisMonthName)
        $("#next_month_name").html(" in " + data.NextMonthName)

        $("#sgi_insepections_list").html("")

        $.each(data.Overdue, function (index, cert) {
            console.log("Adding overdue inspection for vehicle " + cert.Vehicle);
            $("#sgi_insepections_list").append("<div class='sgi_inspection_overdue " + font_style + "'>" + cert.Vehicle + "</div>");
            inspectionsThisMonthcount++;
        });

        $.each(data.ThisMonth, function (index, cert) {
            console.log("Adding inspection for this month for vehicle " + cert.Vehicle);
            $("#sgi_insepections_list").append("<div class='sgi_inspection_normal " + font_style + "'>" + cert.Vehicle + "</div>");
            inspectionsThisMonthcount++;
        });


        $("#sgi_insepections_list_nextmonth").html("")

        $.each(data.NextMonth, function (index, cert) {
            console.log("Adding inspection for next month for vehicle " + cert.Vehicle);
            $("#sgi_insepections_list_nextmonth").append("<div class='sgi_inspection_normal " + font_style + "'>" + cert.Vehicle + "</div>");
            inspectionsNextMonthCount++;
        });


    });

}