/* TO DO:
 * Debug LoadDialogs so that it properly loads NPC dialogs. Problem with regex usage?
 * */

using System;
using System.Collections;
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
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Rectangle viewportRect;
        Texture2D backgroundTexture;
        Character player;
        Map currentMap;
        Dialog dialog;
        Texture2D dialogTexture;
        KeyboardState previousKeyboardState = Keyboard.GetState(PlayerIndex.One);
        bool showDialog = false;
        int dialogIndex = 0;
        const int defaultGameObjectWidth = 50;
        const int defaultGameObjectHeight = 50;
        Camera camera;
        GameTime gameTime;
        CharacterManager characterManager;
        MapManager mapManager;
        int currentMapNumber = 0;
        Texture2D[] letterTextures = new Texture2D[100];


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            viewportRect = new Rectangle(0, 0, GraphicsDevice.Viewport.Width,
                GraphicsDevice.Viewport.Height);

            backgroundTexture = Content.Load<Texture2D>("Sprites\\background");
            
            player = new Character(Content, "Sprites\\MaleHuman", new Rectangle(
                viewportRect.Width / 2 - defaultGameObjectWidth,  // Place in the center of the map and
                    viewportRect.Height / 2 - defaultGameObjectHeight, // compensate for the object's size.
                    defaultGameObjectWidth, defaultGameObjectHeight), 0.2f,
                    true);
            player.positionOnMap = new Vector2(400, 300);

            //npc = new GameObject(Content, "Sprites\\Jelly", new Rectangle((int)(player.position.X - player.width),
            //        (int)(player.position.Y), defaultGameObjectWidth, defaultGameObjectHeight));
            //npc.layerDepth = 0.2f;

            dialogTexture = Content.Load<Texture2D>("Sprites\\DialogBox");
            dialog = new Dialog();
            dialog.LoadDialogs("Content\\Data\\dialogdata.txt");

            mapManager = new MapManager(Content);
            mapManager.LoadMaps("Content\\Data\\maps.txt");
            currentMap = mapManager.SetCurrentMap(currentMapNumber);

            camera = new Camera(player);
            camera.Update(gameTime);

            characterManager = new CharacterManager(player);

            // Code 97 through 122
            for (int i = 65; i < 91; i++)
            {
                // [Brandon 4/24/2015] - Port from XNA 3.0 to 4.0
                // Texture2D letter = Texture2D.FromFile(GraphicsDevice, "Content\\Sprites\\" + (char)i + ".jpg");
                FileStream stream = new FileStream("Content\\Sprites\\" + (char)i + ".jpg", FileMode.Open);
                Texture2D letter = Texture2D.FromStream(GraphicsDevice, stream);
                letterTextures[i] = letter;
                stream.Close();
            }
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            camera.Update(gameTime);

#if !XBOX
            KeyboardState keyboardState = Keyboard.GetState(PlayerIndex.One);
            if (keyboardState.IsKeyDown(Keys.Space) &&
                !previousKeyboardState.IsKeyDown(Keys.Space))
            {
                if (characterManager.IsFacingNpc(mapManager))
                {
                    if (showDialog)
                        showDialog = false;
                    else
                        showDialog = true;
                    //dialogIndex = mapManager.getDialogIndex(player, mapManager);
                    //dialog = new Dialog();
                }
                //if ((npc.position.X == player.position.X + defaultGameObjectWidth) ||
                //    (npc.position.X == player.position.X - defaultGameObjectWidth) ||
                //    (npc.position.Y == player.position.Y + defaultGameObjectHeight) ||
                //    (npc.position.Y == player.position.Y - defaultGameObjectHeight))
                //{
                //    if (showDialog)
                //        showDialog = false;
                //    else
                //        showDialog = true;
                //    dialogIndex = 0;
                //    dialog = new Dialog();
                //}
                //Console.WriteLine(player.positionOnMap.ToString());
                //Console.WriteLine(camera.position);
                //Console.WriteLine(mapManager.currentMap.mapObjects[0, 0].rectOnMap.ToString());
                //Console.WriteLine(mapManager.currentMap.Translate(mapManager.currentMap.mapObjects[0, 0].rectOnMap, camera).ToString());
                //Console.WriteLine(mapManager.currentMap.Translate(mapManager.currentMap.mapObjects[16, 12].rectOnMap, camera).ToString());
            }

            if (keyboardState.IsKeyDown(Keys.Up) &&
                !previousKeyboardState.IsKeyDown(Keys.Up))
            {                
                characterManager.move(Direction.Up, mapManager);
            }

            if (keyboardState.IsKeyDown(Keys.Down) &&
                !previousKeyboardState.IsKeyDown(Keys.Down))
            {
                characterManager.move(Direction.Down, mapManager);
            }

            if (keyboardState.IsKeyDown(Keys.Left) &&
                !previousKeyboardState.IsKeyDown(Keys.Left))
            {
                characterManager.move(Direction.Left, mapManager);
            }

            if (keyboardState.IsKeyDown(Keys.Right) &&
                !previousKeyboardState.IsKeyDown(Keys.Right))
            {
                characterManager.move(Direction.Right, mapManager);
            }

            previousKeyboardState = keyboardState;
#endif

            camera.Update(gameTime);
            //mapManager.UpdatePosition(camera);
            characterManager.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // [Brandon 4/24/2015] - Port XNA 3.0 to 4.0
            // spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.BackToFront, SaveStateMode.SaveState);
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            spriteBatch.Draw(backgroundTexture, viewportRect, null, Color.White,
                0, Vector2.Zero, SpriteEffects.None, 1);

            spriteBatch.Draw(player.sprite, player.rectOnMap, null, Color.White,
                player.rotation, Vector2.Zero,
                SpriteEffects.None, player.layerDepth);

            //spriteBatch.Draw(npc.sprite, npc.rectOnMap, null, Color.White,
            //    npc.rotation, Vector2.Zero, SpriteEffects.None, npc.layerDepth);

            if (showDialog)
                dialog.Draw(spriteBatch, dialogTexture, letterTextures, GraphicsDevice.Viewport.Height, characterManager.NameOfFacingNpc(mapManager));

            if (mapManager.currentMap != null)
                mapManager.currentMap.Draw(spriteBatch, camera);
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
