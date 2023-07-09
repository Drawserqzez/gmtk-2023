using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using IDrawable = ElDood.Game.Screen.IDrawable;

namespace ElDood.Game.Entities;

public class Dood : Entity, IDrawable, IPlaceable, ICollidable {
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

    public float Height => _texture.Height * _scaling.Y;
    public float Width => _texture.Width * _scaling.X;

    public Dood(Vector2 startPosition, Texture2D texture) : base(startPosition, texture) {
        _position = startPosition;
        _velocity = Vector2.Zero;
        _texture = texture;
        _isTouchingGrass = true;
    }

    public override void Update(GameTime gameTime) {
        // _isTouchingGrass = _position.Y + Height >= 1000;

        if (_isTouchingGrass) 
            _velocity.Y = 0f;
        else if (_velocity.Y < Math.Max(Position.Y * Position.Y/500, 10))
            _velocity.Y += .75f + Convert.ToSingle(Math.Log10(Math.Log2(Math.Abs(Position.Y))));

        //var keybState = Keyboard.GetState();

        //if (keybState.IsKeyDown(Keys.Space))
        //AutoJump();

        /*
        _velocity.X = 0;

        if (keybState.IsKeyDown(Keys.A))
            _velocity.X -= 12f; 

        if (keybState.IsKeyDown(Keys.D))
            _velocity.X += 12f; 
        */

        if (_position.X > 1600)
            _position.X = 0 - Width;
        if (_position.X + Width < 0)
            _position.X = 1600;

        _position += _velocity;
    }

    private void Jump() {
        if (!_isTouchingGrass) return;

        _velocity.Y += -40;
        _isTouchingGrass = false;
    }

    public bool Collides(ICollidable other) {
        return base.Collision(other as Entity); // todo: fulcast
    }

    public override void PushOut(Entity other) {
        // Behöver bara putta ut den när den faller neråt
        // Dood kan nu hoppa genom plattformar från undersidan
        // Men om man går av en plattform och sedan går tillbaka in i den tpar man till toppen av den, inte så noticable in gameplay
        if (_velocity.Y >= 0) {
            base.PushOut(other);
            _isTouchingGrass = true;
        }
    }

    public void Gravity() {
        if (_velocity.Y == 0f)
            _isTouchingGrass = false;
    }

    public void AutoJump(Platform platform) {
        if (!_isTouchingGrass) return;
        if (_velocity.Y < 0f) return;

        int Choice = 2;
        double Direction;
        double Multiplier = 0;

        // Choice == 2 gives a different control scheme where dood jumps based on how far from the centre of the platform
        // he collides with is from his centre, kind of like reverse pong. If it collides to the right of the platform centre, he goes to the left.
        if (Choice == 2) {
            // Control direction based on how close to the center of the platform you hit
            Vector2 DoodPoint = new Vector2(this._position.X + Width * 0.5f, this._position.Y + Height * 0.5f);
            Vector2 PlatformCentre = new Vector2(platform.Position.X + platform.Width * 0.5f, platform.Position.Y + platform.Height * 0.5f);
            Vector2 Resultant = new Vector2(DoodPoint.X - PlatformCentre.X, DoodPoint.Y - PlatformCentre.Y);

            //Console.WriteLine("" + DoodPoint + ":" + PlatformCentre + ":" + Resultant);

            // atan2(Y, X)
            Multiplier = Math.Atan2(Resultant.Y, -Resultant.X) * 2/ Math.PI + 1f;
        }

        if (Choice == 1) {
            var rand = new Random();
            // Make the direction more likely to bounce toward the centre
            Multiplier = rand.NextDouble() - 0.5f - (this._position.X - 800)/2400;
            //Console.WriteLine(platform.Position.X + ":" + platform.Position.Y);
        }
        // Span slowly expands as Dood gets higher, increasing chaos of movement
        double Span = Math.Min(3, 7 - Math.Log10(Math.Log10(1000-this._position.Y)));

        // Choose direction to jump in, with a span centered around -Y axis
        Direction = Math.PI * Multiplier / Span + Math.PI / 2;

        _velocity = Vector2.Zero;

        _velocity.X += Convert.ToSingle(Math.Cos(Direction)) * -45f;
        _velocity.Y += Convert.ToSingle(Math.Sin(Direction)) * -45f;

        //Console.WriteLine(_velocity + ":" + Direction + ":" + Multiplier);
        //Console.WriteLine(7 - Math.Log10(Math.Log10(1000-this._position.Y)));
        _isTouchingGrass = false;
        
    }
}
