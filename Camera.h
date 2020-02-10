#ifndef CAMERA_H
#define CAMERA_H
#include <glad/glad.h>
#include "glm/glm.hpp"
#include "glm/gtc/matrix_transform.hpp"
#include "glm/gtc/type_ptr.hpp"

enum camera_Movement{
    FORWARD,
    BACKWARD,
    LEFT,
    RIGHT
};

// Default camera values
const float YAW         = -90.0f;
const float PITCH       =  0.0f;
const float SPEED       =  2.5f;
const float SENSITIVITY =  0.1f;
const float ZOOM        =  45.0f;

class Camera{
public:
    glm::vec3 m_cameraPos;
    glm::vec3 m_front;
    glm::vec3 m_up;

    glm::vec3 m_right;

    float m_pitch;
    float m_yaw;

    float m_movSpeed;
    float m_zoom;
    float m_sensitivity;

    Camera(glm::vec3 position = glm::vec3(0.0, 0.0, 0.0), glm::vec3 up = glm::vec3(0.0, 1.0, 0.0), float pitch = PITCH, float yaw = YAW):m_front(glm::vec3(0.0, 0.0, -1.0)), m_movSpeed(SPEED), m_zoom(ZOOM), m_sensitivity(SENSITIVITY){
        m_cameraPos = position;
        m_up = up;

        m_pitch = pitch;
        m_yaw = yaw;
        updateCameraVectors();
    }

    
    // Returns the view matrix calculated using Euler Angles and the LookAt Matrix
    glm::mat4 GetViewMatrix()
    {
        return glm::lookAt(m_cameraPos, m_cameraPos + m_front, m_up);
    }

    float getZoom(){
        return m_zoom;
    }

    void processKeyBoard(camera_Movement direction, float deltaTime){

        const float movSpeed = m_movSpeed* deltaTime;
        if (direction == FORWARD) {
            m_cameraPos += m_front * movSpeed;
        }
        if (direction == BACKWARD) {
            m_cameraPos -= m_front * movSpeed;
        }
        if (direction == RIGHT) {
            m_cameraPos += m_right * movSpeed;
        }
        if (direction == LEFT) {
            m_cameraPos -= m_right * movSpeed;
        }
        
    }

    void processMouse(float offsetX, float offsetY){

        offsetX *= m_sensitivity;
        offsetY *= m_sensitivity;

        m_yaw += offsetX;
        m_pitch += offsetY;

        if(m_pitch > 89.0f)
            m_pitch = 89.0f;
        if(m_pitch < -89.0f)
            m_pitch = -89.0f;

        updateCameraVectors();
    }

    void processScroll(float yoffset){
        if(m_zoom >= 1.0f && m_zoom <= 45.0f)
          m_zoom -= yoffset;
        else if(m_zoom <= 1.0f)
          m_zoom = 1.0f;
        else if(m_zoom > 45.0f)
          m_zoom = 45.0f;
    }
private:
    void updateCameraVectors(){
        glm::vec3 direction;
        direction.x = cos(glm::radians(m_yaw)) * cos(glm::radians(m_pitch));
        direction.y = sin(glm::radians(m_pitch));
        direction.z = sin(glm::radians(m_yaw)) * cos(glm::radians(m_pitch));
        m_front = glm::normalize(direction);

        m_right = glm::normalize(glm::cross(m_front, m_up));
        m_up = glm::normalize(glm::cross(m_right, m_front));
    }
};



#endif
