public class GameOfLifeService
{
    public int Width { get; set; } = 20;
    public int Height { get; set; } = 20;
    public int PlayCount { get; set; } = 10;
    private bool _isPlaying;
    public bool IsPlaying
    {
        get => _isPlaying;
        set
        {
            if (_isPlaying != value)
            {
                _isPlaying = value;
                OnStateChanged?.Invoke();
            }
        }
    }
    public int CurrentPlayStep { get; set; } = 0;

    public bool[,] board = new bool[20, 20];

    public event Action? OnSaveRequested;
    public event Action? OnNextGenerationRequested;
    public event Action? OnPlayRequested;
    public event Action? OnClearRequested;
    public event Action? OnCreateRequested;

    //dla przycisku play
    public event Action? OnStateChanged;

    public void RequestSave() => OnSaveRequested?.Invoke();
    public void RequestNextGeneration() => OnNextGenerationRequested?.Invoke();
    public void RequestPlay() => OnPlayRequested?.Invoke();
    public void RequestClear() => OnClearRequested?.Invoke();
    public void RequestCreate() => OnCreateRequested?.Invoke();
    public void RequestChange() => OnStateChanged?.Invoke();



    public string SerializeBoard()
    {
        var chars = new List<char>();
        for (int y = 0; y < Height; y++)
            for (int x = 0; x < Width; x++)
                chars.Add(board[x, y] ? '1' : '0');

        return new string(chars.ToArray());
    }

    public void DeserializeBoard(string data)
    {
        board = new bool[Width, Height];
        int i = 0;

        for (int y = 0; y < Height; y++)
            for (int x = 0; x < Width; x++)
                board[x, y] = data[i++] == '1';

        OnStateChanged?.Invoke();
    }
    public static List<List<bool>> ToList2D(bool[,] array)
    {
        var h = array.GetLength(0);
        var w = array.GetLength(1);

        var result = new List<List<bool>>(h);

        for (int y = 0; y < h; y++)
        {
            var row = new List<bool>(w);
            for (int x = 0; x < w; x++)
                row.Add(array[y, x]);

            result.Add(row);
        }

        return result;
    }

    public static bool[,] FromList2D(List<List<bool>> list)
    {
        int h = list.Count;
        int w = list[0].Count;

        var result = new bool[h, w];

        for (int y = 0; y < h; y++)
            for (int x = 0; x < w; x++)
                result[y, x] = list[y][x];

        return result;
    }

}






