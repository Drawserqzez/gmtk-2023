using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ElDood.Game.Screen;

public interface IDrawable {
    Texture2D Texture { get; }
    Vector2 Position { get; }
    Rectangle? Source { get; }
    float Rotation { get; }
    Vector2 Scale { get; }
    SpriteEffects Effects { get; }
    float LayerDepth { get; }
}
