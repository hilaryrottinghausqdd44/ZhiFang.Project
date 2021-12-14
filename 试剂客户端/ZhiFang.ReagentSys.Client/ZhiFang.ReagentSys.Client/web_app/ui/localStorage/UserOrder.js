//依托于JcallShell.LocalStorage,JcallShell.JSON，
//需要先加载util/JcallShell文件

/**统一放置于JcallShell中*/
var JcallShell = JcallShell || {};

/**本地数据存储_订单信息*/
JcallShell.LocalStorage.UserOrder = {
	map: {
		'USER': 'USERORDER_'//当前用户
	},
	/**添加当前用户订单信息*/
	addDocInfo:function(value){
		if(!value) return;
    	
    	var userId = JcallShell.Cookie.get(JcallShell.Cookie.map.USERID),
    		user = JcallShell.LocalStorage.get(this.map.USER + userId),
    		user = JcallShell.JSON.decode(user) || {},
			info = user.info || {};
		
		user.info = value;
		user = JcallShell.JSON.encode(user);
		JcallShell.LocalStorage.set(this.map.USER + userId, user);
	},
	/**删除当前用户订单信息*/
    removeDocInfo:function(){
		var userId = JcallShell.Cookie.get(JcallShell.Cookie.map.USERID),
			user = JcallShell.LocalStorage.get(this.map.USER + userId),
    		user = JcallShell.JSON.decode(user) || {},
			info = user.info || {};
		
		user.info = {};
		user = JcallShell.JSON.encode(user);
		JcallShell.LocalStorage.set(this.map.USER + userId, user);
    },
    /**获取当前用户订单信息*/
    getDocInfo:function(){
    	var userId = JcallShell.Cookie.get(JcallShell.Cookie.map.USERID),
    		user = JcallShell.LocalStorage.get(this.map.USER + userId),
    		user = JcallShell.JSON.decode(user) || {},
			info = user.info || {};
		
		return info;
    },
    /**添加当前用户货品*/
    addGoods:function(value){
    	if(!value) return;
    	
    	var userId = JcallShell.Cookie.get(JcallShell.Cookie.map.USERID),
    		user = JcallShell.LocalStorage.get(this.map.USER + userId),
    		user = JcallShell.JSON.decode(user) || {},
			list = user.list || [],
			len = list.length;
		
		var hasValue = false;
		for(var i=0;i<len;i++){
			if(list[i].Id == value.Id){
				hasValue = true;
				break;
			}
		}
		
		//如果数据已存在，就返回true
		if(hasValue) return true;
		
		list.push(value);
		user.list = list;
		user = JcallShell.JSON.encode(user);
		JcallShell.LocalStorage.set(this.map.USER + userId, user);
    },
    /**删除当前用户货品*/
    removeGoods:function(id){
		var userId = JcallShell.Cookie.get(JcallShell.Cookie.map.USERID),
			user = JcallShell.LocalStorage.get(this.map.USER + userId),
    		user = JcallShell.JSON.decode(user) || {},
			list = user.list || [],
			len = list.length;

		for(var i = 0; i < len; i++) {
			if(list[i].Id == id) {
				list.splice(i, 1);
				break;
			}
		}
		user.list = list;
		user = JcallShell.JSON.encode(user);
		JcallShell.LocalStorage.set(this.map.USER + userId, user);
    },
    /**获取当前用户货品列表*/
    getGoodsList:function(){
    	var userId = JcallShell.Cookie.get(JcallShell.Cookie.map.USERID),
    		user = JcallShell.LocalStorage.get(this.map.USER + userId),
    		user = JcallShell.JSON.decode(user) || {},
			list = user.list || [];
			
		return list;
    },
    /**清空当前用户货品信息*/
    removeAll:function(){
    	var userId = JcallShell.Cookie.get(JcallShell.Cookie.map.USERID);
    	JcallShell.LocalStorage.set(this.map.USER + userId,'');
    },
    /**修改一个货品信息*/
    editGoods:function(id,value){
    	var userId = JcallShell.Cookie.get(JcallShell.Cookie.map.USERID),
			user = JcallShell.LocalStorage.get(this.map.USER + userId),
    		user = JcallShell.JSON.decode(user) || {},
			list = user.list || [],
			len = list.length;

		for(var i = 0; i < len; i++) {
			if(list[i].Id == id) {
				list[i] = value;
				break;
			}
		}
		user.list = list;
		user = JcallShell.JSON.encode(user);
		JcallShell.LocalStorage.set(this.map.USER + userId, user);
    }
};