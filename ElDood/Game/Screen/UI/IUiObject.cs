using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ElDood.Game.Entities;

namespace ElDood.Game.Screen.UI;

public interface IUiObject : IPlaceable, IDrawable {
    bool ShouldBeDisplayed { get; }
}

public interface ITextUiObject : IUiObject {
    string ContextString { get; }
    SpriteFont SpriteFont { get; }
    Vector2 TextPosition { get; }
}
