function getDataReportByWeek(from, to, page, loadAll, tableName, callBack) {
    showSpinner();
    let table = document.getElementById(tableName).getElementsByTagName('tbody')[0];
    while (table.rows.length > 0) {
        table.deleteRow(0);
    }
    $.get("/api/Report/ByWeek?from=" + from + "&to=" + to + "&page=" + page + "&loadAll=" + loadAll, function (data) {
        if (data.days.length > 0) {
            let rowTotal = table.insertRow(0);
            rowTotal.className = "rowtotal";
            let cell1 = rowTotal.insertCell(0);
            cell1.innerHTML = "Tổng";
            cell2 = rowTotal.insertCell(1);
            cell2.innerHTML = numberWithCommas(data.sumOrder);
            cell3 = rowTotal.insertCell(2);
            cell3.innerHTML = numberWithCommas(data.sumAllDrinkCount);
            cell4 = rowTotal.insertCell(3);
            cell4.innerHTML = numberWithCommas(data.sumBaseDrinkCount);
            cell5 = rowTotal.insertCell(4);
            cell5.innerHTML = numberWithCommas(data.sumDiscountDrinkCount);
            cell6 = rowTotal.insertCell(5);
            cell6.innerHTML = numberWithCommas(data.sumFreeDrinkCount);
            cell7 = rowTotal.insertCell(6);
            cell7.innerHTML = numberWithCommas(data.sumCafeCount);
            cell8 = rowTotal.insertCell(7);
            cell8.innerHTML = numberWithCommas(data.sumTeaCount);
            cell9 = rowTotal.insertCell(8);
            cell9.innerHTML = numberWithCommas(data.sumMilkTeaCount);
            cell10 = rowTotal.insertCell(9);
            cell10.innerHTML = numberWithCommas(data.sumHerbalCount);
            cell11 = rowTotal.insertCell(10);
            cell11.innerHTML = numberWithCommas(data.sumFruitCount);
            cell12 = rowTotal.insertCell(11);
            cell12.innerHTML = numberWithCommas(data.sumMacchiatoCount);
            cell13 = rowTotal.insertCell(12);
            cell13.innerHTML = numberWithCommas(data.sumSodaCount);
            cell14 = rowTotal.insertCell(13);
            cell14.innerHTML = numberWithCommas(data.sumDrinksTotal);
            cell15 = rowTotal.insertCell(14);
            cell15.innerHTML = numberWithCommas(data.sumMoneyDiscount);
            cell16 = rowTotal.insertCell(15);
            cell16.innerHTML = numberWithCommas(data.sumMoneySurcharge);
            cell17 = rowTotal.insertCell(16);
            cell17.innerHTML = numberWithCommas(data.sumShipFee);
            cell18 = rowTotal.insertCell(17);
            //cell18.innerHTML = data.partner;
            cell19 = rowTotal.insertCell(18);
            cell19.innerHTML = numberWithCommas(data.sumPartnerShipFee);
            cell20 = rowTotal.insertCell(19);
            cell20.innerHTML = numberWithCommas(data.sumNinjaDayTotal);
            cell21 = rowTotal.insertCell(20);
            cell21.innerHTML = numberWithCommas(data.sumRealDayTotal);

            data.days.forEach(function (day) {
                row = table.insertRow(table.rows.length);
                row.className = "rowgrp";
                cell1 = row.insertCell(0);
                cell1.innerHTML = '<a href="/Report/ByDay?from=' + moment(day.date).format('MM/DD/YYYY') + '">' + day.dateString + ' </a><span><i class="fa fa-chevron-down"></i></span>';
                cell2 = row.insertCell(1);
                cell2.innerHTML = day.orders.length;
                cell3 = row.insertCell(2);
                cell3.innerHTML = numberWithCommas(day.sumAllDrinkCount);
                cell4 = row.insertCell(3);
                cell4.innerHTML = numberWithCommas(day.sumBaseDrinkCount);
                cell5 = row.insertCell(4);
                cell5.innerHTML = numberWithCommas(day.sumDiscountDrinkCount);
                cell6 = row.insertCell(5);
                cell6.innerHTML = numberWithCommas(day.sumFreeDrinkCount);
                cell7 = row.insertCell(6);
                cell7.innerHTML = numberWithCommas(day.sumCafeCount);
                cell8 = row.insertCell(7);
                cell8.innerHTML = numberWithCommas(day.sumTeaCount);
                cell9 = row.insertCell(8);
                cell9.innerHTML = numberWithCommas(day.sumMilkTeaCount);
                cell10 = row.insertCell(9);
                cell10.innerHTML = numberWithCommas(day.sumHerbalCount);
                cell11 = row.insertCell(10);
                cell11.innerHTML = numberWithCommas(day.sumFruitCount);
                cell12 = row.insertCell(11);
                cell12.innerHTML = numberWithCommas(day.sumMacchiatoCount);
                cell13 = row.insertCell(12);
                cell13.innerHTML = numberWithCommas(day.sumSodaCount);
                cell14 = row.insertCell(13);
                cell14.innerHTML = numberWithCommas(day.drinksTotal);
                cell15 = row.insertCell(14);
                cell15.innerHTML = numberWithCommas(day.moneyDiscount);
                cell16 = row.insertCell(15);
                cell16.innerHTML = numberWithCommas(day.moneySurcharge);
                cell17 = row.insertCell(16);
                cell17.innerHTML = numberWithCommas(day.shipFee);
                cell18 = row.insertCell(17);
                //cell18.innerHTML = day.partner;
                cell19 = row.insertCell(18);
                cell19.innerHTML = numberWithCommas(day.partnerShipFee);
                cell20 = row.insertCell(19);
                cell20.innerHTML = numberWithCommas(day.ninjaDayTotal);
                cell21 = row.insertCell(20);
                cell21.innerHTML = numberWithCommas(day.realDayTotal);  
                
                day.orders.forEach(function (order) {
                    row = table.insertRow(table.rows.length);
                    cell1 = row.insertCell(0);
                    cell2 = row.insertCell(1);
                    cell2.innerHTML = order.orderCode;
                    cell3 = row.insertCell(2);
                    cell3.innerHTML = numberWithCommas(order.allDrinkCount);
                    cell4 = row.insertCell(3);
                    cell4.innerHTML = numberWithCommas(order.baseDrinkCount);
                    cell5 = row.insertCell(4);
                    cell5.innerHTML = numberWithCommas(order.discountDrinkCount);
                    cell6 = row.insertCell(5);
                    cell6.innerHTML = numberWithCommas(order.freeDrinkCount);
                    cell7 = row.insertCell(6);
                    cell7.innerHTML = numberWithCommas(order.cafeCount);
                    cell8 = row.insertCell(7);
                    cell8.innerHTML = numberWithCommas(order.teaCount);
                    cell9 = row.insertCell(8);
                    cell9.innerHTML = numberWithCommas(order.milkTeaCount);
                    cell10 = row.insertCell(9);
                    cell10.innerHTML = numberWithCommas(order.herbalCount);
                    cell11 = row.insertCell(10);
                    cell11.innerHTML = numberWithCommas(order.fruitCount);
                    cell12 = row.insertCell(11);
                    cell12.innerHTML = numberWithCommas(order.macchiatoCount);
                    cell13 = row.insertCell(12);
                    cell13.innerHTML = numberWithCommas(order.sodaCount);
                    cell14 = row.insertCell(13);
                    cell14.innerHTML = numberWithCommas(order.drinksTotal);
                    cell15 = row.insertCell(14);
                    cell15.innerHTML = numberWithCommas(order.moneyDiscount);
                    cell16 = row.insertCell(15);
                    cell16.innerHTML = numberWithCommas(order.moneySurcharge);
                    cell17 = row.insertCell(16);
                    cell17.innerHTML = numberWithCommas(order.shipFee);
                    cell18 = row.insertCell(17);
                    cell18.innerHTML = order.partner;
                    cell19 = row.insertCell(18);
                    cell19.innerHTML = numberWithCommas(order.partnerShipFee);
                    cell20 = row.insertCell(19);
                    cell20.innerHTML = numberWithCommas(order.ninjaOrderTotal);
                    cell21 = row.insertCell(20);
                    cell21.innerHTML = numberWithCommas(order.realOrderTotal);                    
                });
            });            
        }
        else {
            let row = table.insertRow(0);
            let cell1 = row.insertCell(0);
            cell1.innerHTML = "<div class='text-center'>Không có dữ liệu</div>";
            cell1.colSpan = 20;
        }
        if (callBack)
            callBack(data);
        hideSpinner();
    });
}

function getDataReportByProduct(from, to, tableName, isShowDrinkOnMenu) {
    showSpinner();
    let table = $(tableName).DataTable();
    table.clear().draw();

    $.get("/api/Report/ByProduct?from=" + from + "&to=" + to + "&isShowDrinkOnMenu=" + isShowDrinkOnMenu, function (data) {
        if (data.productGroups.length > 0) {
            table.row.add(['', '', 'Tổng',
                numberWithCommas(data.sumSizeSQuantity), numberWithCommas(data.sumSizeMQuantity), numberWithCommas(data.sumSizeLQuantity), numberWithCommas(data.sumTotalQuantity),
                '', numberWithCommas(data.sumSizeSAmount), numberWithCommas(data.sumSizeMAmount), numberWithCommas(data.sumSizeLAmount), numberWithCommas(data.sumTotalAmount)
            ]).draw().nodes().to$().addClass('rowtotal');

            let index = 1;
            data.productGroups.forEach(function (group) {
                table.row.add(['', '', group.categoryName,
                    numberWithCommas(group.sumSizeSQuantity), numberWithCommas(group.sumSizeMQuantity), numberWithCommas(group.sumSizeLQuantity), numberWithCommas(group.sumTotalQuantity),
                    '', numberWithCommas(group.sumSizeSAmount), numberWithCommas(group.sumSizeMAmount), numberWithCommas(group.sumSizeLAmount), numberWithCommas(group.sumTotalAmount)
                ]).draw().nodes().to$().addClass('rowgrp');
                
                group.products.forEach(function (product) {
                    table.row.add([index, product.drinkCode, product.drinkName,
                        numberWithCommas(product.sizeSQuantity), numberWithCommas(product.sizeMQuantity), numberWithCommas(product.sizeLQuantity), numberWithCommas(product.totalQuantity),
                        product.drinkUnit, numberWithCommas(product.sizeSAmount), numberWithCommas(product.sizeMAmount), numberWithCommas(product.sizeLAmount), numberWithCommas(product.totalAmount)
                    ]).draw();
                    index++;
                });
            });
        }

        hideSpinner();
    });
}

function showSpinner() {
    $("#spinner").show();
}
function hideSpinner() {
    $("#spinner").hide();
}

$(document).on('click', '.rowgrp', function () {
    $(this).nextUntil('tr.rowgrp').slideToggle(200, function () { });
    if ($(this).find('i').hasClass('fa-chevron-down')) {
        $(this).find('i').removeClass('fa-chevron-down');
        $(this).find('i').addClass('fa-chevron-up');
    } else {
        $(this).find('i').removeClass('fa-chevron-up');
        $(this).find('i').addClass('fa-chevron-down');
    }
});