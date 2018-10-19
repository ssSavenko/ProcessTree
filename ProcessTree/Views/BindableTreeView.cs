using System.Windows;
using System.Windows.Controls;

namespace ProcessTree.Views
{
    internal sealed class BindableTreeView : TreeView
    {
        public static readonly DependencyProperty bindableItemSelected =
            DependencyProperty.Register(nameof(BindableSelectedItem), 
                typeof(object), 
                typeof(BindableTreeView), 
                new PropertyMetadata(null));

        public BindableTreeView()
        {
            SelectedItemChanged += (sender, e) => BindableSelectedItem = SelectedItem;
        }

        public object BindableSelectedItem
        {
            get { return GetValue(bindableItemSelected); }
            set { SetValue(bindableItemSelected, value); }
        }    
    }
}