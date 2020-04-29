#ifndef SHADER_H
#define SHADER_H

#include <glad/glad.h>

#include "glm/glm.hpp"
#include "glm/gtc/matrix_transform.hpp"
#include "glm/gtc/type_ptr.hpp"


class Shader{
public:
    //program ID
    unsigned int ID;
    Shader(const char* vertexPath, const char* fragPath);

    void use();
    void uniformSetInt(const std::string &name, int value);
	void uniformSetFloat(const std::string &name, float value);
    void uniformSetMat4(const std::string &name, const glm::mat4 &value);
	void uniformSetVec3(const std::string &name, const glm::vec3 & value);
	void uniformSetVec3(const std::string &name, float a, float b, float c);
	void uniformSetVec4(const std::string &name, const glm::vec4 & value);
};
#endif
