namespace migradata.Helpers;

public static class ScreenValue
{
    public static void Botton(string value)
    {
        Console.SetCursorPosition(0, Console.CursorTop);
        Console.Write(value);
    }
}