using EstructurasDinamicas.Estructuras;
using EstructurasDinamicas.Utilidades;

namespace EstructurasDinamicas.Demos;

/// <summary>
/// Uso de las clases genéricas dinámicas predefinidas de .NET
/// (Stack&lt;T&gt;, Queue&lt;T&gt;, List&lt;T&gt;, LinkedList&lt;T&gt;) y de las
/// versiones propias basadas en nodos (PilaDinamica, ColaDinamica).
/// </summary>
public static class DemoColeccionesGenericas
{
    public static void Ejecutar()
    {
        Consola.Titulo("PILAS, COLAS Y LISTAS GENÉRICAS DINÁMICAS");

        DemoPila();
        DemoCola();
        DemoLista();
        DemoLinkedList();
        DemoEstructurasPropias();

        Consola.Pausa();
    }

    private static void DemoPila()
    {
        Consola.Subtitulo("Stack<T> - Pila (LIFO): historial de navegación");

        var historial = new Stack<string>();
        historial.Push("inicio.com");
        historial.Push("noticias.com");
        historial.Push("correo.com");

        Console.WriteLine($"Elementos (tope primero): {string.Join(", ", historial)}");
        Console.WriteLine($"Peek (tope sin quitar)  : {historial.Peek()}");
        Console.WriteLine($"Pop (se quita el tope)  : {historial.Pop()}");
        Console.WriteLine($"Cantidad actual         : {historial.Count}");
    }

    private static void DemoCola()
    {
        Consola.Subtitulo("Queue<T> - Cola (FIFO): turnos de atención");

        var turnos = new Queue<string>();
        turnos.Enqueue("Ana");
        turnos.Enqueue("Luis");
        turnos.Enqueue("María");
        turnos.Enqueue("Pedro");

        Console.WriteLine($"Cola                    : {string.Join(" -> ", turnos)}");
        Console.WriteLine($"Peek (siguiente)        : {turnos.Peek()}");
        Console.WriteLine($"Dequeue (atendido)      : {turnos.Dequeue()}");
        Console.WriteLine($"Cantidad actual         : {turnos.Count}");
    }

    private static void DemoLista()
    {
        Consola.Subtitulo("List<T> - Lista dinámica (arreglo redimensionable)");

        var numeros = new List<int> { 40, 10, 30 };
        numeros.Add(20);
        numeros.Insert(1, 99);
        Console.WriteLine($"Lista                   : {string.Join(", ", numeros)}");

        numeros.Remove(30);
        numeros.Sort();
        Console.WriteLine($"Tras Remove(30) y Sort  : {string.Join(", ", numeros)}");
        Console.WriteLine($"Contains(99)            : {numeros.Contains(99)}");
        Console.WriteLine($"IndexOf(40)             : {numeros.IndexOf(40)}");

        Console.WriteLine();
        Console.WriteLine("Crecimiento de la capacidad interna al agregar 20 elementos:");
        var crecimiento = new List<int>();
        int capacidad = crecimiento.Capacity;
        Console.WriteLine($"  Capacidad inicial: {capacidad}");

        for (int i = 1; i <= 20; i++)
        {
            crecimiento.Add(i);
            if (crecimiento.Capacity != capacidad)
            {
                capacidad = crecimiento.Capacity;
                Console.WriteLine($"  Al agregar el elemento {i,2}: capacidad -> {capacidad}");
            }
        }
        Console.WriteLine($"  Resultado: Count = {crecimiento.Count}, Capacity = {crecimiento.Capacity}");
    }

    private static void DemoLinkedList()
    {
        Consola.Subtitulo("LinkedList<T> - Lista doblemente enlazada predefinida");

        var lista = new LinkedList<string>();
        lista.AddLast("B");
        lista.AddLast("C");
        lista.AddFirst("A");

        LinkedListNode<string>? nodoB = lista.Find("B");
        if (nodoB is not null)
            lista.AddAfter(nodoB, "B2");

        Console.WriteLine($"Lista                   : {string.Join(" <-> ", lista)}");
        Console.WriteLine($"First / Last            : {lista.First?.Value} / {lista.Last?.Value}");

        lista.Remove("C");
        Console.WriteLine($"Tras Remove(\"C\")        : {string.Join(" <-> ", lista)}");
    }

    private static void DemoEstructurasPropias()
    {
        Consola.Subtitulo("PilaDinamica<T> y ColaDinamica<T> (propias, sobre nodos)");

        var pila = new PilaDinamica<int>();
        for (int i = 1; i <= 4; i++)
            pila.Push(i * 10);
        Console.WriteLine($"Pila (tope a la izq.)   : {pila}");
        Console.WriteLine($"Pop                     : {pila.Pop()}");
        Console.WriteLine($"Pila                    : {pila}");

        var cola = new ColaDinamica<int>();
        for (int i = 1; i <= 4; i++)
            cola.Enqueue(i * 100);
        Console.WriteLine($"Cola (frente a la izq.) : {cola}");
        Console.WriteLine($"Dequeue                 : {cola.Dequeue()}");
        Console.WriteLine($"Cola                    : {cola}");

        var vacia = new PilaDinamica<int>();
        try
        {
            vacia.Pop();
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Pop en pila vacía       : {ex.Message}");
        }
    }
}
