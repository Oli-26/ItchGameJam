shader_type spatial;
render_mode unshaded;

uniform sampler2D tex_vtx_2 : hint_normal;
uniform sampler2D tex_lava : source_color;
uniform float time;

void vertex() {
	vec3 n_out3p0 = VERTEX;

	vec2 samplePosition = UV * 0.5 
	+ vec2(time * 0.05, 0) * 0.5;

	vec2 samplePosition2 = UV * 0.5 
	+ vec2(-time * 0.088, 0) * 0.3;
	
	float height = (
		texture(tex_vtx_2, samplePosition).r
		+ texture(tex_vtx_2, samplePosition2).r)
		/ 2.0;
	
	VERTEX = VERTEX + vec3(0, height, 0);
}

void fragment() {
	ALBEDO = texture(tex_lava, UV).rgb;	
}
