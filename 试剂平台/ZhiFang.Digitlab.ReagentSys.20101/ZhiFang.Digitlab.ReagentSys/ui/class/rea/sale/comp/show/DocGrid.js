/**
 * 供应商供货查看-供货总单列表
 * @author Jcall
 * @version 2017-02-17
 */
Ext.define('Shell.class.rea.sale.comp.show.DocGrid', {
	extend: 'Shell.class.rea.sale.basic.show.DocGrid',
	title: '供应商供货查看-供货总单列表',
    
	initComponent: function() {
		var me = this;
		
		me.PrintModel = Ext.create('Shell.class.rea.sale.print.Model');
		
		//查询框信息
		me.searchInfo = {
			width: 160,
			emptyText: '供货单号/订货方',
			itemId:'search',
			isLike: true,
			fields: ['bmscensaledoc.SaleDocNo', 'bmscensaledoc.Lab.CName']
		};
		
		me.buttonToolbarItems = ['refresh','-',{
			fieldLabel:'单据状态',xtype:'uxSimpleComboBox',
			itemId:'status',allowBlank:false,value:me.defaultStatusValue,
			width:140,labelWidth:55,labelAlign:'right',hasStyle:true,
			data:JcallShell.REA.Enum.getList('BmsCenSaleDoc_Status',true,true),
			listeners:{change:function(){me.onSearch();}}
		},'-', {
			text:'浏览打印',
			tooltip:'勾选一个供货单进行打印！',
			iconCls:'button-print',
			itemId:'printButton',
			handler:function(){me.onPrintClick();}
		},'-', {
			type: 'search',
			info: me.searchInfo
		}];
		
		me.callParent(arguments);
	},
	onPrintClick:function(){
		var me = this,
			records = me.getSelectionModel().getSelection(),
			len = records.length;

		if (len != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		
		//加载Lodop组件
		me.Lodop = me.Lodop || Ext.create('Shell.lodop.Lodop');
		
		var LODOP = me.Lodop.getLodop();
		if(!LODOP) return;
		
		me.PrintModel.getModelContent(records[0].get(me.PKField),function(table){
			LODOP.PRINT_INIT("实验室名称耗材、一次性物品验收记录");
			//intOrient
			//1-纵向打印，固定纸张；
			//2-横向打印，固定纸张；
			//3-纵向打印，宽度固定，高度按打印内容的高度自适应(见样例18)；
			//0-方向不定，由操作者自行选择或按打印机缺省设置。
			LODOP.SET_PRINT_PAGESIZE(2,0,0,"A4");
			LODOP.ADD_PRINT_TABLE("1%","1%","98%","98%",table);
			LODOP.ADD_PRINT_BARCODE(10,'90%',100,100,"QRCode",records[0].get("BmsCenSaleDoc_SaleDocNo"));
			LODOP.PREVIEW();
		});
	}
});