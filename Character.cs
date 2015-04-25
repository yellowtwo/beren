using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using System.IO;

namespace Beren
{
    public class Character : GameObject
    {
        public Direction directionFacing;
        public Vector2 positionOnMap;

        public Character(ContentManager content, String assetPath, Rectangle rectangle):
            base(content, assetPath, rectangle) { }

        public Character(ContentManager content, String assetPath, Rectangle rectangle,
            float layerDepth, bool solid): base(content, assetPath, rectangle,
            layerDepth, solid) { }

        public void SetDirectionFacing(Direction directionToFace)
        {
            String assetPath;

            directionFacing = directionToFace;

            switch (directionToFace)
            {
                case Direction.Up:
                    assetPath = "Sprites\\MaleHumanUp";
                    break;
                case Direction.Down:
                    assetPath = "Sprites\\MaleHumanDown";
                    break;
                case Direction.Left:
                    assetPath = "Sprites\\MaleHuman";
                    break;
                case Direction.Right:
                    assetPath = "Sprites\\MaleHumanRight";
                    break;
                default:
                    assetPath = "Sprites\\MaleHuman";
                    break;
            }

            SetSprite(assetPath);
        }

        public void Update()
        {
            base.Update();
            positionOnMap = new Vector2(positionOnMap.X, positionOnMap.Y);
        }
    }
}
