var boards = new List<int[][]>(); // Board Id, Y, X

bool hasBoardGotCall(int[][] board, int call)
{
    for (var y = 0; y < 5; ++y)
    {
        for (var x = 0; x < 5; ++x)
        {
            if (board[y][x] == call)
            {
                board[y][x] = -board[y][x] - 1;
                return true;
            }
        }
    }

    return false;
}
bool checkBoard(int[][] board)
{
    for (var y = 0; y < 5; ++y)
    {
        if (board[y].All(c => c < 0))
        {
            return true;
        }
    }
    for (var x = 0; x < 5; ++x)
    {
        if (board.All(c => c[x] < 0))
        {
            return true;
        }
    }
    return false;
}
int sumBoard(int[][] board)
{
    var score = board.Sum(r => r.Sum(c => Math.Max(0, c)));
    return score;
}

var calls = Console.In.ReadLine().Split(',').Select(int.Parse).ToArray();
Console.In.ReadLine();

// Build boards
var ln = Console.In.ReadLine();
int boardId = 0, x = 0, y = 0;
while (ln != null)
{
    int[][] board;
    if (y == 0)
    {
        board = new int[5][];
        boardId = boards.Count;
        boards.Add(board);
    }
    else
    {
        board = boards[boardId];
    }

    board[y++] = ln.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
    if (y > 4)
    {
        y = 0;
        Console.In.ReadLine();
    }
    
    ln = Console.In.ReadLine();
}

// Run calls
var result = 0;
var doneBoards = 0;
foreach (var call in calls)
{
    foreach (var board in boards)
    {
        if (!checkBoard(board) && hasBoardGotCall(board, call) && checkBoard(board))
        {
            if (++doneBoards >= boards.Count)
            {
                result = sumBoard(board) * call;
                break;
            }
        }
    }
    if (result > 0) { break; }
}

Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(boards));
Console.WriteLine("> " + result);
