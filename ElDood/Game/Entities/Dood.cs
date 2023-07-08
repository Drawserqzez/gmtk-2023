using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using IDrawable = ElDood.Game.Screen.IDrawable;
using System;

namespace ElDood.Game.Entities;

public class Dood : Entity, IDrawable, IPlaceable {
    private const float ScaleSize = 5f;
    private readonly Texture2D _texture;

    private Vector2 _velocity;
    private bool _isTouchingGrass;

    public Texture2D Texture => _texture;
    public Vector2 Position => _position;
    public Rectangle? Source => null;
    public float Rotation => 0f;
    public Vector2 Scale => _scaling;
    public SpriteEffects Effects => _velocity.X > 0 
        ? SpriteEffects.FlipHorizontally 
        : SpriteEffects.None;
    public float LayerDepth => 1f;

    private float Height => _texture.Height * _scaling.Y;
    private float Width => _texture.Width * _scaling.X;

    public Dood(Vector2 startPosition, Texture2D texture) : base(startPosition, texture) {
        _position = startPosition;
        _velocity = Vector2.Zero;
        _texture = texture;
        _isTouchingGrass = true;
    }

    public override void Update(GameTime gameTime) {
        _isTouchingGrass = _position.Y + Height >= 1000;

        if (_isTouchingGrass) 
            _velocity.Y = 0f;
        else 
            _velocity.Y += 2f;

        var keybState = Keyboard.GetState();

        if (keybState.IsKeyDown(Keys.Space))
            Jump();

        _velocity.X = 0;

        if (keybState.IsKeyDown(Keys.A))
            _velocity.X -= 12f; 

        if (keybState.IsKeyDown(Keys.D))
            _velocity.X += 12f; 

        if (_position.X > 1600)
            _position.X = 0 - Width;
        if (_position.X + Width < 0)
            _position.X = 1600;

        _position += _velocity;
    }

    private void Jump() {
        if (!_isTouchingGrass) return;

        _velocity.Y += -69;
    }
}
