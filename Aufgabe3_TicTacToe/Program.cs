static void DrawBoard(int[,] board)
{
    //Erstelle die Spalten-Beschriftung
    for(int i = 0;i<=board.GetLength(1);i++)
    {
        if(i==0)
        {
            Console.Write("   ");
        }
        else
        {
            Console.Write("| "+i+" ");
        }
    }
    Console.Write("\n");
    //Erstelle die Abgrenzung der Beschriftung zur Tabelle (---+---+---)
    for(int i = 0;i<=board.GetLength(1);i++)
    {
        Console.Write("---");
        if(i!=board.GetLength(1))
        {
        Console.Write("+");
        }
    }
    Console.Write("\n");
    //Schreibe die Zeilen auf
    for(int i = 0; i<board.GetLength(0);i++)
    {
        Console.Write(" "+(char)(i+65)+" "); //Am Anfang der Zeile ein Buchstabe
        for(int j = 0;j<board.GetLength(1);j++)
        {
            Console.Write("| ");
            switch (board[i,j])
            {
                case 0: Console.Write("  "); break;

                case 1: Console.Write("X "); break;
                
                case 2: Console.Write("O "); break;
            }

        }
        Console.Write("\n");
    }
}

static bool IsInBounds(int[,] board,int newY,int newX)
{
    bool isInBounds = (newY >= 0 && newY < board.GetLength(0)) &&  //Hilfsvariable um festzustellen ob man den Index out of Range fährt 
                      (newX >= 0 && newX < board.GetLength(1));
    return isInBounds;
}
static bool IsWinner(int[,]board,int lastmovYpos,int lastmovXpos)
{
    int searchfor = board[lastmovYpos,lastmovXpos]; //suche nach einer Reihe von dem typ des letzten Spielzugs
    int newY = lastmovYpos;
    int newX = lastmovXpos;
    
    for(int i = -1; i<=1;i++)
    {                             
        newY = lastmovYpos + i; //Verschachtelte for-schleifen dienen zu Iteration über die 8 Überprüfungsrichtungen
        for(int j = -1; j<=1;j++)   
        {                           
            newX = lastmovXpos + j; 
            if(IsInBounds(board,newY,newX)&&(!(j==0&i==0)))
            {
                if(searchfor == board[newY,newX])
                {
                    for(int k=-1;k<=2;k+=3)
                    {
                        newX = lastmovXpos + k*j;
                        newY = lastmovYpos + k*i;
                        if(IsInBounds(board,newY,newX))
                        {
                            if(searchfor == board[newY,newX])
                            {
                                //(1.Schleifendurchlauf) das symbol ist mittig in einer dreier reihe
                                //(2.Schleifendurchlauf) das symbol ist seitlich an einer dreier reihe
                                return true;
                            }
                        }
                    }
                }
            }
        }
    }
    return false; //alle Möglichkeiten wurden erschöpft ohne dass eine Reihe gefunden wurde
}
// Funktion "GetYX" nimmt einen String als Input und gibt ein Array mit Y,X Koordinaten zurück
static void GetYX(string input, out int y, out int x)
{
    // Initialisiere Y und X als Ausgabewerte
    y = -1; // Standardwert, wenn kein Buchstabe gefunden wird
    x = 0;  // Standardwert, wenn keine Zahl gefunden wird

    bool lastCharWasNumber = false;  // War das letzte Zeichen eine Zahl?
    bool yWasWritten = false;        // Wurde bereits ein Y-Wert gefunden?
    bool xWasWritten = false;        // Wurde bereits ein X-Wert gefunden?

    // Schleife durch Input-String
    for (int i = 0; i < input.Length; i++)
    {
        // Prüfe, ob aktuelles Zeichen eine Zahl ist (ASCII 48-57 sind 0-9)
        if ((int)input[i] >= 48 && (int)input[i] <= 57)
        {
            if (lastCharWasNumber)
            {
                // Multipliziere bisherige Zahl mit 10 und addiere neue Ziffer
                x = (x * 10) + (input[i] - '0');
            }
            else if (!xWasWritten)
            {
                // Erste gefundene Zahl: Speichere direkt im X-Wert
                x = input[i]-49;
                lastCharWasNumber = true;
                xWasWritten = true;
            }
        }
        else
        {
            lastCharWasNumber = false; // Kein Zahlenzeichen, setze Flag zurück
        }

        // Prüfe, ob aktuelles Zeichen ein Großbuchstabe ist (A-Z: ASCII 65-90)
        if ((int)input[i] >= 65 && (int)input[i] <= 90)
        {
            if (!yWasWritten)
            {
                y = input[i] - 65; // Konvertiere Buchstabe zu Zahl (A=0, B=1, etc.)
                yWasWritten = true;
            }
        }

        // Prüfe, ob aktuelles Zeichen ein Kleinbuchstabe ist (a-z: ASCII 97-122)
        if ((int)input[i] >= 97 && (int)input[i] <= 122)
        {
            if (!yWasWritten)
            {
                y = input[i] - 97; // Konvertiere Buchstabe zu Zahl (a=0, b=1, etc.)
                yWasWritten = true;
            }
        }
    }
}

Console.Write("Wie viele Spalten soll das Spielbrett haben?: ");
int spalten = Convert.ToInt32(Console.ReadLine());
Console.Write("Wie viele Zeilen soll das Spielbrett haben?: ");
int zeilen = Convert.ToInt32(Console.ReadLine());

int[,] board = new int[zeilen,spalten];
int spieler = 2,movX,movY;
do
{
if(spieler == 1)
    spieler = 2;
else
    spieler = 1;
Console.Clear();
System.Console.WriteLine($"Spieler {spieler} du bist dran!");
DrawBoard(board);
do
{
System.Console.Write("Gebe die Koordinaten deines nächsten Zuges ein!:");
GetYX(Console.ReadLine()!,out movY,out movX);
}while(!IsInBounds(board,movY,movX));
board[movY,movX]=spieler;
} while (!IsWinner(board,movY,movX));
Console.Clear();
DrawBoard(board);
Console.WriteLine($"Spieler {spieler} hat gewonnen!");
System.Console.WriteLine("Drücken sie eine beliebige Taste um zu schließen.");
Console.ReadLine();