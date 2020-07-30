##### 1. （如已有证书，请忽略这一步）首先上阿里云购买证书，照如图方式选择，可以获取免费版
![微信截图_20200730144354](http://www.crazystone.work:8090/upload/2020/7/%E5%BE%AE%E4%BF%A1%E6%88%AA%E5%9B%BE_20200730144354-700ef40c2df34e28a6686a7a4cddada6.png)
##### 2. 下载证书，服务器类型选择“Tomcat”，解压得到如图两个文件
![微信截图_20200730145601](http://www.crazystone.work:8090/upload/2020/7/%E5%BE%AE%E4%BF%A1%E6%88%AA%E5%9B%BE_20200730145601-fcf76336efca49eab6e34d8c41a68050.png)
##### 3. 使用以下命令将PFX格式证书转为jks格式
```bash
keytool -importkeystore -srckeystore domainname.pfx -destkeystore domainname.jks -srcstoretype PKCS12 -deststoretype JKS
```
提示输入口令时，就输入pfx-password.txt中的密码，三次都是。
##### 4. 修改Tomcat下conf/server.xml文件
```xml
<Connector port="443" protocol="HTTP/1.1" SSLEnabled="true"
maxThreads="150" scheme="https" secure="true"
<!-- 证书保存的路径 -->
keystoreFile="conf/*******.jks" 
<!-- 密钥库密码 -->
keystorePass="******"
clientAuth="false"/>
```
##### 5. 重启Tomcat即可

> 更多可参见：  
[*安装JKS格式证书_Tomcat服务器*](https://help.aliyun.com/document_detail/98908.html?spm=a2c4g.11186623.6.634.7f064b20bwcpdh)   
[*SSL证书 Tomcat服务器*](https://cloud.tencent.com/document/product/400/35224)