﻿#pragma checksum "..\..\GUI_Proveedores.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "FE51053CFDDEC48B235C424895DBF63CE3769EA63B18F7E261B15ED45D1B8BE3"
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
    /// GUI_Proveedores
    /// </summary>
    public partial class GUI_Proveedores : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 88 "..\..\GUI_Proveedores.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView lvProveedores;
        
        #line default
        #line hidden
        
        
        #line 103 "..\..\GUI_Proveedores.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GridViewColumn colImagen;
        
        #line default
        #line hidden
        
        
        #line 111 "..\..\GUI_Proveedores.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GridViewColumn colNombre;
        
        #line default
        #line hidden
        
        
        #line 123 "..\..\GUI_Proveedores.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GridViewColumn colDescripcion;
        
        #line default
        #line hidden
        
        
        #line 137 "..\..\GUI_Proveedores.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GridViewColumn colModificar;
        
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
            System.Uri resourceLocater = new System.Uri("/ItalianPicza;component/gui_proveedores.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\GUI_Proveedores.xaml"
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
            this.lvProveedores = ((System.Windows.Controls.ListView)(target));
            
            #line 88 "..\..\GUI_Proveedores.xaml"
            this.lvProveedores.Loaded += new System.Windows.RoutedEventHandler(this.LvProveedores_Loaded);
            
            #line default
            #line hidden
            
            #line 88 "..\..\GUI_Proveedores.xaml"
            this.lvProveedores.SizeChanged += new System.Windows.SizeChangedEventHandler(this.LvProveedores_SizeChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.colImagen = ((System.Windows.Controls.GridViewColumn)(target));
            return;
            case 3:
            this.colNombre = ((System.Windows.Controls.GridViewColumn)(target));
            return;
            case 4:
            this.colDescripcion = ((System.Windows.Controls.GridViewColumn)(target));
            return;
            case 5:
            this.colModificar = ((System.Windows.Controls.GridViewColumn)(target));
            return;
            case 6:
            
            #line 148 "..\..\GUI_Proveedores.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AgregarProveedor);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

