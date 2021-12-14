//依托于JcallShell.LocalStorage,JcallShell.JSON，
//需要先加载util/JcallShell文件

/**统一放置于JcallShell中*/
var JcallShell = JcallShell || {};

/**本地数据存储_枚举信息*/
JcallShell.LocalStorage.Enum = {
	/**枚举服务地址*/
	_url:'/ServerWCF/SystemCommonService.svc/GetClassDic',
	
	/**映射*/
	_map: {
        'PREFIX': 'ENUM_'//枚举前缀
    },
	/**获取枚举信息列表*/
    getListByClassName:function(className,callback){
    	if(!callback) return;
    	
    	var me = this,
    		name = me._map.PREFIX + className,
    		data = JcallShell.LocalStorage.get(name,true);
    		
    	if(data && data.version == JcallShell.System.ENUM_VERSION){
    		callback(data.data);
    	}else{
    		me._getEnumData(className,callback);
    	}
    },
    /**获取枚举对象信息*/
    getInfoById:function(className,id,callback){
    	if(!callback) return;
    	
    	var me = this;
    	
    	me.getListByClassName(className,function(data){
    		var result = null;
    		for(var i in data){
    			if(data[i].Id == id){
    				result = data[i];
    				break;
    			}
    		}
    		callback(result); 
    	});
    },
    /**获取枚举数据*/
    _getEnumData:function(className,callback){
    	var me = this,
    		name = me._map.PREFIX + className,
    		url = JcallShell.System.Path.ROOT + me._url + 
    			'?classnamespace=ZhiFang.WeiXin.Entity&classname=' + className;
    		
    	JcallShell.Server.ajax({
    		url:url,
    		showError:true
    	},function(data){
    		if(data.success){
    			JcallShell.LocalStorage.set(name,JcallShell.JSON.encode({
    				version:JcallShell.System.ENUM_VERSION,
    				data:data.value
    			}));
    			callback(data.value);
    		}else{
    			alert(data.msg);
    		}
    	});
    }
};