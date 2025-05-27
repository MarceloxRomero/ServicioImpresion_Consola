using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioImpresion_Consola
{
    public class ImpresionData
    {
        public string NumeroPedido { get; set; }    //Número de pedido o factura, por ejemplo: B87493 o 1234567890
        public string TipoDocumento { get; set; }  // Tipo de Documento
        public List<string> Materiales { get; set; }    //Material
        public List<string> Descripciones { get; set; } // Descripción del texto
        public List<int> Cantidades { get; set; }       //Cantidad de materiales
        public List<decimal> ValorUnitario { get; set; } // Valor unitaro x item
        public int Total { get; set; }  // Total de la factura o pedido
    }
}
