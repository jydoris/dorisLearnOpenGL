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

struct pointLight{
	vec3 lightPos;

	vec3 ambient;
	vec3 diffuse;
	vec3 specular;

	float constant;
	float linear;
	float quadratic;
};

struct dirLight{
	vec3 direction;
	vec3 ambient;
	vec3 diffuse;
	vec3 specular;
};

struct spotLight{
	vec3 position;
	vec3 direction;
	float cutOff;

	vec3 ambient;
	vec3 diffuse;
	vec3 specular;
};

uniform Material objectMate;
uniform pointLight pointlight;
uniform dirLight dirlight;
uniform spotLight spotlight;
uniform vec3 viewPos;


void main()
{
	//vec3 ambient = pointlight.ambient * texture(objectMate.diffuseMap, TexCoord).rgb;
   	//
	////diffuse light
	//vec3 lightVec = normalize(pointlight.lightPos - FragPos);
	//vec3 norm = normalize(Normal);
	//vec3 diffuse = max(dot(norm, lightVec), 0.0) *pointlight.diffuse * texture(objectMate.diffuseMap, TexCoord).rgb;
	//
	////specular light
	//vec3 viewVec = normalize(viewPos - FragPos);
	//vec3 lightReflect = reflect(-lightVec, norm);
	//float spec =  pow(max(dot(viewVec, lightReflect), 0.0), objectMate.shiness);
	//vec3 specular = spec * pointlight.specular * texture(objectMate.specularMap, TexCoord).rgb;
	//
	////emission part
	//vec3 emission = texture(objectMate.emissionMap, TexCoord).rgb;
	//
	////attenuation
	//float distance = length(pointlight.lightPos - FragPos);
	//float attenuation = 1.0 / (pointlight.constant + distance * pointlight.linear + distance * distance * pointlight.quadratic);
	//
	//ambient *= attenuation;
	//diffuse *= attenuation;
	//specular *= attenuation;
	//vec3  lightResCor = ambient + diffuse  + specular;


	//vec3 ambient = dirlight.ambient * texture(objectMate.diffuseMap, TexCoord).rgb;
	//
	////diffuse light
	//vec3 lightVec = normalize(-dirlight.direction);
	//vec3 norm = normalize(Normal);
	//vec3 diffuse = max(dot(norm, lightVec), 0.0) *dirlight.diffuse * texture(objectMate.diffuseMap, TexCoord).rgb;
	//
	////specular light
	//vec3 viewVec = normalize(viewPos - FragPos);
	//vec3 lightReflect = reflect(-lightVec, norm);
	//float spec =  pow(max(dot(viewVec, lightReflect), 0.0), objectMate.shiness);
	//vec3 specular = spec * dirlight.specular * texture(objectMate.specularMap, TexCoord).rgb;
	//
	////emission part
	//vec3 emission = texture(objectMate.emissionMap, TexCoord).rgb;
	//
	//vec3  lightResCor = ambient + diffuse + specular ;
	
	//spot light
	vec3 ambient = spotlight.ambient * texture(objectMate.diffuseMap, TexCoord).rgb;
   	
	//diffuse light
	vec3 lightVec = normalize(spotlight.position - FragPos);
	vec3 norm = normalize(Normal);
	vec3 diffuse = max(dot(norm, lightVec), 0.0) *spotlight.diffuse * texture(objectMate.diffuseMap, TexCoord).rgb;
	

	float factor = dot(lightVec, normalize(-spotlight.direction)) > spotlight.cutOff? 1.0 : 0.0;
	
	//specular light
	vec3 viewVec = normalize(viewPos - FragPos);
	vec3 lightReflect = reflect(-lightVec, norm);
	float spec =  pow(max(dot(viewVec, lightReflect), 0.0), objectMate.shiness);
	vec3 specular = spec * spotlight.specular * texture(objectMate.specularMap, TexCoord).rgb;
	
	//emission part
	vec3 emission = texture(objectMate.emissionMap, TexCoord).rgb;

	diffuse *= factor;
	specular *= factor;
	vec3  lightResCor = ambient + diffuse + specular;



    FragColor = vec4(lightResCor, 1.0);
} 
