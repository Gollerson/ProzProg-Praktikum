namespace Praktikum08
{
    class TextStatistik
    {
        static int Index(char c)
        {
            // Kleinbuchstaben (ohne Umlaute)
            if (c >= 'a' && c <= 'z')
                return c - 'a';
            // Großbuchstaben (ohne Umlaute)
            else if (c >= 'A' && c <= 'Z')
                return c - 'A';
            // alles andere
            else
                return -1;
        }


    }
}