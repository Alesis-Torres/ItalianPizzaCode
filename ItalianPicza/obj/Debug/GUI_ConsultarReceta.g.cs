﻿#pragma checksum "..\..\GUI_ConsultarReceta.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "14C10A97D3886CFDA2355C32B453BDCCEF6401C5F508617447009561DC158A51"
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
    /// GUI_ConsultarReceta
    /// </summary>
    public partial class GUI_ConsultarReceta : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 15 "..\..\GUI_ConsultarReceta.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView lvIngredientes;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\GUI_ConsultarReceta.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbNombreProducto;
        
        #line default
        #line hidden
        
        
        #line 79 "..\..\GUI_ConsultarReceta.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tbInstrucciones;
        
        #line default
        #line hidden
        
        
        #line 80 "..\..\GUI_ConsultarReceta.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAgregarReceta;
        
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
            System.Uri resourceLocater = new System.Uri("/ItalianPicza;component/gui_consultarreceta.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\GUI_ConsultarReceta.xaml"
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
            this.lvIngredientes = ((System.Windows.Controls.ListView)(target));
            return;
            case 2:
            this.lbNombreProducto = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            
            #line 75 "..\..\GUI_ConsultarReceta.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Regresar);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 76 "..\..\GUI_ConsultarReceta.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ModificarReceta);
            
            #line default
            #line hidden
            return;
            case 5:
            this.tbInstrucciones = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.btnAgregarReceta = ((System.Windows.Controls.Button)(target));
            
            #line 80 "..\..\GUI_ConsultarReceta.xaml"
            this.btnAgregarReceta.Click += new System.Windows.RoutedEventHandler(this.AgregarReceta);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

