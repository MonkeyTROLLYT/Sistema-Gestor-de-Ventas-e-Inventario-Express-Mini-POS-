# Sistema-Gestor-de-Ventas-e-Inventario-Express-Mini-POS-
👤 Nombre del estudiante

[Alan Padilla Alvear]

📋 Descripción del proyecto

Aplicación de consola desarrollada en C# (.NET 8) como parte del Reto Final de la Unidad 1. Simula un punto de venta (POS) básico para una tienda de barrio, permitiendo:

Registrar productos en el inventario (nombre, precio y stock).
Consultar el inventario completo, con alertas visuales de bajo stock (menos de 5 unidades).
Registrar ventas aplicando descuento de cliente frecuente (10%) e IVA (19%), con generación de ticket detallado.
Consultar un reporte de caja con estadísticas del día: total de ventas, dinero acumulado, promedio por venta y producto más vendido.

El proyecto utiliza únicamente los conceptos de la Unidad 1: variables, tipos primitivos, List<T>, estructuras de control (if, switch, do-while), métodos estáticos y manejo de errores con try/catch y TryParse. No se emplea Programación Orientada a Objetos (clases personalizadas) ni bases de datos — toda la información se maneja en memoria.
Con diferencites opcines validando siempre para que no haya ningun error como no poder registrar una venta si no hay producto y mensaje guias para registrar inventario, consultar y reguistrar ua ventra y un mensaje para mostra reporte de la caja

🖥️ Ejemplos de ejecución
Menú principal
====================================================
   SISTEMA GESTOR DE VENTAS E INVENTARIO (MINI-POS)
====================================================
1. Registrar nuevo producto en inventario
2. Consultar inventario completo
3. Registrar una venta
4. Ver reporte de caja y estadísticas diarias
5. Salir
====================================================
Seleccione una opción (1-5):
Registro de venta con ticket generado
====================================================
                 REGISTRAR VENTA
====================================================
1. Café Colombiano 500g | Precio: $ 18.000,00 | Stock: 10
2. Pan Tajado Integral  | Precio: $  6.500,00 | Stock: 3 [ALERTA: BAJO STOCK]

Seleccione el número del producto a vender (1-2): 1
Ingrese la cantidad a comprar: 2
¿Aplica descuento de cliente frecuente (10%)? (S/N): S

====================================================
                  TICKET DE VENTA
====================================================
 Producto:             Café Colombiano 500g (x2)
 Subtotal:             $ 36.000,00
 Descuento (10%):     -$  3.600,00
 IVA (19%):            +$  6.156,00
 ---------------------------------------------------
 TOTAL A PAGAR:        $ 38.556,00
====================================================
[OK] Venta efectuada con éxito. Stock actualizado: 8 unidades.
Manejo de entradas inválidas
Seleccione una opción (1-5): abc
[ERROR] Entrada no válida. Debe ingresar un número entero.

Seleccione una opción (1-5): 9
[ERROR] Opción fuera de rango. Ingrese un valor entre 1 y 5.
