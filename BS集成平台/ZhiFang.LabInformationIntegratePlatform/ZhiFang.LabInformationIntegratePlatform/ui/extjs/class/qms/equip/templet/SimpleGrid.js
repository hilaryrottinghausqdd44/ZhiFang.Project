/**
 * 仪器模板列表（简单）
 * @author liangyl
 * @version 2016-08-12
 */
Ext.define('Shell.class.qms.equip.templet.SimpleGrid', {
	extend: 'Shell.ux.grid.Panel',
	title: '仪器模板列表',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.BoolComboBox'
	],
	/**获取数据服务路径*/
	selectUrl: '/QMSReport.svc/ST_UDTO_SearchETempletByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl: '/QMSReport.svc/ST_UDTO_UpdateETempletByField',
	/**删除数据服务路径*/
	delUrl: '/QMSReport.svc/ST_UDTO_DelETemplet',
	/**默认排序字段*/
	defaultOrderBy: [{
		property: 'ETemplet_DispOrder',
		direction: 'ASC'
	}],

	/**默认加载数据*/
	defaultLoad: true,
	/**默认选中数据*/
	autoSelect: true,
	/**主键列*/
	PKField: 'ETemplet_Id',
	checkOne: true,
	IsbtnAccept: false,
	hasEquip:true,
	/**后台排序*/
	remoteSort: true,
	/**删除模板及相关数据*/
	hasDelTempletData: false,
	/**默认每页数量*/
	defaultPageSize: 200,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);  
	},
	initComponent: function() {
		var me = this;
		//查询框信息
		me.searchInfo = {width: 145,emptyText: '模板名称',isLike: true,
			itemId: 'search',fields: ['etemplet.CName']
		};
		me.buttonToolbarItems = ['refresh', '-'];
		if(me.hasAdd) {
			me.buttonToolbarItems.push('add')
		}
		if(me.hasDel) {
			me.buttonToolbarItems.push('del');
		}
		if(me.hasSave) {
			me.buttonToolbarItems.push('save');
		}
		var userid=JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		if(me.hasDelTempletData && !userid){
			me.buttonToolbarItems.push({
				text:'删除模板及相关数据',tooltip:'删除模板及相关数据',
				iconCls:'button-del',
				handler: function() {
					me.onSaveDelTempletData();
				}
			});
		}
		if(me.hasSearch) {
			me.buttonToolbarItems.push('->', {
				type: 'search',
				info: me.searchInfo
			});
		}
		if(me.hasEquip==true) {
			me.buttonToolbarItems.push({
				fieldLabel: '主键ID',
				hidden: true,
				xtype: 'textfield',
				name: 'ETemplet_Equip_Id',
				itemId: 'ETemplet_Equip_Id'
			}, {
				fieldLabel: '仪器',emptyText: '仪器',labelWidth:40,width: 185,labelAlign: 'right',xtype: 'uxCheckTrigger',
				name: 'ETemplet_EquipID',itemId: 'ETemplet_EquipID',className: 'Shell.class.qms.equip.CheckGrid',
				listeners: {
					check: function(p, record) {
						me.onEquipAccept(record);
						p.close();
						me.onSearch();
					}
				}
			},'-', {
				type: 'search',info: me.searchInfo},{
				width: 60,iconCls: 'button-search',margin: '0 0 0 10px',
				xtype: 'button',hidden:true,text: '查询',tooltip: '<b>查询</b>',
				handler: function() {
					me.onSearch();
				}
			});
			if(me.IsbtnAccept == true) {
				me.buttonToolbarItems.push({
					text:'确定',tooltip:'确定',
					iconCls:'button-accept',
					funName:'Accept',
					handler: function() {
						me.fireEvent('accept',me);
					}
				});
			}
			
			if(!me.checkOne) {
				me.multiSelect = true;
				me.selType = 'checkboxmodel';
			}
		}
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1
		});
		//创建数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**删除模板及相关数据*/
	onSaveDelTempletData:function(){
		var me=this;
		
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
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			text: '编号',
			dataIndex: 'ETemplet_Id',
			width: 160,
			sortable: false,
			hidden:true,
			defaultRenderer: true
		},{
			text: '名称',
			dataIndex: 'ETemplet_CName',
			width: 250,
			sortable: true,
			defaultRenderer: true
		},{
			text: '小组',
			dataIndex: 'ETemplet_Section_Id',
			width: 120,
			hidden:true,
			sortable: false,
			defaultRenderer: true
		},{
			text: '仪器',
			dataIndex: 'ETemplet_EEquip_CName',
			width: 120,
//			flex:1,
//			hidden: !me.hasEquip,
			sortable: true,
			defaultRenderer: true
		},{
			text: '仪器id',
			dataIndex: 'ETemplet_Equip_Id',
			width: 150,
			sortable: false,
			defaultRenderer: true,
			hidden: true
		},{
			text: '类型id',
			dataIndex: 'ETemplet_TempletType_Id',
			width: 120,
			hidden:true,
			sortable: false,
			defaultRenderer: true
		},{
			text: '类型',
			dataIndex: 'ETemplet_TempletType_CName',
			width: 120,
			sortable: true,
			defaultRenderer: true
		},{
			text: '小组',
			dataIndex: 'ETemplet_Section_CName',
			width: 120,
			sortable: true,
			defaultRenderer: true
		}];
		return columns;
	},
    /**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			arr = [];

		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.selectUrl;

		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true).join(',');

		//默认条件
		if (me.defaultWhere && me.defaultWhere != '') {
			arr.push(me.defaultWhere);
		}
		//内部条件
		if (me.internalWhere && me.internalWhere != '') {
			arr.push(me.internalWhere);
		}
		//外部条件
		if (me.externalWhere && me.externalWhere != '') {
			arr.push(me.externalWhere);
		}
	
		var buttonsToolbar = me.getComponent('buttonsToolbar'),
		EquipId = buttonsToolbar.getComponent('ETemplet_Equip_Id'),
		search = buttonsToolbar.getComponent('search').getValue(),

		params = [];
		if(EquipId) {
			var searchValue=EquipId.getValue();
			if(searchValue!='' && searchValue!=null){
				params.push("etemplet.EEquip.Id=" + searchValue);
			}
		}
		if(search) {
			params.push( '(' + "etemplet.CName like '%" + search + "%'" + ')');
		}
		
		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
        return me.callParent(arguments);
	}
});