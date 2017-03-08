using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Resources;
using Windows.ApplicationModel.Resources.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// The Templated Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234235

namespace ManagedPackage
{
    /// <summary>
    /// MyCustomControl
    /// </summary>
    public sealed class MyCustomControl : Control
    {
        /// <summary>
        /// MyString
        /// </summary>
        public string MyString
        {
            get { return (string)GetValue(MyStringProperty); }
            set { SetValue(MyStringProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyString.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty MyStringProperty =
            DependencyProperty.Register("MyString", typeof(string), typeof(MyCustomControl), new PropertyMetadata(0));



        /// <summary>
        /// MyCustomControl
        /// </summary>
        public MyCustomControl()
        {
            this.DefaultStyleKey = typeof(MyCustomControl);
            ResourceContext ctx = new Windows.ApplicationModel.Resources.Core.ResourceContext();
            ResourceMap libmap = ResourceManager.Current.MainResourceMap;
            MyString = libmap.GetSubtree("ManagedPackage").GetSubtree("Resources").GetValue("MyString", ctx).ValueAsString;
            
        }
    }
}
