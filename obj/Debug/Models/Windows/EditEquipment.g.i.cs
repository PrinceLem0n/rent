﻿#pragma checksum "..\..\..\..\Models\Windows\EditEquipment.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "AEC0B159AD42CD5D48A436743E9B29190EC6B52DFCEF7B3DFB162BC70B810766"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Rent.Models.Windows;
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


namespace Rent.Models.Windows {
    
    
    /// <summary>
    /// EditEquipment
    /// </summary>
    public partial class EditEquipment : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 21 "..\..\..\..\Models\Windows\EditEquipment.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Name_tb;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\..\Models\Windows\EditEquipment.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Count_tb;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\..\Models\Windows\EditEquipment.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Price_tb;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\..\Models\Windows\EditEquipment.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox Supplier_cb;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\..\Models\Windows\EditEquipment.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Exit_btn;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\..\Models\Windows\EditEquipment.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Save_btn;
        
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
            System.Uri resourceLocater = new System.Uri("/Rent;component/models/windows/editequipment.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Models\Windows\EditEquipment.xaml"
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
            
            #line 8 "..\..\..\..\Models\Windows\EditEquipment.xaml"
            ((Rent.Models.Windows.EditEquipment)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Name_tb = ((System.Windows.Controls.TextBox)(target));
            
            #line 21 "..\..\..\..\Models\Windows\EditEquipment.xaml"
            this.Name_tb.GotFocus += new System.Windows.RoutedEventHandler(this.Name_tb_GotFocus);
            
            #line default
            #line hidden
            
            #line 21 "..\..\..\..\Models\Windows\EditEquipment.xaml"
            this.Name_tb.LostFocus += new System.Windows.RoutedEventHandler(this.Name_tb_LostFocus);
            
            #line default
            #line hidden
            return;
            case 3:
            this.Count_tb = ((System.Windows.Controls.TextBox)(target));
            
            #line 22 "..\..\..\..\Models\Windows\EditEquipment.xaml"
            this.Count_tb.GotFocus += new System.Windows.RoutedEventHandler(this.Count_tb_GotFocus);
            
            #line default
            #line hidden
            
            #line 22 "..\..\..\..\Models\Windows\EditEquipment.xaml"
            this.Count_tb.LostFocus += new System.Windows.RoutedEventHandler(this.Count_tb_LostFocus);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Price_tb = ((System.Windows.Controls.TextBox)(target));
            
            #line 23 "..\..\..\..\Models\Windows\EditEquipment.xaml"
            this.Price_tb.GotFocus += new System.Windows.RoutedEventHandler(this.Price_tb_GotFocus);
            
            #line default
            #line hidden
            
            #line 23 "..\..\..\..\Models\Windows\EditEquipment.xaml"
            this.Price_tb.LostFocus += new System.Windows.RoutedEventHandler(this.Price_tb_LostFocus);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Supplier_cb = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.Exit_btn = ((System.Windows.Controls.Button)(target));
            
            #line 26 "..\..\..\..\Models\Windows\EditEquipment.xaml"
            this.Exit_btn.Click += new System.Windows.RoutedEventHandler(this.Exit_btn_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.Save_btn = ((System.Windows.Controls.Button)(target));
            
            #line 47 "..\..\..\..\Models\Windows\EditEquipment.xaml"
            this.Save_btn.Click += new System.Windows.RoutedEventHandler(this.Save_btn_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

