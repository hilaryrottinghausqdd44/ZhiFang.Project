/**
 * 服务交互类
 * 
 * 【提供的方法】
 * saveInfo 保存数据
 * getInfoById 根据ID获取信息
 * deleteInfoById 根据ID删除信息
 */
Ext.ns('Ext.build');
Ext.define('Ext.build.InteractiveService',{
	
	promptBoxErrorInfoA:'<b style="color:red">',
	promptBoxErrorInfo:'错误信息:',
	promptBoxErrorInfoB:'</b>',
	promptBoxServiceErrorInfo:'<b style="color:red">请求服务失败！</b>',
	promptBoxEmpty:'返回信息内容为空！',
	promptBoxIdEmpty:'<b style="color:red">没有传递id参数！</b>',
	
	/**
     * 将应用对象保存到数据库中
     * @public
     * @param {} url 服务地址
     * @param {} obj 需要保存的对象
     * @param {} callback 回调函数
     */
    saveInfo:function(url,obj,callback){
    	var me = this;
		Ext.Ajax.defaultPostHeader = 'application/x-www-form-urlencoded';
		Ext.Ajax.request({
			async:false,//非异步
			url:url,
			params:obj,
			method:'POST',
			timeout:2000,
			success:function(response,opts){
				var result = Ext.JSON.decode(response.responseText);
				if(result.success){//回调函数
					(Ext.typeOf(callback) == "function") && callback(result);
				}else{
					me.alertErrorInfo();
				}
			},
			failure:function(response,options){ 
				me.alertFailureInfo();
			}
		});
    },
    /**
     * 根据ID获取信息
     * @public
     * @param {} url 服务地址
     * @param {} id 信息ID
     * @param {} callback 回调函数
     */
    getInfoById:function(url,id,callback){
    	this.interactById(url,id,callback);
    },
    /**
     * 根据ID删除信息
     * @public
     * @param {} url 服务地址
     * @param {} id 信息ID
     * @param {} callback 回调函数
     */
    deleteInfoById:function(url,id,callback){
    	this.interactById(url,id,callback);
    },
    /**
     * 根据ID删除信息
     * @private
     * @param {} url 服务地址
     * @param {} id 信息ID
     * @param {} callback 回调函数
     */
    interactById:function(url,id,callback){
    	var me = this;
    	if(id && id != -1){
			Ext.Ajax.defaultPostHeader = 'application/json';
			Ext.Ajax.request({
				async:false,//非异步
				url:url,
				method:'GET',
				timeout:2000,
				success:function(response,opts){
					var result = Ext.JSON.decode(response.responseText);
					if(result.success){
						if(result.ResultDataValue && result.ResultDataValue != ""){
							var appInfo = Ext.JSON.decode(result.ResultDataValue);
							(Ext.typeOf(callback) == "function") && callback(appInfo);//回调函数	
						}else{
							me.alertEmptyInfo();
						}
					}else{
						me.alertErrorInfo();
					}
				},
				failure:function(response,options){ 
					me.alertFailureInfo();
				}
			});
		}else{
			me.alertIdEmptyInfo();
		}
    },
    /**
     * 弹出错误提示框
     * @private
     */
    alertErrorInfo:function(){
    	var me = this;
    	alertError(me.promptBoxErrorInfoA + me.promptBoxErrorInfo + result.ErrorInfo + me.promptBoxErrorInfoB);
    },
    /**
     * 弹出请求服务失败提示框
     * @private
     */
    alertFailureInfo:function(){
    	var me = this;
    	alertInfo(me.promptBoxServiceErrorInfo);
    },
    /**
     * 弹出返回信息为空提示框
     * @private
     */
    alertEmptyInfo:function(){
    	var me = this;
    	alertInfo(me.promptBoxEmpty);
    },
    /**
     * 弹出没有传递id参数提示框
     * @private
     */
    alertIdEmptyInfo:function(){
    	var me = this;
    	Ext.Msg.alert(me.promptBoxIdEmpty);
    }
});