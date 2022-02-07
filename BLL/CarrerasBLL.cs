using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Tarea3.DAL;
using Tarea3.Entidades;

namespace Tarea3.BLL;

public class CarrerasBLL
{
    public static bool Existe(int id)
    {
        using (var context = new Context())
        {
            bool encontrado = false;
            try
            {
                encontrado = context.Carreras.Any(c => c.CarreraId == id);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
                throw;
            }

            return encontrado;
        }
    }

    public static bool Guardar(Carrera carrera)
    {
        carrera.Nombre = carrera.Nombre.ToUpper();
        return !Existe(carrera.CarreraId) ? Insertar(carrera) : Modificar(carrera);
    }

    private static bool Insertar(Carrera carrera)
    {
        var paso = false;
        using (var context = new Context())
        {
            try
            {
                context.Carreras.Add(carrera);
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

    private static bool Modificar(Carrera carrera)
    {
        var paso = false;
        using (var context = new Context())
        {
            try
            {
                context.Entry(carrera).State = EntityState.Modified;
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

    public static bool Eliminar(int id)
    {
        bool paso = false;
        using (var context = new Context())
        {
            var carrera = Buscar(id);
            if (carrera is not null)
            {
                context.Carreras.Remove(carrera);
                paso = context.SaveChanges() > 0;
            }
        }

        return paso;
    }

    public static Carrera? Buscar(int id)
    {
        using (var context = new Context())
        {
            return context.Carreras.Find(id);
        }
    }

    public static List<Carrera> GetList(Expression<Func<Carrera, bool>> criterio)
    {
        using (var context = new Context())
        {
            try
            {
                return context.Carreras.Where(criterio).ToList();
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
                throw;
            }
        }
    }
}