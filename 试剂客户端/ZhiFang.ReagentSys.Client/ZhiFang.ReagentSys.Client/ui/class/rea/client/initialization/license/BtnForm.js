/**
 * 机构授权
 * @author liangyl	
 * @version 2018-02-27
 */
Ext.define('Shell.class.rea.client.initialization.license.BtnForm', {
	extend: 'Shell.ux.form.Panel',
	title: '机构授权',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	width: 320,
	height: 390,
	/**内容周围距离*/
	bodyPadding: '0px 20px 0px 10px',
	formtype: "add",
	autoScroll: false,
	/**新增服务地址*/
	addUrl: '/ReaManageService.svc/ST_UDTO_AddCenOrgInitializeByStep',
	OrgObject: {},
	/**布局方式*/
	layout: {
		type: 'table',
		columns:1//每行有几列
	},

	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.initFilterListeners();
	},
	initComponent: function() {
		var me = this;
		me.addEvents('changeResult', 'load', 'saveClick');
		me.defaultTitle = me.title;
		me.items = me.items || me.createItems();
		me._thisfields = [];
		me.initPKField(); //初始化主键字段
		//创建挂靠功能栏
		var dockedItems = me.createDockedItems();
		if(dockedItems.length > 0) {
			me.dockedItems = dockedItems;
		}
		me.callParent(arguments);
	},
	createItems: function(html) {
		var me = this;
		return [{
			xtype: 'button',
			itemId: 'addbtn0',
			text: '初始化功能模块',
			margin: '5px 5px',
			width: 100,
			hidden:true,
			handler: function() {
				var str = 'RBACModule';
				me.onSaveClick(str);
			}
		},{
			xtype: 'button',
			itemId: 'addbtn2',
			text: '初始化第一步',
			margin: '5px 5px',
			width: 100,
			handler: function() {
				var str = 'RBACModule|SServiceClient|HRDept';
				me.onSaveClick(str);
			}
		}, {
			xtype: 'button',
			itemId: 'addbtn3',
			text: '初始化第二步',
			margin: '5px 5px',
			width: 100,
			handler: function() {
				var str = 'HREmployee|RBACUser|RBACRole';
				me.onSaveClick(str);
			}
		}, {
			xtype: 'button',
			itemId: 'addbtn4',
			text: '初始化第三步',
			margin: '5px 5px',
			width: 100,
			handler: function() {
				var str = 'RBACEmpRoles|RBACRoleModule';
				me.onSaveClick(str);
			}
		}, {
			xtype: 'button',
			itemId: 'addbtn5',
			text: '初始化第四步',
			margin: '5px 5px',
			width: 100,
			handler: function() {
				var str = 'BParameter|Others';
				me.onSaveClick(str);
			}
		}, {
			xtype: 'displayfield',
			width: me.width-20,
			fieldLabel: '',
			labelWidth: 0,
			//hidden:true,
			value: '' + //<h2>功能按钮说明：</h2>
				//'<b>初始化第一步操作:</b><p>【功能模块信息初始化】处理</p><p>【授权机构信息初始化】处理</p><p>【机构管理部门初始化】处理</p><br/>' +
				//'<b>初始化第二步操作:</b><p>【系统管理员初始化】    处理</p><p>【管理帐号信息初始化】处理</p><p>【管理角色信息初始化】处理</p><br/>' +
				//'<b>初始化第三步操作:</b><p>【员工角色权限初始化】处理</p><p>【角色模块信息初始化】处理</p><p></p><br/>' +
				//'<b>初始化第四步操作:</b><p>【系统运行参数初始化】处理</p><p>【系统条码规则初始化】处理</p><p>【机构字典类型初始化】处理</p><br/>' +
				'<b style="color:#0000FF;">以上步骤完成后,靖查看各步骤操作是否都显示为【初初始化成功】,如有显示【初初始化失败】,请查看操作提示或再重复操作对应步骤按钮</b>'
		}];

	},
	/**创建功能按钮栏*/
	createButtontoolbar: function() {
		var me = this,
			items = me.buttonToolbarItems || [];
		items.push();
	},

	getAddParams: function(str) {
		var me = this;
		var entity = str;
		var obj = {
			cenOrg: me.OrgObject,
			entity: entity
		};
		return obj;
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
	},
	/**更改标题*/
	changeTitle: function() {
		var me = this;
	},
	onSaveClick: function(str) {
		var me = this;
		var url = me.addUrl;
		url = (url.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + url;
		var params = me.getAddParams(str);
		if(!params) return;
		var obj = Ext.JSON.encode(params);
		me.showMask(JShell.Server.SAVE_TEXT); //显示遮罩层
		JShell.Server.post(url, obj, function(data) {
			me.hideMask();
			if(data.success) {
				var cenorg=me.OrgObject;
				if(data.value){
					cenorg=data.value;
				}
				me.fireEvent('saveClick',cenorg);
			} else {
				JShell.Msg.error(data.msg);
			}
		});
	}
});