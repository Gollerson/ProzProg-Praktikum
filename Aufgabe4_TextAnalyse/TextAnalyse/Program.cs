static string LongestWord(string[] words,string longestWord) //gets all words in a Line and the prev longest word, returns longest word so far
{
    string longest = longestWord;
    for(int i = 0; i<words.Length;i++)
    {
        if(words[i].Length > longest.Length)
            longest = words[i];
    }
    return longest; 
}
StreamReader sr = new StreamReader(@"..\Test.txt");
string line,longestWord = "";
int symbolcount = 0,wordcount = 0,linecount = 0;
while(!sr.EndOfStream)
{
    line = sr.ReadLine()!;
    if(line!="")
    {
        linecount ++;
        string[] words = line.Split(" ");
        longestWord = LongestWord(words,longestWord);
        wordcount += words.Length;
        symbolcount += line.Length;
    }
}
Console.WriteLine($"Zeilen: {linecount}");
Console.WriteLine($"Wörter: {wordcount}");
Console.WriteLine($"Zeichen: {symbolcount}");
Console.WriteLine($"Längstes Wort: {longestWord}");