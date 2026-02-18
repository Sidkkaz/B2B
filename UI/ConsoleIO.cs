namespace B2B.UI;

public static class ConsoleIO
{
    public static void Output(string texto)
        => Console.WriteLine(texto);

    public static string InputS()
    {
        var x = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(x))
            throw new Exception("Entrada inválida");
        return x;
    }

    public static int InputI()
    {
        if (!int.TryParse(Console.ReadLine(), out var x))
            throw new Exception("Número inválido");
        return x;
    }

    public static double InputD()
    {
        if (!double.TryParse(Console.ReadLine(), out var x))
            throw new Exception("Número inválido");
        return x;
    }
}
