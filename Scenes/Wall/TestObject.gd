extends StaticBody3D
class_name TestObject



@onready var _mesh : MeshInstance3D = $Mesh

func _ready():
	var full_texture_path = 'res://Scenes/TestObject/TestObject.gd'
	var texture : Resource = load(full_texture_path)
	if not (texture is Texture):
		return
	
	_mesh.get_surface_override_material(0).set("albedo_texture", texture as Texture)


func _process(_delta):
	pass
