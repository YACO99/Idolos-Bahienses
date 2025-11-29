using Godot;

public partial class Main : Node
{
	public int Creditos = 5;
	PackedScene BasePlayer,BaseFlock;
	Player P1=null,P2=null;
	Node2D  Spawn1, Spawn2, Nido, Flecha;
	Label CrediLabel;
	Control ContrMain;
	CollisionShape2D areaNext, areaNextEnd;
	double TimeDelay = 0;
	bool boMap = false;
	Camera2D Cam;
	
	public override void _Ready()
	{
		BasePlayer = ResourceLoader.Load<PackedScene>("res://Tscns/Player.tscn");
		BaseFlock = ResourceLoader.Load<PackedScene>("res://Tscns/Flock"+1+".tscn");
		Cam = GetNode<Camera2D>("Cam");
		Flecha = Cam.GetNode<Node2D>("Flecha");
		Spawn1 = Cam.GetNode<Node2D>("Spawn1");
		Spawn2 = Cam.GetNode<Node2D>("Spawn2");
		Nido = Cam.GetNode<Node2D>("nido");
		ContrMain = GetNode<Control>("Control");
		CrediLabel = ContrMain.GetNode<Label>("CrediLabel");
		CrediLabel.Text = "Creditos: 5";
		areaNext = Cam.GetNode<CollisionShape2D>("Next/CollisionShape2D");
		areaNextEnd = Cam.GetNode<CollisionShape2D>("End/CollisionShape2D");
	}

	public override void _Process(double delta)
	{
		if (Creditos > 0)
		{
			if (P1 == null && Input.IsActionJustPressed("U") && !boMap)
			{
				Creditos--;
				Spawn(1);
			}
			if (P1 != null && P2 != null)
			{
				if (P1.Position.Y < P2.Position.Y)
				{
					P1.ZIndex = 10;
					P2.ZIndex = 20;
				}
				else if (P2.Position.Y < P1.Position.Y)
				{
					P1.ZIndex = 20;
					P2.ZIndex = 10;
				}
			}
			if (P1 != null || P2 != null)
			{
				if (boMap)
				{
					Cam.Position += (float)delta*512*Vector2.Right;
				}
				if (TimeDelay > 0)
					TimeDelay -= delta;
				if (TimeDelay < 0)
				{
					//ver flecha
					Flecha.Visible = true;
					//enable colision de area al borde final
					areaNext.Disabled = false;
					TimeDelay = 0;
				}
				var en=GetTree().GetNodesInGroup("enemigo");
				if (en.Count==0 && TimeDelay==0 && !boMap)
					TimeDelay = 4;

			}
			if (P2 == null && Input.IsActionJustPressed("3") && !boMap)
			{
				Creditos--;
				Spawn(2);
			}
		}
		else
		{
			//Fin del juego, musica mala
			//10 segundos, pedir coins caso contraio reiniciar escena.
		}
	}

	public void Spawn(int i)
	{
		CrediLabel.Text = "Creditos: " + Creditos;

		switch (i) {
			case 1:
				if (P1!=null)
					P1.QueueFree();
				P1 = BasePlayer.Instantiate<Player>();
				P1.Init(1);//configura color y colicion
				P1.GlobalPosition = Spawn1.GlobalPosition;
				AddChild(P1);
				break;
			case 2:
				if (P2 != null)
					P2.QueueFree();
				P2 = BasePlayer.Instantiate<Player>();
				P2.Init(2);//configura color y colicion
				P2.GlobalPosition = Spawn2.GlobalPosition;
				
				AddChild(P2);
				break;
		}
	}

	public void _on_end_body_entered(Node2D node)
	{
		if (node.IsInGroup("player") && ((Player)node).id==1)
		{
			areaNextEnd.CallDeferred(CollisionShape2D.MethodName.SetDisabled, true);
			if (P1 != null)
			{
				P1.FStop = 0.2;
				P1.anim.Play();

			}
			if (P2 != null)
			{
				P2.FStop = 0.2;
				P2.anim.Play();
			}
			boMap = false;
			
		}
	}

	public void _on_area_2d_body_entered(Node2D node)
	{
		if (node.IsInGroup("player") && ((Player)node).id == 1)
		{
			
			Flecha.CallDeferred(Sprite2D.MethodName.SetVisible, false);
			areaNext.CallDeferred(CollisionShape2D.MethodName.SetDisabled, true);
			areaNextEnd.CallDeferred(CollisionShape2D.MethodName.SetDisabled, false);
			if (P1 != null)
			{
				P1.FStop = 10;
				P1.anim.Pause();
			}
			if (P2 != null)
			{
				P2.FStop = 10;
				P2.anim.Pause();
			}
			boMap = true;
			var f = BaseFlock.Instantiate<Node2D>();
			f.GlobalPosition = Nido.GlobalPosition;
			CallDeferred(Node.MethodName.AddChild, f);
		}

	}
}
