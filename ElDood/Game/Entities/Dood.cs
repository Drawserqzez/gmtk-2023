using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ElDood.Game.Entities;

public class Dood {
    private readonly Texture2D _texture;

    private Vector2 _position;
    private Vector2 _velocity;

    private bool _isTouchingGrass;

    public Dood(Vector2 startPosition, Texture2D texture) {
        _position = startPosition;
        _velocity = Vector2.Zero;
        _texture = texture;
        _isTouchingGrass = true;
    }

    public void Update(GameTime gameTime) {
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
            _velocity.X -= 2f; 

        if (keybState.IsKeyDown(Keys.D))
            _velocity.X += 2f; 

        _position += _velocity;
    }

    private void Jump() {
        if (!_isTouchingGrass) return;

        _velocity.Y += -20f;
    }

    public void Draw(SpriteBatch spriteBatch) {
        spriteBatch.Draw(_texture, _position, null, Color.White, 0f, Vector2.Zero, new Vector2(5, 5), SpriteEffects.None, 1f);
    }
}
