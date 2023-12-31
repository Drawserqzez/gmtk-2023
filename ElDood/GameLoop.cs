﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ElDood.Game.Debug;
using ElDood.Game.Debug.Trackers;
using ElDood.Game.Entities;
using ElDood.Game.Screen;
using ElDood.Game.Screen.UI;
using IDrawable = ElDood.Game.Screen.IDrawable;
using System.Collections.Generic;
using System.Linq;

namespace ElDood;

public class GameLoop : Microsoft.Xna.Framework.Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D _redFilledTexture;
    private Camera _mainCamera;
    private UiManager _uiManager;
    private DebugMenu _debugMenu;
    private Dood _dood;
    private Platform _platform;
    private Platform _ground;
    private List<Platform> _platforms;
    private List<Platform> _placedPlatforms;
    private MouseState _thisMouse;
    private MouseState _prevMouse;
    private double _points;
    private bool _end = false;

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

        _dood = new Dood(new Vector2(200, 600), doodTexture);

        var platformTexture = this.Content.Load<Texture2D>("platform");

        _platform = new Platform(new Vector2(-1000, 1200), platformTexture, new Vector2(5, 5));

        _ground = new Platform(new Vector2(-550, 1000), platformTexture, new Vector2(75, 5));

        _platforms = new List<Platform>();
        _platforms.Add(_ground);
        _platforms.Add(_platform);

        _placedPlatforms = new List<Platform>();
        

        _debugMenu = new DebugMenu(debugFont, _redFilledTexture);

        _debugMenu.AddTracker(new PositionTracker(_dood, "Dood"));
        _debugMenu.AddTracker(new PositionTracker(_platform, "this here platform"));
        _debugMenu.AddTracker(new CollisionTracker(_dood, _platform));

        //var quitButton = new Button(new Rectangle(1140, 0, 60, 30), _redFilledTexture, "Quit game LOL", new Action(() => Exit()), debugFont);
        //var pointButton = new Button(new Rectangle(700, 0, 200, 50), _redFilledTexture, $"Points: {_dood.Position.Y}", new Action(() => Exit()), debugFont);
        //_uiManager = new UiManager(new [] { });

        _thisMouse = Mouse.GetState();
        _prevMouse = _thisMouse;

        _points = 0;
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape) || _dood.Position.Y > -_points + 2000)
        {
            _end = true; 
            var debugFont = this.Content.Load<SpriteFont>("DebugFont");
            var pointButton = new Button(new Rectangle(800, 600, 200, 50), _redFilledTexture, $"Points: {Math.Truncate(_points)}", new Action(() => Exit()), debugFont);
            var exitButton = new Button(new Rectangle(800, 700, 200, 50), _redFilledTexture, "Press Esc to exit.", new Action(() => Exit()), debugFont);
            _uiManager = new UiManager(new [] { pointButton, exitButton });

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

        }

        _points = Math.Max(_points, 600 - _dood.Position.Y);


        var keyBoardState = Keyboard.GetState();

        
        if (keyBoardState.IsKeyDown(Keys.P))
            _debugMenu.ToggleVisibility(gameTime);
        

        
        //Console.WriteLine(_dood.Collision(_platform));

        foreach (var platform in _platforms)
        {
            platform.Update(gameTime);
            
            if (!_dood.Collision(platform))
                _dood.Gravity();

            if (_dood.Collision(platform))
                _dood.PushOut(platform);
                _dood.AutoJump(platform);
        }
    
        _dood.Update(gameTime);

        
        if (_thisMouse.LeftButton == ButtonState.Pressed && _prevMouse.LeftButton == ButtonState.Released)
        {
            // This is such an ugly way of doing it
            var realSpacePos = _mainCamera.ReverseScreenSpace(new Vector2(_thisMouse.X, _thisMouse.Y));
            _platform.AddPlatform(_platforms, realSpacePos);
            if (_platforms.Count >= 6)
                _platforms.RemoveAt(2);
        }
        _prevMouse = _thisMouse;
        _thisMouse = Mouse.GetState();


        /*
        
        _platform.Update(gameTime);

        _ground.Update(gameTime);
        
        if (!_dood.Collision(_platform) || !_dood.Collision(_ground)) {
            _dood.Gravity();
        }
        
        if (_dood.Collision(_platform)) {
            _dood.PushOut(_platform);
        }
        

        if (_dood.Collision(_ground)) {
            _dood.PushOut(_ground);
        }
        */


        _mainCamera.Update(gameTime, _dood);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin();

        _mainCamera.Draw(_spriteBatch, gameTime, new IDrawable[] { _dood }.Concat(_platforms));
        

        _debugMenu.Draw(_spriteBatch);
        if (_end)
            _uiManager.Draw(_spriteBatch, gameTime);
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}
