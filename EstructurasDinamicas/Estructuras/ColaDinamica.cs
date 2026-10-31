namespace EstructurasDinamicas.Estructuras;

/// <summary>
/// Cola (FIFO) construida sobre la lista enlazada de nodos dinámicos.
/// Se encola por la cola de la lista y se desencola por la cabeza.
/// </summary>
public class ColaDinamica<T>
{
    private readonly ListaDoblementeEnlazada<T> _nodos = new();

    public int Cantidad => _nodos.Cantidad;
    public bool EstaVacia => _nodos.EstaVacia;

    public void Enqueue(T valor) => _nodos.AgregarAlFinal(valor);

    public T Dequeue() => _nodos.EliminarPrimero();

    public T Peek() => _nodos.Primero;

    public override string ToString() => _nodos.ToString();
}
