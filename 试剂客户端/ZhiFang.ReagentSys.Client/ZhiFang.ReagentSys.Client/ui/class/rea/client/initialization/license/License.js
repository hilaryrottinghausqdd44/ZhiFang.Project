Ext.ns('Shell.class.rea.client.initialization');
Ext.define('Shell.class.rea.client.initialization.license.License', {
	extend: 'Ext.panel.Panel',
	layout: 'border',
	title: '机构授权初始化',
	//获取机构服务
	selectUrl: "/ReaManageService.svc/ST_UDTO_GetCenOrgInitializeInfo",
	/**机构名称*/
	OrgTitle: '',
	/**机构信息*/
	OrgObject: {},
	User: null,
	hasLoadMask:true,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//打开加载获取机构信息
		me.getOrganizationInfo(function(data) {
			if(data && data.value) {
				if(!data.value) return;
				me.OrgObject = data.value;
				me.loadData(me.OrgObject);
			}
		});

		me.UploadPanel.on({
			onUploadClick: function(obj) {
				me.hideMask();
				var objstr=Ext.JSON.decode(obj);
				if(objstr) {
					me.OrgObject = objstr;
					me.OrgTitle = objstr.CName;
				}
				me.loadData(objstr);
			},
			addClick:function(){
				me.showMask('正在进行机构注册授权初始化处理...请稍等！');
				me.UploadPanel.onUploadAuthorization();
			}
		});
		me.AddPanel.on({
			saveClick: function(cenorg) {
				me.loadData(cenorg);
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;

		me.UploadPanel = Ext.create('Shell.class.rea.client.initialization.license.UploadPanel', {
			header: false,
			itemId: 'UploadPanel',
			region: 'north',
			width: 60,
			collapsible: false,
			collapsed: false,
			animCollapse: false,
			animate: false
		});
		me.AddPanel = Ext.create('Shell.class.rea.client.initialization.license.AddPanel', {
			header: true,
			itemId: 'AddPanel',
			region: 'west',
			width: 380,
			title: '机构初始化向导',
			activeItem: 0,
			layout: 'card'

		});
		me.MemoPanel = Ext.create('Shell.class.rea.client.initialization.license.MemoPanel', {
			title: '机构初始化说明',
			region: 'center',
			minHeight: 200,
			collapsible: false
		});
		me.ProgressPanel = Ext.create('Shell.class.rea.client.initialization.license.ScheduleInfo', {
			region: 'south',
			title: '当前机构授权进度',
			html: '',
			height: 120,
			collapsible: false
		});
		me.items = [
			me.MemoPanel,
			me.UploadPanel,
			me.AddPanel,
			me.ProgressPanel
		];
	},
	/**获取机构信息*/
	getOrganizationInfo: function(callback) {
		var me = this;
		var url = JShell.System.Path.ROOT + me.selectUrl;
		JShell.Server.get(url, function(data) {
			if(data.success) {
				callback(data);
			} else {
				JShell.Action.delay(function(){
					JShell.Msg.error(data.msg);
				},null,500);
			}
		}, false);
	},
	/**没有机构时,禁用*/
	disableControl: function() {
		var me = this;
		me.AddPanel.disable();
	},
	enableControl: function() {
		var me = this;
		me.MemoPanel.enable();
		me.AddPanel.enable();
		me.ProgressPanel.enable();
	},
	changeTitle: function() {
		var me = this;
		var title = '机构授权初始化';
		if(me.OrgTitle) title = me.OrgTitle + '授权初始化';
		me.setTitle(title);
		if(!me.OrgTitle) me.disableControl();
		if(me.OrgTitle) me.enableControl();
	},
	loadData: function(cenorg) {
		var me = this;
		if(cenorg && cenorg.Account) {
			me.User = {
				"Account": cenorg.Account,
				"PWD": cenorg.PWD
			}
			me.MemoPanel.User = me.User;
		}
		me.OrgTitle = me.OrgObject.CName;
		me.changeTitle();
		me.MemoPanel.loadData(me.OrgObject);
		me.ProgressPanel.loadData(me.OrgObject);
		me.AddPanel.loadData(me.OrgObject);
	},
	showMask:function(text){
		var me = this;
		if(me.hasLoadMask){me.body.mask(text);}//显示遮罩层
	},
	/**隐藏遮罩*/
	hideMask:function(){
		var me = this;
		if(me.hasLoadMask){me.body.unmask();}//隐藏遮罩层
	}
});