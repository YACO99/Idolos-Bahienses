extends Node
@export var PauseMenu: TextureRect
@export var ButtonQuit: Button
var is_paused = false

func _ready():
	get_tree().paused = false
	PauseMenu.visible = false
	set_process_mode(Node.PROCESS_MODE_ALWAYS)

func _process(_delta: float) -> void:
	if Input.is_action_just_pressed("ui_cancel"):
		toggle_pause()


func toggle_pause():	
	print(is_paused)
	if is_paused: 
		is_paused = false
	else:
		is_paused = true
	get_tree().paused = is_paused
	if $TextureRect.visible == true:
		$TextureRect.visible = false
	$TextureRect.visible = is_paused


func _on_button_quit_pressed() -> void:
	get_tree().quit()
	pass # Replace with function body.

func _on_butto_retry_pressed() -> void:
	get_tree().paused = false
	get_tree().reload_current_scene()
	pass # Replace with function body.

func _on_button_mute_pressed() -> void:
	if $"../../Audio1".volume_db==0:
		$"../../Audio1".volume_db=-80
		$"../../Audio2".volume_db=-80
	else:
		$"../../Audio1".volume_db=0
		$"../../Audio2".volume_db=0
