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

struct Light{
	vec3 lightPos;

	vec3 ambientLig;
	vec4 diffuseLig;
	vec3 specularLig;
};

uniform Material objectMate;
uniform Light light;
uniform vec3 viewPos;

void main()
{
	vec3 ambient = light.ambientLig * objectMate.ambientPart;
   
	//diffuse light
	vec3 lightVec = normalize(light.lightPos - FragPos);
	vec3 norm = normalize(Normal);
	vec3 diffuse = max(dot(lightVec, norm), 0.0) * objectMate.diffusePart;
	
	//specular light
	vec3 viewVec = normalize(viewPos - FragPos);
	vec3 lightReflect = reflect(-lightVec, norm);
	vec3 specular = pow(max(dot(lightReflect, viewVec), 0.0), objectMate.shiness) * light.specularLig;

	vec3  lightResCor = ambient + diffuse + specular;

    FragColor = vec4(lightResCor, 1.0);
} 
