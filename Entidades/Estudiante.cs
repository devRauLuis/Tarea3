using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Tarea3.Entidades;

public class Estudiante
{
    [Key]
    public int EstudianteId { get; set; }

    [Required]
    [MaxLength(100)]
    public string? NombreCompleto { get; set; }

    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    [Required]
    public int? CarreraId { get; set; }
    
    [Required]
    public bool? Activo { get; set; }
}