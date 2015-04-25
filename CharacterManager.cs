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
    class CharacterManager
    {
        Character character;

        public CharacterManager(Character character) { this.character = character; }

        public bool move(Direction directionToMove, MapManager mapManager)
        {
            Vector2 desiredPosition;

            switch (directionToMove)
            {
                case Direction.Up:
                    desiredPosition = character.positionOnMap -
                        (new Vector2(0, character.height));
                    character.SetDirectionFacing(Direction.Up);
                    break;
                case Direction.Down:
                    desiredPosition = character.positionOnMap +
                        (new Vector2(0, character.height));
                    character.SetDirectionFacing(Direction.Down);
                    break;
                case Direction.Left:
                    desiredPosition = character.positionOnMap -
                        (new Vector2(character.width, 0));
                    character.SetDirectionFacing(Direction.Left);
                    break;
                case Direction.Right:
                    desiredPosition = character.positionOnMap +
                        (new Vector2(character.width, 0));
                    character.SetDirectionFacing(Direction.Right);
                    break;
                default:
                    return false;
            }

            //if (mapManager.ObjectIsSolid(desiredPosition))
            //{
            //    character.Update();
            //    return false;
            //}
            //else
            if (CanMove(directionToMove, character.positionOnMap, mapManager))
            {
                
                character.positionOnMap = desiredPosition;
                character.Update();
                return true;
            }
            return false;
        }

        public bool CanMove(Direction directionToMove, Vector2 positionOnMap, MapManager mapManager)
        {

            Map map = mapManager.currentMap;
            GameObject[,] mapObjects = map.mapObjects;
            Vector2 newPositionOnMap = positionOnMap;
            switch (directionToMove)
            {
                case Direction.Up:
                    newPositionOnMap.Y -= character.height;
                    break;
                case Direction.Down:
                    newPositionOnMap.Y += character.height;
                    break;
                case Direction.Left:
                    newPositionOnMap.X -= character.width;
                    break;
                case Direction.Right:
                    newPositionOnMap.X += character.width;
                    break;   
            }
            
            return Conflicts(mapObjects, newPositionOnMap, character);
        }
        bool Conflicts(GameObject[,] mapObjects, Vector2 positionOnMap, Character character)
        {            
            foreach (GameObject mapPiece in mapObjects)
            {
                if (Conflicts(mapPiece, positionOnMap, character))
                {
                    return false;
                }

            }
            return true;
        }
     

        bool Conflicts(GameObject mapPiece, Vector2 positionOnMap, Character character)
        {
            bool result = false;
            
            Rectangle rect = new Rectangle((int)positionOnMap.X, (int)positionOnMap.Y, character.width * 1, character.height * 1);
            mapPiece.rectOnMap.Intersects(ref rect, out result);
            if (result && mapPiece.solid)
            {
                return true;
            }
            return false;            
        }

        public bool IsFacingNpc(MapManager mapManager)
        {
            foreach (GameObject mapPiece in mapManager.currentMap.mapObjects)
            {
                if (mapPiece.spriteString.Contains("Jelly")) {
                    if (character.directionFacing.Equals(Direction.Up))
                    {
                        if ((mapPiece.rectOnMap.X == character.positionOnMap.X) && (mapPiece.rectOnMap.Y == character.positionOnMap.Y - 50))
                        {
                            return true;
                        }
                    }
                    else if (character.directionFacing.Equals(Direction.Down))
                    {
                        if ((mapPiece.rectOnMap.X == character.positionOnMap.X) && (mapPiece.rectOnMap.Y == character.positionOnMap.Y + 50))
                        {
                            return true;
                        }
                    }
                    else if (character.directionFacing.Equals(Direction.Left))
                    {
                        if ((mapPiece.rectOnMap.X == character.positionOnMap.X - 50) && (mapPiece.rectOnMap.Y == character.positionOnMap.Y))
                        {
                            return true;
                        }
                    }
                    else if (character.directionFacing.Equals(Direction.Right))
                    {
                        if ((mapPiece.rectOnMap.X == character.positionOnMap.X + 50) && (mapPiece.rectOnMap.Y == character.positionOnMap.Y))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public String NameOfFacingNpc(MapManager mapManager)
        {
            foreach (GameObject mapPiece in mapManager.currentMap.mapObjects)
            {
                if (mapPiece.spriteString.Contains("Jelly"))
                {
                    if (character.directionFacing.Equals(Direction.Up))
                    {
                        if ((mapPiece.rectOnMap.X == character.positionOnMap.X) && (mapPiece.rectOnMap.Y == character.positionOnMap.Y - 50))
                        {
                            return mapPiece.name;
                        }
                    }
                    else if (character.directionFacing.Equals(Direction.Down))
                    {
                        if ((mapPiece.rectOnMap.X == character.positionOnMap.X) && (mapPiece.rectOnMap.Y == character.positionOnMap.Y + 50))
                        {
                            return mapPiece.name;
                        }
                    }
                    else if (character.directionFacing.Equals(Direction.Left))
                    {
                        if ((mapPiece.rectOnMap.X == character.positionOnMap.X - 50) && (mapPiece.rectOnMap.Y == character.positionOnMap.Y))
                        {
                            return mapPiece.name;
                        }
                    }
                    else if (character.directionFacing.Equals(Direction.Right))
                    {
                        if ((mapPiece.rectOnMap.X == character.positionOnMap.X + 50) && (mapPiece.rectOnMap.Y == character.positionOnMap.Y))
                        {
                            return mapPiece.name;
                        }
                    }
                }
            }
            return null;
        }

        public void Update()
        {
            character.Update();
        }
    }
}
