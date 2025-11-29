using Godot;
using System;

public partial class God : Node
{
	//elemento God, no se borra, util para que un hijo acceda a algo
	//ej: Player -> "God.lambda.Main.Creditos--;"
	public static God Lambda { get; set; }
	public Main Main;

	public override void _Ready()
	{
		Lambda = this;
        Main = GetTree().Root.GetNode<Main>("Main");
    }
}
