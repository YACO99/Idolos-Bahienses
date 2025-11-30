extends Area2D

@export var speed = 120
@export var detection_range = 400                     #Se crean las variables de la paloma a utilizar
@export var spawn_interval := 1
@export var spawn_radius := 600
var is_diving:bool = false
@export var muerto:bool = false
@onready var a := $anim
var player = null

func _physics_process(_delta):
	if muerto:
		queue_free()
	player = get_tree().get_nodes_in_group("player")
	if len(player) > 0:
		var dir0 = player[0].global_position+Vector2.UP*100 - global_position
		var dist0 = dir0.length()
		if len(player) > 1:
			var dir1 = player[1].global_position - global_position
			var dist1 = dir1.length()
			if dist1<dist0 :
				if(dist1<detection_range):
					position += dir1.normalized() * _delta* speed
			else:
				if(dist0<detection_range):
					position += dir0.normalized() * _delta* speed
		else:
			if(dist0<detection_range):
				position += dir0.normalized() * _delta* speed
		
func _on_body_entered(t):              #Función para que la paloma reconozca cuando colisiona con el jugador
	if t.is_in_group("player"):
		t.call("Damage")
		a.play("Muerte")

func start_attack():
	is_diving = true
