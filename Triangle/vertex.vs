#version 330 core
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec3 aNormal;
layout (location = 2) in vec2 aTexCoord;
  
uniform mat4 model;
uniform mat4 view;
uniform mat4 proj;

out vec3 FragPos;
out vec3 Normal;
out vec2 TexCoord;

void main()
{
    gl_Position = proj * view * model * vec4(aPos, 1.0); // see how we directly give a vec3 to vec4's constructor
	Normal = mat3(transpose(inverse(model))) * aNormal;
	FragPos = vec3(model * vec4(aPos, 1.0));
	TexCoord = aTexCoord;
}
