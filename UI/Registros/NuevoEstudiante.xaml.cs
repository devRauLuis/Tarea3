using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tarea3.BLL;
using Tarea3.Entidades;

namespace Tarea3.UI.Registros;

public partial class NuevoEstudiante : Window
{
    private int _id;
    private string _nombre;
    private string _email;
    private int _carreraId;
    private bool _activo;
    private List<Carrera> _carreras;
    public NuevoEstudiante()
    {
        InitializeComponent();
        SetCarrerasToComboBox();
    }

    private void SetCarrerasToComboBox()
    {
        _carreras = CarrerasBLL.GetList(c => true);
        CarrerasCB.ItemsSource = _carreras;
    }

    private void OnNuevoClick(object sender, EventArgs e)
    {
        ResetFields();
        SetEliminarBtn(false);
    }

    private void ResetFields()
    {
        IdTB.Text = "";
        NombreTB.Text = "";
        EmailTB.Text = "";
        CarrerasCB.SelectedIndex = -1;
        ActivoCheck.IsChecked = false;
    }

    private void OnGuardarClick(object sender, EventArgs e)
    {

        var nuevoEstudiante = new Estudiante()
        {
            EstudianteId = _id, NombreCompleto = _nombre, Email = _email, Activo = _activo, CarreraId = _carreraId
        };
        if (Validar(nuevoEstudiante))
            if (EstudianteBLL.Guardar(nuevoEstudiante))
            {
                MessageBox.Show("Guardado exitosamente!");
                this.Close();
            }
            else
            {
                MessageBox.Show("Ocurrio un error al guardar!");
            }
    }

    private void OnEliminarClick(object sender, EventArgs e)
    {
        if (EstudianteBLL.Eliminar((int) _id))
        {
            MessageBox.Show("Eliminado exitosamente!");
            ResetFields();
            SetEliminarBtn(false);
        }
        else
        {
            MessageBox.Show("Ha ocurrido un error al eliminar!");
        }
    }

    private void OnBuscarClick(object sender, EventArgs e)
    {
        _id = int.TryParse(IdTB.Text, out _id) ? _id : -1;
        if (_id > -1)
        {
            Estudiante? estudiante = EstudianteBLL.Buscar((int) _id);
            if (estudiante is null) MessageBox.Show("No ha sido encontrado");
            else
            {

                IdTB.Text = estudiante.EstudianteId + "";
                NombreTB.Text = estudiante.NombreCompleto;
                EmailTB.Text = estudiante.Email;
                CarrerasCB.SelectedIndex = _carreras.FindIndex(c => c.CarreraId == estudiante.CarreraId);
                ActivoCheck.IsChecked = estudiante.Activo;
                SetEliminarBtn(true);
            }
        }

    }

    private void SetEliminarBtn(bool isEnabled)
    {
        EliminarBtn.IsEnabled = isEnabled;
    }

    private void OnNombreTextChanged(object sender, EventArgs e)
    {
        _nombre = NombreTB.Text;
        var nombreLength = _nombre.Length;
        GuardarBtn.IsEnabled = nombreLength > 0 && nombreLength < 100;
    }

    private void OnEmailTextChanged(object sender, EventArgs e)
    {
        _email = EmailTB.Text;
        GuardarBtn.IsEnabled = _email.Length > 0;
    }
    private void OnCarrerasSelectionChanged(object sender, EventArgs e)
    {
        _carreraId = (CarrerasCB.SelectedItem as Carrera).CarreraId;
        GuardarBtn.IsEnabled = CarrerasCB.SelectedItem is not null;

    }

    private void OnActivoCheckedChanged(object sender, EventArgs e)
    {
        _activo = (bool) ActivoCheck.IsChecked;
    }

    private void OnIdTextChanged(object sender, EventArgs e)
    {
        var valido = int.TryParse(IdTB.Text, out _id);
        BuscarBtn.IsEnabled = valido;
        // EliminarBtn.IsEnabled = valido;
    }

    private bool Validar(Estudiante estudiante)
    {
        var nombre = estudiante.NombreCompleto;
        var id = estudiante.CarreraId;
        var carreraId = estudiante.CarreraId;
        var listaMsjs = new List<string>();
        var mensaje = "";

        if (carreraId < 1)
        {
            listaMsjs.Add("Seleccione una carrera valida.");
        }

        if (!Utils.IsValidEmail(_email))
        {
            listaMsjs.Add("El correo no es valido.");
        }

        if (nombre.Length > 100 || nombre.Length < 0)
        {
            listaMsjs.Add($"Nombre del estudiante es muy {(nombre.Length < 0 ? "corto" : "largo")}");
        }

        if (nombre.Any(c => char.IsDigit(c)))
        {
            listaMsjs.Add("El nombre no puede contener digitos numericos.");
        }


        if (listaMsjs.Count > 0)
        {
            foreach (var msg in listaMsjs)
            {
                mensaje += "\n" + msg;
            }

            MessageBox.Show(mensaje);
        }

        return !(listaMsjs.Count > 0);
    }
}