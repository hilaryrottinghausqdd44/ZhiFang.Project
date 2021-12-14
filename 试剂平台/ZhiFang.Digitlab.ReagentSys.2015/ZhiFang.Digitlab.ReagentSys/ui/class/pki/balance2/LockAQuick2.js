/**
 * 对账快捷锁定
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.balance2.LockAQuick2', {
	extend: 'Ext.panel.Panel',
	
	title: '对账快捷锁定',
	bodyPadding: 10,
	autoScroll: true,
	/**锁定服务*/
	lockUrl: '/StatService.svc/Stat_UDTO_QuickReconciliationLocking',
	/**显示成功信息*/
	showSuccessInfo: true,
	/**开启加载数据遮罩层*/
	hasLoadMask: true,
	/**消息框消失时间*/
	hideTimes:3000,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		me.getComponent('filterToolbar').on({
			search:function(p,params){
				me.doCheckedLock(p,params);
			}
		});
	},
	initComponent: function() {
		var me = this,
			config = me.searchToolbarConfig || {};
			
		//创建挂靠功能栏
		me.dockedItems = me.dockedItems || [Ext.create('Shell.class.pki.balance2.SearchToolbar',Ext.apply(config,{
			itemId:'filterToolbar',
			dock:'top',
			isLocked: true,
			height:105,
			createButtons:me.createButtons
		}))];
		
		me.callParent(arguments);
	},
	/**创建功能按钮*/
	createButtons:function(){
		var me = this,
			items = [];
		
		items.push({
			x: 700,
			y: 80,
			width: 60,
			iconCls: 'button-cancel',
			margin: '0 0 0 10px',
			xtype: 'button',
			text: '清空',
			tooltip: '<b>清空查询条件</b>',
			handler: function() {
				me.onClearSearch();
			}
		}, {
			x: 770,
			y: 80,
			width: 60,
			iconCls: 'button-lock',
			margin: '0 0 0 10px',
			xtype: 'button',
			text: '锁定',
			tooltip: '<b>对符合条件的项目进行锁定</b>',
			handler: function() {
				me.onFilterSearch();
			}
		});
		
		return items;
	},
	/**锁定选中的数据*/
	doCheckedLock: function(p,params) {
		var me = this;

		JShell.Msg.confirm("确定要锁定吗", function(but) {
			if (but != "ok") return;

			var url = (me.lockUrl.slice(0, 4) == 'http' ? '' :
				JShell.System.Path.ROOT) + me.lockUrl;

			url += "?" + me.getParamStr() + "&isLock=true";

			me.showMask(me.saveText); //显示遮罩层
			JShell.Server.get(url, function(data) {
				me.hideMask(); //隐藏遮罩层
				if (data.success) {
					if (me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, me.hideTimes);
				} else {
					JShell.Msg.error(data.msg);
				}
			});
		});
	}
});