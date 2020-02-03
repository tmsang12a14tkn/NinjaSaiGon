var feeShip = 0;
var tempShip = 0;

const numberWithCommas = (x) => {
    return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
}

function round(value, precision) {
    var multiplier = Math.pow(10, precision || 0);
    return Math.round(value * multiplier) / multiplier;
}

var updateFeeShipFromMap = function (distance) {
    if (distance > 1000) {
        var multiply = round(distance / 1000, 1);
        feeShip = multiply * 5000;
        $('#lb-Distance').html(multiply + " km");
    } else {
        feeShip = 0;
        $('#lb-Distance').html("");
    }
    $('#Distance').val(distance);
    $('#lb-ShipFee').val(numberWithCommas(feeShip));

    if ($('#qc-order-details').length > 0) {
        var IsAutoShipFee = $('#IsAutoShipFee').bootstrapSwitch('state');
        if (IsAutoShipFee) {
            $('#ShipFee').val(feeShip);
        }
        updateCartTotal();
    } else {
        $('#ShipFee').val(feeShip);
    }
};