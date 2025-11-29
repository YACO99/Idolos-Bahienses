extends Node2D

@export var pidgeo_scene: PackedScene
@export var count:int = 5
@export var spawn_radius: float = 80

var pidgeon : Array = []

func _ready ():
	spawn_pidgeo()

func spawn_pidgeo():
	
	var formation = [
		Vector2(0, 0),
		Vector2(-20, -20),
		Vector2(20, -20),
		Vector2(-40, -40),
		Vector2(40, -40)]
		
	for i in range (count):
		var p = pidgeo_scene.instantiate()
		var offset = formation[i % formation.size()]
		p.global_position = global_position + offset
		add_child(p)
		pidgeon.append(p)


		
