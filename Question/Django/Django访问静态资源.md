## 在html页面中访问静态资源  
### 1.settings.py配置  

```python  
DEBUG = False  
STATIC_ROOT = 'static'  
```  

### 2.url.py配置  
```python  
from django.urls import path, include, re_path  
from django.views import static  
from django.conf import settings  

urlpatterns = [  
    re_path(r'^static/(?P<path>.*)$', static.serve, {'document_root': settings.STATIC_ROOT}, name='static')  
]  
```

### 3.html页面中访问路径  
```html  
<javascript src="/static/config.js" />  
```

## 在views.py中读写静态资源  
```python  
fo = open('static/config.js', 'w')  
```
