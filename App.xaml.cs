using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Tarea3
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_DispatcherUnhandledException(object sender,
            System.Windows.Threading.DispatcherUnhandledExceptionEventArgs args)
        {
            MessageBox.Show(
                $"Ocurrio un error:( {args.Exception.Message}", "Error no controlado", MessageBoxButton.OK,
                MessageBoxImage.Error);
            args.Handled = true;
        }
    }
}