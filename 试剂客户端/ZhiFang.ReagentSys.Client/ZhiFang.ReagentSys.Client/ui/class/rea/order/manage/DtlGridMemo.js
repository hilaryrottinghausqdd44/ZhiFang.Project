/**
 * 订货明细列表
 * @author Jcall
 * @version 2018-01-11
 */
Ext.define('Shell.class.rea.order.manage.DtlGridMemo', {
	extend: 'Shell.class.rea.order.basic.DtlGridMemo',
	title: '订货明细列表',
	
	/**创建数据列*/
	createGridColumns:function(){
		var me = this,
			columns = me.callParent(arguments);
			
		columns[3].text = '<b style="color:blue;">订购数</b>';
		columns[3].editor = {xtype:'numberfield',minValue:0,allowBlank:false};
		
		return columns; 
	},
	/**修改一条明细信息*/
	updateOneDtl:function(record){
		var me = this,
			url = JShell.System.Path.ROOT + me.editUrl;
			
		var entity = {
			Id:record.get('BmsCenOrderDtl_Id'),
			GoodsQty:record.get('BmsCenOrderDtl_GoodsQty'),
			Memo:record.get('BmsCenOrderDtl_Memo')
		};
		var fields = [];
		for(var i in entity){
			fields.push(i);
		}
		
		var params = Ext.JSON.encode({
			entity:entity,
			fields:fields.join(',')
		});
		
		JShell.Server.post(url,params,function(data){
			if(data.success){
				me.saveCount++;
			}else{
				me.saveErrorCount++;
			}
			if(me.saveCount + me.saveErrorCount == me.saveLength){
				me.hideMask();//隐藏遮罩层
				if(me.saveErrorCount == 0){
					me.onSearch();
				}else{
					JShell.Msg.error("保存信息有误！");
				}
			}
		});
	}
});