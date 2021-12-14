/**
 * 财务锁定
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.balance2.LockFGrid', {
	extend: 'Shell.class.pki.balance2.ItemGrid',
	
	requires: [
		'Shell.ux.form.field.BoolComboBox'
	],
	
	title: '财务锁定',
	
	/**默认条件*/
	defaultWhere: '(nrequestitem.IsLocked=1 or nrequestitem.IsLocked=2)',
	/**带功能按钮栏*/
	hasButtontoolbar:true,
	
	/**锁定服务*/
	lockUrl: '/StatService.svc/Stat_UDTO_SelectFinanceLocking',
	
	/**锁定的提示文字*/
	lockText: '财务锁定',
	/**锁定的状态值*/
	lockValue: '2',
	/**返差价的提示文字*/
	returnText:'返差价',
	/**返差价的状态值*/
	returnValue:'1',
	
	/**财务报表类型*/
	reportType:'2',
	
	/**查询栏参数设置*/
	searchToolbarConfig:{
		/**是否包含财务*/
		hasFinanceLock:true,
		/**对账状态列表*/
		IsLockedList: [
			[0, '全部', 'font-weight:bold;color:black;'],
			['1', '对账中', 'font-weight:bold;color:' + JcallShell.PKI.Enum.Color['E1'] + ';'],
			['2', '财务锁定', 'font-weight:bold;color:' + JcallShell.PKI.Enum.Color['E2'] + ';']
		]
	},
	plugins:Ext.create('Ext.grid.plugin.CellEditing',{clicksToEdit:1}),
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		me.on({
			beforeedit:function(editor,e){
				var IsSpread = e.record.get('NRequestItem_IsSpread') == '1';
				if(!IsSpread) return false;
			}
		});
	},
	
	initComponent:function(){
		var me = this;
		
		//自定义按钮功能栏
		me.buttonToolbarItems = [{
			text: me.lockText,
			iconCls: 'button-lock',
			tooltip: '<b>将选中的记录进行' + me.lockText + '</b>',
			handler: function() {
				me.doCheckedLock(false);
			}
		}, '-', {
			text: '解除' + me.lockText,
			iconCls: 'button-text-relieve',
			tooltip: '<b>将选中的记录进行解除' + me.lockText + '</b>',
			handler: function() {
				me.doCheckedLock(true);
			}
		}, '-', {
			text: me.returnText,
			iconCls: 'button-text-return',
			tooltip: '<b>将选中的记录进行' + me.returnText + '</b>',
			handler: function() {
				me.doCheckedReturn(false);
			}
		}, '-', {
			text: '解除' + me.returnText,
			iconCls: 'button-text-relieve',
			tooltip: '<b>将选中的记录进行解除' + me.returnText + '</b>',
			handler: function() {
				me.doCheckedReturn(true);
			}
		},'-','save'];
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var columns = [{
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
			dataIndex: 'NRequestItem_BTestItem_CName',
			text: '项目名称',
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
			xtype: 'actioncolumn',
			text: '锁',
			align: 'center',
			width: 40,
			style:'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					if (record.get('NRequestItem_IsLocked') == me.lockValue) {
						meta.tdAttr = 'data-qtip="<b>解除' + me.lockText + '</b>"';
						meta.style = 'background-color:green;';
						return 'button-text-relieve hand';
					} else {
						meta.tdAttr = 'data-qtip="<b>' + me.lockText + '</b>"';
						meta.style = 'background-color:red;';
						return 'button-lock hand';
					}
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var id = rec.get(me.PKField);
					var isOpen = rec.get('NRequestItem_IsLocked') == me.lockValue ? true : false;
					me.doLock(id, isOpen);
				}
			}]
		},{
			dataIndex:'NRequestItem_IsSpread',text:'已返差价',
			width:60,isBool:true,align:'center',type:'bool'
		},{
			xtype: 'actioncolumn',
			text: '返',
			align: 'center',
			width: 40,
			style:'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					if(record.get('NRequestItem_IsLocked') == me.lockValue){
						if (record.get('NRequestItem_IsSpread') == me.returnValue) {
							meta.tdAttr = 'data-qtip="<b>解除' + me.returnText + '</b>"';
							meta.style = 'background-color:green;';
							return 'button-text-relieve hand';
						} else {
							meta.tdAttr = 'data-qtip="<b>' + me.returnText + '</b>"';
							meta.style = 'background-color:red;';
							return 'button-text-return hand';
						}
					}
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var id = rec.get(me.PKField);
					var isOpen = rec.get('NRequestItem_IsSpread') == me.returnValue ? true : false;
					me.doReturn(id, isOpen);
				}
			}]
		},{
			dataIndex: 'NRequestItem_SpreadMemo',
			text: '<b style="color:blue;">已返差价备注</b>',
			width:200,editor:{},
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
			text: '免单价格',
			align:'right',
			width:60,
			defaultRenderer: true
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
			dataIndex: 'NRequestItem_ItemContPrice',
			text: '合同价',
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
			dataIndex: 'NRequestItem_BDealer_Name',
			text: '经销商',
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
			dataIndex: 'NRequestItem_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}];	
		
		return columns;
	},
	/**锁定选中的数据*/
	doCheckedLock: function(isOpen) {
		var me = this,
			records = me.getSelectionModel().getSelection(),
			len = records.length;

		if (len == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}

		var ids = [];
		for (var i = 0; i < len; i++) {
			ids.push(records[i].get(me.PKField));
		}

		me.doLock(ids.join(","), isOpen);
	},
	/**锁定一条数据*/
	doLock: function(ids, isOpen) {
		var me = this;
		var msg = isOpen ? '解除' : '';
		
		msg = '您确定要' + msg + me.lockText + '吗？';

		JShell.Msg.confirm({
			msg:msg
		}, function(but) {
			if (but != "ok") return;

			var url = (me.lockUrl.slice(0, 4) == 'http' ? '' :
				JShell.System.Path.ROOT) + me.lockUrl;

			url += "?idList=" + ids + "&isLock=" + (isOpen ? false : true);

			me.showMask(me.saveText); //显示遮罩层
			JShell.Server.get(url, function(data) {
				me.hideMask(); //隐藏遮罩层
				if (data.success) {
					if (me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, me.hideTimes);
					me.onSearch();
				} else {
					if(data.msg == 'ERROR001'){
						data.msg = '提示找不到对应的合同价格，对账错误';
					}
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
			var SpreadMemo = rec.get('NRequestItem_SpreadMemo');
			me.updateOneByParams(id, {
				entity: {
					Id: id,
					SpreadMemo:SpreadMemo
				},
				fields: 'Id,SpreadMemo'
			});
		}
	},
	/**将选中的数据返差价*/
	doCheckedReturn:function(isOpen){
		var me = this,
			records = me.getSelectionModel().getSelection(),
			len = records.length;

		if (len == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		
		var msg = '您确定要' + (isOpen ? '解除' : '') + me.returnText + '吗？';
		JShell.Msg.confirm({
			msg:msg,
			multiline:!isOpen,
			emptyText:'已返差价备注'
		}, function(but,text) {
			if (but != "ok") return;
			
			me.showMask(me.saveText); //显示遮罩层
			me.saveErrorCount = 0;
			me.saveCount = 0;
			me.saveLength = 1;
			
			var IsSpread = isOpen ? '0' : '1';
			for (var i = 0; i < len; i++) {
				var rec = records[i];
				var id = rec.get(me.PKField);
				me.updateOneByParams(id, {
					entity: {
						Id: id,
						IsSpread: IsSpread,
						SpreadMemo:text
					},
					fields: 'Id,IsSpread,SpreadMemo'
				});
			}
		});
	},
	/**返差价处理*/
	doReturn:function(id,isOpen){
		var me = this;
		var rec = me.store.findRecord(me.PKField, id);
		var msg = '您确定要' + (isOpen ? '解除' : '') + me.returnText + '吗？';
		
		JShell.Msg.confirm({
			msg:msg,
			multiline:!isOpen,
			emptyText:'已返差价备注'
		}, function(but,text) {
			if (but != "ok") return;
			
			me.showMask(me.saveText); //显示遮罩层
			me.saveErrorCount = 0;
			me.saveCount = 0;
			me.saveLength = 1;
			
			var IsSpread = isOpen ? '0' : '1';
			me.updateOneByParams(id, {
				entity: {
					Id: id,
					IsSpread: IsSpread,
					SpreadMemo:text
				},
				fields: 'Id,IsSpread,SpreadMemo'
			});
		});
	}
});