/**
 * 仪器模板选择列表
 * @author liangyl
 * @version 2016-08-16
 */
Ext.define('Shell.class.qms.equip.templet.basic.CheckGrid', {
    extend:'Shell.ux.grid.CheckPanel',
    title:'仪器模板选择列表',
    requires: [
		'Shell.class.qms.equip.templet.basic.CheckTrigger',
		'Shell.ux.form.field.CheckTrigger'
	],
    width:800,
    height:380,
	/**获取数据服务路径*/
	selectUrl: '/QMSReport.svc/ST_UDTO_SearchETempletByHQL?isPlanish=true',
    /**是否单选*/
	checkOne:false,
    /**后台排序*/
	remoteSort: false,
	/**默认每页数量*/
	defaultPageSize: 200,
	initComponent:function(){
		var me = this;
		//数据列
		me.columns = me.createGridColumns();
		me.initButtonToolbarItems();
		me.callParent(arguments);
	},
		/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			text: '编号',dataIndex: 'ETemplet_Id',
			width: 160,sortable: false,hidden:true,defaultRenderer: true
		},{
			text: '名称',dataIndex: 'ETemplet_CName',
			width: 250,sortable: true,defaultRenderer: true
		},{
			text: '小组',dataIndex: 'ETemplet_Section_Id',width: 120,
			hidden:true,sortable: false,defaultRenderer: true
		},{
			text: '仪器',dataIndex: 'ETemplet_EEquip_CName',width: 120,
			sortable: true,defaultRenderer: true
		},{
			text: '仪器id',dataIndex: 'ETemplet_Equip_Id',width: 150,
			sortable: false,defaultRenderer: true,hidden: true
		},{
			text: '类型id',dataIndex: 'ETemplet_TempletType_Id',
			width: 120,hidden:true,sortable: false,defaultRenderer: true
		},{
			text: '类型',dataIndex: 'ETemplet_TempletType_CName',
			width: 120,sortable: true,defaultRenderer: true
		},{
			text: '小组',dataIndex: 'ETemplet_Section_CName',
			width: 120,sortable: true,defaultRenderer: true
		}];
		return columns;
	},
	
	initButtonToolbarItems:function(){
		var me = this;
		//自定义按钮功能栏
		me.buttonToolbarItems = [];
		//查询框信息
		me.searchInfo = {width: 145,emptyText: '模板名称',isLike: true,
			itemId: 'search',fields: ['etemplet.CName']
		};
		me.buttonToolbarItems.push('refresh','-',{
				fieldLabel: '仪器主键ID',hidden: true,xtype: 'textfield',
				name: 'ETemplet_Equip_Id',itemId: 'ETemplet_Equip_Id'
			}, {
				fieldLabel: '仪器',labelWidth:35,
				width: 185,	labelAlign: 'right',xtype: 'uxCheckTrigger',
				name: 'ETemplet_EquipID',itemId: 'ETemplet_EquipID',className: 'Shell.class.qms.equip.CheckGrid',
				listeners: {
					check: function(p, record) {
						me.onEquipAccept(record);
						p.close();
						me.onSearch();
					}
				}
			},{
			fieldLabel:'小组',labelAlign: 'right',
			emptyText:'小组',labelWidth:35,width: 185,	
			name:'HRDept_CName',itemId:'HRDept_CName',xtype:'uxCheckTrigger1',
			className:'Shell.class.qms.equip.templet.basic.CheckTree',
			classConfig: {
				title: '小组选择',
				/**是否显示根节点*/
	            rootVisible:false
			},
			listeners: {
				check: function(p, record) {
					var	buttonsToolbar = me.getComponent('buttonsToolbar'),
				        Id = buttonsToolbar.getComponent('HRDept_Id'),
			            CName = buttonsToolbar.getComponent('HRDept_CName');
                    if(record==null){
			    		CName.setValue('');
				    	Id.setValue('');
				    	p.close();
			    	    me.onSearch();
			    	    return;
			    	}
			    	if(record.data){
			    		CName.setValue(record.data ? record.data.text : '');
				    	Id.setValue(record.data ? record.data.tid : '');
				    	p.close();
			    	    me.onSearch();
			    	}
				}
			}
		},{
			fieldLabel:'小组ID',hidden:true,
			name:'HRDept_Id',xtype: 'textfield',itemId:'HRDept_Id'
		},{
			type: 'search',info: me.searchInfo
		});
		me.buttonToolbarItems.push({
			text:'确定',tooltip:'确定',iconCls:'button-accept',funName:'Accept',
			handler: function() {
				me.fireEvent('accept',me);
			}
		});
		return me.buttonToolbarItems;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
		    buttonsToolbar = me.getComponent('buttonsToolbar'),
			EquipId=null,
			HRDeptId =null,
		    params = [];
		 if(buttonsToolbar){
		 	EquipId = buttonsToolbar.getComponent('ETemplet_Equip_Id').getValue();
		    HRDeptId=buttonsToolbar.getComponent('HRDept_Id').getValue();
		    search = buttonsToolbar.getComponent('search').getValue();
		 }
		 
		if(EquipId) {
			params.push("etemplet.EEquip.Id=" + EquipId);
		}
		if(HRDeptId) {
			params.push("etemplet.Section.Id=" + HRDeptId);
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
		/**仪器选择确认处理*/
	onEquipAccept: function(record) {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			Id = buttonsToolbar.getComponent('ETemplet_Equip_Id'),
			Name = buttonsToolbar.getComponent('ETemplet_EquipID');
		Id.setValue(record ? record.get('EEquip_Id') : '');
		Name.setValue(record ? record.get('EEquip_CName') : '');
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
	}
});