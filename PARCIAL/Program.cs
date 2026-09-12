using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime;
// Declaración e inicialización correcta de las listas
List<string> nombreP = new List<string>();
List<decimal> precioP = new List<decimal>();
List<int> cantidadP = new List<int>();
List<int> unidadesVendidasP = new List<int>();
decimal alcancia=0, totaldinero;
int opc = 0;
int totalVentasRealizadas = 0;
string mensaje = "Seleccione una opción (1-5):";
string mensaje2 = "Digite el precio del producto: ";
string mensaje3 = "Digite el stock minimo 0:";
string namemenu = "SISTEMA GESTOR DE VENTAS E INVENTARIO (MINI-POS)";
string mensajeventa = "REGISTRAR VENTA";
string mensajeinv = "INVENTARIO DE PRODUCTOS";
do
{
    //menu
    imprimirencabezado(namemenu);
    //se llama el metodo para la leer la opcion
    opc = LeerEntero(mensaje, 1, 5);
            switch(opc)
            {
                case 1:
                    nombreP.Add(validarnombre(nombreP));
                    precioP.Add(LeerDecimal(mensaje2, 1));
                    cantidadP.Add(LeerStock(mensaje3, 0));
                    unidadesVendidasP.Add(0);
                    Console.WriteLine("Producto registrado con exito");
                    Console.ReadLine();
                break; 
                case 2:
                    MostrarInv(nombreP, precioP, cantidadP, mensajeinv);
                    Console.ReadLine();
                break; 
                case 3:
                    if (nombreP.Count == 0)
                    {
                        Console.WriteLine("\n[INFO] No hay productos registrados en el sistema.");
                        Console.ReadLine();
                        break;
                    }
                    MostrarInv(nombreP, precioP, cantidadP, mensajeventa);
                    ProcesarVenta(nombreP, precioP, cantidadP, unidadesVendidasP, ref alcancia, ref totalVentasRealizadas);
                    Console.ReadLine();
            break;  
                case 4:
                    MostrarReporte(nombreP, unidadesVendidasP, alcancia, totalVentasRealizadas);
                    Console.ReadLine();
                    break; 
                case 5:
                    Environment.Exit(0);
                break; 
            }
} while(opc!=5);
static void imprimirencabezado(string nombreP)
{
    Console.Clear();
    Console.WriteLine("====================================================\n" +
        "  "+nombreP+" \n" +
        "====================================================\n" +
        "1. Registrar nuevo producto en inventario\n" +
        "2. Consultar inventario completo\n" +
        "3. Registrar una venta\n" +
        "4. Ver reporte de caja y estadísticas diarias\n" +
        "5. Salir\n" +
        "====================================================\n");
}
static int LeerEntero(string mensaje, int min, int max)
{
    //VARIBLE LOCAL
    int opc;
    //Hacemos que solo vuelve al main cuando elija una de las opciones disponibles
    while (true)
    {
        try
        {
            Console.Write(mensaje);
            opc = int.Parse(Console.ReadLine());
            if (opc < min || opc > max) Console.WriteLine($"[ERROR] Opción fuera de rango. Ingrese un valor entre {min} y {max}.");
            else return opc;
        }
        catch
        {
            Console.WriteLine("[ERROR] Entrada no Valida. Debe ingresar un numero entero.");
        }
    }
}
static string validarnombre(List<string> listaProductos)
{
    string nombre = "";
    bool nombreValido = false;
        while (!nombreValido)
        {
            Console.Write("Digite nombre del producto: ");
            nombre = Console.ReadLine().Trim();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                Console.WriteLine("[ERROR] El nombre no puede estar vacío.");
                continue;
            }

            bool duplicado = false;
            foreach (string p in listaProductos)
            {
                if (p.ToLower() == nombre.ToLower())
                {
                    duplicado = true;
                    break;
                }
            }

            if (duplicado)
            {
                Console.WriteLine("[ERROR] Ya existe un producto con ese nombre.");
            }
            else
            {
                nombreValido = true; // Sale del ciclo si pasa las validaciones

            }
        }
    return nombre;
}
static decimal LeerDecimal(string mensaje, decimal min)
{
    while (true)
    {
        Console.Write(mensaje);
        string entrada = Console.ReadLine()?.Trim() ?? "";

        try
        {
            // Intenta convertir; si falla, salta directamente al bloque catch
            decimal numero = decimal.Parse(entrada);

            // Validación de rango
            if (numero < min)
            {
                Console.WriteLine($"[ERROR] El valor debe ser mayor a {min}.");
            }
            else
            {
                return numero; // Retorna y sale del bucle si todo es correcto
            }
        }
        catch
        {
            Console.WriteLine("[ERROR] Entrada no Valida. Debe ingresar un numero entero.");
        }
    }
}
static int LeerStock(string mensaje, int min)
{
    while (true)
    {
        Console.Write(mensaje);
        string entrada = Console.ReadLine()?.Trim() ?? "";

        try
        {
            // Intenta convertir el texto a un número entero
            int numero = int.Parse(entrada);

            // Valida que sea mayor o igual al mínimo permitido (en tu caso, 0)
            if (numero < min)
            {
                Console.WriteLine($"[ERROR] El valor debe ser mayor o igual a {min}.");
            }
            else
            {
                return numero; // Retorna el número válido y sale del bucle
            }
        }
        catch
        {
            Console.WriteLine("[ERROR] Formato incorrecto. Debe ingresar un número entero válido.");
        }
    }
}

static void MostrarInv(List<string> listaNombres, List<decimal> listaPrecios, List<int> listaStock, string msg)
{
    // 1. Validar si no hay productos
    if (listaNombres.Count == 0)
    {
        Console.WriteLine("\n[INFO] No hay productos registrados en el sistema.");
        return;
    }
    // 2. Encabezados de la tabla con interpolación y alineación fija
    // Los números negativos (ej. -5) alinean a la izquierda. Los positivos (ej. 12) a la derecha.
    Console.WriteLine("\n==============================================================================");
    Console.WriteLine("                             "+msg+"                          ");
    Console.WriteLine("==============================================================================");
    Console.WriteLine($"{"ID",-5} | {"Nombre del Producto",-25} | {"Precio",12} | {"Stock",6} | {"Estado",-20}");
    Console.WriteLine("------------------------------------------------------------------------------");

    // 3. Recorrer e imprimir cada producto
    for (int i = 0; i < listaNombres.Count; i++)
    {
        int id = i + 1;
        string nombre = listaNombres[i];
        decimal precio = listaPrecios[i];
        int stock = listaStock[i];

        // Determinar si lleva alerta usando un operador ternario de una sola línea
        string alerta = stock < 5 ? "[ALERTA: BAJO STOCK]" : "OK";

        // :C2 aplica formato de moneda (ej: $1,250.00). :N0 aplica formato de número sin decimales para el stock
        Console.WriteLine($"{id,-5} | {nombre,-25} | {precio,12:C2} | {stock,6:N0} | {alerta,-20}");
    }

    Console.WriteLine("------------------------------------------------------------------------------");
    Console.WriteLine($"Total de productos únicos: {listaNombres.Count}");
    Console.WriteLine("==============================================================================\n");
}

static decimal CalcularFactura(decimal precio, int cantidad, bool tieneDescuento, out decimal montoIva, out decimal montoDescuento)
{
    decimal subtotal = precio * cantidad;
    montoDescuento = tieneDescuento ? subtotal * 0.10m : 0.00m;
    montoIva = (subtotal - montoDescuento) * 0.19m;

    return subtotal - montoDescuento + montoIva;
}
static void ProcesarVenta(List<string> listaNombres, List<decimal> listaPrecios, List<int> listaStock, List<int> unidadesVendidas, ref decimal cajaDelDia, ref int contadorVentas)
{
    // 1. Pedir ID del producto
    int id = LeerEntero("Seleccione el ID del producto a comprar: ", 1, listaNombres.Count);
    int idx = id - 1;

    string nombre = listaNombres[idx];
    decimal precio = listaPrecios[idx];
    int stockDisp = listaStock[idx];

    if (stockDisp == 0)
    {
        Console.WriteLine($"\n[RECHAZADO] No hay stock disponible para '{nombre}'.");
        return;
    }

    // 2. Pedir cantidad
    int cantidad = LeerEntero($"Ingrese la cantidad a vender de '{nombre}': ", 1, stockDisp);

    // 3. Preguntar por descuento
    bool tieneDescuento = false;
    while (true)
    {
        Console.Write("¿Aplica descuento de cliente frecuente (10%)? (S/N): ");
        string rta = Console.ReadLine()?.Trim().ToUpper() ?? "";
        if (rta == "S") { tieneDescuento = true; break; }
        if (rta == "N") { tieneDescuento = false; break; }
        Console.WriteLine("[ERROR] Digite 'S' para Sí o 'N' para No.");
    }

    // 4. Llamar a CalcularFactura con OUT
    decimal totalPagar = CalcularFactura(precio, cantidad, tieneDescuento, out decimal iva, out decimal descuento);
    decimal subtotal = precio * cantidad;

    // 5. Actualizar inventario, ventas y caja
    listaStock[idx] -= cantidad;
    unidadesVendidas[idx] += cantidad;
    cajaDelDia += totalPagar;
    contadorVentas++;

    // 6. Ticket
    Console.WriteLine("\n==============================================");
    Console.WriteLine("               TICKET DE VENTA                ");
    Console.WriteLine("==============================================");
    Console.WriteLine($"Producto:    {nombre} (x{cantidad})");
    Console.WriteLine($"Subtotal:    {subtotal,15:C2}");
    Console.WriteLine($"Descuento:  -{descuento,15:C2} {(tieneDescuento ? "(10%)" : "")}");
    Console.WriteLine($"IVA (19%):   +{iva,15:C2}");
    Console.WriteLine("----------------------------------------------");
    Console.WriteLine($"TOTAL PAGAR: {totalPagar,15:C2}");
    Console.WriteLine("==============================================");
    Console.WriteLine($"[OK] Venta exitosa. Nuevo stock de '{nombre}': {listaStock[idx]} unidades.\n");
}
static void MostrarReporte(List<string> listaNombres, List<int> unidadesVendidas, decimal cajaDelDia, int contadorVentas)
{
    Console.WriteLine("\n==============================================================================");
    Console.WriteLine("                     REPORTE DE CAJA Y ESTADÍSTICAS DIARIAS                  ");
    Console.WriteLine("==============================================================================");

    // 1. Validar si hubo ventas
    if (contadorVentas == 0)
    {
        Console.WriteLine("[INFO] Aún no se ha registrado ninguna venta en esta sesión.");
        Console.WriteLine("==============================================================================\n");
        return;
    }

    // 2. Total de ventas y total en caja
    Console.WriteLine($"Total de ventas realizadas:      {contadorVentas}");
    Console.WriteLine($"Total acumulado en caja:         {cajaDelDia:C2}");

    // 3. Promedio de dinero por venta
    decimal promedio = cajaDelDia / contadorVentas;
    Console.WriteLine($"Promedio de dinero por venta:     {promedio:C2}");

    // 4. Producto con mayor cantidad de unidades vendidas
    int indiceMax = 0;
    for (int i = 1; i < unidadesVendidas.Count; i++)
    {
        if (unidadesVendidas[i] > unidadesVendidas[indiceMax])
        {
            indiceMax = i;
        }
    }

    if (unidadesVendidas[indiceMax] == 0)
    {
        Console.WriteLine("Producto más vendido:            Ningún producto tiene ventas registradas.");
    }
    else
    {
        Console.WriteLine($"Producto más vendido:            {listaNombres[indiceMax]} ({unidadesVendidas[indiceMax]} unidades)");
    }

    Console.WriteLine("==============================================================================\n");
}