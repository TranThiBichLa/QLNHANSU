﻿#pragma checksum "..\..\ThongKe.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "A91553E3671CFDFD3CBE28335FA3CCDEFC641587E4BAA497097AD0C0BCE787B9"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using LiveCharts.Wpf;
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


namespace QLNHANSU {
    
    
    /// <summary>
    /// ThongKe
    /// </summary>
    public partial class ThongKe : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 25 "..\..\ThongKe.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbNamCoBan;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\ThongKe.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LiveCharts.Wpf.CartesianChart chartCoBan;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\ThongKe.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbNamViTri;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\ThongKe.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbViTri;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\ThongKe.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LiveCharts.Wpf.CartesianChart chartViTri;
        
        #line default
        #line hidden
        
        
        #line 83 "..\..\ThongKe.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbNamLuong;
        
        #line default
        #line hidden
        
        
        #line 90 "..\..\ThongKe.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LiveCharts.Wpf.CartesianChart chartLuong;
        
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
            System.Uri resourceLocater = new System.Uri("/QLNHANSU;component/thongke.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\ThongKe.xaml"
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
            this.cbNamCoBan = ((System.Windows.Controls.ComboBox)(target));
            
            #line 25 "..\..\ThongKe.xaml"
            this.cbNamCoBan.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cbNamCoBan_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.chartCoBan = ((LiveCharts.Wpf.CartesianChart)(target));
            return;
            case 3:
            this.cbNamViTri = ((System.Windows.Controls.ComboBox)(target));
            
            #line 52 "..\..\ThongKe.xaml"
            this.cbNamViTri.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cbNamViTri_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.cbViTri = ((System.Windows.Controls.ComboBox)(target));
            
            #line 61 "..\..\ThongKe.xaml"
            this.cbViTri.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cbViTri_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.chartViTri = ((LiveCharts.Wpf.CartesianChart)(target));
            return;
            case 6:
            this.cbNamLuong = ((System.Windows.Controls.ComboBox)(target));
            
            #line 83 "..\..\ThongKe.xaml"
            this.cbNamLuong.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cbNamLuong_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.chartLuong = ((LiveCharts.Wpf.CartesianChart)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

