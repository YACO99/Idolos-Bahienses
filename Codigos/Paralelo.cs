using Godot;
using System;

public partial class Paralelo : Node2D
{
	[Export]
	int i = 0;//0=calle, 1 = proximo, 2 = medio, 3 = lejos
    float x=0;
    // Called every frame. 'delta' is the elapsed time since the previous frame.
    bool bo = true;
    [Export]
    Vector2 reset;
    public override void _Ready()
    {

    }
	public override void _Process(double delta)
	{
        
        if (GlobalPosition.X < -1920)
        {
            GlobalPosition += Vector2.Right * 3840;
        }
        var _x=God.Lambda.Main.Camera.GlobalPosition.X;
        if (!God.Lambda.Main.boMap)
        {

            switch (i)
            {
                case 0:
                    if (GlobalPosition.X < -1920)
                    {
                        GlobalPosition += Vector2.Right * 3840;
                    }
                    break;
                case 1:
                    if (GlobalPosition.X < -1920)
                    {
                        GlobalPosition += Vector2.Right * 3840;
                    }
                    GlobalPosition += Vector2.Right * 0.5f * (x - _x);
                    break;
                case 2:
                    
                    GlobalPosition += Vector2.Right * 0.65f * (x - _x);
                    break;
                case 3:
                    if (GlobalPosition.X < -1920)
                    {
                        GlobalPosition += Vector2.Right * 3840f;
                    }
                    GlobalPosition += Vector2.Right * 0.8f * (x - _x);
                    break;
            }
        }
        x = _x;
        if (bo)
        {
            bo = false;
            Position = reset;
            x = God.Lambda.Main.Cam.GlobalPosition.X;
        }
    }
}
