/**
 * 模板人员列表
 * @author liangyl
 * @version 2016-08-12
 */
Ext.define('Shell.class.qms.equip.templet.user.Grid', {
	extend: 'Shell.ux.grid.Panel',
	title: '模板人员列表',
	/**获取数据服务路径*/
	selectUrl: '/QMSReport.svc/ST_UDTO_SearchETempletEmpByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl: '/QMSReport.svc/ST_UDTO_UpdateETempletEmpByField',
	/**新增服务地址*/
	addUrl: '/QMSReport.svc/ST_UDTO_AddETempletEmp',
	/**删除数据服务路径*/
	delUrl: '/QMSReport.svc/ST_UDTO_DelETempletEmp',
	/**默认排序字段*/
	defaultOrderBy: [{
		property: 'ETempletEmp_DispOrder',
		direction: 'ASC'
	}],
	/**默认加载数据*/
	defaultLoad: false,
	/**默认选中数据*/
	autoSelect: true,
	IsShowAll:true,	
	/**后台排序*/
	remoteSort: false,
	/**主键列*/
	PKField: 'ETempletEmp_Id',
	/**是否显示姓名列*/
	IsCandidaShow:false,
	/**默认每页数量*/
//	defaultPageSize: 200,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.regStr = new RegExp('"', "g");
		if(me.IsCandidaShow==true){
			//查询框信息
			me.searchInfo = {
				width:140,emptyText:'姓名',isLike:true,itemId:'search',
				fields:['etempletemp.HREmployee.CName']
			};
		}else{
			me.searchInfo = {
				width:160,emptyText:'名称',isLike:true,itemId:'search',
				fields:['etempletemp.ETemplet.CName']
			};
		}
		me.buttonToolbarItems = ['refresh','-','add','del','->',{
			type: 'search',
			info: me.searchInfo
		}];
		//创建数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns =[];
		columns.push({
			text:'编号',dataIndex:'ETempletEmp_Id',width:150,hidden:true,
			sortable:true,defaultRenderer:true
		});
		if(me.IsCandidaShow==true){
			columns.push({
				text:'姓名',dataIndex:'ETempletEmp_HREmployee_CName',width: 120,
				sortable:true,defaultRenderer:true
			});
		}else{
			columns.push({
				text:'名称',dataIndex:'ETempletEmp_ETemplet_CName',flex:1,minWidth:220,
				sortable:true,renderer: function(value, meta, record) {
	            	var v=me.showMemoText(value, meta, record);
					return v;
				}
			},{
				text:'类型',dataIndex:'ETempletEmp_ETemplet_TempletType_CName',flex:1,
				sortable:true,renderer: function(value, meta, record) {
	            	var v=me.showMemoText(value, meta, record);
					return v;
				}
			},{
				text:'仪器',dataIndex:'ETempletEmp_ETemplet_EEquip_CName',flex:1,
				sortable:true,renderer: function(value, meta, record) {
	            	var v=me.showMemoText(value, meta, record);
					return v;
				}
			},{
				text:'小组',dataIndex:'ETempletEmp_ETemplet_Section_CName',flex:1,
				sortable:true,renderer: function(value, meta, record) {
	            	var v=me.showMemoText(value, meta, record);
					return v;
				}
			});
		}
		columns.push({
			text:'仪器模板id',dataIndex:'ETempletEmp_ETemplet_Id',width:120,
			sortable:false,defaultRenderer:true,hidden:true
		},{
			text:'使用',dataIndex:'ETempletEmp_IsUse',width:60,
			align: 'center',isBool: true,type: 'bool',
			sortable:true,defaultRenderer:true
		},{
			text:'显示次序',dataIndex:'ETempletEmp_DispOrder',width:80,
			sortable:true,defaultRenderer:true
		});
		return columns;
	},
	/**显示*/
	showMemoText:function(value, meta, record){
		var me=this;
		var qtipValue ='';
	    var CName = ""+ record.get("ETempletEmp_ETemplet_CName");
	    var EEquipCName =""+ record.get("ETempletEmp_ETemplet_EEquip_CName");
	  	var TempletTypeCName =""+ record.get("ETempletEmp_ETemplet_TempletType_CName");
	  	var SectionCName =""+ record.get("ETempletEmp_ETemplet_Section_CName");

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

	onAddClick: function() {
		var me = this;
		me.fireEvent('onAddClick', me);
	},
	onEditClick: function() {
		var me = this;
		me.fireEvent('onEditClick', me);
	},
	openForm:function(id,formPanel,width){
		var me = this;
		JShell.Win.open(formPanel,{
			height:400,
			width:width,
			checkOne:false,
			IsbtnAccept:true,
			SUB_WIN_NO: '1',
			title:'质量记录模板列表',
			 maximizable: false, //是否带最大化功能
			/**默认加载数据*/
	        defaultLoad: true,
			listeners:{
				save:function(p){
					p.close();
					me.onSearch();
				},
				accept:function(p){
					me.fireEvent('accept',p);
				}
			}
		}).show();
	},
	/**判断选择的人员是否已存在*/
	loadEmpData:function(ETempletId,Id,record){
		var me = this,
			url = JShell.System.Path.getRootUrl(me.selectUrl);
			
		var fields = [
			'ETempletEmp_Id'
		];
		url += '&fields=' + fields.join(',');
		url += '&where=etempletemp.ETemplet.Id='+ETempletId+ ' and etempletemp.HREmployee.Id=' + Id;
		
		JShell.Server.get(url,function(data){
			if(data.success){
				if( data.value.length==0){
					me.OnSaveETempletEmp(ETempletId,Id,record);
				}
			}
		}, false, 1000, false);
	},
	/**模板员工关系*/
	OnSaveETempletEmp: function(TempletID,EmpID,record) {
		var me = this;
		var TempletTypeId='',SectionId='';
		var url = me.addUrl;
		url = (url.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + url;
        var DataTimeStamp = '1,2,3,4,5,6,7,8';
	    var entity = {
	        IsUse:1
		};
		if(record){
			TempletTypeId=record.get('ETemplet_TempletType_Id');
		    SectionId =record.get('ETemplet_Section_Id');
		}
		if(TempletTypeId){
			entity.TempletType = {
				Id:TempletTypeId,
				DataTimeStamp:DataTimeStamp.split(",")
			};
		}
		if(SectionId){
			entity.Section = {
				Id:SectionId,
				DataTimeStamp:DataTimeStamp.split(",")
			};
		}
		//模板仪器ID
		if(TempletID){
			entity.ETemplet = {
				Id:TempletID,
				DataTimeStamp:DataTimeStamp.split(",")
			};
		}
	    //人员ID
		if(EmpID){
			entity.HREmployee = {
				Id:EmpID,
				DataTimeStamp:DataTimeStamp.split(",")
			};
		}
		
		var params = {
			entity: entity
		};
		
		if(!params) return;
		params = Ext.JSON.encode(params);
	
		JShell.Server.post(url, params, function(data) {
			if(data.success) {
				if(me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, me.hideTimes);
			} else {
				var msg = data.msg;
				if(msg == JShell.Server.Status.ERROR_UNIQUE_KEY) {
					msg = '有重复';
				}
				JShell.Msg.error(msg);
			}
		}, false);
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
//			sort:JSON.stringify(me.defaultOrderBy)
        };
	},
	
	 /**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			search = null,params = [];
				
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