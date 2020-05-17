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
	float outLine;

	vec3 ambient;
	vec3 diffuse;
	vec3 specular;
};

#define POINT_LIGHT_NUM 2 
uniform Material objectMate;
uniform pointLight pointlight[POINT_LIGHT_NUM];
uniform dirLight dirlight;
uniform spotLight spotlight;
uniform vec3 viewPos;

vec3 calSpotLight(spotLight light);
vec3 calDirLight(dirLight light);
vec3 calPointLight(pointLight light);

void main()
{

	vec3 lightResCor = vec3(0.0f);
	lightResCor += calDirLight(dirlight);
	for(int i = 0; i < POINT_LIGHT_NUM; i++)
		lightResCor += calPointLight(pointlight[i]);
	lightResCor += calSpotLight(spotlight);

    FragColor = vec4(lightResCor, 1.0);
} 

vec3 calSpotLight(spotLight light)
{
	vec3 ambient = light.ambient * texture(objectMate.diffuseMap, TexCoord).rgb;
   	
	//diffuse light
	vec3 lightVec = normalize(light.position - FragPos);
	vec3 norm = normalize(Normal);
	vec3 diffuse = max(dot(norm, lightVec), 0.0) *light.diffuse * texture(objectMate.diffuseMap, TexCoord).rgb;
	
	float theta = dot(lightVec, normalize(-light.direction));
	float epsilon = light.cutOff - light.outLine;
	float factor =  theta > light.outLine? (theta - light.outLine) / epsilon: 0.0;
	
	//specular light
	vec3 viewVec = normalize(viewPos - FragPos);
	vec3 lightReflect = reflect(-lightVec, norm);
	float spec =  pow(max(dot(viewVec, lightReflect), 0.0), objectMate.shiness);
	vec3 specular = spec * light.specular * texture(objectMate.specularMap, TexCoord).rgb;
	

	diffuse *= factor;
	specular *= factor;
	vec3  lightResCor = ambient + diffuse + specular;
	return lightResCor;
}


vec3 calPointLight(pointLight light)
{
	vec3 ambient = light.ambient * texture(objectMate.diffuseMap, TexCoord).rgb;
   	
	//diffuse light
	vec3 lightVec = normalize(light.lightPos - FragPos);
	vec3 norm = normalize(Normal);
	vec3 diffuse = max(dot(norm, lightVec), 0.0) *light.diffuse * texture(objectMate.diffuseMap, TexCoord).rgb;
	
	//specular light
	vec3 viewVec = normalize(viewPos - FragPos);
	vec3 lightReflect = reflect(-lightVec, norm);
	float spec =  pow(max(dot(viewVec, lightReflect), 0.0), objectMate.shiness);
	vec3 specular = spec * light.specular * texture(objectMate.specularMap, TexCoord).rgb;
	
	
	//attenuation
	float distance = length(light.lightPos - FragPos);
	float attenuation = 1.0 / (light.constant + distance * light.linear + distance * distance * light.quadratic);
	
	ambient *= attenuation;
	diffuse *= attenuation;
	specular *= attenuation;

	vec3  lightResCor = ambient + diffuse + specular ;
	return lightResCor;
}

vec3 calDirLight(dirLight light)
{
	vec3 ambient = light.ambient * texture(objectMate.diffuseMap, TexCoord).rgb;
	
	//diffuse light
	vec3 lightVec = normalize(-light.direction);
	vec3 norm = normalize(Normal);
	vec3 diffuse = max(dot(norm, lightVec), 0.0) *light.diffuse * texture(objectMate.diffuseMap, TexCoord).rgb;
	
	//specular light
	vec3 viewVec = normalize(viewPos - FragPos);
	vec3 lightReflect = reflect(-lightVec, norm);
	float spec =  pow(max(dot(viewVec, lightReflect), 0.0), objectMate.shiness);
	vec3 specular = spec * light.specular * texture(objectMate.specularMap, TexCoord).rgb;
	
	vec3  lightResCor = ambient + diffuse + specular ;
	return lightResCor;
}