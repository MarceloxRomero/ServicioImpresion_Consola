using ServicioImpresion_Consola;
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using Microsoft.Extensions.Configuration;


class Program
{
    static void Main(string[] args)
    {

        var config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

        string nombreImpresora = config["Impresora:Nombre"];

        string url = "http://+:8080/imprimir/";
        HttpListener listener = new HttpListener();
        listener.Prefixes.Add(url);
        listener.Start();

        Console.WriteLine("🚀 Servidor de impresión iniciado en " + url);
        Console.WriteLine("📨 Esperando datos para imprimir...");

        while (true)
        {
            HttpListenerContext context = listener.GetContext();
            _ = Task.Run(async () => await ProcesarPeticion(context, nombreImpresora));
        }
    }

    static async Task ProcesarPeticion(HttpListenerContext context, string nombreImpresora)
    {
        try
        {
            if (context.Request.HttpMethod != "POST")
            {
                context.Response.StatusCode = 405; // Method Not Allowed
                context.Response.Close();
                return;
            }

            string body = await new StreamReader(context.Request.InputStream).ReadToEndAsync();

            // Ejemplo de cuerpo: B87493|TSK1006,TSK10005,TSK10009|12,13,14|12345
            var partes = body.Split('|');

            var datos = new ImpresionData
            {
                NumeroPedido = partes[0],
                Materiales = partes[1].Split(',').ToList(),
                Descripciones = partes[2].Split(',').ToList(),
                Cantidades = partes[3].Split(',').Select(int.Parse).ToList(),
                ValorUnitario = partes[4].Split(',').Select(decimal.Parse).ToList(),
                Total = int.Parse(partes[5])
            };

            new ImpresoraHelper(datos, nombreImpresora).Imprimir();

            // Respuesta
            byte[] buffer = Encoding.UTF8.GetBytes("🖨 Impresión enviada correctamente.");
            context.Response.ContentLength64 = buffer.Length;
            await context.Response.OutputStream.WriteAsync(buffer, 0, buffer.Length);
            context.Response.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("❌ Error: " + ex.Message);
            context.Response.StatusCode = 500;
            byte[] buffer = Encoding.UTF8.GetBytes("Error al imprimir: " + ex.Message);
            await context.Response.OutputStream.WriteAsync(buffer, 0, buffer.Length);
            context.Response.Close();
        }
    }

}
