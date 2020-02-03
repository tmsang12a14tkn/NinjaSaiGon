/*eslint eqeqeq: ["error", "smart"]*/

var map, autocomplete;
var directionsService;
var markers = [];
var defaultPostion = {
    lat: 10.770896,
    lng: 106.699146
};
var delta = 0.0001;
var LS = LocalStore("NinjaSaiGon");
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
    $.get("/Map/Data", function (data) {
        data_strict = data;
    });
    $("#numberAddress").keyup(function () {
        if ($(this).val() == "") {
            empty_input = true;
        } else {
            empty_input = false;
        }
    });
});
function renderPosition(res) {
    $(".reload-location").css("opacity", 0);
    $("#my-current-location-button").show();
    $(".reload-location").hide();
    $("#my-current-location-button").css("opacity", "1");
    $("#my-current-location-button").css("pointer-events", "auto");
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
                $(".tch-warning-country").hide();
                $(".tch-warning-state").hide();
                $(".tch-warning-district").hide();
            } else {
                validateAddress = false;
                $(".tch-warning-country").hide();
                $(".tch-warning-state").hide();
                $(".tch-warning-district").show();
                $(".tch-warning-district").removeClass("hidden");
            }
        } else {
            validateAddress = false;
            $(".tch-warning-country").hide();
            $(".tch-warning-state").show();
            $(".tch-warning-state").removeClass("hidden");
            $(".tch-warning-district").hide();
        }
    } else {
        validateAddress = false;
        $(".tch-warning-country").show();
        $(".tch-warning-country").removeClass("hidden");
        $(".tch-warning-state").hide();
        $(".tch-warning-district").hide();
    }
    $("#cityAddress").val(city);
    $("#districtAddress").val(district);

    $("#formattedAddress").val(res.formatted_address);

    if (validateAddress) {
        $('#menu-btn').prop('disabled', false);
        $('#menu-btn').css('opacity', 1);
        $('#payment-btn').prop('disabled', false);
        $('#payment-btn').css('opacity', 1);
        $('#view-cart-btn').prop('disabled', false);
        $('#view-cart-btn').css('opacity', 1);
        
        if (!isCalShip) {
            isCalShip = true;
            var desPosition = new google.maps.LatLng(res.geometry.location.lat, res.geometry.location.lng);
            calculateAndDisplayRoute(directionsService, desPosition);
        } else {
            var desPosition = new google.maps.LatLng(res.geometry.location.lat(), res.geometry.location.lng());
            calculateAndDisplayRoute(directionsService, desPosition);
        }
    } else {
        $('#menu-btn').prop('disabled', true);
        $('#menu-btn').css('opacity', 0.4);
        $('#payment-btn').prop('disabled', true);
        $('#payment-btn').css('opacity', 0.4);
        $('#view-cart-btn').prop('disabled', true);
        $('#view-cart-btn').css('opacity', 0.4);
    }

    //var text_address = "";
    //if (res.name && res.name != "") {
    //    $("#numberAddress").val(res.name);
    //} else {
    //    $("#numberAddress").val(res.formatted_address);
    //}
    //$("#shortAddress").val(number + ", " + district + ", " + city);
    //if (empty_input) {
    //    $("#shortAddress").val("");
    //}
    $(".loading-map").hide();
    $(".theme-mobile .loading-map-mobile").hide();
}
function checkState(city) {
    for (var i = 0; i < data_strict.cities.length; i++) {
        for (var x = 0; x < data_strict.cities[i].name.length; x++) {
            if (city.indexOf(data_strict.cities[i].name[x]) >= 0) {
                return { index: i, id: data_strict.cities[i].id };
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
    for (var i = 0; i < data_strict.cities[index].districts.length; i++) {
        var district_dbs = data_strict.cities[index].districts[i].name
            .replace(/Quận/g, "")
            .replace(/Huyện/g, "")
            .replace(/ /g, "")
            .toLowerCase();
        if (district == district_dbs) return data_strict.cities[index].districts[i].id;
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
function clearMarkers() {
    for (var i = 0; i < markers.length; i++) {
        markers[i].setMap(null);
    }
    markers = [];
}
function autocompleteGoogleMap() {
    var options = {
        types: [],
        componentRestrictions: {
            country: "vn"
        }
    };
    if (!autocomplete) {
        autocomplete = new google.maps.places.Autocomplete(
            document.getElementById("numberAddress"),
            options
        );
        autocomplete.addListener("place_changed", fillInAddress);
    }
};
$("#numberAddress").click(function () {
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
    //$("#numberAddress").val(address.address + ', ' + address.district + ', ' + address.state);
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
    clearMarkers();
    var marker = new google.maps.Marker({
        position: desPosition,
        icon: './images/marker.png'
    });
    marker.setMap(map);
    marker.setAnimation(google.maps.Animation.BOUNCE);
    markers.push(marker);
    calculateAndDisplayRoute(directionsService, desPosition);
};
function backToMyCurrentLocation() {
    if ($(".map-container").css("opacity") == 0) {
        if (window.location.hash == "#menu" || window.location.hash == "#order") {
            first = true;
            $(".map-container").css("opacity", 1);
            $(".loading-map").css("opacity", 1);
            $(".map-container").slideToggle();
            $("#close-map").show();
            if (window.location.hash == "#menu") {
                $(".menu").slideToggle();
            }
        } else {
            $(".map-container").css("opacity", 1);
            $(".loading-map").css("opacity", 1);
            $(".iframe-main").slideToggle();
            $("#close-map").show();
            if (first == true) {
                $(".map-container").slideToggle();
            }
            first = true;
        }
    }
    navigator.geolocation.getCurrentPosition(positionCallback, errorHandler);
};
$("#my-current-location-button").click(function () {
    backToMyCurrentLocation();
    timeOutLocation();
});
function errorHandler(err) {
    console.warn("ERROR(" + err.code + "): " + err.message);
}
function findCacheLocation(gps) {
    var locationList = LS.get("location") || [];
    var minDelta = 10e9;
    var result = false;
    for (var i = 0; i < locationList.length; i++) {
        var loc = locationList[i];
        var locGps = loc.geometry.location;
        var cst = (Math.abs(locGps.lat - gps.lat) + Math.abs(locGps.lng - gps.lng)) / 2;
        if (cst < delta && cst < minDelta) {
            minDelta = cst;
            result = loc;
        }
    }
    return result;
}
$(document).ready(function () {
    calcHeightGoogleMapMobile();
});
$(window).resize(function () {
    if (extend) {
        calcHeightGoogleMapMobileExtend();
    } else {
        calcHeightGoogleMapMobile();
    }
});
function calcHeightGoogleMapMobile() {
    var windowheight = $(window).height();
    var headerheight = $(".header-mobile").height();
    addheight = $(".input-address-container").height();
    mapMinHeight = windowheight - headerheight - addheight - 130;
    mapMaxHeight = windowheight - headerheight - 40 - 34 - 90;
    $(".theme-mobile .map-container-mobile").css("height", mapMinHeight);
    $(".theme-mobile .loading-map-mobile").css("height", mapMinHeight);
}
function calcHeightGoogleMapMobileExtend() {
    var windowheight = $(window).height();
    var headerheight = $(".header-mobile").height();
    mapMinHeight = windowheight - headerheight - addheight - 130;
    mapMaxHeight = windowheight - headerheight - 40 - 34 - 90;
    $(".theme-mobile .map-container-mobile").css("height", mapMaxHeight);
    $(".theme-mobile .loading-map-mobile").css("height", mapMaxHeight);
}