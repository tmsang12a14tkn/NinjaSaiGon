using DinkToPdf;
using DinkToPdf.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NinjaSaiGon.Admin.Services
{
    public class PdfService : IPdfService
    {
        private IConverter _converter;

        public PdfService(IConverter converter)
        {
            _converter = converter;
        }

        public byte[] GeneratePdfFromHtml(List<string> htmlBody)
        {
            if (htmlBody == null)
            {
                throw new ApplicationException("Wrong Order!");
            }
            
            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    ColorMode = ColorMode.Grayscale,
                    Orientation = Orientation.Landscape,
                    PaperSize = new PechkinPaperSize("33mm", "45mm"),
                    Margins = new MarginSettings() { Top = 1, Left = 0, Right = 0, Bottom = 0 }
                },
                Objects = {
                    new ObjectSettings()
                    {
                        PagesCount = true,
                        WebSettings = { DefaultEncoding = "utf-8" },
                        HtmlContent = string.Join("", htmlBody)
                    }
                }
            };

            try
            {
                var pdfBytes = _converter.Convert(pdf);
                return pdfBytes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
