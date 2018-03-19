// openweathermap gives temperature in K
// we don't need this at the moment, because wunderground uses C
function convertKToCelcius(K) {
    return Math.round(K - 273.15);
}

function convertHTTPtoHTTPS(thisHTTPURL) {
    return thisHTTPURL.replace("http","https")
}

function updateWeather(JSONPath) {
    /* Weather underground API key: bb5a09f62da58b68 */
    /* Use this section for wunderground */
    $.ajax({
        url : "/Proxy/JSON.aspx?url=https://api.wunderground.com/api/bb5a09f62da58b68/geolookup/conditions/q/North_Battleford.json",
        dataType : "json",
        success : function(parsed_json)
            {
                var location = parsed_json['location']['city'];
                var temp_c = parsed_json['current_observation']['temp_c'];
                var temp_feels = parsed_json['current_observation']['feelslike_c'];
                var description = parsed_json['current_observation']['weather'];
                var icon_url = convertHTTPtoHTTPS(parsed_json['current_observation']['icon_url']);
                var precip = parsed_json['current_observation']['precip_today_metric'];

                var wind_direction = parsed_json['current_observation']['wind_dir'];
                var wind_kph = parsed_json['current_observation']['wind_kph'];
                var wind_string = wind_kph + ' KPH ' + wind_direction;

                var humidity = parsed_json['current_observation']['relative_humidity'];

                $('#weather_temp').html(temp_c + "&deg;");
                $('#weather_temp_feelslike').html(temp_feels + "&deg;");
                $('#weather_detail_description').html(description);
                $('#weather_wind').html(wind_string);
                $('#weather_detail_icon').html("<img src=\"" + icon_url + "\">");
                $('#weather_temp_precip').html(precip + " mm");
                $('#weather_temp_humid').html(humidity);
            }
        });


    /* Use this section for openweathermap API */
    /*
    var JSONPath = dashboardDataCollectorRoot + "/Proxy/JSON.aspx?url=http://api.openweathermap.org/data/2.5/weather?q=North%20Battleford,%20SK"
    $.getJSON(JSONPath, function(data) {
        $('#weather_temp').html(convertKToCelcius(data.main.temp) + "&deg;C&nbsp;");
        //$('#weather_detail_description').html(data.weather[0].main);
        $('#weather_detail_description').html(data.weather[0].description);
        $('#weather_detail_icon').html("<img src=\"http://openweathermap.org/img/w/" + data.weather[0].icon + ".png\">");

        /*
        if ((convertKToCelcius(data.main.temp) > 35) || (convertKToCelcius(data.main.temp) < -30))
        {
            $('#weather_temp').addClass("weather_alert");
        }
        else
        {
            $('#weather_temp').removeClass("weather_alert");
        }

    });
*/
}