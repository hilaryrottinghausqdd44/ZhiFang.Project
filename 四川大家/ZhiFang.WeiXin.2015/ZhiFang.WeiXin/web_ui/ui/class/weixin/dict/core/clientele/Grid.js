/**
 * 中心医疗机构字典表
 * @author liangyl
 * @version 2018-02-01
 */
Ext.define('Shell.class.weixin.dict.core.clientele.Grid', {
	extend: 'Shell.ux.grid.IsUseGrid',
	requires: [
	    'Ext.ux.CheckColumn',
	    'Shell.ux.form.field.CheckTrigger'
	],
	
	title: '中心医疗机构字典表 ',
	width: 800,
	height: 500,
  	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_SearchCLIENTELEByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl:'/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_UpdateCLIENTELEByField',
	/**删除数据服务路径*/
	delUrl:'/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_DelCLIENTELE',
	/**获取区域数据服务路径*/
	selectAreaUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_SearchClientEleAreaByHQL?isPlanish=true',    
    /**是否启用修改按钮*/
	hasEdit:false,
	/**默认加载*/
	defaultLoad: true,
	
	/**查询栏参数设置*/
	searchToolbarConfig:{},
	/**复制按钮点击次数*/
    copyTims:0,
    AreaEnum:{},
    AreaList:[],
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.initFilterListeners();
	},
	initComponent: function() {
		var me = this;
		me.getAreaInfo();
		//数据列
		me.columns = me.createGridColumns();
		//创建功能按钮栏Items
		me.buttonToolbarItems = me.createButtonToolbarItems();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		var columns = [{
			text:'所属区域',dataIndex:'CLIENTELE_AreaID',width:120,
			sortable:false,menuDisabled:true,
			renderer: function(value, meta, record, rowIndex, colIndex, s, v) {
				var v = value;
				if(me.AreaEnum != null){
					v = me.AreaEnum[v];
				}
				return v;
			}
		},{
			text:'编号',dataIndex:'CLIENTELE_Id',width:120,
			isKey:true,sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'中文名称',dataIndex:'CLIENTELE_CNAME',minWidth:150,
			flex:1,sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'英文名称',dataIndex:'CLIENTELE_ENAME',width:150,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'简码',dataIndex:'CLIENTELE_SHORTCODE',width:150,
			sortable:false,menuDisabled:true,defaultRenderer:true
		}];
		
		return columns;
	},
	/**创建功能按钮栏Items*/
	createButtonToolbarItems:function(){
		var me = this,
			buttonToolbarItems = me.buttonToolbarItems || [];
			//查询框信息
		me.searchInfo = {width:135,emptyText:'编号/名称/简码',isLike:true,
			itemId: 'search',fields:['clientele.CNAME','clientele.SHORTCODE','clientele.Id']};

		buttonToolbarItems.unshift('refresh','-','add','edit','del',{
			fieldLabel: '',labelWidth: 0,width: 135,labelSeparator:'',
			emptyText: '区域',
			name: 'AreaName',
			itemId: 'AreaName',
			xtype: 'uxCheckTrigger',
			labelAlign: 'right',
	        className: 'Shell.class.weixin.doctor.AreaCheckGrid',labelWidth: 32,
			width: 135,
			classConfig: {
				title: '区域选择',
				checkOne:true
			},
			listeners: {
				check: function(p, record) {
					me.onAreaAccept(record);
					p.close();
				}
			}
		}, {
			fieldLabel: '',
			emptyText: '区域',
			name: 'AreaID',
			itemId: 'AreaID',
			hidden: true,
			xtype: 'textfield'
		},{
				type: 'search',
				info: me.searchInfo
			});
		return buttonToolbarItems;
	},
	/**获取查询框内容*/
	getSearchWhere: function(value) {
		var me = this;
		//查询栏不为空时先处理内部条件再查询
		var searchInfo = me.searchInfo,
			isLike = searchInfo.isLike,
			fields = searchInfo.fields || [],
			len = fields.length,
			where = [];

		for (var i = 0; i < len; i++) {
			if(i == 'clientele.AreaID'){
				if(!isNaN(value)){
					where.push("clientele.AreaID=" + value);
				}
				continue;
			}
			if (isLike) {
				where.push(fields[i] + " like '%" + value + "%'");
			} else {
				where.push(fields[i] + "='" + value + "'");
			}
		}
		return where.join(' or ');
	},
	/**初始化监听*/
	initFilterListeners: function() {
		var me = this;
	},
	onAreaAccept:function(record){
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
	    var AreaID = buttonsToolbar.getComponent('AreaID');
		var AreaName = buttonsToolbar.getComponent('AreaName');
	    AreaID.setValue(record ? record.get('ClientEleArea_Id') : '');
		AreaName.setValue(record ? record.get('ClientEleArea_AreaCName') : '');
		me.onSearch();
	},
	
	/**@overwrite 修改按钮点击处理方法*/
	onEditClick:function(){
		var me = this;
	    me.fireEvent('onEditClick', me);
	},
	onAddClick:function(){
		var me =this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
	    var AreaID = buttonsToolbar.getComponent('AreaID');
	    var AreaName = buttonsToolbar.getComponent('AreaName');
		me.fireEvent('onAddClick', AreaID.getValue(),AreaName.getValue());
	},

	/**获取区域信息*/
	getAreaInfo:function(){
		var me = this;
		var url = JShell.System.Path.ROOT + me.selectAreaUrl;
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
		},false);
	},
	
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			search = null,AreaID=null,
			params = [];
	    AreaID = buttonsToolbar.getComponent('AreaID').getValue();
	
		//改变默认条件
//		me.changeDefaultWhere();
			
		me.internalWhere = '';
			
		if(buttonsToolbar){
			search = buttonsToolbar.getComponent('search').getValue();
		}
		
		if(AreaID){
			params.push("clientele.AreaID=" + AreaID);
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