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

            nombreDocumento += ".pdf";
            string rutaCompleta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), nombreDocumento);

            Document.Create(document =>
            {
                document.Page(page =>
                {
                    page.Header().Row(row =>
                    {
                        row.RelativeItem().Border(1).Background(Colors.Green.Medium).Height(80).Text("Bienvenido, usuario registrado con éxito.");
                    });
                });
            }).GeneratePdf(rutaCompleta);
            return rutaCompleta;
        }

    }
}
