﻿#pragma checksum "..\..\..\..\Pages\GlownyPage.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "452657F0BBF20F664FD844AB9BF351E2DBE9FCE1"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Autobusy.UI.Pages;
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


namespace Autobusy.UI.Pages {
    
    
    /// <summary>
    /// GlownyPage
    /// </summary>
    public partial class GlownyPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 23 "..\..\..\..\Pages\GlownyPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DyspozytorButton;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\..\Pages\GlownyPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button PlanistaButton;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\..\Pages\GlownyPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ZarzadcaButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Autobusy.UI;component/pages/glownypage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Pages\GlownyPage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.DyspozytorButton = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\..\..\Pages\GlownyPage.xaml"
            this.DyspozytorButton.Click += new System.Windows.RoutedEventHandler(this.DyspozytorButton_OnClick);
            
            #line default
            #line hidden
            return;
            case 2:
            this.PlanistaButton = ((System.Windows.Controls.Button)(target));
            
            #line 24 "..\..\..\..\Pages\GlownyPage.xaml"
            this.PlanistaButton.Click += new System.Windows.RoutedEventHandler(this.PlanistaButton_OnClick);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ZarzadcaButton = ((System.Windows.Controls.Button)(target));
            
            #line 25 "..\..\..\..\Pages\GlownyPage.xaml"
            this.ZarzadcaButton.Click += new System.Windows.RoutedEventHandler(this.ZarzadcaButton_OnClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
