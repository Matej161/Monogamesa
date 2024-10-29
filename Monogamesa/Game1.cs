using System;
using System.Collections.Generic;
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
    private Texture2D _bulletTexture;

    //private Texture2D _bludTexture;
    private Vector2 _tankPosition = new Vector2(250f, 250f);

    //private Vector2 _bludPosition = new Vector2(100f, 100f);
    private float _playerSpeed = 5f;
    private float _tankRotation = 0f;

    private bool _isShooting = false;
    private Vector2 _bulletPosition;
    private Vector2 _bulletDirection;
    private float _bulletSpeed = 8f;
    private bool _bulletActive = false;
    private List<Bullet> _bullets;
    
    private bool _canShoot = true;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        _bullets = new List<Bullet>();
    }

    protected override void Initialize()
    {
        Window.AllowAltF4 = true;
        Window.Title = "Biggy Pavel";
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _tankTexture = Content.Load<Texture2D>("tank");
        _bulletTexture = Content.Load<Texture2D>("bullet");

        /*_bludTexture = new Texture2D(GraphicsDevice, 1, 1);
        _bludTexture.SetData<Color>(new Color[] { Color.White });*/

        _tankPosition = new Vector2(Window.ClientBounds.Width / 2, Window.ClientBounds.Height / 2);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        MouseState mouseState = Mouse.GetState();
        Vector2 mousePosition = new Vector2(mouseState.X, mouseState.Y);

        _tankRotation = (float)Math.Atan2(mousePosition.Y - _tankPosition.Y, mousePosition.X - _tankPosition.X);

        if (mouseState.LeftButton == ButtonState.Pressed && _canShoot)
        {
            ShootBullet(mousePosition);
            _canShoot = false;
        }
        
        if (mouseState.LeftButton == ButtonState.Released)
        {
            _canShoot = true;
        }
        
        for (int i = _bullets.Count - 1; i >= 0; i--)
        {
            _bullets[i].Update();
            if (!_bullets[i].IsActive)
            {
                _bullets.RemoveAt(i);
            }
        }

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

        var windowBounds = Window.ClientBounds;

        _tankPosition.X = Math.Clamp(_tankPosition.X, 0, windowBounds.Width);
        _tankPosition.Y = Math.Clamp(_tankPosition.Y, 0, windowBounds.Height);

        base.Update(gameTime);
    }
    private void ShootBullet(Vector2 targetPosition)
    {
        Vector2 direction = Vector2.Normalize(targetPosition - _tankPosition);
        Bullet newBullet = new Bullet(_tankPosition, direction, _bulletSpeed);
        _bullets.Add(newBullet); 
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        var rectangle = new Rectangle((int)_tankPosition.X, (int)_tankPosition.Y, 100, 100);
        _spriteBatch.Begin();
        
        _spriteBatch.Draw(_tankTexture, rectangle, null, Color.White, _tankRotation,
            new Vector2(_tankTexture.Width / 2f, _tankTexture.Height / 2f), 0, 0);

        /*var ahoj = new Rectangle((int)_bludPosition.X, (int)_bludPosition.Y, 100, 100);
        _spriteBatch.Draw(_bludTexture, _bludPosition, ahoj, Color.White);*/
        

        foreach (Bullet bullet in _bullets)
        {
            var destinationRectangle = new Rectangle((int)bullet.Position.X, (int)bullet.Position.Y, 10, 10);
            _spriteBatch.Draw(_bulletTexture, destinationRectangle, null, Color.White, 0f,
                new Vector2(_bulletTexture.Width / 2f, _bulletTexture.Height / 2f), SpriteEffects.None, 0f);
        }

        _spriteBatch.End();
        base.Draw(gameTime);
    }
}