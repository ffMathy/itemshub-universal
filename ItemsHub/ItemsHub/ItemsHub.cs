using System.Collections;

// ReSharper disable once CheckNamespace
namespace Windows.UI.Xaml.Controls
{
    /// <summary>
    /// Represents a bindable template-driven Hub control.
    /// </summary>
    public class ItemsHub : Hub
    {

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.Register("ItemTemplate", typeof(DataTemplate), typeof(ItemsHub), new PropertyMetadata(null, ItemTemplateChanged));

        private static void ItemTemplateChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var hub = dependencyObject as ItemsHub;
            if (hub != null)
            {
                var template = e.NewValue as DataTemplate;
                if (template != null)
                {
                    foreach (var section in hub.Sections)
                    {
                        section.ContentTemplate = template;
                    }
                }
            }
        }

        public IList ItemsSource
        {
            get { return (IList)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IList), typeof(ItemsHub), new PropertyMetadata(null, ItemsSourceChanged));

        private static void ItemsSourceChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var hub = dependencyObject as ItemsHub;
            if (hub != null)
            {
                var items = e.NewValue as IList;
                if (items != null)
                {
                    hub.Sections.Clear();
                    foreach (var item in items)
                    {
                        var section = new HubSection {DataContext = item, Header = item};

                        var template = hub.ItemTemplate;
                        section.ContentTemplate = template;
                        hub.Sections.Add(section);
                    }
                }
            }
        }

    }
}
