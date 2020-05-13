#version 330 core
in vec3 Normal;
in vec3 FragPos;
in vec2 TexCoord; 

out vec4 FragColor;


struct Material{
	sampler2D diffuseMap;
	sampler2D specularMap;
	sampler2D emissionMap;
	float shiness;
};

struct Light{
	vec3 lightPos;

	vec3 ambientLig;
	vec3 diffuseLig;
	vec3 specularLig;
};

struct dirLight{
	vec3 direction;
	vec3 ambient;
	vec3 diffuse;
	vec3 specular;
};

uniform Material objectMate;
//uniform Light light;
uniform dirLight dirlight;
uniform vec3 viewPos;


void main()
{
	//vec3 ambient = light.ambientLig * texture(objectMate.diffuseMap, TexCoord).rgb;
   	//
	////diffuse light
	//vec3 lightVec = normalize(light.lightPos - FragPos);
	//vec3 norm = normalize(Normal);
	//vec3 diffuse = max(dot(norm, lightVec), 0.0) *light.diffuseLig * texture(objectMate.diffuseMap, TexCoord).rgb;
	//
	////specular light
	//vec3 viewVec = normalize(viewPos - FragPos);
	//vec3 lightReflect = reflect(-lightVec, norm);
	//float spec =  pow(max(dot(viewVec, lightReflect), 0.0), objectMate.shiness);
	//vec3 specular = spec * light.specularLig * texture(objectMate.specularMap, TexCoord).rgb;
	//
	//vec3  lightResCor = ambient + diffuse + specular + texture(objectMate.emissionMap, TexCoord).rgb;


	vec3 ambient = dirlight.ambient * texture(objectMate.diffuseMap, TexCoord).rgb;
	
	//diffuse light
	vec3 lightVec = normalize(-dirlight.direction);
	vec3 norm = normalize(Normal);
	vec3 diffuse = max(dot(norm, lightVec), 0.0) *dirlight.diffuse * texture(objectMate.diffuseMap, TexCoord).rgb;
	
	//specular light
	vec3 viewVec = normalize(viewPos - FragPos);
	vec3 lightReflect = reflect(-lightVec, norm);
	float spec =  pow(max(dot(viewVec, lightReflect), 0.0), objectMate.shiness);
	vec3 specular = spec * dirlight.specular * texture(objectMate.specularMap, TexCoord).rgb;
	
	//emission part
	vec3 emission = texture(objectMate.emissionMap, TexCoord).rgb;

	vec3  lightResCor = ambient + diffuse + specular ;
	

    FragColor = vec4(lightResCor, 1.0);
} 
