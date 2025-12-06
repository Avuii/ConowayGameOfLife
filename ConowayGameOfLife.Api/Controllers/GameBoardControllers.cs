using Microsoft.AspNetCore.Mvc;
using ConowayGameOfLife.Api.Data;
using ConowayGameOfLife.Api.Models;
using Microsoft.EntityFrameworkCore;
using ConowayGameOfLife.Shared.Models;

namespace ConowayGameOfLife.Api.Controllers
{
    [ApiController]
    [Route("api/boards")]
    public class GameBoardController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GameBoardController(AppDbContext context)
        {
            _context = context;
        }

        // GET api/boards
        [HttpGet]
        public async Task<ActionResult<List<BoardSummaryDto>>> GetAll()
        {
            var list = await _context.GameBoards
                .OrderByDescending(b => b.CreatedAt)
                .Select(b => new BoardSummaryDto
                {
                    Id = b.Id,
                    Name = b.Name,
                    Width = b.Width,
                    Height = b.Height,
                    CreatedAt = b.CreatedAt
                })
                .ToListAsync();

            return Ok(list);
        }

        // GET api/boards/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<BoardDTo>> Get(int id)
        {
            var board = await _context.GameBoards.FindAsync(id);
            if (board == null)
                return NotFound();

            var cells = board.BoardData
                .Split(";")
                .Select(row => row.Split(",").Select(x => x == "1").ToList())
                .ToList();

            return Ok(new BoardDTo
            {
                Id = board.Id,
                Name = board.Name,
                Width = board.Width,
                Height = board.Height,
                Cells = cells
            });
        }

        // POST api/boards
        [HttpPost]
        public async Task<IActionResult> Save(BoardDTo dto)
        {
            var entity = new GameBoardEntity
            {
                Name = dto.Name,
                Width = dto.Width,
                Height = dto.Height,
                BoardData = string.Join(";", dto.Cells.Select(
                    row => string.Join(",", row.Select(c => c ? "1" : "0"))
                )),
                CreatedAt = DateTime.UtcNow
            };

            _context.GameBoards.Add(entity);
            await _context.SaveChangesAsync();

            return Ok(entity.Id);
        }

        // DELETE api/boards/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var board = await _context.GameBoards.FindAsync(id);
            if (board == null)
                return NotFound();

            _context.GameBoards.Remove(board);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}

