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

public partial class NuevaCarrera : Window
{
    private bool _buscado;
    private int _id;
    private string _nombre;
    public NuevaCarrera()
    {
        InitializeComponent();
    }

    private void OnNuevoClick(object sender, EventArgs e)
    {
        ResetFields();
        SetEliminarBtn(false);
    }

    private void ResetFields()
    {
        IdTextBox.Text = "";
        NombreTextBox.Text = "";
    }
    private void OnGuardarClick(object sender, EventArgs e)
    {

        var nuevaCarrera = new Carrera() {CarreraId = _id, Nombre = _nombre};
        if (Validar(nuevaCarrera))
            if (CarrerasBLL.Guardar(nuevaCarrera))
            {
                MessageBox.Show("Guardado exitosamente!");
                this.Close();
            }
            else
            {
                MessageBox.Show("Ocurrio un error al guardar!");
            }
        else MessageBox.Show("Nombre invalido");
    }

    private void OnEliminarClick(object sender, EventArgs e)
    {
        if (CarrerasBLL.Eliminar((int) _id))
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
        _id = int.TryParse(IdTextBox.Text, out _id) ? _id : -1;
        if (_id > -1)
        {
            Carrera? carrera = CarrerasBLL.Buscar((int) _id);
            if (carrera is null) MessageBox.Show("No ha sido encontrado");
            else
            {
                // CarreraCamposPanel.DataContext = carrera;
                IdTextBox.Text = carrera.CarreraId + "";
                NombreTextBox.Text = carrera.Nombre;
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
        _nombre = NombreTextBox.Text;

        var nombreLength = _nombre.Length;
        GuardarBtn.IsEnabled = nombreLength > 0 && nombreLength < 100;
    }

    private void OnIdTextChanged(object sender, EventArgs e)
    {
        var valido = int.TryParse(IdTextBox.Text, out _id);
        BuscarBtn.IsEnabled = valido;
        // EliminarBtn.IsEnabled = valido;
    }

    private bool Validar(Carrera carrera)
    {
        var nombre = carrera.Nombre;
        var id = carrera.CarreraId;
        return nombre.Length < 100 && nombre.Length > 0 && !nombre.Any(c => char.IsDigit(c)) && id > -1;
    }
}