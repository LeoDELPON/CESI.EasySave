﻿#pragma checksum "..\..\..\AddWorkWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "D062BF2E936D4FEE2C26E928BAB1EBEADA9807C3"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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
using WpfApp1;


namespace WpfApp1 {
    
    
    /// <summary>
    /// AddWorkWindow
    /// </summary>
    public partial class AddWorkWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\..\AddWorkWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label coucou;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\AddWorkWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox WorkNameTB;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\AddWorkWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox WorkSourceTB;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\AddWorkWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox WorkTargetTB;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\AddWorkWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox SaveTypeCB;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\AddWorkWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBoxItem dif;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\AddWorkWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBoxItem ful;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\AddWorkWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox isXor;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\AddWorkWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CancelBtn;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\AddWorkWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button OkBtn;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.1.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/WpfApp1;component/addworkwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\AddWorkWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.1.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.coucou = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.WorkNameTB = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.WorkSourceTB = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.WorkTargetTB = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.SaveTypeCB = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.dif = ((System.Windows.Controls.ComboBoxItem)(target));
            return;
            case 7:
            this.ful = ((System.Windows.Controls.ComboBoxItem)(target));
            return;
            case 8:
            this.isXor = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 9:
            
            #line 25 "..\..\..\AddWorkWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.CipherOptions);
            
            #line default
            #line hidden
            return;
            case 10:
            this.CancelBtn = ((System.Windows.Controls.Button)(target));
            
            #line 28 "..\..\..\AddWorkWindow.xaml"
            this.CancelBtn.Click += new System.Windows.RoutedEventHandler(this.CancelBtn_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.OkBtn = ((System.Windows.Controls.Button)(target));
            
            #line 29 "..\..\..\AddWorkWindow.xaml"
            this.OkBtn.Click += new System.Windows.RoutedEventHandler(this.OkBtn_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

