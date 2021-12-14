/**
 * 供货总单列表-基础查看
 * @author Jcall
 * @version 2017-02-17
 */
Ext.define('Shell.class.rea.sale.basic.show.DocGrid', {
	extend: 'Shell.class.rea.sale.basic.DocGrid',
	title: '供货总单列表-基础查看',
	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
	
	initComponent: function() {
		var me = this;
		
		me.addEvents('addclick','editclick');
		
		//查询框信息
		me.searchInfo = me.searchInfo || {
			width: 160,
			emptyText: '供货单号/订货方',
			itemId:'search',
			isLike: true,
			fields: ['bmscensaledoc.SaleDocNo', 'bmscensaledoc.Lab.CName']
		};
		me.buttonToolbarItems = me.buttonToolbarItems || ['refresh','-',{
			fieldLabel:'验收',xtype:'uxSimpleComboBox',
			itemId:'IsAccepterError',allowBlank:false,value:0,
			width:100,labelWidth:40,labelAlign:'right',hasStyle:true,
			data:[
				[0,'全部','font-weight:bold;color:black;'],
				['false','正常','font-weight:bold;color:green;'],
				['true','异常','font-weight:bold;color:red;']
			],
			listeners:{change:function(){me.onSearch();}}
		},'-',{
			text:'金额统计',
			tooltip:'对勾选的多条供货单进行金额统计',
			iconCls:'button-config',
			itemId:'StatisticsButton',
			handler:function(){me.onStatisticsClick();}
		},'->', {
			type: 'search',
			info: me.searchInfo
		}];
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this,
			columns = me.callParent(arguments);
		
		columns.splice(1,0,{
			dataIndex: 'BmsCenSaleDoc_IsAccepterError',
			text: '验收',
			width: 40,
			renderer: function(value, meta) {
				value += '';
				var v = value == 'false' ? '正常' : '异常';
				meta.style = value == 'false' ? 'color:green;' : 'color:red;';
				return v;
			}
		});
		columns.push({
			dataIndex: 'BmsCenSaleDoc_AccepterName',
			text: '主验收人',
			width: 60,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDoc_SecAccepterName',
			text: '次验收人',
			width: 60,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDoc_AccepterTime',
			text: '验收时间',
			width: 130,
			isDate:true,
			hasTime:true
		});
		
		return columns;
	},
	/**获取内部条件*/
	getInternalWhere:function(){
		var me = this,
			where = me.callParent(arguments);
		
		var buttonsToolbar = me.getComponent('buttonsToolbar'),
			IsAccepterError = buttonsToolbar.getComponent('IsAccepterError');
		
		if(IsAccepterError){
			var value = IsAccepterError.getValue();
			if(value){
				if(where){
					where += ' and ';
				}
				where += 'bmscensaledoc.IsAccepterError=' + value;
			}
		}
		
		return where;
	},
	/**金额统计*/
	onStatisticsClick:function(){
		var me = this,
			records = me.getSelectionModel().getSelection(),
			len = records.length,
			saleIDList = [];

		if (len == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		
		for(var i=0;i<len;i++){
			saleIDList.push(records[i].get(me.PKField));
		}
		
		JShell.Win.open('Shell.class.rea.sale.basic.show.TotalPriceGrid',{
			saleIDList:saleIDList
		}).show();
	}
});