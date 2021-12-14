/**
 * 项目分类列表
 * @author Jcall
 * @version 2016-12-28
 */
Ext.define('Shell.class.weixin.item.type.Grid', {
	extend: 'Shell.ux.grid.IsUseGrid',
	title: '项目分类列表',
	width: 1200,
	height: 600,

	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/DictionaryService.svc/ST_UDTO_SearchOSItemProductClassTreeAndChildTreeByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl: '/ServerWCF/DictionaryService.svc/ST_UDTO_UpdateOSItemProductClassTreeByField',
	/**删除数据服务路径*/
	delUrl: '/ServerWCF/DictionaryService.svc/ST_UDTO_DelOSItemProductClassTree',
   	/**获取区域服务路径*/
    areaUrl: '/ServerWCF/DictionaryService.svc/ST_UDTO_SearchClientEleAreaByHQL?isPlanish=true',
	/**获取产品分类关系服务路径*/
	selectUrl2: '/ServerWCF/DictionaryService.svc/ST_UDTO_SearchOSItemProductClassTreeLinkByHQL?isPlanish=true',
	
	/**是否使用字段名*/
    IsUseField:'OSItemProductClassTree_IsUse',
    /**是否启用修改按钮*/
	hasEdit:true,
	/**默认加载*/
	defaultLoad: false,
	/**是否启用序号列*/
	hasRownumberer: false,
	 /**父检验项目分类ID*/
	ParentID:0,
	/**父检验项目分类名称*/
	ParentName:'',
	/**父检验项目分类名称(修改）)*/
	editParentName:null,
	 /**区域ID*/
	AreaID:0,
	/**区域名称*/
	AreaName:'',
	
    AreaList:[],
    AreaEnum:null,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.getAreaInfo();
		
		me.addEvents('addclick');
        //创建功能按钮栏Items
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();

		me.callParent(arguments);
	},
	/**创建功能按钮栏Items*/
	createButtonToolbarItems:function(){
		var me = this,
			buttonToolbarItems = me.buttonToolbarItems || [];
			
		//查询框信息
		me.searchInfo = {
			width: 145,emptyText: '名称',isLike: true,
			itemId: 'search',fields: ['ositemproductclasstree.CName']
		};
		buttonToolbarItems.push('refresh','-','add','edit',
		{text:'删除',tooltip:'删除',iconCls:'button-del',itemId: 'del',
		handler: function() {
			me.onDelClick();
		   }
		},'save');
		buttonToolbarItems.push('-',{
			boxLabel: '本节点',itemId: 'checkBDictTreeId',checked: false,value: false,inputValue: false,xtype: 'checkbox',
				style: {
					marginRight: '8px'
				},
				listeners: {
					change: function(com, newValue, oldValue, eOpts) {
						me.onSearch();
					}
				}
		});
		buttonToolbarItems.push('->',{
			type: 'search',
			info: me.searchInfo
		});
		return buttonToolbarItems;
	},
	changeShowType: function(value) {
		var me = this;
		me.DeptTypeModel = value ? false : true;
		me.onSearch();
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		
		var columns = [{
			text:'区域',dataIndex:'OSItemProductClassTree_AreaID',width:100,
			sortable:false,menuDisabled:true,renderer: function(value, meta) {
				var v = value;
				if(me.AreaEnum != null){
					v = me.AreaEnum[value];
				}
				return v;
			}
		},{
			text:'名称',dataIndex:'OSItemProductClassTree_CName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'简称',dataIndex:'OSItemProductClassTree_SName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'快捷码',dataIndex:'OSItemProductClassTree_Shortcode',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			xtype:'checkcolumn',text:'使用',dataIndex:'OSItemProductClassTree_IsUse',
			width:40,align:'center',sortable:false,menuDisabled:true,
			stopSelection:false,type:'boolean'
		},{
			text:'次序',dataIndex:'OSItemProductClassTree_DispOrder',
			width:40,align:'center',sortable:false,menuDisabled:true
		},{
			text:'leaf',dataIndex:'OSItemProductClassTree_IsLeaf',hidden:true,
			width:40,align:'center',sortable:false,menuDisabled:true
		},{
			text:'PItemProductClassTreeID',dataIndex:'OSItemProductClassTree_PItemProductClassTreeID',hidden:true,
			width:40,align:'center',sortable:false,menuDisabled:true
		},{
			text:'主键ID',dataIndex:'OSItemProductClassTree_Id',isKey:true,hidden:true,hideable:false
		}]
		
		return columns;
	},
	/**根据父节点ID查询列表*/
	loadByParentId: function(id) {
		var me = this;
		var parentId = id == null ? -1 : id;
		me.defaultWhere = "ositemproductclasstree.PItemProductClassTreeID=" + parentId;
		me.onSearch();
	},
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick:function(){
		var me = this;
		
		JShell.Win.open('Shell.class.weixin.item.type.Form', {
//			SUB_WIN_NO:'1',//内部窗口编号
			resizable: false,
			formtype:'add',
			 /**父检验项目分类ID*/
			ParentID:me.ParentID,
			/**父检验项目分类名称*/
			ParentName:me.ParentName,
			 /**区域ID*/
			AreaID:me.AreaID,
			/**区域名称*/
			AreaName:me.AreaName,
			listeners: {
				save: function(p,id) {
					me.fireEvent('save',me);
					p.close();
					me.onSearch();
				}
			}
		}).show();
	},
	/**@overwrite 修改按钮点击处理方法*/
	onEditClick:function(){
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(!records || records.length != 1){
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		
		var id = records[0].get(me.PKField);
		me.openEditForm(id,records[0]);
	},
	/**打开修改页面*/
	openEditForm:function(id,rec){
		var me = this;
		JShell.Win.open('Shell.class.weixin.item.type.Form', {
//			SUB_WIN_NO:'2',//内部窗口编号
			resizable: false,
			formtype:'edit',
			PK:id,
		    /**父检验项目分类ID*/
			ParentID:rec.get('OSItemProductClassTree_Id'),
			/**父检验项目分类名称*/
			editParentName:me.editParentName,
            AreaEnum:me.AreaEnum,
            AreaList:me.AreaList,
			listeners: {
				save: function(p,id) {
					p.close();
					me.onSearch();
				}
			}
		}).show();
	},
	/**删除按钮点击处理方法*/
	onDelClick: function() {
		var me = this,
			records = me.getSelectionModel().getSelection();

		if (records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		var isExect=true,isExist=false;
		var	len = records.length;
		var ids='';
		for(var i=0;i<len;i++){
			if(i>0){
				ids+=','+records[i].get('OSItemProductClassTree_Id');
			}else{
				ids+=records[i].get('OSItemProductClassTree_Id');
			}
			var IsLeaf = records[i].get('OSItemProductClassTree_IsLeaf') + '';
			if(IsLeaf == 'false'){
				isExect = false;
			}
		}
		if(isExect!=true){
			JShell.Msg.error('只能删除叶子节点');
			return;
		}
		me.getOSItemProductClassTreeLinkfo(ids,function(data){
			if(data && data.value ){
				if(data.value.list.length>0){
					isExist=true;
				}
			}
		});
		if(isExist){
			JShell.Msg.error('已维护检测项目产品分类树关系,不能删除');
			return;
		}
		JShell.Msg.del(function(but) {
			if (but != "ok") return;

			me.delErrorCount = 0;
			me.delCount = 0;
			me.delLength = records.length;

			me.showMask(me.delText); //显示遮罩层
			for (var i in records) {
				var id = records[i].get(me.PKField);
				me.delOneById(i, id);
			}
		});
	},
	
	/**根据父节点ID查询列表*/
	loadParentById: function(id,AreaID) {
		var me = this;
		var parentId = id == null ? 0 : id;
		var where="ositemproductclasstree.PItemProductClassTreeID=" + parentId;
		if(AreaID ){
			where = where+ " and ositemproductclasstree.AreaID=" + AreaID;
		}
		me.defaultWhere = where;
		me.onSearch();
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
		    buttonsToolbar = me.getComponent('buttonsToolbar'),
			url = me.callParent(arguments);
        var checkBDictTreeId= buttonsToolbar.getComponent('checkBDictTreeId').getValue();
		var ischeckBDictTree=checkBDictTreeId == false ? true : false;
		url += '&isSearchChild=' + ischeckBDictTree;
		return url;
	},
	/**获取区域信息*/
	getAreaInfo:function(){
		var me = this;
		var url = JShell.System.Path.ROOT + me.areaUrl;
		url += '&fields=ClientEleArea_AreaCName,ClientEleArea_Id';
        me.AreaEnum = {},me.AreaList=[];
		JShell.Server.get(url,function(data){
			if(data.success){
				if(data.value) {
					Ext.Array.each(data.value.list, function(obj, index) {
						tempArr = [obj.ClientEleArea_Id, obj.ClientEleArea_AreaCName];
						me.AreaEnum[obj.ClientEleArea_Id] = obj.ClientEleArea_AreaCName;
						me.AreaList.push(tempArr);
					});
				}
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	},
	/**获取检测项目产品分类树关系信息*/
	getOSItemProductClassTreeLinkfo:function(ids,callback){
		var me = this;
		var url = JShell.System.Path.ROOT + me.selectUrl2;
		url += '&fields=OSItemProductClassTreeLink_Id&where=ositemproductclasstreelink.ItemProductClassTreeID in ('+ids+")";
		JShell.Server.get(url,function(data){
			if(data.success){
				callback(data);
			}else{
				JShell.Msg.error(data.msg);
			}
		},false);
	}
});