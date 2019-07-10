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
    DisplayData: {
        Initiate: function (data) {
            $('.FileSelector').hide('slow', function () {
                $('.AppBody').show('slow');
            });
            IncidentApp.Map.Initiate(data);
            IncidentApp.Weather.Initiate(data);
            IncidentApp.ParcelData.Initiate(data);
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
        AddMarker: function(location, map, label) {
            var marker = new google.maps.Marker({
                position: location,
                label: label,
                map: map
            });
        },
        AfterRender: function (param) {
            
        },
        //just for reference, key is in the script tag in Index.cshtml.
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
        Initiate: function (data) {
            console.log('Todo..');
        }
    },
    Weather: {
        Initiate: function (data) {
            console.log('Todo..');
        }
    }
};

$(function () {IncidentApp.Initialize();});