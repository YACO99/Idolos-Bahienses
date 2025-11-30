extends Node
@export var PauseMenu: TextureRect

func _ready():
	get_tree().paused = false
	PauseMenu.visible = false
	set_process_mode(Node.PROCESS_MODE_ALWAYS)

func _input(event:InputEvent) -> void:
	if Input.is_action_just_pressed ("ui_cancel"):
		toggle_pause()

func toggle_pause():
	var is_paused = get_tree().is_paused()
	get_tree().paused = not is_paused
	PauseMenu.visible = not is_paused

func _on_Button_pressed () -> void:
	get_tree().quit()
