#include <string>
#include <iostream>
#include <fstream>
#include <sstream>
#include "Shader.h"

Shader::Shader(const char* vertexPath, const char* fragPath)
{
    std::string vertexCode;
    std::string fragCode;
    std::ifstream vShaderFile;
    std::ifstream fShaderFile;

    vShaderFile.open(vertexPath);
    fShaderFile.open(fragPath);

    std::stringstream vShaderStream, fShaderStream;

    vShaderStream << vShaderFile.rdbuf();
    fShaderStream << fShaderFile.rdbuf();

    vShaderFile.close();
    fShaderFile.close();

    vertexCode = vShaderStream.str();
    fragCode = fShaderStream.str();

    const char* vShaderSource = vertexCode.c_str();
    const char* fShaderSource = fragCode.c_str();

    unsigned int vertexShader;
    vertexShader = glCreateShader(GL_VERTEX_SHADER);
    glShaderSource(vertexShader, 1, &vShaderSource, NULL);
    glCompileShader(vertexShader);

    int success;
       char infoLog[512];
       glad_glGetShaderiv(vertexShader, GL_COMPILE_STATUS, &success);
       if (!success) {
           glad_glGetShaderInfoLog(vertexShader, 512.,NULL, infoLog);
           std::cout << "ERROR::SHADER::VERTEX::COMPILATION_FAILED\n" << infoLog << std::endl;
       }

    unsigned int fragShader;
    fragShader = glCreateShader(GL_FRAGMENT_SHADER);
    glShaderSource(fragShader, 1, &fShaderSource, NULL);
    glCompileShader(fragShader);

    glad_glGetShaderiv(fragShader, GL_COMPILE_STATUS, &success);
    if (!success) {
        glad_glGetShaderInfoLog(fragShader, 512.,NULL, infoLog);
        std::cout << "ERROR::SHADER::FRAGMENT::COMPILATION_FAILED\n" << infoLog << std::endl;
    }

    ID = glCreateProgram();

    glAttachShader(ID, vertexShader);
    glAttachShader(ID, fragShader);
    glLinkProgram(ID);

    glGetProgramiv(ID, GL_LINK_STATUS, &success);
    if(!success) {
        glGetProgramInfoLog(ID, 512, NULL, infoLog);
        std::cout << "ERROR::SHADER::PROGRAM::COMPILATION_FAILED\n" << infoLog << std::endl;
    }

    glDeleteShader(vertexShader);
    glDeleteShader(fragShader);
}

void Shader::use()
{
    glUseProgram(ID);
}
