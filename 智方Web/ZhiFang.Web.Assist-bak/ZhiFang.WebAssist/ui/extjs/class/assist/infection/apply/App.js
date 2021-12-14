/**
 * @description 环境监测送检样本登记--只按当前登录科室使用
 * @author longfc
 * @version 2020-11-09
 */
Ext.define('Shell.class.assist.infection.apply.App',{
    extend:'Shell.class.assist.infection.basic.App',
	
    title:'环境监测送检样本登记',
    
    afterRender:function(){
		var me = this;
		me.callParent(arguments);		
		me.onListenersLink();
		
		//如果科室信息为空,提示帐号绑定科室后再使用
		var deptId = JcallShell.System.Cookie.get(JcallShell.System.Cookie.map.DEPTID) || "";
		if(!deptId){
			me.onShowBindDept();
		}
	},
	initComponent:function(){
		var me = this;
		me.getRecordTypeItemList(function() {
			me.items = me.createItems();
		});
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
		 var width1=document.body.clientWidth * 0.98;
		 
		me.Grid = Ext.create('Shell.class.assist.infection.apply.Grid', {
			region: 'center',
			header: false,
			itemId: 'Grid',
			RecordTypeItemList:me.RecordTypeItemList
		});
		me.Form = Ext.create('Shell.class.assist.infection.apply.Form', {
			region: 'south',//north
			header: false,
			height: 295,
			width: width1,
			itemId: 'Form',
			split: true,
			collapsible: true,
			RecordTypeItemList:me.RecordTypeItemList,
			GridPanel:me.Grid//注入作条码打印使用
		});
		
		return [me.Grid,me.Form];
	},
	/**
	 * @description 加载表单信息
	 * @param {Object} record
	 */
	loadData:function(record){
		var me=this;
		var id=record.get(me.Grid.PKField);
		var statusID=""+record.get("GKSampleRequestForm_StatusID");
		var recordTypeId=""+record.get("GKSampleRequestForm_SCRecordType_Id");
		
		me.Form.SCRecordTypeId=recordTypeId;
		if(statusID=="0"){
			me.Form.isEdit(id);
		}else{
			me.Form.isShow(id);
		}
	},
	/**
	 * 绑定科室
	 * @param {Object} record
	 */
	onShowBindDept: function() {
		var me = this;
		
		var userNo = ""+JcallShell.System.Cookie.get(JcallShell.System.Cookie.map.USERID) || "";
		if(!userNo)return;
		
		var config = {
			resizable: true,
			title: '绑定科室',
			width:360,
			height: 280,
			PK: userNo,
			listeners: {
				close:function(){
					var deptId = JcallShell.System.Cookie.get(JcallShell.System.Cookie.map.DEPTID) || "";
					me.Grid.defaultWhere="gksamplerequestform.DeptId="+deptId;
					me.Grid.onSearch();
				}
			}
		};
		var win = JShell.Win.open('Shell.class.sysbase.puser.reg.BindDept', config);
		win.show();
	}
});
	