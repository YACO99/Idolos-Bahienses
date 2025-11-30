using Godot;

public partial class Main : Node
{
	public int Creditos = 5;
	PackedScene BasePlayer,BaseFlock1, BaseFlock2,BaseFlock3,BaseFlock4;
	public Player P1=null,P2=null;
	Node2D  Map, Spawn1, Spawn2, Nido, Flecha;
	Label CrediLabel;
	Control ContrMain;
	CollisionShape2D areaNext, areaNextEnd;
	double TimeDelay = 0;
	public bool boMap = false;
	public Node2D Cam;
	public Camera2D Camera;
	public float pixeles = 0;
	RandomNumberGenerator random;
	int idA = 0;
	public int Puntaje = 0;
	public override void _Ready()
	{
		random = new RandomNumberGenerator();
		random.Randomize();
		BasePlayer = ResourceLoader.Load<PackedScene>("res://Tscns/Player.tscn");
		BaseFlock1 = ResourceLoader.Load<PackedScene>("res://Tscns/Flock"+1+".tscn");
		BaseFlock2 = ResourceLoader.Load<PackedScene>("res://Tscns/Flock"+2+".tscn");
		BaseFlock3 = ResourceLoader.Load<PackedScene>("res://Tscns/Flock"+3+".tscn");
		BaseFlock4 = ResourceLoader.Load<PackedScene>("res://Tscns/Flock"+4+".tscn");
		Cam = GetNode<Node2D>("Cam");
		Camera = Cam.GetNode<Camera2D>("Cam");
		Map = GetNode<Node2D>("Map");
		Flecha = Cam.GetNode<Node2D>("Flecha");
		Spawn1 = Cam.GetNode<Node2D>("Spawn1");
		Spawn2 = Cam.GetNode<Node2D>("Spawn2");
		Nido = Cam.GetNode<Node2D>("nido");
		ContrMain = GetNode<Control>("Layer/Control");
		CrediLabel = ContrMain.GetNode<Label>("CrediLabel");
		CrediLabel.Text = "Creditos: 5";
		areaNext = Cam.GetNode<CollisionShape2D>("Next/CollisionShape2D");
		areaNextEnd = Cam.GetNode<CollisionShape2D>("End/CollisionShape2D");
	}

	public override void _Process(double delta)
	{
		if (Creditos >= 0)
		{
			if (P1 == null && Input.IsActionJustPressed("U") && !boMap && Creditos > 0)
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
				Vector2 p = Vector2.Zero;
				int l = 0;
				if (P1 != null)
				{
					l++;
					p += P1.GlobalPosition;
				}
				if (P2 != null)
				{
					l++;
					p += P2.GlobalPosition;
				}
				p /= l;
				Camera.GlobalPosition += p - Camera.GlobalPosition;
				if (boMap)
				{
					if (P1 != null)
						P1.Position += (float)delta * 980 * Vector2.Left;
					if (P2 != null)
						P2.Position += (float)delta * 980 * Vector2.Left;
					Map.Position += (float)delta * 980 * Vector2.Left;
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
				var en = GetTree().GetNodesInGroup("enemigo");
				if (en.Count == 0 && TimeDelay == 0 && !boMap && !Flecha.Visible)
					TimeDelay = 2;

			}
			else {
				Camera.GlobalPosition += Spawn1.Position - Camera.GlobalPosition;
			}
			if (P2 == null && Input.IsActionJustPressed("3") && !boMap && Creditos>0)
			{
				Creditos--;
				Spawn(2);
			}
		}
		if (Creditos == 0 && P1 == null && P2 == null)
		{
			//Fin
			Creditos = 5;
			Puntaje = 0;

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
		if (node.IsInGroup("player") && ((Player)node).id==idA)
		{
			areaNextEnd.CallDeferred(CollisionShape2D.MethodName.SetDisabled, true);
			if (P1 != null)
			{
				P1.FStop = 0.2;
				P1.anim.CallDeferred(AnimationPlayer.MethodName.Play);

			}
			if (P2 != null)
			{
				P2.FStop = 0.2;
				P2.anim.CallDeferred(AnimationPlayer.MethodName.Play);
			}
			boMap = false;
			pixeles = Camera.GlobalPosition.X;
			var r=random.RandiRange(1,4);
			PackedScene temp=null;
			switch (r)
			{
				case 1:
					temp = BaseFlock1;
					break;
				case 2:
					temp = BaseFlock2;
					break;
				case 3:
					temp = BaseFlock3;
					break;
				case 4:
					temp = BaseFlock4;
					break;
			}
			var f = temp.Instantiate<Node2D>();
			f.GlobalPosition = Nido.GlobalPosition;
			CallDeferred(Node.MethodName.AddChild, f);
		}
	}

	public void _on_area_2d_body_entered(Node2D node)
	{
		if (node.IsInGroup("player"))
		{
			idA=((Player)node).id;
			Flecha.CallDeferred(Sprite2D.MethodName.SetVisible, false);
			areaNext.CallDeferred(CollisionShape2D.MethodName.SetDisabled, true);
			areaNextEnd.CallDeferred(CollisionShape2D.MethodName.SetDisabled, false);
			if (P1 != null)
			{
				P1.FStop = 10;
				P1.anim.CallDeferred(AnimationPlayer.MethodName.Play);
			}
			if (P2 != null)
			{
				P2.FStop = 10;
				P2.anim.CallDeferred(AnimationPlayer.MethodName.Pause);
			}
			boMap = true;
		}

	}
}
