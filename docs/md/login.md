# 登录参数
| username | 用户名或邮箱 |
| ------- | ------- |
| password | 密码 |
## 登录成功API返回
```
{
    "msg": "登录成功",
    "code": 200,
    "data": {
        "userName": "Qyzgj",
        "password": "null",
        "realName": "已实名",
        "usertoken": "token",
        "qianDao": "false",
        "userGroup": "普通用户",
        "integral": 5,
        "abroadBandwidth": 16,
        "bandwidth": 8,
        "qq": 2976779544,
        "tunnel": 2,
        "usedTunnel": 1,
        "mail": "2976779544@qq.com",
        "userID": 1,
        "userImg": "https:www.example.com/logo.png",
        "identityID": "4049************01",
        "dateOut": null
    },
    "state": "success"
}
```
### 登录成功API解析
| 字段          | 描述     |
| ------------- | -------- |
| username      | 用户名   |
| password      | 密码     |
| email         | 邮箱     |
| usertoken     | 令牌     |
| realName      | 实名状态 |
| usergroup     | 用户等级 |
| userimg       | 用户头像 |
| integral      | 积分数   |
| bandwidth     | 国内带宽 |
| abroadBandwidth | 国外带宽 |
| tunnel        | 隧道拥有数 |
| usedTunnel    | 隧道使用数 |
### 示例代码 C#
``` csharp
using Newtonsoft.Json.Linq;

class Program
{
    static void Main()
    {
            string jsonContent = System.IO.File.ReadAllText("path/to/your/jsonfile.json"); // 为文件路径
            var jsonObject = JObject.Parse(jsonContent);
            string msg = jsonObject["msg"]?.ToString(); // 读取是否登录状态
    }
}
```
## 登录失败API返回
### 一
```
{
    "msg": "用户不存在",
    "code": 401,
    "state": "fail"
}
```
### 二
```
{
    "msg": "密码错误",
    "code": 401,
    "state": "fail"
}
```
### 三
```
{
    "msg": "密码不符合要求，长度在6到48个字符之间，并且至少包含字母、数字和符号中的两种。",
    "code": 400,
    "state": "fail"
}
```
> 声明：部分示例来自[这里](https://github.com/boringstudents/chmlfrp_v2api)，还有部分来自[这里](https://apifox.com/apidoc/shared-24b31bd1-e48b-44ab-a486-81cf5f964422/)，在此感谢。