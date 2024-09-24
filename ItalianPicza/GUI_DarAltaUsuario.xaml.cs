﻿using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace ItalianPicza
{

    public partial class GUI_DarAltaUsuario : Page
    {
        public GUI_DarAltaUsuario()
        {
            InitializeComponent();
        }

        private void CerrarSesion(object objetoOrigen, MouseButtonEventArgs evento)
        {
            VentanaPrincipal.CambiarPagina(new GUI_InicioSesion());
        }

        private void GuardarUsuario(object sender, RoutedEventArgs e)
        {
            //DAO Dar alta usuario
            GestorCuadroDialogo.MostrarInformacion
                ("Usuario registrado de manera exitosa en el sistema", 
                "Usuario dado de alta");
            VentanaPrincipal.CambiarPagina(new GUI_Usuarios());
        }

        private void Cancelar(object sender, RoutedEventArgs e)
        {
            VentanaPrincipal.CambiarPagina(new GUI_Usuarios());
        }

        private void SeleccionarImagen(object sender, RoutedEventArgs e)
        {
            // Crear un diálogo para seleccionar archivos
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Archivos de imagen (*.png;*.jpg)|*.png;*.jpg",
                Multiselect = false // Permitir solo una imagen
            };

            // Verificar si el usuario selecciona una imagen
            if (openFileDialog.ShowDialog() == true)
            {
                // Obtener la ruta del archivo seleccionado
                string filePath = openFileDialog.FileName;

                try
                {
                    // Cargar la imagen seleccionada en el control Image
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(filePath);
                    bitmap.CacheOption = BitmapCacheOption.OnLoad; // Cargar la imagen inmediatamente
                    bitmap.EndInit();

                    imagenUsuario.Source = bitmap; // Asignar la imagen al control
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar la imagen: " + ex.Message);
                }
            }
        }

    }
}
