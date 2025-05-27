using ServicioImpresion_Consola;
using System.Drawing;
using System.Drawing.Printing;

public class ImpresoraHelper
{
    private ImpresionData _datos;
    private string _nombreImpresora;

    public ImpresoraHelper(ImpresionData datos, string nombreImpresora)
    {
        _datos = datos;
        _nombreImpresora = nombreImpresora;
    }

    public void Imprimir()
    {
        PrintDocument doc = new PrintDocument();
        if (!string.IsNullOrEmpty(_nombreImpresora))
        {
            doc.PrinterSettings.PrinterName = _nombreImpresora;
        }
        doc.PrintPage += new PrintPageEventHandler(Print);
        doc.Print();
    }

    private void Print(object sender, PrintPageEventArgs e)
    {
        Font font = new Font("Consolas", 10);
        float y = -4;
        float salto = 18;
        string titulo = "AconcaguaFoods S.A.";

        float anchoArea = e.MarginBounds.Width;
        SizeF sizeTitulo = e.Graphics.MeasureString(titulo, font);
        float xTitulo = e.MarginBounds.Left + (anchoArea - sizeTitulo.Width) / 2;
        e.Graphics.DrawString(titulo, new Font("Consolas", 10, FontStyle.Bold), Brushes.Black, xTitulo, y);
        y += salto * 2;

        string fechaHora = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
        e.Graphics.DrawString($"Fecha: {fechaHora}", font, Brushes.Black, 5, y);
        y += salto;

        e.Graphics.DrawString($"N° Pedido: {_datos.NumeroPedido}", new Font("Consolas", 10, FontStyle.Bold), Brushes.Black, 5, y);
        y += salto;

        string tipo = _datos.NumeroPedido.StartsWith("B") ? "Detalle Venta" : "FACTURA";
        e.Graphics.DrawString($"Tipo: {tipo}", font, Brushes.Black, 5, y);
        y += salto * 2;

        // Cabeceras columnas
        e.Graphics.DrawString("Material", font, Brushes.Black, 5, y);
        e.Graphics.DrawString("Cant.", font, Brushes.Black, 90, y);
        e.Graphics.DrawString("V.Unit.", font, Brushes.Black, 140, y);
        e.Graphics.DrawString("Total", font, Brushes.Black, 210, y);
        y += salto;

        for (int i = 0; i < _datos.Materiales.Count; i++)
        {
            string mat = _datos.Materiales[i];
            int cant = _datos.Cantidades[i];
            decimal valUnit = _datos.ValorUnitario[i];
            decimal totalItem = cant * valUnit;

            string cantidadFormateada = cant.ToString("N0");
            string valUnitFormateado = valUnit.ToString("N0");
            string totalItemFormateado = totalItem.ToString("N0");

            string descripcion = _datos.Descripciones[i];
            string descripcionFormateada = char.ToUpper(descripcion[0]) + descripcion.Substring(1).ToLower();
            e.Graphics.DrawString($"{descripcionFormateada}\n{mat}", font, Brushes.Black, 5, y);
            y += salto * 1;
            e.Graphics.DrawString(cantidadFormateada, font, Brushes.Black, 100, y);
            e.Graphics.DrawString("$"+valUnitFormateado, font, Brushes.Black, 130, y);
            e.Graphics.DrawString("$"+totalItemFormateado, font, Brushes.Black, 200, y);
            y += salto;
        }

        y += salto;
        e.Graphics.DrawString("--------------------------", font, Brushes.Black, 5, y);
        y += salto;
        e.Graphics.DrawString($"TOTAL: ${_datos.Total.ToString("N0")}", new Font("Consolas", 10, FontStyle.Bold), Brushes.Black, 5, y);
        y += salto * 2;
        e.Graphics.DrawString("¡Gracias por tu compra!", font, Brushes.Black, xTitulo, y);
    }
}
