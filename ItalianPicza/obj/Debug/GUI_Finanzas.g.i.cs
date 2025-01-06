﻿#pragma checksum "..\..\GUI_Finanzas.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "4B8F0FAE40F2828D0601B5C486856C016888184D909B3AA074F0F4B9C8391938"
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
    /// GUI_Finanzas
    /// </summary>
    public partial class GUI_Finanzas : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 19 "..\..\GUI_Finanzas.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbTipoTransaccion;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\GUI_Finanzas.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker cuadroFecha;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\GUI_Finanzas.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid listaEntradas;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\GUI_Finanzas.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView lvEntradas;
        
        #line default
        #line hidden
        
        
        #line 114 "..\..\GUI_Finanzas.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid listaSalidas;
        
        #line default
        #line hidden
        
        
        #line 121 "..\..\GUI_Finanzas.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView lvSalidas;
        
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
            System.Uri resourceLocater = new System.Uri("/ItalianPicza;component/gui_finanzas.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\GUI_Finanzas.xaml"
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
            this.cbTipoTransaccion = ((System.Windows.Controls.ComboBox)(target));
            
            #line 20 "..\..\GUI_Finanzas.xaml"
            this.cbTipoTransaccion.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.filtrarTransacciones);
            
            #line default
            #line hidden
            return;
            case 2:
            this.cuadroFecha = ((System.Windows.Controls.DatePicker)(target));
            
            #line 36 "..\..\GUI_Finanzas.xaml"
            this.cuadroFecha.SelectedDateChanged += new System.EventHandler<System.Windows.Controls.SelectionChangedEventArgs>(this.cuadroFecha_SelectedDateChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.listaEntradas = ((System.Windows.Controls.Grid)(target));
            return;
            case 4:
            this.lvEntradas = ((System.Windows.Controls.ListView)(target));
            return;
            case 5:
            this.listaSalidas = ((System.Windows.Controls.Grid)(target));
            return;
            case 6:
            this.lvSalidas = ((System.Windows.Controls.ListView)(target));
            return;
            case 7:
            
            #line 189 "..\..\GUI_Finanzas.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.RealizarBalanceDiario);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 208 "..\..\GUI_Finanzas.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AgregarTransaccion);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
