google.load('visualization', '1', { packages: ['gauge'] });
google.setOnLoadCallback(drawChart);

function drawChart() {
    var practiceId = urlParam('practiceId');
    $.get('/Client/GetData?practiceId=' + practiceId, {},
        function (data) {
            var tdata = new google.visualization.DataTable();
            tdata.addColumn('string', 'Label');
            tdata.addColumn('number', 'Value');

            for (var i = 0; i < data.length; i++) {
                tdata.addRow([data[i].Name, data[i].ExpenseToCashOnHandPercentage]);
            }

            var options = {
                width: 1000,
                height: 120,
                greenFrom: 0,
                greenTo: 50,
                redFrom: 75,
                redTo: 100,
                yellowFrom: 50,
                yellowTo: 75,
                minorTicks: 5
            };

            var chart = new google.visualization.Gauge(document.getElementById('chart_div'));
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