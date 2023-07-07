using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ElDood.Game.Entities;

public abstract class Entity {
    private const float ScaleSize = 5f;
    private readonly Texture2D _texture;
    protected readonly Vector2 _scaling;

    protected Vector2 _position;

    public Entity(Vector2 startPosition, Texture2D texture) {
        _position = startPosition;
        _texture = texture;
        _scaling = new Vector2(ScaleSize);
    }

    public abstract void Update(GameTime gameTime);

    public bool Collision(Entity other) {
        if (this._position.X < other._position.X + other._texture.Width * other._scaling.X && this._position.X + this._texture.Width * this._scaling.X > other._position.X && 
            this._position.Y < other._position.Y + other._texture.Height * other._scaling.Y && this._position.Y + this._texture.Height * this._scaling.Y > other._position.Y)
            return true;
        //Console.WriteLine($"{this._position.X}:{this._position.Y}, {other._position.X + other._texture.Width * other._scaling.X}:{other._position.Y + other._texture.Height * other._scaling.Y}");
        return false;
    }
}
