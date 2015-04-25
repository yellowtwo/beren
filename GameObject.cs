/*
 * Holds all the required information for all
 * objects in the game, including how and where
 * to draw it. Holds the name of the
 * sprite but does NOT load the sprite; loading
 * must be done by the game engine.
 */

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
using System.IO;

namespace Beren
{
    public class GameObject
    {
        public Texture2D sprite;
        public Vector2 position;
        public float rotation;
        public Vector2 center;
        public Vector2 velocity;
        public bool alive;
        public int width;
        public int height;
        public String spriteString;
        public float layerDepth;
        public Rectangle rectOnMap;
        public ContentManager content;
        public bool solid;
        public String name;

        /// <summary>
        /// Creates a GameObject with attributes equal to the rectangle's and the sprite at assetPath.
        /// </summary>
        /// <param name="content">Your game's ContentManager.</param>
        /// <param name="assetPath">The path of the sprite.</param>
        /// <param name="rectangle">Rectangle to be used to set the object's path and size to.</param>
        public GameObject(ContentManager content, String assetPath, Rectangle rectangle)
        {
            rotation = 0.0f;
            position = new Vector2((float)rectangle.X, (float)rectangle.Y);
            sprite = content.Load<Texture2D>(assetPath);
            velocity = Vector2.Zero;
            alive = false;
            width = rectangle.Width;
            height = rectangle.Height;
            center = new Vector2(position.X + width / 2, position.Y + height / 2);
            spriteString = assetPath;
            layerDepth = 1;
            rectOnMap = rectangle;
            this.content = content;
            solid = false;
        }

        /// <summary>
        /// Creates a GameObject with attributes equal to the rectangle's and the sprite at assetPath.
        /// </summary>
        /// <param name="content">Your game's ContentManager.</param>
        /// <param name="assetPath">The path of the sprite.</param>
        /// <param name="rectangle">Rectangle to be used to set the object's path and size to.</param>
        /// <param name="layerDepth">Layer depth value of range 0 to 1 (0 is front, 1 is back).</param>
        public GameObject(ContentManager content, String assetPath, Rectangle rectangle,
            float layerDepth, bool solid)
        {
            rotation = 0.0f;
            position = new Vector2((float)rectangle.X, (float)rectangle.Y);
            sprite = content.Load<Texture2D>(assetPath);
            velocity = Vector2.Zero;
            alive = false;
            width = rectangle.Width;
            height = rectangle.Height;
            center = new Vector2(position.X + width / 2, position.Y + height / 2);
            spriteString = assetPath;
            this.layerDepth = layerDepth;
            rectOnMap = rectangle;
            this.content = content;
            this.solid = solid;
        }

        public void Update()
        {
            //rectOnMap = new Rectangle((int)position.X, (int)position.Y, width, height);
        }

        public void SetSprite(String assetPath)
        {
            sprite = content.Load<Texture2D>(assetPath);
        }
    }

    public enum Direction
    {
        Up, Down, Left, Right
    }
}
