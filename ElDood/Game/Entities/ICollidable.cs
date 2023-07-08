using Microsoft.Xna.Framework;

namespace ElDood.Game.Entities;

public interface ICollidable {
    bool Collides(ICollidable other);
    Vector2 Position { get; }
    float Height { get; }
    float Width { get; }
}
