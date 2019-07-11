var IncidentApp = {
    AddFile: {
        Complete: function (data) {
            if (typeof data === 'string') {
                //successful call but error was returned.
                alert(data);
            } else {
                IncidentApp.DisplayData.Initiate(data);
            }
        },
        Error: function () {
            alert('Error Posting!');
        },
        Initiate: function (e) {
            var files = e.target.files;
            if (files.length > 0) {
                var toPost = new FormData();
                toPost.append("File1", files[0]);
                $.ajax({
                    type: "POST",
                    url: "/Home/ProcessFile",
                    contentType: false,
                    processData: false,
                    data: toPost,
                    success: IncidentApp.AddFile.Complete,
                    error: IncidentApp.AddFile.Error
                });

            } else {
                alert('No Files Selected.');
            }
        }
    },
    AdditionalAttributes: {
        Initiate: function (data) {
            $('.ShowAdditionalAttributes').on('click', function () { $('.AdditionalAttributesWindow').show('slow'); });
            $('.AdditionalAttributesWindow>.Close').on('click', function () { $('.AdditionalAttributesWindow').hide('slow'); });
            IncidentApp.AdditionalAttributes.PopulateBody(data);
        },
        PopulateBody: function (data) {
            $('.AdditionalAttributesBody').remove('.Detail');
            $('.AdditionalAttributesBody').append("<h3>Address Information</h1>");
            $.each(data.address, function (key, val) { IncidentApp.CreateDetail(key, val, '.AdditionalAttributesBody'); });
            $('.AdditionalAttributesBody').append("<h3>Incident Descripion</h1>");
            $.each(data.description, function (key, val) { IncidentApp.CreateDetail(key, val, '.AdditionalAttributesBody'); });
        }
    },
    CreateDetail: function (key, val, bodySelector) {
        if (key === 'time')
            return;
        var weatherDetails =
            '<div class="Detail">' +
            '<div class="DetailLabel"><label>' + key + '</label></div>' +
            '<div><label class="DetailValue">' + val + '</label></div>' +
            '</div>';
        $(bodySelector).append(weatherDetails);
    },
    DisplayData: {
        Initiate: function (data) {
            $('.FileSelector').hide('slow', function () {
                $('.AppBody').show('slow');
            });
            IncidentApp.Map.Initiate(data);
            IncidentApp.Weather.Initiate(data);
            IncidentApp.ParcelData.Initiate(data);
            IncidentApp.AdditionalAttributes.Initiate(data);
        }
    },
    Initialize: function () {
        if (!IncidentApp.Initialized) {
            $('.AddFile').on('change', function (e) { IncidentApp.AddFile.Initiate(e); });
            IncidentApp.Initialized = true;
        }
    },
    Initialized: false,
    Map: {
        AddEachApparatus: function (data, map) {

        },
        AddMarker: function (location, map, label) {
            var marker = new google.maps.Marker({
                position: location,
                label: label,
                map: map
            });
        },
        AfterRender: function (param) {

        },
        //just for reference, key is in the script tag in Index.cshtml.
        //This key, the Weather API and Parcel API keys would typicaly be stored as 
        //NOT in JavaScript/clien side code but using some securing mecanism in Source Control
        //avaiable to the server code.  In my case, we have one sensitive configuration item 
        //that we store as an encrypted string and the application decrypts it when it is 
        //accessed.
        SuperSecretAPIKey: "AIzaSyBNyTTI985RbrjLcU2gA5EVCOCfiKEbcEw",
        Initiate: function (data) {
            var location = { lat: data.address.latitude, lng: data.address.longitude };
            var map = new google.maps.Map(document.getElementById('TheMap'), {
                center: location,
                zoom: 12
            });
            IncidentApp.Map.AddMarker(location, map, '');
            IncidentApp.Map.AddEachApparatus(data, map);
        }
    },
    ParcelData: {
        AfterCall: function (data) {
            console.log(data);
        },
        Initiate: function (data) {
            $('.ShowParcel').on('click', function () { $('.ParcelDataWindow').show('slow'); });
            $('.ParcelDataWindow>.Close').on('click', function () { $('.ParcelDataWindow').hide('slow'); });
            IncidentApp.ParcelData.PopulateBody(data);
        },
        PopulateBody: function (data) {
            var param = {
                latitude: data.address.latitude,
                longitude: data.address.longitude,
                eventDateUnixTime: IncidentApp.Weather.GetUnixTime(data.description.event_opened)
            };
            var tempDate = data.description.event_opened;
            var url = '/Home/ParcelAPI';
            $.post(url, param)
                .done(IncidentApp.ParcelData.AfterCall)
                .fail(function () { alert('Error calling Parcel API.'); });
        }
    },
    Weather: {
        AfterCall: function (data, eventOccurred) {
            $('.WeatherDataBody').remove('.Detail');
            $('.WeatherTime').text(eventOccurred);
            $.each(data.currently, function (key, val) { IncidentApp.CreateDetail(key, val, '.WeatherDataBody'); });
        },
        GetUnixTime: function (fromData) {
            return Math.round((new Date(fromData)).getTime() / 1000);
        },
        Initiate: function (data) {
            $('.ShowWeather').on('click', function () { $('.WeatherDataWindow').show('slow'); });
            $('.WeatherDataWindow>.Close').on('click', function () { $('.WeatherDataWindow').hide('slow'); });
            IncidentApp.Weather.PopulateBody(data);
        },
        PopulateBody: function (data) {
            var param = {
                latitude: data.address.latitude,
                longitude: data.address.longitude,
                eventDateUnixTime: IncidentApp.Weather.GetUnixTime(data.description.event_opened)
            };
            var tempDate = data.description.event_opened;
            var url = '/Home/WeatherAPI';
            $.post(url, param)
                .done(function (data) { IncidentApp.Weather.AfterCall(data, tempDate); })
                .fail(function () { alert('Error calling Weather API.'); });
        }
    }
};

$(function () {IncidentApp.Initialize();});