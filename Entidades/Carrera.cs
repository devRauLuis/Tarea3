using System.Linq;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Tarea3.Entidades;

public class Carrera
{
    [Key]
    public int CarreraId { get; set; }

    [Required]
    [MaxLength(100)]
    public string? Nombre { get; set; }
}