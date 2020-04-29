#version 330 core
in vec3 Normal;
in vec3 FragPos;

out vec4 FragColor;


struct Material{
	vec3 ambientPart;
	vec3 diffusePart;
	vec3 specularPart;
	float shiness;
};



uniform Material objectMate;
uniform vec3 lightColor;
uniform vec3 lightPos;
uniform vec3 viewPos;

void main()
{
	float ambStrength = 0.1;
	vec3 ambientFac = lightColor * ambStrength;
   
	//diffuse light
	vec3 lightVec = normalize(lightPos - FragPos);
	vec3 norm = normalize(Normal);
	float diffuseFac = max(dot(lightVec, norm), 0.0);
	
	//specular light
	float specStrength = 0.5;
	vec3 viewVec = normalize(viewPos - FragPos);
	vec3 lightReflect = reflect(-lightVec, norm);
	float specFac = pow(max(dot(lightReflect, viewVec), 0.0), objectMate.shiness) *specStrength;

	vec3  lightResCor = (ambientFac * objectMate.ambientPart + diffuseFac * objectMate.diffusePart+ specFac * objectMate.specularPart) * lightColor;

    FragColor = vec4(lightResCor, 1.0);
} 
