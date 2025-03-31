# Smudging
尝试使用 WinForms 通过网络协议操控的 WebView2 窗体\
龟速开发中...

## 运行项目  
1. 下载项目依赖：
   > dotnet restore
2. 运行项目：
    > dotnet run
3. 访问控制，窗口加载页面\
   注意 `必须` 添加请求头: 
   > Content-Type: application/json  
   > X-Request-Level: 0

   `只支持 JSON 类型数据`
   ```javascript
   var myHeaders = new Headers();
   // 必须
   myHeaders.append("X-Request-Level", "0");
   // 必须，只支持 JSON
   myHeaders.append("Content-Type", "application/json");

   myHeaders.append("User-Agent", "Apifox/1.0.0 (https://apifox.com)");
   myHeaders.append("Accept", "*/*");
   myHeaders.append("Host", "localhost:9902");
   myHeaders.append("Connection", "keep-alive");

   // 必须 JSON 格式数据
   var raw = JSON.stringify({
      "url": "https://www.microsoft.com/"
   });

   var requestOptions = {
      method: 'POST',
      headers: myHeaders,
      body: raw,
      redirect: 'follow'
   };

   fetch("http://localhost:9902/smudging/webview/source", requestOptions)
      .then(response => response.text())
      .then(result => console.log(result))
      .catch(error => console.log('error', error));
   ```
更多信息，请查看 `Smudging.src.Controller` 命名空间下的所有 api 文件，\
`smudging` 永远是 api 请求的前缀，它是自动加上去的，如：`http://localhost:9902/smudging`\
程序通过扫描该命名空间下的 `ApiCustom` 标记进行工作。
