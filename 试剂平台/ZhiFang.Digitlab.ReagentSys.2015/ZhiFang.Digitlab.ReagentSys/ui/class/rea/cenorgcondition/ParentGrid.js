/**
 * 上级机构列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.rea.cenorgcondition.ParentGrid',{
    extend:'Shell.ux.grid.Panel',
    title:'上级机构列表',
    width:800,
    height:500,
    
    /**获取数据服务路径*/
    selectUrl:'/ReagentSysService.svc/ST_UDTO_SearchCenOrgConditionByHQL?isPlanish=true',
    /**删除数据服务路径*/
	delUrl: '/ReagentSysService.svc/ST_UDTO_DelCenOrgCondition',
	/**新增数据服务路径*/
	addUrl:'/ReagentSysService.svc/ST_UDTO_AddCenOrgCondition',
	
    /**是否启用刷新按钮*/
	hasRefresh:true,
	/**是否启用新增按钮*/
	hasAdd:true,
	/**是否启用删除按钮*/
	hasDel:true,
	/**是否启用查询框*/
	hasSearch:true,
	
	/**默认加载数据*/
	defaultLoad:false,
	/**排序字段*/
	//defaultOrderBy:[{property:'CenOrgCondition',direction:'ASC'}],
	
	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
	hasDel: true,
	
	/**本机构ID*/
	CenOrgId:null,
    
    afterRender:function(){
    	var me = this;
    	me.callParent(arguments);
    	me.on({
    		itemdblclick:function(view,record){
    			JShell.Win.open('Shell.class.rea.cenorgcondition.Form',{
    				formtype:'edit',
					PK:record.get(me.PKField),
					listeners:{
						save:function(p){
							p.close();
						}
					}
				}).show();
    		}
    	});
    },
    
	initComponent:function(){
		var me = this;
		//查询框信息
		me.searchInfo = me.createSearchInfo();
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建查询框信息*/
	createSearchInfo:function(){
		return {
			width:160,emptyText:'机构编号/中文名/英文名',isLike:true,
			fields:[
				'cenorgcondition.cenorg1.OrgNo',
				'cenorgcondition.cenorg1.CName',
				'cenorgcondition.cenorg1.EName'
			]
		};
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var columns = [{
			dataIndex: 'CenOrgCondition_cenorg1_OrgNo',
			text: '机构编号',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'CenOrgCondition_cenorg1_CName',
			text: '中文名',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'CenOrgCondition_cenorg1_EName',
			text: '英文名',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'CenOrgCondition_cenorg1_CenOrgType_CName',
			text: '机构类型',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'CenOrgCondition_Memo',
			text: '备注',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'CenOrgCondition_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}];
		
		return columns;
	},
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick:function(){
		var me = this;
		var config = {
			resizable:false,
			checkOne:false,
			width:315,
			defaultWhere:'cenorg.Id<>' + me.CenOrgId,
			listeners:{
				accept:function(p,records){me.onAccept(p,records);}
			}
		};
		JShell.Win.open('Shell.class.rea.cenorg.CheckGrid',config).show();
	},
	getLoadUrl:function(){
		var me = this;
		me.initDefaultWhere();
		return me.callParent(arguments);
	},
	initDefaultWhere:function(){
		var me = this;
		me.defaultWhere = 'cenorgcondition.cenorg2.Id=' + me.CenOrgId;
	},
	onAccept:function(p,records){
		var me = this,
			len = records.length;
			
		me.saveCount = len;
		me.saveIndex = 0;
		me.saveError = [];
		
		me.showMask(me.saveText);//显示遮罩层
		for(var i=0;i<len;i++){
			var rec = records[i];
			var entity = me.getEntity(rec);
			me.saveOne(i,p,entity);
		}
	},
	getEntity:function(record){
		var me = this;
		return {
			cenorg1:{Id:record.get('CenOrg_Id')},
			cenorg2:{Id:me.CenOrgId}
		};
	},
	/**保存一条数据*/
	saveOne:function(index,p,entity){
		var me = this;
		var url = (me.addUrl.slice(0,4) == 'http' ? '' : JShell.System.Path.ROOT) + me.addUrl;
		
		var params = Ext.JSON.encode({entity:entity});
		
		setTimeout(function(){
			JShell.Server.post(url,params,function(data){
				me.saveIndex++;
				if(!data.success){
					if(data.msg.indexOf('唯一索引') == -1){
						me.saveError.push(data.msg);
					}
				}
				if(me.saveIndex == me.saveCount){
					me.hideMask();//隐藏遮罩层
					if(me.saveError.length == 0){
						JShell.Msg.alert(JShell.All.SUCCESS_TEXT,null,1000);
						me.onSearch();p.close();
					}else{
						JShell.Msg.error(me.saveError.join('</br>'));
					}
				}
			});
		},100 * index);
	}
});