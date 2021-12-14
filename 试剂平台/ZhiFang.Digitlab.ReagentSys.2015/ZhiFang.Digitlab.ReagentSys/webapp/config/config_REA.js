var Shell = Shell || {};
Shell.Rea = Shell.Rea || {};
/**系统管理员账号*/
Shell.Rea.ADMINNAME = 'admin';
Shell.Rea.LocalStorage = {
	map:{
		'CENORGID':'R000001',//中心机构ID
		'CENORGCODE':'R000002',//中心机构编码
		'CENORGNAME':'R000003',//中心机构名称
		'CENORGTYPE_SHORTCODE':'R000004'//中心机构类型编码
	}
};
/**订单信息*/
Shell.Rea.Order={
	/**确定的状态值*/
	IsWriteExternalSystem:"2"
};
/**枚举*/
Shell.Rea.Enum = {
	/**结构类型*/
	CenOrgType:{
		'E1': '厂商',
		'E2': '供应商',
		'E3': '实验室 '
	},
	/**供货总单_紧急标志*/
	BmsCenSaleDoc_UrgentFlag: {
		'E0': {value:'正常',color:'#fff',bcolor:'#5cb85c'},
		'E1': {value:'紧急',color:'#fff',bcolor:'#d9534f'}
	},
	/**供货总单_单据状态*/
	BmsCenSaleDoc_Status: {
		'E0': {value:'临时',color:'#fff',bcolor:'#ccc'},
		'E1': {value:'已提交',color:'#fff',bcolor:'#5cb85c'},
		'E2': {value:'已确认',color:'#fff',bcolor:'#5bc0de'},
		'E3': {value:'已出货',color:'#fff',bcolor:'#f0ad4e'},
		'E4': {value:'已验收',color:'#fff',bcolor:'#777'},
		'E999': {value:'已删除',color:'#fff',bcolor:'red'}
	},
	/**供货总单_数据上传标志*/
	BmsCenSaleDoc_IOFlag: {
		'E0': {value:'未提取',color:'#fff',bcolor:'#d9534f'},
		'E1': {value:'已提取',color:'#fff',bcolor:'#5cb85c'},
		'E1': {value:'部分提取',color:'#fff',bcolor:'#f0ad4e'}
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
			list.push([0, '全部', 'font-weight:bold;color:black;']);
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
	}
};
/**系统登录后处理*/
Shell.Rea.onAfterLogin = function(callback){
	var DEPTCODE = Shell.util.Cookie.get(Shell.util.Cookie.map.DEPTCODE);
	
	if(!DEPTCODE){
		var msg = "【DEPTCODE机构编号为空】";
		if(window.console){
			console.log('[' + new Date() + ']' + msg);//LOG输出
		}
		callback({success:true});
		return;
	}
	
	var url = Shell.util.Path.ROOT + "/ReagentSysService.svc/ST_UDTO_SearchCenOrgByHQL";
    url += "?isPlanish=true&fields=CenOrg_Id,CenOrg_OrgNo,CenOrg_CName,CenOrg_CenOrgType_ShortCode";
    url += "&where=cenorg.OrgNo='" + DEPTCODE + "'";
    
    ShellComponent.mask.loading();
    Shell.util.Server.ajax({
		async:false,
		url: url
	}, function(data){
		ShellComponent.mask.hide();
		var result = {success:false};
		if(data.success){
			if(data.value && data.value.count == 1){
    			var obj = data.value.list[0];
    			//localStorage存储数据
				Shell.util.LocalStorage.set(Shell.Rea.LocalStorage.map.CENORGID,obj.CenOrg_Id);
    			Shell.util.LocalStorage.set(Shell.Rea.LocalStorage.map.CENORGCODE,obj.CenOrg_OrgNo);
    			Shell.util.LocalStorage.set(Shell.Rea.LocalStorage.map.CENORGNAME,obj.CenOrg_CName);
    			Shell.util.LocalStorage.set(Shell.Rea.LocalStorage.map.CENORGTYPE_SHORTCODE,obj.CenOrg_CenOrgType_ShortCode);
    			result.success = true;
    			result.msg = '获取到中心机构信息成功';
    		}else{
    			result.msg = '获取到中心机构信息失败';
    		}
		}else{
			result.msg = '获取到中心机构信息失败';
		}
		if(window.console){
			console.log('[' + new Date() + ']' + result.msg);//LOG输出
		}
		callback(result);
	});
};
(function() {
	window.JShell = Shell;
})();