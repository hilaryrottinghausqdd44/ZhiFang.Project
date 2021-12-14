/**
 * 互动交流信息
 * @author longfc
 * @version 2017-03-21
 */
Ext.define('Shell.class.sysbase.scinteraction.basic.InteractionApp', {
	extend: 'Shell.ux.panel.AppPanel',

	/**标题*/
	title: '互动交流信息',
	width: 680,
	height: 620,
	/**获取数据服务路径*/
	selectUrl: '/SystemCommonService.svc/SC_UDTO_SearchSCInteractionByHQL?isPlanish=true',
	/**依某一业务对象ID获取该业务对象ID下的所有交流内容信息*/
	selectAllUrl: '/SystemCommonService.svc/SC_UDTO_SearchAllSCInteractionByBobjectID?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/SystemCommonService.svc/SC_UDTO_AddSCInteractionExtend',
	/**交流对象名*/
	objectName: '',
	/**交流关联对象名*/
	fObejctName: '',
	/**交流关联对象主键*/
	fObjectValue: '',
	/**交流关联对象是否ID,@author Jcall,@version 2016-08-19*/
	fObjectIsID: false,

	/**提交后刷新列表*/
	IsSaveToLoad: true,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);

		me.Form.on({
			save: function(form, id) {
				form.getComponent('Content').setValue('');
				form.TopicTitle = "";
				//新增的是交流内容
				if(form.IsCommunication == false) {					
					me.fireEvent('onAddInteraction', me,form, me.fObjectValue);	
				} else if(form.IsCommunication == true) {
					me.fObjectValue = id;
					me.InteractionList.fObjectValue = id;
					me.fireEvent('onAddTopic', me, form, id);
				}
			}
		});
		me.setBtnInteraction(true);
	},

	initComponent: function() {
		var me = this;
		me.addEvents('onAddTopic', 'onAddInteraction');
		if(me.objectName && me.fObejctName && me.fObjectValue) {
			me.items = me.createItems();
		} else {
			me.html =
				'<div style="margin:20px;text-align:center;color:red;font-weight:bold;">' +
				'请传递objectName、fObejctName、fObjectValue参数！' +
				'</div>';
		}
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.InteractionList = Ext.create('Shell.class.sysbase.scinteraction.basic.InteractionList', {
			region: 'center',
			header: false,
			selectUrl: me.selectUrl,
			selectAllUrl: me.selectAllUrl,
			itemId: 'InteractionList',
			objectName: me.objectName,
			fObejctName: me.fObejctName,
			fObjectValue: me.fObjectValue,
			fObjectIsID: me.fObjectIsID
		});

		var FormConfig = {
			header: false,
			itemId: 'Form',
			//border: false,
			split: true,
			collapsible: false,
			addUrl: me.addUrl,
			objectName: me.objectName,
			fObejctName: me.fObejctName,
			fObjectValue: me.fObjectValue,
			fObjectIsID: me.fObjectIsID,
			region: 'south',
			height: 150
		};
		me.Form = Ext.create('Shell.class.sysbase.scinteraction.basic.Form', FormConfig);
		return [me.InteractionList, me.Form];
	},
	/**话题列表联动清空交流应用相关数据*/
	clearData: function() {
		var me = this;
		me.InteractionList.clearData();
		me.setBtnInteraction(true);
	},
	/**话题列表联动交流应用*/
	loadData: function(isLoadAllData) {
		var me = this;
		me.InteractionList.loadAllData = isLoadAllData;
		me.InteractionList.fObjectValue = me.fObjectValue;
		me.Form.fObjectValue = me.fObjectValue;
		me.InteractionList.load();
		if(isLoadAllData == true) {
			me.setBtnInteraction(true);
		}
	},
	/**获取交流列表的数据*/
	loadInteractionListData: function(isLoadAllData) {
		var me = this;
		me.InteractionList.loadAllData = isLoadAllData;
		me.InteractionList.fObjectValue = me.fObjectValue;
		me.InteractionList.load();
	},
	/**设置交流按钮是否可用*/
	setBtnInteraction: function(disabledValue) {
		var me = this;
		setTimeout(function() {
			var buttonsToolbar = me.Form.getComponent('buttonsToolbar');
			var btnSaveInteraction = buttonsToolbar.getComponent('btnSaveInteraction');
			btnSaveInteraction.setDisabled(disabledValue);
		}, 500);
	},
});