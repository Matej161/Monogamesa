using Microsoft.Xna.Framework;
using System;

namespace Monogamesa;

public class Enemy
{
    public Vector2 Position;
    public float Speed;
    private Vector2 _direction;
    public float Rotation;
    private float _shootInterval = 2f; 
    private float _shootTimer = 0f;
    private bool _canShoot = true;
    public static bool IsActive;

    public Enemy(Vector2 startPosition)
    {
        Position = startPosition;
        Rotation = 0f;
        Speed = 1f;
        IsActive = true;
    }

    public void Update(GameTime gameTime, Vector2 playerPosition, Action<Bullet> shootBullet)
    {
        if (!IsActive) return;
        
        _direction = Vector2.Normalize(playerPosition - Position);
        
        Position += _direction * Speed;
        
        Rotation = (float)Math.Atan2(_direction.Y, _direction.X);
        
        _shootTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (_shootTimer >= _shootInterval && _canShoot)
        {
            _shootTimer = 0f; 
            Vector2 bulletDirection = _direction;
            Bullet aiBullet = new Bullet(Position, bulletDirection, 6f); 
            shootBullet(aiBullet); 
        }
    }

    public float GetRotation()
    {
        return Rotation;
    }
}