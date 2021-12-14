/**
 * 个人开票
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.balance.PersonGrid', {
	extend: 'Shell.class.pki.balance.ItemBasicGrid',

	title: '个人开票',

	/**修改服务地址*/
	editUrl: '/BaseService.svc/ST_UDTO_UpdateNRequestItemByField',

	/**默认条件*/
	defaultWhere: "(nrequestitem.ItemPriceType='1' or nrequestitem.ItemPriceType='2' or nrequestitem.ItemPriceType='4')",
	/**显示价格类型下拉框*/
	showItemPriceTypeCombobox: true,
	/**包含终端价*/
	hasPersonPrice: true,

	/**默认选中送检时间*/
	isDateRadio: false,

	/**设定价格错误信息*/
	priceErrorInfo: '个人开票终端价格不能低于合同价格，请重新设定',

	/**价格类型列表*/
	ItemPriceTypeList: [
		[0, '全部', 'font-weight:bold;color:black;'],
		['1', '合同价', 'font-weight:bold;color:' + JcallShell.PKI.Enum.Color['E1'] + ';'],
		['2', '阶梯价', 'font-weight:bold;color:' + JcallShell.PKI.Enum.Color['E2'] + ';'],
		['4', '终端价', 'font-weight:bold;color:' + JcallShell.PKI.Enum.Color['E4'] + ';']
	],

	plugins: Ext.create('Ext.grid.plugin.CellEditing', {
		clicksToEdit: 1
	}),

	initComponent: function() {
		var me = this;
		
		//初始化送检时间
		me.initDate();
		//自定义按钮功能栏
		me.buttonToolbarItems = [{
			text: '个人开票',
			iconCls: 'button-text-person',
			tooltip: '<b>批量个人开票</b>',
			handler: function() {
				me.onCheckedFreeClick(false);
			}
		}, '-', {
			text: '取消个人开票',
			iconCls: 'button-text-relieve',
			tooltip: '<b>批量取消个人开票</b>',
			handler: function() {
				me.onCheckedFreeClick(true);
			}
		}, '-', 'save'];
		//创建挂靠功能栏
		me.dockedItems = me.createFilterToolbar();

		me.columns = [{
			dataIndex: 'NRequestItem_NRequestForm_NFClientName',
			text: '送检单位',
			defaultRenderer: true
		}, {
			dataIndex: 'NRequestItem_NRequestForm_NFDeptName',
			text: '科室',
			defaultRenderer: true
		}, {
			dataIndex: 'NRequestItem_NRequestForm_CName',
			text: '姓名',
			width: 60,
			defaultRenderer: true
		}, {
			dataIndex: 'NRequestItem_BarCode',
			text: '条码号',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'NRequestItem_BTestItem_CName',
			text: '项目',
			defaultRenderer: true
		}, {
			dataIndex: 'NRequestItem_BBillingUnit_Name',
			text: '开票方',
			defaultRenderer: true
		}, {
			dataIndex: 'NRequestItem_BillingUnitInfo',
			text: '开票方信息',
			defaultRenderer: true
		}, {
			dataIndex: 'NRequestItem_BDealer_Name',
			text: '经销商',
			defaultRenderer: true
		}, {
			dataIndex: 'NRequestItem_ItemPriceType',
			text: '价格类型',
			width: 60,
			renderer: function(value, meta) {
				var v = JShell.PKI.Enum.ItemPriceType['E' + value] || '';
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = 'background-color:' + JcallShell.PKI.Enum.Color['E' + value] || '#FFFFFF';
				return v;
			}
		}, {
			dataIndex: 'NRequestItem_ItemContPrice',
			text: '合同价格',
			width: 60,
			defaultRenderer: true
		}, {
			dataIndex: 'NRequestItem_ItemPrice',
			text: '<b style="color:blue;">终端价格</b>',
			width: 60,
			editor: {
				xtype: 'numberfield',
				minValue: 0,
				allowBlank: false
			},
			sortable: false,
			type: 'float'
		}, {
			xtype: 'actioncolumn',
			text: '操作',
			align: 'center',
			width: 40,
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					var type = record.get('NRequestItem_ItemPriceType');
					if (type == '4') {
						meta.tdAttr = 'data-qtip="<b>解除个人开票</b>"';
						meta.style = 'background-color:green;';
						return 'button-text-relieve hand';
					} else if (type == '1' || type == '2') {
						meta.tdAttr = 'data-qtip="<b>个人开票</b>"';
						meta.style = 'background-color:red;';
						return 'button-text-person hand';
					}
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var id = rec.get(me.PKField);
					var isOpen = rec.get('NRequestItem_ItemPriceType') == '4' ? true : false;
					me.onFreeClick(id, isOpen);
				}
			}]
		}, {
			dataIndex: 'NRequestItem_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}];

		me.callParent(arguments);
	},
	/**初始化送检时间*/
	initDate: function() {
		var me = this;
		
		var date = JShell.Date.getNextDate(new Date(),JShell.PKI.Balance.Dates);
		
		var year = date.getFullYear();
		var month = date.getMonth() + 1;
		
		me.BeginDate = JShell.Date.getMonthFirstDate(year,month);
		me.EndDate = JShell.Date.getMonthLastDate(year,month);

		me.YearMonth = year;
		me.MonthMonth = month;
		
	},
	/**选中的数据免单/解除免单*/
	onCheckedFreeClick: function(isOpen) {
		var me = this,
			records = me.getSelectionModel().getSelection(),
			len = records.length;
		
		if (len == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		
		var msg = '';
		var ItemPriceType = '';
		if (isOpen) {
			msg = "确定取消个人开票吗";
			ItemPriceType = '1';
		} else {
			msg = "确定个人开票吗";
			ItemPriceType = '4';
		}
		
		if(isOpen){
			JShell.Msg.confirm({
				msg:msg
			}, function(but,text) {
				if (but != "ok") return;
				
				var t = text || '';
				if(!isOpen && !Ext.String.trim(t)){
					JShell.Msg.error('必须填写个人开票信息');
					return;
				}
				
				me.showMask(me.saveText); //显示遮罩层
				me.saveErrorCount = 0;
				me.saveCount = 0;
				me.saveLength = len;
				
				var fields = 'Id,ItemPriceType';
				if(!isOpen) fields += ',BillingUnitInfo';
				
				for (var i in records) {
					var id = records[i].get(me.PKField);
					var entity = {
						Id: id,
						ItemPriceType: ItemPriceType
					};
					if(!isOpen) entity.BillingUnitInfo = text;
					
					me.updateOneByParams(id, {
						entity: entity,
						fields: fields
					});
				}
			});
		}else{
			JShell.Win.open('Shell.class.pki.balance.PersonForm', {
				formtype:'add',
				resizable: false,
				listeners: {
					save: function(win, isAll,name) {
						win.close();
						me.showMask(me.saveText); //显示遮罩层
						me.saveErrorCount = 0;
						me.saveCount = 0;
						me.saveLength = len;
						
						var fields = 'Id,ItemPriceType';
						if(!isOpen) fields += ',BillingUnitInfo';
						
						for (var i in records) {
							var id = records[i].get(me.PKField);
							var text = isAll ? name : records[i].get('NRequestItem_NRequestForm_CName');
							var entity = {
								Id: id,
								ItemPriceType: ItemPriceType
							};
							if(!isOpen) entity.BillingUnitInfo = text;
							
							me.updateOneByParams(id, {
								entity: entity,
								fields: fields
							});
						}
					}
				}
			}).show();
		}
		
		
	},
	/**单个数据免单/解除免单*/
	onFreeClick: function(id, isOpen) {
		var me = this;

		var msg = '';
		var ItemPriceType = '';
		if (isOpen) {
			msg = "确定取消个人开票吗";
			ItemPriceType = '1';
		} else {
			msg = "请填写个人开票信息";
			ItemPriceType = '4';
		}
		
		var record = me.store.findRecord(me.PKField, id);
		var name = record.get('NRequestItem_NRequestForm_CName');
		
		JShell.Msg.confirm({
			msg:msg,
			multiline:!isOpen,
			value:name 
		}, function(but,text) {
			if (but != "ok") return;
			
			if(!isOpen && !Ext.String.trim(text)){
				JShell.Msg.error('必须填写个人开票信息');
				return;
			}
			
			me.showMask(me.saveText); //显示遮罩层
			me.saveErrorCount = 0;
			me.saveCount = 0;
			me.saveLength = 1;
			
			var fields = 'Id,ItemPriceType';
			if(!isOpen) fields += ',BillingUnitInfo';
			
			var entity = {
				Id: id,
				ItemPriceType: ItemPriceType
			};
			if(!isOpen) entity.BillingUnitInfo = text;
			
			me.updateOneByParams(id, {
				entity: entity,
				fields: fields
			});
		});
	},
	/**保存数据*/
	onSaveClick: function() {
		var me = this,
			records = me.store.getModifiedRecords(), //获取修改过的行记录
			len = records.length;

		if (len == 0) return;

		var bError = false;
		for (var i = 0; i < len; i++) {
			var rec = records[i];
			//价格类型
			var type = rec.get('NRequestItem_ItemPriceType');
			//合同价
			var ItemContPrice = rec.get('NRequestItem_ItemContPrice');
			//设定价
			var ItemPrice = rec.get('NRequestItem_ItemPrice');

			if (type == '4' && ItemContPrice > ItemPrice) {
				bError = true;
				break;
			}
		}

		if (bError) {
			JShell.Msg.error(me.priceErrorInfo);
			return;
		}

		me.showMask(me.saveText); //显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;

		for (var i = 0; i < len; i++) {
			var rec = records[i];
			var id = rec.get(me.PKField);
			var price = rec.get('NRequestItem_ItemPrice');
			me.updateOneByParams(id, {
				entity: {
					Id: id,
					ItemPrice: price
				},
				fields: 'Id,ItemPrice'
			});
		}
	},
	/**修改价格*/
	updateOneByParams: function(id, params) {
		var me = this;
		var url = (me.editUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;

		var params = Ext.JSON.encode(params);

		JShell.Server.post(url, params, function(data) {
			var record = me.store.findRecord(me.PKField, id);
			if (data.success) {
				if (record) {
					record.set(me.DelField, true);
					record.commit();
				}
				me.saveCount++;
			} else {
				me.saveErrorCount++;
				if (record) {
					record.set(me.DelField, false);
					record.commit();
				}
			}
			if (me.saveCount + me.saveErrorCount == me.saveLength) {
				me.hideMask(); //隐藏遮罩层
				if (me.saveErrorCount == 0) me.onSearch();
			}
		}, false);
	}
});