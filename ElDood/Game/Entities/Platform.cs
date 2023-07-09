using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

    public Platform(Vector2 startPosition, Texture2D texture, Vector2 scaling) : base(startPosition, texture) {
        _position = startPosition;
        _texture = texture;
        _scaling = scaling;
    }

    public override void Update(GameTime gameTime) {

    }

    public bool Collides(ICollidable other) {
        return base.Collision(other as Entity); // todo: fulcast 
    }

    public void AddPlatform(List<Platform> platforms, Vector2 realSpacePos) {

        platforms.Add(new Platform(new Vector2(realSpacePos.X - Width * 0.5f, realSpacePos.Y - Height * 0.5f), _texture, _scaling));
    }
}
