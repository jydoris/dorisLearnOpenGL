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
    void uniformSetMat4(const std::string &name, const glm::mat4 &value);
};
#endif
