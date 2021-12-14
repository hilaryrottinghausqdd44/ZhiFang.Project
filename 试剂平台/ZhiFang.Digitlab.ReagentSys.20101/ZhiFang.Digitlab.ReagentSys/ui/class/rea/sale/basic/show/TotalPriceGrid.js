/**
 * 供货单金额统计
 * @author Jcall
 * @version 2017-03-07
 */
Ext.define('Shell.class.rea.sale.basic.show.TotalPriceGrid', {
	extend: 'Shell.ux.grid.PostPanel',
	title: '供货单金额统计',
	width: 1050,
	height: 1000,

	/**获取数据服务路径*/
	selectUrl: '/ReagentService.svc/RS_UDTO_StatBmsCenSaleDocTotalPrice',
	/**字段数组*/
	_fieldsList: [],
	/**带功能按钮栏*/
	hasButtontoolbar: false,
	/**带分页栏*/
	hasPagingtoolbar: false,
	/**是否启用序号列*/
	hasRownumberer: false,
	/**后台排序*/
	remoteSort: false,
	/**默认加载数据*/
	defaultLoad: true,
	features: [{
		ftype: 'summary'
	}],

	/**供货单ID数组*/
	saleIDList: [],

	initComponent: function() {
		var me = this;

		//数据列
		me.columns = me.createGridColumns();

		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			dataIndex: 'BmsCenSaleDoc_SaleDocNo',
			text: '供货单号',
			width: 130,
			defaultRenderer: true,
			summaryType: 'count',
			summaryRenderer: function(value, summaryData, dataIndex) {
				return '<b>共 <span style="color:green;">' + value + '</span> 个供货单</b>';
			}
		}, {
			dataIndex: 'BmsCenSaleDoc_OperDate',
			text: '操作时间',
			align: 'center',
			width: 130,
			isDate: true,
			hasTime: true
		}, {
			dataIndex: 'BmsCenSaleDoc_TotalPrice',
			text: '金额',
			align: 'right',
			width: 130,
			type:'float',
			renderer: function(value){
				return Ext.util.Format.currency(value,'￥',2) + '元';
			},
			summaryRenderer: function(value){
				return '共<span style="color:green;">' + 
					Ext.util.Format.currency(value,'￥',2) + '</span>元';
			},
			summaryType: 'sum'
		}, {
			dataIndex: 'BmsCenSaleDoc_Lab_CName',
			text: '订货方',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDoc_Comp_CName',
			text: '供货方',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDoc_UserName',
			text: '操作人员',
			width: 60,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDoc_UrgentFlag',
			text: '紧急标志',
			align: 'center',
			width: 60,
			renderer: function(value, meta) {
				var v = JShell.REA.Enum.BmsCenSaleDoc_UrgentFlag['E' + value] || '';
				if(v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = 'background-color:' + JcallShell.REA.Enum.Color['E' + value] || '#FFFFFF';
				return v;
			}
		}, {
			dataIndex: 'BmsCenSaleDoc_Status',
			text: '单据状态',
			align: 'center',
			width: 60,
			renderer: function(value, meta) {
				var v = JShell.REA.Enum.BmsCenSaleDoc_Status['E' + value] || '';
				if(v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = 'background-color:' + JcallShell.REA.Enum.Color['E' + value] || '#FFFFFF';
				return v;
			}
		}, {
			dataIndex: 'BmsCenSaleDoc_IOFlag',
			text: '提取标志',
			align: 'center',
			width: 60,
			renderer: function(value, meta) {
				var v = JShell.REA.Enum.BmsCenSaleDoc_IOFlag['E' + value] || '';
				if(v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = 'background-color:' + JcallShell.REA.Enum.Color['E' + value] || '#FFFFFF';
				return v;
			}
		}, {
			dataIndex: 'BmsCenSaleDoc_Memo',
			text: '备注',
			width: 200,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDoc_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}];

		me._fieldsList = [];
		for(var i in columns) {
			me._fieldsList.push(columns[i].dataIndex);
		}

		return columns;
	},

	onSearch: function() {
		var me = this;
		
		me.UpdateForm = Ext.create('Ext.form.Panel',{
			items:[
				{xtype:'filefield',name:'file'},
				{xtype:'textfield',name:'fields',value:me._fieldsList.join(',')},
				{xtype:'textfield',name:'listID',value:me.saleIDList.join(',')}
			]
		});
		
		me.showMask(me.loadingText);//显示遮罩层
		me.UpdateForm.getForm().submit({
			url:me.getLoadUrl(),
            success:function (form,action) {
            	me.hideMask();//隐藏遮罩层
        		var value = action.result.ResultDataValue;
        		var data = Ext.JSON.decode(value);
        		me.store.loadData(data.list);
				
				if(data.list.length == 0){
					var msg = me.msgFormat.replace(/{msg}/,JShell.Server.NO_DATA);
					JShell.Action.delay(function(){me.getView().update(msg);},200);
				}
            },
            failure:function(form,action){
            	me.hideMask();
				var msg = me.msgFormat.replace(/{msg}/,action.result.ErrorInfo);
				JShell.Action.delay(function(){me.getView().update(msg);},200);
			}
		});
	}
});