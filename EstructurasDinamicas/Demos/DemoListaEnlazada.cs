using EstructurasDinamicas.Estructuras;
using EstructurasDinamicas.Utilidades;

namespace EstructurasDinamicas.Demos;

/// <summary>Menú interactivo para probar la lista doblemente enlazada.</summary>
public static class DemoListaEnlazada
{
    public static void Ejecutar()
    {
        var lista = new ListaDoblementeEnlazada<int>();
        bool salir = false;

        while (!salir)
        {
            Consola.Titulo("LISTA DOBLEMENTE ENLAZADA (nodos dinámicos)");
            Console.WriteLine($"Contenido: {lista}   (Cantidad: {lista.Cantidad})");
            Console.WriteLine();
            Console.WriteLine(" 1. Agregar al inicio");
            Console.WriteLine(" 2. Agregar al final");
            Console.WriteLine(" 3. Insertar en una posición");
            Console.WriteLine(" 4. Eliminar por valor");
            Console.WriteLine(" 5. Eliminar por posición");
            Console.WriteLine(" 6. Buscar un valor");
            Console.WriteLine(" 7. Mostrar la lista al revés");
            Console.WriteLine(" 8. Invertir la lista");
            Console.WriteLine(" 9. Cargar datos de ejemplo");
            Console.WriteLine("10. Vaciar la lista");
            Console.WriteLine(" 0. Volver al menú principal");

            try
            {
                switch (Consola.LeerEntero("Opción: "))
                {
                    case 1:
                        lista.AgregarAlInicio(Consola.LeerEntero("Valor: "));
                        break;

                    case 2:
                        lista.AgregarAlFinal(Consola.LeerEntero("Valor: "));
                        break;

                    case 3:
                        int posicion = Consola.LeerEntero("Posición (0 = inicio): ");
                        int valorNuevo = Consola.LeerEntero("Valor: ");
                        lista.InsertarEn(posicion, valorNuevo);
                        break;

                    case 4:
                        int aEliminar = Consola.LeerEntero("Valor a eliminar: ");
                        Console.WriteLine(lista.Eliminar(aEliminar)
                            ? "Valor eliminado."
                            : "El valor no existe en la lista.");
                        break;

                    case 5:
                        lista.EliminarEn(Consola.LeerEntero("Posición a eliminar: "));
                        Console.WriteLine("Elemento eliminado.");
                        break;

                    case 6:
                        int buscado = Consola.LeerEntero("Valor a buscar: ");
                        int indice = lista.IndiceDe(buscado);
                        Console.WriteLine(indice >= 0
                            ? $"Encontrado en la posición {indice}."
                            : "No encontrado.");
                        break;

                    case 7:
                        Console.WriteLine(lista.EstaVacia
                            ? "[vacía]"
                            : "[" + string.Join(" <-> ", lista.RecorrerAlReves()) + "]");
                        Consola.Pausa();
                        break;

                    case 8:
                        lista.Invertir();
                        Console.WriteLine("Lista invertida.");
                        break;

                    case 9:
                        lista.Limpiar();
                        foreach (int valor in new[] { 10, 20, 30, 40, 50 })
                            lista.AgregarAlFinal(valor);
                        Console.WriteLine("Datos de ejemplo cargados.");
                        break;

                    case 10:
                        lista.Limpiar();
                        Console.WriteLine("Lista vaciada.");
                        break;

                    case 0:
                        salir = true;
                        break;

                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Posición fuera de rango.");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
