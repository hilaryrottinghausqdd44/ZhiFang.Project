/**
 * Ajax数据处理
 * @author Jcall
 * @version 2014-07-24
 */
Ext.define('Shell.ux.server.Ajax',{
	/**
	 * POST方式与后台交互,callback返回response.responseText
	 * @param {} url 服务地址
	 * @param {} params 参数
	 * @param {} callback 回调函数
	 * @param {} async 是否异步,默认false
	 * @param {} timeout 超时时间,默认30秒
	 * @param {} defaultPostHeader 请求头(选配)，默认为application/json
	 */
	postToServer:function(url,params,callback,async,timeout,defaultPostHeader){
		var t = timeout || 30000;
	    this.uiToServer('POST',url,callback,async,params,t,defaultPostHeader);
	},
	/**
	 * GET方式与后台交互,返回response.responseText
	 * @param {} url 服务地址
	 * @param {} callback 回调函数
	 * @param {} async 是否异步,默认false
	 * @param {} timeout 超时时间,默认30秒
	 */
	getToServer:function(url,callback,async,timeout){
		var t = timeout || 30000;
	    this.uiToServer('GET',url,callback,async,null,t,null);
	},
	/**
	 * 前后台交互
	 * @param {} url url 服务地址
	 * @param {} callback 回调函数
	 * @param {} async 是否异步,默认false
	 * @param {} params POST参数
	 * @param {} timeout 超时时间,默认30秒
	 * @param {} defaultPostHeader 请求头(选配)，默认为application/json
	 */
	uiToServer:function(method,url,callback,async,params,timeout,defaultPostHeader){
		Ext.Ajax.defaultPostHeader = defaultPostHeader || 'application/json';
	    var bo = async === false ? false :true;
	    
	    var con = {
	        url:url,
	        async:bo,
	        method:method,
	        success:function(response,opts){
	            if(Ext.typeOf(callback) == "function"){
	                callback(response.responseText);//回调函数
	            }
	        },
	        failure:function(response,options){
	        	if(Ext.typeOf(callback) == "function"){
	        		var value = "";
	        		if(response.request.timedout){//请求超时
	        			value = "{success:false,ErrorInfo:'请求超时!'}";
	        		}else{
	        			value = "{success:false,ErrorInfo:'系统错误!状态码:" + response.status + "'}";
	        		}
	                callback(value);//回调函数
	            }else{
	            	var value = "";
	            	if(response.request.timedout){//请求超时
	        			value = "请求超时!";
	        		}else{
	        			value = "系统错误!状态码:" + response.status;
	        		}
	            	Shell.util.Msg.showError(value);
	            }
	        }
	    };
	    //POST参数
	    if(params){con.params = params;}
	    //超时
	    if(timeout){con.timeout = timeout;}
	    
	    Ext.Ajax.request(con);
	},
	/**表单方式数据匹配方法-response*/
	responseToForm:function(response){
		var result = this.responseTextToForm(response.responseText);
        response.responseText = Ext.JSON.encode(result);
        return response;
	},
	/**表单方式数据匹配方法-responseText*/
    responseTextToForm:function(responseText){
    	var result = Ext.JSON.decode(responseText),
			infoField = 'ResultDataValue';
		
		var info = result[infoField];
		if(info){
            info = Ext.JSON.decode(info.replace(/[\r\n]+/g,'</br>'));
            var type = Ext.typeOf(info);
            if(type === 'object'){
            	result.values = info;
            	for(var i in result.values){
                	result.values[i] = result.values[i].replace(/<\/br>/g,'\r\n');
                }
            }else{
	        	result.values = {};
	        }
            
            result[infoField] = null;
        }else{
        	result.values = {};
        }
        
        return result;
    },
	/**列表格式数据匹配方法-response*/
    responseToList:function(response){
    	var result = this.responseTextToList(response.responseText);
        response.responseText = Ext.JSON.encode(result);
        return response;
    },
    /**列表格式数据匹配方法-responseText*/
    responseTextToList:function(responseText){
    	var result = Ext.JSON.decode(responseText),
    		infoField = 'ResultDataValue';
        
        var info = result[infoField];
        if(info){
        	info = info.replace(/[\r\n]+/g,'');
            info = Ext.JSON.decode(info);
            
            var type = Ext.typeOf(info);
            if(type === 'object'){
            	result.count = info.count;
            	result.list = info.list;
            }else if(type === 'array'){
            	result.count = info.length;
            	result.list = info;
            }else{
	        	result.count = 0;
	        	result.list = [];
	        }
            result[infoField] = null;
        }else{
        	result.count = 0;
        	result.list = [];
        }
        
        return result;
    },
    /**树型格式数据匹配方法-response*/
    responseToTree:function(response,defaultRootProperty){
    	var result = this.responseTextToTree(response.responseText,defaultRootProperty);
        response.responseText = Ext.JSON.encode(result);
        return response;
    },
    /**树型格式数据匹配方法-responseText*/
    responseTextToTree:function(responseText,defaultRootProperty){
    	var result = Ext.JSON.decode(responseText),
    		infoField = 'ResultDataValue',
    		defaultRootProperty = defaultRootProperty || 'Tree';
        
        var info = result[infoField];
        if(info){
            info = Ext.JSON.decode(info);
            var type = Ext.typeOf(info);
            
            if(type === 'object'){
            	result[defaultRootProperty] = info[defaultRootProperty];
            }else if(type === 'array'){
            	result[defaultRootProperty] = info;
            }else{
	        	result[defaultRootProperty] = [];
	        }
            result[infoField] = null;
        }else{
        	result[defaultRootProperty] = [];
        }
        
        return result;
    }
});