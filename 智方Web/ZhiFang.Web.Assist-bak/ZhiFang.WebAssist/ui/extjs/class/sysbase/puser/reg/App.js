/**
 * 注册帐号,
 * @author longfc
 * @version 2020-12-04
 */
Ext.define('Shell.class.sysbase.puser.reg.App', {
	extend: 'Ext.tab.Panel',

	title: '注册帐号',
	
	width: 240,
	height: 320,
	border: false,
	bodyPadding: 1,
	//activeTab: 0,	
	isDoctorFormLoad: false,
	isNurseFormLoad: false,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);

		me.on({
			/**页签切换事件处理*/
			tabchange: function(tabPanel, newCard, oldCard, eOpts) {
				var me = this;
				switch (newCard.itemId) {
					case 'DoctorForm':

						break;
					case 'NurseForm':

						break;
					default:

						break
				}
			}
		});

		me.activeTab = 0;
		//当前激活的页签项
		var comtab = me.getActiveTab(me.items.items[0]);
		//comtab.loadData();
	},
	initComponent: function() {
		var me = this;
		me.onGetRBACRole(function(){
			me.items = me.createItems();
		});
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.NurseForm = Ext.create('Shell.class.sysbase.puser.reg.Form', {
			title: '护士账号',
			header: true,
			border: true,
			usertypeValue: "护士",
			sySTypeValue: "3", //临床护士预置
			roleData:me.nurseList,
			roleDefaultVal:me.nurseList.length>0?me.nurseList[0][0]:"",
			itemId: 'NurseForm'
		});
		me.DoctorForm = Ext.create('Shell.class.sysbase.puser.reg.Form', {
			title: '医生账号',
			header: true,
			border: true,
			usertypeValue: "医生",
			sySTypeValue: "2", //临床医生预置
			roleData:me.doctorList,
			roleDefaultVal:me.doctorList.length>0?me.doctorList[0][0]:"",
			itemId: 'DoctorForm'
		});
		var appInfos = [];
		appInfos.push(me.NurseForm);
		appInfos.push(me.DoctorForm);
		return appInfos;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = [];
		return null;
	},
	/**显示遮罩*/
	showMask: function(text) {
		var me = this;
		if (me.hasLoadMask) {
			me.body.mask(text);
		}
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if (me.hasLoadMask) {
			me.body.unmask();
		}
	},
	/**
	 * @description 
	 * @param {Object} callback
	 */
	onGetRBACRole: function(callback) {
		var me = this;
		var fields = "RBACRole_Id,RBACRole_CName,RBACRole_SySType,RBACRole_DispOrder";
		var where = "(rbacrole.SySType=2 or rbacrole.SySType=3) and rbacrole.IsUse=1 ";
		var url = JShell.System.Path.ROOT + '/ServerWCF/RBACService.svc/RBAC_UDTO_SearchRBACRoleByHQL?isPlanish=true';
		url += '&where=' + where + "&fields=" + fields;

		JShell.Server.get(url, function(data) {
			if (data.success && data.value) {
				var list = data.value.list;
				if (list.length>0) {
					me.loadRole(list,function(){
						callback();
					});
				}else{
					callback();
				}
			} else {
				JShell.Msg.error('获取系统预置角色信息失败！');
				callback();
			}
		},false);
	},
	/**
	 * @description 
	 * @param {Object} list
	 */
	loadRole: function(list,callback) {
		var me = this;
		me.doctorList = [];
		me.nurseList = [];

		for (var i = 0; i < list.length; i++) {
			var item = list[i];
			var arr = [];
			arr.push(item["RBACRole_Id"]);
			arr.push(item["RBACRole_CName"]);
			var sysType = item["RBACRole_SySType"];
			if (sysType == "2") {
				me.doctorList.push(arr);
			} else if (sysType == "3") {
				me.nurseList.push(arr);
			}
		}
		callback();
	}
});
