/**
 * 项目结果源
 * @author liangyl	
 * @version 2019-11-19
 */
Ext.define('Shell.class.lts.merge.ResultGrid', {
	extend: 'Shell.ux.grid.Panel',	
	requires: ['Ext.ux.CheckColumn'],
	title: '项目结果源 ',
	width: 800,
	height: 500,
	
    /**获取样本单项目数据服务路径*/
	selectUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_QueryLisTestItemByHQL?isPlanish=true',
	/**显示成功信息*/
	showSuccessInfo: false,
	/**消息框消失时间*/
	hideTimes: 3000,
	/**默认加载*/
	defaultLoad: false,
	/**默认每页数量*/
	defaultPageSize:1000,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**后台排序*/
	remoteSort: false,
	/**带分页栏*/
	hasPagingtoolbar: false,
	/**带功能按钮栏*/
	hasButtontoolbar:false,
	/**是否启用序号列*/
	hasRownumberer: true,
	/**开启加载数据遮罩层*/
	hasLoadMask: true,
	defaultOrderBy:[{property:'LisTestItem_DispOrder',direction:'ASC'}],
    /**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
        var items = me.headerCt.items.items;
        for(var i=0;i<items.length;i++){
        	var dataIndex = items[i].initialConfig.dataIndex ;
        	if(dataIndex == 'LisTestItem_ReportValue' || dataIndex =="IsExist"){
        		items[i].el.setStyle("background", '#F08080');
        	}
        }
      
	},
	initComponent: function() {
		var me = this;
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var columns = [{
			text:'项目编号',dataIndex:'LisTestItem_LBItem_Id',width:100,hidden:true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'项目名称',dataIndex:'LisTestItem_LBItem_CName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'源项目值',dataIndex:'LisTestItem_ReportValue',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'→',dataIndex:'Direction',width:40,align:'center',
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'目标项目编号',dataIndex:'LisTestItem_DLBItem_Id',width:100,hidden:true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'目标项目值',dataIndex:'LisTestItem_DReportValue',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'存在',dataIndex:'IsExist',width:50,
			sortable:false,menuDisabled:true,
			isBool: true,align: 'center',type: 'bool',defaultRenderer:true
		},{
			text:'检验单项目结果ID',dataIndex:'LisTestItem_Id',isKey:true,hidden:true,hideable:false
		},{
			text:'目标检验单结果ID',dataIndex:'LisTestItem_DId',hidden:true,hideable:false
		}];
		return columns;
	},
	onSearch : function(TestFormID,GTestDate,DTestFormID,DGTestDate){
		var me =  this;
		me.hideMask();
		me.store.removeAll();
		//源样本项目
		var ItemList=[];
		//目标样本项目
		var DItemList=[];		
		//源样本项目
		me.getItemInfo(TestFormID,GTestDate,function(data){
			if(data && data.value)ItemList = data.value.list;
		});
		//目标样本项目
		if(DTestFormID){
			 me.getItemInfo(DTestFormID,DGTestDate,function(data){
				if(data && data.value)DItemList = data.value.list;
			});
		}
		for(var i= 0;i<ItemList.length;i++){
			var ItemID =ItemList[i].LisTestItem_LBItem_Id;
			for(var j = 0;j<DItemList.length;j++){
				if(ItemID == DItemList[j].LisTestItem_LBItem_Id){
					var obj = {
						LisTestItem_DReportValue:DItemList[j].LisTestItem_ReportValue,
						LisTestItem_DId:DItemList[j].LisTestItem_Id,
						LisTestItem_DLBItem_Id : DItemList[j].LisTestItem_LBItem_Id,
						IsExist:'1'
					} 
					Object.assign(ItemList[i],obj);
					DItemList.splice(j,1);
					break;
				}
			}
			me.store.add(ItemList[i]);
		}
		for (var i = 0; i <me.store.data.items.length; i++){  
            me.getSelectionModel().select(i,true,false);      
        }
	},
	/**根据样本单ID，检验日期,得到样本单项目*/
	getItemInfo: function(TestFormID,GTestDate,callback) {
		var me = this,
			url = (me.selectUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.selectUrl;

		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=LisTestItem_LBItem_Id,LisTestItem_LBItem_CName,LisTestItem_ReportValue,LisTestItem_Id';
        url +="&where=listestitem.LisTestForm.Id="+TestFormID+" and listestitem.GTestDate='"+GTestDate+"'";
//		url +="&sort="+Ext.encode(me.defaultOrderBy);
		JcallShell.Server.get(url, function(data) {
			if(data.success) {
				callback(data);
			} 
		},false);
	}
	
});