/**
 * 实验室供货验收-供货总单列表
 * @author Jcall
 * @version 2017-02-17
 */
Ext.define('Shell.class.rea.sale.lab.check.DocGrid', {
	extend: 'Shell.class.rea.sale.basic.DocGrid',
	title: '实验室供货验收-供货总单列表',
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		me.on({
			select:function(rowModel,record){
				var status = record.get('BmsCenSaleDoc_Status') + '';
				me.showCheckButton(status);
			}
		});
	},
	
	initComponent: function() {
		var me = this;
		
		me.addEvents('checkclick');
		
		//查询框信息
		me.searchInfo = {
			width: 160,
			emptyText: '供货单号/供货方',
			itemId:'search',
			isLike: true,
			fields: ['bmscensaledoc.SaleDocNo', 'bmscensaledoc.Comp.CName']
		};
		me.buttonToolbarItems = ['refresh','-',{
			text:'验收',
			tooltip:'收货单位验收确认，验收后将不能更改供货单明细内容',
			iconCls:'button-check',
			itemId:'CheckButton',
			disabled:true,
			handler:function(){me.onCheckClick();}
		},'-','->', {
			type: 'search',
			info: me.searchInfo
		}];
		
		me.callParent(arguments);
	},
	/**获取内部条件*/
	getInternalWhere:function(){
		var me = this,
			where = me.callParent(arguments);
		
		if(where){
			where += ' and ';
		}
		where += 'bmscensaledoc.Status=2';
		
		return where;
	},
	/**验收确认*/
	onCheckClick:function(){
		var me = this,
			records = me.getSelectionModel().getSelection();
			
		if(records.length != 1){
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		
		me.fireEvent('checkclick',me,records[0]);
	},
	/**保存验收数据*/
	onCheckData:function(record){
		var me = this;
		
		me.updateOneByParams({
			entity:{
				Id:record.get(me.PKField),
				Status:'3'
			},
			fields:'Id,Status'
		},function(){
			record.set('BmsCenSaleDoc_Status','3');
			record.commit();
			//选中这一行
			me.getSelectionModel().deselect(record);
			me.getSelectionModel().select(record);
		});
	},
	
	/**更新供货单数据*/
	updateOneByParams:function(params,callback){
		var me = this,
			url = JShell.System.Path.ROOT + me.editUrl;
		
		JShell.Server.post(url,Ext.JSON.encode(params),function(data){
			if(data.success){
				JShell.Msg.alert(JcallShell.All.SUCCESS_TEXT,null,1000);
				callback();
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	},
	
	showCheckButton:function(status){
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			CheckButton = buttonsToolbar.getComponent('CheckButton');
			
		CheckButton.disable();
		
		if(status == '2'){
			CheckButton.enable();
		}
	}
});