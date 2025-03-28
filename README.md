# Smudging
尝试使用 WinForms 通过网络协议提供给其他语言使用的 WebView2 窗体

## 运行项目  
1. 下载项目依赖：
   > dotnet restore
2. 运行项目：
    > dotnet run
3. 访问 http://localhost:9902/welcome  
   注意添加请求头: 
   > Content-Type: application/json  
   > X-Request-Level: 0

   `只支持 JSON 类型数据`

更多信息，请访问 `Smudging.src.Controller` 命名空间下的所有 `api` 文件，  
程序通过扫描该命名空间下的 `ApiCustom` 标记进行工作。