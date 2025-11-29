using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;
	public AnimationPlayer anim;
	double FStop=0;
    public override void _Ready()
    {
		anim = GetNode<AnimationPlayer>("Anim");
    }
	public void Init(int i)
	{
		anim.Play("Start");
		FStop = 0.3333;
		if (i == 2)
		{
			CollisionLayer = 2;
		}
	}
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		if (Input.IsActionJustPressed("ui_accept"))
		{
			velocity.Y = JumpVelocity;
		}
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero && FStop == 0)
		{
			velocity.X = direction.X * Speed;
			velocity.Y = direction.Y * Speed;
		}
		else if(FStop == 0)
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Y = Mathf.MoveToward(Velocity.Y, 0, Speed);
		}
		else
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

	}
}
