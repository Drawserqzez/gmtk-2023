using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ElDood.Game.Entities;

public class Dood {
    private readonly Texture2D _texture;

    private Vector2 _position;
    private Vector2 _velocity;

    public Dood(Vector2 startPosition, Texture2D texture) {
        _position = startPosition;
        _velocity = Vector2.Zero;
        _texture = texture;
    }

    public void Update(GameTime gameTime) {
        _position += _velocity;
    }

    public void Draw(SpriteBatch spriteBatch) {
        spriteBatch.Draw(_texture, _position, null, Color.White, 0f, _position, new Vector2(20, 20), SpriteEffects.None, 0f);
        //spriteBatch.Draw(_texture, _position, Color.White);
    }
}
