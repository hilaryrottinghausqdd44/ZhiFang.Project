/**
 * 所有可用模板
 * @author liangyl
 * @version 2017-11-23
 */
Ext.define('Shell.class.qms.equip.res.templetduty.TempletGrid', {
	extend: 'Shell.class.qms.equip.templet.basic.Grid',
	title: '模板列表',
	requires: [
		'Shell.class.qms.equip.templet.basic.CheckTrigger',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.BoolComboBox'
	],
		/**默认加载数据*/
	defaultLoad: false,
	/**是否启用查询框*/
	hasSearch: true,
	 /**是否启用新增按钮*/
	hasAdd: true,
	/**是否启用删除按钮*/
	hasDel: true,
	
    /**后台排序*/
	remoteSort: false,
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
			itemId: 'search',fields: ['etemplet.CName']
		};
		
		buttonToolbarItems.push('refresh','-',{
				fieldLabel: '仪器主键ID',hidden: true,xtype: 'textfield',
				name: 'ETemplet_Equip_Id',itemId: 'ETemplet_Equip_Id'
			}, {
				fieldLabel: '仪器',labelWidth:35,emptyText: '仪器',
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
			type: 'search',
			info: me.searchInfo
		});
		return buttonToolbarItems;
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		me.regStr = new RegExp('"', "g");
		var columns=[{
			text: '编号',dataIndex: 'ETemplet_Id',width: 160,sortable: false,hidden:true,defaultRenderer: true
		},{
			text: '模板名称',dataIndex: 'ETemplet_CName',flex:1,minWidth:280,sortable: true,renderer: function(value, meta, record) {
            	var v=me.showMemoText(value, meta, record);
				return v;
			}
		},{
			text: '小组',dataIndex: 'ETemplet_Section_Id',width: 120,
			hidden:true,sortable: false,defaultRenderer: true
		},{
			text: '仪器',dataIndex: 'ETemplet_EEquip_CName',width: 120,
			sortable: true,renderer: function(value, meta, record) {
            	var v=me.showMemoText(value, meta, record);
				return v;
			}
		},{
			text: '仪器id',dataIndex: 'ETemplet_Equip_Id',width: 150,
			sortable: false,defaultRenderer: true,hidden: true
		},{
			text: '类型id',dataIndex: 'ETemplet_TempletType_Id',
			width: 120,hidden:true,sortable: false,defaultRenderer: true
		},{
			text: '类型',dataIndex: 'ETemplet_TempletType_CName',
			width: 120,sortable: true,renderer: function(value, meta, record) {
            	var v=me.showMemoText(value, meta, record);
				return v;
			}
		},{
			text: '小组',dataIndex: 'ETemplet_Section_CName',
			width: 120,sortable: true,renderer: function(value, meta, record) {
            	var v=me.showMemoText(value, meta, record);
				return v;
			}
		},{
			text: '代码',dataIndex: 'ETemplet_UseCode',width: 80,
			hidden: false,sortable: true,defaultRenderer: true
		}];
		return columns;
	},
		/**显示*/
	showMemoText:function(value, meta, record){
		var me=this;
		var qtipValue ='';
	    var CName = ""+ record.get("ETemplet_CName");
	    var EEquipCName =""+ record.get("ETemplet_EEquip_CName");
	  	var TempletTypeCName =""+ record.get("ETemplet_TempletType_CName");
	  	var SectionCName =""+ record.get("ETemplet_Section_CName");

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
			var index = me.defaultWhere.indexOf('etemplet.IsUse=1');
			if(index == -1){
				me.defaultWhere += ' and etemplet.IsUse=1';
			}
		}else{
			me.defaultWhere = 'etemplet.IsUse=1';
		}
	},
	 /**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			EquipId = null,HRDeptId = null,
			search = null,params = [];
			
		//改变默认条件
		me.changeDefaultWhere();
		me.internalWhere = '';	
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
	},
	/**仪器选择确认处理*/
	onEquipAccept: function(record) {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			Id = buttonsToolbar.getComponent('ETemplet_Equip_Id'),
			Name = buttonsToolbar.getComponent('ETemplet_EquipID');
		Id.setValue(record ? record.get('EEquip_Id') : '');
		Name.setValue(record ? record.get('EEquip_CName') : '');
	}
});