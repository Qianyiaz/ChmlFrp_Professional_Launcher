# 登录参数
| 参数 | 描述 |
| ------- | ------- |
| username | 用户名或邮箱 |
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
        "integral": 114514,
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
| 字段 | 描述 | 例举 |
| --------- | -------- | --------  |
| username      | 用户名   | Qyzgj |
| password      | 密码     | null |
| email         | 邮箱     | <2976779544@qq.com> |
| usertoken     | 令牌     | token |
| realName      | 实名状态 | 已实名 |
| usergroup     | 用户等级 | 普通用户 |
| userimg       | 用户头像 | https:www.example.com/logo.png |
| integral      | 积分数   | 114514 |
| bandwidth     | 国内带宽 | 8 |
| abroadBandwidth | 国外带宽 | 16 |
| tunnel        | 隧道拥有数 | 2 |
| usedTunnel    | 隧道使用数 | 1 |
### 示例代码 C#
``` csharp
using Newtonsoft.Json.Linq; // 使用Json.net,自行下载。

class Program
{
    static void Main()
    {
            // 不提供下载方法，自行学习。
            string jsonContent = System.IO.File.ReadAllText("path/to/your/jsonfile.json"); // 为文件路径
            var jsonObject = JObject.Parse(jsonContent);
            string username = jsonObject["data"]["username"]?.ToString(); // 读取用户名。
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
