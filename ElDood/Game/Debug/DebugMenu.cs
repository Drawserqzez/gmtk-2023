using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ElDood.Game.Debug;

public class DebugMenu {
    private readonly SpriteFont _spriteFont;
    private readonly Texture2D _filledTexture;
    private readonly List<IDebugInfo> _debugInfos;
    private bool _display;

    public DebugMenu(SpriteFont spriteFont, Texture2D filledTexture) {
        _spriteFont = spriteFont;
        _filledTexture = filledTexture;
        _debugInfos = new();
        _display = false;
    }

    public void AddTracker(IDebugInfo debugInfo) {
        _debugInfos.Add(debugInfo);
    }

    public void ToggleVisibility(GameTime gameTime) {
        //todo: prevent from toggling too often :D
        _display = !_display;
    }

    public void Draw(SpriteBatch spriteBatch) {
        if (!_display) return;

        var boxRectangle = new Rectangle(0, 0, _debugInfos.Max(x => x.InfoText.Length) + 180, _debugInfos.Count * 25);

        spriteBatch.Draw(_filledTexture, boxRectangle, Color.White);

        for (int i = 0; i < _debugInfos.Count; i++) {
            var debugInfo = _debugInfos[i];

            var pos = new Vector2(boxRectangle.X + 15, boxRectangle.Y + 15 * i);

            spriteBatch.DrawString(_spriteFont, debugInfo.InfoText, pos, Color.Black);
        }
    }
}

