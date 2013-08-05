

google.load('visualization', '1', { packages: ['gauge'] });



function hello() {
    alert("Hello");
}

function drawChart(clientId) {
    var practiceId = urlParam('practiceId');
    $.get('/CashFlowManager/Client/GetData?practiceId=' + practiceId + '&clientId=' + clientId, {},
        function (data) {
            var tdata = new google.visualization.DataTable();
            tdata.addColumn('string', 'Label');
            tdata.addColumn('number', 'Value');

            for (var i = 0; i < data.length; i++) {
                tdata.addRow([data[i].Name, data[i].ExpenseToCashOnHandPercentage]);
            }

            var options = {
                min: 0,
                max: 100,
                width: 810,
                height: 120,
                greenFrom: 0,
                greenTo: 25,
                redFrom: 75,
                redTo: 100,
                yellowFrom: 25,
                yellowTo: 75,
                minorTicks: 5,
                animation: {easing:'in',duration:2000}                
            };

            var chart = new google.visualization.Gauge(document.getElementById(clientId));
            chart.draw(tdata, options);
        });
}

function urlParam(name) {
    var results = new RegExp('[\\?&]' + name + '=([^&#]*)').exec(window.location.href);
    if (results == null) {
        return null;
    }
    else {
        return results[1] || 0;
    }
}