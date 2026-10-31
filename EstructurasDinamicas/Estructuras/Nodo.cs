namespace EstructurasDinamicas.Estructuras;

/// <summary>
/// Nodo dinámico de una lista doblemente enlazada.
/// Cada nodo se crea con "new" en el heap administrado y guarda referencias
/// (el equivalente a punteros en C#) a su vecino siguiente y anterior.
/// </summary>
public class Nodo<T>
{
    public T Valor { get; set; }
    public Nodo<T>? Siguiente { get; set; }
    public Nodo<T>? Anterior { get; set; }

    public Nodo(T valor)
    {
        Valor = valor;
    }
}
