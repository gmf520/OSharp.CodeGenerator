# OSharp代码生成器

## 介绍

OSharp代码生成器，管理 **项目 -> 模块 -> 实体 -> 实体属性** 的结构数据，生成各个模块的实体类，DTO类，业务Service类，API控制器类等各层次的代码

## 生成器使用流程

1. 配置代码生成的需要的项目信息，项目的各个模块信息，模块的各个实体信息，实体的各个属性信息，保存为配置文件
2. 基于项目配置信息和内置的已编译Razor模板，使用Razor引擎`RazorEngine`生成 实体类，实体配置类，InputDto，OutputDto，业务层接口，业务层实现，MVC控制器代码 等项目各个层次的代码
3. 将生成的代码整合进项目工程中，通过类继承和partial分部类对生成代码进行扩展，实现实际的业务需求

## 代码生成流程

### 1. 创建项目，加载项目

![image-20210414132119855](https://gitee.com/i66soft/blog-media/raw/master/osharp/image-20210414132119855.png)

![image-20210414131234889](https://gitee.com/i66soft/blog-media/raw/master/osharp/image-20210414131234889.png)



### 2. 添加 模块-实体-实体属性 层级数据

#### 2.1 在项目下添加模块信息

![image-20210414131635727](https://gitee.com/i66soft/blog-media/raw/master/osharp/image-20210414131635727.png)

#### 2.2 在模块下添加实体信息

![image-20210414131733607](https://gitee.com/i66soft/blog-media/raw/master/osharp/image-20210414131733607.png)

#### 2.3 在实体下添加属性信息

![image-20210414131812823](https://gitee.com/i66soft/blog-media/raw/master/osharp/image-20210414131812823.png)

#### 2.4 在实体中配置外键信息

![image-20210414131856270](https://gitee.com/i66soft/blog-media/raw/master/osharp/image-20210414131856270.png)

### 3. 生成代码

#### 3.1 全局模板管理，可以添加自定义模板

![image-20210414132255249](https://gitee.com/i66soft/blog-media/raw/master/osharp/image-20210414132255249.png)

#### 3.2 项目模板管理，给项目选择要用到的模板

![image-20210414132339423](https://gitee.com/i66soft/blog-media/raw/master/osharp/image-20210414132339423.png)

#### 3.3 生成代码

![image-20210414132414305](https://gitee.com/i66soft/blog-media/raw/master/osharp/image-20210414132414305.png)

#### 3.4 生成的代码

![image-20210414132629986](https://gitee.com/i66soft/blog-media/raw/master/osharp/image-20210414132629986.png)
