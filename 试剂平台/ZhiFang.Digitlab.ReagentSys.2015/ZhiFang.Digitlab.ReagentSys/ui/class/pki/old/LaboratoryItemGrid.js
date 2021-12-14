/**
 * 送检单位项目列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.LaboratoryItemGrid',{
    extend:'Shell.ux.grid.Panel',
    title:'送检单位项目列表',
    width:370,
    
    /**获取数据服务路径*/
    selectUrl:'/BaseService.svc/ST_UDTO_SearchDUnitItemByHQL?isPlanish=true',
    /**删除数据服务路径*/
    delUrl:'/BaseService.svc/ST_UDTO_DelDUnitItem',
    /**新增经销商项目服务路径*/
   	saveUrl:'/BaseService.svc/ST_UDTO_AddDUnitItem',
    /**默认加载*/
	defaultLoad:false,
	/**后台排序*/
	remoteSort:false,
	/**带分页栏*/
	hasPagingtoolbar:false,
	/**默认每页数量*/
	defaultPageSize:500,
	/**是否启用序号列*/
	hasRownumberer:true,
	/**复选框*/
	multiSelect:true,
	selType:'checkboxmodel',
	hasDel:true,
	
	/**经销商ID*/
	LaboratoryId:null,
	/**经销商时间戳*/
	DealerDataTimeStamp:null,
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
	},
	initComponent:function(){
		var me = this;
		//查询框信息
		me.searchInfo = {width:165,emptyText:'项目名称',isLike:true,fields:['dunititem.Item.CName']};
		//自定义按钮功能栏
		me.buttonToolbarItems = ['add','del','->',{type:'search',info:me.searchInfo}];
		//数据列
		me.columns = [{
			dataIndex:'DUnitItem_BTestItem_CName',text:'项目名称',width:150,defaultRenderer:true
		},{
			dataIndex:'DUnitItem_BTestItem_CoopLevel',text:'合作级别',width:70,
			renderer:function(value,meta){
				var v = JShell.PKI.Enum.CoopLevel['E' + value] || '';
		        if(v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
		        return v;
		    }},{
			dataIndex:'DUnitItem_Id',text:'主键ID',hidden:true,hideable:false,isKey:true
		},{
			dataIndex:'DUnitItem_BTestItem_Id',text:'项目ID',hidden:true,hideable:false
		},{
			dataIndex:'DUnitItem_DataTimeStamp',text:'时间戳',hidden:true,hideable:false
		}];
		
		me.callParent(arguments);
	},
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick:function(){
		var me = this;
		JShell.Win.open('Shell.class.pki.ItemCheckGrid',{
			resizable:false,
			checkOne:false,
			listeners:{
				accept:function(p,records){me.onAccept(records);p.close();}
			}
		}).show();
	},
	/**选择确认处理*/
	onAccept:function(records){
		var me = this;
		//获取没有保存过的数据
		var recs = me.getUnCheck(records);
		var len = recs.length;
		
		me.saveCount = len;
		me.saveIndex = 0;
		me.saveError = [];
		
		for(var i=0;i<len;i++){
			var rec = recs[i];
			me.saveOne({
				UnitType:2,
				UnitID:me.LaboratoryId,
				BTestItem:{
					Id:rec.get('BTestItem_Id'),
					DataTimeStamp:rec.get('BTestItem_DataTimeStamp').split(',')
				}
			});
		}
	},
	/**保存一条信息*/
	saveOne:function(data){
		var me = this;
		var url = (me.saveUrl.slice(0,4) == 'http' ? '' : JShell.System.Path.ROOT) + me.saveUrl;
		
		var params = {entity:data};
		params = Ext.JSON.encode(params);
		
		me.showMask(me.saveText);//显示遮罩层
		JShell.Server.post(url,params,function(data){
			me.saveIndex++;
			if(!data.success){
				me.saveError.push(data.msg);
			}
			if(me.saveIndex == me.saveCount){
				me.hideMask();//隐藏遮罩层
				if(me.saveError.length == 0){
					JShell.Msg.alert(JShell.All.SUCCESS_TEXT);
					me.onSearch();
				}else{
					JShell.Msg.error(me.saveError.join('</br>'));
				}
			}
		});
	},
	/**获取没有保存过的数据*/
	getUnCheck:function(records){
		var me = this,
			recs = records || [],
			len = recs.length,
			result = [];
		
		for(var i=0;i<len;i++){
			var index = me.store.find('DUnitItem_BTestItem_Id',recs[i].get('BTestItem_Id'));
			if(index == -1) result.push(recs[i]);
		}
		
		return result;
	},
	/**根据送检单位ID获取数据*/
	loadByLaboratoryId:function(id){
		var me = this;
		me.DealerId = id;
		me.defaultWhere = 'dunititem.UnitType=2 and dunititem.UnitID=' + id;
		me.onSearch();
	}
});