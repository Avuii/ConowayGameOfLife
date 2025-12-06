using System.Net.Http.Json;
using ConowayGameOfLife.Shared.Models;
public class GameOfLifeApiClient
{
    private readonly HttpClient _http;

    public GameOfLifeApiClient(HttpClient http)
    {
        _http = http;
    }

    // Zapis planszy
    public async Task SaveBoardAsync(BoardDTo board)
    {
        await _http.PostAsJsonAsync("api/boards", board);
    }
    // Lista zapisanych plansz
    public async Task<List<BoardSummaryDto>> GetBoardsAsync()
    {
        var result = await _http.GetFromJsonAsync<List<BoardSummaryDto>>("api/boards");
        return result ?? new List<BoardSummaryDto>();
    }

    // Wczytanie jednej planszy
    public async Task<BoardDTo> LoadBoardAsync(int id)
    {
        return await _http.GetFromJsonAsync<BoardDTo>($"api/boards/{id}");
    }

    // Usuwanie planszy
    public async Task DeleteBoardAsync(int id)
    {
        await _http.DeleteAsync($"api/boards/{id}");
    }
}
