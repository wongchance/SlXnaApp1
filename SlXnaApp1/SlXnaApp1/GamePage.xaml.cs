using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Windows.Media.Animation;
using System.Threading;
using SlXnaApp1.Controls;


namespace SlXnaApp1
{
    public partial class GamePage : PhoneApplicationPage
    {
        //ContentManager contentManager;
        //GameTimer timer;
        //SpriteBatch spriteBatch;
        SlXnaApp1.Controls.AnimatedUserControl animateduserControl = new AnimatedUserControl();
        
        public GamePage()
        {
            InitializeComponent();

            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {


            base.OnNavigatedTo(e);


        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            // Stop the timer
            //timer.Stop();

            // Set the sharing mode of the graphics device to turn off XNA rendering
            SharedGraphicsDeviceManager.Current.GraphicsDevice.SetSharingMode(false);

            base.OnNavigatedFrom(e);
        }

        /// <summary>
        /// Allows the page to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        private void OnUpdate(object sender, GameTimerEventArgs e)
        {
            // TODO: Add your update logic here
            
        }

        /// <summary>
        /// Allows the page to draw itself.
        /// </summary>
        private void OnDraw(object sender, GameTimerEventArgs e)
        {
            //SharedGraphicsDeviceManager.Current.GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
        }

        private void Grid_Tap(object sender, GestureEventArgs e)
        {
            AnimationEye.Begin();
            

            ArcSegmentEnd.Size = new Size(35, 35);
            ArcSegmentLeft.Size = new Size(35, 35);
            AnimationMouthCat.Begin();
            AnimationMouthCat.Completed += new EventHandler(AnimationMouthCat_Completed);
        }


        private void AnimationMouthCat_Completed(object sender, EventArgs e)
        {
            ArcSegmentEnd.Size = new Size(30, 30);
            ArcSegmentLeft.Size = new Size(30, 30);
            ArcSegmentMid.Size = new Size(40, 40);
            ArcSegmentRight.Size = new Size(40, 40);


            AnimationMouthCat.AutoReverse = true;
                ArcSegmentEnd.Size = new Size(50,50);
                ArcSegmentLeft.Size = new Size(50, 50);
                ArcSegmentMid.Size = new Size(50, 50);
                ArcSegmentRight.Size = new Size(50, 50);
            
        }
    }
}