# 🖨️ Servicio de Impresión - Consola (.NET 8)

Este proyecto es una aplicación de consola desarrollada en C# (.NET 8) que permite imprimir tickets personalizados mediante una impresora térmica, leyendo la configuración desde un archivo `appsettings.json` y consumiendo datos de entrada en formato plano.

---

## ✅ Características

- Impresión directa con formato definido (empresa, fecha, pedido, detalle).
- Soporte para impresoras térmicas (ej: THERMAL Receipt Printer).
- Configuración de la impresora mediante `appsettings.json`.
- Recepción de datos en una única cadena de texto separada por `|`.
- Formateo y alineación de contenido impreso.
- Muestra Fecha y Hora del momento de la impresión.

---

## 🛠️ Requisitos

- .NET 8 SDK
- Impresora térmica configurada en Windows
- Acceso al archivo `appsettings.json` con el nombre correcto de la impresora

---

## ⚙️ Configuración

### 📁 Archivo `appsettings.json`

Debe colocarse en la misma carpeta que el ejecutable (por ejemplo: `bin\Debug\net8.0\`):

```json
{
  "Impresora": {
    "Nombre": "THERMAL Receipt Printer"
  }
}
