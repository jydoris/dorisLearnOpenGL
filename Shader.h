#ifndef SHADER_H
#define SHADER_H

#include <glad/glad.h>


class Shader{
public:
    //program ID
    unsigned int ID;
    Shader(const char* vertexPath, const char* fragPath);

    void use();
};
#endif
