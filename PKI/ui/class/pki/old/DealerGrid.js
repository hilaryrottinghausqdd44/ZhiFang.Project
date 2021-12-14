/**
 * 经销商列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.DealerGrid',{
    extend:'Shell.ux.grid.Panel',
    title:'经销商列表',
    
    /**获取数据服务路径*/
    selectUrl:'/BaseService.svc/ST_UDTO_SearchBDealerByHQL?isPlanish=true',
    /**删除数据服务路径*/
	delUrl:'/BaseService.svc/ST_UDTO_DelBDealer',
    /**默认加载*/
	defaultLoad:true,
	/**后台排序*/
	remoteSort:false,
	/**带分页栏*/
	hasPagingtoolbar:false,
	/**默认每页数量*/
	defaultPageSize:500,
	/**是否启用序号列*/
	hasRownumberer:false,
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
		//查询框信息
		me.searchInfo = {width:'100%',emptyText:'经销商名称',isLike:true,fields:['bdealer.Name']};
		//自定义按钮功能栏
		me.buttonToolbarItems = [{type:'search',info:me.searchInfo}];
		//创建挂靠功能栏
		me.dockedItems = [Ext.create('Shell.ux.toolbar.Button',{
			dock:'bottom',
			items:['add','edit','del','-','import_excel']
		})];
		//数据列
		me.columns = [{
			dataIndex:'BDealer_Name',text:'经销商名称',width:100,defaultRenderer:true
		},{
			dataIndex:'BDealer_BStepPrice',text:'阶梯价',width:50,isBool:true,align:'center'
		},{
			dataIndex:'BDealer_BBillingUnit_Name',text:'默认开票方',width:100,defaultRenderer:true
		},{
			dataIndex:'BDealer_Id',text:'主键ID',hidden:true,hideable:false,isKey:true
		},{
			dataIndex:'BDealer_BBillingUnit_Id',text:'开票方ID',hidden:true,hideable:false
		},{
			dataIndex:'BDealer_DataTimeStamp',text:'时间戳',hidden:true,hideable:false
		}];
		
		me.callParent(arguments);
	},
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick:function(){
		var me = this;
		me.openDealerForm();
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
		me.openDealerForm(id);
	},
	/**打开表单*/
	openDealerForm:function(id){
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
		JShell.Win.open('Shell.class.pki.DealerForm',config).show();
	},
	/**点击导入按钮处理*/
	onImportExcelClick:function(){
		JShell.Win.open('Shell.class.pki.excel.FileUpdatePanel',{
			resizable:false
		}).show();
	}
});