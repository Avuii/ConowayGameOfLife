using System;
namespace ConowayGameOfLife.Shared.Models
{
    public class BoardSummaryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Width { get; set; }
        public int Height { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}