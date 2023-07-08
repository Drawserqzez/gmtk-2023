using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ElDood.Game.Screen.UI;

public class UiManager {
    private readonly List<IUiObject> _uiObjects; 

    public UiManager(IEnumerable<IUiObject> uiObjects) {
        _uiObjects = new List<IUiObject>();
        _uiObjects.AddRange(uiObjects);
    }

    public void Draw(SpriteBatch spriteBatch, GameTime gameTime) {
        foreach (var ui in _uiObjects) {
            if (!ui.ShouldBeDisplayed) continue;

            Draw(spriteBatch, ui);

            if (ui is not ITextUiObject) continue;

            var uiText = ui as ITextUiObject; 

            spriteBatch.DrawString(uiText.SpriteFont, uiText.ContextString,  
                uiText.TextPosition, Color.Black);
        }
    }

    private void Draw(SpriteBatch spriteBatch, IDrawable drawable) {
        spriteBatch.Draw(drawable.Texture, drawable.Position, drawable.Source, 
                Color.White, drawable.Rotation, Vector2.Zero, drawable.Scale, 
                drawable.Effects, drawable.LayerDepth); 
    }
}
