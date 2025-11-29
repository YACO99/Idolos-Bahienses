using Godot;

public partial class Main : Node
{
    public int Creditos = 5;
    PackedScene BasePlayer;
    Player P1=null,P2=null;
    Node2D Map, Spawn1, Spawn2;
    Label CrediLabel;
    Control ContrMain;
    public override void _Ready()
    {
        var BasePlayer = ResourceLoader.Load<PackedScene>("res://Tscns/Player.tscn");
        Map = GetNode<Node2D>("Map");
        Spawn1 = Map.GetNode<Node2D>("Spawn1");
        Spawn2 = Map.GetNode<Node2D>("Spawn2");
        ContrMain = GetNode<Control>("Control");
        CrediLabel = ContrMain.GetNode<Label>("CrediLabel");
        CrediLabel.Text = "Creditos: 5";
    }

    public override void _Process(double delta)
    {
        if (Creditos > 0)
        {
            if (P1 == null & Input.IsActionJustPressed("Y"))
            {
                Creditos--;
                Spawn(1);
            }

            if (P2 == null & Input.IsActionJustPressed("3"))
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
                P1.Position = Spawn1.Position;
                Map.AddChild(P1);
                break;
            case 2:
                if (P2 != null)
                    P2.QueueFree();
                P2 = BasePlayer.Instantiate<Player>();
                P2.Init(2);//configura color y colicion
                P2.Position = Spawn2.Position;
                Map.AddChild(P2);
                break;
        }
    }
}