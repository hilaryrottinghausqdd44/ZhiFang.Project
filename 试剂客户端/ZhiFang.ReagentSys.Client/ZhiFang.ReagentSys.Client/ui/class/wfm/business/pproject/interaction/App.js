/**
 * 互动交流信息
 * @author liangyl	
 * @version 2017-03-21
 */
Ext.define('Shell.class.wfm.business.pproject.interaction.App', {
	extend: 'Shell.ux.panel.AppPanel',

	/**标题*/
	title: '工作记录信息',
	width: 680,
	height: 620,
	/**获取数据服务路径*/
	selectUrl: '/SingleTableService.svc/ST_UDTO_SearchPProjectTaskProgressByHQL?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/SingleTableService.svc/ST_UDTO_AddPProjectTaskProgress',
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
	PK:null,
    ProjectTaskID:null,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);

		me.Form.on({
			save: function(form, id) {
				form.getComponent('Content').setValue('');
				me.fObjectValue = id;
				me.InteractionList.fObjectValue = id;
				me.fireEvent('onAddTopic', me, form, id);
				me.InteractionList.onSearch();
			}
		});
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
		me.InteractionList = Ext.create('Shell.class.wfm.business.pproject.interaction.InteractionList', {
			region: 'center',
			header: false,
			selectUrl: me.selectUrl,
			itemId: 'InteractionList',
			objectName: me.objectName,
			fObejctName: me.fObejctName,
			fObjectValue: me.fObjectValue,
			ProjectID:me.ProjectID,
			ProjectTaskID:me.ProjectTaskID,
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
			PK:me.PK,
			ProjectID:me.ProjectID,
		    ProjectTaskID:me.ProjectTaskID,
			region: 'south',
			height: 210
		};
		me.Form = Ext.create('Shell.class.wfm.business.pproject.interaction.Form', FormConfig);
		return [me.InteractionList, me.Form];
	},
	/**话题列表联动清空交流应用相关数据*/
	clearData: function() {
		var me = this;
		me.InteractionList.clearData();
	},
	/**话题列表联动交流应用*/
	loadData: function(isLoadAllData) {
		var me = this;
		me.InteractionList.loadAllData = isLoadAllData;
		me.InteractionList.fObjectValue = me.fObjectValue;
		me.Form.fObjectValue = me.fObjectValue;
		me.InteractionList.load();
	},
	/**获取交流列表的数据*/
	loadInteractionListData: function(isLoadAllData) {
		var me = this;
		me.InteractionList.loadAllData = isLoadAllData;
		me.InteractionList.fObjectValue = me.fObjectValue;
		me.InteractionList.load();
	}

});