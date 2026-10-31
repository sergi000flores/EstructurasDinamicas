using System.Text;
using EstructurasDinamicas.Demos;
using EstructurasDinamicas.Utilidades;

Console.OutputEncoding = Encoding.UTF8;

while (true)
{
    Consola.Titulo("ESTRUCTURAS DE DATOS DINÁMICAS EN C#");
    Console.WriteLine("1. Lista doblemente enlazada (nodos dinámicos) - menú interactivo");
    Console.WriteLine("2. Pilas, colas y listas genéricas dinámicas");
    Console.WriteLine("3. Comparación estático vs. dinámico (memoria)");
    Console.WriteLine("0. Salir");

    switch (Consola.LeerEntero("Opción: "))
    {
        case 1:
            DemoListaEnlazada.Ejecutar();
            break;
        case 2:
            DemoColeccionesGenericas.Ejecutar();
            break;
        case 3:
            DemoEstaticoVsDinamico.Ejecutar();
            break;
        case 0:
            Console.WriteLine("¡Hasta luego!");
            return;
        default:
            Console.WriteLine("Opción no válida.");
            break;
    }
}
