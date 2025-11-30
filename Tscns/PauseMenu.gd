extends Node
@export var PauseMenu: TextureRect
@export var ButtonQuit: Button

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


func _on_button_quit_pressed() -> void:
	get_tree().quit()
	pass # Replace with function body.

func _on_butto_retry_pressed() -> void:
	get_tree().paused = false
	get_tree().reload_current_scene()
	pass # Replace with function body.
