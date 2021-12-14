/**
 * 实验室供货查看-供货总单列表
 * @author Jcall
 * @version 2017-02-17
 */
Ext.define('Shell.class.rea.sale.lab.show.DocGrid', {
	extend: 'Shell.class.rea.sale.basic.show.DocGrid',
	title: '实验室供货查看-供货总单列表',
	requires:[
	    'Shell.ux.form.field.SimpleComboBox',
	    'Shell.ux.toolbar.Button',
	    'Shell.ux.form.field.DateArea'
    ],
	hasButtontoolbar:false,
	
    afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		me.on({
			select:function(){
				//只有勾选一个的时候才能使用"取消验收"按钮
				var records = me.getSelectionModel().getSelection(),
					UnCheckButton = me.getComponent('buttonsToolbar2').getComponent('UnCheckButton');
				if(records.length != 1){
					UnCheckButton.disable();
				}else{
					UnCheckButton.enable();
				}
			}
		});
	},
	initComponent: function() {
		var me = this;
		
		me.PrintModel = Ext.create('Shell.class.rea.sale.print.Model');
		
		//查询框信息
		me.searchInfo = {
			width: 160,
			emptyText: '供货单号/供货方',
			itemId:'search',
			isLike: true,
			fields: ['bmscensaledoc.SaleDocNo', 'bmscensaledoc.Comp.CName']
		};
		
		me.dockedItems = [{
			dock: 'top',
			xtype:'uxButtontoolbar',
			itemId: 'buttonsToolbar',
			items:['refresh','-',{
				fieldLabel:'验收',xtype:'uxSimpleComboBox',
				itemId:'IsAccepterError',allowBlank:false,value:0,
				width:100,labelWidth:40,labelAlign:'right',hasStyle:true,
				data:[
					[0,'全部','font-weight:bold;color:black;'],
					['false','正常','font-weight:bold;color:green;'],
					['true','异常','font-weight:bold;color:red;']
				],
				listeners:{change:function(){me.onSearch();}}
			},{
				xtype:'uxSimpleComboBox',
				width:145,labelWidth:65,
				itemId:'dateType',fieldLabel:'日期范围',
				data:[
					['OperDate','操作时间'],
					['AccepterTime','验收时间']
				],
				value:'OperDate',
				listeners:{change:function(){me.onSearch();}}
			},{
				xtype:'uxdatearea',itemId:'date',
				width:194,labelWidth:0,
				listeners:{
					change:function(){
						setTimeout(function(){
							me.onSearch();
						},100);
					},
					enter:function(){
						me.onSearch();
					}
				}
			},'-',{
				type: 'search',
				info: me.searchInfo
			}]
		},{
			dock: 'top',
			xtype:'uxButtontoolbar',
			itemId: 'buttonsToolbar2',
			items:[{
				text:'金额统计',
				tooltip:'对勾选的多条供货单进行金额统计',
				iconCls:'button-config',
				itemId:'StatisticsButton',
				handler:function(){me.onStatisticsClick();}
			},'-',{
				text:'取消验收',
				tooltip:'将一个供货单重新置为"已审核"状态！',
				iconCls:'button-cancel',
				itemId:'UnCheckButton',
				handler:function(){me.onUnCheckClick();}
			},'-', {
				text:'浏览打印',
				tooltip:'勾选一个供货单进行打印！',
				iconCls:'button-print',
				itemId:'printButton',
				handler:function(){me.onPrintClick();}
			},'-', {
				text:'按勾选导出',
				tooltip:'按勾选导出成Excel文件！',
				iconCls:'file-excel',
				itemId:'outButton1',
				handler:function(){me.onOutClick(1);}
			},'-', {
				text:'按条件导出',
				tooltip:'按条件导出成Excel文件！',
				iconCls:'file-excel',
				itemId:'outButton2',
				handler:function(){me.onOutClick(2);}
			}]
		}];
		
		me.callParent(arguments);
	},
	/**取消审核*/
	onUnCheckClick:function(){
		var me = this,
			records = me.getSelectionModel().getSelection(),
			len = records.length;

		if (len != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		
		JShell.Win.open('Shell.class.rea.sale.lab.show.UnCheckForm',{
			resizable:false,
			SaleId:records[0].get(me.PKField),
			listeners:{
				save:function(p,id){
					p.close();
					me.onSearch();
				}
			}
		}).show();
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
	},
	/**获取内部条件*/
	getInternalWhere:function(){
		var me = this,
			where = me.callParent(arguments);
		
		var buttonsToolbar = me.getComponent('buttonsToolbar'),
			IsAccepterError = buttonsToolbar.getComponent('IsAccepterError'),
			dateType = buttonsToolbar.getComponent('dateType'),
			date = buttonsToolbar.getComponent('date'),
			params = [];
			
		if(where){
			params.push(where);
		}
		if(IsAccepterError){
			var value = IsAccepterError.getValue();
			if(value){
				params.push("bmscensaledoc.IsAccepterError=" + value);
			}
		}
		if(date){
			var dateField = dateType.getValue();
			var value = date.getValue();
			if(value) {
				if(value.start){
					params.push("bmscensaledoc." + dateField + ">='" + JShell.Date.toString(value.start,true) + "'");
				}
				if(value.end){
					params.push("bmscensaledoc." + dateField + "<'" + JShell.Date.toString(JShell.Date.getNextDate(value.end),true) + "'");
				}
			}
		}
		
		return params.join(" and ");
	}
});