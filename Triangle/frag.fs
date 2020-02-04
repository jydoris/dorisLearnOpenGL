#version 330 core
out vec4 FragColor;
  
in vec4 vertexColor; // the input variable from the vertex shader (same name and same type)  
in vec2 texCoord;

uniform sampler2D texture1;
uniform sampler2D texture2;
void main()
{
    FragColor =  mix(texture(texture1, texCoord), texture(texture2, texCoord), 0.2);
} 
