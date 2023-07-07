using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ElDood.Game.Entities;
using ElDood.Game.Screen;
using System;

namespace ElDood;

public class GameLoop : Microsoft.Xna.Framework.Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Camera _mainCamera;
    private Dood _dood;
    private Platform _platform;

    public GameLoop()
    {
        _graphics = new GraphicsDeviceManager(this);
        _graphics.PreferredBackBufferWidth = 1600;
        _graphics.PreferredBackBufferHeight = 1200;

        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _mainCamera = new Camera(new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight));
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        var doodTexture = this.Content.Load<Texture2D>("dood");

        _dood = new Dood(new Vector2(800, 600), doodTexture);

        var platformTexture = this.Content.Load<Texture2D>("platform");

        _platform = new Platform(new Vector2(800, 600), platformTexture);


        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        _dood.Update(gameTime);
        _platform.Update(gameTime);

        Console.WriteLine(_dood.Collision(_platform));


        _mainCamera.Update(gameTime, 0f);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin();

        // TODO: Add your drawing code here
        _mainCamera.Draw(_spriteBatch, gameTime, new[] { _dood });
        _platform.Draw(_spriteBatch);

        _spriteBatch.End();
        base.Draw(gameTime);
    }
}
