using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ElDoodl.Game.Entities;

public class Dood {
    private Vector2 _position;
    private Vector2 _velocity;

    public Dood(Vector2 startPosition) {
        _position = startPosition;
        _velocity = new Vector2(12, 34);
    }

    public void Update(GameTime gameTime) {

    }

    public void Draw(SpriteBatch spriteBatch) {

    }
}
