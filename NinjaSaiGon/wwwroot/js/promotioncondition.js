String.prototype.replaceAll = function (search, replacement) {
    var target = this;
    return target.replace(new RegExp(search, 'g'), replacement);
};

$(".currency-input").inputmask("currency", { digits: 0, prefix: "", autoUnmask: true, removeMaskOnSubmit: true });

$(".datepicker").datepicker({
    format: "dd/mm/yyyy",
    todayHighlight: !0,
    orientation: "bottom left",
    templates: {
        leftArrow: '<i class="la la-angle-left"></i>',
        rightArrow: '<i class="la la-angle-right"></i>'
    }
});

$(".timepicker").timepicker({
    showMeridian: false
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

//Photo
$("#photoInput").on("change", function (evt) {
    var files = evt.target.files;
    var formData = new FormData();
    formData.append("files", files[0]);

    $.ajax({
        type: "POST",
        url: "/Upload/UploadPhoto",
        data: formData,
        contentType: false,
        processData: false
    }).done(function (response) {
        console.log(response);
        if (response.success) {
            var pIndex = $("#photo-container").data('index');
            $("#photo-container").data('index', pIndex + 1);
            var $photo = $('<div></div>').attr('id', 'photo-' + pIndex).attr('class', 'col-sm-3 text-center').html($("#addPhotoTemplate").html().replaceAll("{pIndex}", pIndex));
            $photo.appendTo($("#photo-container"));
            $photo.find('.img').css("background-image", "url(" + response.url + ")");
            $photo.find('.url-hidden').val(response.url);
            $photo.find('button').data('id', pIndex);
        } else {
            $.confirm({
                title: 'Lỗi:',
                content: response.status,
                type: 'red',
                buttons: {
                    ok: {
                        text: "Đóng",
                        btnClass: 'btn-primary'
                    }
                },
                backgroundDismiss: true,
                animation: 'none',
                closeAnimation: 'none'
            });
        }
    });
});
$("#photo-container").on("click", ".img", function () {
    $("#photo-container").find('.img').removeClass('img-bordered');
    $("#photo-container").find(".isprimary-hidden").val("False");

    $(this).parent().find('.img').addClass('img-bordered');
    $(this).parent().find(".isprimary-hidden").val("True");
});
$("#photo-container").on("click", ".btn-deletePhoto", function () {
    var photoid = $(this).data('id');
    $('#photo-' + photoid).find('.IsDeletedPhoto').val('true');
    $('#photo-' + photoid).hide();
});

$('.btn-addApplyHour').on('click', function (e) {
    e.preventDefault();

    var pIndex = $("#apply-hr-container").data('index');
    $("#apply-hr-container").data('index', pIndex + 1);
    var applyHour = $("#addApplyHourTemplate").html().replaceAll("{pIndex}", pIndex);
    $('#add-applyhr-container').append(applyHour);
    $(applyHour).find('button').data('id', pIndex);
    $(".timepicker").timepicker({
        showMeridian: false
    });
});
$("#apply-hr-container").on("click", ".btn-deleteApplyHour", function () {
    var hrid = $(this).data('id');
    $('#apply-hr-' + hrid).find('.IsDeletedApplyHour').val('true');
    $('#apply-hr-' + hrid).hide();
});

$(document).on('change', '.dow-chkbox', function (e) {
    e.preventDefault();

    var allVals = [];
    $('input[name=DayOfWeeks]:checked').each(function () {
        allVals.push($(this).val());
    });
    $('#ApplyDayOfWeek').val(allVals);
});
$('#ApplyTimeType').on('change', function () {
    if (this.value === '0') {
        $("#applyDatesSelect").show();
        $("#applyHoursSelect").hide();
        $("#applyDOWSelect").hide();
    }
    else if (this.value === '1') {
        $("#applyHoursSelect").show();
        $("#applyDatesSelect").hide();
        $("#applyDOWSelect").hide();
    }
    else if (this.value === '2') {
        $("#applyDOWSelect").show();
        $("#applyDatesSelect").hide();
        $("#applyHoursSelect").hide();
    }
});
$('#ApplyTimeType').trigger('change');

$.get('/Orders/DrinkCategories', function (categories) {
    for (var i = 0; i < categories.length; i++) {
        let category = categories[i];
        $("#category-list").append('<div class="cat-opt" data-id=' + i + ' data-catId=' + category.id + '><p>' + categories[i].name + '</p></div>');
    }
    $("#category-list div.cat-opt").on("click", function () {
        resetDrinkSelect();

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
                '<input class="topping-quantity" name="OrderDetailToppings[' + index + '].Quantity" type="text" value="1" />' +
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
});

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

    $("#pd-order-details .cart-item").removeClass("cart-item-highlight");
    $("#pd-order-details .cart-item").removeClass("curCartSelected");

    resetDrinkSelect();
});
$("#btn-remove-drink").on("click", function () {
    $.confirm({
        title: 'Chú ý',
        content: 'Bạn có muốn xóa món này khỏi danh sách?',
        type: 'green',
        buttons: {
            ok: {
                text: "Xóa",
                btnClass: 'btn-primary',
                keys: ['enter'],
                action: function () {
                    var $cartItem = $("#pd-order-details .cart-item.curCartSelected");
                    $cartItem.find(".isDeleted").val("true");
                    $cartItem.hide();
                    $cartItem.removeClass('curCartSelected');

                    resetDrinkSelect();
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
});
$("#btn-save-drink").on("click", function () {
    var msg = [];
    var typePromo = $(this).data('type');
    if ($('.drink-opt.highlighted').length <= 0) msg.push('món');
    if ($('.size-opt.highlighted').length <= 0) msg.push('size');

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
            Id: $("#pd-order-details .cart-item.curCartSelected").length > 0 ? $("#pd-order-details .cart-item.curCartSelected").data('id') : 0,
            Index: $("#pd-order-details .cart-item.curCartSelected").length > 0 ? $("#pd-order-details .cart-item.curCartSelected").data('index') : $('#pd-order-details .cart-item[data-mainrow="1"]').length,
            PromotionId: $("#pd-order-details .cart-item.curCartSelected").length > 0 ? $("#pd-order-details .cart-item.curCartSelected").data('promotionid') : 0,
            DrinkId: $('.drink-opt.highlighted').data('drinkid'),
            DrinkSizeId: $('.size-opt.highlighted').data('sizeid'),
            Quantity: $('#drink-quantity').val(),
            PromotionDrinkToppings: toppings
        };

        if (typePromo == 0) {
            $.post('/Promotion/GetPromotionDrinkItem', data, function (cartItem) {
                onSuccessGetPromotionDrinkItem(cartItem);
            });
        } else {
            $.post('/PrivatePromotion/GetPrivatePromotionDrinkItem', data, function (cartItem) {
                onSuccessGetPromotionDrinkItem(cartItem);
            });
        }
    }
});

$(document).on('change', 'input[name="PromotionGiftType"]', function () {
    var tabId = $(this).data("target");
    $(".tab-content.gift-type > .tab-pane").hide();
    $(".tab-content.gift-type > #" + tabId).show();
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
    var drinkQuantity = $(this).data('quantity');

    $("#pd-order-details .cart-item").removeClass("cart-item-highlight");
    $("#pd-order-details .cart-item").removeClass("curCartSelected");

    $(".cart-item[data-index=" + cartIdx + "]").addClass('curCartSelected');
    $("#category-list .cat-opt[data-catid=" + catId + "]").addClass("highlighted");
    $("#category-list .cat-opt[data-catid=" + catId + "]").trigger('click');

    $("#drink-list .drink-opt[data-drinkid=" + drinkId + "]").addClass("highlighted");
    $("#drink-list .drink-opt[data-drinkid=" + drinkId + "]").trigger('click');

    $("#size-list .size-opt").removeClass("highlighted");
    $("#size-list .size-opt[data-sizeid=" + sizeId + "]").addClass("highlighted");

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

            $toppingQuantity.val(currentQuantity - 1);

            if (currentQuantity == 1) {
                $toppingOptBtns.siblings('div.topping-opt').removeClass("highlighted");
                $toppingOptBtns.remove();
            }
        });
    });
});

var onSuccessGetPromotionDrinkItem = function (cartItem) {
    var index = $(cartItem).data("index");
    var hlIndex = index;
    var $existCartItem = $("#pd-order-details .cart-item[data-index=" + index + "][data-mainrow='1']");
    if ($existCartItem.length == 0) {
        $("#pd-order-details tbody").append(cartItem);
    }
    else {
        //edit
        $existCartItem.hide();
        $existCartItem.find(".isDeleted").val("true");
        $existCartItem.next().remove();
        $existCartItem.before(cartItem);
    }

    $("#pd-order-details .cart-item").removeClass("cart-item-highlight");
    $("#pd-order-details .cart-item").removeClass("curCartSelected");
    $("#pd-order-details .cart-item[data-index=" + hlIndex + "]").addClass("cart-item-highlight");
    if ($existCartItem.length == 0) {
        $("#pd-order-details tbody").animate({ scrollTop: $('#pd-order-details tbody').prop("scrollHeight") }, 1000);
    }

    resetDrinkSelect();
    //updateCartItems();
}

var getInfoFromCartItem = function (cartItem) {
    var drinkId = $(cartItem).find("[id$=__DrinkId_]").val();
    var drinkSizeId = $(cartItem).find("[id$=__DrinkSizeId_]").val();
    var toppings = [];
    $(cartItem).next().find(".topping").each(function () {
        var toppingId = $(this).find("[id$=__ToppingId_]").val();
        var toppingQuantity = $(this).find("[id$=__Quantity_]").val();
        toppings.push({ toppingId: toppingId, toppingQuantity: toppingQuantity });
    });
    var note = $(cartItem).next().next().find(".cart-item-note").val();
    return { drinkId: drinkId, drinkSizeId: drinkSizeId, toppings: toppings, note: note };
}

var compareCartItem = function (item1, item2) {
    return item1.drinkId == item2.drinkId
        && item1.drinkSizeId == item2.drinkSizeId
        && item1.toppings.length == item2.toppings.length
        && item1.note == item2.note
        && item1.toppings.every(function (topping, index) {
            return topping.toppingId == item2.toppings[index].toppingId && topping.toppingQuantity == item2.toppings[index].toppingQuantity;
        });
}

var updateCartItems = function () {
    $("#pd-order-details .cart-item[data-mainrow='1']").each(function () {
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
        $("#pd-order-details .cart-item[data-index=" + index + "]").data("quantity", quantity);
        $("#pd-order-details .cart-item[data-index=" + index + "]").data("amount", amount);
        $(this).find(".drink-multi-price").html(numberWithCommas(amount));
    });
}

let resetDrinkSelect = function () {
    $("#category-list div.cat-opt").removeClass("highlighted");
    $("#drink-quantity").val(1);
    $("#drink-list").empty();
    $("#size-list").empty();


    $("#topping-list div.topping-opt").removeClass("highlighted");
    $("#topping-list div.topping-opt-btns").remove();

    resetDrinkForm();
}

let resetDrinkForm = function () {

}

let getToppingTempate = function (topping, i) {
    var $html = $('<div class="topping-opt-group"></div>');
    $html.append('<div class="topping-opt" data-id=' + i + ' data-toppingid=' + topping.id + ' data-price=' + topping.price + '><p>' + topping.name + '</p></div>');
    //$html.append('<input class="checkbox-topping invisible" name="OrderDetailToppings[' + i + '].IsChecked" type="checkbox" value="false">');
    return $html;
}

var onSuccessEditPromotionDrinkSetting = function (data) {

    if (data.success) {
        $.confirm({
            title: 'Lưu thành công',
            content: '',
            type: 'green',
            buttons: {
                ok: {
                    text: "Đóng",
                    btnClass: 'btn-primary'
                }
            },
            backgroundDismiss: true,
            animation: 'none',
            closeAnimation: 'none'
        });
    }
    else {
        $.confirm({
            title: 'Lưu thất bại',
            content: '',
            type: 'red',
            buttons: {
                ok: {
                    text: "Đóng",
                    btnClass: 'btn-primary'
                }
            },
            backgroundDismiss: true,
            animation: 'none',
            closeAnimation: 'none'
        });
    }
}

var onFailureEditPromotionDrinkSetting = function () {
    console.log("onFailureEditPromotionDrinkSetting");
}

$(document).on('click', '.btnDeleteCode', function () {
    $('#PrivateCodeId').val($(this).data('id'));
    $('#deleteCodeModal').modal('show');
});

$(document).on('click', '.btnEditCode', function () {
    $.get('/PrivatePromotion/EditCode?id=' + $(this).data('id'), function (data) {
        $('#editcode-container').empty();
        $('#editcode-container').html(data);
        $(".datepicker").datepicker({
            format: "dd/mm/yyyy",
            todayHighlight: !0,
            orientation: "bottom left",
            templates: {
                leftArrow: '<i class="la la-angle-left"></i>',
                rightArrow: '<i class="la la-angle-right"></i>'
            }
        });
        $('#editCodeModal').modal('show');
    });
});

$(document).on('change', '.code-status', function () {
    $.post('/PrivatePromotion/UpdateCodeStatus?id=' + $(this).data('id'), function () { });
});

$(document).on('click', '#btnSearchCode', function () {
    $.get('/PrivatePromotion/FilterCode?code=' + $('#srch-code').val() + '&ppid=' + $('#srch-ppid').val() + '&status=' + $('#srch-status').val() + '&startdate=' + $('#srch-startdate').val() + '&enddate=' + $('#srch-enddate').val(), function (data) {
        $('#tb-code-container').empty();
        $('#tb-code-container').html(data);
    });
});

$(document).on('click', '#btnSearchCode', function () {
    $.get('/PrivatePromotion/ManageCode?code=' + $('#srch-code').val() + '&ppid=' + $('#srch-prp').val() + '&status=' + $('#srch-status').val() + '&startdate=' + $('#srch-startdate').val() + '&enddate=' + $('#srch-enddate').val(), function () { });
});

var onSuccessGenerateCode = function (data) {
    $('#generateCodeModal').modal('hide');
    $('.modal-backdrop').remove();
    if (data.success) {
        $.confirm({
            title: 'Lưu thành công',
            content: '',
            type: 'green',
            buttons: {
                ok: {
                    text: "Đóng",
                    btnClass: 'btn-primary',
                    action: function () {
                        location.reload();
                    }
                }
            },
            backgroundDismiss: true,
            animation: 'none',
            closeAnimation: 'none'
        });
    }
    else {
        $.confirm({
            title: 'Lưu thất bại',
            content: '',
            type: 'red',
            buttons: {
                ok: {
                    text: "Đóng",
                    btnClass: 'btn-primary',
                    action: function () {
                        location.reload();
                    }
                }
            },
            backgroundDismiss: true,
            animation: 'none',
            closeAnimation: 'none'
        });
    }
}

var onFailureGenerateCode = function () {
    $('#generateCodeModal').modal('hide');
    $('.modal-backdrop').remove();
    $.confirm({
        title: 'Lỗi GenerateCode',
        content: '',
        type: 'red',
        buttons: {
            ok: {
                text: "Đóng",
                btnClass: 'btn-primary'
            }
        },
        backgroundDismiss: true,
        animation: 'none',
        closeAnimation: 'none'
    });
}

var onSuccessDeleteCode = function (data) {
    $('#deleteCodeModal').modal('hide');
    $('.modal-backdrop').remove();
    if (data.success) {
        $.confirm({
            title: 'Xóa thành công',
            content: '',
            type: 'green',
            buttons: {
                ok: {
                    text: "Đóng",
                    btnClass: 'btn-primary',
                    action: function () {
                        $('.row-code[data-id=' + data.codeId + ']').remove();
                    }
                }
            },
            backgroundDismiss: true,
            animation: 'none',
            closeAnimation: 'none'
        });
    }
    else {
        $.confirm({
            title: 'Xóa thất bại',
            content: '',
            type: 'red',
            buttons: {
                ok: {
                    text: "Đóng",
                    btnClass: 'btn-primary',
                    action: function () {
                    }
                }
            },
            backgroundDismiss: true,
            animation: 'none',
            closeAnimation: 'none'
        });
    }
}

var onFailureDeleteCode = function () {
    $('#deleteCodeModal').modal('hide');
    $('.modal-backdrop').remove();
    $.confirm({
        title: 'Lỗi xóa mã',
        content: '',
        type: 'red',
        buttons: {
            ok: {
                text: "Đóng",
                btnClass: 'btn-primary'
            }
        },
        backgroundDismiss: true,
        animation: 'none',
        closeAnimation: 'none'
    });
}

var onSuccessEditCode = function (data) {
    $('#editCodeModal').modal('hide');
    $('.modal-backdrop').remove();
    if (data.success) {
        $.confirm({
            title: 'Lưu thành công',
            content: '',
            type: 'green',
            buttons: {
                ok: {
                    text: "Đóng",
                    btnClass: 'btn-primary',
                    action: function () {
                        $('.row-code[data-id=' + data.codeId + ']').find('.row-code-status').find('input').prop('checked', data.codeStatus);
                        $('.row-code[data-id=' + data.codeId + ']').find('.row-code-use').html(data.codeUse);
                        $('.row-code[data-id=' + data.codeId + ']').find('.row-code-time').html(data.codeTime);
                        $('.row-code[data-id=' + data.codeId + ']').find('.row-code-comment').html(data.codeComment);
                    }
                }
            },
            backgroundDismiss: true,
            animation: 'none',
            closeAnimation: 'none'
        });
    }
    else {
        $.confirm({
            title: 'Lưu thất bại',
            content: '',
            type: 'red',
            buttons: {
                ok: {
                    text: "Đóng",
                    btnClass: 'btn-primary',
                    action: function () {
                    }
                }
            },
            backgroundDismiss: true,
            animation: 'none',
            closeAnimation: 'none'
        });
    }
}

var onFailureEditCode = function () {
    $('#editCodeModal').modal('hide');
    $('.modal-backdrop').remove();
    $.confirm({
        title: 'Lỗi sửa mã',
        content: '',
        type: 'red',
        buttons: {
            ok: {
                text: "Đóng",
                btnClass: 'btn-primary'
            }
        },
        backgroundDismiss: true,
        animation: 'none',
        closeAnimation: 'none'
    });
}