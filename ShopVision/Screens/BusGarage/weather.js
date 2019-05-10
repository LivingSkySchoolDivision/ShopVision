
function insertWeatherWidgetHTML() {
    var returnMe = "";
    returnMe += "<div id=\"weather\">";
    returnMe += "<div id=\"weather_row_1\">";
    returnMe += "<div id=\"weather_temperature_container\">";
    returnMe += "    <div id=\"weather_temp\"></div>";
    returnMe += "</div>";
    returnMe += "</div>";
    returnMe += "<div id=\"weather_row_2\">";
    returnMe += "  <div id=\"weather_detail_description\"></div>";
    returnMe += "</div>";
    returnMe += "</div>";
    return returnMe;
}

function updateWeather(JSONPath) {
    $.ajax({
        url: "https://lssdweatherapi.azurewebsites.net/api/GetWeather/sk-34",
        dataType: "json",
        success: function (parsed_json) {
            console.log(parsed_json);
            var location = parsed_json['locationName'];
            var temp_c = parsed_json['temperatureCelsiusWithWindChill'];
            var conditions = parsed_json['conditions'];
            var humidity = parsed_json['humidity'];
            var wind = parsed_json['wind'];
            var visibility = parsed_json['visibility'];

            //var icon_url = convertHTTPtoHTTPS(parsed_json['current_observation']['icon_url']);
            //var precip = parsed_json['current_observation']['precip_today_metric'];

            //var wind_direction = parsed_json['current_observation']['wind_dir'];
            //var wind_kph = parsed_json['current_observation']['wind_kph'];
            //var wind_string = wind_kph + ' KPH ' + wind_direction;

            $('#weather_temp').html(temp_c + "&deg;");
            //$('#weather_detail_icon').html("<img src=\"../images/WeatherIcons/" + getWeatherIcon(conditions) + "\">");
            $('#weather_detail_description').html(conditions);
            $('#weather_wind').html(wind);
            $('#weather_temp_humid').html(humidity);
            $('#weather_visibility').html(visibility);

            //$('#weather_temp_feelslike').html(temp_feels + "&deg;");

            //$('#weather_temp_precip').html(precip + " mm");

        }
    });
}

function getWeatherIcon(conditions) {
    return conditions.toLowerCase().replace(/\s/g, '') + ".png";
}