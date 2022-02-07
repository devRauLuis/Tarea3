using Microsoft.EntityFrameworkCore;
using System.Linq;
using Tarea3.Entidades;

namespace Tarea3.DAL;

public class Context : DbContext
{
    public DbSet<Carrera> Carreras { get; set; }
    public DbSet<Estudiante> Estudiantes { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=Data/Data.db");
    }
}