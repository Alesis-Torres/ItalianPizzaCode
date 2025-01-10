using ItalianPicza.DatabaseModel.DAO_s;
using ItalianPicza.DatabaseModel.DataBaseMapping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ItalianPicza
{
    public partial class GUI_Finanzas : Page
    {
        private List<entradaextraordinaria> entradas;
        private List<salidaextraordinaria> salidas;

        public GUI_Finanzas()
        {
            InitializeComponent();
            CargarTransaccionesEntrada();
            CargarTransaccionesSalida();
            cbTipoTransaccion.SelectedIndex = 0;
        }

        private void RealizarBalanceDiario(object sender, RoutedEventArgs e)
        {

        }

        private void AgregarTransaccion(object sender, RoutedEventArgs e)
        {
            VentanaPrincipal.CambiarPagina(new GUI_RegistrarTransaccionFinanciera());
        }

        private void CargarTransaccionesEntrada()
        {
            TransaccionesDAO transaccionesDAO = new TransaccionesDAO();

            try
            {
                entradas = transaccionesDAO.ObtenerEntradasExtraordinarias();
                lvEntradas.ItemsSource = entradas;
            }
            catch (EntityException)
            {
                GestorCuadroDialogo.MostrarError
                           ("No hay conexión con la base de datos, por favor, intentelo más tarde",
                           "Sin conexión a la base de datos");
            }
        }

        private void cuadroFecha_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime? fechaSeleccionada = cuadroFecha.SelectedDate;

            if (fechaSeleccionada.HasValue)
            {
                // Filtrar entradas
                var entradasFiltradas = entradas
                    .Where(entrada => entrada.fechaRealizacion?.Date == fechaSeleccionada.Value.Date)
                    .ToList();
                lvEntradas.ItemsSource = entradasFiltradas;

                // Filtrar salidas
                var salidasFiltradas = salidas
                    .Where(salida => salida.fechaRealizacion?.Date == fechaSeleccionada.Value.Date)
                    .ToList();
                lvSalidas.ItemsSource = salidasFiltradas;
            }
            else
            {
                lvEntradas.ItemsSource = entradas;
                lvSalidas.ItemsSource = salidas;
            }
        }


        private void CargarTransaccionesSalida()
        {
            TransaccionesDAO transaccionesDAO = new TransaccionesDAO();

            try
            {
                salidas = transaccionesDAO.ObtenerSalidasExtraordinarias();
                lvSalidas.ItemsSource = salidas;
            }
            catch (EntityException)
            {
                GestorCuadroDialogo.MostrarError
                           ("No hay conexión con la base de datos, por favor, intentelo más tarde",
                           "Sin conexión a la base de datos");
            }
        }

        private void filtrarTransacciones(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            string seleccion = (comboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (seleccion == "Entrada")
            {
                listaEntradas.Visibility = Visibility.Visible;
                listaSalidas.Visibility = Visibility.Collapsed;
            }
            else if (seleccion == "Salida")
            {
                listaEntradas.Visibility = Visibility.Collapsed;
                listaSalidas.Visibility = Visibility.Visible;
            }
        }

    }
}
