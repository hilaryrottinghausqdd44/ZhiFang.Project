/**
 * 个人开票
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.balance2.PersonGrid', {
	extend: 'Shell.class.pki.balance2.ItemGrid',

	title: '个人开票',
	
	/**默认条件*/
	defaultWhere: "nrequestitem.IsLocked=1 and (nrequestitem.ItemPriceType='1' or nrequestitem.ItemPriceType='2' or nrequestitem.ItemPriceType='4')",
	/**带功能按钮栏*/
	hasButtontoolbar:true,
	
	/**取消个人开票服务地址*/
	openPersonUrl:'/StatService.svc/Stat_UDTO_CancelIndividualInvoice',
	
	plugins: Ext.create('Ext.grid.plugin.CellEditing', {
		clicksToEdit: 1
	}),
	
	/**查询栏参数设置*/
	searchToolbarConfig:{
		/**对账状态列表*/
		IsLockedList: [
			['1', '对账中', 'font-weight:bold;color:' + JcallShell.PKI.Enum.Color['E1'] + ';']
		],
		defaultIsLockedValue:'1',
		/**价格类型列表*/
		ItemPriceTypeList: [
			[0, '全部', 'font-weight:bold;color:black;'],
			['1', '合同价', 'font-weight:bold;color:' + JcallShell.PKI.Enum.Color['E1'] + ';'],
			['2', '阶梯价', 'font-weight:bold;color:' + JcallShell.PKI.Enum.Color['E2'] + ';'],
			['4', '终端价', 'font-weight:bold;color:' + JcallShell.PKI.Enum.Color['E4'] + ';']
		]
	},
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		me.on({
			beforeedit:function(editor,e){
				var isFree = e.record.get('NRequestItem_ItemPriceType') == '4';
				if(!isFree) return false;
			}
		});
	},
	
	initComponent:function(){
		var me = this;
		
		//自定义按钮功能栏
		me.buttonToolbarItems = [{
			text: '个人开票',
			iconCls: 'button-text-person',
			tooltip: '<b>批量个人开票</b>',
			handler: function() {
				me.onAllPersonClick();
			}
		}, '-', {
			text: '取消个人开票',
			iconCls: 'button-text-relieve',
			tooltip: '<b>批量取消个人开票</b>',
			handler: function() {
				me.onOpenPersonClick();
			}
		}, '-', 'save'];
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var columns = [{
			dataIndex: 'NRequestItem_NRequestForm_BLaboratory_CName',
			text: '送检单位',
			defaultRenderer: true
		},{
			dataIndex: 'NRequestItem_NRequestForm_BDept_CName',
			text: '科室',
			defaultRenderer: true
		},{
			dataIndex: 'NRequestItem_NRequestForm_CName',
			text: '病人姓名',
			width:60,
			defaultRenderer: true
		},{
			dataIndex: 'NRequestItem_NRequestForm_SerialNo',
			text: '样本预制条码',
			width:90,
			defaultRenderer: true
		},{
			dataIndex: 'NRequestItem_BarCode',
			text: '实验室条码',
			width:90,
			defaultRenderer: true
		},{
			dataIndex: 'NRequestItem_BTestItem_CName',
			text: '项目名称',
			defaultRenderer: true
		},{
			dataIndex: 'NRequestItem_BillingUnitType',
			align:'center',
			text: '开票方类型',
			width: 75,
			renderer: function(value, meta) {
				var v = JShell.PKI.Enum.UnitType['E' + value] || '';
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = 'background-color:' + JcallShell.PKI.Enum.Color['E' + value] || '#FFFFFF';
				return v;
			}
		},{
			dataIndex: 'NRequestItem_BBillingUnit_Name',
			text: '开票方(付款单位)',
			width:105,
			defaultRenderer: true
		},{
			dataIndex: 'NRequestItem_BillingUnitInfo',
			text: '个人开票信息',
			width:85,
			defaultRenderer: true
		},{
			dataIndex: 'NRequestItem_ItemPriceType',
			text: '价格类型',
			width: 60,
			align:'center',
			renderer: function(value, meta) {
				var v = JShell.PKI.Enum.ItemPriceType['E' + value] || '';
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = 'background-color:' + JcallShell.PKI.Enum.Color['E' + value] || '#FFFFFF';
				return v;
			}
		},{
			dataIndex: 'NRequestItem_ItemEditPrice',
			text: '<b style="color:blue;">终端价</b>',
			width: 60,align:'right',
			editor: {
				xtype: 'numberfield',
				minValue: 0,
				allowBlank: false
			},
			sortable: false,
			type: 'float'
		},{
			xtype: 'actioncolumn',
			text: '操作',
			align: 'center',
			width: 40,
			style:'font-weight:bold;color:white;background:orange;',
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
					if(isOpen){
						me.onOpenPersonClick([rec]);
					}else{
						me.onPersonClick([rec]);
					}
				}
			}]
		},{
			dataIndex: 'NRequestItem_BDealer_Name',
			text: '经销商',
			defaultRenderer: true
		},{
			dataIndex: 'NRequestItem_ItemContPrice',
			text: '合同价',
			align:'right',
			width:60,
			defaultRenderer: true
		},{
			dataIndex: 'NRequestItem_IsFreeType',
			text: '免单类型',width:60,
			defaultRenderer: true
		},{
			dataIndex: 'NRequestItem_ItemFreePrice',
			text: '免单价格',
			align:'right',
			width:60,
			defaultRenderer: true
		},{
			dataIndex: 'NRequestItem_CollectDate',
			text: '采样时间',width:130,isDate:true,hasTime:true
		},{
			dataIndex: 'NRequestItem_OperDate',
			text: '录入时间',width:130,isDate:true,hasTime:true
		},{
			dataIndex: 'NRequestItem_ReceiveDate',
			text: '核收时间',width:130,isDate:true,hasTime:true
		},{
			dataIndex: 'NRequestItem_SenderTime2',
			text: '报告时间',width:130,isDate:true,hasTime:true
		},{
			dataIndex: 'NRequestItem_IsStepPrice',
			text: '是否有阶梯价',
			width:90,
			align:'center',
			isBool:true,
			type:'bool'
		},{
			dataIndex: 'NRequestItem_IsFree',
			text: '是否免单',
			width:60,
			align:'center',
			isBool:true,
			type:'bool'
		},{
			dataIndex: 'NRequestItem_ItemStepPrice',
			text: '阶梯价',
			align:'right',
			width:60,
			defaultRenderer: true
		},{
			dataIndex: 'NRequestItem_ItemPrice',
			text: '应收价',
			align:'right',
			width:60,
			defaultRenderer: true
		},{
			dataIndex: 'NRequestItem_BSeller_AreaIn',
			text: '销售区域',
			defaultRenderer: true
		},{
			dataIndex: 'NRequestItem_BSeller_Name',
			text: '销售',
			defaultRenderer: true
		},{
			dataIndex: 'NRequestItem_CoopLevel',
			align:'center',
			text: '合作分级',
			width: 60,
			renderer: function(value, meta) {
				var v = JShell.PKI.Enum.CoopLevel['E' + value] || '';
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = 'background-color:' + JcallShell.PKI.Enum.Color['E' + value] || '#FFFFFF';
				return v;
			}
		},{
			dataIndex: 'NRequestItem_IsLocked',
			align:'center',
			text: '状态',
			width: 60,
			renderer: function(value, meta) {
				var v = JShell.PKI.Enum.IsLocked['E' + value] || '';
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = 'background-color:' + JcallShell.PKI.Enum.Color['E' + value] || '#FFFFFF';
				return v;
			}
		},{
			dataIndex: 'NRequestItem_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}];	
		
		return columns;
	},
	/**批量个人开票操作*/
	onAllPersonClick:function(records){
		var me = this,
			recs = records || me.getSelectionModel().getSelection(),
			len = recs.length;

		if (len == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		
		JShell.Win.open('Shell.class.pki.balance2.PersonForm', {
			formtype:'add',
			resizable: false,
			listeners: {
				save: function(win,params) {
					var Name = params.Name;
					var Price = params.Price;
					var IsAll = params.IsAll;
					win.close();
					
					me.showMask(me.saveText); //显示遮罩层
					me.saveErrorCount = 0;
					me.saveCount = 0;
					me.saveLength = len;
					
					for (var i in recs) {
						var rec = recs[i];
						var id = rec.get(me.PKField);
						var BillingUnitInfo = IsAll ? Name : rec.get('NRequestItem_NRequestForm_CName');
						var entity = {
							Id: id,
							BillingUnitType:3,//开票方类型:个人开票
							BBillingUnit:{Id:1},//开票方
							BillingUnitInfo:BillingUnitInfo,//开票方信息
							ItemPriceType:4//项目价格类型:终端价
						};
						var fields = ['Id','BillingUnitType','BBillingUnit_Id','BillingUnitInfo','ItemPriceType'];
						
						if(Price != null){
							entity.ItemPrice = Price;//项目价格=终端价
							entity.ItemEditPrice = Price;//项目指定价格=终端价格
							fields.push('ItemPrice','ItemEditPrice');
						}
						
						me.updateOneByParams(id, {
							entity: entity,
							fields: fields.join(',')
						});
					}
				}
			}
		}).show();
	},
	/**个人开票操作*/
	onPersonClick:function(records){
		var me = this,
			recs = records,
			len = recs.length;

		if (len != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		var rec = recs[0];
		var name = rec.get('NRequestItem_NRequestForm_CName');
		JShell.Win.open('Shell.class.pki.balance2.OnePersonForm', {
			formtype:'add',
			resizable: false,
			defaultName:name,
			Price:rec.get('NRequestItem_ItemEditPrice'),
			listeners: {
				save: function(win,params) {
					var Name = params.Name;
					var Price = params.Price;
					win.close();
					
					me.showMask(me.saveText); //显示遮罩层
					me.saveErrorCount = 0;
					me.saveCount = 0;
					me.saveLength = len;
					
					var id = rec.get(me.PKField);
					var BillingUnitInfo = Name;
					var entity = {
						Id: id,
						BillingUnitType:3,//开票方类型:个人开票
						BBillingUnit:{Id:1},//开票方
						BillingUnitInfo:BillingUnitInfo,//开票方信息
						ItemPriceType:4//项目价格类型:终端价
					};
					var fields = ['Id','BillingUnitType','BBillingUnit_Id','BillingUnitInfo','ItemPriceType'];
					
					if(Price != null){
						entity.ItemPrice = Price;//项目价格=终端价
						entity.ItemEditPrice = Price;//项目指定价格=终端价格
						fields.push('ItemPrice','ItemEditPrice');
					}
					
					me.updateOneByParams(id, {
						entity: entity,
						fields: fields.join(',')
					});
				}
			}
		}).show();
	},
	/**解除个人开票操作*/
	onOpenPersonClick:function(records){
		var me = this,
			recs = records || me.getSelectionModel().getSelection(),
			len = recs.length;

		if (len == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		
		var ids = [];
		for(var i=0;i<len;i++){
			ids.push(recs[i].get(me.PKField));
		}
		
		var msg = "确定要取消个人开票吗";
		
		JShell.Msg.confirm({
			msg:msg
		}, function(but) {
			if (but != "ok") return;

			var url = (me.openPersonUrl.slice(0, 4) == 'http' ? '' :
				JShell.System.Path.ROOT) + me.openPersonUrl;

			url += "?idList=" + ids.join(',');

			me.showMask(me.saveText); //显示遮罩层
			JShell.Server.get(url, function(data) {
				me.hideMask(); //隐藏遮罩层
				if (data.success) {
					if (me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, me.hideTimes);
					me.onSearch();
				} else {
					JShell.Msg.error(data.msg);
				}
			});
		});
	},
	/**保存数据*/
	onSaveClick: function() {
		var me = this,
			records = me.store.getModifiedRecords(), //获取修改过的行记录
			len = records.length;

		if (len == 0) return;

		me.showMask(me.saveText); //显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;

		for (var i = 0; i < len; i++) {
			var rec = records[i];
			var id = rec.get(me.PKField);
			var price = rec.get('NRequestItem_ItemEditPrice');
			me.updateOneByParams(id, {
				entity: {
					Id: id,
					ItemEditPrice: price,
					ItemPrice: price
				},
				fields: 'Id,ItemEditPrice,ItemPrice'
			});
		}
	}
});