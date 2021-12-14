/**
 * 样本合并
 * @author liangyl
 * @version 2019-11-20
 */
Ext.define('Shell.class.lts.merge.Panel',{
    extend:'Shell.ux.panel.AppPanel',
    title:'样本合并',
    
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		me.ComSampleGrid.on({
			itemclick:function(v, record) {
				JShell.Action.delay(function(){
					me.loadSearch(record);
				},null,200);
			},
			select:function(RowModel, record){
				JShell.Action.delay(function(){
					me.loadSearch(record);
				},null,200);
			},
			cleardata:function(){
				me.ResultGrid.clearData();
			}
		});
	},
	initComponent:function(){
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
		
	    me.ComSampleGrid = Ext.create('Shell.class.lts.merge.ComSampleGrid', {
			itemId: 'ComSampleGrid',
			region: 'north',
			height: 180,
			split: false,
			padding: '0px 0px 1px 0px',
			collapsible: false,
			title:"确认合并样本",
			collapseMode: 'mini'
		});
		me.ResultGrid = Ext.create('Shell.class.lts.merge.ResultGrid', {
			title: '项目结果源',
			itemId: 'DtlGrid',
			region: 'center'
		});
		return [me.ComSampleGrid,me.ResultGrid];
	},
	onSearch : function(obj){
		var me = this;
		me.ComSampleGrid.onSearch(obj);
	},
	/**项目结果源数据加载*/
	loadSearch : function(record){
		var me = this;
		//源样本单ID
		var Id = record.get('LisTestForm_Id');
		//源样本单检验日期
		var GTestDate = record.get('LisTestForm_GTestDate');
		//目标样本单ID
		var DId =record.get('LisTestForm_DId');
		//目标样本单ID
		var DGTestDate = record.get('LisTestForm_DGTestDate');
		me.ResultGrid.onSearch(Id,GTestDate,DId,DGTestDate);
	},
	/**删除源样本校验
	 * 源样本：源样本可以是MainStatusID>=0,=0时才能删除源样本
	 * */
	isVerify : function(){
		var me = this,isExec =true;
		var recs = me.ComSampleGrid.store.data.items;
		if(recs.length==0) return false;
		for(var i=0;i<recs.length;i++){
			var MainStatusID =recs[i].get('LisTestForm_MainStatusID'); 
			if(!MainStatusID)MainStatusID=0;
			MainStatusID = Number(MainStatusID);
		   if(MainStatusID != 0){
				JShell.Msg.alert('源样本主单状态不能删除!');
				isExec = false;
				break
			}
		}
		return isExec;
	},
	/**获取保存的参数
	 * */
	getParams : function(){
		var me = this;
		var Params ={};
		//获取确认合并样本选择行
		var res = me.ComSampleGrid.getSelectionModel().getSelection();
		if (res.length == 0) return;
		//源样本单ID
		var fromTestFormID = res[0].get('LisTestForm_Id');
		//目标样本单信息
		var toTestForm = {};
		if (res[0].get('LisTestForm_DId')) toTestForm.Id = res[0].get('LisTestForm_DId');//目标样本单ID
		if (res[0].get('LisTestForm_DGSampleNo')) toTestForm.GSampleNo = res[0].get('LisTestForm_DGSampleNo');//目标样本单样本号
		if (res[0].get('LisTestForm_DGTestDate')) toTestForm.GTestDate = JShell.Date.toServerDate(res[0].get('LisTestForm_DGTestDate'));//目标样本单检验日期
		if (res[0].get('LisTestForm_DSectionID')) toTestForm.LBSection = { Id: res[0].get('LisTestForm_DSectionID'), DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0]}; //目标样本单条码号
		if (res[0].get('LisTestForm_LisPatient_DId')) toTestForm.LisPatient = { Id: res[0].get('LisTestForm_LisPatient_DId'), DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0] };//就诊信息Id
		if (res[0].get('LisTestForm_DCName')) toTestForm.CName = res[0].get('LisTestForm_DCName');//目标样本单姓名
		if (res[0].get('LisTestForm_DPatNo')) toTestForm.PatNo = res[0].get('LisTestForm_DPatNo');//目标样本单病历号
		if (res[0].get('LisTestForm_DBarCode')) toTestForm.BarCode = res[0].get('LisTestForm_DBarCode');//目标样本单条码号
		if (res[0].get('LisTestForm_DMainStatusID') != "") toTestForm.MainStatusID = res[0].get('LisTestForm_DMainStatusID');//目标样本单主状态
		
		Params.FromTestFormID = fromTestFormID;
		Params.toTestForm = toTestForm;
		
		var strFromTestItemID = "";
		//样本单项目LisTestItem_Id
		var records = me.ResultGrid.getSelectionModel().getSelection();
		if (records.length >0) {
			for(var i=0;i<records.length;i++){
				if(i>0)strFromTestItemID+=",";
				strFromTestItemID+=records[i].get('LisTestItem_Id');
		    }
		}
		Params.StrFromTestItemID = strFromTestItemID;
		return Params;
	},
	clearData : function(){
		var me = this;
		me.ComSampleGrid.clearData();
		me.ResultGrid.clearData();
	}
});