using Services.SessionServiceReference;
using System.Windows;
using System.Windows.Controls;

namespace ScheduleModule
{
    public class ScheduleTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var slot = item as Slot;
            if (slot != null)
            {
                if (slot.IsBreak)
                {
                    return ((FrameworkElement)container).TryFindResource("breakTemplate") as DataTemplate;
                }
                else
                {
                    return ((FrameworkElement)container).TryFindResource("sessionTemplate") as DataTemplate;
                }
            }
            return base.SelectTemplate(item, container);
        }
    }
}
