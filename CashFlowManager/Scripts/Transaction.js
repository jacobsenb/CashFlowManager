

jQuery(document).ready(function ($) {
    var selectedItem = $("#Frequency :selected").text();
    if (selectedItem != "One Off") {
        $('#StartDatelbl').css('visibility', 'visible');
        $('#StartDateEdt').css('visibility', 'visible');
    }
    else {
        $('#StartDatelbl').css('visibility', 'hidden');
        $('#StartDateEdt').css('visibility', 'hidden');

    }

});

function ShowStartDate() {
    var selectedItem = $("#Frequency :selected").text();
    if (selectedItem != "One Off") {
        $('#StartDatelbl').css('visibility','visible');
        $('#StartDateEdt').css('visibility', 'visible');
    }
    else
    {
        $('#StartDatelbl').css('visibility', 'hidden');
        $('#StartDateEdt').css('visibility', 'hidden');

    }
}