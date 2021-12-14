//依托于JcallShell.LocalStorage,JcallShell.JSON，
//需要先加载util/JcallShell文件
//当前用户信息
//ACCOUNT:{
	//UserName:'员工姓名',
	//AccountName:'员工账户名',
	//DeptName:"部门名称"
//}

/**统一放置于JcallShell中*/
var JcallShell = JcallShell || {};

/**本地数据存储_用户信息*/
JcallShell.LocalStorage.User = {
	COUNT: 3, //最大存储数量
	map: {
		'ACCOUNTLIST': 'USER_001',//用户列表
		'ACCOUNT': 'USER_002'//当前用户
	},
	/**设置当前用户信息*/
	setAccount:function(value){
		if(!value) return;
		var user = JcallShell.JSON.encode(value);
		JcallShell.LocalStorage.set(this.map.ACCOUNT,user);
	},
	/**
	 * 获取当前用户信息
	 * @param {Object} isDecode 是否解码
	 */
	getAccount:function(isDecode){
		return JcallShell.LocalStorage.get(this.map.ACCOUNT,isDecode);
	},
	/**
	 * 历史用户追加
	 * @param {Object} account
	 * @param {Object} password
	 */
	setAccountList:function(account,password){
		//历史用户追加
		var list = JcallShell.LocalStorage.get(this.map.ACCOUNTLIST,true) || [],
			len = list.length;
			
		for(var i=0;i<len;i++){
			if(list[i].split(',')[0] == account){
				list.splice(i,1);
				break;
			}
		}
		list.unshift(account + ',' + password);
		list = JcallShell.JSON.encode(list.slice(0,this.COUNT));
		
		JcallShell.LocalStorage.set(this.map.ACCOUNTLIST,list);
	},
	/**
	 * 获取历史用户列表
	 */
	getAccountList:function(){
		var list = JcallShell.LocalStorage.get(this.map.ACCOUNTLIST,true) || [];
		return list;
	}
};