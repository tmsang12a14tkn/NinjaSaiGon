"use strict";
//var table;

var audio = new Audio('/audio/notify.mp3');

var connection = new signalR.HubConnectionBuilder().withUrl("https://order.ninjasaigon.com/notifyHub").build();

let titleScroller = function (text, timeOut, complete) {
    if (timeOut >= 0) {
        document.title = text;
        setTimeout(function () {
            titleScroller(text.substr(text.length - 2, text.length - 1) + text.substr(0, text.length - 2), timeOut - 300, complete);
        }, 300);
    }
    else
        complete();
};

let gotoWattingTab = function () {
    console.log("gotoWattingTab");
    $("#order_waiting").trigger("click");
}
let showNotify = function (name, phone, deliveriedTime) {
    toastr.options = {
        "closeButton": true,
        "debug": false,
        "newestOnTop": false,
        "progressBar": false,
        "positionClass": "toast-top-right",
        "preventDuplicates": false,
        "onclick": gotoWattingTab,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "60000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    };

    toastr.info(name + "<br/>" + phone + "<br/>" + deliveriedTime, "Đơn hàng mới");
    
    var playPromise = audio.play();
    if (playPromise !== undefined) {
        playPromise.then(function () {
            // Automatic playback started!
        }).catch(function (error) {
            // Automatic playback failed.
        });
    }
}

let reloadTable = function () {
    console.log("reload table");
    if($("#m_table_1").length > 0)
    {
        //table = $("#m_table_1").DataTable();
        if (table !== undefined && table !== null && table.ajax != null)
            table.ajax.reload(function () {
                console.log("reload completed");
            }, false);
    }

}
connection.on("receiveNewOrder", function (name, phone, deliveriedTime) {
    showNotify(name, phone, deliveriedTime);
    reloadTable();
    let title = document.title;
    titleScroller("Đơn hàng mới...", 30000, function () {
        document.title = title;
    });
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});
