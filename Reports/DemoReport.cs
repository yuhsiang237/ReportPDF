using System.IO;
using System.Collections.Generic;

using iText.IO.Font;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Kernel.Pdf.Action;

using ReportPDF.Models;

namespace ReportPDF.Reports
{
    public class DemoReport : IReport
    {
        private readonly string _filepath;
        private readonly string _fontpath;
        public DemoReport()
        {
            _filepath = "D:/repo/20230329/report.pdf";
            _fontpath = "C:/WINDOWS/FONTS/KAIU.TTF";
        }
        public void GeneratePDF()
        {
            var pdfDocument = new PdfDocument(new PdfWriter(new FileStream(_filepath, FileMode.Create, FileAccess.Write)));
            var document = new Document(pdfDocument);
            var font = PdfFontFactory.CreateFont(_fontpath, PdfEncodings.IDENTITY_H);
            document.SetFont(font);

            document.Add(new Paragraph("Report Demo 報表範例")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(20));
            document.Add(new Paragraph("This is a test report. 這是一份測試報告。")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(10));
            document.Add(new LineSeparator(new SolidLine()));

            document.Add(new Paragraph("報表簡介(Intro):")
                .SetBold()
                .SetTextAlignment(TextAlignment.LEFT));

            document.Add(new Paragraph("In order to be more familiar with PDF production, " +
                "this sample was written, and this report test was generated using C# and iText7, " +
                "and Chinese and English characters were used in this PDF, " +
                "and the text and table rendering were demonstrated.")
                .SetTextAlignment(TextAlignment.LEFT));

            document.Add(new Paragraph("為了更熟悉PDF產製，因此撰寫了這份範例，而這份報表測試使用了C#與" +
                "iText7進行生成，且在這份PDF中使用了中文與英文字，並示範了文字與表格描繪。")
              .SetTextAlignment(TextAlignment.LEFT));

            document.Add(new Paragraph());

            var table = new Table(5, true);

            table.SetFontSize(11);
            var headerProductId = new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).Add(new Paragraph("代碼(Code)"));
            var headerProduct = new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).Add(new Paragraph("產品(Product)"));
            var headerProductQty = new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).Add(new Paragraph("價格(Price)"));
            var headerProductPrice = new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).Add(new Paragraph("數量(Qty)"));
            var headerTotal = new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).Add(new Paragraph("總額(Total)"));

            table.AddCell(headerProductId);
            table.AddCell(headerProduct);
            table.AddCell(headerProductPrice);
            table.AddCell(headerProductQty);
            table.AddCell(headerTotal);

            var orderDetails = new List<OrderDetail>
            {
                new OrderDetail
                {
                    Code = "AAAAA-1",
                    Name = "產品1(Product1)",
                    Qty = 1521,
                    Price = 110,
                }
                ,
                new OrderDetail
                {
                    Code = "BBBBB-2",
                    Name = "產品2(Product2)",
                    Qty = 2330,
                    Price = 110,
                },
                new OrderDetail
                {
                    Code = "CCCCC-3",
                    Name = "產品3(Product3)",
                    Qty = 3050,
                    Price = 130,
                },
                new OrderDetail
                {
                    Code = "OTHER-1",
                    Name = "其他相關測試產品(OtherProduct1)",
                    Qty = 555,
                    Price = 150,
                }
            };

            decimal groundTotal = default;
            orderDetails.ForEach(x =>
            {
                var total = (x.Qty * x.Price);
                groundTotal += total;

                table.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).Add(new Paragraph(x.Code)));
                table.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).Add(new Paragraph(x.Name)));
                table.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).Add(new Paragraph(x.Qty.ToString())));
                table.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).Add(new Paragraph(x.Price.ToString())));
                table.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).Add(new Paragraph(total.ToString())));
            });

            var grandTotalHeader = new Cell(1, 4).SetTextAlignment(TextAlignment.RIGHT).Add(new Paragraph("總計(Total)"));
            var grandTotal = new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).Add(new Paragraph(" " + groundTotal.ToString()));

            table.AddCell(grandTotalHeader);
            table.AddCell(grandTotal);
            document.Add(table);
            table.Flush();
            table.Complete();

            var p1 = new Paragraph();
            p1.Add(new Paragraph("報表產製者(Report producer) : ").SetBold());
            p1.Add(new Paragraph("YU HSIANG"));
            document.Add(p1.SetTextAlignment(TextAlignment.RIGHT));

            var p2 = new Paragraph();
            p2.Add(new Paragraph("Github: ").SetBold());
            var link = new Link("https://github.com/yuhsiang237",
                PdfAction.CreateURI(
                    "https://github.com/yuhsiang237")).SetUnderline();
            p2.Add(link);
            document.Add(p2.SetTextAlignment(TextAlignment.RIGHT));

            document.Close();
        }
    }
}
