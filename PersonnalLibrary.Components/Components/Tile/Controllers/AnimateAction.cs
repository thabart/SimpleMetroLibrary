using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace PersonnalLibrary.Components
{
    public class AnimateAction
    {
        public void InitPosition(
            StackPanel container,
            double translateY,
            double translateX)
        {
            TranslateTransform translateTransform = new TranslateTransform()
            {
                Y = translateY,
                X = translateX
            };

            container.RenderTransform = translateTransform;
        }

        public void InitAnimation(
            StackPanel container,
            int nbElements,
            double finalValue,
            string propertyPath,
            bool transitionY)
        {
            Storyboard story = new Storyboard();
            story.RepeatBehavior = RepeatBehavior.Forever;

            var begin = new TimeSpan(0, 0, 1);
            var duration = new Duration(new TimeSpan(0, 0, 2));
            var transition = new TimeSpan(0, 0, 1);
            var transitionValue = transitionY ? (container.RenderTransform as TranslateTransform).Y : (container.RenderTransform as TranslateTransform).X;

            for (var indice = 0; indice < nbElements; indice++)
            {
                if (indice > 0)
                {
                    transitionValue += finalValue;

                    DoubleAnimation animation = new DoubleAnimation()
                    {
                        To = transitionValue,
                        BeginTime = begin,
                        Duration = duration,
                        EasingFunction = new CubicEase()
                        {
                            EasingMode = EasingMode.EaseOut
                        }
                    };

                    begin += duration.TimeSpan + transition;

                    story.Children.Add(animation);

                    Storyboard.SetTarget(animation, container);
                    Storyboard.SetTargetProperty(animation, new PropertyPath(propertyPath));
                }
            }

            story.Duration = new Duration(begin + transition);
            story.Begin();
        }
    }
}
