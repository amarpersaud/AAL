using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using Newtonsoft.Json;

using MonoGame.CExt.Input;
using MonoGame.CExt.Sprites;
using MonoGame.CExt.Utility;
using MonoGame.CExt.UI;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json.Linq;

namespace MonoGame.CExt.Utility
{

    public class ResourceHandler
    {
        /// <summary>
        /// Content manager
        /// </summary>
        public ContentManager _content { get; private set; }

        /// <summary>
        /// Sprite batch for drawing 
        /// </summary>
        public SpriteBatch _sb { get; private set; }

        /// <summary>
        /// Input helper for handling input
        /// </summary>
        public InputHelper _ih { get; private set; }

        /// <summary>
        /// Frame counter for counting framerate
        /// </summary>
        public FrameCounter _fc { get; private set; }

        /// <summary>
        /// Screen settings, resolution 
        /// </summary>
        public ScreenSettings ScreenSettings { get; private set; }

        /// <summary>
        /// Dictionary of sprites
        /// </summary>
        public Dictionary<string, Sprite> Sprites = new Dictionary<string, Sprite>();

        /// <summary>
        /// Dictionary of fonts
        /// </summary>
        public Dictionary<string, SpriteFont> Fonts = new Dictionary<string, SpriteFont>();

        /// <summary>
        /// Json serializer settings to allow serialization with inherited classes
        /// </summary>
        public JsonSerializerSettings JsonSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
            TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Full,
            Formatting = Formatting.Indented
        };

        public ResourceHandler(ContentManager content, SpriteBatch sb, InputHelper ih, FrameCounter fc, ScreenSettings screenSettings)
        {
            this._content = content;
            this._sb = sb;
            this._ih = ih;
            this._fc = fc;
            this.ScreenSettings = screenSettings;
        }

        /// <summary>
        /// Get sprite using file name. 
        /// </summary>
        /// <param name="name">Name of sprite</param>
        /// <returns>Sprite if found. null otherwise</returns>
        public Sprite GetSprite(string name)
        {
            return Sprites.GetValueOrDefault(name);
        }

        /// <summary>
        /// Get SpriteFont using file name. 
        /// </summary>
        /// <param name="name">Name of font</param>
        /// <returns>SpriteFont if found. null otherwise</returns>
        public SpriteFont GetFont(string name)
        {
            return Fonts.GetValueOrDefault(name);
        }

        /// <summary>
        /// Loads text string from file
        /// </summary>
        /// <param name="name">file name</param>
        /// <returns>string with text content from file if it exists</returns>
        public string LoadFileText(string name)
        {
            if (!File.Exists(name))
            {
                throw new FileNotFoundException("Could not find text file, {0}", name);
            }
            return File.ReadAllText(System.IO.Path.Combine(_content.RootDirectory, name));
        }

        public T LoadJsonObject<T>(string name, JsonSerializerSettings s)
        {
            string text = LoadFileText(name);
            return JsonConvert.DeserializeObject<T>(text, s);
        }

    }
}