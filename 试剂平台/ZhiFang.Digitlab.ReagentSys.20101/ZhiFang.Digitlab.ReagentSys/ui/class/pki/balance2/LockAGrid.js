/**
 * 对账锁定
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.balance2.LockAGrid', {
	extend: 'Shell.class.pki.balance2.ItemGrid',
	
	title: '对账锁定',
	
	/**默认条件*/
	defaultWhere: '(nrequestitem.IsLocked=0 or nrequestitem.IsLocked=1 or nrequestitem.IsLocked=3)',
	/**带功能按钮栏*/
	hasButtontoolbar:true,
	
	/**锁定服务*/
	lockUrl: '/StatService.svc/Stat_UDTO_SelectReconciliationLocking',
	/**锁定的确认内容*/
	lockText:'您确定要对账锁定吗？',
	/**解除锁定的确认内容*/
	openText:'您确定要解除对账锁定吗？',
	
	/**财务报表类型*/
	reportType:'1',
	
	initComponent:function(){
		var me = this;
		
		//自定义按钮功能栏
		me.buttonToolbarItems = [{
			text: '对账锁定',
			iconCls: 'button-lock',
			tooltip: '<b>将选中的记录进行对账锁定</b>',
			handler: function() {
				me.doCheckedLock(false);
			}
		}, '-', {
			text: '解除锁定',
			iconCls: 'button-text-relieve',
			tooltip: '<b>将选中的记录进行解除锁定</b>',
			handler: function() {
				me.doCheckedLock(true);
			}
		}, '-', {
			itemId:'print',
			text: '打印异常清单',
			hidden:true,
			iconCls: 'button-print',
			tooltip: '<b>打印清单</b>',
			handler: function() {}
		}, {
			itemId:'print2',
			text: '打印异常送检单位和科室',
			hidden:true,
			iconCls: 'button-print',
			tooltip: '<b>打印送检单位和科室</b>',
			handler: function() {}
		}];
		
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
			text: '操作',
			align: 'center',
			width: 40,
			style:'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					if (record.get('NRequestItem_IsLocked') == '1') {
						meta.tdAttr = 'data-qtip="<b>解除对账锁定</b>"';
						meta.style = 'background-color:green;';
						return 'button-text-relieve hand';
					} else {
						meta.tdAttr = 'data-qtip="<b>对账锁定</b>"';
						meta.style = 'background-color:red;';
						return 'button-lock hand';
					}
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var id = rec.get(me.PKField);
					var isOpen = rec.get('NRequestItem_IsLocked') == '1' ? true : false;
					me.doLock(id, isOpen);
				}
			}]
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
		var msg = isOpen ? me.openText : me.lockText;

		JShell.Msg.confirm({
			msg:msg
		}, function(but) {
			if (but != "ok") return;

//			var url = (me.lockUrl.slice(0, 4) == 'http' ? '' :
//				JShell.System.Path.ROOT) + me.lockUrl;
//
//			url += "?idList=" + ids + "&isLock=" + (isOpen ? false : true);
//
//			me.showMask(me.saveText); //显示遮罩层
//			JShell.Server.get(url, function(data) {
//				me.hideMask(); //隐藏遮罩层
//				if (data.success) {
//					if (me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, me.hideTimes);
//					me.onSearch();
//				} else {
//					if(data.msg == 'ERROR001'){
//						data.msg = '提示找不到对应的合同价格，对账错误';
//					}
//					JShell.Msg.error(data.msg);
//				}
//			});
			
			var url = (me.lockUrl.slice(0, 4) == 'http' ? '' :
				            JShell.System.Path.ROOT) + me.lockUrl;

            var params = {
                idList:ids,
                isLock: (isOpen ? false : true)
            };

            me.showMask(me.saveText); //显示遮罩层
            JShell.Server.post(url,Ext.JSON.encode(params), function (data) {
                me.hideMask(); //隐藏遮罩层
                if (data.success) {
                    if (me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, me.hideTimes);
                    me.onSearch();
                } else {
                    if (data.msg == 'ERROR001') {
                        data.msg = '提示找不到对应的合同价格，对账错误';
                    }
                    JShell.Msg.error(data.msg);
                }
            },null,600000);
		});
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
			
		me.showPrintButtons();
		
		return me.callParent(arguments);
	},
	/**显示打印按钮*/
	showPrintButtons:function(){
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			print = buttonsToolbar.getComponent('print'),
			print2 = buttonsToolbar.getComponent('print2'),
			IsLocked = me.getComponent('filterToolbar').getComponent('IsLocked');
			
		if(IsLocked){
			if(IsLocked.getValue() == '3'){
				print.show();
				print2.show();
			}else{
				print.hide();
				print2.hide();
			}
		}
	}
});