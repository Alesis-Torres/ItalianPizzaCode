﻿#pragma checksum "..\..\GUI_ModificarUsuario.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "8525DA5FD1BE6DABE3C99A76DEE5C4C35D9DFD977948CEA7352694D985CF09E6"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using ItalianPicza;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace ItalianPicza {
    
    
    /// <summary>
    /// GUI_ModificarUsuario
    /// </summary>
    public partial class GUI_ModificarUsuario : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 26 "..\..\GUI_ModificarUsuario.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox cuadroTextoNombre;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\GUI_ModificarUsuario.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox cuadroTextoApellidoPaterno;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\GUI_ModificarUsuario.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox cuadroTextoApellidoMaterno;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\GUI_ModificarUsuario.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox cuadroTextoNombreUsuario;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\GUI_ModificarUsuario.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox cuadroTextoTelefono;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\GUI_ModificarUsuario.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox cuadroTextoCorreo;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\GUI_ModificarUsuario.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbRol;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\GUI_ModificarUsuario.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image imagenUsuario;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ItalianPicza;component/gui_modificarusuario.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\GUI_ModificarUsuario.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.cuadroTextoNombre = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.cuadroTextoApellidoPaterno = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.cuadroTextoApellidoMaterno = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.cuadroTextoNombreUsuario = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.cuadroTextoTelefono = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.cuadroTextoCorreo = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.cbRol = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 8:
            this.imagenUsuario = ((System.Windows.Controls.Image)(target));
            return;
            case 9:
            
            #line 68 "..\..\GUI_ModificarUsuario.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.seleccionarImagen);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 91 "..\..\GUI_ModificarUsuario.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Cancelar);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 110 "..\..\GUI_ModificarUsuario.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Guardar);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

