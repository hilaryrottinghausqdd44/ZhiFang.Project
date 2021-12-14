/**
 * 职责人员列表（职责人员关系表）
 * @author liangyl
 * @version 2017-11-23
 */
Ext.define('Shell.class.qms.equip.res.dutyemp.EmpGrid', {
	extend: 'Shell.ux.grid.Panel',
	requires: ['Ext.ux.CheckColumn'],
	title: '职责人员列表',
	/**获取数据服务路径*/
	selectUrl: '/QMSReport.svc/ST_UDTO_SearchEResEmpByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl: '/QMSReport.svc/ST_UDTO_UpdateEResEmpByField',
	/**删除数据服务路径*/
	delUrl: '/QMSReport.svc/ST_UDTO_DelEResEmp',
	/**默认排序字段*/
	defaultOrderBy: [{property: 'EResEmp_DispOrder',direction: 'ASC'}],

	addUrl:'/QMSReport.svc/ST_UDTO_AddEResEmp',
	
	/**默认加载数据*/
	defaultLoad: false,
	/**默认选中数据*/
	autoSelect: true,
	IsShowAll:true,	
	/**职责Id*/
	DutyId: null,
//	defaultWhere:'',
	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
		/**默认每页数量*/
//	defaultPageSize: 200,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
    initComponent: function() {
		var me = this;
		//创建功能按钮栏Items
		me.buttonToolbarItems = me.createButtonToolbarItems();
	
		//创建数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	
	/**创建功能按钮栏Items*/
	createButtonToolbarItems:function(){
		var me = this,
			buttonToolbarItems = [];
         me.searchInfo = {
			width:135,emptyText:'姓名',isLike:true,itemId:'search',
			fields:['eresemp.HREmployee.CName']
		};		buttonToolbarItems.push('refresh','-','add','del');

		buttonToolbarItems.push('->',{
			type: 'search',
			info: me.searchInfo
		});
		return buttonToolbarItems;
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns =[{
			text:'主键',dataIndex:'EResEmp_Id',width:150,hidden:true,
				sortable:true,isKey: true,defaultRenderer:true
			},{
				text:'姓名',dataIndex:'EResEmp_HREmployee_CName',width: 120,
				sortable:true,defaultRenderer:true
			},{
				text:'使用',
				dataIndex:'EResEmp_IsUse',
				width:60,
				align: 'center',
				isBool: true,
				type: 'bool',
				sortable:true,
				defaultRenderer:true
			},{
				text:'次序',dataIndex:'EResEmp_DispOrder',width:55,
				sortable:true,defaultRenderer:true
		}];
		return columns;
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
				width:700,
				listeners: {
					accept:function(p,records){
					    me.onSave(p,records);
					}
				}
			};
		JShell.Win.open('Shell.class.qms.equip.templet.user.check.CheckApp', config).show();
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
			ids.push(records[i].get('HREmployee_Id'));
		}
		//获取现有关系用于验证过滤已经存在的关系
		me.getLinkByIds(ids,function(list){
			addIds=[];
			for(var i in records){
				var HREmployeeId = records[i].get('HREmployee_Id');
				var hasLink = false;
				for(var j in list){
					if(HREmployeeId == list[j].EResEmp_HREmployee_Id ){
						hasLink = true;
						break;
					}
				}
				if(!hasLink){
					addIds.push(HREmployeeId);
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
	onAddOneLink:function(HREmployeeId,callback){
		var me = this,
			url = JShell.System.Path.ROOT + me.addUrl;
		var params = {
			entity:{
				IsUse:1
			}
		};
		if(me.DutyId){
			params.entity.EResponsibility={
				Id: me.DutyId,
				DataTimeStamp:[0,0,0,0,0,0,0,0]
			};
		}
		if(HREmployeeId){
			params.entity.HREmployee={
				Id: HREmployeeId,
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
	
	/**根据IDS获取关系数据，用于验证勾选的项目是否已经存在于关系中*/
	getLinkByIds:function(ids,callback){
		var me = this,
			url = JShell.System.Path.ROOT + me.selectUrl.split('?')[0] + 
				'?fields=EResEmp_Id,EResEmp_HREmployee_Id' +
				'&isPlanish=true&where=eresemp.HREmployee.Id in(' + ids.join(',') + ') and eresemp.EResponsibility.Id='+me.DutyId ;
		JShell.Server.get(url,function(data){
			if(data.success){
				var list = (data.data || {}).list || [];
				callback(data.value.list);
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	},
	/**根据职责id加载*/
	loadDataId:function(id){
		var me=this;
		me.defaultWhere='eresemp.EResponsibility.Id='+id;
		me.onSearch();
	},
	changeDefaultWhere:function(){
		var me=this;
		//defaultWhere追加上IsUse约束
		if(me.defaultWhere){
			var index = me.defaultWhere.indexOf('eresemp.IsUse=1');
			if(index == -1){
				me.defaultWhere += ' and eresemp.IsUse=1';
			}
		}else{
			me.defaultWhere = 'eresemp.IsUse=1';
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