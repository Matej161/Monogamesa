using System;
using System.Windows.Forms.VisualStyles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogamesa;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D _tankTexture;
    //private Texture2D _bludTexture;
    private Vector2 _tankPosition = new Vector2(250f, 250f);
    //private Vector2 _bludPosition = new Vector2(100f, 100f);
    private float _playerSpeed = 5f;
    private float _tankRotation = 0f;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        Window.AllowUserResizing = true;
        Window.AllowAltF4 = true;
        Window.Title = "Biggy Pavel";
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _tankTexture = Content.Load<Texture2D>("arrow");
        /*_bludTexture = new Texture2D(GraphicsDevice, 1, 1);
        _bludTexture.SetData<Color>(new Color[] { Color.White });*/
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        
        KeyboardState state = Keyboard.GetState();
        if (state.IsKeyDown(Keys.D))
        {
            //_tankRotation += 0.06f;
            _tankPosition.X += _playerSpeed;
        }
        if (state.IsKeyDown(Keys.A))
        {
            //_tankRotation -= 0.06f;
            _tankPosition.X -= _playerSpeed;
        }
        if (state.IsKeyDown(Keys.W))
        {
            //_tankPosition.X += (float)Math.Sin(_tankRotation) * _playerSpeed;
            //_tankPosition.Y -= (float)Math.Cos(_tankRotation) * _playerSpeed;
            _tankPosition.Y -= _playerSpeed;
        }
        if (state.IsKeyDown(Keys.S))
        {
            _tankPosition.Y += _playerSpeed;
            //_tankPosition.X -= (float)Math.Sin(_tankRotation) * _playerSpeed;
            //_tankPosition.Y += (float)Math.Cos(_tankRotation) * _playerSpeed;
        }
        
        /*
        
        var windowBounds = Window.ClientBounds;

        _tankPosition.X = Math.Clamp(_tankPosition.X, 0, windowBounds.Width);
        _tankPosition.Y = Math.Clamp(_tankPosition.Y, 0, windowBounds.Height);
        */
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        var rectangle = new Rectangle((int)_tankPosition.X, (int)_tankPosition.Y, 100, 100);
        _spriteBatch.Begin();
        _spriteBatch.Draw(_tankTexture, rectangle, null, Color.White, _tankRotation, new Vector2(_tankTexture.Width / 2f, _tankTexture.Height / 2f), 0, 0);
        
        /*var ahoj = new Rectangle((int)_bludPosition.X, (int)_bludPosition.Y, 100, 100);
        _spriteBatch.Draw(_bludTexture, _bludPosition, ahoj, Color.White);*/
        
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}