#region usings
using SharedUI.Animations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
#endregion

namespace SharedUI
{
    public class Assets
    {

        #region Animations

        public static readonly DependencyProperty AnimationsProperty =
                    DependencyProperty.RegisterAttached("Animations", typeof(AnimationType), typeof(Assets),
                    new PropertyMetadata(AnimationType.None, OnAnimationsPropertyChanged));

        private static void OnAnimationsPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(sender)) return;
            var destinationElement = sender as FrameworkElement;
            if (destinationElement == null) return;
            var animationType = (AnimationType)e.NewValue;
            if (animationType == AnimationType.None) return;

            var obj = GetAnimationResource(animationType);
            if (obj is AnimationPlaceholder)
            {
                var placeholder = obj as AnimationPlaceholder;
                RoutedEvent routedEvent;
                var storyboard = ClonePlaceholderItems(placeholder, destinationElement, out routedEvent);
                var beginStoryboard = new BeginStoryboard();
                beginStoryboard.Storyboard = storyboard;
                var trigger = new EventTrigger(routedEvent);
                trigger.Actions.Add(beginStoryboard);
                destinationElement.Triggers.Add(trigger);
            }
        }

        private static object GetAnimationResource(AnimationType animationType)
        {
            return Application.Current.TryFindResource(animationType.ToString());
        }

        public static void SetAnimations(UIElement element, AnimationType value)
        {
            element.SetValue(AnimationsProperty, value);
        }

        public static AnimationType GetAnimations(UIElement element)
        {
            return (AnimationType)element.GetValue(AnimationsProperty);
        }

        internal static Storyboard ClonePlaceholderItems(AnimationPlaceholder placeholder, FrameworkElement destinationElement, out RoutedEvent routedEvent)
        {
            routedEvent = null;
            var orgTransformOrigin = placeholder.RenderTransformOrigin;
            var orgTransform = placeholder.RenderTransform;
            if (placeholder.Triggers.Count == 0) return null;
            var orgTrigger = placeholder.Triggers[0] as EventTrigger;
            if (orgTrigger == null) return null;
            routedEvent = orgTrigger.RoutedEvent;
            var orgBeginStoryboard = orgTrigger.Actions[0] as BeginStoryboard;
            if (orgBeginStoryboard == null) return null;
            var orgStoryboard = orgBeginStoryboard.Storyboard;
            destinationElement.RenderTransformOrigin = placeholder.RenderTransformOrigin;
            var transformation = orgTransform.Clone();
            destinationElement.RenderTransform = orgTransform.Clone();
            var storyboard = new Storyboard();
            foreach (var ani in orgStoryboard.Children)
            {
                storyboard.Children.Add(ani.Clone());
            }
            Storyboard.SetTarget(storyboard, destinationElement);
            return storyboard;
        }

        #endregion
    }

    #region AnimationPlaceholder

    public class AnimationPlaceholder : FrameworkElement
    {
    }

    #endregion

}
