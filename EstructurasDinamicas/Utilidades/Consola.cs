namespace EstructurasDinamicas.Utilidades;

public static class Consola
{
    public static int LeerEntero(string mensaje)
    {
        while (true)
        {
            Console.Write(mensaje);
            if (int.TryParse(Console.ReadLine(), out int numero))
                return numero;

            Console.WriteLine("Valor no válido. Intenta de nuevo.");
        }
    }

    public static void Titulo(string texto)
    {
        Console.WriteLine();
        Console.WriteLine(new string('=', 64));
        Console.WriteLine(texto);
        Console.WriteLine(new string('=', 64));
    }

    public static void Subtitulo(string texto)
    {
        Console.WriteLine();
        Console.WriteLine($"--- {texto} ---");
    }

    public static void Pausa()
    {
        Console.WriteLine();
        Console.Write("Presiona ENTER para continuar...");
        Console.ReadLine();
    }
}
