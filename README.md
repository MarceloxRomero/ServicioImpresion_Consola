# 🖨️ Servicio de Impresión - Consola (.NET)

Este proyecto es una aplicación de consola desarrollada en C# (.NET 8) que permite imprimir tickets personalizados mediante una impresora térmica, leyendo la configuración desde un archivo `appsettings.json` y consumiendo datos vía HTTP POST.

## 📦 Funcionalidades

- Impresión directa con diseño personalizado (empresa, fecha/hora, total, etc.)
- Configuración flexible de la impresora desde `appsettings.json`
- Recepción de datos vía `Invoke-WebRequest` (PowerShell)
- Uso de `System.Drawing.Printing` para manejar el formato del ticket
- Formateo de valores, nombres de productos y montos
- Compatibilidad con impresoras térmicas como "THERMAL Receipt Printer"

---

## ⚙️ Estructura de entrada esperada

El contenido enviado al endpoint de impresión es un string con los siguientes campos separados por `|`:

