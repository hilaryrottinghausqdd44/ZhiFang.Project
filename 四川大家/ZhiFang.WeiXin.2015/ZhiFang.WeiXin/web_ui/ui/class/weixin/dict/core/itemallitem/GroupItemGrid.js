/**
 * 组套内项目
 * @author liangyl
 * @version 2018-02-01
 */
Ext.define('Shell.class.weixin.dict.core.itemallitem.GroupItemGrid', {
	extend: 'Shell.ux.grid.Panel',
	requires: ['Ext.ux.CheckColumn'],
	
	title: '组套内项目表 ',
	width: 800,
	height: 500,
	
  	/**获取数据服务路径*/
	selectUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_SearchGroupItemSubItemByPItemNo?isPlanish=true',
	/**修改服务地址*/
	editUrl:'/ServerWCF/WeiXinAppService.svc/ST_UDTO_UpdateBDoctorAccountByField',
	/**删除数据服务路径*/
	delUrl:'/ServerWCF/WeiXinAppService.svc/ST_UDTO_DelBDoctorAccount',
	/**默认加载*/
	defaultLoad: false,
	/**是否启用序号列*/
	hasRownumberer: false,
	/**带分页栏*/
	hasPagingtoolbar: false,
    LabCode:null,
    /**项目编号*/
	ItemNo:null,
	/**实验室id*/
    ClienteleID:null,
    /**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
	formtype:'edit',
	autoSelect: false,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		
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
		buttonToolbarItems.push({
	        xtype: 'label',
	        text: '组套内项目',
	        style: "font-weight:bold;color:blue;",
	        margin: '0 0 10 10'
		},'-',{text:'编辑',tooltip:'编辑',iconCls:'button-edit',
			itemId:'Edit',disabled:true,
			handler:function(){
				me.onEditClick();
			}
		},'-',{text:'删除',tooltip:'删除',iconCls:'button-del',
			itemId:'btnDel',disabled:true,
			handler:function(){
				me.onDelClick();
			}
		});
		return buttonToolbarItems;
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		
		var columns = [{
			text:'项目编号',dataIndex:'GroupItemVO_TestItem_Id',width:100,hidden:false,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'项目名称',dataIndex:'GroupItemVO_TestItem_CName',width:250,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'三甲价格',dataIndex:'GroupItemVO_TestItem_MarketPrice',width:100,
			hidden:true,sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'内部价格',dataIndex:'GroupItemVO_TestItem_GreatMasterPrice',width:100,
			hidden:true,sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'执行价格',dataIndex:'GroupItemVO_TestItem_Price',width:100,
			hidden:true,sortable:false,menuDisabled:true,defaultRenderer:true
		}];
		return columns;
	},
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick:function(){
		var me = this;
		
	},
	/**@overwrite 新增按钮点击处理方法*/
	onEditClick:function(){
		var me = this;	
		var maxWidth = document.body.clientWidth * 0.99;
		var height = document.body.clientHeight * 0.65; 
		JShell.Win.open('Shell.class.weixin.dict.core.itemallitem.comitem.ItemPanel', {
			SUB_WIN_NO:'1',//内部窗口编号
			resizable: false,
			formtype:me.formtype,
			/**项目编号*/
			ItemNo:me.ItemNo,
			/**实验室id*/
		    ClienteleID:me.ClienteleID,
		    minHeight:420,
		    height:height,
		    RecDatas:me.getRecDatas(),
			listeners: {
				onAcceptClick:function(p,list){
					me.store.removeAll();
					me.store.add(list);      
					me.fireEvent('update', me);
					p.close();
				}
			}
		}).show();
	},
	
	  /**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			arr = [];

		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.selectUrl;

		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true).join(',');
	    //根据TestItem编号
	    if(!me.ItemNo)return;
	    url +="&pitemNo="+me.ItemNo;
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
	},
	
	
	onSaveClick:function(){
		var me =this;
	},
	   /**获取明细信息*/
	getEntity:function(PItemNo){
		var me = this,
		records = me.store.data.items,
		len = records.length;
		var dtAddList=[];
		
		for(var i=0;i<len;i++){
		    var rec = records[i];
			var obj = {
				ItemNo:records[i].get('GroupItemVO_TestItem_Id')
			}
			if(PItemNo){
				obj.PItemNo=PItemNo;
			}
			dtAddList.push(obj);
		}
		return dtAddList;
	},
    /**启用所有的操作功能*/
	enableControl: function(bo) {
		var me = this,
			enable = bo === false ? false : true,
			toolbars = me.dockedItems.items || [],
			length = toolbars.length,
			items = [];

		for (var i = 0; i < length; i++) {
			if(toolbars[i].itemId!='Edit')continue;
			if (toolbars[i].xtype == 'header') continue;
			if (toolbars[i].isLocked) continue;
			var fields = toolbars[i].items.items;
			items = items.concat(fields);
		}

		var iLength = items.length;
		for (var i = 0; i < iLength; i++) {
			items[i][enable ? 'enable' : 'disable']();
		}
		if (bo) {
			me.defaultLoad = true;
		}
		
	},
		/**获取列表数据行*/
	getRecDatas:function(){
		var me = this,
		    records = me.store.data.items,
		    len = records.length;
		    
	    return records;
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
	}
});