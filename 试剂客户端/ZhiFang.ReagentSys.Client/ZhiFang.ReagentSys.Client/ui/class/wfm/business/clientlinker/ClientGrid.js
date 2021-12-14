/**
 * 客户
 * @author liangyl
 * @version 2017-03-20
 */
Ext.define('Shell.class.wfm.business.clientlinker.ClientGrid', {
    extend: 'Shell.ux.grid.Panel',
    requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
    title: '客户列表',
    width: 800,
    height: 500,

   	/**获取数据服务路径*/
	selectUrl: '/SingleTableService.svc/ST_UDTO_SearchPClientByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl: '/SingleTableService.svc/ST_UDTO_UpdatePClientByField',
	/**删除数据服务路径*/
	delUrl: '/SingleTableService.svc/ST_UDTO_DelPClient',

    /**默认加载*/
    defaultLoad: true,

    /**是否启用刷新按钮*/
    hasRefresh: true,
    /**是否启用查询框*/
    hasSearch: true,
    afterRender: function () {
        var me = this;
        me.callParent(arguments);
        me.initFilterListeners();
    },
    initComponent: function () {
        var me = this;
        //查询框信息
        me.searchInfo = {
            width: 125, emptyText: '客户名称', isLike: true,itemId:'search',
            fields: ['pclient.Name']
        };
        me.buttonToolbarItems = ['refresh','-',{
			width:150,labelWidth:35,labelAlign:'right',
			xtype:'uxCheckTrigger',itemId:'ClientAreaName',fieldLabel:'区域',
			className:'Shell.class.wfm.dict.CheckGrid',
			classConfig:{
				title:'地理区域选择',
				defaultWhere:"pdict.BDictType.Id='" + JShell.WFM.GUID.DictType.ClientArea.value + "'"
			}
		},{
			xtype:'textfield',itemId:'ClientAreaID',fieldLabel:'区域主键ID',hidden:true
		},'->',{
			type: 'search',
			info: me.searchInfo
		}];
        //数据列
        me.columns = me.createGridColumns();
        me.callParent(arguments);
    },
    /**创建数据列*/
    createGridColumns: function () {
        var me = this;
        var columns = [ {
            text: '客户名称', dataIndex: 'PClient_Name', width: 180,
            sortable: false, defaultRenderer: true
        }, {
            text: '客户简称', dataIndex: 'PClient_SName', width: 150,
            sortable: false,hidden:true	, defaultRenderer: true
        } ,{
            text: '区域', dataIndex: 'PClient_ClientAreaName', width: 80,
            sortable: false, defaultRenderer: true
        },{
			text:'客户主键ID',dataIndex:'PClient_Id',
			isKey:true,hidden:true,hideable:false
		}];
        return columns;
    },
     /**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			PClientID=null,
			params=[],
			search = null;
			
		me.internalWhere = '';
			
		if(buttonsToolbar){
			search = buttonsToolbar.getComponent('search').getValue();
			PClientID = buttonsToolbar.getComponent('ClientAreaID').getValue();

		}
		if(PClientID){
			params.push("pclient.ClientAreaID='" + PClientID + "'");
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
	},
    /**区域监听*/
	doClientAreaListeners:function(){
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			CName = buttonsToolbar.getComponent('ClientAreaName'),
			Id = buttonsToolbar.getComponent('ClientAreaID');
			
		if(!CName) return;
		
		CName.on({
			check: function(p, record) {
				CName.setValue(record ? record.get('BDict_CName') : '');
				Id.setValue(record ? record.get('BDict_Id') : '');
				me.onSearch();
				p.close();
			}
		});
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
		me.doClientAreaListeners();
//		me.addAllData();
	},
	/**@overwrite 改变返回的数据*/
	changeResult: function(data) {
		var result = {},
			list = [],
			arr = [],
			obj = {};
	     obj={PClient_ClientAreaName:'',PClient_Name:'全部',
		PClient_SName:'',PClient_Id:'-1'};
		if(data.value){
			list=data.value.list;
			list.splice(0,0,obj);
		}else{
			list=[];
			list.push(obj);
		}
	    result.list =  list;
		return result;
	},
	/**禁用所有的操作功能*/
	disableControl: function() {
		this.enableControl(true);
	}
//		/**添加默认行（全部)*/
//	addAllData:function(){
//		var me = this;
//		var obj={PClient_ClientAreaName:'',PClient_Name:'全部',
//		PClient_SName:'',PClient_Id:'-1'};
//	    me.store.insert(0,obj);
//	    me.getSelectionModel().select(0);
//	},
//	/**清空数据,禁用功能按钮*/
//	clearData: function() {
//		var me = this;
//		me.disableControl(); //禁用 所有的操作功能
//		me.store.removeAll(); //清空数据
//		me.addAllData();
//	}
});