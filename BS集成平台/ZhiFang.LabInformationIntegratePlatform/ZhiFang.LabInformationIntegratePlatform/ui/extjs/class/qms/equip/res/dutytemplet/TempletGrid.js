/**
 * 职责模板列表（关系列表）
 * @author liangyl
 * @version 2017-11-23
 */
Ext.define('Shell.class.qms.equip.res.dutytemplet.TempletGrid', {
	extend: 'Shell.ux.grid.Panel',
	requires: ['Ext.ux.CheckColumn'],
    title: '职责模板列表',
	/**获取数据服务路径*/
	selectUrl: '/QMSReport.svc/ST_UDTO_SearchETempletResByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl: '/QMSReport.svc/ST_UDTO_UpdateETempletResByField',
	/**删除数据服务路径*/
	delUrl: '/QMSReport.svc/ST_UDTO_DelETempletRes',
	/**默认排序字段*/
	defaultOrderBy: [{property: 'ETempletRes_DispOrder',direction: 'ASC'}],
	/**默认选中数据*/
	autoSelect: true,
     /**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
    /**默认加载数据*/
	defaultLoad: false,
	/**是否启用查询框*/
	hasSearch: true,
	 /**是否启用新增按钮*/
	hasAdd: true,
	/**是否启用删除按钮*/
	hasDel: true,
	/**职责id*/
	EResponsibilityId:null,
	/**后台排序*/
	remoteSort: false,
	/**新增职责模板关系数据服务*/
	addUrl:'/QMSReport.svc/ST_UDTO_AddETempletRes',
	/**默认每页数量*/
	defaultPageSize: 200,
	initComponent: function() {
		var me = this;
		me.regStr = new RegExp('"', "g");
		//创建功能按钮栏Items
		me.buttonToolbarItems = me.createButtonToolbarItems();
	    //创建数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建功能按钮栏Items*/
	createButtonToolbarItems: function() {
		var me = this,
		buttonToolbarItems = [];
	   //查询框信息
		me.searchInfo = {width: 145,emptyText: '模板名称',isLike: true,
			itemId: 'search',fields: ['etempletres.ETemplet.CName']
		};
		buttonToolbarItems.push('refresh','-','add','del','->',{
			type: 'search',
			info: me.searchInfo
		});
		return buttonToolbarItems;
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		
		var columns = [{
				text:'主键',dataIndex:'ETempletRes_Id',width:170,hidden:true,
				sortable:false,isKey: true,defaultRenderer:true
		    },{
				text:'名称',dataIndex:'ETempletRes_ETemplet_CName',flex:1,minWidth:250,
				sortable:true,renderer: function(value, meta, record) {
	            	var v=me.showMemoText(value, meta, record);
					return v;
				}
			},{
				text:'类型',dataIndex:'ETempletRes_ETemplet_TempletType_CName',width:100,
				sortable:true,renderer: function(value, meta, record) {
	            	var v=me.showMemoText(value, meta, record);
					return v;
				}
			},{
				text:'仪器',dataIndex:'ETempletRes_ETemplet_EEquip_CName',width:180,
				sortable:true,renderer: function(value, meta, record) {
	            	var v=me.showMemoText(value, meta, record);
					return v;
				}
			},{
				text:'小组',dataIndex:'ETempletRes_ETemplet_Section_CName',width:100,
				sortable:true,renderer: function(value, meta, record) {
	            	var v=me.showMemoText(value, meta, record);
					return v;
				}
			},{
			text:'使用',dataIndex:'ETempletRes_IsUse',
			width:60,align: 'center',
			isBool: true,type: 'bool',sortable:true,
			defaultRenderer:true
		}];
		
		return columns;
	},
	/**显示*/
	showMemoText:function(value, meta, record){
		var me=this;
		var qtipValue ='';
	    var CName = ""+ record.get("ETempletRes_ETemplet_CName");
	    var EEquipCName =""+ record.get("ETempletRes_ETemplet_EEquip_CName");
	  	var TempletTypeCName =""+ record.get("ETempletRes_ETemplet_TempletType_CName");
	  	var SectionCName =""+ record.get("ETempletRes_ETemplet_Section_CName");

	    CName = CName.replace(me.regStr, "'");
	    EEquipCName = EEquipCName.replace(me.regStr, "'");
	    TempletTypeCName = TempletTypeCName.replace(me.regStr, "'");
	    SectionCName = SectionCName.replace(me.regStr, "'");
		qtipValue = qtipValue + "<p border=0 style='vertical-align:top;font-size:12px;'>" + "<b>模板名称:</b>" + CName + "</p>";
		qtipValue = qtipValue + "<p border=0 style='vertical-align:top;font-size:12px;'>" + "<b>仪器:</b>" + EEquipCName + "</p>";
		qtipValue = qtipValue + "<p border=0 style='vertical-align:top;font-size:12px;'>" + "<b>类型:</b>" + TempletTypeCName + "</p>";
		qtipValue = qtipValue + "<p border=0 style='vertical-align:top;font-size:12px;'>" + "<b>小组:</b>" + SectionCName + "</p>";
		
		meta.tdAttr = 'data-qtip="' + qtipValue + '"';
		return value;
	},
	/**根据职责id加载*/
	loadByDutyId:function(id){
		var me=this;
		me.defaultWhere='etempletres.EResponsibility.Id='+id;
		me.onSearch();
	},
	onAddClick: function() {
		var me = this;
		me.showForm();
	},
	/**打开人员新增选择*/
	showForm: function() {
		var me = this,
			config = {
				resizable: false,
				maximizable: false, //是否带最大化功能
				checkOne:false,
				width:800,
				listeners: {
					accept:function(p){
						var records = p.getSelectionModel().getSelection();
					    me.onSave(p,records);
					}
				}
			};
		JShell.Win.open('Shell.class.qms.equip.templet.basic.CheckGrid', config).show();
	},
    /**保存关系数据*/
	onSave:function(p,records){
		var me = this,
			ids = [],
		    nos = [],
			addIds = [],addNos=[];
			
		if(records.length == 0) return;
			
		me.showMask(me.saveText);//显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = records.length;
			
		for(var i in records){
			ids.push(records[i].get('ETemplet_Id'));
		}
		//获取现有关系用于验证过滤已经存在的关系
		me.getLinkByIds(ids,function(list){
			addIds=[];
			for(var i in records){
				var ETempletId = records[i].get('ETemplet_Id');
				var hasLink = false;
				for(var j in list){
					if(ETempletId == list[j].ETempletRes_ETemplet_Id ){
						hasLink = true;
						break;
					}
				}
				if(!hasLink){
					addIds.push(ETempletId);
				}
				if(hasLink){
					me.hideMask();//隐藏遮罩层
					p.close();
				}
			}
			
			//循环保存数据
			for(var i in addIds){
				me.saveLength = addIds.length;
				me.onAddOneLink(addIds[i],function(){
					p.close();
					me.onSearch();
				});
			}
		});
	},
	/**新增关系数据*/
	onAddOneLink:function(ETempletId,callback){
		var me = this,
			url = JShell.System.Path.ROOT + me.addUrl;
		var params = {
			entity:{
				IsUse:1
			}
		};
		if(ETempletId){
			params.entity.ETemplet={
				Id: ETempletId,
				DataTimeStamp:[0,0,0,0,0,0,0,0]
			};
		}
		if(me.EResponsibilityId){
			params.entity.EResponsibility={
				Id: me.EResponsibilityId,
				DataTimeStamp:[0,0,0,0,0,0,0,0]
			};
		}
		//提交数据到后台
		JShell.Server.post(url,Ext.JSON.encode(params),function(data){
			if(data.success){
				me.saveCount++;
			}else{
				me.saveErrorCount++;
			}
			if(me.saveCount + me.saveErrorCount == me.saveLength){
				me.hideMask();//隐藏遮罩层
				if(me.saveErrorCount == 0){callback();}
			}
		});
	},
	/**系数据，用于验证勾选的项目是否已经存在于关系中*/
	getLinkByIds:function(ids,callback){
		var me = this,
			url = JShell.System.Path.ROOT + me.selectUrl.split('?')[0] + 
				'?fields=ETempletRes_Id,ETempletRes_ETemplet_Id' +
				'&isPlanish=true&where=etempletres.ETemplet.Id in(' + ids.join(',') + ') and etempletres.EResponsibility.Id='+me.EResponsibilityId ;
		JShell.Server.get(url,function(data){
			if(data.success){
				var list = (data.data || {}).list || [];
				callback(data.value.list);
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	},
	/**加载数据前*/
	onBeforeLoad: function() {
		var me = this;
		me.disableControl(); //禁用 所有的操作功能
		if (!me.defaultLoad) return false;
		me.getView().update();
		me.store.proxy.url = me.getLoadUrl(); //查询条件
		me.store.proxy.extraParams= {
			sort:Ext.encode(me.defaultOrderBy)
		};
	},
	changeDefaultWhere:function(){
		var me=this;
		//defaultWhere追加上IsUse约束
		if(me.defaultWhere){
			var index = me.defaultWhere.indexOf('etempletres.IsUse=1');
			if(index == -1){
				me.defaultWhere += ' and etempletres.IsUse=1';
			}
		}else{
			me.defaultWhere = 'etempletres.IsUse=1';
		}
	},
	 /**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			search = null,params = [];
			
		//改变默认条件
		me.changeDefaultWhere();
			
		me.internalWhere = '';
			
		if(buttonsToolbar){
			search = buttonsToolbar.getComponent('search').getValue();
		}
		
		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
		if(search) {
			if(me.internalWhere) {
				me.internalWhere += ' and (' + me.getSearchWhere(search) + ')';
			} else {
				me.internalWhere = me.getSearchWhere(search);
			}
		}
		return me.callParent(arguments);
	}
});