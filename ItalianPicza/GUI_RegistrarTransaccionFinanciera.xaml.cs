using System.Windows.Controls;
using System.Windows;
using System;
using Seguridad;
using ItalianPicza.DatabaseModel.DAO_s;
using ItalianPicza.DatabaseModel.DataBaseMapping;
using System.Data;
using System.Windows.Input;

namespace ItalianPicza
{
    public partial class GUI_RegistrarTransaccionFinanciera : Page
    {
        private string descripcion, cantidad;
        private decimal cantidadDecimal;
        private DateTime soloFecha;
        private cortecaja caja;
        TransaccionesDAO transaccionesDAO = new TransaccionesDAO();

        public GUI_RegistrarTransaccionFinanciera()
        {
            InitializeComponent();
        }

        private void cuadroTextoCantidad_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            // Permitir solo números y un punto decimal
            e.Handled = !System.Text.RegularExpressions.Regex.IsMatch(e.Text, @"[\d\.]");
        }

        private void Cancelar(object sender, RoutedEventArgs e)
        {
            VentanaPrincipal.CambiarPagina(new GUI_Finanzas());
        }

        private void RegistrarTransaccion(object sender, RoutedEventArgs e)
        {
            descripcion = cuadroTextoDescripcion.Text.Trim();
            cantidad = cuadroTextoCantidad.Text.Trim();
            Decimal.TryParse(cantidad, out cantidadDecimal);

            if (!ExistenCamposVacíos() && !ExistenDatosInvalidos() && ValidarFormatoCantidad(cantidad))
            {
                DateTime fechaSeleccionada = cuadroFecha.SelectedDate.Value;
                soloFecha = fechaSeleccionada.Date;
                ComboBoxItem selectedItem = cbTipoTransaccion.SelectedItem as ComboBoxItem;

                try
                {
                    caja = new cortecaja();
                    caja = transaccionesDAO.ObtenerCorteCajaActual();
                }
                catch (EntityException)
                {
                    GestorCuadroDialogo.MostrarError(
                        "No hay conexión con la base de datos, por favor, intentelo más tarde",
                        "Sin conexión a la base de datos");
                }

                if (selectedItem.Content.ToString() == "Entrada")
                {
                    RegistrarEntrada();
                }
                else
                {
                    RegistrarSalida();
                }
            }
        }

        private void RegistrarEntrada()
        {
            try
            {
                TransaccionesDAO transaccionesDAO = new TransaccionesDAO();

                entradaextraordinaria entrada = new entradaextraordinaria()
                {
                    cantidad = cantidadDecimal,
                    descripcion = descripcion,
                    fechaRealizacion = soloFecha,
                    idEmpleado = VentanaPrincipal.empleado,
                    idCorteCaja = caja.idCorteCaja
                };

                int resultado = transaccionesDAO.RegistrarTransaccionEntrada(entrada);

                if (resultado > 0)
                {
                    bool actualizado = transaccionesDAO.ActualizarBalanceCorteCajaEntrada(caja.idCorteCaja, cantidadDecimal);

                    if (actualizado)
                    {
                        GestorCuadroDialogo.MostrarInformacion(
                          "Transacción financiera registrada correctamente!",
                          "Registro exitoso");
                        VentanaPrincipal.CambiarPagina(new GUI_Finanzas());
                    }
                    else
                    {
                        GestorCuadroDialogo.MostrarError(
                           "No se pudo actualizar el balance del corte de caja.",
                           "Error al actualizar balance");
                    }
                }
                else
                {
                    GestorCuadroDialogo.MostrarError(
                        "No se pudo registrar la entrada",
                        "Error al registrar la entrada");
                }
            }
            catch (EntityException)
            {
                GestorCuadroDialogo.MostrarError(
                    "No hay conexión con la base de datos, por favor, intentelo más tarde",
                    "Sin conexión a la base de datos");
            }
        }

        private void RegistrarSalida()
        {
            try
            {
                caja = transaccionesDAO.ObtenerCorteCajaActual();

                salidaextraordinaria salida = new salidaextraordinaria()
                {
                    cantidad = cantidadDecimal,
                    descripcion = descripcion,
                    fechaRealizacion = soloFecha,
                    idEmpleado = VentanaPrincipal.empleado,
                    idCorteCaja = caja.idCorteCaja
                };

                if(caja.balance >= cantidadDecimal)
                {
                    int resultado = transaccionesDAO.RegistrarTransaccionSalida(salida);

                    if (resultado > 0)
                    {
                        bool actualizado = transaccionesDAO.ActualizarBalanceCorteCajaSalida(caja.idCorteCaja, cantidadDecimal);

                        if (actualizado)
                        {
                            GestorCuadroDialogo.MostrarInformacion(
                              "Transacción financiera registrada correctamente!",
                              "Registro exitoso");
                            VentanaPrincipal.CambiarPagina(new GUI_Finanzas());
                        }
                        else
                        {
                            GestorCuadroDialogo.MostrarError(
                               "No se pudo actualizar el balance del corte de caja.",
                               "Error al actualizar balance");
                        }
                    }
                    else
                    {
                        GestorCuadroDialogo.MostrarError(
                            "No se pudo registrar la entrada",
                            "Error al registrar la entrada");
                    }
                }
                else
                {
                    GestorCuadroDialogo.MostrarAdvertencia(
                        "No se pudo actualizar el balance del corte de caja debido a que la cantidad actual de la caja no es suficiente para realizar la salida.",
                        "Cantidad insuficiente");
                }

            }
            catch (EntityException)
            {
                GestorCuadroDialogo.MostrarError(
                    "No hay conexión con la base de datos, por favor, intentelo más tarde",
                    "Sin conexión a la base de datos");
            }
        }

        #region Validaciones
        private bool ExistenCamposVacíos()
        {
            bool existenCamposVacios = false;

            if (ValidarDatos.EsCadenaVacia(descripcion) || ValidarDatos.EsCadenaVacia(cantidad) || cbTipoTransaccion.SelectedValue == null
                || cuadroFecha.SelectedDate == null)
            {
                existenCamposVacios = true;
                GestorCuadroDialogo.MostrarAdvertencia(
                "Existen campos que están vacíos, por favor, ingrese toda la información solicitada.",
                "Campos vacíos");
            }

            return existenCamposVacios;
        }

        private bool ExistenDatosInvalidos()
        {
            bool existenCamposVacios = false;

            if (cantidadDecimal <= 0)
            {
                existenCamposVacios = true;
                GestorCuadroDialogo.MostrarAdvertencia(
                "La cantidad es inválida, por favor, revise la cantidad ingresada.",
                "Cantidad inválida");
            }

            return existenCamposVacios;
        }

        private bool ValidarFormatoCantidad(string cantidad)
        {
            // Patrón para 20 dígitos antes del punto y hasta 2 después del punto
            string patron = @"^\d{1,20}(\.\d{1,2})?$";

            // Validar el texto contra el patrón
            if (!System.Text.RegularExpressions.Regex.IsMatch(cantidad, patron))
            {
                // Mostrar mensaje de error si no cumple el formato
                GestorCuadroDialogo.MostrarAdvertencia(
                    "La cantidad ingresada no es válida. Asegúrese de que tenga hasta 20 dígitos antes del punto y solo 2 después.",
                    "Formato de cantidad inválido");
                return false;
            }

            return true;
        }


        #endregion

    }
}
