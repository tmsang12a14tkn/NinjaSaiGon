/*eslint eqeqeq: ["error", "smart"]*/

var map, autocomplete;
var directionsService;
var defaultPostion = {
    lat: 10.770896,
    lng: 106.699146
};
var delta = 0.0001;
var currentPostionCache = {
    lat: 0,
    lng: 0
};
var mapMinHeight = 0;
var mapMaxHeight = 0;
var extend = false;
var addheight = 0;
var data_strict = {};
var empty_input = false;
var isCalShip = false;
var validateAddress = false;
var personalAddresses = undefined;
$(document).ready(function () {
    $.get("/js/data_state_district.json", function (data) {
        data_strict = data;
    });
    $("#AddressLine").keyup(function () {
        if ($(this).val() == "") {
            empty_input = true;
        } else {
            empty_input = false;
        }
    });
});
function renderPosition(res) {
    var address = res.formatted_address.split(",");
    var addLength = address.length;
    var country = address[addLength - 1];
    var city = address[addLength - 2];
    var district = address[addLength - 3];
    var number = "";
    for (var i = 0; i < addLength - 3; i++) {
        if (address[i] != "Unnamed Road") {
            if (i == addLength - 4) {
                number += address[i];
            } else {
                number += address[i] + ", ";
            }
        }
    }
    if (number == "Unnamed Road" || number == "") {
        number = "Không có địa chỉ";
    }
    if (checkCountry(country)) {
        var state = checkState(city);
        if (state) {
            var idDistrict = checkDistrict(district, state.index);
            if (idDistrict != 0) {
                validateAddress = true;
                $("#selected_address").val(res.place_id);
                $("#id_state").val(state.id);
                $("#id_district").val(idDistrict);
            } else {
                validateAddress = false;
            }
        } else {
            validateAddress = false;
        }
    } else {
        validateAddress = false;
    }

    $("#formattedAddress").val(res.formatted_address);

    if (validateAddress) {
        if (!isCalShip) {
            isCalShip = true;
            var desPosition = new google.maps.LatLng(res.geometry.location.lat, res.geometry.location.lng);
            calculateAndDisplayRoute(directionsService, desPosition);
        } else {
            var desPosition = new google.maps.LatLng(res.geometry.location.lat(), res.geometry.location.lng());
            calculateAndDisplayRoute(directionsService, desPosition);
        }
    }
    
    $("#AddressLine").val(number + ", " + district + ", " + city);
}
function checkState(city) {
    for (var i = 0; i < data_strict.state.length; i++) {
        for (var x = 0; x < data_strict.state[i].name.length; x++) {
            if (data_strict.state[i].name[x] == city.trim()) {
                return { index: i, id: data_strict.state[i].id };
            }
        }
    }
    return 0;
}
function checkDistrict(district, index) {
    district = district
        .replace(/Quận/g, "")
        .replace(/ /g, "")
        .replace(/Huyện/g, "")
        .toLowerCase();
    for (var i = 0; i < data_strict.state[index].district.length; i++) {
        var district_dbs = data_strict.state[index].district[i].name
            .replace(/Quận/g, "")
            .replace(/Huyện/g, "")
            .replace(/ /g, "")
            .toLowerCase();
        if (district == district_dbs) return data_strict.state[index].district[i].id;
    }
}
function checkCountry(country) {
    if (country.trim() == "Việt Nam" || country.trim() == "Vietnam") {
        return true;
    }
    return false;
}
function initMap() {
    try {
        $(".marker-google").css("opacity", 0);
        map = new google.maps.Map(document.getElementById("map"), {
            zoom: 15,
            center: defaultPostion,
            disableDefaultUI: true
        });
        directionsService = new google.maps.DirectionsService();
    } catch (e) {
        console.log("Error: ", e.message);
    }
}
function calculateAndDisplayRoute(directionsService, desPosition) {
    directionsService.route({
        origin: new google.maps.LatLng(defaultPostion.lat, defaultPostion.lng),
        destination: desPosition,
        travelMode: google.maps.DirectionsTravelMode.DRIVING,
    }, function (response, status) {
        if (status === google.maps.DirectionsStatus.OK) {
            var bestRoute = response.routes[0].legs[0];
            updateFeeShipFromMap(bestRoute.distance.value);
        }
    });
}  
function setupPersonalAddress(addresses) {
    personalAddresses = addresses.data;
    if (personalAddresses.length > 0) {
        fillPersonalAddress(0);
    }
};
function autocompleteGoogleMap() {
    var options = {
        types: [],
        componentRestrictions: {
            country: "vn"
        }
    };
    if (!autocomplete) {
        autocomplete = new google.maps.places.Autocomplete(
            document.getElementById("AddressLine"),
            options
        );
        autocomplete.addListener("place_changed", fillInAddress);
        setTimeout(function () {
            if (personalAddresses.length > 0) {
                for (var i = 0; i < personalAddresses.length; i++) {
                    $(".pac-container").append(
                        '<div id="areasearch" class="pac-item" onmousedown="fillPersonalAddress(' +
                        i +
                        ')"><span class="pac-icon icon-airport"></span><span class="pac-item-query"><span class="pac-matched"></span><strong>' +
                        personalAddresses[i].name +
                        "</strong> - " +
                        personalAddresses[i].address +
                        "</span> <span>it's Working</span></div>"
                    );
                }
            }
        }, 500);
    }
};
$("#AddressLine").click(function () {
    autocompleteGoogleMap();
});
function fillPersonalAddress(index) {
    var address = personalAddresses[index];
    var newPostion = {
        lat: parseFloat(address.latitude),
        lng: parseFloat(address.longitude)
    };
    $(".reload-location").css("opacity", 0);
    $("#my-current-location-button").show();
    $(".reload-location").hide();
    $("#my-current-location-button").css("opacity", "1");
    $("#my-current-location-button").css("pointer-events", "auto");
    $(".loading-map").hide();
    $(".theme-mobile .loading-map-mobile").hide();
    $(".marker-google").css("opacity", 1);
    $("#numberAddress").val(address.address);
    $("#id_state").val(address.state_id);
    $("#id_district").val(address.district_id);
    $("#selected_address").val(address.id);
    $("#lat").val(address.latitude);
    $("#lng").val(address.longitude);
    $("#cityAddress").val(address.state);
    $("#districtAddress").val(address.district);
    $("#formattedAddress").val(address.address);

    map.panTo(newPostion);

    var desPosition = new google.maps.LatLng(newPostion.lat, newPostion.lng);
    calculateAndDisplayRoute(directionsService, desPosition);
};
function fillInAddress() {
    var place = autocomplete.getPlace();
    renderPosition(place);
    var lat = place.geometry.location.lat();
    var lng = place.geometry.location.lng();
    var newPostion = {
        lat: lat,
        lng: lng
    };
    map.panTo(newPostion);

    var desPosition = new google.maps.LatLng(newPostion.lat, newPostion.lng);
    calculateAndDisplayRoute(directionsService, desPosition);
};
function errorHandler(err) {
    console.warn("ERROR(" + err.code + "): " + err.message);
};