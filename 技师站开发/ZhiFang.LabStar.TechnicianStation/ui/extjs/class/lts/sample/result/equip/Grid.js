/**
 * 仪器结果
 * @author gzj
 * @version 2020-03-22
 */
Ext.define('Shell.class.lts.sample.result.equip.Grid', {
	extend: 'Shell.ux.grid.Panel',
	requires: [
		'Shell.ux.form.field.DateArea',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox'
	],
	title: '',
	width: 410,
	height: 800,

	//获取数据服务路径
	selectUrl: '/ServerWCF/LabStarService.svc/LS_UDTO_SearchLisEquipItemByHQL?isPlanish=true',
	//修改服务地址
	editUrl: '/ServerWCF/LabStarService.svc/LS_UDTO_UpdateLisEquipItemByField',
	
	//显示成功信息
	showSuccessInfo: false,
	//消息框消失时间
	hideTimes: 3000,

	//默认加载
	//defaultLoad: true,
	//默认每页数量
	defaultPageSize: 50,
	/**是否启用序号列*/
	hasRownumberer: false,

	//复选框
	multiSelect: true,
	selType: 'checkboxmodel',

	afterRender: function () {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function () {
		var me = this;
		//数据列
		me.columns = me.createGridColumns();

		me.callParent(arguments);
	},
	//创建挂靠功能栏
	createDockedItems: function () {
		var me = this,
			items = me.dockedItems || [];
		items.push(me.createdfToolbar());
		return items;
	},
	//创建快捷栏
	createdfToolbar: function () {
		var me = this;
		var dockedItems = {
			xtype: 'toolbar', dock: 'top', itemId: 'Toolbar1',
			
				items: [{
					xtype: 'button',
					text: '采用复检结果',
					iconCls: 'button-save',
					handler: function () {
						me.useRecheckResult();
					}
				}, {
					xtype: 'button',
					text: '取消复检标记',
						iconCls: 'button-save',
					handler: function () {
						me.UnRecheckMark();
					}
					}, {
						fieldLabel: '项目过滤',
						itemId: 'itemgl',
						width: 160,
						labelWidth: 60,
						labelAlign: 'right',
						xtype: 'uxCheckTrigger',
						className: 'Shell.class.lts.sample.result.equip.CheckGrid',
						listeners: {
							check: function (p, record) {
								me.getComponent('Toolbar1').getComponent('itemgl').setValue(record ? record.get('LisEquipItem_LBItem_CName') : '');
								me.getComponent('Toolbar1').getComponent('itemid').setValue(record ? record.get('LisEquipItem_LBItem_Id') : '');
								p.close();
							}
						}
					}, {
						xtype: 'textfield',
						itemId: 'itemid',
						hidden:true
					},{
						xtype: 'textfield',
						itemId: 'resultNo'
					}, {
						xtype: 'button',
						text: '清除过滤',
						iconCls: 'button-del',
						handler: function () {
							me.clearFiler();
						}
					}, {
						xtype: 'button',
						text: '查询',
						iconCls: 'button-search',
						handler: function () {
							me.btnsearch();
						}
					}]
		};
		return dockedItems;
	},
	//查询
	btnsearch: function () {
		var me = this;
		var tb = me.getComponent("Toolbar1");
		var resultNo = tb.getComponent("resultNo").getValue();
		var itemid = tb.getComponent("itemid").getValue();
		var where = "1=1";
		if (resultNo) {
			where += " and IExamine=" + resultNo;
		}
		if (itemid) {
			where += " and ItemID=" + itemid;
		}
		me.internalWhere = where;
		me.onSearch();
	},
	//清除过滤
	clearFiler: function () {
		var me = this;
		var tb = me.getComponent("Toolbar1");
		var resultNo = tb.getComponent("resultNo").setValue("");
		var itemid = tb.getComponent("itemid").setValue("");
		tb.getComponent("itemgl").setValue("");
	},
	//采用复检结果
	useRecheckResult: function () {
		var me = this;
		var url = JShell.System.Path.ROOT + me.editUrl;
		var records = me.getSelectionModel().getSelection();
		
		for (var i = 0; i < records.length; i++) {
			var param = {
				entity: {
					Id: records[i].get("LisEquipItem_Id"),
					BItemResultFlag: true
				},
				fields:"Id,BItemResultFlag"
			};	
			var index = 0;
			JShell.Server.post(url, Ext.JSON.encode(param), function (data) {
				if (!data.success) {
				//	JShell.Msg.error("失败");
				}
				index++;
				if (index == i) {
					me.onSearch();
				}
			});
		}
	},
	//取消复检标记
	UnRecheckMark: function myfunction() {
		var me = this;
		var url = JShell.System.Path.ROOT + me.editUrl;
		var records = me.getSelectionModel().getSelection();

		for (var i = 0; i < records.length; i++) {
			var param = {
				entity: {
					Id: records[i].get("LisEquipItem_Id"),
					BRedo: false
				},
				fields: "Id,BRedo"
			};
			var index = 0;
			JShell.Server.post(url, Ext.JSON.encode(param), function (data) {
				if (!data.success) {
					//	JShell.Msg.error("失败");
				}
				index++;
				if (index == i) {
					me.onSearch();
				}
			});
		}
	},
	//创建数据列
	createGridColumns: function () {
		var me = this;
		var columns = [{
			text: '采用', dataIndex: 'LisEquipItem_BItemResultFlag', width: 50,
			sortable: false, menuDisabled: true,isBool:true
		}, {
				text: '对应项目', dataIndex: 'LisEquipItem_LBItem_CName', width: 80,
		}, {
				text: '检验结果', dataIndex: 'LisEquipItem_EReportValue', width: 80, defaultRenderer: true
		}, {
				text: '检测序号', dataIndex: 'LisEquipItem_IExamine', width: 60, defaultRenderer: true
			}, {
				text: '检测序号', dataIndex: 'LisEquipItem_Id', width: 60, hidden: true
			}];

		return columns;
	}
});