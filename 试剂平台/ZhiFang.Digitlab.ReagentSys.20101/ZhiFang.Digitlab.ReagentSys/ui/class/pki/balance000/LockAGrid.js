/**
 * 对账锁定
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.balance.LockAGrid', {
	extend: 'Shell.class.pki.balance.ItemBasicGrid',

	requires: [
		'Shell.ux.form.field.YearComboBox',
		'Shell.ux.form.field.MonthComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],

	title: '对账锁定',
	
	/**默认条件*/
	defaultWhere: '(nrequestitem.IsLocked=0 or nrequestitem.IsLocked=1 or nrequestitem.IsLocked=3)',
	
	/**锁定服务*/
	lockUrl: '/StatService.svc/Stat_UDTO_SelectReconciliationLocking',
	/**锁定的提示文字*/
	lockTooltipText: '对账锁定',
	/**锁定的状态值*/
	lockValue:'1',
	/**显示对账状态下拉框*/
	showIsLockedCombobox: true,
	
	/**锁定的确认内容*/
	lockText:'您确定要对账锁定吗？',
	/**解除锁定的确认内容*/
	openText:'您确定要解除对账锁定吗？',
	
	/**默认选中送检时间*/
	isDateRadio:false,
	
	/**默认每页数量*/
	defaultPageSize:200,
	
	initComponent: function() {
		var me = this;

		//自定义按钮功能栏
		me.buttonToolbarItems = ['lock', '-', {
			text: '解除锁定',
			iconCls: 'button-text-relieve',
			tooltip: '<b>解除锁定</b>',
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
			dataIndex: 'NRequestItem_IsLocked',
			text: '状态',
			width: 60,
			renderer: function(value, meta) {
				var v = JShell.PKI.Enum.IsLocked['E' + value] || '';
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = 'background-color:' + JcallShell.PKI.Enum.Color['E' + value] || '#FFFFFF';
				return v;
			}
		}, {
			xtype: 'actioncolumn',
			text: '操作',
			align: 'center',
			width: 40,
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					if (record.get('NRequestItem_IsLocked') == me.lockValue) {
						meta.tdAttr = 'data-qtip="<b>解除锁定</b>"';
						meta.style = 'background-color:green;';
						return 'button-text-relieve hand';
					} else {
						meta.tdAttr = 'data-qtip="<b>' + me.lockTooltipText + '</b>"';
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
		}, {
			dataIndex: 'NRequestItem_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}];

		me.callParent(arguments);
	},
	

	/**锁定按钮点击处理*/
	onLockClick: function() {
		this.doCheckedLock(false);
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
			IsLocked = me.getFilterComponent('IsLocked');
			
		if(IsLocked){
			if(IsLocked.getValue() == me.IsLockedList[4][1]){
				print.show();
				print2.show();
			}else{
				print.hide();
				print2.hide();
			}
		}
	}
});