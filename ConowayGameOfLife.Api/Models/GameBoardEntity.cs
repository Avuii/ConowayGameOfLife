using System;

namespace ConowayGameOfLife.Api.Models
{
    public class GameBoardEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public int Width { get; set; }
        public int Height { get; set; }

        // plansza jako string
        public string BoardData { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
