using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Markup;
using System.Windows.Threading;

namespace SlXnaApp1.Controls
{
    public class AnimatedUserControl : UserControl
    {
        // Fields
        private Rectangle animatedElement;
        private bool positionAnimating;
        private Storyboard positionAnimation;
        private TimeSpan positionAnimationTimespan = new TimeSpan(0, 0, 0, 0, 500);
        private SplineDoubleKeyFrame positionAnimationXKeyFrame;
        private SplineDoubleKeyFrame positionAnimationYKeyFrame;
        private bool sizeAnimating;
        private Storyboard sizeAnimation;
        private SplineDoubleKeyFrame sizeAnimationHeightKeyFrame;
        private TimeSpan sizeAnimationTimespan = new TimeSpan(0, 0, 0, 0, 500);
        private SplineDoubleKeyFrame sizeAnimationWidthKeyFrame;
        private DispatcherTimer timer;
        private bool widthAnimating;

        // Methods
        public AnimatedUserControl()
        {
            string str = "";
            Canvas canvas = XamlReader.Load(((((((((((str + "<Canvas xmlns='http://schemas.microsoft.com/client/2007' xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'>") + "  <Canvas.Resources>" + "      <Storyboard x:Key='sizeStoryboard' BeginTime='00:00:00'>") + "          <DoubleAnimationUsingKeyFrames Storyboard.TargetName='animatedElement' Storyboard.TargetProperty='(FrameworkElement.Width)'> " + "              <SplineDoubleKeyFrame Value='0' KeyTime='00:00:0.5' KeySpline='0.528,0,0.142,0.847' />") + "           </DoubleAnimationUsingKeyFrames>" + "          <DoubleAnimationUsingKeyFrames Storyboard.TargetName='animatedElement' Storyboard.TargetProperty='(FrameworkElement.Height)'> ") + "             <SplineDoubleKeyFrame Value='0' KeyTime='00:00:0.5' KeySpline='0.528,0,0.142,0.847' />" + "          </DoubleAnimationUsingKeyFrames>") + "      </Storyboard>" + "      <Storyboard x:Key='positionStoryboard' BeginTime='00:00:00'>") + "          <DoubleAnimationUsingKeyFrames Storyboard.TargetName='animatedElement' Storyboard.TargetProperty='(Canvas.Left)'> " + "              <SplineDoubleKeyFrame Value='0' KeyTime='00:00:0.5' KeySpline='0.528,0,0.142,0.847' />") + "          </DoubleAnimationUsingKeyFrames>" + "          <DoubleAnimationUsingKeyFrames Storyboard.TargetName='animatedElement' Storyboard.TargetProperty='(Canvas.Top)'> ") + "              <SplineDoubleKeyFrame Value='0' KeyTime='00:00:0.5' KeySpline='0.528,0,0.142,0.847' />" + "          </DoubleAnimationUsingKeyFrames>") + "      </Storyboard>" + "  </Canvas.Resources>") + "  <Rectangle x:Name='animatedElement' Height='0' Width='0' Canvas.Top='0' Canvas.Left='0' />" + "</Canvas>") as Canvas;
            this.animatedElement = canvas.Children[0] as Rectangle;
            this.sizeAnimation = canvas.Resources["sizeStoryboard"] as Storyboard;
            this.sizeAnimation.Completed += new EventHandler(this.SizeAnimation_Completed);
            this.positionAnimation = canvas.Resources["positionStoryboard"] as Storyboard;
            this.positionAnimation.Completed += new EventHandler(this.PositionAnimation_Completed);
            this.sizeAnimationWidthKeyFrame = ((DoubleAnimationUsingKeyFrames)this.sizeAnimation.Children[0]).KeyFrames[0] as SplineDoubleKeyFrame;
            this.sizeAnimationHeightKeyFrame = ((DoubleAnimationUsingKeyFrames)this.sizeAnimation.Children[1]).KeyFrames[0] as SplineDoubleKeyFrame;
            this.positionAnimationXKeyFrame = ((DoubleAnimationUsingKeyFrames)this.positionAnimation.Children[0]).KeyFrames[0] as SplineDoubleKeyFrame;
            this.positionAnimationYKeyFrame = ((DoubleAnimationUsingKeyFrames)this.positionAnimation.Children[1]).KeyFrames[0] as SplineDoubleKeyFrame;
            this.timer = new DispatcherTimer();
            this.timer.Tick += new EventHandler(this.Timer_Tick);
        }

        public void AnimatePosition(double positionX, double positionY)
        {
            if (this.animatedElement != null)
            {
                if (this.positionAnimating)
                {
                    this.positionAnimation.Pause();
                }
                Canvas.SetLeft(this.animatedElement, Canvas.GetLeft(this));
                Canvas.SetTop(this.animatedElement, Canvas.GetTop(this));
                if (base.Parent != null)
                {
                    this.positionAnimating = true;
                    this.positionAnimationXKeyFrame.Value = positionX;
                    this.positionAnimationYKeyFrame.Value = positionY;
                    this.positionAnimation.Begin();
                    this.timer.Start();
                }
                else
                {
                    Canvas.SetLeft(this, positionX);
                    Canvas.SetTop(this, positionY);
                }
            }
            else
            {
                Canvas.SetLeft(this, positionX);
                Canvas.SetTop(this, positionY);
            }
        }

        public void AnimateSize(double width, double height)
        {
            this.widthAnimating = false;
            if (this.animatedElement != null)
            {
                if (this.sizeAnimating)
                {
                    this.sizeAnimation.Pause();
                }
                this.animatedElement.Width = base.ActualWidth;
                this.animatedElement.Height = base.ActualHeight;
                if (base.Parent != null)
                {
                    this.sizeAnimating = true;
                    this.sizeAnimationWidthKeyFrame.Value = width;
                    this.sizeAnimationHeightKeyFrame.Value = height;
                    this.sizeAnimation.Begin();
                    this.timer.Start();
                }
                else
                {
                    base.Width = width;
                    base.Height = height;
                }
            }
            else
            {
                base.Width = width;
                base.Height = height;
            }
        }

        public void AnimateWidth(double width)
        {
            this.widthAnimating = true;
            if (this.animatedElement != null)
            {
                if (this.sizeAnimating)
                {
                    this.sizeAnimation.Pause();
                }
                this.animatedElement.Width = base.ActualWidth;
                if (base.Parent != null)
                {
                    this.sizeAnimating = true;
                    this.sizeAnimationWidthKeyFrame.Value = width;
                    this.sizeAnimation.Begin();
                    this.timer.Start();
                }
                else
                {
                    base.Width = width;
                }
            }
            else
            {
                base.Width = width;
            }
        }

        private void PositionAnimation_Completed(object sender, EventArgs e)
        {
            this.positionAnimating = false;
            Canvas.SetLeft(this, Canvas.GetLeft(this.animatedElement));
            Canvas.SetTop(this, Canvas.GetTop(this.animatedElement));
            if (!this.sizeAnimating && !this.positionAnimating)
            {
                this.timer.Stop();
            }
            this.PositionAnimationCompleted();
        }

        public virtual void PositionAnimationCompleted()
        {
        }

        private void SizeAnimation_Completed(object sender, EventArgs e)
        {
            this.sizeAnimating = false;
            base.Width = this.animatedElement.Width;
            if (!this.widthAnimating)
            {
                base.Height = this.animatedElement.Height;
            }
            else
            {
                this.widthAnimating = false;
            }
            if (!this.sizeAnimating && !this.positionAnimating)
            {
                this.timer.Stop();
            }
            this.SizeAnimationCompleted();
        }

        public virtual void SizeAnimationCompleted()
        {
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (this.sizeAnimating)
            {
                base.Width = this.animatedElement.Width;
                if (!this.widthAnimating)
                {
                    base.Height = this.animatedElement.Height;
                }
            }
            if (this.positionAnimating)
            {
                Canvas.SetLeft(this, Canvas.GetLeft(this.animatedElement));
                Canvas.SetTop(this, Canvas.GetTop(this.animatedElement));
            }
        }

        // Properties
        public TimeSpan PositionAnimationDuration
        {
            get
            {
                return this.positionAnimationTimespan;
            }
            set
            {
                this.positionAnimationTimespan = value;
                if (this.positionAnimationXKeyFrame != null)
                {
                    this.positionAnimationXKeyFrame.KeyTime = KeyTime.FromTimeSpan(this.positionAnimationTimespan);
                }
                if (this.positionAnimationYKeyFrame != null)
                {
                    this.positionAnimationYKeyFrame.KeyTime = KeyTime.FromTimeSpan(this.positionAnimationTimespan);
                }
            }
        }

        public TimeSpan SizeAnimationDuration
        {
            get
            {
                return this.sizeAnimationTimespan;
            }
            set
            {
                this.sizeAnimationTimespan = value;
                if (this.sizeAnimationWidthKeyFrame != null)
                {
                    this.sizeAnimationWidthKeyFrame.KeyTime = KeyTime.FromTimeSpan(this.sizeAnimationTimespan);
                }
                if (this.sizeAnimationHeightKeyFrame != null)
                {
                    this.sizeAnimationHeightKeyFrame.KeyTime = KeyTime.FromTimeSpan(this.sizeAnimationTimespan);
                }
            }
        }
    }
}
