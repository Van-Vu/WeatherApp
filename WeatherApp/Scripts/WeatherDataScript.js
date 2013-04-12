$(document).ready(function () {
    $('#latestdata').dataTable({
        "sDom": '<"top"ifp>rt<"bottom"ifp><"clear">',
        "bJQueryUI": false,
        "iDisplayLength": 20,
        "sPaginationType": "full_numbers",
        "bProcessing": true,
        "bServerSide": true,
        "sAjaxSource": "/Home/GetLatestData",
        "bRetrieve": false,
        "bDestroy": true,
        "bFilter": false,
        "aoColumns": [{ "bSortable": false }, { "bSortable": false }, { "sClass": "alignment-right", "bSortable": false }],
        "bSortable": false,
        "fnServerData": function (url, data, callback) {
            $.ajax({
                "url": url,
                "data": data,
                "success": callback,
                "contentType": "application/x-www-form-urlencoded; charset=utf-8",
                "dataType": "json",
                "type": "POST",
                "cache": false,
                "error": function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.statusText);
                    alert(thrownError);
                }
            });
        }
    });
})
