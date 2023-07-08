using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using IDrawable = ElDood.Game.Screen.IDrawable;

namespace ElDood.Game.Entities;

public class Platform : Entity, IDrawable, IPlaceable, ICollidable {
    private const float ScaleSize = 5f;
    private readonly Texture2D _texture;

    public Vector2 Position => _position;
    public Texture2D Texture => _texture;
    public Rectangle? Source => null;
    public float Rotation => 0f;
    public Vector2 Scale => _scaling;
    public SpriteEffects Effects => SpriteEffects.None;
    public float LayerDepth => 1f;

    public float Height => _texture.Height * _scaling.Y;
    public float Width => _texture.Width * _scaling.X;

    public Platform(Vector2 startPosition, Texture2D texture) : base(startPosition, texture) {
        _position = startPosition;
        _texture = texture;
    }

    public override void Update(GameTime gameTime) {

    }

    public bool Collides(ICollidable other) {
        return base.Collision(other as Entity); // todo: fulcast 
    }
}
