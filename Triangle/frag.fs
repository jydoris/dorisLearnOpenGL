#version 330 core
in vec3 Normal;
in vec3 FragPos;

out vec4 FragColor;

uniform vec3 objectColor;
uniform vec3 lightColor;
uniform vec3 lightPos;

void main()
{
	float ambientStrength = 0.1;
	vec3 ambientLight = lightColor * ambientStrength;
   
	//diffuse light
	vec3 lightVec = normalize(lightPos - FragPos);
	vec3 norm = normalize(Normal);
	float diffuseFac = max(dot(lightVec, norm), 0.0);
	
	
	vec3  lightResCor = (ambientLight + diffuseFac) * lightColor;

    FragColor = vec4(objectColor * lightResCor, 1.0);
	//FragColor = vec4(lightPos, 1.0);
} 
