# AspnetCoreScopeLogLab

## 配置

配置中增加 `"IncludeScopes": true`

## 使用

开启配置后默认会将请求日志分组，也可以使用 `using (_logger.BeginScope(xxxxx))` 传入自己的分组标识。

## 日志结果

会在日志中自动加入 `TraceId`, `ParentId`, `ConnectionId`, `RequestId`。

```none
info: AspnetCoreScopeLogLab.Controllers.LogTestController[0]
      => SpanId:4fcee7948a19be89, TraceId:f12bffd9c5a131c72a9e965ac9f3e8c0, ParentId:0000000000000000 => ConnectionId:0HME509KV7K85 => RequestPath:/LogTest/log RequestId:0HME509KV7K85:00000005 => AspnetCoreScopeLogLab.Controllers.LogTestController.Log (AspnetCoreScopeLogLab) => 450
      Enter action

info: AspnetCoreScopeLogLab.Controllers.LogTestController[0]
      => SpanId:4fcee7948a19be89, TraceId:f12bffd9c5a131c72a9e965ac9f3e8c0, ParentId:0000000000000000 => ConnectionId:0HME509KV7K85 => RequestPath:/LogTest/log RequestId:0HME509KV7K85:00000005 => AspnetCoreScopeLogLab.Controllers.LogTestController.Log (AspnetCoreScopeLogLab) => 450
      Start do something

info: AspnetCoreScopeLogLab.Controllers.LogTestController[0]
      => SpanId:4fcee7948a19be89, TraceId:f12bffd9c5a131c72a9e965ac9f3e8c0, ParentId:0000000000000000 => ConnectionId:0HME509KV7K85 => RequestPath:/LogTest/log RequestId:0HME509KV7K85:00000005 => AspnetCoreScopeLogLab.Controllers.LogTestController.Log (AspnetCoreScopeLogLab) => 450
      Finish do something

info: AspnetCoreScopeLogLab.Controllers.LogTestController[0]
      => SpanId:4fcee7948a19be89, TraceId:f12bffd9c5a131c72a9e965ac9f3e8c0, ParentId:0000000000000000 => ConnectionId:0HME509KV7K85 => RequestPath:/LogTest/log RequestId:0HME509KV7K85:00000005 => AspnetCoreScopeLogLab.Controllers.LogTestController.Log (AspnetCoreScopeLogLab) => 450
      Level action
```
