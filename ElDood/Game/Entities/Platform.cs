using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ElDood.Game.Entities;

public class Platform : Entity {
    private const float ScaleSize = 5f;
    private readonly Texture2D _texture;


    public Platform(Vector2 startPosition, Texture2D texture) : base(startPosition, texture) {
        _position = startPosition;
        _texture = texture;
    }

    public override void Update(GameTime gameTime) {

    }

    public override void Draw(SpriteBatch spriteBatch) {
        spriteBatch.Draw(_texture, _position, null, Color.White, 0f, Vector2.Zero, _scaling, SpriteEffects.None, 1f);
        //spriteBatch.Draw(_texture, _position, Color.White);
    }
}