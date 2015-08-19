using System;
using System.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ItemsHub
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

        public DataTemplateSelector ItemTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(ItemTemplateSelectorProperty); }
            set { SetValue(ItemTemplateSelectorProperty, value); }
        }

        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.Register("ItemTemplate", typeof(DataTemplate), typeof(ItemsHub), new PropertyMetadata(null, ItemTemplateChanged));

        public static readonly DependencyProperty ItemTemplateSelectorProperty =
            DependencyProperty.Register("ItemTemplateSelector", typeof(DataTemplateSelector), typeof(ItemsHub), new PropertyMetadata(null, ItemTemplateSelectorChanged));

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

        private static void ItemTemplateSelectorChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        public DataTemplate ItemHeaderTemplate
        {
            get { return (DataTemplate)GetValue(ItemHeaderTemplateProperty); }
            set { SetValue(ItemHeaderTemplateProperty, value); }
        }

        public static readonly DependencyProperty ItemHeaderTemplateProperty =
            DependencyProperty.Register("ItemHeaderTemplate", typeof(DataTemplate), typeof(ItemsHub), new PropertyMetadata(null, ItemHeaderTemplateChanged));

        private static void ItemHeaderTemplateChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var hub = dependencyObject as ItemsHub;
            if (hub != null)
            {
                var template = e.NewValue as DataTemplate;
                if (template != null)
                {
                    foreach (var section in hub.Sections)
                    {
                        section.HeaderTemplate = template;
                    }
                }
            }
        }

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(ItemsHub), new PropertyMetadata(null, ItemsSourceChanged));

        private static void ItemsSourceChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var hub = dependencyObject as ItemsHub;
            if (hub != null)
            {
                var items = e.NewValue as IEnumerable;
                if (items != null)
                {
                    hub.Sections.Clear();
                    foreach (var item in items)
                    {
                        var section = new HubSection {DataContext = item, Header = item};

                        if (hub.ItemTemplateSelector != null)
                        {
                            section.ContentTemplate = hub.ItemTemplateSelector.SelectTemplate(item, dependencyObject);
                        }
                        else
                        {
                            section.ContentTemplate = hub.ItemTemplate;
                        }
                        section.HeaderTemplate = hub.ItemHeaderTemplate;

                        hub.Sections.Add(section);
                    }
                }
            }
        }

    }
}
