/**
 * 销售客户列表
 * @author Jcall
 * @version 2016-11-10
 */
Ext.define('Shell.class.wfm.client.user.Grid',{
    extend:'Shell.ux.grid.Panel',
    requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
    title:'销售客户列表',
    
    /**获取数据服务路径*/
   	selectUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPClientByHQLAndSalesManId?isPlanish=true',

	/**新增数据服务路径*/
	addUrl:'/ProjectProgressMonitorManageService.svc/ST_UDTO_AddPSalesManClientLink',
	/**删除数据服务路径*/
	delUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_DelPSalesManClientLink',
	 /**导出客户数据服务路径*/
	downlUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_ExportExcelPClient',

	/**默认排序字段*/
	defaultOrderBy: [{
		property: 'PClient_ProvinceName',
		direction: 'ASC'
	},{
		property: 'PClient_Name',
		direction: 'ASC'
	}],
	/**默认加载数据*/
	defaultLoad: false,
	/**默认选中数据*/
	autoSelect: false,
	/**只显示销售客户列表*/
	isOnlyClient:true,
	
	/**销售人员ID*/
	SalesManID:null,
	/**销售人员姓名*/
	SalesManName:null,
	/**已分配客户*/
	OnlyClient:true,
	/**颜色*/
	Color:'green',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
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
			width:160,emptyText:'客户名称',isLike:true,itemId:'search',
			fields:['pclient.Name']
		};
		
		buttonToolbarItems.unshift(	{
			xtype: 'label',text: '',itemId: 'ShowText',
			name:'ShowText',style: "font-weight:bold;color:blue;",margin: '0 10 0 10'
		},'-','refresh','-',{
			width:65,boxLabel:'合约用户',itemId:'IsContract',
		    xtype:'checkbox',checked:false,
		    listeners: {
				change: function() {
					me.onSearch();
				}
			}
	    },'-',{
			type: 'search',
			info: me.searchInfo
		},'-',{
			xtype:'trigger',
			triggerCls:'x-form-search-trigger',
			enableKeyEvents:true,
			itemId:'searchBman',
			width:120,emptyText:'业务员',
			onTriggerClick:function(){
				me.onSearch();
			},
			listeners:{
	            keyup:{
	                fn:function(field,e){
	                	if(e.getKey() == Ext.EventObject.ESC){
	                		me.onSearch();
	                	}else if(e.getKey() == Ext.EventObject.ENTER){
	                		me.onSearch();
	                	}
	                }
	            }
	        }
		});
		return buttonToolbarItems;
		
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			text:'区域',dataIndex:'PClient_ClientAreaName',width:70,
			sortable:false,renderer:function(value,meta,record){
				return me.onBackgroundRender(value,meta,record);
			}
		},{
			text:'客户名称',dataIndex:'PClient_Name',width:250,
			sortable:false,renderer:function(value,meta,record){
				return me.onBackgroundRender(value,meta,record);
			}
		},{
			text:'客户类型',dataIndex:'PClient_ClientTypeName',width:70,
			sortable:false,renderer:function(value,meta,record){
				return me.onBackgroundRender(value,meta,record);
			}
		},{
			text:'省份',dataIndex:'PClient_ProvinceName',width:70,
			sortable:false,renderer:function(value,meta,record){
				return me.onBackgroundRender(value,meta,record);
			}
		},{
			text:'城市',dataIndex:'PClient_CityName',width:70,
			sortable:false,renderer:function(value,meta,record){
				return me.onBackgroundRender(value,meta,record);
			}
		},{
			text:'地址',dataIndex:'PClient_Address',width:140,
			sortable:false,renderer:function(value,meta,record){
				return me.onBackgroundRender(value,meta,record);
			}
		},{
			text:'区域',dataIndex:'PClient_ClientAreaName',width:70,
			sortable:false,defaultRenderer:true
		},{
			text:'授权编码',dataIndex:'PClient_LicenceCode',width:110,
			sortable:false,defaultRenderer:true
		},{
			text:'用户编号',dataIndex:'PClient_ClientNo',width:110,
			sortable:true,defaultRenderer:true
		},{
			text:'医院类别',dataIndex:'PClient_HospitalTypeName',width:70,
			sortable:false,defaultRenderer:true
		},{
			text:'医院等级',dataIndex:'PClient_HospitalLevelName',width:70,
			sortable:false,defaultRenderer:true
		},{
			text:'使用',dataIndex:'PClient_IsUse',
			width:35,align:'center',sortable:false,
			isBool: true,type: 'bool'
		},{
			text:'重复标记',dataIndex:'PClient_IsRepeat',
			width:55,	align: 'center',
			isBool: true,
			type: 'bool',
			sortable:false,
			defaultRenderer:true
		},{
			text:'合约用户',dataIndex:'PClient_IsContract',
			width:55,align:'center',sortable:false,
			isBool: true,type: 'bool'
		},{
			text:'主服务器授权号',dataIndex:'PClient_LRNo1',width:120,
			sortable:false,defaultRenderer:true
		},{
			text:'备份服务器授权号',dataIndex:'PClient_LRNo2',width:120,
			sortable:false,defaultRenderer:true
		},{
			text:'业务员',dataIndex:'PClient_Bman',width:70,
			sortable:false,defaultRenderer:true
		},{
			text:'客户主键ID',dataIndex:'PClient_Id',
			isKey:true,hidden:true,hideable:false
		},{
			text:'销售客户关系主键ID',dataIndex:'PClient_PSalesManClientLinkID',
			hidden:true,hideable:false
		}];
		
		return columns;
	},
	/**背景色处理*/
	onBackgroundRender:function(value,meta,record){
		var me = this;
		var PSalesManClientLinkID = record.get('PClient_PSalesManClientLinkID');
		if(PSalesManClientLinkID){
			meta.style = 'color:' + me.Color;
		}
		if (value) {
			value = Ext.typeOf(value) == 'string' ? value.replace(/"/g, '&quot;') : value;
			meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
		}
		return value;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		me.internalWhere=me.changeWhere();
		url = me.callParent(arguments);
		url += '&SalesManId=' + me.SalesManID;
		//所属客户
		if(me.OnlyClient){
			url += '&IsOwn=true';
		}
		return url;
	},
	

	changeWhere:function(){
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			search = buttonsToolbar.getComponent('search').getValue(),
			searchBman= buttonsToolbar.getComponent('searchBman').getValue(),
			IsContract=false,params = [];
			
        IsContract= buttonsToolbar.getComponent('IsContract').getValue();
		 //合约用户
		if(IsContract){
			params.push("(pclient.IsContract=1 )");
		}else{
			params.push("(pclient.IsContract=0 or pclient.IsContract =1 or pclient.IsContract is null)");
		}
		if(searchBman){
			params.push("(pclient.Bman like '%" + searchBman + "%')");
		}
		if(params.length > 0){
			me.internalWhere = params.join(' and ');
		}else{
			me.internalWhere = '';
		}
		if(search){
			if(me.internalWhere){
				me.internalWhere += ' and (' + me.getSearchWhere(search) + ')';
			}else{
				me.internalWhere = me.getSearchWhere(search);
			}
		}
		return me.internalWhere;
	},

	/**创建数据列*/
	createColumns: function() {
		var me = this,
			columns = me.callParent(arguments);
		
		//序号列修改
		columns.splice(0,1,{
			xtype: 'rownumberer',
			text: me.Shell_ux_grid_Panel.NumberText,
			width: me.rowNumbererWidth,
			align: 'center',
			renderer:function(value,meta,record,rowIndex,colIndex){
				var PSalesManClientLinkID = record.get('PClient_PSalesManClientLinkID');
				if(PSalesManClientLinkID){
					meta.style = 'color:' + me.Color;
				}
				meta.tdCls = Ext.baseCSSPrefix + 'grid-cell-special';
        		return me.store.indexOfTotal(record) + 1;
			}
		});

		return columns;
	},
	/**保存结束*/
	onSaveOver:function(){
		var me = this,
			count = me.addRecords.length + me.delRecords.length,
			now = me.addCount + me.delCount;
		//数量一致，整个保存处理结束
		if(count == now){
			me.fireEvent('save',me);
		}
	},
	/**判断是都在选中的列表中*/
	isInSelectRecords:function(array,id){
		var len = array.length,
			isInArray = false;
			
		for(var i=0;i<len;i++){
			var PSalesManClientLinkID = array[i].get('PClient_PSalesManClientLinkID');
			if(PSalesManClientLinkID == id){
				isInArray = true;
				break;
			}
		}
		
		return isInArray;
	},
		/**新增、删除数据*/
	onAddAndDel:function(){
		var me = this;
		me.fireEvent('beforesave',me);
		for(var i in me.addRecords){
			me.onAddOne(me.addRecords[i]);
		}
		for(var i in me.delRecords){
			me.onDelOne(me.delRecords[i]);
		}
	},
		/**导出用户信息*/
	onDownExcel:function(){
		var me=this;
		var where =me.changeWhere();
		var	buttonsToolbar = me.getComponent('buttonsToolbar');
		var	ShowText = buttonsToolbar.getComponent('ShowText');
		var url = JShell.System.Path.getRootUrl(me.downlUrl);
		url += '?operateType=0&type=PSalesManClientLink';
		url += '&SalesManId=' + me.SalesManID;
		//所属客户
		if(me.OnlyClient){
			url += '&IsOwn=true';
		}
		if(where){
			url += '&where='+where;
		}
		if(ShowText.text){
			url += '&filename='+ShowText.text;
		}
		window.open(url);
	}
});
	