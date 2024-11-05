using Microsoft.Xna.Framework;

namespace Monogamesa;

public class Bullet
{
    public Vector2 Position;
    public Vector2 Direction;
    public bool IsActive;
    public float Speed;

    public Bullet(Vector2 position, Vector2 direction, float speed)
    {
        Position = position;
        Direction = direction;
        Speed = speed;
        IsActive = true;
    }

    public void Update()
    {
        if (IsActive)
        {
            Position += Direction * Speed;
            
            if (Position.X < 0 || Position.X > 800 || Position.Y < 0 || Position.Y > 600)
            {
                IsActive = false;
            }
        }
    }
}