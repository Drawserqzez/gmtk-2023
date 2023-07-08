using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ElDood.Game.Debug;
using ElDood.Game.Debug.Trackers;
using ElDood.Game.Entities;
using ElDood.Game.Screen;
using IDrawable = ElDood.Game.Screen.IDrawable;


namespace ElDood;

public class GameLoop : Microsoft.Xna.Framework.Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D _redFilledTexture;
    private Camera _mainCamera;
    private DebugMenu _debugMenu;
    private Dood _dood;
    private Platform _platform;

    public GameLoop()
    {
        _graphics = new GraphicsDeviceManager(this);
        _graphics.PreferredBackBufferWidth = 1600;
        _graphics.PreferredBackBufferHeight = 1200;

        _graphics.ApplyChanges();

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

        _redFilledTexture = new Texture2D(_graphics.GraphicsDevice, 1, 1);
        _redFilledTexture.SetData(new [] { Color.Red });

        var doodTexture = this.Content.Load<Texture2D>("dood");
        var debugFont = this.Content.Load<SpriteFont>("DebugFont");

        _dood = new Dood(new Vector2(800, 600), doodTexture);

        var platformTexture = this.Content.Load<Texture2D>("platform");

        _platform = new Platform(new Vector2(800, 600), platformTexture);

        _debugMenu = new DebugMenu(debugFont, _redFilledTexture);

        _debugMenu.AddTracker(new PositionTracker(_dood, "Dood"));
        _debugMenu.AddTracker(new PositionTracker(_platform, "this here platform"));
        _debugMenu.AddTracker(new CollisionTracker(_dood, _platform));
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        var keyBoardState = Keyboard.GetState();

        if (keyBoardState.IsKeyDown(Keys.P))
            _debugMenu.ToggleVisibility(gameTime);

        _dood.Update(gameTime);
        _platform.Update(gameTime);

        Console.WriteLine(_dood.Collision(_platform));
        
        if (_dood.Collision(_platform)) {
            _dood.PushOut(_platform);
        }
        


        _mainCamera.Update(gameTime, _dood);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin();

        _mainCamera.Draw(_spriteBatch, gameTime, new IDrawable[] { _dood, _platform });

        _debugMenu.Draw(_spriteBatch);
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}
