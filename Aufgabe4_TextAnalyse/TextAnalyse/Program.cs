
static string LongestWord(string[] words, string longestWord) // Checkt ob in einem Array von wörtern das jetzt längste wort ist und gibt das zurück.
{
    string longest = longestWord;
    for (int i = 0; i < words.Length; i++)
    {
        if (words[i].Length > longest.Length)
            longest = words[i];
    }
    return longest;
}
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
StreamReader sr = new StreamReader(@"..\kafka_verwandlung.txt");
string line, longestWord = "";
int[] occurences = new int[26];
int[] kombinationen = new int[676]; //26*26 Variationen aa,ab,ac,ad,...,zx,zy,zz. 1. buchstabe = index / 26 2. buchstabe = index % 26
int symbolcount = 0, wordcount = 0, linecount = 0;
int[,] mostusedcomb = new int[2,26];
while (!sr.EndOfStream)
{
    line = sr.ReadLine()!;
    if (line != "")
    {
        linecount++;
        string[] words = line.Split(" ");
        longestWord = LongestWord(words, longestWord);
        wordcount += words.Length;
        for (int i = 0; i < line.Length; i++) //Iteriere durch alle Symbole
        {
            symbolcount++;
            if (Index(line[i]) != -1) // Nur wenn das Zeichen ein Buchstabe ist (wichtig für die Kombinationen)
            {
                occurences[Index(line[i])]++; //Zähle alle Instanzen eines buchstaben mit um die Häufigkeit des Buchstaben zu erhalten
                if (i < line.Length - 1)
                    if (Index(line[i + 1]) != -1)
                    {
                        kombinationen[(Index(line[i]) * 26) + Index(line[i + 1])]++; //adde 2 stellige kombination nach array logik von aa,ab,ac,...ax,ay,az,ba,bb,bc,...yx,zy,zz
                        if (kombinationen[(Index(line[i]) * 26) + Index(line[i + 1])] > mostusedcomb[1,Index(line[i])]) // wenn die kombination am häufigsten ist -> update die meistverwendetste Kombination
                        {
                            mostusedcomb[1,Index(line[i])] = kombinationen[(Index(line[i]) * 26) + Index(line[i + 1])]; //häufigkeit der Kombination
                            mostusedcomb[0,Index(line[i])] = (Index(line[i]) * 26) + Index(line[i + 1]); //index der Kombination
                        }
                    }
            }
        }

    }
}
Console.WriteLine($"Zeilen: {linecount}");
Console.WriteLine($"Wörter: {wordcount}");
Console.WriteLine($"Zeichen: {symbolcount}");
Console.WriteLine($"Längstes Wort: {longestWord}");
for (int i = 'a'; i <= 'z'; i++)
{
    Console.WriteLine($"{(char)i}:{occurences[i - 'a'],6}; Häufigste Kombination: {(char)(mostusedcomb[0,i-'a']/26+'a')}{(char)(mostusedcomb[0,i-'a']%26+'a')} ({mostusedcomb[1,i-'a']}X)"); //Ganze Ausgabe in eine Zeile gequetscht. Hatte aber kein Bock das aufzusplitten.
}