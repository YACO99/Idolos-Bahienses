using Godot;
using System;

public partial class Boss : Node
{
	int Try = 3;
	AnimationPlayer anim;
	float f = 3;
	RandomNumberGenerator r;
    public override void _Ready()
    {

		r = new RandomNumberGenerator();
		r.Randomize();
		anim = GetNode<AnimationPlayer>("AnimationPlayer");
    }
	public override void _Process(double delta)
	{
		if (!anim.IsPlaying())
		{
			if (f < 0)
			{
				anim.Play("attaque" + r.RandiRange(1, 2));
				Try--;
				if (Try <= 0)
				{
                    QueueFree();
					God.Lambda.Main.WIN();
                }
            }
			else if (f > 0)
				f -= (float)delta;
		}
	}
	public void _on_area_2d_body_entered(Node2D n)
	{
		if (n is Player p)
		{
			Try++;
        }
	}
}
