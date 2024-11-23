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

static bool IsWinner(int[,]board,int lastmovYpos,int lastmovXpos)
{
    int searchfor = board[lastmovYpos,lastmovXpos]; //suche nach einer Reihe von dem typ des letzten Spielzugs
    int newY = lastmovYpos;
    int newX = lastmovXpos;
    bool isInBounds = newY >= 0 && newY < board.GetLength(0) &&  //Hilfsvariable um festzustellen ob man den Index out of Range fährt 
                      newX >= 0 && newX < board.GetLength(1);
    
    for(int i = -1; i<=1;i++)
    {                             
        newY = lastmovYpos + i; //Verschachtelte for-schleifen dienen zu Iteration über die 8 Überprüfungsrichtungen
        for(int j = -1; j<=1;j++)   
        {                           
            newX = lastmovXpos + j; 
            if(isInBounds)
            {
                if(searchfor == board[newY,newX])
                {
                    for(int k=-1;k<=2;k+=3)
                    {
                        newX = lastmovXpos + k*j;
                        newY = lastmovYpos + k*i;
                        if(isInBounds)
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
// Funktion nimmt einen String als Input und gibt ein Array mit Y,X Koordinaten zurück
static int[] GetYX(string input)
{
    // Array für Y und X Position, Index 0 = Y, Index 1 = X
    int[] YXPos = new int[2];
    
    bool lastcharwasnumber = false;  // War das letzte Zeichen eine Zahl?
    bool ywasWritten = false;        // Wurde bereits ein Y-Wert (Buchstabe) gefunden?
    bool xwasWritten = false;        // Wurde bereits ein X-Wert (Zahl) gefunden?

    // Schleife durch Input-String
    for(int i = 0; i < input.Length; i++)
    {
        // Prüfe ob aktuelles Zeichen eine Zahl ist (ASCII und UTF-8 48-57 sind Zahlen 0-9)
        if((int)input[i] >= 48 && (int)input[i] <= 57)
        {
            if (lastcharwasnumber == true)
                // Wenn vorheriges Zeichen auch eine Zahl war (nur bei erstem eintrag):
                // Multipliziere bisherige Zahl mit 10 und addiere neue Ziffer
                YXPos[1] = (YXPos[1] * 10) + (int)input[i];
            else
            {
                if(!xwasWritten)
                {
                    // Erste gefundene Zahl: Speichere direkt im X-Wert
                    YXPos[1] = (int)input[i];
                    lastcharwasnumber = true;
                    xwasWritten = true;
                }
            }
        }
        else
        {
            // Wenn kein Zahlenzeichen, setze Flag zurück
            lastcharwasnumber = false;
        }

        // Prüfe ob aktuelles Zeichen ein Großbuchstabe ist (ASCII 65-90 sind A-Z)
        if((int)input[i] >= 65 && (int)input[i] <= 90)
        {
            // Wenn noch kein Y-Wert gesetzt wurde:
            // Konvertiere Buchstabe zu Zahl (A=0, B=1, etc.)
            if(!ywasWritten)
            {
               YXPos[0] = (int)input[i] - 65;
               ywasWritten = true;
            }
        }

        // Prüfe ob aktuelles Zeichen ein Kleinbuchstabe ist (ASCII 97-122 sind a-z)
        if((int)input[i] >= 97 && (int)input[i] <= 122)
        {
            // Wenn noch kein Y-Wert gesetzt wurde:
            // Konvertiere Buchstabe zu Zahl (a=0, b=1, etc.)
            if(!ywasWritten)
            {
               YXPos[0] = (int)input[i] - 97;
               ywasWritten = true;
            }
        }
    }

    // Gebe das Array mit den gefundenen Koordinaten zurück
    return YXPos;
}
Console.Write("Wie groß soll eine Spielbrettseite sein?: ");
int boardsize = Convert.ToInt32(Console.ReadLine());
int[,] board = new int[boardsize,boardsize];
int spieler = 2;
do
{
if(spieler == 1)
    spieler = 2;
else
    spieler = 1;
System.Console.WriteLine($"Spieler {spieler} du bist dran!");
DrawBoard(board);
System.Console.Write("Gebe die Koordinaten deines nächsten Zuges ein!:");


} while (!isWinner(board,movY,movX));