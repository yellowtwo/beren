/*
 * Manager for the Map class.
 */

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
    class MapManager
    {
        public int numMaps;
        public Map[] maps;
        public Map currentMap;
        private ContentManager content;

        public MapManager(ContentManager content)
        {
            this.content = content;
        }

        public void LoadMaps(String filePath)
        {
            StreamReader reader = new StreamReader(filePath);
            Map map = new Map();
            char spriteLetter;
            int sizeX, sizeY, mapNumber;

            numMaps = System.Convert.ToInt32(reader.ReadLine());
            maps = new Map[numMaps];

            Console.WriteLine("Loading map statistics...");
            for (int currentMapIndex = 0; currentMapIndex < numMaps; currentMapIndex++)
            {
                mapNumber = System.Convert.ToInt32(reader.ReadLine());
                sizeX = System.Convert.ToInt32(reader.ReadLine());
                sizeY = System.Convert.ToInt32(reader.ReadLine());
                map = new Map(mapNumber, sizeX, sizeY);

                Console.WriteLine("Loading map information...");
                for (int y = 0; y < map.sizeY; y++)
                {
                    for (int x = 0; x < map.sizeX; x++)
                    {
                        spriteLetter = System.Convert.ToChar(reader.Read());

                        switch (spriteLetter)
                        {
                            case 't':
                                map.mapObjects[x, y] = new GameObject(content, "Sprites\\tree",
                                    new Rectangle(x * 50, y * 50, 50, 50),
                                    0.5f, false);
                                break;
                            case 'w':
                                map.mapObjects[x, y] = new GameObject(content, "Sprites\\wall1",
                                    new Rectangle(x * 50, y * 50, 50, 50),
                                    0.5f, true);
                                break;
                            case 'e':
                                map.mapObjects[x, y] = new GameObject(content, "Sprites\\empty",
                                    new Rectangle(x * 50, y * 50, 50, 50),
                                    0.5f, false);
                                break;
                            case 'n':
                                map.mapObjects[x, y] = new GameObject(content, "Sprites\\Jelly",
                                    new Rectangle(x * 50, y * 50, 50, 50),
                                    0.5f, true);
                                spriteLetter = System.Convert.ToChar(reader.Peek());
                                String npcName = "";
                                if (spriteLetter.Equals('*'))
                                {
                                    spriteLetter = System.Convert.ToChar(reader.Read());
                                    spriteLetter = System.Convert.ToChar(reader.Read());
                                    while (!spriteLetter.Equals('*'))
                                    {
                                        npcName += spriteLetter.ToString();
                                        spriteLetter = System.Convert.ToChar(reader.Read());
                                    }
                                }
                                map.mapObjects[x, y].name = npcName;
                                break;
                            default:
                                map.mapObjects[x, y] = new GameObject(content, "Sprites\\empty",
                                    new Rectangle(x * 50, y * 50, 50, 50),
                                    0.5f, false);
                                break;
                        }
                    }
                }

                maps[currentMapIndex] = map;
            }
        }

        public Map SetCurrentMap(int mapNumber)
        {
            for (int i = 0; i < numMaps; i++)
            {
                if (maps[i].mapNumber == mapNumber)
                {
                    currentMap = maps[i];
                    return currentMap;
                }
            }

            return null;
        }

        /*public void UpdatePosition(Camera camera)
        {
            Vector2 positionChange = currentMap.mapPosition - camera.position;

            currentMap.ShiftMap(positionChange);
        }*/

        public bool ObjectIsSolid(Vector2 objectPosition)
        {
            return currentMap.MapObjectIsSolid(objectPosition);
        }
    }
}
