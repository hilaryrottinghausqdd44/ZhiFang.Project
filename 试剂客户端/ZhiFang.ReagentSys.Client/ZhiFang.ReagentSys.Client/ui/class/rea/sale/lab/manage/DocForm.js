/**
 * 供货总单表单-实验室专用
 * @author Jcall
 * @version 2017-08-09
 */
Ext.define('Shell.class.rea.sale.lab.manage.DocForm', {
	extend: 'Shell.class.rea.sale.basic.DocForm',
	
	
	createItems:function(){
		var me = this,
			items = me.callParent(arguments),
			len = items.length;
			
		for(var i=0;i<len;i++){
			if(items[i].itemId == 'BmsCenSaleDoc_Lab_CName'){
				items[i] = {
					fieldLabel:'订货方',
					name:'BmsCenSaleDoc_Lab_CName',
					itemId:'BmsCenSaleDoc_Lab_CName',
					readOnly:true,locked:true
				};
			}else if(items[i].itemId == 'BmsCenSaleDoc_Comp_CName'){
				items[i] = {
					fieldLabel:'供货方',emptyText:'必填项',allowBlank:false,
					name:'BmsCenSaleDoc_Comp_CName',itemId:'BmsCenSaleDoc_Comp_CName',
					xtype:'trigger',triggerCls:'x-form-search-trigger',
					enableKeyEvents:false,editable:false,
					onTriggerClick:function(){
						JShell.Win.open('Shell.class.rea.cenorgcondition.ParentCheckGrid',{
							resizable:false,
							CenOrgId:JShell.REA.System.CENORG_ID,
							listeners:{
								accept:function(p,record){me.onCompAccept(record);p.close();}
							}
						}).show();
					}
				};
			}
		}
		
		return items;
	},
	/**供货方选择*/
	onCompAccept:function(record){
		var me = this;
		var ComId = me.getComponent('BmsCenSaleDoc_Comp_Id');
		var ComName = me.getComponent('BmsCenSaleDoc_Comp_CName');
		
		ComId.setValue(record.get('CenOrgCondition_cenorg1_Id') || '');
		ComName.setValue(record.get('CenOrgCondition_cenorg1_CName') || '');
	},
	/**新增初始化信息*/
	initAddInfo:function(){
		var me = this;
		var BmsCenSaleDoc_UserID = me.getComponent('BmsCenSaleDoc_UserID');
		var BmsCenSaleDoc_UserName = me.getComponent('BmsCenSaleDoc_UserName');
		BmsCenSaleDoc_UserID.setValue(JShell.System.Cookie.get(JShell.System.Cookie.map.USERID));
		BmsCenSaleDoc_UserName.setValue(JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME));
		
		var BmsCenSaleDoc_Lab_Id = me.getComponent('BmsCenSaleDoc_Lab_Id');
		var BmsCenSaleDoc_Lab_CName = me.getComponent('BmsCenSaleDoc_Lab_CName');
		BmsCenSaleDoc_Lab_Id.setValue(JShell.REA.System.CENORG_ID);
		BmsCenSaleDoc_Lab_CName.setValue(JShell.REA.System.CENORG_NAME);
		
		var BmsCenSaleDoc_OperDate = me.getComponent('BmsCenSaleDoc_OperDate');
		BmsCenSaleDoc_OperDate.setValue(new Date());
	}
});