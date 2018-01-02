    var _from, _to;

$(function () {


    


});
function calc() {
    _from = $("#dateFrom").val();
    _to = $("#dateTo").val();
    var price = $("#price").val();
    if (_to != null && _from != null) {
        var from = new Date(_from);
        var to = new Date(_to);
        var calc = (to - from) / 86400000;
        if (calc <= 0) {
            $("#dateTo").val("");
            alert('זמן התחלה קטן או שווה לזמן סיום ');
            $("#dateTo").focus();

        }
        else {

            if ($("#dateTo").val() != "") {
                $('#priceLable').text(calc * price + '₪')
                isCarAvailable();

            }
        }
    }
   
};

function isCarAvailable()
{
    $.get("/Orders/IsCarAvailable", { car: $("#car").val(), _from: $("#dateFrom").val(), _to: $("#dateTo").val() }, function (result) {
        if(!result)
        {
           
            alert('המכונית אינה פנויה בתאריך זה');
            $("#dateTo").val("");
            $("#dateFrom").val("");
            $('#priceLable').text("");
        }

    })
}


