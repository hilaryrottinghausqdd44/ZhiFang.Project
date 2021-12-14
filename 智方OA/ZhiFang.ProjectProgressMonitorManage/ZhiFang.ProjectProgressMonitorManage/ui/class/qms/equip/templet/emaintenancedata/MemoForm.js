/**
 * 备注
 * @author liangyl
 * @version 2016-08-25
 */
Ext.define('Shell.class.qms.equip.templet.emaintenancedata.MemoForm', {
	extend: 'Shell.ux.form.Panel',
	layout: 'fit',
//	height: 300,
//	width: 440,
	bodyPadding:'10px 10px 10px 10px',
	title: "仪器模板信息",
	formtype: 'edit',
	  /**主键字段*/
	PKField:'EMaintenanceData_Id',
	/**数据主键*/
	PK:'',
		/**查询对象*/
	objectEName: 'EMaintenanceData',
	/**获取数据服务路径*/
	selectUrl: '/QMSReport.svc/QMS_UDTO_SearchEMaintenanceData',
	/**新增服务地址*/
	addUrl: '/QMSReport.svc/ST_UDTO_AddEMaintenanceData',
	/**编辑服务地址*/
	editUrl: '/QMSReport.svc/ST_UDTO_UpdateEMaintenanceDataByField',
	/**删除服务地址*/
	delUrl: '/QMSReport.svc/ST_UDTO_DelEMaintenanceData',
	/**显示成功信息*/
	showSuccessInfo: false,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.isEdit();
	},
	initComponent: function() {
		var me = this;
	    me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
		return [ {
			fieldLabel: '编号',
			name: 'EMaintenanceData_Id',
			hidden:true,itemId: 'EMaintenanceData_Id'
		},{
			fieldLabel: '',
			emptyText: '备注',
			name: 'EMaintenanceData_ItemMemo',
			itemId: 'EMaintenanceData_ItemMemo',
			minHeight:60,hidden:false,
		    labelSeparator: '',
			colspan :1,
			width : me.defaults.width * 1,
			xtype:'textarea'
		}];
	},
	/**保存按钮点击处理方法*/
	onSaveClick:function(entity,Id,formtype){
		var me = this;
		if(!me.getForm().isValid()) return;
		var url = formtype == 'add' ? me.addUrl : me.editUrl;
		url = JShell.System.Path.getRootUrl(url);
		var params= {entity:entity};
		if(formtype=='edit' && Id!=''){
			var fields = ['ItemMemo','Id'];
			params.entity.Id = Id;
			params.fields = fields.join(',');
		}
        if(!params) return;
		var id = params.entity.Id;
		params = Ext.JSON.encode(params);
		me.showMask(me.saveText);//显示遮罩层
		me.fireEvent('beforesave',me);
		JShell.Server.post(url,params,function(data){
			me.hideMask();//隐藏遮罩层
			if(data.success){
				id = me.formtype == 'add' ? data.value : id;
				if(Ext.typeOf(id) == 'object'){
					id = id.id;
				}
				me.fireEvent('save',me,id);
				if(me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT,null,me.hideTimes);
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	},
   	/**更改标题*/
	changeTitle:function(){
		var me = this;
	},
	/**获取到备注信息*/
	getMemoInfo:function(){
		var me = this,val=null;
		var ItemMemo = me.getComponent('EMaintenanceData_ItemMemo');
		val=ItemMemo.getValue();
        return val;
	},
	/**备注保存*/
	SaveFormData: function(TempletID,TempletTypeCode,operatedate,isTbAdd,BatchNumber) {
		var me = this;
		var EMaintenanceData_Id = me.getComponent('EMaintenanceData_Id');
		var ItemMemo = me.getComponent('EMaintenanceData_ItemMemo');
        var OperateTime = JcallShell.Date.toString(operatedate);
		var entity = {
			TempletTypeCode: TempletTypeCode,
			TempletDataType: 1,
			ItemMemo: ItemMemo.getValue(),
			ItemDate: JShell.Date.toServerDate(operatedate),
			OperateTime:JShell.Date.toServerDate(OperateTime)
		}
		//当前是列表类型时,使用TB区分
	    if(TempletTypeCode.Length == 2 && TempletTypeCode.substr(0, 1).toUpperCase() == "T"){
	    	TempletTypeCode='TB';
	    }
		if(TempletTypeCode=='TB' &&  (isTbAdd==1 || isTbAdd=='1')){
			entity.BatchNumber=BatchNumber;
		}
		if(TempletID != '') {
			entity.ETemplet = {
				Id: TempletID,
				DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0]
			};
		}
		if(EMaintenanceData_Id.getValue()){
			entity.Id=EMaintenanceData_Id.getValue();
		}
		//默认员工ID
		var UserID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		//默认员工名称
		var UserName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);

		if(UserID != '') {
			entity.HREmployee = {
				Id: UserID,
				CName:UserName,
				DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0]
			};
		}
		return entity;
//		me.isAdd();
//		me.onSaveClick(entity, null, 'add');
	},
	/**@overwrite 重置按钮点击处理方法*/
	onResetClick:function(){
		var me = this;
		this.getForm().reset();
	},
	/**还原备注 */
	getItemMemo: function(ItemMemoObj) {
		var me = this;
		var EMaintenanceData_Id = me.getComponent('EMaintenanceData_Id');
		var EMaintenanceData_ItemMemo = me.getComponent('EMaintenanceData_ItemMemo');
		if(ItemMemoObj.Id != '') {
			EMaintenanceData_Id.setValue(ItemMemoObj.Id);
			EMaintenanceData_ItemMemo.setValue(ItemMemoObj.ItemMemo);
		} else {
			EMaintenanceData_Id.setValue('');
			EMaintenanceData_ItemMemo.setValue('');
		}
		me.isEdit();
	},

	/**清空备份信息*/
	clearMemoData: function() {
		var me = this;
		var obj = {
			Id: '',
			ItemMemo: ''
		};
		me.getItemMemo(obj);
	},
	/**只还原结果值，不还原id*/
	onSetResult:function(val){
		var me =this;
		var ItemMemo = me.getComponent('EMaintenanceData_ItemMemo');
		ItemMemo.setValue(val);
	},
	/**只还原ID*/
	onSetIdVal:function(val){
		var me =this;
		var Id = me.getComponent('EMaintenanceData_Id');
		Id.setValue(val);
	},
	   /**还原备注 */
	setItemMemoValue: function(ItemMemoObj) {
		var me = this;
		var EMaintenanceData_Id = me.getComponent('EMaintenanceData_Id');
		var EMaintenanceData_ItemMemo = me.getComponent('EMaintenanceData_ItemMemo');
		if(ItemMemoObj.Id != '') {
			EMaintenanceData_Id.setValue(ItemMemoObj.Id);
		} 
		if(ItemMemoObj.ItemMemo != '') {
			EMaintenanceData_ItemMemo.setValue(ItemMemoObj.ItemMemo);
		} 
	}
});