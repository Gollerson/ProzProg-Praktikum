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

Console.Write("Wie groß soll eine Spielbrettseite sein?: ");
int boardsize = Convert.ToInt32(Console.ReadLine());
int[,] array = new int[boardsize,boardsize];
int[,] array2 = {{0,0,2},{1,2,1},{2,1,0}}; 
DrawBoard(array);