using System;
using System.Collections.Generic;

namespace ConowayGameOfLife.Shared.Models
{
    public class BoardDTo
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Width { get; set; }
        public int Height { get; set; }
        public List<List<bool>> Cells { get; set; } = new List<List<bool>>();

    }

}