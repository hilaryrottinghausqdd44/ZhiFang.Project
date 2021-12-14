/**
 * 免单项目
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.balance2.FreeSingleGrid', {
	extend: 'Shell.class.pki.balance2.ItemGrid',

	title: '免单',
	
	/**默认条件*/
	defaultWhere: "nrequestitem.IsLocked=1 and (nrequestitem.ItemPriceType='1' or nrequestitem.ItemPriceType='2' or nrequestitem.ItemPriceType='3')",
	/**带功能按钮栏*/
	hasButtontoolbar:true,
	
	/**取消免单服务地址*/
	openFreeUrl:'/StatService.svc/Stat_UDTO_CancelFreeSingle',
	
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
			['3', '免单价', 'font-weight:bold;color:' + JcallShell.PKI.Enum.Color['E3'] + ';']
		]
	},
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		me.on({
			beforeedit:function(editor,e){
				var isFree = e.record.get('NRequestItem_ItemPriceType') == '3';
				if(!isFree) return false;
			}
		});
	},
	
	initComponent:function(){
		var me = this;
		
		//自定义按钮功能栏
		me.buttonToolbarItems = [{
			text: '批量免单',
			iconCls: 'button-text-free',
			tooltip: '<b>批量免单</b>',
			handler: function() {
				me.onFreeClick();
			}
		}, '-', {
			text: '批量解单',
			iconCls: 'button-text-relieve',
			tooltip: '<b>批量解单</b>',
			handler: function() {
				me.onOpenFreeClick();
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
			dataIndex: 'NRequestItem_BBillingUnit_BillingUnitType',
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
			dataIndex: 'NRequestItem_IsFreeType',
			text: '免单类型',width:60,
			defaultRenderer: true
		},{
			dataIndex: 'NRequestItem_ItemFreePrice',
			text: '<b style="color:blue;">免单价格</b>',
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
			style:'font-weight:bold;color:white;background:orange;',
			width: 40,
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					var type = record.get('NRequestItem_ItemPriceType');
					if (type == '3') {
						meta.tdAttr = 'data-qtip="<b>解除免单</b>"';
						meta.style = 'background-color:green;';
						return 'button-text-relieve hand';
					} else if (type == '1' || type == '2') {
						meta.tdAttr = 'data-qtip="<b>免单</b>"';
						meta.style = 'background-color:red;';
						return 'button-text-free hand';
					}
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var id = rec.get(me.PKField);
					var isOpen = rec.get('NRequestItem_ItemPriceType') == '3' ? true : false;
					if(isOpen){
						me.onOpenFreeClick([rec]);
					}else{
						me.onFreeClick([rec]);
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
			dataIndex: 'NRequestItem_ItemEditPrice',
			text: '终端价',
			align:'right',
			width:60,
			defaultRenderer: true
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
			dataIndex: 'NRequestItem_BillingUnitInfo',
			text: '个人开票信息',
			width:85,
			defaultRenderer: true
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
	/**免单操作*/
	onFreeClick:function(records){
		var me = this,
			recs = records || me.getSelectionModel().getSelection(),
			len = recs.length;

		if (len == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		
		JShell.Win.open('Shell.class.pki.balance2.FreeTypeWin', {
			resizable: false,
			listeners: {
				save: function(win, values) {
					var IsFreeType = values.FreeType;
					var ItemFreePrice = values.ItemFreePrice;
					win.close();

					me.showMask(me.saveText); //显示遮罩层
					me.saveErrorCount = 0;
					me.saveCount = 0;
					me.saveLength = len;

					for (var i in recs) {
						var id = recs[i].get(me.PKField);
						me.updateOneByParams(id, {
							entity: {
								Id: id,
								IsFree:'1',//免单标志
								ItemPriceType: '3',//项目价格类型:免单
								ItemPrice:ItemFreePrice,//项目价格=免单价格
								ItemStepPrice:null,//项目阶梯价格
								ItemFreePrice:ItemFreePrice,//项目免单价格
								IsFreeType: IsFreeType//免单类型
							},
							fields: 'Id,IsFree,ItemPriceType,ItemPrice,ItemStepPrice,ItemFreePrice,IsFreeType'
						});
					}
				}
			}
		}).show();
	},
	/**解除免单操作*/
	onOpenFreeClick:function(records){
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
		
		var msg = "确定要取消免单吗";
		
		JShell.Msg.confirm({
			msg:msg
		}, function(but) {
			if (but != "ok") return;

			var url = (me.openFreeUrl.slice(0, 4) == 'http' ? '' :
				JShell.System.Path.ROOT) + me.openFreeUrl;

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
			var price = rec.get('NRequestItem_ItemFreePrice');
			me.updateOneByParams(id, {
				entity: {
					Id: id,
					ItemFreePrice: price,
					ItemPrice: price
				},
				fields: 'Id,ItemFreePrice,ItemPrice'
			});
		}
	}
});