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

    vShaderFile.exceptions(std::ifstream::failbit | std::ifstream::badbit);
    fShaderFile.exceptions(std::ifstream::failbit | std::ifstream::badbit);

    try {
        vShaderFile.open(vertexPath);
        fShaderFile.open(fragPath);

        std::stringstream vShaderStream, fShaderStream;

        vShaderStream << vShaderFile.rdbuf();
        fShaderStream << fShaderFile.rdbuf();

        vShaderFile.close();
        fShaderFile.close();

        vertexCode = vShaderStream.str();
        fragCode = fShaderStream.str();
    } catch (std::ifstream::failure e) {
         std::cout << "ERROR::SHADER::FILE_NOT_SUCCESFULLY_READ" << std::endl;
    }


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

void Shader::uniformSetInt(const std::string &name, int value)
{
    unsigned int loc = glGetUniformLocation(ID, name.c_str());
    glUniform1i(loc, value);
}
void Shader::uniformSetMat4(const std::string &name, const glm::mat4 &mat)
{
    unsigned int loc = glGetUniformLocation(ID, name.c_str());
    glUniformMatrix4fv(loc, 1, GL_FALSE, &mat[0][0]);
}

void Shader::uniformSetVec3(const std::string &name, const glm::vec3 &value)
{
	unsigned int loc = glGetUniformLocation(ID, name.c_str());
	glUniform3fv(loc, 1, &value[0]);
}

void Shader::uniformSetVec4(const std::string &name, const glm::vec4 &value)
{
	unsigned int loc = glGetUniformLocation(ID, name.c_str());
	glUniform4fv(loc, 1, &value[0]);
}
