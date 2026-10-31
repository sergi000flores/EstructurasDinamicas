using System.Collections;

namespace EstructurasDinamicas.Estructuras;

/// <summary>
/// Lista doblemente enlazada genérica con nodos dinámicos.
/// Crece y decrece nodo por nodo: no reserva capacidad por adelantado.
/// </summary>
public class ListaDoblementeEnlazada<T> : IEnumerable<T>
{
    private Nodo<T>? _cabeza;
    private Nodo<T>? _cola;

    public int Cantidad { get; private set; }
    public bool EstaVacia => Cantidad == 0;

    public T Primero => _cabeza is null
        ? throw new InvalidOperationException("La lista está vacía.")
        : _cabeza.Valor;

    public T Ultimo => _cola is null
        ? throw new InvalidOperationException("La lista está vacía.")
        : _cola.Valor;

    // ---------------------------------------------------------------
    // Inserción
    // ---------------------------------------------------------------

    /// <summary>Inserta al inicio. Complejidad O(1).</summary>
    public void AgregarAlInicio(T valor)
    {
        var nuevo = new Nodo<T>(valor);
        if (_cabeza is null)
        {
            _cabeza = nuevo;
            _cola = nuevo;
        }
        else
        {
            nuevo.Siguiente = _cabeza;
            _cabeza.Anterior = nuevo;
            _cabeza = nuevo;
        }
        Cantidad++;
    }

    /// <summary>Inserta al final. Complejidad O(1).</summary>
    public void AgregarAlFinal(T valor)
    {
        var nuevo = new Nodo<T>(valor);
        if (_cola is null)
        {
            _cabeza = nuevo;
            _cola = nuevo;
        }
        else
        {
            nuevo.Anterior = _cola;
            _cola.Siguiente = nuevo;
            _cola = nuevo;
        }
        Cantidad++;
    }

    /// <summary>Inserta en una posición (0..Cantidad). Complejidad O(n) por la búsqueda del nodo.</summary>
    public void InsertarEn(int indice, T valor)
    {
        if (indice < 0 || indice > Cantidad)
            throw new ArgumentOutOfRangeException(nameof(indice));

        if (indice == 0)
        {
            AgregarAlInicio(valor);
            return;
        }
        if (indice == Cantidad)
        {
            AgregarAlFinal(valor);
            return;
        }

        Nodo<T> actual = ObtenerNodo(indice);
        var nuevo = new Nodo<T>(valor)
        {
            Anterior = actual.Anterior,
            Siguiente = actual
        };
        actual.Anterior!.Siguiente = nuevo;
        actual.Anterior = nuevo;
        Cantidad++;
    }

    // ---------------------------------------------------------------
    // Eliminación
    // ---------------------------------------------------------------

    public T EliminarPrimero()
    {
        if (_cabeza is null)
            throw new InvalidOperationException("La lista está vacía.");

        T valor = _cabeza.Valor;
        Desenlazar(_cabeza);
        return valor;
    }

    public T EliminarUltimo()
    {
        if (_cola is null)
            throw new InvalidOperationException("La lista está vacía.");

        T valor = _cola.Valor;
        Desenlazar(_cola);
        return valor;
    }

    /// <summary>Elimina la primera aparición del valor. Devuelve false si no existe.</summary>
    public bool Eliminar(T valor)
    {
        Nodo<T>? nodo = BuscarNodo(valor);
        if (nodo is null) return false;

        Desenlazar(nodo);
        return true;
    }

    public void EliminarEn(int indice)
    {
        Desenlazar(ObtenerNodo(indice));
    }

    public void Limpiar()
    {
        // Al soltar las referencias, el recolector de basura (GC) libera los nodos.
        _cabeza = null;
        _cola = null;
        Cantidad = 0;
    }

    // ---------------------------------------------------------------
    // Consulta
    // ---------------------------------------------------------------

    public T ObtenerEn(int indice) => ObtenerNodo(indice).Valor;

    public bool Contiene(T valor) => BuscarNodo(valor) is not null;

    /// <summary>Devuelve la posición del valor o -1 si no existe.</summary>
    public int IndiceDe(T valor)
    {
        var comparador = EqualityComparer<T>.Default;
        Nodo<T>? actual = _cabeza;
        int indice = 0;

        while (actual is not null)
        {
            if (comparador.Equals(actual.Valor, valor))
                return indice;
            actual = actual.Siguiente;
            indice++;
        }
        return -1;
    }

    /// <summary>Invierte la lista intercambiando los enlaces de cada nodo. O(n).</summary>
    public void Invertir()
    {
        Nodo<T>? actual = _cabeza;
        while (actual is not null)
        {
            Nodo<T>? siguiente = actual.Siguiente;
            actual.Siguiente = actual.Anterior;
            actual.Anterior = siguiente;
            actual = siguiente;
        }
        (_cabeza, _cola) = (_cola, _cabeza);
    }

    // ---------------------------------------------------------------
    // Recorridos
    // ---------------------------------------------------------------

    public IEnumerator<T> GetEnumerator()
    {
        Nodo<T>? actual = _cabeza;
        while (actual is not null)
        {
            yield return actual.Valor;
            actual = actual.Siguiente;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>Recorre desde la cola hacia la cabeza (ventaja de la doble enlace).</summary>
    public IEnumerable<T> RecorrerAlReves()
    {
        Nodo<T>? actual = _cola;
        while (actual is not null)
        {
            yield return actual.Valor;
            actual = actual.Anterior;
        }
    }

    public override string ToString()
        => EstaVacia ? "[vacía]" : "[" + string.Join(" <-> ", this) + "]";

    // ---------------------------------------------------------------
    // Apoyo interno
    // ---------------------------------------------------------------

    private Nodo<T>? BuscarNodo(T valor)
    {
        var comparador = EqualityComparer<T>.Default;
        Nodo<T>? actual = _cabeza;

        while (actual is not null)
        {
            if (comparador.Equals(actual.Valor, valor))
                return actual;
            actual = actual.Siguiente;
        }
        return null;
    }

    /// <summary>Localiza un nodo recorriendo desde el extremo más cercano.</summary>
    private Nodo<T> ObtenerNodo(int indice)
    {
        if (indice < 0 || indice >= Cantidad)
            throw new ArgumentOutOfRangeException(nameof(indice));

        Nodo<T> actual;
        if (indice < Cantidad / 2)
        {
            actual = _cabeza!;
            for (int i = 0; i < indice; i++)
                actual = actual.Siguiente!;
        }
        else
        {
            actual = _cola!;
            for (int i = Cantidad - 1; i > indice; i--)
                actual = actual.Anterior!;
        }
        return actual;
    }

    /// <summary>Quita un nodo de la cadena reconectando a sus vecinos. O(1).</summary>
    private void Desenlazar(Nodo<T> nodo)
    {
        if (nodo.Anterior is not null)
            nodo.Anterior.Siguiente = nodo.Siguiente;
        else
            _cabeza = nodo.Siguiente;

        if (nodo.Siguiente is not null)
            nodo.Siguiente.Anterior = nodo.Anterior;
        else
            _cola = nodo.Anterior;

        nodo.Anterior = null;
        nodo.Siguiente = null;
        Cantidad--;
    }
}
