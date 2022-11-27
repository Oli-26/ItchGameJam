extends CharacterBody3D

@onready var gunRay = $Head/Camera3d/RayCast3d as RayCast3D
@onready var Cam = $Head/Camera3d as Camera3D
@onready var playerHealth = $PlayerHealth
@onready var collisionShape3d = $CollisionShape3d
@export var _bullet_scene : PackedScene

var gravityToggle = true;
var lightToggle = false;
var mouseSensibility = 1200
var mouse_relative_x = 0
var mouse_relative_y = 0
const SPEED = 5.0
const JUMP_VELOCITY = 6.5

var lantern : Node
# Get the gravity from the project settings to be synced with RigidBody nodes.
var gravity = ProjectSettings.get_setting("physics/3d/default_gravity")

func _ready():
	#Captures mouse and stops rgun from hitting yourself
	gunRay.add_exception(self)
	lantern = $lantern2
	lantern.visible = false;
	Input.mouse_mode = Input.MOUSE_MODE_CAPTURED
	
func _physics_process(delta):
	if playerHealth.Health == 0:
		return
	
	var spritingMultiplier = 1;
	# Add the gravity.
	if gravityToggle and not is_on_floor():
		velocity.y -= gravity * delta

	# Handle Jump.
	if Input.is_action_just_pressed("Jump") and is_on_floor():
		velocity.y = JUMP_VELOCITY
	# Handle Shooting
	if Input.is_action_just_pressed("Use"):
		use()
		
	if Input.is_action_pressed ("Sprint"):
		spritingMultiplier = 1.7;
		
	if !Input.is_action_pressed ("Crouch"):
		collisionShape3d.shape.height = 2.0
	else:
		collisionShape3d.shape.height = 1.0
	
	if Input.is_action_just_pressed("ToggleGravity"):
		gravityToggle = !gravityToggle;
		
	if Input.is_action_just_pressed("ToggleLight"):
		lightToggle = !lightToggle;
		print(lightToggle)
		lantern.visible = lightToggle;
		
	# Get the input direction and handle the movement/deceleration.
	var input_dir = Input.get_vector("moveLeft", "moveRight", "moveUp", "moveDown")
	var direction = (transform.basis * Vector3(input_dir.x, 0, input_dir.y)).normalized()
	
	var horizontalVelocity = Vector3(velocity.x, 0, velocity.z)
	if horizontalVelocity.length() < SPEED * spritingMultiplier * 2:
		if direction:
			velocity.x = direction.x * SPEED * spritingMultiplier
			velocity.z = direction.z * SPEED * spritingMultiplier
		else:
			velocity.x = move_toward(velocity.x, 0, SPEED * spritingMultiplier)
			velocity.z = move_toward(velocity.z, 0, SPEED * spritingMultiplier)
			
	move_and_slide()

func _input(event):
	if event is InputEventMouseMotion:
		rotation.y -= event.relative.x / mouseSensibility
		$Head/Camera3d.rotation.x -= event.relative.y / mouseSensibility
		$Head/Camera3d.rotation.x = clamp($Head/Camera3d.rotation.x, deg_to_rad(-90), deg_to_rad(90) )
		mouse_relative_x = clamp(event.relative.x, -50, 50)
		mouse_relative_y = clamp(event.relative.y, -50, 10)

func use():
	if not gunRay.is_colliding():
		return
	var collider = gunRay.get_collider().get_parent()
	if collider.is_in_group("useable"):
		collider.Use(self)
	else:
		print('not useable')
