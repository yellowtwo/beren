using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    class Dialog
    {
        Hashtable dialogs = new Hashtable();

        public Dialog()
        {
        }

        public String GetDialog(String npcName)
        {
            if (dialogs.ContainsKey(npcName))
                return (String)dialogs[npcName];
            else
                return null;
        }

        public void LoadDialogs(String filePath)
        {
            StreamReader dataFile = new StreamReader(filePath);
            String npcName, npcDialog, lineFeed;

            lineFeed = dataFile.ReadLine();
            while (!lineFeed.Equals("EOF"))
            {
                Match match = Regex.Match(lineFeed, @"([^=]*)=>([^~]+)~", RegexOptions.IgnoreCase);
                for (int i = 1; i < match.Groups.Count; i++)
                {
                    npcName = match.Groups[i].Value;
                    npcDialog = match.Groups[i + 1].Value;
                    dialogs.Add(npcName, npcDialog);
                }
                lineFeed = dataFile.ReadLine();
            }
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D dialogTexture, Texture2D[] letters, int viewPortHeight, String npcName)
        {
            spriteBatch.Draw(dialogTexture, new Rectangle(0, viewPortHeight - 300, 800, 300), null, Color.White, 0, new Vector2(0, 0), SpriteEffects.None, (float).1);

            // Print the dialog message.
            if (GetDialog(npcName) != null)
            {
                Char[] dialogText = GetDialog(npcName).ToCharArray();
                for (int i = 0; i < dialogText.GetLength(0) && !dialogText[i].Equals('~'); i++)
                {
                    spriteBatch.Draw(letters[Convert.ToInt64(dialogText[i])], new Rectangle(i * 30 + 295, (viewPortHeight - 175), 30, 30), null, Color.White, 0, new Vector2(0,0), SpriteEffects.None, 0);
                }
            }
        }
    }
}
