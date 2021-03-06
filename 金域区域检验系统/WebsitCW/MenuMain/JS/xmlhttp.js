// JScript 文件

		
//创建一个XMLHttpRequest对象池		
var XMLHttp = 
{
    _objPool: [], 
    _getInstance: function () //获取对象实例
    {
        for (var i = 0; i < this._objPool.length; i ++) //循环当前对象池中的对象
        {
            if (this._objPool[i].readyState == 0 || this._objPool[i].readyState == 4) //对象是否空闲
            {
                return this._objPool[i]; //如果有空闲则返回空闲的对象。
            }
        }
        // IE5中不支持push方法
        this._objPool[this._objPool.length] = this._createObj(); //没有空闲就新建一个对象
        return this._objPool[this._objPool.length - 1]; 
    },
    _createObj: function ()
    {
        if (window.XMLHttpRequest)
        {
            var objXMLHttp = new XMLHttpRequest();
        }
        else
        {
            var MSXML = ['MSXML2.XMLHTTP.6.0', 'MSXML2.XMLHTTP.5.0', 'MSXML2.XMLHTTP.4.0', 'MSXML2.XMLHTTP.3.0', 'MSXML2.XMLHTTP', 'Microsoft.XMLHTTP'];
            for(var j = 0; j < MSXML.length; j++)
            {
                try
                {
                    var objXMLHttp = new ActiveXObject(MSXML[j]);
                    break;
                }
                catch(e)
                {
                    //什么都不做
                }
            }
         }    
               
        // mozilla某些版本没有readyState属性
        if (objXMLHttp.readyState == null)
        {
            objXMLHttp.readyState = 0;
            objXMLHttp.addEventListener("load", function ()
                {
                    objXMLHttp.readyState = 4;
                    if (typeof objXMLHttp.onreadystatechange == "function")
                    {
                        objXMLHttp.onreadystatechange();
                    }
                },  false);
        }
        return objXMLHttp;
    },
    // 发送请求(方法[post,get], 地址, 数据, 回调函数)
    sendReq: function (method, url, data, callback)
    {
        var objXMLHttp = this._getInstance();
        with(objXMLHttp)
        {
            try
            {
                // 加随机数防止缓存
                if (url.indexOf("?") > 0)
                {
                    url += "&randnum=" + Math.random();
                }
                else
                {
                    url += "?randnum=" + Math.random();
                }
                open(method, url, true);
                // 设定请求编码方式
                setRequestHeader('Content-Type', 'application/x-www-form-urlencoded; charset=UTF-8');
                send(data);
                onreadystatechange = function ()
                {
                    if (objXMLHttp.readyState == 4 && (objXMLHttp.status == 200 || objXMLHttp.status == 304))
                    {
                        callback(objXMLHttp);
                       //changeMark(objXMLHttp);
                    }
                }
            }
            catch(e)
            {
                
            }
        }
    }
};  