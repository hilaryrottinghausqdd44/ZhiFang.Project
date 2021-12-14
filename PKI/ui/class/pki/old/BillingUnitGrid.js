/**
 * 开票方列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.BillingUnitGrid',{
    extend:'Shell.ux.grid.Panel',
    title:'开票方列表',
    width:300,
    height:500,
    
    /**获取数据服务路径*/
    selectUrl:'/BaseService.svc/ST_UDTO_SearchBBillingUnitByHQL?isPlanish=true',
    /**删除数据服务路径*/
	delUrl:'/BaseService.svc/ST_UDTO_DelBBillingUnit',
    /**默认加载*/
	defaultLoad:true,
	/**后台排序*/
	remoteSort:true,
	/**带分页栏*/
	hasPagingtoolbar:true,
	/**是否启用序号列*/
	hasRownumberer:true,
	
	/**复选框*/
	multiSelect:true,
	selType:'checkboxmodel',
	hasDel:true,
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		me.on({
			itemdblclick:function(view,record){
				me.onEditClick();
			}
		});
	},
	initComponent:function(){
		var me = this;
		me.addEvents('accept');
		//查询框信息
		me.searchInfo = {width:220,emptyText:'开票方名称',isLike:true,fields:['bbillingunit.Name']};
		//自定义按钮功能栏
		me.buttonToolbarItems = ['add','edit','del','-','import_excel','->',{type:'search',info:me.searchInfo}];
		//数据列
		me.columns = [{
			dataIndex:'BBillingUnit_Name',text:'开票方名称',width:200,defaultRenderer:true
		},{
			dataIndex:'BBillingUnit_Id',text:'开票方ID',hidden:true,hideable:false,isKey:true
		},{
			dataIndex:'BBillingUnit_DataTimeStamp',text:'开票方时间戳',hidden:true,hideable:false
		}];
		
		me.callParent(arguments);
	},
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick:function(){
		var me = this;
		me.openBillingUnitForm();
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
		me.openBillingUnitForm(id);
	},
	/**打开表单*/
	openBillingUnitForm:function(id){
		var me = this;
		var config = {
			showSuccessInfo:false,//成功信息不显示
			resizable:false,
			formtype:'add',
			listeners:{
				save:function(win){
					me.onSearch();
					win.close();
				}
			}
		};
		if(id){
			config.formtype = 'edit';
			config.PK = id;
		}
		JShell.Win.open('Shell.class.pki.BillingUnitForm',config).show();
	}
});