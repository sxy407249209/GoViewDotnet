# GoViewDotnet

#### 介绍
GoView .Net后台服务

#### 软件架构
软件架构说明
计划实现wtm和volcore两大.NET快速开发框架后台对接
目前wtm已实现

#### wtm安装教程

1.  wtm框架为net6.0版本，启动前确认本机知否安装.NET6 或者 vs2022
1.  下载或者clone源码 wtm项目再src/wtm中
2.  VS双击GoViewWtm.sln vscode则再wtm目录打开
3.  在GoViewWtm修改appsettings.json配置文件中Connections数据库连接字符串，支持mysql,sqlserver,pgsql,sqlite,oracle。其中DBType是目标数据库，Value是连接字符串。
4.  项目启动为ef codefirst方式，如果是一次试用，建议数据库连接字符串中Database随便给一个数据库名称就行了，不要使用已经存在的数据库名称，也不需要手动去创建空的库。ef会给你自动生成。
5.  项目启动成功后账号是admin 密码000000



#### 开源项目地址

1.  goview https://gitee.com/dromara/go-view
2.  wtm https://gitee.com/liuliang-wtm/WTM?_from=gitee_search
3.  volcore https://gitee.com/x_discoverer/Vue.NetCore



#### 说明
有问题，提交issue

