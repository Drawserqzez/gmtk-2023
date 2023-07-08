using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ElDood.Game.Screen.UI;

public class Button : ITextUiObject {
    private readonly string _buttonText;
    private readonly Action _onClick;
    private readonly SpriteFont _font;
    private bool _isVisible;

    public Button(Rectangle size, Texture2D fillTexture, string text, 
            Action onClick, SpriteFont font) {
        Source = size;
        Texture = fillTexture;
        _buttonText = text;
        _onClick = onClick;
        _font = font;

        _isVisible = true;
    }

    public bool ShouldBeDisplayed => _isVisible;
    public string ContextString => _buttonText;
    public SpriteFont SpriteFont => _font;
    public Vector2 TextPosition => new Vector2(Position.X - this.Source.Value.Width / 2, Position.Y - this.Source.Value.Y / 2);

    public Vector2 Position => new Vector2(Source.Value.X, Source.Value.Y);
    public Rectangle? Source { get; init; }
    public Texture2D Texture { get; init; }

    public float Rotation => 0f;
    public float LayerDepth => 12f;
    public Vector2 Scale => Vector2.One;
    public SpriteEffects Effects => SpriteEffects.None;
}
