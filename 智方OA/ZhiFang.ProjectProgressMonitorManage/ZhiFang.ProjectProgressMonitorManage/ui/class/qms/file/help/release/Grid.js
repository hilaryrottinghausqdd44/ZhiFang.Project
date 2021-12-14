/**
 * 帮助系统发布列表
 * @author longfc
 * @version 2016-11-22
 */
Ext.define('Shell.class.qms.file.help.release.Grid', {
	extend: 'Shell.class.qms.file.help.basic.Grid',
	requires: [
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],

	checkOne: true,
	hasReset: true,
	hasAdd: true,
	hasShow: true,
	title: '帮助信息发布',
	width: 1200,
	height: 800,
	FTYPE: '5',
	/**是否禁用按钮是否显示*/
	HiddenButtonLock: false,
	HiddenDisagreeOfGrid: false,
	DisagreeOfGridText: "撤消禁用",
	hasSave: false,
	/**是否有更新树节点列*/
	hasEditBDictTreeColumn: true,
	/*当前列表是否应用在帮助系统管理维护中**/
	isManageApp: true,
	/**文档状态值*/
	fFileStatus: 5,
	/**列表的默认查询条件--是否只查询当前登录者的数据*/
	isSearchUSERID: false,
	defaultStatusValue: "",
	/**修改服务地址*/
	editUrl: '/QMSService.svc/QMS_UDTO_UpdateFFileByField',
	/**帮助系统生成html及Json文件服务地址*/
	saveJsonUrl: '/QMSService.svc/QMS_UDTO_SaveHelpHtmlAndJson',
	/**overwrite*/
	createdefaultWhere: function() {
		var me = this;
		me.defaultWhere = "";
	},
	/**生成帮助文档*/
	createSaveHelpHtmlAndJsonButton: function(items) {
		var me = this;
		items.push({
			xtype: 'button',
			text: '生成文档',
			iconCls: 'button-save',
			tooltip: '<b>生成帮助文档</b>',
			handler: function() {
				var records = me.getSelectionModel().getSelection();
				if(records && records.length < 1) {
					JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
					return;
				}
				me.showMask("生成帮助文档开始"); //显示遮罩层
				me.saveErrorCount = 0;
				me.saveCount = 0;
				me.saveLength = records.length;

				for(var i = 0; i < records.length; i++) {
					var record = records[i];
					me.saveJson(i, record);
				}
			}
		});
		return items;
	},
	initComponent: function() {
		var me = this;
		me.multiSelect = true;
		me.selType = 'checkboxmodel';
		//me.addEvents('onSaveHelpHtmlAndJsonClick', me);
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1
		});
		me.callParent(arguments);
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	saveJson: function(index, rec) {
		var me = this;
		var id = rec.get(me.PKField);
		var url = JShell.System.Path.getRootUrl(me.saveJsonUrl) + "?id=" + id;
		setTimeout(function() {
			JShell.Server.get(url, function(data) {
				var success = data.success;
				if(data.success) {
					me.saveCount++;
					//JShell.Msg.alert("生成帮助文档成功!", null, 1000);
				} else {
					me.saveErrorCount++;
					//JShell.Msg.alert("生成帮助文档失败!", null, 1000);
				}
				if(me.saveCount + me.saveErrorCount == me.saveLength) {
					me.hideMask(); //隐藏遮罩层
					JShell.Msg.alert("生成帮助文档完成!", null, 1000);
				}
			}, false);
		}, 200 * index);
	},
	onSaveHelpHtmlAndJsonClick: function() {
		var me = this;

	},
	onSaveClick: function() {
		var me = this,
			records = me.store.getModifiedRecords(), //获取修改过的行记录
			len = records.length;

		if(len == 0) return;

		me.showMask(me.saveText); //显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;

		for(var i = 0; i < len; i++) {
			var record = records[i];
			me.updateOne(i, record);
		}
	},
	/*更新模块的子序号**/
	updateOne: function(index, rec) {
		var me = this;
		var id = rec.get(me.PKField);
		var Summary = rec.get('FFile_Summary');
		var url = JShell.System.Path.getUrl(me.editUrl);
		var entity = {
			entity: {
				Id: id,
				Summary: Summary
			},
			fields: 'Id,Summary'
		};
		var params = Ext.JSON.encode(entity);
		setTimeout(function() {
			JShell.Server.post(url, params, function(data) {
				var record = me.store.findRecord(me.PKField, id);
				if(data.success) {
					if(record) {
						record.set(me.DelField, true);
						record.commit();
					}
					me.saveCount++;
				} else {
					me.saveErrorCount++;
					if(record) {
						record.set(me.DelField, false);
						record.commit();
					}
				}
				if(me.saveCount + me.saveErrorCount == me.saveLength) {
					me.hideMask(); //隐藏遮罩层
					if(me.saveErrorCount == 0) me.onSearch();
				}
			});
		}, 100 * index);
	}
});