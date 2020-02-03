using NinjaSaiGon.Admin.Models.OrderModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace NinjaSaiGon.Admin.Helpers
{
    public static class PrintHelper
    {
        public static bool PrintBillForCustomer(OrderView orderView, string logoPath, string qrPath, CultureInfo cul, int maxLength = 56)
        {
            // Enters 'Transaction' mode.
            BXLAPI.TransactionStart();

            BXLAPI.InitializePrinter();
            BXLAPI.SetCharacterSet(BXLAPI.BXL_CS_UTF8);
            BXLAPI.SetInterChrSet(BXLAPI.BXL_ICS_LATIN);

            //Render base info + shipping info
            BXLAPI.PrintBitmap(logoPath, BXLAPI.BXL_WIDTH_NONE, BXLAPI.BXL_ALIGNMENT_CENTER, 50, true);
            BXLAPI.PrintText(" \n", BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_FONTC, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT);
            BXLAPI.PrintTextW("NINJA SAIGON\n", BXLAPI.BXL_ALIGNMENT_CENTER, BXLAPI.BXL_FT_FONTC, BXLAPI.BXL_TS_1WIDTH | BXLAPI.BXL_TS_1HEIGHT, BXLAPI.BXL_CS_VISCII);
            BXLAPI.PrintTextW("ninjasaigon.com\n", BXLAPI.BXL_ALIGNMENT_CENTER, BXLAPI.BXL_FT_FONTB, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);
            BXLAPI.PrintTextW("Điện thoại: 0909063366\n", BXLAPI.BXL_ALIGNMENT_CENTER, BXLAPI.BXL_FT_FONTB, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);
            BXLAPI.PrintTextW("Phiếu Thanh Toán\n", BXLAPI.BXL_ALIGNMENT_CENTER, BXLAPI.BXL_FT_FONTC, BXLAPI.BXL_TS_1WIDTH | BXLAPI.BXL_TS_1HEIGHT, BXLAPI.BXL_CS_VISCII);
            BXLAPI.PrintTextW(orderView.Code + "\n", BXLAPI.BXL_ALIGNMENT_CENTER, BXLAPI.BXL_FT_FONTB, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);
            BXLAPI.PrintTextW("Khách hàng:   " + orderView.FullName + "\n", BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_FONTB, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);
            BXLAPI.PrintTextW("Điện thoại:   " + orderView.PhoneNumber + "\n", BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_FONTB, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);
            if (orderView.AddressLine.Length <= 42)
            {
                BXLAPI.PrintTextW("Địa chỉ:      " + orderView.AddressLine + "\n", BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_FONTB, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);
            }
            else
            {
                var listAddressLine = WrapText(orderView.AddressLine, 42);
                for (int i = 0; i < listAddressLine.Count; i++)
                {
                    var subAddressLine = listAddressLine[i];
                    if (i == 0)
                    {
                        var text = "Địa chỉ:      " + subAddressLine + "\n";
                        BXLAPI.PrintTextW(text, BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_FONTB, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);
                    }
                    else
                    {
                        var text = new string(' ', 14) + subAddressLine + "\n";
                        BXLAPI.PrintTextW(text, BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_FONTB, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);
                    }
                }
            }

            if (!orderView.IsDeliveryNow)
                BXLAPI.PrintTextW("Thời gian giao: " + orderView.OrderDeliveried.ToString("HH:mm dd/MM/yyyy") + "\n", BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_FONTB, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);

            if (!string.IsNullOrEmpty(orderView.CustomerNote))
            {
                if (orderView.AddressLine.Length <= 42)
                {
                    BXLAPI.PrintTextW("Ghi chú:      " + orderView.CustomerNote + "\n", BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_FONTB, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);
                }
                else
                {
                    var listCustomerNote = WrapText(orderView.CustomerNote, 42);
                    for (int i = 0; i < listCustomerNote.Count; i++)
                    {
                        var subCustomerNote = listCustomerNote[i];
                        if (i == 0)
                        {
                            var text = "Ghi chú:      " + subCustomerNote + "\n";
                            BXLAPI.PrintTextW(text, BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_FONTB, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);
                        }
                        else
                        {
                            var text = new string(' ', 14) + subCustomerNote + "\n";
                            BXLAPI.PrintTextW(text, BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_FONTB, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);
                        }
                    }
                }
            }

            // Render table header
            BXLAPI.PrintText(new string(' ', 56) + "\n", BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_FONTB | BXLAPI.BXL_FT_BOLD | BXLAPI.BXL_FT_UNDERLINE, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT);
            BXLAPI.PrintTextW("   Tên món                  SL      Đ.Giá        T.Tiền\n", BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_FONTB | BXLAPI.BXL_FT_BOLD, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);
            BXLAPI.PrintTextW("                                                  (VNĐ) \n", BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_FONTC | BXLAPI.BXL_FT_BOLD | BXLAPI.BXL_FT_UNDERLINE, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);

            for (int od = 0; od < orderView.OrderDetails.Count; od++)
            {
                // Render drink name / quantity / price / fullprice
                var detail = orderView.OrderDetails[od];
                var stt = od + 1 + ".";

                var amount = detail.Amount.ToString("#,###", cul.NumberFormat);
                var amountSpace = amount.Length == 6 ? 4 : 3; //max 7 chars for prices

                var fullPrice = detail.FullPrice.ToString("#,###", cul.NumberFormat);
                var fullPriceSpace = fullPrice.Length == 6 ? 4 : 3; //max 7 chars for prices

                var quantity = detail.Quantity.ToString();
                var quantitySpace = quantity.Length == 1 ? 3 : 2; //max 2 chars for quantity

                var drinkName = detail.DrinkName;
                var numbs = new string(' ', quantitySpace) + quantity + new string(' ', fullPriceSpace) + fullPrice + new string(' ', amountSpace) + amount;
                if (drinkName.Length <= 15)
                {
                    var text = stt + drinkName + new string(' ', 42 - numbs.Length - drinkName.Length - stt.Length) + numbs + "\n";
                    BXLAPI.PrintTextW(text, BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_BOLD, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);
                }
                else
                {
                    // Wrap text to prevent break line at wrong chars
                    var listDrinkName = WrapText(drinkName);
                    for (int i = 0; i < listDrinkName.Count; i++)
                    {
                        var subName = listDrinkName[i];
                        if (i == 0)
                        {
                            var text = stt + subName + new string(' ', 42 - numbs.Length - subName.Length - stt.Length) + numbs + "\n";
                            BXLAPI.PrintTextW(text, BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_BOLD, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);
                        }
                        else
                        {
                            var text = subName + "\n";
                            BXLAPI.PrintTextW(text, BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_BOLD, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);
                        }
                    }
                }

                // Render Size / Ice / Sugar options
                var sizeQuantity = quantity + "x" + quantity;
                var sizeQuantitySpace = sizeQuantity.Length == 3 ? 16 : sizeQuantity.Length == 4 ? 15 : 14;

                var sizePrice = detail.Price.ToString("#,###", cul.NumberFormat);
                var sizePriceSpace = sizePrice.Length == 6 ? 7 : 6;

                BXLAPI.PrintTextW(" + Size: " + detail.DrinkSize + new string(' ', sizeQuantitySpace) + sizeQuantity + new string(' ', sizePriceSpace) + sizePrice + "\n", BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_FONTB, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);
                BXLAPI.PrintTextW(" + Đá: " + detail.IceOption + "\n", BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_FONTB, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);
                BXLAPI.PrintTextW(" + Đường: " + detail.SugarOption + "\n", BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_FONTB, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);

                // Render toppings
                foreach (var topping in detail.OrderDetailToppings)
                {
                    var toppingName = topping.ToppingName;
                    var toppingQuantity = topping.Quantity + "x" + quantity;
                    var toppingQuantitySpace = toppingQuantity.Length == 3 ? 26 - toppingName.Length - 3 : toppingQuantity.Length == 4 ? 25 - toppingName.Length - 3 : 24 - toppingName.Length - 3;

                    var toppingPrice = topping.Price.ToString("#,##0", cul.NumberFormat);
                    var toppingPriceSpace = toppingPrice.Length == 5 ? 8 : toppingPrice.Length == 6 ? 7 : 6;

                    if (toppingName.Length <= 22)
                    {
                        var text = " + " + toppingName + new string(' ', toppingQuantitySpace) + toppingQuantity + new string(' ', toppingPriceSpace) + toppingPrice + "\n";
                        BXLAPI.PrintTextW(text, BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_FONTB, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);
                    }
                    else
                    {
                        var listToppingName = WrapText(toppingName, 22);
                        for (int i = 0; i < listToppingName.Count; i++)
                        {
                            var subName = listToppingName[i];
                            if (i == 0)
                            {
                                toppingQuantitySpace = toppingQuantity.Length == 3 ? 26 - subName.Length - 3 : toppingQuantity.Length == 4 ? 25 - subName.Length - 3 : 25 - subName.Length - 3;
                                var text = " + " + subName + new string(' ', toppingQuantitySpace) + toppingQuantity + new string(' ', toppingPriceSpace) + toppingPrice + "\n";
                                BXLAPI.PrintTextW(text, BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_FONTB, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);
                            }
                            else
                            {
                                var text = subName + "\n";
                                BXLAPI.PrintTextW(text, BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_FONTB, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);
                            }
                        }
                    }
                }
            }

            BXLAPI.PrintText(new string(' ', 56) + "\n", BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_FONTB | BXLAPI.BXL_FT_UNDERLINE, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT);

            // Render summaries
            var totalQuantity = orderView.OrderDetails.Sum(od => od.Quantity).ToString();
            var totalQuantitySpace = totalQuantity.Length == 1 ? 20 : 19;

            var total = orderView.OrderDetails.Sum(d => d.Amount).ToString("#,###", cul.NumberFormat);
            var totalSpace = maxLength - total.Length - totalQuantity.Length - totalQuantitySpace - 9;

            var discount = orderView.DiscountAmount.ToString("#,##0", cul.NumberFormat);
            var discountSpace = maxLength - discount.Length - 9;

            var shipFee = orderView.ShipFee.ToString("#,##0", cul.NumberFormat);
            var shipFeeSpace = maxLength - shipFee.Length - 13;

            var orderTotal = orderView.OrderTotal.ToString("#,##0", cul.NumberFormat);
            var orderTotalSpace = 42 - orderTotal.Length - 9;

            BXLAPI.PrintTextW("Tổng cộng" + new string(' ', totalQuantitySpace) + totalQuantity + new string(' ', totalSpace) + total + "\n", BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_FONTB, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);
            BXLAPI.PrintTextW("Giảm giá" + new string(' ', discountSpace) + "-" + discount + "\n", BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_FONTB, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);
            BXLAPI.PrintTextW("Phí giao hàng" + new string(' ', shipFeeSpace) + shipFee + "\n", BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_FONTB, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);

            BXLAPI.PrintText(new string(' ', 56) + "\n", BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_FONTB | BXLAPI.BXL_FT_UNDERLINE, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT);

            BXLAPI.PrintTextW("Tổng tiền" + new string(' ', orderTotalSpace) + orderTotal + "\n", BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_BOLD, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);

            if (!string.IsNullOrEmpty(orderView.PromotionName))
            {
                BXLAPI.PrintText(new string(' ', 56) + "\n", BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_FONTB | BXLAPI.BXL_FT_BOLD | BXLAPI.BXL_FT_UNDERLINE, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT);

                BXLAPI.PrintTextW("Chương trình: " + orderView.PromotionName + "\n", BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_FONTC, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);
            }

            BXLAPI.PrintText(new string(' ', 56) + "\n", BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_FONTB | BXLAPI.BXL_FT_UNDERLINE, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT);

            BXLAPI.PrintTextW("Ninja Saigon chúc quý khách ngon miệng!\n", BXLAPI.BXL_ALIGNMENT_CENTER, BXLAPI.BXL_FT_FONTC, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);

            BXLAPI.PrintText(new string(' ', 56) + "\n", BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_FONTB | BXLAPI.BXL_FT_UNDERLINE, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT);

            //BXLAPI.PrintTextW("Like & Share: facebook.com/NinjaSaigon\n", BXLAPI.BXL_ALIGNMENT_CENTER, BXLAPI.BXL_FT_DEFAULT, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);
            BXLAPI.PrintBitmap(qrPath, BXLAPI.BXL_WIDTH_NONE, BXLAPI.BXL_ALIGNMENT_CENTER, 50, true);

            BXLAPI.OpenDrawer(BXLAPI.BXL_CASHDRAWER_PIN2);
            BXLAPI.CutPaper();

            // Leaves 'Transaction' mode, and then sends print data in the buffer to start printing.
            if (BXLAPI.TransactionEnd(true, 3000 /* 3 seconds */) != BXLAPI.BXL_SUCCESS)
            {
                // failed to read a response from the printer after sending the print-data.
                return false;
            }

            return true;
        }

        public static bool PrintBillForSelf(OrderView orderView, string logoPath, string qrPath, CultureInfo cul, int maxLength = 56)
        {
            // Enters 'Transaction' mode.
            BXLAPI.TransactionStart();

            BXLAPI.InitializePrinter();
            BXLAPI.SetCharacterSet(BXLAPI.BXL_CS_UTF8);
            BXLAPI.SetInterChrSet(BXLAPI.BXL_ICS_LATIN);

            BXLAPI.PrintTextW("BILL NỘI BỘ\n\n", BXLAPI.BXL_ALIGNMENT_CENTER, BXLAPI.BXL_FT_BOLD, BXLAPI.BXL_TS_1WIDTH | BXLAPI.BXL_TS_1HEIGHT, BXLAPI.BXL_CS_VISCII);

            //Render base info + shipping info
            BXLAPI.PrintBitmap(logoPath, BXLAPI.BXL_WIDTH_NONE, BXLAPI.BXL_ALIGNMENT_CENTER, 50, true);
            BXLAPI.PrintText(" \n", BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_FONTC, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT);
            BXLAPI.PrintTextW("NINJA SAIGON\n", BXLAPI.BXL_ALIGNMENT_CENTER, BXLAPI.BXL_FT_FONTC, BXLAPI.BXL_TS_1WIDTH | BXLAPI.BXL_TS_1HEIGHT, BXLAPI.BXL_CS_VISCII);
            BXLAPI.PrintTextW("ninjasaigon.com\n", BXLAPI.BXL_ALIGNMENT_CENTER, BXLAPI.BXL_FT_FONTB, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);
            BXLAPI.PrintTextW("Điện thoại: 0909063366\n", BXLAPI.BXL_ALIGNMENT_CENTER, BXLAPI.BXL_FT_FONTB, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);
            BXLAPI.PrintTextW("Phiếu Thanh Toán\n", BXLAPI.BXL_ALIGNMENT_CENTER, BXLAPI.BXL_FT_FONTC, BXLAPI.BXL_TS_1WIDTH | BXLAPI.BXL_TS_1HEIGHT, BXLAPI.BXL_CS_VISCII);
            BXLAPI.PrintTextW(orderView.Code + "\n", BXLAPI.BXL_ALIGNMENT_CENTER, BXLAPI.BXL_FT_FONTB, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);
            BXLAPI.PrintTextW("Khách hàng:   " + orderView.FullName + "\n", BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_FONTB, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);
            BXLAPI.PrintTextW("Điện thoại:   " + orderView.PhoneNumber + "\n", BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_FONTB, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);
            if (orderView.AddressLine.Length <= 42)
            {
                BXLAPI.PrintTextW("Địa chỉ:      " + orderView.AddressLine + "\n", BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_FONTB, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);
            }
            else
            {
                var listAddressLine = WrapText(orderView.AddressLine, 42);
                for (int i = 0; i < listAddressLine.Count; i++)
                {
                    var subAddressLine = listAddressLine[i];
                    if (i == 0)
                    {
                        var text = "Địa chỉ:      " + subAddressLine + "\n";
                        BXLAPI.PrintTextW(text, BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_FONTB, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);
                    }
                    else
                    {
                        var text = new string(' ', 14) + subAddressLine + "\n";
                        BXLAPI.PrintTextW(text, BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_FONTB, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);
                    }
                }
            }

            if (!orderView.IsDeliveryNow)
                BXLAPI.PrintTextW("Thời gian giao: " + orderView.OrderDeliveried.ToString("HH:mm dd/MM/yyyy") + "\n", BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_FONTB, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);

            if (!string.IsNullOrEmpty(orderView.CustomerNote))
            {
                if (orderView.AddressLine.Length <= 42)
                {
                    BXLAPI.PrintTextW("Ghi chú:      " + orderView.CustomerNote + "\n", BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_FONTB, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);
                }
                else
                {
                    var listCustomerNote = WrapText(orderView.CustomerNote, 42);
                    for (int i = 0; i < listCustomerNote.Count; i++)
                    {
                        var subCustomerNote = listCustomerNote[i];
                        if (i == 0)
                        {
                            var text = "Ghi chú:      " + subCustomerNote + "\n";
                            BXLAPI.PrintTextW(text, BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_FONTB, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);
                        }
                        else
                        {
                            var text = new string(' ', 14) + subCustomerNote + "\n";
                            BXLAPI.PrintTextW(text, BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_FONTB, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);
                        }
                    }
                }
            }

            // Render table header
            BXLAPI.PrintText(new string(' ', 56) + "\n", BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_FONTB | BXLAPI.BXL_FT_BOLD | BXLAPI.BXL_FT_UNDERLINE, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT);
            BXLAPI.PrintTextW("   Tên món                  SL      Đ.Giá        T.Tiền\n", BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_FONTB | BXLAPI.BXL_FT_BOLD, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);
            BXLAPI.PrintTextW("                                                  (VNĐ) \n", BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_FONTC | BXLAPI.BXL_FT_BOLD | BXLAPI.BXL_FT_UNDERLINE, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);

            for (int od = 0; od < orderView.OrderDetails.Count; od++)
            {
                // Render drink name / quantity / price / fullprice
                var detail = orderView.OrderDetails[od];
                var stt = od + 1 + ".";

                var amount = detail.Amount.ToString("#,###", cul.NumberFormat);
                var amountSpace = amount.Length == 6 ? 4 : 3; //max 7 chars for prices

                var fullPrice = detail.FullPrice.ToString("#,###", cul.NumberFormat);
                var fullPriceSpace = fullPrice.Length == 6 ? 4 : 3; //max 7 chars for prices

                var quantity = detail.Quantity.ToString();
                var quantitySpace = quantity.Length == 1 ? 3 : 2; //max 2 chars for quantity

                var drinkName = detail.DrinkName;
                var numbs = new string(' ', quantitySpace) + quantity + new string(' ', fullPriceSpace) + fullPrice + new string(' ', amountSpace) + amount;
                if (drinkName.Length <= 15)
                {
                    var text = stt + drinkName + new string(' ', 42 - numbs.Length - drinkName.Length - stt.Length) + numbs + "\n";
                    BXLAPI.PrintTextW(text, BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_BOLD, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);
                }
                else
                {
                    // Wrap text to prevent break line at wrong chars
                    var listDrinkName = WrapText(drinkName);
                    for (int i = 0; i < listDrinkName.Count; i++)
                    {
                        var subName = listDrinkName[i];
                        if (i == 0)
                        {
                            var text = stt + subName + new string(' ', 42 - numbs.Length - subName.Length - stt.Length) + numbs + "\n";
                            BXLAPI.PrintTextW(text, BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_BOLD, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);
                        }
                        else
                        {
                            var text = subName + "\n";
                            BXLAPI.PrintTextW(text, BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_BOLD, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);
                        }
                    }
                }

                // Render Size / Ice / Sugar options
                var sizeQuantity = quantity + "x" + quantity;
                var sizeQuantitySpace = sizeQuantity.Length == 3 ? 16 : sizeQuantity.Length == 4 ? 15 : 14;

                var sizePrice = detail.Price.ToString("#,###", cul.NumberFormat);
                var sizePriceSpace = sizePrice.Length == 6 ? 7 : 6;

                if (!string.IsNullOrEmpty(detail.Note))
                {
                    var listDrinkNote = WrapText(detail.Note, 22);
                    for (int i = 0; i < listDrinkNote.Count; i++)
                    {
                        var subNote = listDrinkNote[i];
                        if (i == 0)
                        {
                            var text = " + " + subNote + "\n";
                            BXLAPI.PrintTextW(text, BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_FONTB, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);
                        }
                        else
                        {
                            var text = subNote + "\n";
                            BXLAPI.PrintTextW(text, BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_FONTB, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);
                        }
                    }
                }

                BXLAPI.PrintTextW(" + Size: " + detail.DrinkSize + new string(' ', sizeQuantitySpace) + sizeQuantity + new string(' ', sizePriceSpace) + sizePrice + "\n", BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_FONTB, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);
                BXLAPI.PrintTextW(" + Đá: " + detail.IceOption + "\n", BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_FONTB, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);
                BXLAPI.PrintTextW(" + Đường: " + detail.SugarOption + "\n", BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_FONTB, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);

                // Render toppings
                foreach (var topping in detail.OrderDetailToppings)
                {
                    var toppingName = topping.ToppingName;
                    var toppingQuantity = topping.Quantity + "x" + quantity;
                    var toppingQuantitySpace = toppingQuantity.Length == 3 ? 26 - toppingName.Length - 3 : toppingQuantity.Length == 4 ? 25 - toppingName.Length - 3 : 24 - toppingName.Length - 3;

                    var toppingPrice = topping.Price.ToString("#,##0", cul.NumberFormat);
                    var toppingPriceSpace = toppingPrice.Length == 5 ? 8 : toppingPrice.Length == 6 ? 7 : 6;

                    if (toppingName.Length <= 22)
                    {
                        var text = " + " + toppingName + new string(' ', toppingQuantitySpace) + toppingQuantity + new string(' ', toppingPriceSpace) + toppingPrice + "\n";
                        BXLAPI.PrintTextW(text, BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_FONTB, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);
                    }
                    else
                    {
                        var listToppingName = WrapText(toppingName, 22);
                        for (int i = 0; i < listToppingName.Count; i++)
                        {
                            var subName = listToppingName[i];
                            if (i == 0)
                            {
                                toppingQuantitySpace = toppingQuantity.Length == 3 ? 26 - subName.Length - 3 : toppingQuantity.Length == 4 ? 25 - subName.Length - 3 : 25 - subName.Length - 3;
                                var text = " + " + subName + new string(' ', toppingQuantitySpace) + toppingQuantity + new string(' ', toppingPriceSpace) + toppingPrice + "\n";
                                BXLAPI.PrintTextW(text, BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_FONTB, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);
                            }
                            else
                            {
                                var text = subName + "\n";
                                BXLAPI.PrintTextW(text, BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_FONTB, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);
                            }
                        }
                    }
                }
            }

            BXLAPI.PrintText(new string(' ', 56) + "\n", BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_FONTB | BXLAPI.BXL_FT_UNDERLINE, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT);

            // Render summaries
            var totalQuantity = orderView.OrderDetails.Sum(od => od.Quantity).ToString();
            var totalQuantitySpace = totalQuantity.Length == 1 ? 20 : 19;

            var total = orderView.OrderDetails.Sum(d => d.Amount).ToString("#,###", cul.NumberFormat);
            var totalSpace = maxLength - total.Length - totalQuantity.Length - totalQuantitySpace - 9;

            var discount = orderView.DiscountAmount.ToString("#,##0", cul.NumberFormat);
            var discountSpace = maxLength - discount.Length - 9;

            var shipFee = orderView.ShipFee.ToString("#,##0", cul.NumberFormat);
            var shipFeeSpace = maxLength - shipFee.Length - 13;

            var orderTotal = orderView.OrderTotal.ToString("#,##0", cul.NumberFormat);
            var orderTotalSpace = 42 - orderTotal.Length - 9;

            BXLAPI.PrintTextW("Tổng cộng" + new string(' ', totalQuantitySpace) + totalQuantity + new string(' ', totalSpace) + total + "\n", BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_FONTB, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);
            BXLAPI.PrintTextW("Giảm giá" + new string(' ', discountSpace) + "-" + discount + "\n", BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_FONTB, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);
            BXLAPI.PrintTextW("Phí giao hàng" + new string(' ', shipFeeSpace) + shipFee + "\n", BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_FONTB, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);

            BXLAPI.PrintText(new string(' ', 56) + "\n", BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_FONTB | BXLAPI.BXL_FT_UNDERLINE, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT);

            BXLAPI.PrintTextW("Tổng tiền" + new string(' ', orderTotalSpace) + orderTotal + "\n", BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_BOLD, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);
                        
            BXLAPI.OpenDrawer(BXLAPI.BXL_CASHDRAWER_PIN2);
            BXLAPI.CutPaper();

            // Leaves 'Transaction' mode, and then sends print data in the buffer to start printing.
            if (BXLAPI.TransactionEnd(true, 3000 /* 3 seconds */) != BXLAPI.BXL_SUCCESS)
            {
                // failed to read a response from the printer after sending the print-data.
                return false;
            }

            return true;
        }

        public static bool PrintBillForCook(OrderView orderView, string logoPath, string qrPath, CultureInfo cul, int maxLength = 42)
        {
            var nowDate = DateTime.Now.ToString("dd-MM-yyyy");
            var nowTime = DateTime.Now.ToString("HH:mm");

            // Enters 'Transaction' mode.
            BXLAPI.TransactionStart();

            BXLAPI.InitializePrinter();
            BXLAPI.SetCharacterSet(BXLAPI.BXL_CS_UTF8);
            BXLAPI.SetInterChrSet(BXLAPI.BXL_ICS_LATIN);

            BXLAPI.PrintTextW(" \n\n", BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_BOLD, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);
            BXLAPI.PrintTextW("Mang về" + new string(' ', maxLength - 10) + "BẾP\n", BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_BOLD, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);
            BXLAPI.PrintTextW(orderView.Code + new string(' ', maxLength - orderView.Code.Length - 20) + "Thời gian đặt: " + orderView.OrderPlaced.ToString("HH:mm") + "\n", BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_BOLD, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);
            BXLAPI.PrintTextW("Phục vụ: Ninja Saigon" + new string(' ', maxLength - nowDate.Length - 21) + nowDate + "\n", BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_DEFAULT, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);
            BXLAPI.PrintTextW("Tên khách: " + orderView.FullName + new string(' ', maxLength - orderView.FullName.Length - nowTime.Length - 15) + "In: " + nowTime + "\n", BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_DEFAULT, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);

            BXLAPI.PrintTextW(" \n", BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_BOLD, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);
            if (!orderView.IsDeliveryNow)
            {
                BXLAPI.PrintTextW("HOÀN THÀNH TRƯỚC" + "\n", BXLAPI.BXL_ALIGNMENT_CENTER, BXLAPI.BXL_FT_UNDERTHICK | BXLAPI.BXL_FT_UNDERLINE, BXLAPI.BXL_TS_1WIDTH | BXLAPI.BXL_TS_1HEIGHT, BXLAPI.BXL_CS_VISCII);
                BXLAPI.PrintTextW(orderView.OrderDeliveried.AddMinutes(-30).ToString("HH:mm") + "\n", BXLAPI.BXL_ALIGNMENT_CENTER, BXLAPI.BXL_FT_UNDERTHICK | BXLAPI.BXL_FT_UNDERLINE, BXLAPI.BXL_TS_1WIDTH | BXLAPI.BXL_TS_1HEIGHT, BXLAPI.BXL_CS_VISCII);
            }
            BXLAPI.PrintTextW(" \n", BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_BOLD, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);

            if (!string.IsNullOrEmpty(orderView.CustomerNote))
                BXLAPI.PrintTextW("Ghi chú:    " + orderView.CustomerNote + "\n", BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_DEFAULT, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);

            // Render table header
            BXLAPI.PrintText(new string(' ', 56) + "\n", BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_FONTB | BXLAPI.BXL_FT_BOLD | BXLAPI.BXL_FT_UNDERLINE, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT);
            BXLAPI.PrintTextW("   Tên món                             SL \n", BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_BOLD, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);
            BXLAPI.PrintTextW(new string(' ', 56) + "\n", BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_FONTB | BXLAPI.BXL_FT_BOLD | BXLAPI.BXL_FT_UNDERLINE, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);

            var totalQuantity = orderView.OrderDetails.Sum(od => od.Quantity).ToString();
            BXLAPI.PrintTextW(new string(' ', maxLength - 16 - totalQuantity.Length) + "Tổng số ly    " + totalQuantity + "\n", BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_BOLD, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);
            BXLAPI.PrintTextW("                                          \n", BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_BOLD | BXLAPI.BXL_FT_UNDERLINE, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);

            for (int od = 0; od < orderView.OrderDetails.Count; od++)
            {
                // Render drink name / quantity
                var detail = orderView.OrderDetails[od];
                var stt = od + 1 + ".";

                var quantity = detail.Quantity.ToString();
                var quantitySpace = quantity.Length == 1 ? 6 : 5;

                var drinkName = detail.DrinkName;
                var numbs = new string(' ', quantitySpace) + quantity;
                if (drinkName.Length <= 27)
                {
                    var text = stt + drinkName + new string(' ', maxLength - 2 - numbs.Length - drinkName.Length - stt.Length) + numbs + "\n";
                    BXLAPI.PrintTextW(text, BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_BOLD, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);
                }
                else
                {
                    // Wrap text to prevent break line at wrong chars
                    var listDrinkName = WrapText(drinkName, 27);
                    for (int i = 0; i < listDrinkName.Count; i++)
                    {
                        var subName = listDrinkName[i];
                        if (i == 0)
                        {
                            var text = stt + subName + new string(' ', maxLength - numbs.Length - subName.Length - stt.Length - 2) + numbs + "\n";
                            BXLAPI.PrintTextW(text, BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_BOLD, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);
                        }
                        else
                        {
                            var text = subName + "\n";
                            BXLAPI.PrintTextW(text, BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_BOLD, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);
                        }
                    }
                }

                // Render Size / Ice / Sugar options
                var sizeQuantity = quantity + "x" + quantity;
                if (!string.IsNullOrEmpty(detail.Note))
                {
                    var listDrinkNote = WrapText(detail.Note, 27);
                    for (int i = 0; i < listDrinkNote.Count; i++)
                    {
                        var subNote = listDrinkNote[i];
                        if (i == 0)
                        {
                            var text = " + " + subNote + "\n";
                            BXLAPI.PrintTextW(text, BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_DEFAULT, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);
                        }
                        else
                        {
                            var text = subNote + "\n";
                            BXLAPI.PrintTextW(text, BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_DEFAULT, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);
                        }
                    }
                }

                BXLAPI.PrintTextW(" + Size: " + detail.DrinkSize + new string(' ', maxLength - detail.DrinkSize.Length - sizeQuantity.Length - 15) + sizeQuantity + "\n", BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_DEFAULT, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);
                BXLAPI.PrintTextW(" + Đá: " + detail.IceOption + "\n", BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_DEFAULT, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);
                BXLAPI.PrintTextW(" + Đường: " + detail.SugarOption + "\n", BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_DEFAULT, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);

                // Render toppings
                foreach (var topping in detail.OrderDetailToppings)
                {
                    var toppingName = topping.ToppingName;
                    var toppingQuantity = topping.Quantity + "x" + quantity;

                    if (toppingName.Length <= 26)
                    {
                        var text = " + " + toppingName + new string(' ', maxLength - toppingName.Length - toppingQuantity.Length - 9) + toppingQuantity + "\n";
                        BXLAPI.PrintTextW(text, BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_DEFAULT, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);
                    }
                    else
                    {
                        var listToppingName = WrapText(toppingName, 14);
                        for (int i = 0; i < listToppingName.Count; i++)
                        {
                            var subName = listToppingName[i];
                            if (i == 0)
                            {
                                var text = " + " + subName + new string(' ', maxLength - toppingName.Length - toppingQuantity.Length - 9) + toppingQuantity + "\n";
                                BXLAPI.PrintTextW(text, BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_DEFAULT, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);
                            }
                            else
                            {
                                var text = subName + "\n";
                                BXLAPI.PrintTextW(text, BXLAPI.BXL_ALIGNMENT_LEFT, BXLAPI.BXL_FT_DEFAULT, BXLAPI.BXL_TS_0WIDTH | BXLAPI.BXL_TS_0HEIGHT, BXLAPI.BXL_CS_VISCII);
                            }
                        }
                    }
                }
            }

            BXLAPI.OpenDrawer(BXLAPI.BXL_CASHDRAWER_PIN2);
            BXLAPI.CutPaper();

            // Leaves 'Transaction' mode, and then sends print data in the buffer to start printing.
            if (BXLAPI.TransactionEnd(true, 3000 /* 3 seconds */) != BXLAPI.BXL_SUCCESS)
            {
                // failed to read a response from the printer after sending the print-data.
                return false;
            }

            return true;
        }

        private static List<string> WrapText(string text, int partLength = 15)
        {
            string[] words = text.Split(' ');
            var parts = new List<string>();
            string part = string.Empty;

            foreach (var word in words)
            {
                if (part.Length + word.Length < partLength)
                {
                    part += word + " ";
                }
                else
                {
                    parts.Add(part);
                    part = word + " ";
                }
            }
            parts.Add(part);

            return parts;
        }
    }
}
