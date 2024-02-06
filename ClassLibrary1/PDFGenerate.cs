using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;

namespace ClassLibrary1
{
    public class PDFGenerate{
        
        public string generar(string nombreDocumento)
        {
            QuestPDF.Settings.License = LicenseType.Community;
            string name = nombreDocumento;
            nombreDocumento += ".pdf";
            string rutaCompleta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), nombreDocumento);

            Document.Create(document =>
            {
                document.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(20));

                    page.Header()
                        .Text("Bienvenido " + name + ", has sido registrado con éxito.")
                        .SemiBold().FontSize(38).FontColor(Colors.Red.Medium);
                    
                    page.Content()
                        
                        .PaddingVertical(1, Unit.Centimetre)
                        .Column(x =>
                       {
                           x.Spacing(10);
                           x.Item().Text("Ignacio ponte el 10.");
                           x.Item().Text(Placeholders.LoremIpsum());
                           x.Item().Image(Placeholders.Image(150, 70));
                       });

                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Pagina ");
                            x.CurrentPageNumber();
                        });
                });
            }).GeneratePdf(rutaCompleta);
            return rutaCompleta;
        }

    }
}
