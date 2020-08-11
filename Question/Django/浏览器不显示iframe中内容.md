#### 1.不显示iframe中内容  
浏览器打开页面报错：  
![微信截图_20200811174821.png](http://www.crazystone.work:8090/upload/2020/8/%E5%BE%AE%E4%BF%A1%E6%88%AA%E5%9B%BE_20200811174821-65758a3733224834b85711dfde08941b.png)  
> Refused to display 'http://localhost:8000/cdny/student_manage/' in a frame because it set 'X-Frame-Options' to 'deny'.  
  
解决方案：    
settings.py中加一条配置：    
```python  
X_FRAME_OPTIONS = 'SAMEORIGIN'    
# DENY ：表示该页面不允许在 frame 中展示，即便是在相同域名的页面中嵌套也不允许    
# SAMEORIGIN ：表示该页面可以在相同域名页面的 frame 中展示    
# ALLOW-FROM uri ：表示该页面可以在指定来源的 frame 中展示    
```  
  
#### 2.不加载js文件  
浏览器打开页面报错：    
![微信图片_20200811174053.png](http://www.crazystone.work:8090/upload/2020/8/%E5%BE%AE%E4%BF%A1%E5%9B%BE%E7%89%87_20200811174053-3a2324594587433c8b365ecbd98b2ce7.png)    
>Refused to execute script from 'http://127.0.0.1:8000/static/layui/layui.js' because its MIME type ('text/plain') is not executable, and strict MIME type checking is enabled.    
  
解决方案：  
修改settings.py配置  
```python  
SECURE_CONTENT_TYPE_NOSNIFF = False  
```  
**<font color="red">然后清空浏览器缓存，重启浏览器</font>**  