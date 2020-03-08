cmake_minimum_required(VERSION 3.10)

# set the project name and version
project(dorisLearnOpenGL VERSION 1.0)

file(GLOB SOURCE
"*.h"
"*.cpp"
"*.c"
"Triangle/*fs"
"Triangle/*vs"
"Triangle/*cpp")

set(INCLUDEDIR include)
set(INCLUDEDIR ${INCLUDEDIR} ${PROJECT_SOURCE_DIR})
# add the executable
add_executable(dorisLearnOpenGL ${SOURCE})

message(STATUS "PROJECT_SOURCE_DIR is ${PROJECT_SOURCE_DIR}")
message(STATUS "CMAKE_SOURCE_DIR is ${CMAKE_SOURCE_DIR}")


# link_directories(${PROJECT_SOURCE_DIR}/libs)
find_library(LIB_PATH glfw3 ${PROJECT_SOURCE_DIR}/libs)
IF(NOT LIB_PATH)
MESSAGE(FATAL_ERROR "libs not found")
ENDIF(NOT LIB_PATH) 

target_link_libraries(dorisLearnOpenGL ${LIB_PATH})


target_include_directories(dorisLearnOpenGL PUBLIC
                           "${INCLUDEDIR}"
                           )
