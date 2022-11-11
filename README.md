# GoViewDotnet

#### 介绍
GoView .Net后台服务

#### 软件架构
软件架构说明
计划实现wtm和volcore两大.NET快速开发框架后台对接
目前wtm已实现

#### goview安装教程

1.  按照goview官方提供办法启动项目
1.  wtm 修改env文件的 VITE_DEV_PATH = "http://localhost:7960/"  VITE_DEV_PORT = '7960'
2.  volcore 修改env文件的 VITE_DEV_PATH = "http://localhost:9991/"  VITE_DEV_PORT = '9991'
3.  由于 goview目前没有处理401错误的封装，在axios.ts 中 响应拦截器的错误中添加对应的跳转即可
    (err: AxiosResponse) => {
    window['$message'].error(window['$t']('http.token_overdue_message'))
    routerTurnByName(PageEnum.BASE_LOGIN_NAME)
    Promise.reject(err)
  }


#### wtm安装教程

1.  wtm框架为net6.0版本，启动前确认本机知否安装.NET6 或者 vs2022
1.  下载或者clone源码 wtm项目再src/wtm中
2.  VS双击GoViewWtm.sln vscode则再wtm目录打开
3.  在GoViewWtm修改appsettings.json配置文件中Connections数据库连接字符串，支持mysql,sqlserver,pgsql,sqlite,oracle。其中DBType是目标数据库，Value是连接字符串。
4.  项目启动为ef codefirst方式，如果是一次试用，建议数据库连接字符串中Database随便给一个数据库名称就行了，不要使用已经存在的数据库名称，也不需要手动去创建空的库。ef会给你自动生成。
5.  项目启动成功后账号是admin 密码000000

#### volcoew安装教程

1.  volcore框架为net3.1版本，启动前确认本机知否安装.NET Core 3.1 或者 vs2019 vs2022
1.  下载或者clone源码 wtm项目再src/volcore中
2.  VS双击VOL.sln vscode则再Vue.Net目录打开
3.  在VOL.WebApi修改appsettings.json配置文件中Connection数据库连接字符串，支持mysql,sqlserver,pgsql。其中DBType是目标数据库，DbConnectionString是连接字符串。
4.  项目启动为DB codefirst方式，如果是一次试用，先执行volcore\DB 下对应数据库脚本goviewvolcore.sql 目前仅提供mysql
5.  项目启动成功后账号是admin 密码123456




#### 开源项目地址

1.  goview https://gitee.com/dromara/go-view
2.  wtm https://gitee.com/liuliang-wtm/WTM?_from=gitee_search
3.  volcore https://gitee.com/x_discoverer/Vue.NetCore



#### 说明
有问题，提交issue

