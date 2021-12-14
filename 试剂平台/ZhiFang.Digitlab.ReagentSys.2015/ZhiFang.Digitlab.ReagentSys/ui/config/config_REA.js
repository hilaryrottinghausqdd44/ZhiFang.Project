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
JcallShell.System.Name = 'REA系统';
/**系统编号*/
JcallShell.System.CODE = 'REA';
/**系统登录顶部图片*/
JcallShell.System.LoginTopImage = '/images/rea/LoginTop.png';
/**第三方库ADO项目名称*/
JcallShell.System.ADOName = 'rea2015ado';

/**系统登录后处理*/
JcallShell.System.onAfterLogin = function(callback){
	JcallShell.REA.System.init(callback);
};

JcallShell.REA = JcallShell.REA || {};

JcallShell.REA.Goods = {
	EXCEL:'试剂信息模版V3.2.xls'
};
JcallShell.REA.System  = {
	/**中心机构ID*/
	CENORG_ID:null,
	/**中心机构编码*/
	CENORG_CODE:null,
	/**中心机构名称*/
	CENORG_NAME:null,
	/**初始化信息*/ 
	init:function(callback){
		var me = this;
		var DEPTCODE = JcallShell.System.Cookie.get(JcallShell.System.Cookie.map.DEPTCODE);
		
		if(!DEPTCODE){
			var msg = "【DEPTCODE机构编号为空】";
			//LOG输出
			JcallShell.Msg.log(JcallShell.Date.toString(new Date(),false,true) + msg);
			return;
		}
		
		var url = JcallShell.System.Path.ROOT + "/ReagentSysService.svc/ST_UDTO_SearchCenOrgByHQL";
	    url += "?isPlanish=true&fields=CenOrg_Id,CenOrg_OrgNo,CenOrg_CName";
	    url += "&where=cenorg.OrgNo='" + DEPTCODE + "'";
	    
	    JcallShell.Server.get(url,function(data){
	    	var msg = "";
	    	if(data.success){
	    		if(data.value && data.value.count == 1){
	    			var obj = data.value.list[0];
	    			me.CENORG_ID = obj.CenOrg_Id;
	    			me.CENORG_CODE = obj.CenOrg_OrgNo;
	    			me.CENORG_NAME = obj.CenOrg_CName;
	    			msg = "【获取到中心机构信息成功】";
	    		}else{
	    			msg = "【获取到中心机构信息失败】";
	    			//JcallShell.Msg.error("没有获取到中心机构信息");
	    		}
	    	}else{
	    		msg = "【获取到中心机构信息失败】";
	    		//JcallShell.Msg.error(data.msg);
	    	}
	    	//LOG输出
			JcallShell.Msg.log(JcallShell.Date.toString(new Date(),false,true) + msg);
			if(Ext.typeOf(callback) == 'function'){
				var bo = msg ? false : true;
				callback(bo);
			}
	    });
	}
};
/**枚举*/
JcallShell.REA.Enum = {
	/**结构类型*/
	CenOrgType:{
		'E1': '厂商',
		'E2': '供应商',
		'E3': '实验室 '
	},
	/**供货总单_紧急标志*/
	BmsCenSaleDoc_UrgentFlag: {
		'E0': '正常',
		'E1': '紧急'
	},
	/**供货总单_单据状态*/
	BmsCenSaleDoc_Status: {
		'E0': '临时',
		'E2': '已审核',
		'E1': '已验收'
	},
	/**供货总单_数据上传标志*/
	BmsCenSaleDoc_IOFlag: {
		'E0': '未提取',
		'E1': '已提取',
		'E2': '部分提取'
	},
	/**供货总单_来源*/
	BmsCenSaleDoc_Source:{
		'E1':'平台(供应商)',
		'E2':'平台(实验室)',
		'E3':'PC(供应商)',
		'E4':'PC(实验室)',
		'E5':'手持(供应商)',
		'E6':'手持(实验室)'
	},
	
	/**订货总单_紧急标志*/
	BmsCenOrderDoc_UrgentFlag: {
		'E0': {value:'正常',color:'#fff',bcolor:'#5cb85c'},
		'E1': {value:'紧急',color:'#fff',bcolor:'#d9534f'}
	},
	/**订货总单_单据状态*/
	BmsCenOrderDoc_Status: {
		'E0': {value:'临时',color:'#fff',bcolor:'#ccc'},
		'E1': {value:'已提交',color:'#fff',bcolor:'#5cb85c'},
		'E2': {value:'已确认',color:'#fff',bcolor:'#5bc0de'},
		'E3': {value:'已出货',color:'#fff',bcolor:'#f0ad4e'},
		'E4': {value:'已验收',color:'#fff',bcolor:'#777'},
		'E999': {value:'已删除',color:'#fff',bcolor:'red'}
	},
	
	/**出入库_使用标志*/
	UseFlag:{
		'E1':'仪器使用',
		'E2':'报损',
		'E3':'回退供应商',
		'E4':'调账',
		'E5':'入库'
	},
	/**颜色*/
	Color: {
		'E0': '#FFCC00',
		'E1': '#FF99CC',
		'E2': '#99CC33',
		'E3': '#CC0033',
		'E4': '#663366',
		'E5': '#999966',
		'E6': '#663300',
		'E7': '#6699CC'
	},
	/**
	 * @param {String} name 枚举类型名称
	 * @param {Boolean} hasAll 是否带'全部'选项
	 * @param {Boolean} hasColor 是否带颜色属性
	 * @param {Boolean} hasNull 是否带'无'选项
	 */
	getList: function(name, hasAll, hasColor,hasNull) {
		var me = this;
		var obj = me[name];
		var list = [];

		if (!obj) return [];

		if (hasAll) {
			list.push([0, JShell.All.ALL, 'font-weight:bold;color:black;']);
		}

		for (var i in obj) {
			if(!hasNull){
				if(obj[i] == '无') continue;
			}
			var li = [i.slice(1), obj[i]];
			if (hasColor) {
				li.push('font-weight:bold;color:' + me.Color[i] + ';');
			}
			list.push(li);
		}

		return list;
	},
	/**
	 * @param {String} name 枚举类型名称
	 * @param {Boolean} hasAll 是否带'全部'选项
	 * @param {Boolean} hasColor 是否带颜色属性
	 * @param {Boolean} hasNull 是否带'无'选项
	 */
	getComboboxList: function(name, hasAll, hasColor,hasNull) {
		var me = this;
		var obj = me[name];
		var list = [];

		if (!obj) return [];

		if (hasAll) {
			list.push([0, JShell.All.ALL, 'font-weight:bold;color:black;']);
		}

		for (var i in obj) {
			if(!hasNull){
				if(obj[i] == '无') continue;
			}
			var li = [i.slice(1), obj[i].value];
			if (hasColor) {
				li.push('font-weight:bold;color:' + obj[i].bcolor + ';');
			}
			list.push(li);
		}

		return list;
	}
};

(function() {
	window.JShell = JcallShell;
	//语言包处理，默认加载中文语言包
	var params = JShell.Page.getParams(true);
	if(params.LANG){
		JcallShell.System.Lang = params.LANG;
	}
	//加载语言
	JcallShell.Page.changeLangage(JcallShell.System.Lang);
})();