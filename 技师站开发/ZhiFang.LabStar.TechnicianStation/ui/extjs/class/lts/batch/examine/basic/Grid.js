/**
 *检验单
 * @author liangyl
 * @version 2021-03-26
 */
Ext.define('Shell.class.lts.batch.examine.basic.Grid',{
    extend:'Shell.class.lts.batch.Grid',
    requires: ['Ext.ux.CheckColumn'],
    title:'检验单',
    width:810,
    height:500,
    //获取数据服务路径
    selectUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_QueryWillConfirmLisTestForm?isPlanish=true',
	//默认加载
	defaultLoad:false,
	//小组ID
	SectionID:'',
		/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**带功能按钮栏*/
	hasButtontoolbar: false,
	
	/**默认每页数量*/
	defaultPageSize: 510,
	/**带分页栏*/
	hasPagingtoolbar: false,
	//查询条件
	whereObj:{
    	ZFSysCheckStatus:false,//全部——智能审核成功样本
        TestStatus:false,//全部——检验完成样本
        SickTypeID:null,//就诊类型
        StartDate:"",//检验日期开始日期
        EndDate:"",//检验日期结束日期
        DeptID:null,//送检科室
        beginSampleNo:"",//开始样本号        
        endSampleNo:"",//结束样本号
        itemResultFlag:""//无阳性,无异常,无HH/LL
	},
	
	//复选框
	multiSelect:true,
	selType:'checkboxmodel',
	
	defaultOrderBy:[{property:'LisTestForm_GSampleNoForOrder',direction:'ASC'}],
	
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
		me.store.on({
			load :function(store){
				me.onSelect(true);
			}
		});
	},
	initComponent:function(){
		var me = this;
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	//创建数据列
	createGridColumns:function(){
		var me = this;
		var columns = [{
			text:'检验单ID',dataIndex:'LisTestForm_Id',isKey:true,hidden:true,hideable:false
		},{
			text:'检验日期',dataIndex:'LisTestForm_GTestDate',width:85,
			sortable:false,menuDisabled:true,isDate:true,defaultRenderer:true
		},{
			text:'样本号',dataIndex:'LisTestForm_GSampleNo',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'条码号',dataIndex:'LisTestForm_BarCode',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'姓名',dataIndex:'LisTestForm_CName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'状态',dataIndex:'LisTestForm_MainStatusID',width:60,hidden:false,
			sortable:false,menuDisabled:true,
			renderer: me.onStatusRenderer
		}];
		
		return columns;
	},
	//数据加载
	loadData : function(obj){
		var me= this;
		me.whereObj = obj;
		me.onSearch();
	},
//	/**获取带查询参数的URL*/
//	getLoadUrl: function() {
//		var me = this,
//			params = [];
//
//		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.selectUrl;
//		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true).join(',');
//		
//		//样本号范围
//		if(me.whereObj.beginSampleNo)url+='&beginSampleNo='+me.whereObj.beginSampleNo;
//		if(me.whereObj.endSampleNo)url+='&endSampleNo='+me.whereObj.endSampleNo;
//		url+='&itemResultFlag='+me.whereObj.itemResultFlag;
//		//小组ID
//		if(me.SectionID) {
//			params.push("listestform.LBSection.Id=" + me.SectionID + "");
//		}
//		if(me.whereObj.StartDate)params.push("listestform.GTestDate>='" + me.whereObj.StartDate + "'");
// 		if(me.whereObj.EndDate)params.push("listestform.GTestDate<='" + me.whereObj.EndDate + "'");
//      //科室
//      if(me.whereObj.DeptID)params.push("listestform.DeptID=" + me.whereObj.DeptID + "");
//      //就诊类型
//      if(me.whereObj.SickTypeID)params.push("listestform.SickTypeID=" + me.whereObj.SickTypeID + "");
//      //全部—智能审核成功样本
//      if(me.whereObj.ZFSysCheckStatus)params.push("listestform.ZFSysCheckStatus=1");
//      //全部—检验完成样本
//      if(me.whereObj.TestStatus)params.push("(listestform.TestAllStatus=1 and listestform.FormInfoStatus=1)");
//
//      if(params.length > 0) {
//			me.internalWhere = params.join(' and ');
//		} else {
//			me.internalWhere = '';
//		}
//		var where = me.getLoadWhere(true);
//		if (where) {
//			url += '&where=' + where;
//		}
//		return url;
//	},
	//获取检验单ID列表
	getIdList : function(){
		var me = this,
		    testFormIDList=[], 
		    records = me.getSelectionModel().getSelection();
    	for(var i =0;i<records.length;i++){
    		testFormIDList.push(records[i].data.LisTestForm_Id);
    	}
    	return testFormIDList.join(',');
	},
	/**@overwrite 改变返回的数据*/
	changeResult: function(data) {
		var me = this;
		//超过500条的情况下，提示用户，只显示500条
		if(data.count>500){
			JShell.Msg.warning("数据量过大,最多只能显示500条,请重新选择查询条件");
			data.count =0;
			data.list=[];
		}
		return data;
	},
	//默认全选
	onSelect : function(bo){
		var me = this,
		     recs = me.store.data.items;
	    for(var i=0;i<recs.length;i++){
	    	if(bo)me.getSelectionModel().select(i,true,false);  
	    	else 
	    	    me.getSelectionModel().deselectAll(true);  
	    }
	}
});