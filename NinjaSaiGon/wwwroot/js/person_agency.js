var isSelectedUsername = false;

String.prototype.replaceAll = function (search, replacement) {
    var target = this;
    return target.replace(new RegExp(search, 'g'), replacement);
};

$("#DiscountValue").inputmask("currency", { digits: 0, prefix: "", autoUnmask: true, removeMaskOnSubmit: true });

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

var tableTour = $("#srchemployee-table").DataTable({
    searching: false,
    paging: false,
    info: false,
    ordering: false,
    columnDefs: [{
        orderable: false,
        targets: 0
    }]
});

//Editing
$('.iddocument-type:not([data-index="{pIndex}"])').each(function (index, item) {
    var id = $(this).data('index');
    $('.iddocument-name[data-index=' + id + ']').val($(this).val() != '' ? $(this).find("option:selected").text() : '');
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
            $photo = $('<div></div>').attr('id', 'photo-' + pIndex).attr('class', 'col-sm-3 text-center').html($("#addPhotoTemplate").html().replaceAll("{pIndex}", pIndex));
            $photo.appendTo($("#photo-container"));
            $photo.find('.img').css("background-image", "url(" + response.url + ")");
            $photo.find('.url-hidden').val(response.url);
            $photo.find('button').data('id', pIndex);
        } else {
            alert(response.status);
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

//Tim kiem nhan vien/khach hang/nguoi dai dien/dai ly
$(document).on('click', '.btnSrchPerson', function (e) {
    e.preventDefault();
    $(this).prop('disabled', true);
    var type = $(this).data('persontype');
    var name = $('#srchPersonName').val();
    var phone = $('#srchPersonPhone').val();
    var url = type == 'Agency' ? '/Agency/FilterAgency?name=' + name + '&phone=' + phone : '/Person/FilterPerson?type=' + type + '&name=' + name + '&phone=' + phone;
    $.get(url, function (data) {
        $('.srchemployee-container').empty();
        $('.srchemployee-container').append(data);
        $('.btnSrchPerson').removeAttr('disabled');
    });
});

$(document).on('click', '.row-person-chks', function () {
    $('.person-chks:checkbox:checked').prop('checked', false);
    $(this).find('.person-chks').prop('checked', true);
});

//Nguon khach hang
$(document).on('click', '#btn-chooseCustomerSource', function (e) {
    e.preventDefault();

    $('.srchemployee-container').empty();
    var type = $('#PersonCustomerSourceTypeId').val();
    $('#chooseCustomerSourceModal').find('.btnSrchPerson').data('persontype', type == 1 ? 'Employee' : type == 2 ? 'Agency' : type == 3 ? 'Customer' : '');
    $('#chooseCustomerSourceModal').modal('show');
});

$(document).on('click', '#btnSaveCustomerSource', function (e) {
    e.preventDefault();

    var personId = $('.person-chks:checkbox:checked').val();
    var personName = $('.person-chks:checkbox:checked').data('name');
    if (personId != '' && personId != undefined) {
        var type = $('#PersonCustomerSourceTypeId').val();
        $('#CustomerSourceName').val(personName);
        if (type == 1) {
            $('#CustomerSourceEmpId').val(personId);
        } else if (type == 2) {
            $('#AgencyId').val(personId);
        } else if (type == 3) {
            $('#CustomerSourceCusId').val(personId);
        }

        $('#chooseCustomerSourceModal').modal('hide');
        $('.modal-backdrop').remove();
    } else {
        alert('Bạn chưa chọn nguồn');
    }
});

//Quoc tich
$(document).on('click', '#add-nationality', function (e) {
    e.preventDefault();
    var pIndex = $(this).data('index');
    $(this).data('index', pIndex + 1);
    $row = $("#addNationalityTemplate").html().replaceAll("{pIndex}", pIndex);
    $('#nationalities-container').append($row);
});

$(document).on('click', '.btn-deleteNationality', function (e) {
    e.preventDefault();

    var id = $(this).data('index');
    $('.nationality-item[data-index=' + id + ']').find('.IsDeletedNationality').val('true');
    $('.nationality-item[data-index=' + id + ']').hide();

    return false;
});

//Passport
$(document).on('click', '#add-passport', function (e) {
    e.preventDefault();
    var pIndex = $(this).data('index');
    $(this).data('index', pIndex + 1);
    $row = $("#addPassportTemplate").html().replaceAll("{pIndex}", pIndex);
    $('#passport-container').append($row);

    $(".datepicker").datepicker({
        format: "dd/mm/yyyy",
        todayHighlight: !0,
        orientation: "bottom left",
        templates: {
            leftArrow: '<i class="la la-angle-left"></i>',
            rightArrow: '<i class="la la-angle-right"></i>'
        }
    });
});

$(document).on('click', '.btn-deletePassport', function (e) {
    e.preventDefault();

    var id = $(this).data('index');
    $('.passport-item[data-index=' + id + ']').find('.IsDeletedPassport').val('true');
    $('.passport-item[data-index=' + id + ']').hide();

    return false;
});

//CMND
$(document).on('click', '#add-idcard', function (e) {
    e.preventDefault();
    var pIndex = $(this).data('index');
    $(this).data('index', pIndex + 1);
    $row = $("#addIDCardTemplate").html().replaceAll("{pIndex}", pIndex);
    $('#idcard-container').append($row);

    $(".datepicker").datepicker({
        format: "dd/mm/yyyy",
        todayHighlight: !0,
        orientation: "bottom left",
        templates: {
            leftArrow: '<i class="la la-angle-left"></i>',
            rightArrow: '<i class="la la-angle-right"></i>'
        }
    });
});

$(document).on('click', '.btn-deleteIDCard', function (e) {
    e.preventDefault();

    var id = $(this).data('index');
    $('.idcard-item[data-index=' + id + ']').find('.IsDeletedIDCard').val('true');
    $('.idcard-item[data-index=' + id + ']').hide();

    return false;
});

//Giay to khac
$(document).on('click', '#add-iddocument', function (e) {
    e.preventDefault();
    var pIndex = $(this).data('index');
    $(this).data('index', pIndex + 1);
    $row = $("#addIDDocumentTemplate").html().replaceAll("{pIndex}", pIndex);
    $('#iddocument-container').append($row);

    $(".datepicker").datepicker({
        format: "dd/mm/yyyy",
        todayHighlight: !0,
        orientation: "bottom left",
        templates: {
            leftArrow: '<i class="la la-angle-left"></i>',
            rightArrow: '<i class="la la-angle-right"></i>'
        }
    });
});

$(document).on('click', '.btn-deleteIDDocument', function (e) {
    e.preventDefault();

    var id = $(this).data('index');
    $('.iddocument-item[data-index=' + id + ']').find('.IsDeletedIDDocument').val('true');
    $('.iddocument-item[data-index=' + id + ']').hide();

    return false;
});

$(document).on('click', '.iddocument-type', function (e) {
    e.preventDefault();

    var id = $(this).data('index');
    $('.iddocument-name[data-index=' + id + ']').val($(this).val() != '' ? $(this).find("option:selected").text() : '');

    return false;
});

//Dien thoai
$(document).on('click', '#add-phone', function (e) {
    e.preventDefault();
    var pIndex = $(this).data('index');
    $(this).data('index', pIndex + 1);
    $row = $("#addPhoneTemplate").html().replaceAll("{pIndex}", pIndex);
    $('#phone-container').append($row);
});

$(document).on('click', '.btn-deletePhone', function (e) {
    e.preventDefault();

    var id = $(this).data('index');
    $('.phone-item[data-index=' + id + ']').find('.IsDeletedPhone').val('true');
    $('.phone-item[data-index=' + id + ']').hide();

    return false;
});

//Email
$(document).on('click', '#add-email', function (e) {
    e.preventDefault();
    var pIndex = $(this).data('index');
    $(this).data('index', pIndex + 1);
    $row = $("#addEmailTemplate").html().replaceAll("{pIndex}", pIndex);
    $('#email-container').append($row);
});

$(document).on('click', '.btn-deleteEmail', function (e) {
    e.preventDefault();

    var id = $(this).data('index');
    $('.email-item[data-index=' + id + ']').find('.IsDeletedEmail').val('true');
    $('.email-item[data-index=' + id + ']').hide();

    return false;
});

//Dia chi
$(document).on('click', '#add-address', function (e) {
    e.preventDefault();
    var pIndex = $(this).data('index');
    $(this).data('index', pIndex + 1);
    $row = $("#addAddressTemplate").html().replaceAll("{pIndex}", pIndex);
    $('#address-container').append($row);
});

$(document).on('click', '.btn-deleteAddress', function (e) {
    e.preventDefault();

    var id = $(this).data('index');
    $('.address-item[data-index=' + id + ']').find('.IsDeletedAddress').val('true');
    $('.address-item[data-index=' + id + ']').hide();

    return false;
});

$(document).on('focus', '.contact-province', function () {
    var index = $(this).data('index');
    var nationalId = $('.contact-national[data-index=' + index + ']').val();
    if (nationalId == '' || nationalId == undefined) {
        $('.contact-national[data-index=' + index + ']').focus();
        alert('Vui lòng chọn Quốc gia');
    }
});

$(document).on('focus', '.contact-district', function () {
    var index = $(this).data('index');
    var nationalId = $('.contact-national[data-index=' + index + ']').val();
    var provinceId = $('.contact-province[data-index=' + index + ']').val();

    if (nationalId == '' || nationalId == undefined) {
        $('.contact-national[data-index=' + index + ']').focus();
        alert('Vui lòng chọn Quốc gia');
    } else if ((provinceId == '' || provinceId == undefined) && nationalId == 1) {
        $('.contact-province[data-index=' + index + ']').focus();
        alert('Vui lòng chọn Tỉnh/Thành');
    }
});

$(document).on('focus', '.contact-ward', function () {
    var index = $(this).data('index');
    var nationalId = $('.contact-national[data-index=' + index + ']').val();
    var provinceId = $('.contact-province[data-index=' + index + ']').val();
    var districtId = $('.contact-district[data-index=' + index + ']').val();

    if (nationalId == '' || nationalId == undefined) {
        $('.contact-national[data-index=' + index + ']').focus();
        alert('Vui lòng chọn Quốc gia');
    } else if ((provinceId == '' || provinceId == undefined) && nationalId == 1) {
        $('.contact-province[data-index=' + index + ']').focus();
        alert('Vui lòng chọn Tỉnh/Thành');
    } else if ((districtId == '' || districtId == undefined) && nationalId == 1) {
        $('.contact-district[data-index=' + index + ']').focus();
        alert('Vui lòng chọn Quận/Huyện');
    }
});

$(document).on('change', '.contact-national', function () {
    var id = $(this).val();
    var index = $(this).data('index');
    if (id != '' && id != undefined && id != 1) {
        $('.contact-province[data-index=' + index + '] option[value!=""]').hide();
        $('.contact-province[data-index=' + index + ']').val('');
        $('.contact-district[data-index=' + index + '] option[value!=""]').hide();
        $('.contact-district[data-index=' + index + ']').val('');
        $('.contact-ward[data-index=' + index + '] option[value!=""]').hide();
        $('.contact-ward[data-index=' + index + ']').val('');
    } else {
        $('.contact-province[data-index=' + index + '] option[value!=""]').show();
        $('.contact-district[data-index=' + index + '] option[value!=""]').show();
        $('.contact-ward[data-index=' + index + '] option[value!=""]').show();
    }
});

$(document).on('change', '.contact-province', function () {
    var id = $(this).val();
    var index = $(this).data('index');
    if (id != '' && id != undefined) {
        $('.contact-district[data-index=' + index + '] option[data-parent!=' + id + ']').hide();
        $('.contact-district[data-index=' + index + '] option[data-parent=' + id + ']').show();
        $('.contact-district[data-index=' + index + '] option[value=""]').show();
        $('.contact-district[data-index=' + index + ']').val('');
    }
});

$(document).on('change', '.contact-district', function () {
    var id = $(this).val();
    var index = $(this).data('index');
    if (id != '' && id != undefined) {
        $.get('/Person/GetWardByDistrict?districtid=' + id, function (data) {
            for (var i = 0; i < data.result.length; i++) {
                var opt = new Option(data.result[i].text, data.result[i].value);
                $(opt).html(data.result[i].text);
                $('.contact-ward[data-index=' + index + ']').append(opt);
            }
        });
    }
});

//Mang xa hoi
$(document).on('click', '#add-social', function (e) {
    e.preventDefault();
    var pIndex = $(this).data('index');
    $(this).data('index', pIndex + 1);
    $row = $("#addSocialTemplate").html().replaceAll("{pIndex}", pIndex);
    $('#social-container').append($row);
});

$(document).on('click', '.btn-deleteSocial', function (e) {
    e.preventDefault();

    var id = $(this).data('index');
    $('.social-item[data-index=' + id + ']').find('.IsDeletedSocial').val('true');
    $('.social-item[data-index=' + id + ']').hide();

    return false;
});

//OTT
$(document).on('click', '#add-ott', function (e) {
    e.preventDefault();
    var pIndex = $(this).data('index');
    $(this).data('index', pIndex + 1);
    $row = $("#addOTTTemplate").html().replaceAll("{pIndex}", pIndex);
    $('#ott-container').append($row);
});

$(document).on('click', '.btn-deleteOTT', function (e) {
    e.preventDefault();

    var id = $(this).data('index');
    $('.ott-item[data-index=' + id + ']').find('.IsDeletedOTT').val('true');
    $('.ott-item[data-index=' + id + ']').hide();

    return false;
});

//Ngan hang
$(document).on('click', '#add-bank', function (e) {
    e.preventDefault();
    var pIndex = $(this).data('index');
    $(this).data('index', pIndex + 1);
    $row = $("#addBankTemplate").html().replaceAll("{pIndex}", pIndex);
    $('#bank-container').append($row);

    $(".datepicker").datepicker({
        format: "dd/mm/yyyy",
        todayHighlight: !0,
        orientation: "bottom left",
        templates: {
            leftArrow: '<i class="la la-angle-left"></i>',
            rightArrow: '<i class="la la-angle-right"></i>'
        }
    });
});

$(document).on('click', '.btn-deleteBank', function (e) {
    e.preventDefault();

    var id = $(this).data('index');
    $('.bank-item[data-index=' + id + ']').find('.IsDeletedBank').val('true');
    $('.bank-item[data-index=' + id + ']').hide();

    $('#card-container-' + id).find('.card-item').each(function () {
        $(this).find('.IsDeletedCard').val('true');
        $(this).hide();
    });

    return false;
});

//The thanh toan
$(document).on('click', '.add-card', function (e) {
    e.preventDefault();
    var pIndex = $(this).data('index');
    var bIndex = $(this).data('parent');
    $(this).data('index', pIndex + 1);
    $row = $("#addCardTemplate").html().replaceAll("{bIndex}", bIndex).replaceAll("{pIndex}", pIndex);
    $('#card-container-' + bIndex).append($row);

    $(".datepicker").datepicker({
        format: "dd/mm/yyyy",
        todayHighlight: !0,
        orientation: "bottom left",
        templates: {
            leftArrow: '<i class="la la-angle-left"></i>',
            rightArrow: '<i class="la la-angle-right"></i>'
        }
    });
});

$(document).on('click', '.btn-deleteCard', function (e) {
    e.preventDefault();

    var index = $(this).data('index');
    var parent = $(this).data('parent');
    $('.card-item[data-parent=' + parent + '][data-index=' + index + ']').find('.IsDeletedCard').val('true');
    $('.card-item[data-parent=' + parent + '][data-index=' + index + ']').hide();

    return false;
});

//Cong viec (khach hang/nguoi dai dien)
$(document).on('click', '#add-pcw', function (e) {
    e.preventDefault();
    var pIndex = $(this).data('index');
    $(this).data('index', pIndex + 1);
    $.post('/Person/AddPersonWorking', {
        index: pIndex,
        isWorking: $('#pcwIsWorking').is(':checked'),
        position: $('#pcwPosition').val(),
        department: $('#pcwDepartment').val(),
        company: $('#pcwCompany').val(),
        startDate: $('#pcwStartDate').val(),
        endDate: $('#pcwEndDate').val(),
        isMainPosition: $('#pcwIsMainPosition').is(':checked')
    }, function (data) {
        $('#pcw-history').append(data);

        $('#pcwPosition').val('');
        $('#pcwDepartment').val('');
        $('#pcwCompany').val('');
        $('#pcwStartDate').val('');
        $('#pcwEndDate').val('');
        $("#pcwIsMainPosition").prop("checked", false);
        $("#pcwIsWorking").prop("checked", false);
    });
});

$(document).on('click', '.btnEditPCW', function (e) {
    e.preventDefault();

    var index = $(this).data('index');
    var id = $(this).data('id');
    var personid = $(this).data('personid');
    var position = $(this).data('position');
    var department = $(this).data('department');
    var company = $(this).data('company');
    var ismainposition = $(this).data('ismainposition');
    var isworking = $(this).data('isworking');
    var startdate = $(this).data('startdate');
    var enddate = $(this).data('enddate');

    $('#epcwIndex').val(index);
    $('#epcwId').val(id);
    $('#epcwPersonId').val(personid);
    $('#epcwPosition').val(position);
    $('#epcwDepartment').val(department);
    $('#epcwCompany').val(company);
    $('#epcwIsWorking').prop("checked", isworking === 'True');
    $('#epcwIsMainPosition').prop("checked", ismainposition === 'True');
    $('#epcwStartDate').val(startdate);
    $('#epcwEndDate').val(enddate);

    $('.pcw-item[data-index=' + index + ']').addClass('editing');
    $('#editPCWModal').modal('show');
});

$(document).on('click', '.btnDeletePCW', function (e) {
    e.preventDefault();

    var index = $(this).data('index');
    $('.pcw-item[data-index=' + index + ']').find('.IsDeletePCW').val('true');
    $('.pcw-item[data-index=' + index + ']').hide();

    return false;
});

var onSuccessEditPCW = function (data) {
    $('#editPCWModal').modal('hide');
    $('.modal-backdrop').remove();
    $.confirm({
        title: 'Lưu thành công',
        content: '',
        type: 'green',
        buttons: {
            ok: {
                text: "Đóng",
                btnClass: 'btn-primary',
                action: function () {
                    var tr = $('.pcw-item.editing');
                    tr.replaceWith(data);
                }
            }
        },
        backgroundDismiss: true,
        animation: 'none',
        closeAnimation: 'none'
    });
}

var onFailureEditPCW = function () {
    $('#editPCWModal').modal('hide');
    $('.modal-backdrop').remove();
    $.confirm({
        title: 'Lỗi sửa công việc',
        content: '',
        type: 'red',
        buttons: {
            ok: {
                text: "Đóng",
                btnClass: 'btn-primary',
                action: function () {
                    var tr = $('.pcw-item.editing');
                    tr.removeClass('editing');
                }
            }
        },
        backgroundDismiss: true,
        animation: 'none',
        closeAnimation: 'none'
    });
}

//Cong viec (nhan vien)
$(document).on('click', '#add-pew', function (e) {
    e.preventDefault();
    var pIndex = $(this).data('index');
    $(this).data('index', pIndex + 1);
    $.post('/Person/AddEmployeeWorking', {
        index: pIndex,
        isWorking: $('#pewIsWorking').is(':checked'),
        departmentPositionId: $('#pewPosition').val(),
        departmentId: $('#pewDepartment').val(),
        trialWorkBeginDate: $('#pewTrialWorkDate').val(),
        workBeginDate: $('#pewWorkDate').val(),
        positionBeginDate: $('#pewStartDate').val(),
        positionEndDate: $('#pewEndDate').val(),
        isMainPosition: $('#pewIsMainPosition').is(':checked')
    }, function (data) {
        $('#pew-history').append(data);

        $('#pewPosition').val('');
        $('#pewDepartment').val('');
        $('#pewTrialWorkDate').val('');
        $('#pewWorkDate').val('');
        $('#pewStartDate').val('');
        $('#pewEndDate').val('');
        $("#pewIsMainPosition").prop("checked", false);
        $("#pewIsWorking").prop("checked", false);
    });
});

$(document).on('click', '.btnEditPEW', function (e) {
    e.preventDefault();

    var index = $(this).data('index');
    var id = $(this).data('id');
    var personid = $(this).data('personid');
    var departmentid = $(this).data('departmentid');
    var departmentpositionid = $(this).data('departmentpositionid');
    var ismainposition = $(this).data('ismainposition');
    var isworking = $(this).data('isworking');
    var positionbegindate = $(this).data('positionbegindate');
    var positionenddate = $(this).data('positionenddate');
    var trialworkbegindate = $(this).data('trialworkbegindate');
    var workbegindate = $(this).data('workbegindate');

    $('#epewIndex').val(index);
    $('#epewId').val(id);
    $('#epewPersonId').val(personid);
    $('#epewPosition').val(departmentpositionid);
    $('#epewDepartment').val(departmentid);
    $('#epewIsWorking').prop("checked", isworking === 'True');
    $('#epewIsMainPosition').prop("checked", ismainposition === 'True');
    $('#epewStartDate').val(positionbegindate);
    $('#epewEndDate').val(positionenddate);
    $('#epewTrialWorkDate').val(trialworkbegindate);
    $('#epewWorkDate').val(workbegindate);

    $('.pew-item[data-index=' + index + ']').addClass('editing');
    $('#editPEWModal').modal('show');
});

$(document).on('click', '.btnDeletePEW', function (e) {
    e.preventDefault();

    var index = $(this).data('index');
    $('.pew-item[data-index=' + index + ']').find('.IsDeletePEW').val('true');
    $('.pew-item[data-index=' + index + ']').hide();

    return false;
});

var onSuccessEditPEW = function (data) {
    $('#editPEWModal').modal('hide');
    $('.modal-backdrop').remove();
    $.confirm({
        title: 'Lưu thành công',
        content: '',
        type: 'green',
        buttons: {
            ok: {
                text: "Đóng",
                btnClass: 'btn-primary',
                action: function () {
                    var tr = $('.pew-item.editing');
                    tr.replaceWith(data);
                }
            }
        },
        backgroundDismiss: true,
        animation: 'none',
        closeAnimation: 'none'
    });
}

var onFailureEditPEW = function () {
    $('#editPEWModal').modal('hide');
    $('.modal-backdrop').remove();
    $.confirm({
        title: 'Lỗi sửa công việc',
        content: '',
        type: 'red',
        buttons: {
            ok: {
                text: "Đóng",
                btnClass: 'btn-primary',
                action: function () {
                    var tr = $('.pew-item.editing');
                    tr.removeClass('editing');
                }
            }
        },
        backgroundDismiss: true,
        animation: 'none',
        closeAnimation: 'none'
    });
}

//Nhan vien cham soc khach hang
$(document).on('click', '#add-pcc', function (e) {
    e.preventDefault();

    var pIndex = $(this).data('index');
    $('#pccIndex').val(pIndex);
    $('.srchemployee-container').empty();
    $('#pccStartDate').val('');
    $('#pccEndDate').val('');
    $('#addPCCModal').modal('show');
});

$(document).on('click', '#btnAddPCC', function (e) {
    e.preventDefault();

    var personId = $('.person-chks:checkbox:checked').val();
    var personName = $('.person-chks:checkbox:checked').data('name');
    var pIndex = $('#pccIndex').val();
    if (personId != '' && personId != undefined) {
        $.post('/Person/AddPersonCustomerCare', {
            index: pIndex,
            startDate: $('#pccStartDate').val(),
            endDate: $('#pccEndDate').val(),
            employeeId: personId,
            employeeName: personName
        }, function (data) {
            $('#pcc-history').append(data);
            $('#add-pcc').data('index', pIndex + 1);
        });

        $('#addPCCModal').modal('hide');
        $('.modal-backdrop').remove();
    } else {
        alert('Vui lòng chọn nhân viên chăm sóc');
    }
});

$(document).on('click', '.btnEditPCC', function (e) {
    e.preventDefault();

    var index = $(this).data('index');
    var id = $(this).data('id');
    var personid = $(this).data('personid');
    var employeeid = $(this).data('employeeid');
    var employeename = $(this).data('employeename');
    var startdate = $(this).data('startdate');
    var enddate = $(this).data('enddate');

    $('#epccIndex').val(index);
    $('#epccId').val(id);
    $('#epccPersonId').val(personid);
    $('#epccEmployeeId').val(employeeid);
    $('#epccEmployeeName').val(employeename);
    $('#epccStartDate').val(startdate);
    $('#epccEndDate').val(enddate);

    $('.pcc-item[data-index=' + index + ']').addClass('editing');
    $('#editPCCModal').modal('show');
});

$(document).on('click', '.btnDeletePCC', function (e) {
    e.preventDefault();

    var index = $(this).data('index');
    $('.pcc-item[data-index=' + index + ']').find('.IsDeletePCC').val('true');
    $('.pcc-item[data-index=' + index + ']').hide();

    return false;
});

var onSuccessEditPCC = function (data) {
    $('#editPCCModal').modal('hide');
    $('.modal-backdrop').remove();
    $.confirm({
        title: 'Lưu thành công',
        content: '',
        type: 'green',
        buttons: {
            ok: {
                text: "Đóng",
                btnClass: 'btn-primary',
                action: function () {
                    var tr = $('.pcc-item.editing');
                    tr.replaceWith(data);
                }
            }
        },
        backgroundDismiss: true,
        animation: 'none',
        closeAnimation: 'none'
    });
}

var onFailureEditPCC = function () {
    $('#editPCCModal').modal('hide');
    $('.modal-backdrop').remove();
    $.confirm({
        title: 'Lỗi sửa nhân viên chăm sóc',
        content: '',
        type: 'red',
        buttons: {
            ok: {
                text: "Đóng",
                btnClass: 'btn-primary',
                action: function () {
                    var tr = $('.pcc-item.editing');
                    tr.removeClass('editing');
                }
            }
        },
        backgroundDismiss: true,
        animation: 'none',
        closeAnimation: 'none'
    });
}

//Nhan vien cham soc (dai ly)
$(document).on('click', '#add-ac', function (e) {
    e.preventDefault();

    var pIndex = $(this).data('index');
    $('#acIndex').val(pIndex);
    $('.srchemployee-container').empty();
    $('#acStartDate').val('');
    $('#acEndDate').val('');
    $('#addACModal').modal('show');
});

$(document).on('click', '#btnAddAC', function (e) {
    e.preventDefault();

    var personId = $('.person-chks:checkbox:checked').val();
    var personName = $('.person-chks:checkbox:checked').data('name');
    var pIndex = $('#acIndex').val();
    if (personId != '' && personId != undefined) {
        $.post('/Agency/AddAgencyCare', {
            index: pIndex,
            startDate: $('#acStartDate').val(),
            endDate: $('#acEndDate').val(),
            employeeId: personId,
            employeeName: personName
        }, function (data) {
            $('#ac-history').append(data);
            $('#add-ac').data('index', pIndex + 1);
        });

        $('#addACModal').modal('hide');
        $('.modal-backdrop').remove();
    } else {
        alert('Vui lòng chọn nhân viên chăm sóc');
    }
});

$(document).on('click', '.btnEditAC', function (e) {
    e.preventDefault();

    var index = $(this).data('index');
    var id = $(this).data('id');
    var agencyid = $(this).data('agencyid');
    var employeeid = $(this).data('employeeid');
    var employeename = $(this).data('employeename');
    var startdate = $(this).data('startdate');
    var enddate = $(this).data('enddate');

    $('#eacIndex').val(index);
    $('#eacId').val(id);
    $('#eacAgencyId').val(agencyid);
    $('#eacEmployeeId').val(employeeid);
    $('#eacEmployeeName').val(employeename);
    $('#eacStartDate').val(startdate);
    $('#eacEndDate').val(enddate);

    $('.ac-item[data-index=' + index + ']').addClass('editing');
    $('#editACModal').modal('show');
});

$(document).on('click', '.btnDeleteAC', function (e) {
    e.preventDefault();

    var index = $(this).data('index');
    $('.ac-item[data-index=' + index + ']').find('.IsDeleteAC').val('true');
    $('.ac-item[data-index=' + index + ']').hide();

    return false;
});

var onSuccessEditAC = function (data) {
    $('#editACModal').modal('hide');
    $('.modal-backdrop').remove();
    $.confirm({
        title: 'Lưu thành công',
        content: '',
        type: 'green',
        buttons: {
            ok: {
                text: "Đóng",
                btnClass: 'btn-primary',
                action: function () {
                    var tr = $('.ac-item.editing');
                    tr.replaceWith(data);
                }
            }
        },
        backgroundDismiss: true,
        animation: 'none',
        closeAnimation: 'none'
    });
}

var onFailureEditAC = function () {
    $('#editACModal').modal('hide');
    $('.modal-backdrop').remove();
    $.confirm({
        title: 'Lỗi sửa nhân viên chăm sóc',
        content: '',
        type: 'red',
        buttons: {
            ok: {
                text: "Đóng",
                btnClass: 'btn-primary',
                action: function () {
                    var tr = $('.ac-item.editing');
                    tr.removeClass('editing');
                }
            }
        },
        backgroundDismiss: true,
        animation: 'none',
        closeAnimation: 'none'
    });
}

//Nguoi dai dien (dai ly)
$(document).on('click', '#add-arepresentative', function (e) {
    e.preventDefault();

    var pIndex = $(this).data('index');
    $('#arIndex').val(pIndex);
    $('.srchemployee-container').empty();
    $('#addARepresentativeModal').modal('show');
});

$(document).on('click', '#btnAddARepresentative', function (e) {
    e.preventDefault();

    var personId = $('.person-chks:checkbox:checked').val();
    var pIndex = $('#arIndex').val();
    if (personId != '' && personId != undefined) {
        $.post('/Agency/AddAgencyRepresentative', {
            index: pIndex,
            personId: personId
        }, function (data) {
            $('#arepresentative-history').append(data);
            $('#add-arepresentative').data('index', pIndex + 1);
        });

        $('#addARepresentativeModal').modal('hide');
        $('.modal-backdrop').remove();
    } else {
        alert('Vui lòng chọn người đại diện');
    }
});

$(document).on('click', '.btnDeleteAR', function (e) {
    e.preventDefault();

    var index = $(this).data('index');
    $('.ar-item[data-index=' + index + ']').find('.IsDeleteAR').val('true');
    $('.ar-item[data-index=' + index + ']').hide();

    return false;
});

//Thanh toan (dai ly)
$(document).on('click', '#add-apm', function (e) {
    e.preventDefault();

    var pIndex = $(this).data('index');
    $(this).data('index', pIndex + 1);
    $.post('/Agency/AddPayment', {
        index: pIndex,
        startDate: $('#apmStartDate').val(),
        endDate: $('#apmEndDate').val(),
        paymentTypeId: $('#apmPaymentTypeId').val(),
        currencyTypeId: $('#apmCurrencyTypeId').val(),
        paymentTermTypeId: $('#apmPaymentTermTypeId').val(),
        agencyDiscountTypeId: $('#apmAgencyDiscountTypeId').val(),
        paymentLimit: $('#apmPaymentLimit').val(),
        interestRate: $('#apmInterestRate').val(),
        paymentCredit: $('#apmPaymentCredit').val(),
        invoiceLimit: $('#apmInvoiceLimit').val(),
        discountAmount: $('#apmDiscountAmount').val(),
        note: $('#apmNote').val()
    }, function (data) {
        $('#apm-history').append(data);

        $('#apmStartDate').val('');
        $('#apmEndDate').val('');
        $('#apmPaymentTypeId').val('');
        $('#apmCurrencyTypeId').val('');
        $('#apmPaymentTermTypeId').val('');
        $('#apmAgencyDiscountTypeId').val('');
        $('#apmPaymentLimit').val('');
        $('#apmInterestRate').val('');
        $('#apmPaymentCredit').val('');
        $('#apmInvoiceLimit').val('');
        $('#apmDiscountAmount').val('');
        $('#apmNote').val('');
    });
});

$(document).on('click', '.btnEditAPM', function (e) {
    e.preventDefault();

    var index = $(this).data('index');
    var id = $(this).data('id');
    var agencyid = $(this).data('agencyid');
    var startDate = $(this).data('startdate');
    var endDate = $(this).data('enddate');
    var paymentTypeid = $(this).data('paymentTypeid');
    var currencytypeid = $(this).data('currencytypeid');
    var paymenttermtypeid = $(this).data('paymenttermtypeid');
    var agencydiscounttypeid = $(this).data('agencydiscounttypeid');
    var paymentlimit = $(this).data('paymentlimit');
    var interestrate = $(this).data('interestrate');
    var paymentcredit = $(this).data('paymentcredit');
    var invoicelimit = $(this).data('invoicelimit');
    var discountamount = $(this).data('discountamount');
    var note = $(this).data('note');

    $('#eapmIndex').val(index);
    $('#eapmId').val(id);
    $('#eapmAgencyId').val(agencyid);
    $('#eapmStartDate').val(startDate);
    $('#eapmEndDate').val(endDate);
    $('#eapmPaymentTypeId').val(paymentTypeid);
    $('#eapmCurrencyTypeId').val(currencytypeid);
    $('#eapmPaymentTermTypeId').val(paymenttermtypeid);
    $('#eapmAgencyDiscountTypeId').val(agencydiscounttypeid);
    $('#eapmPaymentLimit').val(paymentlimit);
    $('#eapmInterestRate').val(interestrate);
    $('#eapmPaymentCredit').val(paymentcredit);
    $('#eapmInvoiceLimit').val(invoicelimit);
    $('#eapmDiscountAmount').val(discountamount);
    $('#eapmNote').val(note);

    $('.apm-item[data-index=' + index + ']').addClass('editing');
    $('#editAPMModal').modal('show');
});

$(document).on('click', '.btnDeleteAPM', function (e) {
    e.preventDefault();

    var index = $(this).data('index');
    $('.apm-item[data-index=' + index + ']').find('.IsDeleteAPM').val('true');
    $('.apm-item[data-index=' + index + ']').hide();

    return false;
});

var onSuccessEditAPM = function (data) {
    $('#editAPMModal').modal('hide');
    $('.modal-backdrop').remove();
    $.confirm({
        title: 'Lưu thành công',
        content: '',
        type: 'green',
        buttons: {
            ok: {
                text: "Đóng",
                btnClass: 'btn-primary',
                action: function () {
                    var tr = $('.apm-item.editing');
                    tr.replaceWith(data);
                }
            }
        },
        backgroundDismiss: true,
        animation: 'none',
        closeAnimation: 'none'
    });
}

var onFailureEditAPM = function () {
    $('#editAPMModal').modal('hide');
    $('.modal-backdrop').remove();
    $.confirm({
        title: 'Lỗi sửa thanh toán',
        content: '',
        type: 'red',
        buttons: {
            ok: {
                text: "Đóng",
                btnClass: 'btn-primary',
                action: function () {
                    var tr = $('.apm-item.editing');
                    tr.removeClass('editing');
                }
            }
        },
        backgroundDismiss: true,
        animation: 'none',
        closeAnimation: 'none'
    });
}

//Giao hang (dai ly)
$(document).on('click', '#add-ad', function (e) {
    e.preventDefault();

    var pIndex = $(this).data('index');
    $(this).data('index', pIndex + 1);
    $.post('/Agency/AddDelivery', {
        index: pIndex,
        startDate: $('#adStartDate').val(),
        endDate: $('#adEndDate').val(),
        agencyDiscountCustomerTypeId: $('#adAgencyDiscountCustomerTypeId').val(),
        pickupTypeId: $('#adPickupTypeId').val(),
        note: $('#adNote').val()
    }, function (data) {
        $('#ad-history').append(data);

        $('#adStartDate').val('');
        $('#adEndDate').val('');
        $('#adAgencyDiscountCustomerTypeId').val('');
        $('#adPickupTypeId').val('');
        $('#adNote').val('');
    });
});

$(document).on('click', '.btnEditAD', function (e) {
    e.preventDefault();

    var index = $(this).data('index');
    var id = $(this).data('id');
    var agencyid = $(this).data('agencyid');
    var startDate = $(this).data('startdate');
    var endDate = $(this).data('enddate');
    var agencyDiscountCustomerTypeId = $(this).data('agencydiscountcustomertypeid');
    var pickupTypeId = $(this).data('pickuptypeid');
    var note = $(this).data('note');

    $('#eadIndex').val(index);
    $('#eadId').val(id);
    $('#eadAgencyId').val(agencyid);
    $('#eadStartDate').val(startDate);
    $('#eadEndDate').val(endDate);
    $('#eadAgencyDiscountCustomerTypeId').val(agencyDiscountCustomerTypeId);
    $('#eadPickupTypeId').val(pickupTypeId);
    $('#eadNote').val(note);

    $('.ad-item[data-index=' + index + ']').addClass('editing');
    $('#editADModal').modal('show');
});

$(document).on('click', '.btnDeleteAD', function (e) {
    e.preventDefault();

    var index = $(this).data('index');
    $('.ad-item[data-index=' + index + ']').find('.IsDeleteAD').val('true');
    $('.ad-item[data-index=' + index + ']').hide();

    return false;
});

var onSuccessEditAD = function (data) {
    $('#editADModal').modal('hide');
    $('.modal-backdrop').remove();
    $.confirm({
        title: 'Lưu thành công',
        content: '',
        type: 'green',
        buttons: {
            ok: {
                text: "Đóng",
                btnClass: 'btn-primary',
                action: function () {
                    var tr = $('.ad-item.editing');
                    tr.replaceWith(data);
                }
            }
        },
        backgroundDismiss: true,
        animation: 'none',
        closeAnimation: 'none'
    });
}

var onFailureEditAD = function () {
    $('#editADModal').modal('hide');
    $('.modal-backdrop').remove();
    $.confirm({
        title: 'Lỗi sửa giao hàng',
        content: '',
        type: 'red',
        buttons: {
            ok: {
                text: "Đóng",
                btnClass: 'btn-primary',
                action: function () {
                    var tr = $('.ad-item.editing');
                    tr.removeClass('editing');
                }
            }
        },
        backgroundDismiss: true,
        animation: 'none',
        closeAnimation: 'none'
    });
}


$('#dob-picker').datepicker().on('changeDate', function (e) {
    $('#dob-day').val(e.format(0, "dd"));
    $('#dob-month').val(e.format(0, "mm"));
    $('#dob-year').val(e.format(0, "yyyy"));
});

$(document).on('change', '#PersonCustomerSourceTypeId', function (e) {
    e.preventDefault();
    var type = $(this).val();
    SwitchCustomerSource(type);
});

$(document).on('change', '#selectedUserName', function (e) {
    e.preventDefault();
    $('#peUsername').val($(this).find("option:selected").text());
    $('#peIsMapAccount').val('true');
    $('.row-password').hide();
    isSelectedUsername = true;
});

$(document).on('keyup', '#peUsername', function () {
    $('#selectedUserName').val('');
    $('#peIsMapAccount').val('false');
    $('.row-password').show();
    isSelectedUsername = false;
});

$(document).on('focusout', '#peUsername', function (e) {
    e.preventDefault();
    if (isSelectedUsername == false && $('#IsHaveAccount').val().toLowerCase() === 'false') {
        $.get('/Account/CheckUsername?username=' + $(this).val(), function (data) {
            if (data.success == false) {
                $('#peUsername').val('').focus();
                alert(data.message);
            }
        });
    }
});

$('#editPersonForm').submit(function () {
    var password = $('#pePassword').val();
    var confirmPassword = $('#peConfirmPassword').val();

    if ($('#peUsername').val() != '' && $('#IsHaveAccount').val().toLowerCase() === 'false' && $('#peIsMapAccount').val().toLowerCase() === 'false' && password.length < 8) {
        alert('Mật khẩu phải có ít nhất 8 ký tự');
        return false;
    }

    if (password !== '' && confirmPassword === '') {
        $('#peConfirmPassword').focus();
        alert('Vui xác nhận lại mật khẩu');
        return false;
    } else if (confirmPassword !== '' && password != confirmPassword) {
        $('#pePassword').focus();
        alert('Mật khẩu chưa trùng khớp');
        return false;
    }
});

$(document).on('click', '#remove-account', function (e) {
    e.preventDefault();
    $(this).hide();
    $('#peUserId').val('');
    $('#peUsername').val('').focus();
    $('#peUsername').removeAttr('readonly');
    $('#pePassword').val('');
    $('#peConfirmPassword').val('');
    $('.row-password').show();
    $('.row-select-username').show();
    $('#IsHaveAccount').val('false');
});

function SwitchCustomerSource(type) {
    $('#CustomerSourceName').val('');
    if (type == 1) { // nhan vien
        $('.cussrc-display-name-container').show();
        $('#btn-chooseCustomerSource').show();
        $('#CustomerSourceName').show();
        $('#CustomerSourceName').attr('required', 'required');
        $('#CustomerSourceEmpId').val('');
        
        $('#AgencyId').val('');
        $('#CustomerSourceCusId').val('');
        $('#SocialTypeId').val('');
        $('#SocialTypeId').removeAttr('required');
    } else if (type == 2) { // dai ly
        $('.cussrc-display-name-container').show();
        $('#btn-chooseCustomerSource').show();
        $('#CustomerSourceName').show();
        $('#CustomerSourceName').attr('required', 'required');
        $('#AgencyId').val('');
        
        $('#CustomerSourceEmpId').val('');
        $('#CustomerSourceCusId').val('');
        $('#SocialTypeId').val('');
        $('#SocialTypeId').removeAttr('required');
    } else if (type == 3) { // khach hang gioi thieu
        $('.cussrc-display-name-container').show();
        $('#btn-chooseCustomerSource').show();
        $('#CustomerSourceName').show();
        $('#CustomerSourceName').attr('required', 'required');
        $('#CustomerSourceCusId').val('');
        
        $('#AgencyId').val('');
        $('#CustomerSourceEmpId').val('');
        $('#SocialTypeId').val('');
        $('#SocialTypeId').removeAttr('required');
    } else if (type == 5) { // mang xa hoi
        $('.cussrc-display-name-container').show();
        $('#btn-chooseCustomerSource').hide();
        $('#SocialTypeId').show();
        $('#SocialTypeId').val('');
        $('#SocialTypeId').attr('required', 'required');

        $('#CustomerSourceName').hide();
        $('#CustomerSourceName').removeAttr('required');
        $('#CustomerSourceCusId').val('');
        $('#AgencyId').val('');
        $('#CustomerSourceEmpId').val('');
    } else {
        $('.cussrc-display-name-container').hide();
        $('#btn-chooseCustomerSource').hide();
        $('#CustomerSourceName').hide();
        $('#CustomerSourceName').removeAttr('required');
        $('#CustomerSourceEmpId').val('');
        $('#AgencyId').val('');
        $('#CustomerSourceCusId').val('');
        $('#SocialTypeId').hide();
        $('#SocialTypeId').val('');
        $('#SocialTypeId').removeAttr('required');
    }
}