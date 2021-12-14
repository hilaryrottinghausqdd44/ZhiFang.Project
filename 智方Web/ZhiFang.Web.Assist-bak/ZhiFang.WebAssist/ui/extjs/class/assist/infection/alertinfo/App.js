/**
 * @description 待评估及待归档处理
 * @author longfc
 * @version 2020-08-27
 */
Ext.define('Shell.class.assist.infection.alertinfo.App', {
	extend: 'Ext.tab.Panel',
	
	title: '待评估及待归档处理',
	//	header: false,
	border: false,
	bodyPadding: 1,
	//activeTab: 0,
	
	isCommentGridLoad:false,
	isToBeArchivedGridLoad:false,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		me.on({
			/**页签切换事件处理*/
			tabchange: function(tabPanel, newCard, oldCard, eOpts) {
				var me = this;
				switch(newCard.itemId) {
					case 'CommentGrid':
						if(!me.isCommentGridLoad) {
							me.CommentGrid.loadData();
							me.isCommentGridLoad = true;
						}
						break;
					case 'ToBeArchivedGrid':
						if(!me.isToBeArchivedGridLoad) {
							me.ToBeArchivedGrid.loadData();
							me.isToBeArchivedGridLoad = true;
						}
						break;
					default:
		
						break
				}
			}
		});
		
		 me.activeTab = 0;
		//当前激活的页签项
		var comtab = me.getActiveTab(me.items.items[0]);
		comtab.loadData();
	},
	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.CommentGrid = Ext.create('Shell.class.assist.infection.alertinfo.CommentGrid', {
			title: '待评估处理',
			header: true,
			border: true,
			/**默认加载*/
			defaultLoad: true,
			itemId: 'CommentGrid'
		});
		me.ToBeArchivedGrid = Ext.create('Shell.class.assist.infection.alertinfo.ToBeArchivedGrid', {
			title: '待归档处理',
			header: true,
			border: true,
			itemId: 'ToBeArchivedGrid'
		});
		var appInfos = [];
		appInfos.push(me.CommentGrid);
		appInfos.push(me.ToBeArchivedGrid);
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
		if(me.hasLoadMask) {
			me.body.mask(text);
		}
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if(me.hasLoadMask) {
			me.body.unmask();
		}
	}
});