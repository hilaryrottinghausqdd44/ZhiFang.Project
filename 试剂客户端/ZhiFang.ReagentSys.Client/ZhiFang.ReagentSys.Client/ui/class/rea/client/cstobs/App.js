/**
 * CS试剂客户端升级BS
 * @author longfc
 * @version 2018-10-17
 */
Ext.define('Shell.class.rea.client.cstobs.App', {
	extend: 'Ext.panel.Panel',
	layout: 'border',
	title: 'CS试剂客户端升级BS',
	//获取机构服务
	selectUrl: "/ReaManageService.svc/RS_UDTO_GetCSUpdateToBSInfo",
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

		me.BtnPanel.on({
			saveClick: function(cenorg) {
				//me.loadData(cenorg);
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

		me.BtnPanel = Ext.create('Shell.class.rea.client.cstobs.BtnPanel', {
			header: true,
			itemId: 'BtnPanel',
			region: 'west',
			width: 380,
			title: 'CS试剂客户端升级BS向导',
			activeItem: 0,
			layout: 'card'

		});
		me.MemoPanel = Ext.create('Shell.class.rea.client.cstobs.MemoPanel', {
			title: 'CS试剂客户端升级BS说明',
			region: 'center',
			minHeight: 200,
			collapsible: false
		});
		me.ProgressPanel = Ext.create('Shell.class.rea.client.cstobs.ScheduleInfo', {
			region: 'south',
			title: 'CS试剂客户端升级BS进度',
			html: '',
			height: 120,
			collapsible: false
		});
		me.items = [
			me.MemoPanel,
			me.BtnPanel,
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
		//me.BtnPanel.disable();
	},
	enableControl: function() {
		var me = this;
		me.MemoPanel.enable();
		//me.BtnPanel.enable();
		me.ProgressPanel.enable();
	},
	changeTitle: function() {
		var me = this;
		var title = 'CS试剂客户端升级BS';
		if(me.OrgTitle) title = me.OrgTitle + '';
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
		me.changeTitle();
		me.MemoPanel.loadData(me.OrgObject);
		me.ProgressPanel.loadData(me.OrgObject);
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