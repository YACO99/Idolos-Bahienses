using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;
	public AnimationPlayer anim;
	Node2D cuerpo;
	public double FStop=0;
	int t = 0;
	public int id = 0;
	string[] keys= new string[] { "W","A","S","D","T","Y","U","Up", "Le", "Do", "Ri", "1", "2", "3" };
    public override void _Ready()
    {
		anim = GetNode<AnimationPlayer>("Anim");
        anim.Play("Start");
		cuerpo = GetNode<Node2D>("Cuerpo");
    }
	public void Init(int i)
	{
		FStop = 0.3333;
		if (i == 2)
		{
			id = 2;
			t = 7;
			CollisionLayer = 2;
			CollisionMask = 2;
		}
		else
		{
			id = 1;
		}
	}
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		Vector2 direction = Input.GetVector(keys[1 + t], keys[3 + t], keys[0 + t], keys[2 + t]);
		if (direction != Vector2.Zero && FStop == 0)
		{
			cuerpo.Scale = Vector2.Down + ((direction.X < 0) ? Vector2.Left : Vector2.Right);
			anim.Play("Correr");
            velocity.X = direction.X * Speed;
			velocity.Y = direction.Y * Speed;
		}
		else
		{
			if(FStop == 0)
				anim.Play("Quieto");
            velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Y = Mathf.MoveToward(Velocity.Y, 0, Speed);
		}
		if(FStop>0)
        {
			FStop -= delta;
			if (FStop < 0)
				FStop = 0;
        }

		Velocity = velocity;
		MoveAndSlide();
	}
	public void Damage()
	{
        anim.Play("Daño");
		FStop = 0.5;
    }
}
