//依托于JcallShell.LocalStorage,JcallShell.JSON，
//需要先加载util/JcallShell文件

/**统一放置于JcallShell中*/
var JcallShell = JcallShell || {};

/**本地数据存储_用户信息*/
JcallShell.LocalStorage.User = {
	COUNT: 3, //最大存储数量
	map: {
		'ACCOUNT': 'USER_003', //用户账号
		'DOCTOR': 'USER_004' //医生账号
	},
	/**设置用户账户*/
	setAccount:function(value){
		JcallShell.LocalStorage.set(this.map.ACCOUNT, value || '');
	},
	/**
	 * 获取用户账号
	 * @param {Object} isDecode 是否解码
	 */
	getAccount:function(isDecode){
		return JcallShell.LocalStorage.get(this.map.ACCOUNT,isDecode);
	},
	/**设置医生账户*/
	setDoctor:function(value){
		JcallShell.LocalStorage.set(this.map.DOCTOR, value || '');
	},
	/**
	 * 获取医生账号
	 * @param {Object} isDecode 是否解码
	 */
	getDoctor:function(isDecode){
		return JcallShell.LocalStorage.get(this.map.DOCTOR,isDecode);
	}
};