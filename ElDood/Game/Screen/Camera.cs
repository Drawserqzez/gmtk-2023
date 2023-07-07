using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ElDood.Game.Screen;

public class Camera {
    private Vector2 _position;
    private readonly Vector2 _screenSize;

    public Camera(Vector2 screenSize) {
        _position = Vector2.Zero;
        _screenSize = screenSize;
    }

    public void Update(GameTime gameTime, IDrawable followThis) {
        var screenPos = CalculateScreenSpace(followThis.Position);

        var velocityY = 0f;

        if (screenPos.Y < _screenSize.Y * 0.25f)
            velocityY += 5f;
        else if (velocityY > 0f)
            velocityY -= 2f;

        _position.Y += velocityY;
    }

    public void Draw(SpriteBatch spriteBatch, GameTime gameTime, IEnumerable<IDrawable> drawables) {
        foreach (var drawable in drawables) {
            (var shouldBeRendered, var screenPos) = CalculateScreenSpace(drawable);

            if (!shouldBeRendered) continue;

            spriteBatch.Draw(
                drawable.Texture, screenPos, drawable.Source, Color.White, 
                drawable.Rotation, Vector2.Zero, drawable.Scale, 
                drawable.Effects, drawable.LayerDepth
            );
        }
    }

    private (bool shouldBeRendered, Vector2 screenSpacePosition) CalculateScreenSpace(IDrawable drawable) {
        var screenSpacePosition = CalculateScreenSpace(drawable.Position);

        var isYWithinRenderDistance = screenSpacePosition.Y > _screenSize.Y * -0.25f
            && screenSpacePosition.Y < _screenSize.Y * 1.25f;

        var isXWithinRenderDistance = screenSpacePosition.X > _screenSize.X * -0.25f
            && screenSpacePosition.X < _screenSize.X * 1.25f;

        return (isXWithinRenderDistance && isYWithinRenderDistance, screenSpacePosition);
    }

    private Vector2 CalculateScreenSpace(Vector2 realSpacePos) {
        var x = _position.X + realSpacePos.X;
        var y = _position.Y + realSpacePos.Y;

        return new Vector2(x, y);
    }
}
