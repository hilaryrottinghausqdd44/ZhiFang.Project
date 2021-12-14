/**
 * 组套内项目B_Lab_GroupItem
 * @author liangyl
 * @version 2018-02-01
 */
Ext.define('Shell.class.weixin.dict.lab.dictitem.comitem.GroupItemGrid', {
	extend: 'Shell.ux.grid.Panel',
	requires: ['Ext.ux.CheckColumn'],
	
	title: '组套内项目 ',
	width: 800,
	height: 500,
	
  	/**获取数据服务路径*/
	selectUrl:'/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_SearchBLabGroupItemSubItemByPItemNoAndLabCode?isPlanish=true',

	/**默认加载*/
	defaultLoad: false,
	/**是否启用序号列*/
	hasRownumberer: false,
	/**带分页栏*/
	hasPagingtoolbar: false,
   /**项目编号*/
	ItemNo:null,
	/**实验室id*/
    ClienteleID:null,
     /**默认每页数量*/
	defaultPageSize: 500,
	/**删除的数据*/
	delArr:[],
		/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
	RecDatas:[],
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
        if(me.RecDatas){
        	me.store.add(me.RecDatas);
        }
		me.on({
        	nodata:function(){
				me.getView().update('');
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.addEvents('save','onAcceptClick');
		//数据列
		me.columns = me.createGridColumns();
		//创建功能按钮栏Items
		me.buttonToolbarItems = me.createButtonToolbarItems();
		me.callParent(arguments);
	},
	/**创建功能按钮栏Items*/
	createButtonToolbarItems:function(){
		var me = this,
			buttonToolbarItems = [];
	    //查询框信息
		me.searchInfo = {width:145,emptyText:'编号',isLike:true,
			fields:['blabgroupitem.ItemNo']};
		buttonToolbarItems.push({
	        xtype: 'label',
	        text: '组套内项目',
	        style: "font-weight:bold;color:blue;",
	        margin: '0 0 10 10'
		},'-','del','-','Accept');
		return buttonToolbarItems;
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		var columns = [{
			text:'项目编号',dataIndex:'BLabGroupItemVO_BLabTestItemVO_ItemNo',width:100,hidden:false,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'项目名称',dataIndex:'BLabGroupItemVO_BLabTestItemVO_CName',width:120,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'三甲价格',dataIndex:'BLabGroupItemVO_BLabTestItemVO_MarketPrice',width:85,
			sortable:false,menuDisabled:true,renderer : function(value, meta, record, rowIndex, colIndex) {
				var v = Number(value) || 0;
                v = v.toFixed(4);
                v = Ext.isNumber(v) ? v : parseFloat(String(v).replace('.', '.'));
                v = isNaN(v) ? '' : String(v).replace('.', '.');
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		},{
			text:'内部价格',dataIndex:'BLabGroupItemVO_BLabTestItemVO_GreatMasterPrice',width:85,
			sortable:false,menuDisabled:true,
			renderer : function(value, meta, record, rowIndex, colIndex) {
				var v = Number(value) || 0;
                v = v.toFixed(4);
                v = Ext.isNumber(v) ? v : parseFloat(String(v).replace('.', '.'));
                v = isNaN(v) ? '' : String(v).replace('.', '.');
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		},{
			text:'执行价格',dataIndex:'BLabGroupItemVO_Price',width:85,
			sortable:false,menuDisabled:true,renderer : function(value, meta, record, rowIndex, colIndex) {
				var v = Number(value) || 0;
                v = v.toFixed(4);
                v = Ext.isNumber(v) ? v : parseFloat(String(v).replace('.', '.'));
                v = isNaN(v) ? '' : String(v).replace('.', '.');
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		},{
			text:'新增标记',dataIndex:'BLabGroupItemVO_BLabTestItemVO_Tag',width:100,
			hidden:true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'删除标记',dataIndex:'BLabGroupItemVO_BLabTestItemVO_DelTag',width:100,
			hidden:true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'主键ID',dataIndex:'BLabGroupItemVO_BLabTestItemVO_Id',isKey:true,hidden:true,hideable:false
		}];
		return columns;
	},
    /**删除按钮点击处理方法*/
	onDelClick: function() {
		var me = this,
			records = me.getSelectionModel().getSelection();
		if (records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
        for (var i in records) {
			me.store.remove(records[i]); 
		}
	},
	/**删除*/
	onDelSave:function(){
		var me = this;
		 //删除
	    for(var i=0;i<me.delArr.length;i++){
	    	if(me.delArr[i].get('BLabGroupItemVO_BLabTestItemVO_Id')){
	    		var id = me.delArr[i].get('BLabGroupItemVO_BLabTestItemVO_Id');
				me.delOneById(i, id);
	    	}
		}
	},
	/**获取列表数据行*/
	getAllRecDatas:function(){
		var me = this,
		    records = me.store.data.items,
		    len = records.length;
	    return records;
	},
	
	/**保存*/
	onSaveClick:function(){
		var me = this,
			records = me.store.getModifiedRecords(),//获取修改过的行记录
			len = records.length;
		if(len==0) return;
		me.showMask(me.saveText);//显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;
		for(var i=0;i<len;i++){
			//Tag=1 新增行
			if(records[i].get('BLabGroupItemVO_BLabTestItemVO_Tag')=='1'){
				me.AddOne(records[i]);
			}
		}
		if(me.saveCount + me.saveErrorCount == me.saveLength){
			me.hideMask();//隐藏遮罩层
			if(me.saveErrorCount == 0){
				me.fireEvent('save', me);
//				me.onSearch();
			}else{
				JShell.Msg.error("保存信息有误！");
			}
		}
	},
	onAcceptClick:function(){
		var me =this;
		var recs=me.getAllRecDatas();
    	me.fireEvent('onAcceptClick',me,recs);
	},
    /**新增信息*/
	AddOne:function(record){
		var me = this;
		var url = (me.addUrl.slice(0,4) == 'http' ? '' : JShell.System.Path.ROOT) + me.addUrl;
		
		var params = Ext.JSON.encode({
			entity:{
				PItemNo:me.ItemNo,
				CName:record.get('BLabGroupItemVO_BLabTestItemVO_CName'),
				Price:record.get('BLabGroupItemVO_BLabTestItemVO_Price'),
				ItemNo:record.get('BLabGroupItemVO_BLabTestItemVO_ItemNo'),
				LabCode:me.ClienteleID
			}
		});
		JShell.Server.post(url,params,function(data){
			if(data.success){
				me.saveCount++;
			}else{
				me.saveErrorCount++;
			}
		},false);
	},
	/**禁用所有的操作功能*/
	disableControl: function() {
		this.enableControl(true);
	},
	 /**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			arr = [];
	  
        //根据BLabTestItem编号
	    if(!me.ItemNo)return;
	    if(!me.ClienteleID)return;
		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.selectUrl;

		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true).join(',');
	    
	    url +='&pitemNo='+me.ItemNo+'&LabCode='+me.ClienteleID;
		//默认条件
		if (me.defaultWhere && me.defaultWhere != '') {
			arr.push(me.defaultWhere);
		}
		//内部条件
		if (me.internalWhere && me.internalWhere != '') {
			arr.push(me.internalWhere);
		}
		//外部条件
		if (me.externalWhere && me.externalWhere != '') {
			arr.push(me.externalWhere);
		}
		var where = arr.join(") and (");
		if (where) where = "(" + where + ")";
		if (where) {
			url += '&where=' + JShell.String.encode(where);
		}
		return url;
	}
});