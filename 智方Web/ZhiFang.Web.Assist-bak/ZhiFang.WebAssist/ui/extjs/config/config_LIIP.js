/**
 * PKI参数设置
 * @author Jcall
 * @version 2015-09-10
 */

var JcallShell = JcallShell || {};

JcallShell.System = JcallShell.System || {};
/**系统语言*/
JcallShell.System.Lang = 'CN';
/**系统信息*/
JcallShell.System.Name = 'LIIP_实验室信息集成平台';
/**系统编号*/
JcallShell.System.CODE = 'LIIP';
/**系统登录顶部图片*/
JcallShell.System.LoginTopImage = '/images/login/LoginTop.png';
/**第三方库ADO项目名称*/
JcallShell.System.ADOName = 'rea2015ado';

JcallShell.LIIP = JcallShell.LIIP || {};
JcallShell.LIIP.ClassDict = {
	//命名空间域名
	_classNameSpace:'ZhiFang.Entity.LIIP',
	/** @public
	 * 初始化字典信息，支持单个字典，也支持多个字典
	 * @param {Object} className 类名
	 * @param {Object} callback 回调函数
	 * @example
	 * 	JcallShell.System.ClassDict.init(
	 * 		'PContractStatus',
	 * 		function(){
	 * 			//回调函数处理
	 * 		}
	 * 	);
	 * 	JcallShell.System.ClassDict.init(
	 * 		['PContractStatus','PTaskStatus'],
	 * 		function(){
	 * 			//回调函数处理
	 * 		});
	 */
	init:function(className,callback){
		var me = JcallShell.System.ClassDict;
		var type = Ext.typeOf(className);
		
		if(type == 'string'){
			className = [className];
		}
		
		var hasData = true;
			
		for(var i in className){
			className[i] = {classnamespace:this._classNameSpace,classname:className[i]};
			if(!me[className[i].classname]){
				hasData = false;
			}
		}
		
		if(hasData){
			if(Ext.typeOf(callback) == 'function'){
				callback();
			}
		}else{
			JcallShell.System.ClassDict.loadClassInfoList(className,callback);
		}
	},
	/** @public
	 * 根据字典内容ID获取字典内容
	 * @param {Object} className
	 * @param {Object} id
	 */
	getClassInfoById:function(className,id){
		return JcallShell.System.ClassDict.getClassInfoById(className,id);
	},
	/** @public
	 * 根据字典内容Name获取字典内容
	 * @param {Object} className
	 * @param {Object} name
	 */
	getClassInfoByName:function(className,name){
		return JcallShell.System.ClassDict.getClassInfoByName(className,name);
	}
};

(function() {
	window.JShell = JcallShell;
	//语言包处理，默认加载中文语言包
	var params = JShell.Page.getParams(true);
	if(params.LANG) {
		JcallShell.System.Lang = params.LANG;
	}
	//加载语言
	JcallShell.Page.changeLangage(JcallShell.System.Lang);
})();