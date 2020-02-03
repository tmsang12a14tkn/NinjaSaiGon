var promotionData = null;

$(document).ready(function () {
    SetActiveMenu('orderMenu');
});

$(".datepicker").datetimepicker({
    format: "dd/mm/yyyy",
    todayHighlight: !0,
    autoclose: !0,
    startView: 2,
    minView: 2,
    forceParse: 0,
    pickerPosition: "bottom-left"
});

$(".time-input").inputmask("hh:mm");

$(".price-input").inputmask("currency", { digits: 0, prefix: "", autoUnmask: true, removeMaskOnSubmit: true });

var tableTour = $("#srchcustomer-table").DataTable({
    searching: false,
    paging: false,
    info: false,
    ordering: false,
    columnDefs: [{
        orderable: false,
        targets: 0
    }]
});

var tableTour = $("#srchordersource-table").DataTable({
    searching: false,
    paging: false,
    info: false,
    ordering: false,
    columnDefs: [{
        orderable: false,
        targets: 0
    }]
});

$("[data-switch=true]").bootstrapSwitch();
$('#IsAutoDiscount').on('switchChange.bootstrapSwitch', function (event, state) {
    $(this).prop("checked", state);
    $('#DiscountAmountManual').prop('disabled', state);
    $('#DiscountAmountManual').val(0);
    if (!state) {
        $('#cart-promo-drinks').addClass('strikethough');
    } else {
        $('#cart-promo-drinks').removeClass('strikethough');
    }
    updateCartTotal();
});
$('#DiscountAmountManual').on('change', function () {
    $('#DiscountAmount').val($(this).val());
    updateCartTotal();
});

$('#IsAutoSurcharge').on('switchChange.bootstrapSwitch', function (event, state) {
    $(this).prop("checked", state);
    $('#SurchargeAmountManual').prop('disabled', state);
    $('#SurchargeAmountManual, #SurchargeAmount').val(0);
    if (!state) {
        $('#cart-surcharge').addClass('strikethough');
    } else {
        $('#cart-surcharge').removeClass('strikethough');
    }
    updateCartTotal();
});
$('#SurchargeAmountManual').on('change', function () {
    $('#SurchargeAmount').val($(this).val());
    updateCartTotal();
});

$('#IsAutoShipFee').on('switchChange.bootstrapSwitch', function (event, state) {
    $(this).prop("checked", state);
    $('#ShipFeeManual').prop('disabled', state);
    $('#ShipFeeManual, #ShipFee').val(0);
    if (!state) {
        $('#cart-fee-ship').addClass('strikethough');
    } else {
        $('#cart-fee-ship').removeClass('strikethough');
        updateFeeShipFromMap($('#Distance').val());
    }
    updateCartTotal();
});
$('#ShipFeeManual').on('change', function () {
    $('#ShipFee').val($(this).val());
    updateCartTotal();
});

$(window).on('keydown', function (e) {
    if (e.keyCode == 13) {
        e.preventDefault();
        return false;
    }
});

$(document).on('keyup', 'input', function (event) {
    if (event.keyCode === 13) {
        $(this).blur();
        event.preventDefault();
    }
});

//on edit page
if ($("#promotionCode").val() != '') {
    var promotionCode = $("#promotionCode").val();
    var orderId = $("#OrderId").val();
    $.get("/Orders/CheckPromotionCode?c=" + promotionCode + "&orderid=" + orderId, function (data) {
        promotionData = data;
    });
}
if ($('#Distance').val() != '') {
    var distance = $('#Distance').val();
    if (distance > 1000) {
        var multiply = round(distance / 1000, 1);
        feeShip = multiply * 5000;
    } else {
        feeShip = 0;
    }
}

$.get('/Orders/DrinkCategories', function (categories) {
    for (var i = 0; i < categories.length; i++) {
        let category = categories[i];
        $("#category-list").append('<div class="cat-opt" data-id=' + i + ' data-catId=' + category.id + '><p>' + categories[i].name + '</p></div>');
    }
    $("#category-list div.cat-opt").on("click", function () {
        resetDrinkSelect();

        if ($('#qc-order-details').find('.cart-item.curCartSelected').length != 0) {
            $("#drink-quantity").val($('#qc-order-details').find('.cart-item.curCartSelected').data('quantity'));
        }

        $(this).addClass("highlighted");
        var index = $(this).data("id");
        var drinks = categories[index].drinks;

        for (var j = 0; j < drinks.length; j++) {
            var drink = drinks[j];
            $("#drink-list").append('<div class="drink-opt" data-cid=' + index + ' + data-id=' + j + ' data-drinkId=' + drink.id + '>' +
                '<p>' + drink.name + '</p>' +
                '<input class="invisible" type="radio" name="DrinkId" value="' + drink.id + '">' +
                '</div>');
        }
        $("#drink-list div.drink-opt").on("click", function () {
            $("#drink-list div.drink-opt").removeClass("highlighted");
            $(this).addClass("highlighted");
            $(this).find("input:radio").prop("checked", true);
            var cIndex = $(this).data("cid");
            var index = $(this).data("id");
            var drink = categories[cIndex].drinks[index];
            $("#size-list").empty();
            for (var k = 0; k < drink.sizeOptions.length; k++) {
                var size = drink.sizeOptions[k];
                $("#size-list").append(
                    '<div class="size-opt' + (size.name == drink.primarySize ? ' highlighted' : '') + '" data-sizeId=' + size.id + ' data-name="' + size.name + '">' +
                    '<p>' + size.name + '</p>' +
                    '<input class="invisible" type="radio" name="DrinkSizeId" value="' + size.id + '" ' + (size.name == drink.primarySize ? 'checked' : '') + '>' +
                    '</div>');
            }
            $("#size-list div.size-opt").on("click", function () {
                $("#size-list div.size-opt").removeClass("highlighted");
                $(this).addClass("highlighted");
                $(this).find("input:radio").prop("checked", true);
            });

            $("#ice-list").empty();
            for (var k = 0; k < drink.iceOptions.length; k++) {
                var ice = drink.iceOptions[k];
                $("#ice-list").append('<div class="ice-opt' + (ice.name == drink.primaryIce ? ' highlighted' : '') + '" data-iceId=' + ice.id + ' data-name="' + ice.name + '">' +
                    '<p>' + ice.name + '</p>' +
                    '<input class="invisible" type="radio" name="IceOptionId" value="' + ice.id + '" ' + (ice.name == drink.primaryIce ? ' highlighted' : '') + '>' +
                    '</div>');
            }
            $("#ice-list div.ice-opt").on("click", function () {
                $("#ice-list div.ice-opt").removeClass("highlighted");
                $(this).addClass("highlighted");
                $(this).find("input:radio").prop("checked", true);
            });

            $("#sugar-list").empty();
            for (var k = 0; k < drink.sugarOptions.length; k++) {
                var sugar = drink.sugarOptions[k];
                $("#sugar-list").append('<div class="sugar-opt' + (sugar.name == drink.primarySugar ? ' highlighted' : '') + '" data-sugarId=' + sugar.id + ' data-name="' + sugar.name + '">' +
                    '<p>' + sugar.name + '</p>' +
                    '<input class="invisible" type="radio" name="SugarOptionId" value="' + sugar.id + '" ' + (ice.name == drink.primaryIce ? ' highlighted' : '') + '>' +
                    '</div>');
            }
            $("#sugar-list div.sugar-opt").on("click", function () {
                $("#sugar-list div.sugar-opt").removeClass("highlighted");
                $(this).addClass("highlighted");
                $(this).find("input:radio").prop("checked", true);
            });
        });

    });

});

$.get("/Topping/QuickCreateList", function (toppings) {
    $(".topping-list-l, .topping-list-r").empty();
    for (var i = 0; i < toppings.length; i++) {
        let topping = toppings[i];
        if (i % 2 == 0) {
            $(".topping-list-l").append(getToppingTempate(topping, i));
        } else {
            $(".topping-list-r").append(getToppingTempate(topping, i));
        }
    }
    $("#topping-list div.topping-opt").on("click", function () {
        //$(this).parent().siblings().find('div.topping-opt').removeClass("highlighted");
        $(this).addClass("highlighted");
        if ($(this).siblings('.topping-opt-btns').length == 0) {
            let index = $(this).data('id');
            let $toppingOptBtns = $(
                '<div class="topping-opt-btns">' +
                '<button type="button" class="btn btn-info btn-sm btn-minus"><i class="fa fa-minus"></i></button>' +
                '<input class="topping-quantity" name="OrderDetailToppings[' + index + '].Quantity" type="number" min="0" value="1" />' +
                '<button type="button" class="btn btn-info btn-sm btn-plus"><i class="fa fa-plus"></i></button>' +
                '</div>');
            $(this).parent().append($toppingOptBtns);

            $toppingOptBtns.on('click', '.btn-plus', function () {
                let $toppingQuantity = $(this).siblings('.topping-quantity');
                $toppingQuantity.val(Number($toppingQuantity.val()) + 1);
            });

            $toppingOptBtns.on('click', '.btn-minus', function () {
                let $toppingQuantity = $(this).siblings('.topping-quantity');
                let currentQuantity = Number($toppingQuantity.val());
                if (currentQuantity > 1) {
                    $toppingQuantity.val(currentQuantity - 1);
                }
                else if (currentQuantity == 1) {
                    $toppingOptBtns.siblings('div.topping-opt').removeClass("highlighted");
                    $toppingOptBtns.remove();
                }
            })
        }
    });
})

$("#drink-quantity-wrapper").on("click", ".btn-less-drink", function () {
    var $selectQuantityEl = $(this).closest(".select-quantity");
    var $volumeElement = $selectQuantityEl.find("input.quantity-value");
    var currentValue = Number($volumeElement.val());
    if (currentValue > 1) {
        $volumeElement.val(currentValue - 1);
    }
});
$("#drink-quantity-wrapper").on("click", ".btn-more-drink", function () {
    var $selectQuantityEl = $(this).closest(".select-quantity");
    var $volumeElement = $selectQuantityEl.find("input.quantity-value");
    var currentValue = Number($volumeElement.val());
    $volumeElement.val(currentValue + 1);

});

$("#btn-add-drink").on("click", function () {
    $("#drink-quantity-wrapper").removeClass("invisible");
    $("#drink-controls").removeClass("invisible");
    $("#drink-selector-wrapper").removeClass("invisible");

});

$("#btn-cancel-drink").on("click", function () {
    $("#drink-quantity-wrapper").addClass("invisible");
    $("#drink-controls").addClass("invisible");
    $("#drink-selector-wrapper").addClass("invisible");

    $("#qc-order-details .cart-item").removeClass("cart-item-highlight");
    $("#qc-order-details .cart-item").removeClass("curCartSelected");

    resetDrinkSelect();
});
$("#btn-remove-drink").on("click", function () {
    var $cartItem = $("#qc-order-details .cart-item.curCartSelected");
    if ($cartItem.data("isfreedrink") === "True") {
        $.alert({
            title: 'Chú ý',
            content: 'Bạn không thể xóa món tặng ra khỏi đơn hàng?',
            icon: 'fa fa-warning',
            type: 'orange',
            buttons: {
                ok: {
                    text: "Ok",
                    btnClass: 'btn-primary',
                    keys: ['enter'],
                }
            },
            animation: 'none',
            closeAnimation: 'none'
        });
    }
    else {
        $.confirm({
            title: 'Chú ý',
            content: 'Bạn có muốn xóa món này khỏi đơn hàng?',
            type: 'green',
            buttons: {
                ok: {
                    text: "Xóa",
                    btnClass: 'btn-primary',
                    keys: ['enter'],
                    action: function () {
                        //var $cartItem = $("#qc-order-details .cart-item.curCartSelected");
                        $cartItem.find(".isDeleted").val("true");
                        $cartItem.hide();
                        $cartItem.removeClass('curCartSelected');

                        resetDrinkSelect();
                        updateCartTotal();
                    }
                },
                cancel: {
                    text: "Hủy",
                    //action: cancel
                }
            },
            animation: 'none',
            closeAnimation: 'none'
        });
    }
});
$("#btn-save-drink").on("click", function () {
    var msg = [];
    if ($('.drink-opt.highlighted').length <= 0) msg.push('món');
    if ($('.size-opt.highlighted').length <= 0) msg.push('size');
    if ($('.ice-opt.highlighted').length <= 0) msg.push('đá');
    if ($('.sugar-opt.highlighted').length <= 0) msg.push('đường');

    if (msg.length > 0) {
        $.confirm({
            title: 'Lưu thất bại',
            content: 'Bạn chưa chọn ' + msg.join(', '),
            type: 'orange',
            buttons: {
                ok: {
                    text: "OK",
                    btnClass: 'btn-primary',
                    keys: ['enter'],
                }
            },
            animation: 'none',
            closeAnimation: 'none'
        });
    } else {
        var toppings = [];
        $('.topping-opt.highlighted').each(function (index) {
            var $toppingQuantity = $(this).closest('.topping-opt-group').find('.topping-quantity');
            var toppingItem = {
                ToppingId: $(this).data('toppingid'),
                Quantity: $toppingQuantity.val(),
                IsChecked: true,
                Price: $(this).data('price')
            };
            toppings.push(toppingItem);
        });

        var data = {
            Index: $("#qc-order-details .cart-item.curCartSelected").length > 0 ? $("#qc-order-details .cart-item.curCartSelected").data('index') : $('#qc-order-details .cart-item[data-mainrow="1"]').length,
            OrderDetailId: $("#qc-order-details .cart-item.curCartSelected").length > 0 ? $("#qc-order-details .cart-item.curCartSelected").data('orderdetailid') : 0,
            CategoryId: $('.cat-opt.highlighted').data('catid'),
            DrinkId: $('.drink-opt.highlighted').data('drinkid'),
            SugarOptionId: $('.sugar-opt.highlighted').data('sugarid'),
            IceOptionId: $('.ice-opt.highlighted').data('iceid'),
            DrinkSizeId: $('.size-opt.highlighted').data('sizeid'),
            Quantity: $('#drink-quantity').val(),
            Toppings: toppings,
            DiscountType: $("#qc-order-details .cart-item.curCartSelected").length > 0 ? $("#qc-order-details .cart-item.curCartSelected").data('manual-discount-type') : 0,
            DiscountPercentValue: $("#qc-order-details .cart-item.curCartSelected").length > 0 ? $("#qc-order-details .cart-item.curCartSelected").data('manual-discount-percent') : 0,
            DiscountMoneyValue: $("#qc-order-details .cart-item.curCartSelected").length > 0 ? $("#qc-order-details .cart-item.curCartSelected").data('manual-discount-money') : 0,
            FreeDrinkReasonId: $("#qc-order-details .cart-item.curCartSelected").length > 0 ? $("#qc-order-details .cart-item.curCartSelected").data('manual-discount-reason') : 'null',
            DiscountReason: $("#qc-order-details .cart-item.curCartSelected").length > 0 ? $("#qc-order-details .cart-item.curCartSelected").data('manual-discount-note') : '',
            IsFreeDrink: $("#qc-order-details .cart-item.curCartSelected").data('isfreedrink'),
            Note: $("#qc-order-details .cart-item.curCartSelected").next().next().find('.cart-item-note').val()
        };

        $.post('/Orders/GetCartItem', data, function (cartItem) {
            onSuccessGetCartItem(cartItem);
        });
    }
});

$(document).on('click', '#btn-discount', function () {
    if ($('#qc-order-details').find('.cart-item.curCartSelected').length == 0) {
        $.confirm({
            title: 'Giảm giá',
            content: 'Bạn chưa chọn món cần giảm giá',
            type: 'orange',
            buttons: {
                ok: {
                    text: "OK",
                    btnClass: 'btn-primary',
                    keys: ['enter'],
                }
            },
            animation: 'none',
            closeAnimation: 'none'
        });
    } else {
        var cartSelected = $('#qc-order-details .cart-item-header.curCartSelected');
        var type = $(cartSelected).data('manual-discount-type');
        var percent = $(cartSelected).data('manual-discount-percent');
        var money = $(cartSelected).data('manual-discount-money');
        var reason = $(cartSelected).data('manual-discount-reason');
        var note = $(cartSelected).data('manual-discount-note');

        if ($(cartSelected).data('selfdiscount') == 1) {
            $("input:radio[name=modalDiscountOpt][value=" + type + "]").prop('checked', true);
            $('#modalDiscountNote').val(note);
            $('.modalDiscountReason').val(reason);
            $('.modalPercentDiscount').val(percent);
            $('.modalMoneyDiscount').val(money);
        } else {
            $("input:radio[name=modalDiscountOpt][value=0]").prop('checked', true);
            $('#modalDiscountNote').val('');
            $('.modalDiscountReason').val('');
            $('.modalPercentDiscount').val(0);
            $('.modalMoneyDiscount').val(0);
        }

        $('#discountModal').modal('show');
    }
});
$(document).on('click', '#btnSaveDrinkDiscount', function () {
    $('#discountModal').modal('hide');
    $('#qc-order-details .cart-item-header.curCartSelected').data('selfdiscount', 1);
    applyDiscountSingleDrink($('#qc-order-details .cart-item-header.curCartSelected'));
    updateCartTotal();
});

$(document).on('click', '.cart-item', function (e) {
    e.preventDefault();

    $("#drink-quantity-wrapper").removeClass("invisible");
    $("#drink-controls").removeClass("invisible");
    $("#drink-selector-wrapper").removeClass("invisible");

    resetDrinkSelect();

    var cartIdx = $(this).data('index');
    var catId = $(this).data('catid');
    var drinkId = $(this).data('drinkid');
    var sizeId = $(this).data('sizeid');
    var iceName = $(this).data('icename');
    var sugarName = $(this).data('sugarname');
    var drinkQuantity = $(this).data('quantity');

    $("#qc-order-details .cart-item").removeClass("cart-item-highlight");
    $("#qc-order-details .cart-item").removeClass("curCartSelected");

    $(".cart-item[data-index=" + cartIdx + "]").addClass('curCartSelected');
    $("#category-list .cat-opt[data-catid=" + catId + "]").addClass("highlighted");
    $("#category-list .cat-opt[data-catid=" + catId + "]").trigger('click');

    $("#drink-list .drink-opt[data-drinkid=" + drinkId + "]").addClass("highlighted");
    $("#drink-list .drink-opt[data-drinkid=" + drinkId + "]").trigger('click');

    $("#size-list .size-opt").removeClass("highlighted");
    $("#size-list .size-opt[data-sizeid=" + sizeId + "]").addClass("highlighted");

    $("#ice-list .ice-opt").removeClass("highlighted");
    $("#ice-list .ice-opt[data-name='" + iceName + "']").addClass("highlighted");

    $("#sugar-list .sugar-opt").removeClass("highlighted");
    $("#sugar-list .sugar-opt[data-name='" + sugarName + "']").addClass("highlighted");
    $('#drink-quantity').val(drinkQuantity);

    $('.cart-item[data-index=' + cartIdx + ']').find('.topping').each(function (index) {
        $("#topping-list .topping-opt[data-toppingid=" + $(this).data('toppingid') + "]").addClass("highlighted");
        let $toppingOptBtns = $(
            '<div class="topping-opt-btns">' +
            '<button type="button" class="btn btn-info btn-sm btn-minus"><i class="fa fa-minus"></i></button>' +
            '<input class="topping-quantity" type="text" value="' + $(this).data('quantity') + '" />' +
            '<button type="button" class="btn btn-info btn-sm btn-plus"><i class="fa fa-plus"></i></button>' +
            '</div>');
        $("#topping-list .topping-opt[data-toppingid=" + $(this).data('toppingid') + "]").parent().append($toppingOptBtns);

        $toppingOptBtns.on('click', '.btn-plus', function () {
            let $toppingQuantity = $(this).siblings('.topping-quantity');
            $toppingQuantity.val(Number($toppingQuantity.val()) + 1);
        });

        $toppingOptBtns.on('click', '.btn-minus', function () {
            let $toppingQuantity = $(this).siblings('.topping-quantity');
            let currentQuantity = Number($toppingQuantity.val());
            if (currentQuantity > 1) {
                $toppingQuantity.val(currentQuantity - 1);
            }
            else if (currentQuantity == 1) {
                $toppingOptBtns.siblings('div.topping-opt').removeClass("highlighted");
                $toppingOptBtns.remove();
            }
        });
    });
});

$(document).on("click", "#promotionSubmitBtn", function () {
    var promotionCode = $("#promotionCode").val();
    var orderId = 0;
    if ($("#OrderId").length > 0)
        orderId = $("#OrderId").val();

    $.get("/Orders/CheckPromotionCode?c=" + promotionCode + "&orderid=" + orderId, function (data) {
        promotionData = data;
        if (data.success) {
            updateCartTotal();

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
                alert("Khuyến mãi FREE UP SIZE và 1 topping đầu tiên cho tất cả các món");
            }
            else if (data.applyRule === 3) {
                alert("Khuyến mãi TẶNG MÓN");
            }
            else if (data.applyRule === 4) {
                alert("Khuyến mãi MUA " + data.buyQuantity + " TẶNG " + data.giveQuantity);
            }
        }
        else {
            $("#promotionCode").val('');
            $('#cart-promotion').text('');
            alert("Mã khuyến mãi không đúng hoặc đã hết hiệu lực!");
        }
    });
});

$(document).on('click', '#collapse-top-payment', function (e) {
    e.preventDefault();
    if ($(this).closest('.m-portlet').hasClass('m-portlet--collapse')) {
        $('#qc-order-details tbody').css('max-height', '70vh');
    } else {
        $('#qc-order-details tbody').css('max-height', '400px');
    }
});

$(document).on('click', '.dropdown-item', function (e) {
    e.preventDefault();
    var timeVal = $(this).data('val');
    $(this).closest('.input-group').find('input.time-input').val(timeVal).trigger('change');
});

$(document).on('change', '#OrderPlacedTimePicker', function () {
    $('#OrderPlaced').val($('#OrderPlacedDatePicker').val() + ' ' + $(this).val());
});
$(document).on('change', '#OrderDeliveriedTimePicker', function () {
    $('#OrderDeliveried').val($('#OrderDeliveriedDatePicker').val() + ' ' + $(this).val());
});

$(document).on('change', '#OrderPlacedDatePicker', function () {
    $('#OrderPlaced').val($(this).val() + ' ' + $('#OrderPlacedTimePicker').val());
});
$(document).on('change', '#OrderDeliveriedDatePicker', function () {
    $('#OrderDeliveried').val($(this).val() + ' ' + $('#OrderDeliveriedTimePicker').val());
});

$(document).on('click', '.btnPrintDetail', function () {
    $(this).prop('disabled', true);
    $('#IsPrint').val('True');
    $('#TypePrint').val($(this).data('type'));
    $('#btnSubmitForm').trigger('click');
});

$(document).on('click', '#btn-updateCustomer', function (e) {
    e.preventDefault();

    $('#srchcustomer-container').empty();
    $('#updateCustomer').modal('show');
});

$(document).on('click', '.row-cus-chks', function () {
    $('.cus-chks:checkbox:checked').prop('checked', false);
    $(this).find('.cus-chks').prop('checked', true);
});

$(document).on('click', '#btnSrchCustomer', function (e) {
    e.preventDefault();
    $(this).prop('disabled', true);
    $.post('/Orders/FilterCustomer', {
        orderCustomerType: $('#srchCusType').val(),
        customerName: $('#srchCusName').val(),
        customerPhone: $('#srchCusPhone').val()
    }, function (data) {
        $('#srchcustomer-container').empty();
        $('#srchcustomer-container').append(data);
        $('#btnSrchCustomer').removeAttr('disabled');
    });
});

$(document).on('click', '#btnSaveUpdateCustomer', function (e) {
    e.preventDefault();
    var cusId = $('.cus-chks:checkbox:checked').val();
    var type = $('.cus-chks:checkbox:checked').parent().find('.selectedCusType').val();
    var primaryName = $('.cus-chks:checkbox:checked').parent().find('.selectedPrimaryName').val();
    var firstName = $('.cus-chks:checkbox:checked').parent().find('.selectedFirstName').val();
    var midName = $('.cus-chks:checkbox:checked').parent().find('.selectedMidName').val();
    var lastName = $('.cus-chks:checkbox:checked').parent().find('.selectedLastName').val();

    $('#OrderCustomerType').val(type);
    if (type === 'Customer') {
        $('#CustomerId').val(cusId);
        $('#AgencyId').val('');
        $('#LastName').val(lastName);
        $('#MiddleName').val(midName);
        $('#FirstName').val(firstName);
    } else if (type === 'Agency') {
        $('#CustomerId').val('');
        $('#AgencyId').val(cusId);
        $('#LastName').val('');
        $('#MiddleName').val('');
        $('#FirstName').val(primaryName);
    }

    $('#updateCustomer').modal('hide');
    $('.modal-backdrop').remove();
});

$(document).on('click', '#btn-updateOrderSource', function (e) {
    e.preventDefault();

    $('#srchordersource-container').empty();
    $('#updateOrderSource').modal('show');
});

$(document).on('click', '.row-source-chks', function () {
    $('.source-chks:checkbox:checked').prop('checked', false);
    $(this).find('.source-chks').prop('checked', true);
});

$(document).on('click', '#btnSrchSourceType', function (e) {
    e.preventDefault();
    $(this).prop('disabled', true);
    $.post('/Orders/FilterOrderSource', {
        sourceType: $('#srchSourceType').val(),
        sourceName: $('#srchSourceName').val()
    }, function (data) {
        $('#srchordersource-container').empty();
        $('#srchordersource-container').append(data);
        $('#btnSrchSourceType').removeAttr('disabled');
        if ($('#OrderSourceId').val() != '' && $('#OrderSourceId').val() != undefined) {
            $('.source-chks:checkbox[value=' + $('#OrderSourceId').val() + ']').prop("checked", "true");
        }
    });
});

$(document).on('click', '#btnSaveUpdateOrderSource', function (e) {
    e.preventDefault();
    var sourceId = $('.source-chks:checkbox:checked').val();
    $('#OrderSourceId').val(sourceId);
    $('#updateOrderSource').modal('hide');
    $('.modal-backdrop').remove();
});

$("form").submit(function (e) {
    e.preventDefault();
    if ($(this).valid()) {
        $('input[type=submit]', this).attr('disabled', 'disabled');

        updateCartTotal();

        var isCreate = $(this).hasClass('iscreate');

        $.ajax({
            type: "POST",
            url: $(this).attr('action'),
            data: $(this).serialize(),
            xhrFields: { responseType: "arraybuffer" }
        }).done(function (data) {
            if (isCreate) {
                window.history.back();
            } else {
                location.reload();
            }

            if (data.byteLength > 0) {
                var newBlob = new Blob([data], { type: 'application/pdf' });

                // IE doesn't allow using a blob object directly as link href
                // instead it is necessary to use msSaveOrOpenBlob
                if (window.navigator && window.navigator.msSaveOrOpenBlob) {
                    window.navigator.msSaveOrOpenBlob(newBlob);
                    return;
                }

                // For other browsers:
                // Create a link pointing to the ObjectURL containing the blob.
                var objectUrl = window.URL.createObjectURL(newBlob);
                var newTab = window.open(objectUrl);
                newTab.print();
                // newTab.close();

                setTimeout(function () {
                    // For Firefox it is necessary to delay revoking the ObjectURL
                    window.URL.revokeObjectURL(objectUrl);
                }, 100);
            }
        });
    }
});

var onSuccessGetCartItem = function (cartItem) {
    var index = $(cartItem).data("index");
    var hlIndex = index;
    var $existCartItem = $("#qc-order-details .cart-item[data-index=" + index + "][data-mainrow='1']");
    if ($existCartItem.length == 0) {
        //new
        //find same drink & options
        var similarCartItem = findSimilarCartItem(cartItem);
        if (similarCartItem != null) {
            var oldQuantity = $(similarCartItem).data("quantity");
            var newQuantity = $(cartItem).data('quantity');
            var mergeQuantity = oldQuantity + newQuantity;
            $(similarCartItem).data("quantity", mergeQuantity);
            $(similarCartItem).find(".quantity-value").text(numberWithCommas(mergeQuantity));
            $(similarCartItem).find(".quantity-value").val(mergeQuantity);
            $(similarCartItem).next().find(".quantity-value").text(numberWithCommas(mergeQuantity));
            hlIndex = $(similarCartItem).data("index");
        }
        else {
            $("#qc-order-details tbody").append(cartItem);
        }
    }
    else {
        //edit
        var similarCartItem = findSimilarCartItem(cartItem);
        if (similarCartItem != null) {
            var oldQuantity = $(similarCartItem).data("quantity");
            var newQuantity = $(cartItem).data('quantity');
            var mergeQuantity = oldQuantity + newQuantity;
            $(similarCartItem).data("quantity", mergeQuantity);
            $(similarCartItem).find(".quantity-value").text(numberWithCommas(mergeQuantity));
            $(similarCartItem).find(".quantity-value").val(mergeQuantity);
            $(similarCartItem).next().find(".quantity-value").text(numberWithCommas(mergeQuantity));
            hlIndex = $(similarCartItem).data("index");
            $existCartItem.find(".isDeleted").val("true");
            $existCartItem.hide();
            $existCartItem.nextAll(':lt(2)').hide();
        }
        else {
            $existCartItem.next().remove();
            $existCartItem.next().remove();
            $existCartItem.replaceWith(cartItem);
        }
    }

    $("#qc-order-details .cart-item").removeClass("cart-item-highlight");
    $("#qc-order-details .cart-item").removeClass("curCartSelected");
    $("#qc-order-details .cart-item[data-index=" + hlIndex + "]").addClass("cart-item-highlight");
    if ($existCartItem.length == 0) {
        $("#qc-order-details tbody").animate({ scrollTop: $('#qc-order-details tbody').prop("scrollHeight") }, 1000);
    }

    resetDrinkSelect();
    updateCartItems();
    applyDiscountSingleDrink($("#qc-order-details .cart-item[data-index=" + hlIndex + "]"));
    updateCartTotal();
}

var findSimilarCartItem = function (newCartItem) {
    var newCartItemInfo = getInfoFromCartItem(newCartItem);
    var cartItem = null;
    $("#qc-order-details .cart-item[data-mainrow='1']").each(function () {
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
    var sugarOptionId = $(cartItem).find("[id$=__SugarOption_]").val();
    var iceOptionId = $(cartItem).find("[id$=__IceOption_]").val();
    var drinkSizeId = $(cartItem).find("[id$=__DrinkSizeId_]").val();
    var toppings = [];
    $(cartItem).next().find(".topping").each(function () {
        var toppingId = $(this).find("[id$=__ToppingId_]").val();
        var toppingQuantity = $(this).find("[id$=__Quantity_]").val();
        toppings.push({ toppingId: toppingId, toppingQuantity: toppingQuantity });
    });
    var note = $(cartItem).next().next().find(".cart-item-note").val();
    return { drinkId: drinkId, sugarOptionId: sugarOptionId, iceOptionId: iceOptionId, drinkSizeId: drinkSizeId, toppings: toppings, note: note };
}

var compareCartItem = function (item1, item2) {
    return item1.drinkId == item2.drinkId
        && item1.sugarOptionId == item2.sugarOptionId
        && item1.iceOptionId == item2.iceOptionId
        && item1.drinkSizeId == item2.drinkSizeId
        && item1.toppings.length == item2.toppings.length
        && item1.note == item2.note
        && item1.toppings.every(function (topping, index) {
            return topping.toppingId == item2.toppings[index].toppingId && topping.toppingQuantity == item2.toppings[index].toppingQuantity;
        });
}

let resetDrinkSelect = function () {
    $("#category-list div.cat-opt").removeClass("highlighted");
    $("#drink-quantity").val(1);
    $("#drink-list").empty();
    $("#ice-list").empty();
    $("#sugar-list").empty();
    $("#size-list").empty();

    $("#topping-list div.topping-opt").removeClass("highlighted");
    $("#topping-list div.topping-opt-btns").remove();

    resetDrinkForm();
}

let resetDrinkForm = function () {

}

var updateCartItems = function () {
    $("#qc-order-details .cart-item[data-mainrow='1']").each(function () {
        var index = $(this).data('index');
        var quantity = $(this).data("quantity");
        var price = $(this).data("price");
        var amount = quantity * price;
        $(this).next().find(".topping").each(function (index, topping) {
            var toppingPrice = $(topping).data("price");
            var toppingQuantity = $(topping).data("quantity");
            var toppingMultiPrice = toppingPrice * toppingQuantity * quantity;
            amount += toppingMultiPrice;
        });
        $("#qc-order-details .cart-item[data-index=" + index + "]").data("quantity", quantity);
        $("#qc-order-details .cart-item[data-index=" + index + "]").data("amount", amount);
        $(this).find(".drink-multi-price").html(numberWithCommas(amount));
    });
}

let updateCartTotal = function () {
    var discount = 0;
    if (promotionData != null && promotionData.success) {
        if (promotionData.applyRule === 0) {
        }
        else if (promotionData.applyRule === 1) {
            //buy 1 get 1
        }
        else if (promotionData.applyRule === 2) {
            discount = applyFreeUpSize1Topping();
        }
        else if (promotionData.applyRule === 3) {
            applyFreeDrink();
        }
    }
    $("#cart-promo-drinks").text(numberWithCommas(-discount));
    mergeDuplicateDrinks();

    var total = updateChargeDrinks();
    if (promotionData != null && promotionData.success) {
        if (promotionData.applyRule === 0) {
            if (promotionData.discountType === 1) {
                discount = total * promotionData.discountValue / 100;
            }
            else {
                discount = promotionData.discountValue;
            }
            $('#cart-promo-drinks').text(numberWithCommas(-discount));
            $('#DiscountAmount').val(discount);
            $('#_dt_am').val(discount);
        }
    }
    total -= discount;
    if (total < 0) total = 0;

    var IsAutoShipFee = $('#IsAutoShipFee').bootstrapSwitch('state');
    if (!IsAutoShipFee) {
        tempShip = $('#ShipFeeManual').val();
    }
    else if (total < 150000) {
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
    if (IsAutoShipFee) $('#cart-fee-ship').text(numberWithCommas(tempShip));
    $('#ShipFee').val(tempShip);
    total += Number(tempShip);

    var IsAutoSurcharge = $('#IsAutoSurcharge').bootstrapSwitch('state');
    if (!IsAutoSurcharge) {
        var surCharge = $('#SurchargeAmountManual').val();
        total += Number(surCharge);
    }
    $("#cart-amount-temp").text(numberWithCommas(total));
};

let applyFreeUpSize1Topping = function () {
    let discountValue = 0;
    let discountItems = [];
    let totalAmount = 0;

    $("#qc-order-details .cart-item-header").each(function () {
        if ($(this).find(".isDeleted").val() !== "true") {
            var index = $(this).data("index");
            let minSizePrice = $(this).data("min-price");
            let maxSizePrice = $(this).data("max-price");
            let minSizeName = $(this).data("min-size-name");
            let maxSizeName = $(this).data("max-size-name");
            let maxSizeId = $(this).data("max-size-id");
            let quantity = $(this).data("quantity");

            let offsetPrice = maxSizePrice - minSizePrice
            let toppingSorted = $(this).next(".cart-item-body").find(".topping").sort(function (a, b) {
                return $(a).data('price') - $(b).data('price');
            });

            let minToppingPrice = toppingSorted.length == 0 ? 0 : $(toppingSorted[0]).data('price');
            var toppingsPrice = 0;
            for (let i = 0; i < toppingSorted.length; i++) {
                toppingsPrice += $(toppingSorted[i]).data('price') * $(toppingSorted[i]).data('quantity');
            }
            let itemDiscountValue = (offsetPrice + minToppingPrice) * quantity;
            discountValue += itemDiscountValue;

            let singlePrice = maxSizePrice + toppingsPrice;

            //update UI: Món đã chọn
            $(this).data("price", maxSizePrice);
            $(this).data("amount", singlePrice * quantity);
            $(this).next(".cart-item-body").find('.cart-item-size').text("+ Size " + maxSizeName);
            $(this).next(".cart-item-body").find('.cart-item-size-price').text(numberWithCommas(maxSizePrice));
            $(this).find('.drink-single-price').text(numberWithCommas(singlePrice));
            $(this).find('.drink-multi-price').text(numberWithCommas(singlePrice * quantity));
            $(this).find('.drinkSizeId').val(maxSizeId);
        }
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
            if (data && data.length > 0) {
                let promotionStr = "Bạn được tặng " +
                    data.map(e => e.quantity + " ly " + e.drinkName).join(",")
                    + " miễn phí";
                $("#promotion-notify").html("<div class='alert alert-success' role='alert'>" + promotionStr + "</div>");
            }
        }
    });

}

let applyDiscountSingleDrink = function (cartItem) {
    if ($(cartItem).data('selfdiscount') == 1) {
        var type = $("input:radio[name=modalDiscountOpt]:checked").val();
        var percent = $('.modalPercentDiscount').val();
        var money = $('.modalMoneyDiscount').val();
        var reason = $('.modalDiscountReason').val();
        var note = $('#modalDiscountNote').val();

        var oldPrice = $(cartItem).data('oldprice');
        var quantity = $(cartItem).data('quantity');

        var amount = type == 1 ? oldPrice - ((oldPrice * percent) / 100) : type == 2 ? oldPrice - money : 0;
        $(cartItem).find('.drink-single-price').html('<span class="strikethough">' + numberWithCommas(oldPrice) + '</span><br><span>' + numberWithCommas(amount) + '</span>');

        amount = amount * quantity;
        $(cartItem).data('amount', amount);
        $(cartItem).find('.drink-multi-price').html('<span class="strikethough">' + numberWithCommas(oldPrice * quantity) + '</span><br><span>' + numberWithCommas(amount) + '</span>');

        $(cartItem).data('manual-discount-type', type);
        $(cartItem).data('manual-discount-percent', percent);
        $(cartItem).data('manual-discount-money', money);
        $(cartItem).data('manual-discount-reason', reason);
        $(cartItem).data('manual-discount-note', note);

        $(cartItem).find('.manual-discount-type').val(type);
        $(cartItem).find('.manual-discount-percent').val(percent);
        $(cartItem).find('.manual-discount-money').val(money);
        $(cartItem).find('.manual-discount-reason').val(reason);
        $(cartItem).find('.manual-discount-note').val(note);
    }
}

let mergeDuplicateDrinks = function () {
    $("#qc-order-details .cart-item-header").each(function () {
        if ($(this).find(".isDeleted").val() !== "true") {
            let cartItemCheck = $(this);
            let cartItemCheckInfo = getInfoFromCartItem(cartItemCheck);
            $("#qc-order-details .cart-item[data-mainrow='1']").each(function () {
                if ($(this).find(".isDeleted").val() !== "true" && $(this).data("index") != $(cartItemCheck).data("index")) {
                    let cartItemInfo = getInfoFromCartItem(this);

                    if (compareCartItem(cartItemInfo, cartItemCheckInfo)) {
                        let oldQuantity = $(cartItemCheck).data("quantity");
                        let newQuantity = $(this).data('quantity');
                        let mergeQuantity = oldQuantity + newQuantity;
                        let cartItemPrice = $(cartItemCheck).data("max-price");
                        let cartItemToppingPrice = 0;
                        let cartItemToppings = $(this).next(".cart-item-body").find(".topping");
                        for (let i = 0; i < cartItemToppings.length; i++) {
                            cartItemToppingPrice += $(cartItemToppings[i]).data('price') * $(cartItemToppings[i]).data('quantity');
                        }
                        let cartItemSinglePrice = cartItemPrice + cartItemToppingPrice;

                        $(cartItemCheck).data("quantity", mergeQuantity);
                        $(cartItemCheck).find(".quantity-value").text(numberWithCommas(mergeQuantity));
                        $(cartItemCheck).find(".quantity-value").val(mergeQuantity);
                        $(cartItemCheck).next().find(".quantity-value").text(numberWithCommas(mergeQuantity));

                        $(cartItemCheck).data("amount", cartItemSinglePrice * mergeQuantity);
                        $(cartItemCheck).find('.drink-single-price').text(numberWithCommas(cartItemSinglePrice));
                        $(cartItemCheck).find('.drink-multi-price').text(numberWithCommas(cartItemSinglePrice * mergeQuantity));

                        $(this).find(".isDeleted").val("true");
                        $(this).hide();
                        $(this).nextAll(':lt(2)').hide();
                    }
                }
            });
        }
    });
}

let updateChargeDrinks = function () {
    var arr = [];
    var cntItem = 0;
    var oldPrice = 0;
    var cntNewItem = 0;

    $("#qc-order-details .cart-item[data-mainrow='1']").each(function () {
        if ($(this).find(".isDeleted").val() !== "true") {
            $(this).find('.isPromoDiscount').val("false");
            let valItem = $(this).data("quantity");
            let index = $(this).data("index");
            let amount = Number($(this).data("amount"));
            let name = $(this).find(".cart-item-name").text();
            let isNew = $(this).data("isnew").toLowerCase();

            oldPrice += Number($(this).data("oldprice")) * valItem;
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
    if (total != oldPrice) {
        $(".cart-amount-drinks").text(numberWithCommas(oldPrice));
        $("#qc-order-details .cart-amount-drinks").html('<span class="strikethough">' + numberWithCommas(oldPrice) + '</span><br><span>' + numberWithCommas(total) + '</span>');
    } else {
        $(".cart-amount-drinks").text(numberWithCommas(total));
    }

    if (cntItem > 0) {
        var IsAutoDiscount = $('#IsAutoDiscount').bootstrapSwitch('state');
        if (!IsAutoDiscount) {
            var result = arr.reduce(function (a, b) { return a + b['amount'] }, 0);
            return result - $('#DiscountAmountManual').val();
        } else {
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
        }
    } else {
        $('#cart-promo-drinks').text(0);
        return 0;
    }
};

//buy 1 get 1
let updateDiscountDrinks = function (arr) {
    if (arr.length > 0) {
        var amount = arr.reduce(function (a, b) { return a + b['amount'] }, 0);
        $('#cart-promo-drinks').text(numberWithCommas(-amount));
        $('#DiscountAmount').val(amount);
        $('#_dt_am').val(amount);
    } else {
        $('#cart-promo-drinks').text(0);
    }
};

let updateFeeShip1Get1 = function () {
    let cntItem = 0;
    let cntNewItem = 0;
    $("#qc-order-details .cart-item[data-mainrow='1']").each(function () {
        if ($(this).find(".isDeleted").val() !== "true") {
            let valItem = $(this).data("quantity");
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
    else if ($('#_ao_dt_pg').val() == '1' || (promotionData != null && promotionData.success && promotionData.applyRule === 5)) {
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
    $("#qc-order-details .cart-item[data-mainrow='1']").each(function () {
        if ($(this).find(".isDeleted").val() !== "true") {
            var valItem = $(this).data("quantity");
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

//html template
let getToppingTempate = function (topping, i) {
    var $html = $('<div class="topping-opt-group"></div>');
    $html.append('<div class="topping-opt" data-id=' + i + ' data-toppingid=' + topping.id + ' data-price=' + topping.price + '><p>' + topping.name + '</p></div>');
    //$html.append('<input class="checkbox-topping invisible" name="OrderDetailToppings[' + i + '].IsChecked" type="checkbox" value="false">');
    return $html;
}