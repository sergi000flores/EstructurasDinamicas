namespace EstructurasDinamicas.Estructuras;

/// <summary>
/// Pila (LIFO) construida sobre la lista enlazada de nodos dinámicos.
/// El tope de la pila es la cabeza de la lista.
/// </summary>
public class PilaDinamica<T>
{
    private readonly ListaDoblementeEnlazada<T> _nodos = new();

    public int Cantidad => _nodos.Cantidad;
    public bool EstaVacia => _nodos.EstaVacia;

    public void Push(T valor) => _nodos.AgregarAlInicio(valor);

    public T Pop() => _nodos.EliminarPrimero();

    public T Peek() => _nodos.Primero;

    public override string ToString() => _nodos.ToString();
}
