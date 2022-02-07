using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Tarea3.DAL;
using Tarea3.Entidades;

namespace Tarea3.BLL;

public class EstudianteBLL
{
    public static bool Existe(int id)
    {
        using (var context = new Context())
        {
            bool encontrado = false;
            try
            {
                encontrado = context.Estudiantes.Any(e => e.EstudianteId == id);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
                throw;
            }

            return encontrado;
        }
    }

    public static bool Guardar(Estudiante estudiante)
    {
        estudiante.NombreCompleto = estudiante.NombreCompleto.ToUpper();
        estudiante.Email = estudiante.Email.ToLower();
        return !Existe(estudiante.EstudianteId) ? Insertar(estudiante) : Modificar(estudiante);
    }

    private static bool Insertar(Estudiante estudiante)
    {
        var paso = false;
        using (var context = new Context())
        {
            try
            {
                context.Estudiantes.Add(estudiante);
                paso = context.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
                throw;
            }
        }

        return paso;
    }

    private static bool Modificar(Estudiante estudiante)
    {
        var paso = false;
        using (var context = new Context())
        {
            try
            {
                context.Entry(estudiante).State = EntityState.Modified;
                paso = context.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
                throw;
            }
        }

        return paso;
    }

    public static bool Eliminar(int? id)
    {
        if (id is null) return false;

        using (var context = new Context())
        {
            var estudiante = Buscar(id);
            if (estudiante is null) return false;
            context.Estudiantes.Remove(estudiante);
            context.SaveChanges();
            return true;
        }
    }

    public static Estudiante? Buscar(int? id)
    {
        using (var context = new Context())
        {
            return context.Estudiantes.Where(e => e.EstudianteId == id).FirstOrDefault();
        }
    }

    public static List<Estudiante> GetList(Expression<Func<Estudiante, bool>> criterio)
    {
        using (var context = new Context())
        {
            try
            {
                return context.Estudiantes.Where(criterio).ToList();
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
                throw;
            }
        }
    }
}