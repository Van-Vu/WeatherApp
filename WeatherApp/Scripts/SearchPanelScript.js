$(document).ready(function () {
    $('#FromDate').datepicker();
    $('#ToDate').datepicker();
    $('#spinner').css({"display": "none"});
});

$('#btnSearch').click(function() {
    $(this).validate();
});

function Loaded(result) {
    var length = result.length;
    var resultText = "<table style=\"width: 650px; margin: 0 auto\"><tr><td  class=\"width-35percent margin-0\"><b>Station Name</b></td><td class=\"width-20percent margin-0\"><b>DateTime</b></td><td class=\"width-20percent margin-0\"><b>Temperature</b></td><tr>";
    
    for (var i = 0; i < length; i++) {
        var element = result[i];

        var src = element.DateTime;
        src = src.replace(/[^0-9 +]/g, '');
        var myDate = new Date(parseInt(src));
        var strDate = (myDate.getMonth() + 1) + "/" + myDate.getDate() + "/" + myDate.getFullYear() + " " + myDate.getHours() + ":" + myDate.getMinutes() + ":" + myDate.getSeconds();
        
        resultText += "<tr><td>" + element.StationName + "</td><td>" + strDate + "</td><td>" + element.Temperature + "</td></tr>";
    }
    resultText += "</table>";

    if (length == 0) {
        $("#searchresult").html("No result found");
    } else {
        $("#searchresult").html(resultText);
    }
}
