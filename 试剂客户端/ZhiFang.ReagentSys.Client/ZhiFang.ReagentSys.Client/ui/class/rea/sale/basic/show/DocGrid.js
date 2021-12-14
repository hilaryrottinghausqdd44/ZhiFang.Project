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
	
	/**导出Excel数据服务路径*/
	outExcelUrl: '/ReagentService.svc/RS_UDTO_GetReportDetailExcelPath',
	
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
	},
	/**导出供货单Excel*/
	onOutClick:function(type){
		var me = this;
			
		//验收时间（倒序）+供货单号（正序）+货品编码（正序）
		var sort = [
			{"property":"BmsCenSaleDtl_BmsCenSaleDoc_AccepterTime","direction":"DESC"},
			{"property":"BmsCenSaleDtl_BmsCenSaleDoc_SaleDocNo","direction":"ASC"},
			{"property":"BmsCenSaleDtl_Goods_GoodsNo","direction":"ASC"}
		];
		me.UpdateForm = me.UpdateForm || Ext.create('Ext.form.Panel',{
			items:[
				{xtype:'filefield',name:'file'},
				{xtype:'textfield',name:'reportType',value:'2'},
				{xtype:'textfield',name:'sort',value:Ext.JSON.encode(sort)},
				{xtype:'textfield',name:'idList'},
				{xtype:'textfield',name:'where'},
				{xtype:'textfield',name:'isHeader',value:"0"}
			]
		});
		//清空数据
		me.UpdateForm.getForm().setValues({idList:'',where:''});
		
		if(type == 1){//类型为勾选导出
			var records = me.getSelectionModel().getSelection(),
				len = records.length,
				ids = [];
				
			if (len == 0) {
				JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
				return;
			}
			
			for(var i=0;i<len;i++){
				ids.push(records[i].get(me.PKField));
				me.UpdateForm.getForm().setValues({idList:ids.join(",")});
			}
		}else if(type == 2){//类型为条件导出
			var gridUrl = me.getLoadUrl(),
				arr = gridUrl.split('&where='),
				where = '';
				
			if(arr.length == 2){
				where = JShell.String.decode(arr[1]);
				where = where.replace(/bmscensaledoc./g,'bmscensaledtl.BmsCenSaleDoc.');
			}
			
			me.UpdateForm.getForm().setValues({where:where});
		}
		
		me.showMask("数据请求中...");
		var url = JShell.System.Path.ROOT + me.outExcelUrl;
		me.UpdateForm.getForm().submit({
			url:url,
            //waitMsg:JShell.Server.SAVE_TEXT,
            success:function (form,action) {
            	me.hideMask();
        		var fileName = action.result.ResultDataValue;
        		var downloadUrl = JShell.System.Path.ROOT + '/ReagentService.svc/RS_UDTO_DownLoadExcel';
				downloadUrl += '?isUpLoadFile=1&operateType=0&downFileName=平台供货单数据&fileName=' + fileName.split('\/')[2];
				downloadUrl = encodeURI(downloadUrl);
				window.open(downloadUrl);
            },
            failure:function(form,action){
            	me.hideMask();
				JShell.Msg.error(action.result.ErrorInfo);
			}
		});
	}
});