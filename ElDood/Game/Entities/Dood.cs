using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ElDood.Game.Entities;

public class Dood : Entity {
    private readonly Texture2D _texture;

    private Vector2 _position;
    private Vector2 _velocity;

    private bool _isTouchingGrass;

    public Dood(Vector2 startPosition, Texture2D texture) : base(startPosition, texture) {
        _position = startPosition;
        _velocity = Vector2.Zero;
        _texture = texture;
        _isTouchingGrass = true;
    }

    public override void Update(GameTime gameTime) {
        _isTouchingGrass = _position.Y + _texture.Height * 5 >= 1000;

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
            _position.X = 0 - _texture.Width * 5;
        if (_position.X + _texture.Width * 5 < 0)
            _position.X = 1600 - _texture.Width;

        _position += _velocity;
    }

    private void Jump() {
        if (!_isTouchingGrass) return;

        _velocity.Y += -50;
    }

    public override void Draw(SpriteBatch spriteBatch) {
        var fx = _velocity.X > 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
        spriteBatch.Draw(_texture, _position, null, Color.White, 0f, Vector2.Zero, new Vector2(5, 5), fx, 1f);
    }
}
