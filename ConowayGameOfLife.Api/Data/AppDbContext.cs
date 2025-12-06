using Microsoft.EntityFrameworkCore;
using ConowayGameOfLife.Api.Models;

namespace ConowayGameOfLife.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<GameBoardEntity> GameBoards { get; set; }

    }
}
