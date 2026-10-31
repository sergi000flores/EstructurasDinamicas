using EstructurasDinamicas.Estructuras;
using EstructurasDinamicas.Utilidades;

namespace EstructurasDinamicas.Demos;

/// <summary>
/// Ilustra los dos problemas que contrasta el documento técnico:
/// desperdicio de memoria (estático) vs. sobrecarga por nodo (dinámico).
/// </summary>
public static class DemoEstaticoVsDinamico
{
    public static void Ejecutar()
    {
        Consola.Titulo("MANEJO ESTÁTICO vs. DINÁMICO");

        EscenarioDesperdicio();
        EscenarioDesbordamiento();
        EscenarioMemoriaMedida();

        Consola.Pausa();
    }

    private static void EscenarioDesperdicio()
    {
        Consola.Subtitulo("1) Desperdicio en un arreglo de tamaño fijo");

        const int capacidad = 1000;
        int[] arreglo = new int[capacidad];
        int usados = 0;

        for (int i = 0; i < 120; i++)
            arreglo[usados++] = i;

        int libres = capacidad - usados;
        Console.WriteLine($"Capacidad reservada : {capacidad} elementos ({capacidad * sizeof(int):N0} bytes)");
        Console.WriteLine($"Elementos usados    : {usados} ({usados * sizeof(int):N0} bytes)");
        Console.WriteLine($"Memoria desperdiciada: {libres * sizeof(int):N0} bytes ({(double)libres / capacidad:P0})");
    }

    private static void EscenarioDesbordamiento()
    {
        Consola.Subtitulo("2) Capacidad agotada: arreglo fijo vs. lista enlazada");

        const int capacidad = 5;
        int[] fijo = new int[capacidad];
        int usados = 0;
        var dinamica = new ListaDoblementeEnlazada<int>();

        for (int valor = 1; valor <= 7; valor++)
        {
            if (usados < capacidad)
            {
                fijo[usados++] = valor;
            }
            else
            {
                Console.WriteLine($"  Arreglo fijo: NO hay espacio para el valor {valor} (capacidad {capacidad}).");
            }

            dinamica.AgregarAlFinal(valor);
        }

        Console.WriteLine($"Arreglo fijo guardó  : {usados} de 7 valores");
        Console.WriteLine($"Lista enlazada guardó: {dinamica.Cantidad} de 7 valores -> {dinamica}");
    }

    private static void EscenarioMemoriaMedida()
    {
        Consola.Subtitulo("3) Memoria medida con 100,000 enteros (valores aproximados)");

        const int n = 100_000;

        long bytesArreglo = Medir(() => new int[n]);

        long bytesList = Medir(() =>
        {
            var lista = new List<int>();
            for (int i = 0; i < n; i++) lista.Add(i);
            return lista;
        });

        long bytesPropia = Medir(() =>
        {
            var lista = new ListaDoblementeEnlazada<int>();
            for (int i = 0; i < n; i++) lista.AgregarAlFinal(i);
            return lista;
        });

        long bytesLinked = Medir(() =>
        {
            var lista = new LinkedList<int>();
            for (int i = 0; i < n; i++) lista.AddLast(i);
            return lista;
        });

        Console.WriteLine($"{"Estructura",-34}{"Bytes",14}{"B/elemento",14}{"vs int[]",10}");
        Imprimir("int[] (arreglo exacto)", bytesArreglo, n, bytesArreglo);
        Imprimir("List<int> (arreglo redimensionable)", bytesList, n, bytesArreglo);
        Imprimir("ListaDoblementeEnlazada<int>", bytesPropia, n, bytesArreglo);
        Imprimir("LinkedList<int> (predefinida)", bytesLinked, n, bytesArreglo);

        Console.WriteLine();
        Console.WriteLine("Nota: las cifras dependen del runtime y de la arquitectura (x64 vs x86).");
    }

    private static long Medir(Func<object> crear)
    {
        long antes = GC.GetTotalMemory(true);
        object objeto = crear();
        long despues = GC.GetTotalMemory(true);
        GC.KeepAlive(objeto);
        return despues - antes;
    }

    private static void Imprimir(string nombre, long bytes, int elementos, long referencia)
    {
        double porElemento = (double)bytes / elementos;
        double veces = referencia > 0 ? (double)bytes / referencia : 0;
        Console.WriteLine($"{nombre,-34}{bytes,14:N0}{porElemento,14:F1}{veces,9:F1}x");
    }
}
