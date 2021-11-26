# IdentityServer4FullLab

## 介绍

主要验证 IdentityServer4 的多种认证授权方式，以及不同客户端使用 IdentityServer4 的流程，最后配合第三方登录进行测试。

## 搭建 IdentityServer4

参见 `IdentityServer.Sample` 程序集，使用的 nuget 包如下：

- 

### 使用数据库存储

本使用直接使用 Sqlite，免去其他外部依赖，nuget 包如下：


### IdentityServer4 UI

框架自带了一个支撑 OIDC 的 UI 框架，使用模板安装

### 添加自定义 Claim

### IdentityServer 颁发 token 回调

在该回调中可以做一些事情，比如往共享 redis 中写入用户的一些信息，用以在验证 token 时供 API 服务器比较，如果后端让 token 失效的操作就很方便了，只需要把 redis 中的用户记录信息做修改下次匹配验证 token 时就会失败。

## 搭建 API Server

模拟要保护的 API 服务，依据角色权限等设置不同级别的 API

## 搭建客户端

### Console Client

### MVC Client

### SPA Client

使用 oidc 的 js 包
