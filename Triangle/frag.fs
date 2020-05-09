#version 330 core
in vec3 Normal;
in vec3 FragPos;
in vec2 TexCoord; 

out vec4 FragColor;


struct Material{
	vec3 ambientPart;
	sampler2D diffuseMap;
	vec3 specularPart;
	float shiness;
};

struct Light{
	vec3 lightPos;

	vec3 ambientLig;
	vec3 diffuseLig;
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
	
	vec3 diffuse = max(dot(norm, lightVec), 0.0) *light.diffuseLig * texture(objectMate.diffuseMap, TexCoord).rgb;
	
	//specular light
	vec3 viewVec = normalize(viewPos - FragPos);
	vec3 lightReflect = reflect(-lightVec, norm);
	vec3 specular = pow(max(dot(viewVec, lightReflect), 0.0), objectMate.shiness) * light.specularLig * objectMate.specularPart;

	vec3  lightResCor = ambient + diffuse + specular;

    FragColor = vec4(lightResCor, 1.0);
} 
