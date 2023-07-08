using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
// For math
using System;

namespace ElDood.Game.Entities;

public abstract class Entity {
    private const float ScaleSize = 5f;
    private readonly Texture2D _texture;
    protected Vector2 _scaling;

    protected Vector2 _position;

    public Entity(Vector2 startPosition, Texture2D texture) {
        _position = startPosition;
        _texture = texture;
        _scaling = new Vector2(ScaleSize);
    }

    public abstract void Update(GameTime gameTime);

    public bool Collision(Entity other) {
        // Ful axis aligned bounding box collisions
        // Behöver tekniskt sett bara kolla kollisioner med högsta pixeln på platformen
        if (this._position.X < other._position.X + other._texture.Width * other._scaling.X && this._position.X + this._texture.Width * this._scaling.X > other._position.X && 
            this._position.Y < other._position.Y + other._texture.Height * other._scaling.Y && this._position.Y + this._texture.Height * this._scaling.Y > other._position.Y)
            return true;
        //Console.WriteLine($"{this._position.X}:{this._position.Y}, {other._position.X + other._texture.Width * other._scaling.X}:{other._position.Y + other._texture.Height * other._scaling.Y}");
        return false;
    }

    // Will only be used by Dood on Other entities(Platforms)
    public virtual void PushOut(Entity other) {
        // Calculate Dood centre
        Vector2 DoodCentre = new Vector2(this._position.X + this._texture.Width * this._scaling.X * 0.5f, this._position.Y + this._texture.Height * this._scaling.Y * 0.5f);
        // to make the platforming more enjoyable, move the "centre" to the top of the sprite
        //Vector2 DoodCentre = new Vector2(this._position.X + this._texture.Width * this._scaling.X * 0.5f, this._position.Y);
        // Calculate Entity centre
        Vector2 EntityCentre = new Vector2(other._position.X + other._texture.Width * other._scaling.X * 0.5f, other._position.Y + other._texture.Height * other._scaling.Y * 0.5f);
        // Calculate the vector from Entity centre to Dood centre
        Vector2 Resultant = new Vector2(DoodCentre.X - EntityCentre.X, DoodCentre.Y - EntityCentre.Y);

        Console.WriteLine("" + DoodCentre + ":" + EntityCentre + ":" + Resultant);

        // atan2(Y, X)
        double theta = Math.Atan2(Resultant.Y, Resultant.X);
        Console.WriteLine("" + theta);
        Console.WriteLine("" + -Math.PI/4 + ":" + -3*Math.PI/4);        
        
        
        // If DoodCentre is over the platform => (135 deg > theta > 45 deg) => (3pi/4 > theta > pi/4), push it up
        // This is wrong because the coord-plane is flipped vertically =>
        // DoodCentre is over the platform if (315 deg > theta > 225 deg) => (7pi/4 > theta > 5pi/4) => (-pi/4 > theta > -3pi/4)
        if (- Math.PI / 4 > theta && theta > -3 * Math.PI / 4) {
            this._position.Y = other._position.Y - this._texture.Height * this._scaling.Y;
        }
        
        
    }
}
