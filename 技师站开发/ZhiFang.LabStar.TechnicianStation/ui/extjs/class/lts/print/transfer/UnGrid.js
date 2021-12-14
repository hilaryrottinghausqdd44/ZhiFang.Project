/**
 * 未选择的检验单
 * @author liangyl
 * @version 2019-12-06
 */
Ext.define('Shell.class.lts.print.transfer.UnGrid', {
	extend: 'Shell.ux.grid.Panel',
	title: '未选择的检验单',
	requires: ['Ext.ux.CheckColumn'],
	width: 800,
	height: 500,
    /**获取样本单数据服务路径*/
	selectUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_QueryLisTestFormAndItemNameList?isPlanish=true',
	/**显示成功信息*/
	showSuccessInfo: false,
	/**消息框消失时间*/
	hideTimes: 3000,
	/**默认加载*/
	defaultLoad: false,
	/**默认每页数量*/
	defaultPageSize:50,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,

	/**带分页栏*/
	hasPagingtoolbar: true,
	/**带功能按钮栏*/
	hasButtontoolbar:false,
	/**是否启用序号列*/
	hasRownumberer: false,
	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
//	defaultOrderBy:[{property:'LisTestForm_DataAddTime',direction:'DESC'}],
	/**小组*/
	SectionID: null,
	/**时间范围,样本范围*/
	sreachObj: {
		BeginDate: null,
		EndDate: null,
		EndSampleNo:null,
		StartSampleNo:null
	},
	/**状态*/
	MainStatusID: null,
	
	//默认排序字段
	defaultOrderBy:[
		{property:'LisTestForm_GTestDate',direction:'ASC'},
		{property:'LisTestForm_GSampleNoForOrder',direction:'ASC'}
	],
	//记录日期的排序类型
	dateDirection: 'ASC',

    //第一次加载数据
    firstLoad:true,
    
    //已选的检验单项目,用于填充颜色
	SelectList:[],
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
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
			text:'检验单ID',dataIndex:'LisTestForm_Id',isKey:true,hidden:true,hideable:false
		},{
			text:'检验日期',dataIndex:'LisTestForm_GTestDate',width:85,
			sortable:true,menuDisabled:true,
			renderer : function(value, meta, record, rowIndex, colIndex) {
				var v = JShell.Date.toString(value, true) || '';
				if (v) {
					meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
					var Tab = record.get('LisTestForm_Tab');
					if(Tab=='1')meta.style = 'background-color:#228B22;color:#ffffff;';
				}
				return v;
			}
		},{
			text:'样本号',dataIndex:'LisTestForm_GSampleNoForOrder',width:80,renderer:function(value,meta,record){
				var v = record.get('LisTestForm_GSampleNo'),
					tipText = v;
				meta.tdAttr = 'data-qtip="' + tipText + '"';
				return v;
			}
		},{
			text:'样本号排序',dataIndex:'LisTestForm_GSampleNo',width:150,hidden:true,renderer:function(value,meta,record){
				var v = record.get('LisTestForm_GSampleNoForOrder');
				meta.tdAttr = 'data-qtip="' + v + '"';
				return v;
			}
		},{
			text:'姓名',dataIndex:'LisTestForm_CName',width:80,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'条码号',dataIndex:'LisTestForm_BarCode',width:80,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'病历号',dataIndex:'LisTestForm_PatNo',width:80,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'样本类型',dataIndex:'LisTestForm_GSampleType',width:80,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'科室',dataIndex:'LisTestForm_LisPatient_DeptName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'已选择标记',dataIndex:'LisTestForm_Tab',width:100,hidden:true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'样本单项目',dataIndex:'LisTestForm_ItemNameList',minWidth:180,flex:1,
			sortable:false,menuDisabled:true,defaultRenderer:true
		}];
		
		return columns;
	},
	
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			params = [];

		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.selectUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true).join(',');
		 //样本号范围
		if(me.sreachObj.StartSampleNo)url+='&beginSampleNo='+me.sreachObj.StartSampleNo;
		if(me.sreachObj.EndSampleNo)url+='&endSampleNo='+me.sreachObj.EndSampleNo;
		//小组Id
		if(me.SectionID)params.push("listestform.LBSection.Id=" + me.SectionID + "");
        //状态
		if(me.MainStatusID)params.push("listestform.MainStatusID=" + me.MainStatusID + "");
		//时间范围
		if(me.sreachObj.BeginDate)params.push("listestform.GTestDate>='" + JShell.Date.toString(me.sreachObj.BeginDate,true) + "'");
		if(me.sreachObj.EndDate)params.push("listestform.GTestDate<'" + JShell.Date.toString(JShell.Date.getNextDate(me.sreachObj.EndDate),true) + "'");
		
		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
		var where = me.getLoadWhere(true);
		if (where) {
			url += '&where=' + where;
		}
		return url;
	},

	loadData : function(SectionID,obj,MainStatusID){
		var me = this;
		if(obj.EndSampleNo || obj.StartSampleNo){
			if(!obj.EndSampleNo || !obj.StartSampleNo){
				JShell.Msg.warning('请输入样本号范围');
				return;
			}
		}
		me.SectionID = SectionID;
		me.sreachObj = obj;
	
		me.MainStatusID = MainStatusID;
		me.onSearch();
	},
	//加载数据前
	onBeforeLoad: function (m, operation){
		var me = this,
			where = [];
		
		//排序：检验日期+样本号
		if (!me.firstLoad) {
			me.store.proxy.extraParams = {};
			var first = me.store.sorters.first();
			if(first && (first.property == 'LisTestForm_GTestDate' || first.property == 'LisTestForm_GSampleNoForOrder')){
				//me.store.sorters.removeAll(me.store.sorters.items);
				if (first.property == 'LisTestForm_GTestDate') me.dateDirection = first.direction;
				me.store.sorters.addAll([
					{ property: 'LisTestForm_GTestDate', direction: me.dateDirection },
					{ property: 'LisTestForm_GSampleNoForOrder', direction: first.direction }
				]);

				//点击检验日期，顺序 检验日期顺序 + 样本号顺序
				//逆序 检验日期逆序 + 样本号逆序

				//点击样本号： 顺序 检验日期(原本的排序保持) + 样本号顺序
				//逆序 检验日期(原本的排序保持) + 样本号逆序

				//设置外部自定义参数排序  -- 替换默认排序
				me.store.proxy.extraParams = {//一个带有属性值的对象，用来给每一个使用当前对象发出的请求附加额外的参数。 Session信息以及你需要在每个请求中传递的其它信息一般都会放在这里
					sort: JSON.stringify([
						{ property: 'LisTestForm_GTestDate', direction: me.dateDirection },
						{ property: 'LisTestForm_GSampleNoForOrder', direction: first.direction }
					])
				};
			}
		}else{
			me.firstLoad = false;
		}
		me.callParent(arguments);
	},
	/**@overwrite 改变返回的数据*/
	changeResult: function(data) {
		var me = this;
		var len = me.SelectList.length;
		for(var i = 0; i < data.list.length; i++) {
			for(var j=0;j<len;j++ ){
				if(me.SelectList[j].data.LisTestForm_Id == data.list[i].LisTestForm_Id){
					data.list[i]["LisTestForm_Tab"] = '1';
					break;
				}
			}
		}
		return data;
	},
	//记录已选择检验单列表数据
	getSelectList:function(list){
		var me = this;
		me.SelectList=list;
	}
});