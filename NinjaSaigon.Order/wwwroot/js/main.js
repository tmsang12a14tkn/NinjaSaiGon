!function (e) { if ("function" == typeof define && define.amd) define(["jquery"], e); else if ("object" == typeof exports) { var n = require("jquery"); module.exports = e(n) } else e(window.jQuery || window.Zepto || window.$) }(function (e) { "use strict"; e.fn.serializeJSON = function (n) { var r, s, t, i, a, u, l, o, p, c, d, f, y; return r = e.serializeJSON, s = this, t = r.setupOpts(n), i = s.serializeArray(), r.readCheckboxUncheckedValues(i, t, s), a = {}, e.each(i, function (e, n) { u = n.name, l = n.value, p = r.extractTypeAndNameWithNoType(u), c = p.nameWithNoType, (d = p.type) || (d = r.attrFromInputWithName(s, u, "data-value-type")), r.validateType(u, d, t), "skip" !== d && (f = r.splitInputNameIntoKeysArray(c), o = r.parseValue(l, u, d, t), (y = !o && r.shouldSkipFalsy(s, u, c, d, t)) || r.deepSet(a, f, o, t)) }), a }, e.serializeJSON = { defaultOptions: { checkboxUncheckedValue: void 0, parseNumbers: !1, parseBooleans: !1, parseNulls: !1, parseAll: !1, parseWithFunction: null, skipFalsyValuesForTypes: [], skipFalsyValuesForFields: [], customTypes: {}, defaultTypes: { string: function (e) { return String(e) }, number: function (e) { return Number(e) }, boolean: function (e) { return -1 === ["false", "null", "undefined", "", "0"].indexOf(e) }, null: function (e) { return -1 === ["false", "null", "undefined", "", "0"].indexOf(e) ? e : null }, array: function (e) { return JSON.parse(e) }, object: function (e) { return JSON.parse(e) }, auto: function (n) { return e.serializeJSON.parseValue(n, null, null, { parseNumbers: !0, parseBooleans: !0, parseNulls: !0 }) }, skip: null }, useIntKeysAsArrayIndex: !1 }, setupOpts: function (n) { var r, s, t, i, a, u; u = e.serializeJSON, null == n && (n = {}), t = u.defaultOptions || {}, s = ["checkboxUncheckedValue", "parseNumbers", "parseBooleans", "parseNulls", "parseAll", "parseWithFunction", "skipFalsyValuesForTypes", "skipFalsyValuesForFields", "customTypes", "defaultTypes", "useIntKeysAsArrayIndex"]; for (r in n) if (-1 === s.indexOf(r)) throw new Error("serializeJSON ERROR: invalid option '" + r + "'. Please use one of " + s.join(", ")); return i = function (e) { return !1 !== n[e] && "" !== n[e] && (n[e] || t[e]) }, a = i("parseAll"), { checkboxUncheckedValue: i("checkboxUncheckedValue"), parseNumbers: a || i("parseNumbers"), parseBooleans: a || i("parseBooleans"), parseNulls: a || i("parseNulls"), parseWithFunction: i("parseWithFunction"), skipFalsyValuesForTypes: i("skipFalsyValuesForTypes"), skipFalsyValuesForFields: i("skipFalsyValuesForFields"), typeFunctions: e.extend({}, i("defaultTypes"), i("customTypes")), useIntKeysAsArrayIndex: i("useIntKeysAsArrayIndex") } }, parseValue: function (n, r, s, t) { var i, a; return i = e.serializeJSON, a = n, t.typeFunctions && s && t.typeFunctions[s] ? a = t.typeFunctions[s](n) : t.parseNumbers && i.isNumeric(n) ? a = Number(n) : !t.parseBooleans || "true" !== n && "false" !== n ? t.parseNulls && "null" == n ? a = null : t.typeFunctions && t.typeFunctions.string && (a = t.typeFunctions.string(n)) : a = "true" === n, t.parseWithFunction && !s && (a = t.parseWithFunction(a, r)), a }, isObject: function (e) { return e === Object(e) }, isUndefined: function (e) { return void 0 === e }, isValidArrayIndex: function (e) { return /^[0-9]+$/.test(String(e)) }, isNumeric: function (e) { return e - parseFloat(e) >= 0 }, optionKeys: function (e) { if (Object.keys) return Object.keys(e); var n, r = []; for (n in e) r.push(n); return r }, readCheckboxUncheckedValues: function (n, r, s) { var t, i, a; null == r && (r = {}), e.serializeJSON, t = "input[type=checkbox][name]:not(:checked):not([disabled])", s.find(t).add(s.filter(t)).each(function (s, t) { if (i = e(t), null == (a = i.attr("data-unchecked-value")) && (a = r.checkboxUncheckedValue), null != a) { if (t.name && -1 !== t.name.indexOf("[][")) throw new Error("serializeJSON ERROR: checkbox unchecked values are not supported on nested arrays of objects like '" + t.name + "'. See https://github.com/marioizquierdo/jquery.serializeJSON/issues/67"); n.push({ name: t.name, value: a }) } }) }, extractTypeAndNameWithNoType: function (e) { var n; return (n = e.match(/(.*):([^:]+)$/)) ? { nameWithNoType: n[1], type: n[2] } : { nameWithNoType: e, type: null } }, shouldSkipFalsy: function (n, r, s, t, i) { var a = e.serializeJSON.attrFromInputWithName(n, r, "data-skip-falsy"); if (null != a) return "false" !== a; var u = i.skipFalsyValuesForFields; if (u && (-1 !== u.indexOf(s) || -1 !== u.indexOf(r))) return !0; var l = i.skipFalsyValuesForTypes; return null == t && (t = "string"), !(!l || -1 === l.indexOf(t)) }, attrFromInputWithName: function (e, n, r) { var s, t; return s = n.replace(/(:|\.|\[|\]|\s)/g, "\\$1"), t = '[name="' + s + '"]', e.find(t).add(e.filter(t)).attr(r) }, validateType: function (n, r, s) { var t, i; if (i = e.serializeJSON, t = i.optionKeys(s ? s.typeFunctions : i.defaultOptions.defaultTypes), r && -1 === t.indexOf(r)) throw new Error("serializeJSON ERROR: Invalid type " + r + " found in input name '" + n + "', please use one of " + t.join(", ")); return !0 }, splitInputNameIntoKeysArray: function (n) { var r; return e.serializeJSON, r = n.split("["), "" === (r = e.map(r, function (e) { return e.replace(/\]/g, "") }))[0] && r.shift(), r }, deepSet: function (n, r, s, t) { var i, a, u, l, o, p; if (null == t && (t = {}), (p = e.serializeJSON).isUndefined(n)) throw new Error("ArgumentError: param 'o' expected to be an object or array, found undefined"); if (!r || 0 === r.length) throw new Error("ArgumentError: param 'keys' expected to be an array with least one element"); i = r[0], 1 === r.length ? "" === i ? n.push(s) : n[i] = s : (a = r[1], "" === i && (o = n[l = n.length - 1], i = p.isObject(o) && (p.isUndefined(o[a]) || r.length > 2) ? l : l + 1), "" === a ? !p.isUndefined(n[i]) && e.isArray(n[i]) || (n[i] = []) : t.useIntKeysAsArrayIndex && p.isValidArrayIndex(a) ? !p.isUndefined(n[i]) && e.isArray(n[i]) || (n[i] = []) : !p.isUndefined(n[i]) && p.isObject(n[i]) || (n[i] = {}), u = r.slice(1), p.deepSet(n[i], u, s, t)) } } });



// John Resig - https://johnresig.com/ - MIT Licensed
(function () {
    var cache = {};

    this.tmpl = function tmpl(str, data) {
        // Figure out if we're getting a template, or if we need to
        // load the template - and be sure to cache the result.
        var fn = !/\W/.test(str) ?
            cache[str] = cache[str] ||
            tmpl(document.getElementById(str).innerHTML) :

            // Generate a reusable function that will serve as a template
            // generator (and which will be cached).
            new Function("obj",
                "var p=[],print=function(){p.push.apply(p,arguments);};" +

                // Introduce the data as local variables using with(){}
                "with(obj){p.push('" +

                // Convert the template into pure JavaScript
                str
                    .replace(/[\r\t\n]/g, " ")
                    .split("<%").join("\t")
                    .replace(/((^|%>)[^\t]*)'/g, "$1\r")
                    .replace(/\t=(.*?)%>/g, "',$1,'")
                    .split("\t").join("');")
                    .split("%>").join("p.push('")
                    .split("\r").join("\\'")
                + "');}return p.join('');");

        // Provide some basic currying to the user
        return data ? fn(data) : fn;
    };
})();

window.normalHeight = window.innerHeight;
window.keyboardHeight = 0;

var feeShip = 0;
var tempShip = 0;
var promotionData = null;
var curDivActive = '.main';

var numberWithCommas = function (x) {
    return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
}

var cookie_value = randomString(30);
if (!readCookie('cart_id')) createCookie('cart_id', cookie_value, 1);

if (/(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|ipad|iris|kindle|Android|Silk|lge |maemo|midp|mmp|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows (ce|phone)|xda|xiino/i.test(navigator.userAgent)
    || /1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-/i.test(navigator.userAgent.substr(0, 4))) {
    $('body').css('cursor', 'pointer');
}

function randomString(length) {
    var text = "";
    var possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    for (var i = 0; i < length; i++) text += possible.charAt(Math.floor(Math.random() * possible.length));
    var currentdate = new Date();
    var datetime = currentdate.getDate() + "-"
        + (currentdate.getMonth() + 1) + "-"
        + currentdate.getFullYear() +
        + currentdate.getHours() + ":"
        + currentdate.getMinutes() + ":"
        + currentdate.getSeconds();
    return datetime + ' ' + text;
}

function createCookie(name, value, hour) {
    if (hour) {
        var date = new Date();
        date.setTime(date.getTime() + (hour * 3600 * 1000));
        var expires = "; expires=" + date.toGMTString();
    }
    else var expires = "";
    document.cookie = name + "=" + value + expires + "; path=/";
}

function readCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}

function eraseCookie(name) {
    createCookie(name, "", -1);
}

function LocalStore(localName) {
    localName = localName || "NinjaSaiGon";
    if (!localStorage[localName]) {
        localStorage[localName] = '{}';
    }
    var __localStorage = JSON.parse(localStorage[localName]),
        __readLocal = function (key) {
            return __localStorage[key];
        },
        __writeLocal = function (key, val) {
            __localStorage[key] = val;
            localStorage[localName] = JSON.stringify(__localStorage);
        },
        __clearLocal = function (key) {
            delete __localStorage[key];
            localStorage[localName] = JSON.stringify(__localStorage);
            return __localStorage[key];
        };

    return {
        get: __readLocal,
        set: __writeLocal,
        clear: __clearLocal
    };
}

function keyboardScroll(selector) {
    var fullSelector = selector + ' input, ' + selector + ' textarea';

    $(document).on('focus', fullSelector, function (event) {
        var inputTop = $(event.target).offset().top;
        var currentScroll = $(selector).scrollTop();
        var scrollTo = currentScroll + inputTop - 100;

        $(selector).animate({
            scrollTop: scrollTo
        }, 500);
    });
}

function round(value, precision) {
    var multiplier = Math.pow(10, precision || 0);
    return Math.round(value * multiplier) / multiplier;
}

if ($('#_s_ntf').val() == 1 || $('#_s_opn').val() == 0) {
    $('#siteOpenModal').modal('show');
}

$(window).on('keydown', function (e) {
    if (e.keyCode == 13) {
        e.preventDefault();
        return false;
    }
});

$(document).on('click', '#btn-preview-menu', function (e) {
    if ($('#_s_opn').val() == 0) {
        viewMenu();
    }
});

$(document).on('keyup', 'input', function (event) {
    if (event.keyCode === 13) { // 13 = Enter Key
        $(this).blur();
        event.preventDefault();
    }
});

$(window).resize(function () {
    if (window.innerHeight < window.normalHeight) {
        window.keyboardHeight = window.normalHeight - window.innerHeight;
    }
});

keyboardScroll('.order-component-mobile');
keyboardScroll('.info-component-mobile');
keyboardScroll('#loginModal');

$('.icon-help').popover({
    content: 'Phí vận chuyển áp dụng trong nội thành TP Hồ Chí Minh: 5.000đ/km.\nHỗ trợ giảm phí vận chuyển 5.000đ/ly cho đơn hàng từ 02 ly trở lên (chỉ áp dụng bắt đầu cho ly thứ 2, không áp dụng cho ly được tặng trong các chương trình khuyến mãi khác)\n* Free ship cho mọi đơn hàng trong bán kính 1km xung quanh Chợ Bến Thành hoặc đơn hàng có giá trị từ 150.000đ'
});
$('.icon-promo-help').popover({
    content: 'Cách thức áp dụng khuyến mãi mua 1 tặng 1\nKhách hàng chọn 2 món đồ uống (size và topping bất kỳ), Ninja Saigon sẽ ghi nhận ly có giá trị cao hơn vào hóa đơn, lý còn lại được tính là ly quà tặng.\nTương tự:\n' +
        '\tMua 3 ly tính tiền 2 ly giá cao nhất trong hóa đơn (Bạn vẫn còn được tặng thêm 1 ly)\n' +
        '\tMua 4 ly tính tiền 2 ly giá cao nhất trong hóa đơn\n' +
        '\tMua 5 ly tính tiền 3 ly giá cao nhất trong hóa đơn (Bạn vẫn còn được tặng thêm 1 ly)\n' +
        '\tMua 6 ly tính tiền 3 ly giá cao nhất trong hóa đơn\n' +
        '\t...\n'
});

$('body').on('click', function (e) {
    $('[data-toggle=popover]').each(function () {
        if (!$(this).is(e.target) && $(this).has(e.target).length === 0 && $('.popover').has(e.target).length === 0) {
            $(this).popover('hide');
        }
    });
});

$('.collapse').on('shown.bs.collapse', function (e) {
    var $card = $(this).closest('.menu-card');
    $card[0].scrollIntoView({ behavior: 'smooth', block: 'start' });
});

$("#name-info").on('input', function (e) {
    var result = splitName($("#name-info").val());
    $("#MiddleName").val(result.mid_name);
    $("#FirstName").val(result.first_name);
    $("#LastName").val(result.last_name);
});

$("#deliveryTime").val($("#deliveryTime").data("initvalue"));

$('#menu-btn').on('click', function (e) {
    e.preventDefault();
    viewMenu();
});

var viewMenu = function () {
    $('.main').hide();
    $('#btn-div-menu').hide();
    $('.map-container').hide();
    $('.map-container').css('opacity', 0);
    $('.loading-map').css('opacity', 0);
    $('.reload-location').css('opacity', 0);
    $('.close-map').hide();
    $('.tch-warning-address').hide();
    $('.tch-warning-country').hide();
    $('.tch-warning-state').hide();
    $('.tch-warning-district').hide();
    $('#info-ship').hide();

    $('#promo-code').show();
    $('.menu').slideToggle();
    $('#btn-div-payment').show();
    $('#view-cart-btn').prop('disabled', true);
    $('#payment-btn').prop('disabled', true);

    $('.col-left')[0].scrollIntoView({ behavior: 'smooth', block: 'start' });
    curDivActive = '.menu';
}

$('.close-map').on('click', function (e) {
    e.preventDefault();
    $(this).hide();
    $('.map-container').slideToggle();
    $('.map-container').css('opacity', 0);
    $('.loading-map').css('opacity', 0);
    $('.reload-location').css('opacity', 0);

    if (curDivActive == '.main') {
        $(curDivActive).slideToggle();
        $('#btn-div-menu').show();
    } else if (curDivActive == '.order') {
        $('#btn-div-payment').hide();
        $('#view-cart-btn').hide();
        $('#btn-div-submit-order').show();

        $('#payment-btn').prop('disabled', false);
        $('#payment-btn').css('opacity', 1);
        $('.tch-warning-address').hide();
        $('.tch-warning-country').hide();
        $('.tch-warning-state').hide();
        $('.tch-warning-district').hide();
    }
});

$('#numberAddress, #cityAddress, #districtAddress').click(function () {
    autocompleteGoogleMap();
    if ($('.map-container').css('opacity') == 0) {
        if ($('.order').is(':visible')) {
            $('.map-container').css('opacity', 1);
            $('.loading-map').css('opacity', 1);
            $('.map-container').slideToggle();
            $('#close-map').show();
            $('#btn-div-payment').show();

            $('#view-cart-btn').show();
            $('#view-cart-btn').prop('disabled', false);
            $('#view-cart-btn').css('opacity', 1);

            $('#btn-div-submit-order').hide();
            $('#payment-btn').prop('disabled', true);
            curDivActive = '.order';
        } else {
            $('.map-container').slideToggle();
            $('.map-container').css('opacity', 1);
            $('.loading-map').css('opacity', 1);
            $('.main').slideToggle();
            $('#close-map').show();
        }
    }
});

$("input[name=IsDeliveryNow]").on("change", function (e) {
    if ($(this).val() === 'true') {
        $("#deliveryDatetime").addClass("d-none");
    } else {
        $("#deliveryDatetime").removeClass("d-none");
    }
});

$('#view-cart-btn').on('click', function (e) {
    e.preventDefault();
    $('#btn-div-submit-order').show();
    $('#btn-div-menu').hide();
    $('.map-container').css('opacity', 0);
    if ($('.map-container').is(':visible')) {
        $('.map-container').slideToggle();
    }
    $('.loading-map').css('opacity', 0);
    $('.reload-location').css('opacity', 0);
    $('.close-map').hide();
    $('#view-cart-btn').hide();

    $('#payment-btn').show();
    if (countActiveItems() == 0) {
        $('#payment-btn').prop('disabled', true);
        $('#payment-btn').css('opacity', 0.4);
    } else {
        $('#payment-btn').prop('disabled', false);
        $('#payment-btn').css('opacity', 1);
    }

    $('#info-ship').show();
    $("#info-cart").hide();
    $('#promo-code').hide();
    if (curDivActive != '.order') {
        $('.menu').slideToggle();
        $('.order').slideToggle();
    }

    $('.col-left')[0].scrollIntoView({ behavior: 'smooth', block: 'start' });
    curDivActive = '.order';

    loadCartReview();
});

$('#payment-btn').on('click', function (e) {
    e.preventDefault();
    if (countActiveItems() == 0) {
        $('.title-missing-info').html('Có vẻ giỏ hàng đang trống, hãy chọn món thêm');
        $('#missingInfoCustomerModal').modal('show');
        return false;
    }
    if (checkValidateCustomerInfo()) {
        $('#payment-btn').attr('disabled', 'disabled');
        $("#createOrderForm").submit();
    }
});

$("#cart-review").on('click', '#return2Menu', function (e) {
    e.preventDefault();

    $('.menu').slideToggle();
    $('.order').slideToggle();
    $('#payment-btn').hide();
    $('#btn-div-submit-order').hide();
    $('#btn-div-payment').show();
    $('#view-cart-btn').show();
    $('#info-ship').hide();
    $("#info-cart").show();
    $('#promo-code').show();

    if (countActiveItems() == 0) {
        $('#view-cart-btn').prop('disabled', true);
        $('#view-cart-btn').css('opacity', 0.4);
    } else {
        $('#view-cart-btn').prop('disabled', false);
        $('#view-cart-btn').css('opacity', 1);
    }
    curDivActive = '.menu';
});

$("#cart-review").on('click', '.btn-editDrinkCart', function (e) {
    e.preventDefault();

    var index = $(this).closest(".cart-item").data("index");

    var orderData = $('#createOrderForm').serializeJSON();
    var orderDetail = orderData.OrderDetails[index];
    
    var endpoint = '/Order/DrinkOptions?index=' + index;
    $.post(endpoint, orderDetail, function (data) {
        $('#topping .modal-content').html(data);
        $('#topping').modal({ show: true });

        updateTotalDrinkPrice();
    });
});

$("#cart-review").on('click', '.btn-removeDrinkCart', function (e) {
    e.preventDefault();
    if (confirm("Bạn có muốn xóa món này trong giỏ hàng!")) {
        var $cartItemPreview = $(this).closest(".cart-item");
        var index = $cartItemPreview.data("index");
        $cartItemPreview.remove();

        removeCartItem(index);

        if ($('#tb-cart-preview .cart-item:visible').length == 0) {
            $('#payment-btn').prop('disabled', true);
            $('#payment-btn').css('opacity', 0.4);
        } else {
            $('#payment-btn').prop('disabled', false);
            $('#payment-btn').css('opacity', 1);
        }

        updateCartItemsPrice();
    }
});

var removeCartItem = function (index) {
    var $cartItem = $("#cart-items .cart-item[data-index=" + index + "]");
    $cartItem.find(".isDeleted").val("true");
    $cartItem.hide();
}

var loadCartReview = function () {
    var endpoint = '/Order/GetCart';
    $.ajax({
        type: "POST",
        url: endpoint,
        data: $('#createOrderForm').serializeArray(),
        success: function (data) {
            $("#cart-review").html(data);
            $('.pgh-txt').html(round($('#_de_lg').val() / 1000, 1) + ' km');
            //updateCartPreviewTotal();
        }
    });
}
/*Drink Options*/
$("#topping").on("click", ".btn-less-topping", function () {

    var $selectQuantityEl = $(this).closest(".select-quantity");
    var $volumeElement = $selectQuantityEl.find("input.quantity-value");
    var currentValue = Number($volumeElement.val());
    if (currentValue > 1) {
        $volumeElement.val(currentValue - 1);
        $selectQuantityEl.find("button.mid-button > span").text(numberWithCommas(currentValue - 1));
    }

    //validate topping count
    if (!validateToppingCount(this)) {
        $volumeElement.val(currentValue);
        $selectQuantityEl.find("button.mid-button > span").text(numberWithCommas(currentValue));
        return false;
    }
    updateTotalDrinkPrice();
    return true;
});
$("#topping").on("click", ".btn-more-topping", function () {

    var $selectQuantityEl = $(this).closest(".select-quantity");
    var $volumeElement = $selectQuantityEl.find("input.quantity-value");
    var currentValue = Number($volumeElement.val());
    $volumeElement.val(currentValue + 1);
    $selectQuantityEl.find("button.mid-button > span").text(numberWithCommas(currentValue + 1));
    //validate topping count
    if (!validateToppingCount(this)) {
        $volumeElement.val(currentValue);
        $selectQuantityEl.find("button.mid-button > span").text(numberWithCommas(currentValue));
        return false;
    }

    updateTotalDrinkPrice();
    return true;
});

$("#topping").on("click", ".btn-less-drink", function () {
    var $selectQuantityEl = $(this).closest(".select-quantity");
    var $volumeElement = $selectQuantityEl.find("input.quantity-value");
    var currentValue = Number($volumeElement.val());
    if (currentValue > 1) {
        $volumeElement.val(currentValue - 1);
        $selectQuantityEl.find("button.mid-button > span").text(numberWithCommas(currentValue - 1));
    }
    updateTotalDrinkPrice();
});
$("#topping").on("click", ".btn-more-drink", function () {
    var $selectQuantityEl = $(this).closest(".select-quantity");
    var $volumeElement = $selectQuantityEl.find("input.quantity-value");
    var currentValue = Number($volumeElement.val());
    $volumeElement.val(currentValue + 1);
    $selectQuantityEl.find("button.mid-button > span").text(numberWithCommas(currentValue + 1));
    updateTotalDrinkPrice();
});

$("#cart-items").on("click", ".btn-less-drink", function () {
    var $selectQuantityEl = $(this).closest(".select-quantity");
    var $volumeElement = $selectQuantityEl.find("input.quantity-value");
    var currentValue = Number($volumeElement.val());
    $volumeElement.val(currentValue - 1);
    $selectQuantityEl.find("button.mid-button > span").text(numberWithCommas(currentValue - 1));
    if (currentValue == 1) {
        var index = $(this).closest(".cart-item").data("index");
        removeCartItem(index);

        if (countActiveItems() == 0) {
            $('#view-cart-btn').prop('disabled', true);
            $('#view-cart-btn').css('opacity', 0.4);
        }
    }
    updateCartItemsPrice();
});
$("#cart-items").on("click", ".btn-more-drink", function () {
    var $selectQuantityEl = $(this).closest(".select-quantity");
    var $volumeElement = $selectQuantityEl.find("input.quantity-value");
    var currentValue = Number($volumeElement.val());
    $volumeElement.val(currentValue + 1);
    $selectQuantityEl.find("button.mid-button > span").text(numberWithCommas(currentValue + 1));
    updateCartItemsPrice();
});

$(".drink-option-btn").on("click", function () {
    var drinkId = $(this).data("drinkid");
    var sizeId = $(this).data("sizeid");
    var cartItemCount = $("#cart-items .cart-item").length;
    $('#topping .modal-content').load("/Order/DrinkOptions?index=" + cartItemCount + "&drinkId=" + drinkId + "&sizeid=" + sizeId, function () {

        $('#topping').modal({ show: true });
    });
});
$("#topping").on("change", "input[name=DrinkSizeId]", function () {
    updateHeader();
    updateTotalDrinkPrice();
});
$("#topping").on("click", ".checkbox-topping", function (e) {
    //e.preventDefault();

    //validate topping count
    if (!validateToppingCount(this)) {
        return false;
    }
    //$(this).prop('checked', !this.checked);

    if (this.checked) {
        $(this).closest(".checkbox").find(".select-quantity").removeClass("invisible");
    }
    else {
        $(this).closest(".checkbox").find(".select-quantity").addClass("invisible");
    }
    updateTotalDrinkPrice();
    return true;
});
$("#topping").on('click', '#submitOrdering', function () {
    //e.preventDefault();
    $("#topping form").submit();
    $("#topping").modal("hide");
    if (!$("#info-ship").is(":visible"))
        $("#info-cart").show();

    if (/(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|ipad|iris|kindle|Android|Silk|lge |maemo|midp|mmp|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows (ce|phone)|xda|xiino/i.test(navigator.userAgent)
        || /1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-/i.test(navigator.userAgent.substr(0, 4))) {
        $('.collapse').collapse('hide');
    }

    $('#view-cart-btn').prop('disabled', false);
    $('#view-cart-btn').css('opacity', 1);
});
$("#topping").on('click', 'input.not-required:radio', function (e) {
    var inp = $(this);
    if (inp.is(".checked")) {
        inp.prop("checked", false).removeClass("checked");
    } else {
        $("input:radio[name='" + inp.prop("name") + "'].checked").removeClass("checked");
        inp.addClass("checked");
    }
});

var validateToppingCount = function (el) {
    var toppingCount = 0;
    var $toppingGroup = $(el).closest(".topping-group");
    var $selectedToppings = $toppingGroup.find("input.checkbox-topping:checked");

    $selectedToppings.each(function (index, element) {

        var toppingQuantity = $(element).closest(".checkbox").find('.select-quantity input.quantity-value').val();
        toppingCount += Number(toppingQuantity);
    });

    var maxTopping = Number($(el).closest(".topping-group").data("max"));
    if (maxTopping > 0 && toppingCount > maxTopping) {
        alert("Bạn không thể chọn quá " + maxTopping + " topping");
        return false;
    }
    return true;
};

var updateHeader = function () {
    //var $drinkSize = $('#topping input[name=DrinkSizeId]:checked');

    //var drinkSizePrice = Number($drinkSize.data("price"));
    //$("#drinkSizePrice").html(drinkSizePrice);

    //var drinkSizeName = $drinkSize.data("name");
    //$("#drinkSizeName").html(drinkSizeName);
};

var updateTotalDrinkPrice = function () {
    //price for drink
    var $drinkSize = $('#topping input[name=DrinkSizeId]:checked');
    var totalDrinkPrice = Number($drinkSize.data("price"));
    //price for topping
    var $selectedToppings = $("#topping input.checkbox-topping:checked");
    $selectedToppings.each(function (index, element) {

        var toppingPrice = Number($(element).data("price"));
        var toppingQuantity = $(element).closest(".checkbox").find('.select-quantity input.quantity-value').val();

        totalDrinkPrice += (toppingPrice * toppingQuantity);
    });
    //total price
    var drinkQuantity = Number($("#select-drink-quantity input.quantity-value").val());
    totalDrinkPrice = totalDrinkPrice *= drinkQuantity;
    $("#drinkPrice").val(numberWithCommas(totalDrinkPrice));
    $(".drinkPriceDisplay").html(numberWithCommas(totalDrinkPrice));
};

var onSuccessGetCartItem = function (cartItem) {
    var index = $(cartItem).data("index");
    var $existCartItem = $("#cart-items .cart-item[data-index=" + index + "]");
    if ($existCartItem.length == 0) {
        //new
        //find same drink & options
        var similarCartItem = findSimilarCartItem(cartItem);
        if (similarCartItem != null) {
            var $volumeElement = $(similarCartItem).find("input.quantity-value");
            var currentValue = Number($volumeElement.val());
            $volumeElement.val(currentValue + 1);
            $(similarCartItem).find("button.mid-button > span").text(numberWithCommas(currentValue + 1));
        }
        else {
            $("#cart-items").append(cartItem);
        }
    }
    else {
        //edit
        var similarCartItem = findSimilarCartItem(cartItem);
        if (similarCartItem != null) {

            var $volumeElement = $(similarCartItem).find("input.quantity-value");
            var currentValue = Number($volumeElement.val());
            $volumeElement.val(currentValue + 1);
            $(similarCartItem).find("button.mid-button > span").text(numberWithCommas(currentValue + 1));
            $existCartItem.find(".isDeleted").val("true");
            $existCartItem.hide();
        }
        else {

            $existCartItem.replaceWith(cartItem);
        }
    }
    loadCartReview();
    updateCartItemsPrice();
}

var onSuccessCreateOrder = function (data) {
    if (data.success)
        $('#orderSuccessModal').modal('show');
    else
        alert("Có lỗi trong quá trình xử lý, vui lòng liên hệ 090.906.3366");
}

$("#promotionSubmitBtn").on("click", function () {
    var promotionCode = $("#promotionCode").val();
    $.get("/Order/CheckPromotionCode?c=" + promotionCode, function (data) {
        promotionData = data;
        if (data.success) {
            updateCartItemsPrice();

            if (data.applyRule === 0) {
                if (data.discountType === 1) {
                    alert("Mã khuyến mãi được áp dụng: " + data.discountValue + "%");
                } else {
                    alert("Mã khuyến mãi được áp dụng: " + data.discountValue + "đ");
                }
            }

            //buy 1 get 1
            else if (data.applyRule === 1) {
                //apply buy 1 get 1
            }
            else if (data.applyRule === 2) {
                //apply free up size + 1 topping
                alert("Khuyến mãi FREE UP SIZE và 1 topping cho tất cả các món");
            }
            else if (data.applyRule === 3) {

            }
            else if (data.applyRule === 4) {
                alert("Khuyến mãi MUA " + data.buyQuantity + " TẶNG " + data.giveQuantity);
            }
            //buy 1 get 1 new drink
            else if (data.applyRule === 5) {

            }
        } else {
            $("#promotionCode").val('');
            $('#cart-promotion').text('');
            alert("Mã khuyến mãi không đúng hoặc đã hết hiệu lực!");
        }
    });
});
$(document).on('click', '#applyCartPreviewPromotion', function () {
    var promotionCode = $("#cartPreviewPromotionCode").val();
    $.get("/Order/CheckPromotionCode?c=" + promotionCode, function (data) {
        promotionData = data;
        if (data.success) {
            if (data.applyRule === 0) {
                if (data.discountType === 1) {
                    alert("Mã khuyến mãi được áp dụng: " + data.discountValue + "%");
                } else {
                    alert("Mã khuyến mãi được áp dụng: " + data.discountValue + "đ");
                }
            }

            //buy 1 get 1
            else if (data.applyRule === 1) {
                //apply buy 1 get 1
            }
            else if (data.applyRule === 2) {
                //apply free up size + 1 topping
                alert("Khuyến mãi FREE UP SIZE và 1 topping cho tất cả các món");
            }
            else if (data.applyRule === 3) {
                //apply free drink

            }
            else if (data.applyRule === 4) {
                alert("Khuyến mãi MUA " + data.buyQuantity + " TẶNG " + data.giveQuantity);
            }
            //buy 1 get 1 new drink
            else if (data.applyRule === 5) {

            }

            $("#promotionCode").val(promotionCode);
            updateCartItemsPrice();
            loadCartReview();
        } else {
            $("#cartPreviewPromotionCode").val('');
            alert("Mã khuyến mãi không đúng hoặc đã hết hiệu lực!");
        }
    });
});

var findSimilarCartItem = function (newCartItem) {
    var newCartItemInfo = getInfoFromCartItem(newCartItem);
    var cartItem = null;
    $("#cart-items .cart-item").each(function () {

        if ($(this).find(".isDeleted").val() !== "true" && $(this).data("index") != $(newCartItem).data("index")) {
            var cartItemInfo = getInfoFromCartItem(this);

            if (compareCartItem(cartItemInfo, newCartItemInfo)) {
                return cartItem = this;
            }
        }
    });
    return cartItem;
}

var getInfoFromCartItem = function (cartItem) {
    var drinkId = $(cartItem).find("[id$=__DrinkId_]").val();
    var sugarOptionId = $(cartItem).find("[id$=__SugarOptionId_]").val();
    var iceOptionId = $(cartItem).find("[id$=__IceOptionId_]").val();
    var drinkSizeId = $(cartItem).find("[id$=__DrinkSizeId_]").val();
    var toppings = [];
    $(cartItem).find(".cart-item-topping").each(function () {
        var toppingId = $(this).find("[id$=__ToppingId_]").val();
        var toppingQuantity = $(this).find("[id$=__Quantity_]").val();
        toppings.push({ toppingId: toppingId, toppingQuantity: toppingQuantity });
    });
    return { drinkId: drinkId, sugarOptionId: sugarOptionId, iceOptionId: iceOptionId, drinkSizeId: drinkSizeId, toppings: toppings };
}

var compareCartItem = function (item1, item2) {
    return item1.drinkId == item2.drinkId
        && item1.sugarOptionId == item2.sugarOptionId
        && item1.iceOptionId == item2.iceOptionId
        && item1.drinkSizeId == item2.drinkSizeId
        && item1.toppings.length == item2.toppings.length
        && item1.toppings.every(function (topping, index) {
            return topping.toppingId == item2.toppings[index].toppingId && topping.toppingQuantity == item2.toppings[index].toppingQuantity;
        });
}

var updateCartItemsPrice = function () {
    updateCartItems();
    updateDiscount();
    updateCartTotal();
    updateCartPreviewTotal();
}
var updateDiscount = function ()
{
    var discount = 0;
    if (promotionData != null && promotionData.success) {
        if (promotionData.applyRule === 0) {
        }
        //buy 1 get 1
        else if (promotionData.applyRule === 1) {

        }
        //free topping + up size
        else if (promotionData.applyRule === 2) {
            discount = applyFreeUpSize1Topping();
        }
        //free drink
        else if (promotionData.applyRule === 3) {
            applyFreeDrink();
        }
        //buy 1 get 1 new drink
        else if (promotionData.applyRule === 5) {

        }
    }
    $("#cart-promo-drinks").data("value", discount);
    $("#cart-promo-drinks").text(numberWithCommas(-discount));
}

var updateCartItems = function () {
    
    $("#cart-items .cart-item").each(function () {
        var quantity = $(this).find(".quantity-value").val();
        var price = $(this).data("price");
        var multiPrice = quantity * price;
        $(this).find(".drink-multi-price").html(numberWithCommas(multiPrice));
        var amount = multiPrice;
        $(this).find(".cart-item-topping").each(function (index, topping) {
            var toppingPrice = $(topping).data("price");
            var toppingQuantity = $(topping).data("quantity");
            var toppingMultiPrice = toppingPrice * toppingQuantity * quantity;
            $(topping).find(".topping-multi-quantity").html(toppingQuantity * quantity);
            $(topping).find(".topping-multi-price").html(numberWithCommas(toppingMultiPrice));

            amount += toppingMultiPrice;
        });
        $(this).data("amount", amount);
    });
}

var updateCartTotal = function () {
    var discount = $("#cart-promo-drinks").data("value");
    //update total money
    var total = updateChargeDrinks();
    if (promotionData != null && promotionData.success) {
        if (promotionData.applyRule === 0) {
            if (promotionData.discountType === 1) {
                discount = total * promotionData.discountValue / 100;
            }
            else {
                discount = promotionData.discountValue;
            }
            $('#cart-promo-drinks').data("value", discount);
            $('#cart-promo-drinks').text(numberWithCommas(-discount));
            $('#cartPreviewDiscount').text(numberWithCommas(-discount));
            $('#_dt_am').val(discount);
        }
    }
    total -= discount;
    if (total < 0) total = 0;

    if (total < 150000) {
        if (promotionData != null && promotionData.success && promotionData.applyRule == 4) {
            tempShip = updateFeeShipBuyMGetNLowPrice();
        }
        else {
            tempShip = updateFeeShip1Get1();
        }
    }
    else {
        tempShip = 0;
    }
    $('#cart-fee-ship').text(numberWithCommas(tempShip));
    $('#_se_am').val(tempShip);
    total += tempShip;
    $("#cart-amount-temp").text(numberWithCommas(total));
};

let applyFreeUpSize1Topping = function () {
    let discountValue = 0;
    let discountItems = [];
    let totalAmount = 0;
    $("#cart-items .cart-item").each(function () {
        if ($(this).find(".isDeleted").val() !== "true") {
            let minSizePrice = $(this).data("min-price");
            let maxSizePrice = $(this).data("max-price");
            let minSizeName = $(this).data("min-size-name");
            let maxSizeName = $(this).data("max-size-name");
            let maxSizeId = $(this).data("max-size-id");
            let quantity = $(this).find(".quantity-value").val();

            let offsetPrice = maxSizePrice - minSizePrice
            let toppingSorted = $(this).find(".cart-item-topping").sort(function (a, b) {
                return $(a).data('price') - $(b).data('price');
            });

            let minToppingPrice = toppingSorted.length == 0 ? 0 : $(toppingSorted[0]).data('price');
            var toppingsPrice = 0;
            for (let i = 0; i < toppingSorted.length; i++) {
                toppingsPrice += $(toppingSorted[i]).data('price') * $(toppingSorted[i]).data('quantity');
            }
            let itemDiscountValue = (offsetPrice + minToppingPrice) * quantity;
            discountValue += itemDiscountValue;

            //update UI: Món đã chọn
            $(this).data("price", maxSizePrice);
            $(this).data("amount", (maxSizePrice + toppingsPrice) * quantity);
            $(this).find('.cart-item-size').text("+ Size " + maxSizeName);
            $(this).find('.drink-multi-price').text(numberWithCommas(maxSizePrice * quantity));
            $(this).find('.drinkSizeId').val(maxSizeId);

            discountItems.push({
                DrinkName: $(this).find(".cart-item-name").text(),
                Quantity: quantity,
                MaxPrice: maxSizePrice,
                MaxSize: maxSizeName,
                MinPrice: minSizePrice,
                MinSize: minSizeName,
                OffsetPrice: offsetPrice,
                Amount: itemDiscountValue,
                FreeTopping: toppingSorted.length > 0 ? toppingSorted[0] : null
            });
        }
    });

    //Update UI: Khuyến mại Món đã chọn
    //$('#cart-promo-drinks').text(numberWithCommas(-discountValue));

    //Update UI: tooltip Khuyến mại
    var htmlFreeUS = '<table class="table" id="fus-details"><thead><tr><th>Tên món</th><th class="text-right">Giảm giá</th></tr></thead><tbody>';

    discountItems.forEach(function (item, i) {
        var lowestId = $(item.FreeTopping).data('toppingid');
        htmlFreeUS += '<tr class="cart-item">'
            + '<td><strong>' + (i + 1) + '. ' + item.DrinkName + ' (' + item.Quantity + ')</strong></td>'
            + '<td class="text-right">&nbsp;</td></tr>'
            + '<tr class="cart-item"><td>'
            + '<div class="row pl-3">+ Size ' + item.MinSize + ' (' + numberWithCommas(item.MinPrice) + ') => Size ' + item.MaxSize + ' (' + numberWithCommas(item.MaxPrice) + ')</div>';
        if (item.FreeTopping != null) {
            htmlFreeUS += '<div class="row pl-3">' + $.trim($(item.FreeTopping).find('.topping-name').text()) + ': ' + $(item.FreeTopping).data('price') + ' => 0</div>'
        }
        htmlFreeUS += '</td><td class="text-right"><div><span>' + item.OffsetPrice + ' x' + item.Quantity + '</span></div>';
        if (item.FreeTopping != null) {
            htmlFreeUS += '<div><span>' + $(item.FreeTopping).data('price') + ' x' + item.Quantity + '</span></div>';
        }
        htmlFreeUS += '</td></tr>';
    });

    htmlFreeUS += '</tbody><tfoot><tr><th>Tổng cộng</th><th class="text-right"><strong>' + discountItems.reduce(function (a, b) { return a + b['Amount'] }, 0) + '</strong></th></tr></tfoot></table>';

    $('.icon-promo-summ-help').popover('dispose').popover({
        html: true,
        content: htmlFreeUS,
    });

    return discountValue;
}

let applyFreeDrink = function () {

    var endpoint = '/Order/GetPromotionFreeDrink';
    $.ajax({
        type: "POST",
        url: endpoint,
        data: $('#createOrderForm').serializeArray(),
        success: function (data) {
            console.log(data);
            var totalFreeDrink = data.reduce(function (total, num) { return total + num.quantity; }, 0);
            console.log(totalFreeDrink);
            if (data && data.length > 0) {
                let promotionStr = "Bạn được tặng " +
                    +  totalFreeDrink
                //data.map(e => e.quantity  + " ly " + e.drinkName).join(", ")
                    + " ly soda miễn phí";
                $("#promotion-notify").html("<div class='alert alert-success' role='alert'>" + promotionStr + "</div>");
            }
        }
    });
    
}

var updateCartPreviewTotal = function () {
    var total = updateChargeDrinks();
    var discount = 0;
    if (promotionData != null && promotionData.success) {
        if (promotionData.applyRule === 0) {
            if (promotionData.discountType === 1) {
                discount = total * promotionData.discountValue / 100;
            }
            else {
                discount = promotionData.discountValue;
                if (total < 0) total = 0;
            }
        }
    }
    total -= discount;
    if (total < 0) total = 0;
    tempShip = total < 150000 ? updateFeeShip1Get1() : 0;
    $('#cartPreviewFeeShip').text(numberWithCommas(tempShip));
    $('#_se_am').val(tempShip);
    total += tempShip;
    $("#cartPreviewTotalAmount").text(numberWithCommas(total));
};

var updateChargeDrinks = function () {
    var arr = [];
    var cntItem = 0;
    var cntNewItem = 0;
    $("#cart-items .cart-item").each(function () {
        if ($(this).find(".isDeleted").val() !== "true") {
            $(this).find('.isPromoDiscount').val("false");
            let $selectQuantityEl = $(this).find(".select-quantity");
            let $volumeElement = $selectQuantityEl.find("input.quantity-value");
            let valItem = Number($volumeElement.val());

            let index = $(this).data("index");
            let amount = Number($(this).data("amount"));
            let name = $(this).find(".cart-item-name").text();
            let isNew = $(this).data("isnew").toLowerCase();

            for (var i = 0; i < valItem; i++) {
                arr.push({ index: index, name: name, amount: amount / valItem });
            }
            cntItem += valItem;
            if (isNew === 'true') {
                cntNewItem += valItem;
            }
        }
    });

    var total = arr.reduce(function (a, b) { return a + b['amount'] }, 0);
    $("#cart-quantity-drinks").text(cntItem);
    $("#cartPreviewQuantity").text(cntItem);

    $("#cart-amount-drinks").text(numberWithCommas(total));
    $("#cartPreviewAmount").text(numberWithCommas(total));
    
    if (cntItem > 0) {
        arr = arr.sort(function (a, b) { return b.amount - a.amount });
        //buy 1 get 1
        if ($('#_ao_dt_pg').val() == '1' || (promotionData != null && promotionData.success && promotionData.applyRule === 1)) {
            var chargeItem = cntItem < 2 ? cntItem : cntItem % 2 == 0 ? cntItem / 2 : (cntItem + 1) / 2;
            var arrCharge = arr.slice(0, chargeItem);
            var result = arrCharge.reduce(function (a, b) { return a + b['amount'] }, 0);

            updateDiscountDrinks(arr.slice(chargeItem, arr.length));

            return result;
        }
        //buy 1 get 1 - new drink
        else if ($('#_ao_dt_pg').val() == '1' || (promotionData != null && promotionData.success && promotionData.applyRule === 5)) {
            let cntFreeItem = Math.min(cntNewItem, Math.floor(cntItem / 2));
            let chargeItem = cntItem - cntFreeItem;
            let arrCharge = arr.slice(0, chargeItem);
            let result = arrCharge.reduce(function (a, b) { return a + b['amount'] }, 0);

            updateDiscountDrinks(arr.slice(chargeItem, arr.length));

            return result;
        }
        else if ($('#_ao_dt_pg').val() == '1' || (promotionData != null && promotionData.success && promotionData.applyRule === 4)) {
            var conditionQuantity = promotionData.buyQuantity + promotionData.giveQuantity;
            var modQuantity = cntItem % conditionQuantity;
            var divQuantity = (cntItem - modQuantity) / conditionQuantity;

            let chargeItem = cntItem <= promotionData.buyQuantity ? cntItem : modQuantity == 0 ? divQuantity * promotionData.buyQuantity : modQuantity <= promotionData.buyQuantity ? promotionData.buyQuantity * divQuantity + modQuantity : promotionData.buyQuantity * divQuantity + promotionData.buyQuantity;
            let arrCharge = arr.slice(0, chargeItem);
            let result = arrCharge.reduce(function (a, b) { return a + b['amount'] }, 0);
            
            updateDiscountDrinks(arr.slice(chargeItem, arr.length));

            return result;
        }
        else {
            return arr.reduce(function (a, b) { return a + b['amount'] }, 0);
        }
    } else {
        $('#cart-promo-drinks').data("value", 0);
        $('#cart-promo-drinks').text(0);
        $('#cartPreviewDiscount').text(0);
        $('.icon-promo-summ-help').popover('dispose');
        return 0;
    }
};

//buy 1 get 1
var updateDiscountDrinks = function (arr) {
    if (arr.length > 0) {
        var amount = arr.reduce(function (a, b) { return a + b['amount'] }, 0);
        $('#cart-promo-drinks').data("value", amount);
        $('#cart-promo-drinks').text(numberWithCommas(-amount));
        $('#cartPreviewDiscount').text(numberWithCommas(-amount));
        $('#_dt_am').val(amount);

        var html = '<p>Bạn được khấu trừ ' + arr.length + ' ly từ hóa đơn</p>';
        html += '<table class="table table-borderless table-sm"><thead><tr><th scope="col">STT</th><th scope="col">Tên món</th><th scope="col">Giá tiền</th></tr></thead><tbody>';
        for (var i = 0; i < arr.length; i++) {
            var item = arr[i];
            html += '<tr><td class="text-center">' + (i + 1) + '</td><td>' + item.name + '</td><td class="text-right">' + numberWithCommas(item.amount) + '</td></tr>';
            $('.cart-item[data-index="' + item.index + '"]').find('.isPromoDiscount').val("true");
        }
        html += '</tbody><tfoot><tr><td colspan="2" class="text-center">Tổng tiền được giảm</td><td class="text-right">' + numberWithCommas(amount) + '</td></tr></tfoot></table>';

        $('.icon-promo-summ-help').popover('dispose').popover({
            html: true,
            content: html,
        });
    } else {
        $('#cart-promo-drinks').data("value", 0);
        $('#cart-promo-drinks').text(0);
        $('#cartPreviewDiscount').text(0);
        $('.icon-promo-summ-help').popover('dispose');
    }
};

var updateFeeShip1Get1 = function () {
    let cntItem = 0;
    let cntNewItem = 0;
    $("#cart-items .cart-item").each(function () {
        if ($(this).find(".isDeleted").val() !== "true") {
            let $selectQuantityEl = $(this).find(".select-quantity");
            let $volumeElement = $selectQuantityEl.find("input.quantity-value");
            let valItem = Number($volumeElement.val());
            let isNew = $(this).data("isnew").toLowerCase();

            cntItem += valItem;
            if (isNew === 'true') {
                cntNewItem += valItem;
            }
        }
    });
    //buy 1 get 1
    if ($('#_ao_dt_pg').val() == '1' || (promotionData != null && promotionData.success && promotionData.applyRule === 1)) {
        var chargeItem = cntItem <= 2 ? cntItem : cntItem % 2 == 0 ? cntItem / 2 : (cntItem + 1) / 2;
        let reducedShipChargeItem = chargeItem >= 2 ? chargeItem - 1 : 0;

        if (cntItem > 2) {
            var temp = feeShip - (reducedShipChargeItem * 5000);
            return temp > 0 ? temp : 0;
        }
    }
    //buy 1 get 1 new drink
    else if ($('#_ao_dt_pg').val() == '1' || (promotionData != null && promotionData.success && promotionData.applyRule === 5))
    {
        let cntFreeItem = Math.min(cntNewItem, Math.floor(cntItem / 2));
        let chargeItem = cntItem - cntFreeItem;
        let reducedShipChargeItem = chargeItem >= 2 ? chargeItem - 1 : 0;
        
        let temp = feeShip - (reducedShipChargeItem * 5000);
        return temp > 0 ? temp : 0;
    }
    else {
        if (cntItem > 1) {
            var temp = feeShip - ((cntItem - 1) * 5000);
            return temp > 0 ? temp : 0;
        }
    }
    return feeShip;
};

let updateFeeShipBuyMGetNLowPrice = function () {
    var cntItem = 0;
    $("#cart-items .cart-item").each(function () {
        if ($(this).find(".isDeleted").val() !== "true") {
            var $selectQuantityEl = $(this).find(".select-quantity");
            var $volumeElement = $selectQuantityEl.find("input.quantity-value");
            var valItem = Number($volumeElement.val());
            cntItem += valItem;
        }
    });

    var conditionQuantity = promotionData.buyQuantity + promotionData.giveQuantity;
    var modQuantity = cntItem % conditionQuantity;
    var divQuantity = (cntItem - modQuantity) / conditionQuantity;

    var chargeItem = cntItem <= promotionData.buyQuantity ? cntItem : modQuantity == 0 ? divQuantity * promotionData.buyQuantity : modQuantity <= promotionData.buyQuantity ? (promotionData.buyQuantity * divQuantity) + modQuantity : promotionData.buyQuantity * divQuantity + promotionData.buyQuantity;
    chargeItem = chargeItem >= 2 ? chargeItem - 1 : 0;

    if (cntItem > 2) {
        var temp = feeShip - (chargeItem * 5000);
        return temp > 0 ? temp : 0;
    }
    return feeShip;
};

var updateFeeShipFromMap = function (distance) {
    if (distance > 1000) {
        var multiply = round(distance / 1000, 1);
        feeShip = multiply * 5000;
        $('.pgh-txt').html(multiply + ' km');
    } else {
        feeShip = 0;
        $('.pgh-txt').html('');
    }
    $('#_de_lg').val(distance);
    updateCartTotal();
    updateCartPreviewTotal();
};

var checkValidateCustomerInfo = function () {
    var result = true;
    if (location.hostname !== "localhost" && ($("#_de_lg").val() === "" || $("#_de_lg").val() === "0")) {
        $('.title-missing-info').html('Địa chỉ không hợp lệ, bạn vui lòng điền lại và chọn địa chỉ từ danh sách gợi ý');
        $('#missingInfoCustomerModal').modal('show');
        return false;
    }
    else if ($("#name-info").val() === "" && $("#phone-info").val() === "") {
        $('.title-missing-info').html('Vui lòng nhập đầy đủ tên, số điện thoại và địa chỉ');
        $('#missingInfoCustomerModal').modal('show');
        return false;
    } else {
        if ($("#name-info").val() === "") {
            $('.title-missing-info').html('Bạn chưa nhập tên');
            $('#missingInfoCustomerModal').modal('show');
            return false;
        }

        var phoneCheck = /\(?([0-9]{3})\)?([ .-]?)([0-9]{3})\2([0-9]{4})/;
        if (!phoneCheck.test($("#phone-info").val())) {
            $('.title-missing-info').html('Bạn cần nhập đúng số điện thoại');
            $('#missingInfoCustomerModal').modal('show');
            return false;
        }

        //var emailCheck = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        //if (!emailCheck.test($("#email-info").val())) {
        //    $('.title-missing-info').html('Bạn cần nhập đúng email');
        //    $('#missingInfoCustomerModal').modal('show');
        //    return false;
        //}

        if ($("#numberAddress").val() === "") {
            $('.title-missing-info').html('Bạn chưa chọn địa chỉ giao hàng');
            $('#missingInfoCustomerModal').modal('show');
            return false;
        }
    }
    return result;
};

var splitName = function (fullName) {
    fullName = fullName.trim();
    var result = {};

    if (fullName) {
        var nameArr = fullName.split(' ');
        result.first_name = nameArr.pop();
        result.last_name = nameArr.shift();
        result.mid_name = nameArr.join(' ');
    }
    return result;
};

var countActiveItems = function () {
    return $("#cart-items .cart-item").filter(function (index) {
        return $(this).find(".isDeleted").val() !== "true";
    }).length;
}