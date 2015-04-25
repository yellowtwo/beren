/* 
 * Contains all of the specified map's
 * GameObjects and its width and height.
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
	public class Map
    {
        public int mapNumber;
		public GameObject[,] mapObjects;
		public int sizeX;
		public int sizeY;
        public Vector2 mapPosition;

        public Map()
        {
            mapNumber = 0;
            sizeX = 0;
            sizeY = 0;
            mapObjects = new GameObject[sizeX, sizeY];
            mapPosition = Vector2.Zero;
        }
		
		public Map(int mapNum, int width, int height)
		{
			mapNumber = mapNum;
			sizeX = width;
			sizeY = height;
			mapObjects = new GameObject[sizeX, sizeY];
            mapPosition = Vector2.Zero;
		}

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            foreach (GameObject mapPiece in mapObjects)
            {
                spriteBatch.Draw(mapPiece.sprite, Translate(mapPiece.rectOnMap, camera), null,
                    Color.White, mapPiece.rotation, Vector2.Zero, SpriteEffects.None,
                    mapPiece.layerDepth);
            }
        }

        /*public void ShiftMap(Vector2 shiftAmount)
        {
            for (int gameObjectY = 0; gameObjectY < sizeY; gameObjectY++)
            {
                for (int gameObjectX = 0; gameObjectX < sizeX; gameObjectX++)
                {
                    mapObjects[gameObjectX, gameObjectY].position += shiftAmount;
                    mapObjects[gameObjectX, gameObjectY].Update();
                }

            }
            mapPosition -= shiftAmount;
        }*/

        public bool MapObjectIsSolid(Vector2 mapObjectPosition)
        {
            foreach (GameObject mapPiece in mapObjects)
            {
                if ((mapPiece.position.X == mapObjectPosition.X) &&
                    (mapPiece.position.Y == mapObjectPosition.Y))
                    if (mapPiece.solid)
                        return true;
                    else
                        return false;
            }

            return false;
        }

        /// <summary>
        /// Translates a rectangle in map coordinates to a rectangle in screen coordinates.
        /// </summary>
        /// <param name="rectOnMap">Rectangle to translate.</param>
        /// <param name="camera"></param>
        /// <returns></returns>
        public Rectangle Translate(Rectangle rectOnMap, Camera camera)
        {
            return new Rectangle((int)(rectOnMap.X + 350 - camera.position.X),
                (int)(rectOnMap.Y + 250 - camera.position.Y), rectOnMap.Width,
                rectOnMap.Height);
        }
    }
}