/**
 * 字典类型列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.dicttype.Grid',{
    extend: 'Shell.ux.grid.Panel',
	requires: ['Ext.ux.CheckColumn'],
    
    title:'字典类型列表',
    width: 800,
	height: 500,
	
  	/**获取数据服务路径*/
	selectUrl:'/BaseService.svc/ST_UDTO_SearchFDictTypeByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl:'/BaseService.svc/ST_UDTO_UpdateFDictTypeByField',
	/**删除数据服务路径*/
	delUrl:'/BaseService.svc/ST_UDTO_DelFDictType',
  	
	/**显示成功信息*/
	showSuccessInfo: false,
	/**消息框消失时间*/
	hideTimes: 3000,
	
	/**默认加载*/
	defaultLoad: false,
	/**默认每页数量*/
	defaultPageSize:50,
	
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: true,
	/**后台排序*/
	remoteSort: false,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**带功能按钮栏*/
	hasButtontoolbar:true,
	/**是否启用序号列*/
	hasRownumberer: true,

	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
	
	/**是否启用刷新按钮*/
	hasRefresh:true,
	/**是否启用新增按钮*/
	hasAdd:true,
//	/**是否启用修改按钮*/
//	hasEdit:true,
	/**是否启用删除按钮*/
	hasDel:true,
	/**是否启用保存按钮*/
	hasSave:true,
	/**是否启用查询框*/
	hasSearch:true,
	
	/**查询栏参数设置*/
	searchToolbarConfig:{},
	
	defaultOrderBy:[{property:'FDictType_DispOrder',direction:'ASC'}],

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		
		//查询框信息
		me.searchInfo = {width:220,emptyText:'编码/名称',isLike:true,
			fields:['fdicttype.DictTypeCode','fdicttype.CName']};
		
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var me = this;
		var columns = [{
			text:'编码',dataIndex:'FDictType_DictTypeCode',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'名称',dataIndex:'FDictType_CName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			xtype:'checkcolumn',text:'使用',dataIndex:'FDictType_IsUse',
			width:40,align:'center',sortable:false,menuDisabled:true,
			stopSelection:false,type:'boolean'
		},{
			text:'创建时间',dataIndex:'FDictType_DataAddTime',width:130,
			isDate:true,hasTime:true
		},{
			text:'备注',dataIndex:'FDictType_Memo',width:200,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'次序',dataIndex:'FDictType_DispOrder',width:100,
			defaultRenderer:true,align:'center',type:'int'
		},{
			text:'主键ID',dataIndex:'FDictType_Id',isKey:true,hidden:true,hideable:false
		}];
		
		return columns;
	},
	onSaveClick:function(){
		var me = this,
			records=me.store.getModifiedRecords(),//获取修改过的行记录
			len = records.length;
			
		if(len == 0) return;
			
		me.showMask(me.saveText);//显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;
		
		for(var i=0;i<len;i++){
			var rec = records[i];
			var id = rec.get(me.PKField);
			var IsUse = rec.get('FDictType_IsUse');
			me.updateOneByIsUse(i,id,IsUse);
		}
	},
	updateOneByIsUse:function(index,id,IsUse){
		var me = this;
		var url = (me.editUrl.slice(0,4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;
		
		var params = Ext.JSON.encode({
			entity:{
				Id:id,
				IsUse:IsUse
			},
			fields:'Id,IsUse'
		});
		
		setTimeout(function(){
			JShell.Server.post(url,params,function(data){
				var record = me.store.findRecord(me.PKField,id);
				if(data.success){
					if(record){record.set(me.DelField,true);record.commit();}
					me.saveCount++;
				}else{
					me.saveErrorCount++;
					if(record){record.set(me.DelField,false);record.commit();}
				}
				if(me.saveCount + me.saveErrorCount == me.saveLength){
					me.hideMask();//隐藏遮罩层
					if(me.saveErrorCount == 0) me.onSearch();
				}
			});
		},100 * index);
	},
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick:function(){
		this.fireEvent('addclick',this);
	}
});