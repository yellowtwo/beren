using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace Tetris
{
    class CountdownTimer
    {
        bool done;
        double initialTime;
        double curTime;
        double duration;

        public void Start(double x)
        {
            done = false;
            duration = x;
            initialTime = curTime;
        }

        public void Update(GameTime gameTime)
        {
            // [Brandon 4/24/2015] - Looks like GameTime.TotalRealTime was valid in XNA 3.0 but not 4.0.
            // curTime = gameTime.TotalRealTime.TotalSeconds;
            curTime = gameTime.TotalGameTime.TotalSeconds;
        }

        public bool Done()
        {
            if (curTime - initialTime >= duration)
            {
                done = true;
            }
            return done;
        }
    }
}


