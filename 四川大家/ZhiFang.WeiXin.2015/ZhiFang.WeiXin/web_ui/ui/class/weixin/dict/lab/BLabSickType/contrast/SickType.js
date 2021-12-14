Ext.define("Shell.class.weixin.dict.lab.BlabSickType.contrast.SickType",{
	extend:"Shell.ux.grid.Panel",
	PKField: 'SickType_Id',
	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/DictionaryService.svc/ST_UDTO_SearchSickTypeByHQL?isPlanish=true',
	/**默认加载*/
	defaultLoad: true,
	/**是否启用查询框*/
	hasSearch: true,
	/**查询框信息*/
	searchInfo: {
		width: 120,
		emptyText: '编码/名称',
		isLike: true,
		fields: ['sicktype.CName','sicktype.Id']
	},
	
	/**创建功能按钮栏*/
	createButtontoolbar: function() {
		var me = this,
			items = me.buttonToolbarItems || [];

		if (items.length == 0) {
			items.push({
				xtype:'label',
				text:'中心就诊类型 ',
				style: "font-weight:bold;color:#0000EE;"
			});
			if (me.hasRefresh) items.push('refresh');
			if (me.hasAdd) items.push('add');
			if (me.hasEdit) items.push('edit');
			if (me.hasDel) items.push('del');
			if (me.hasShow) items.push('show');
			if (me.hasSave) items.push('save');
			if (me.hasSearch) items.push('->', {
				type: 'search',
				info: me.searchInfo
			});
		}

		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'buttonsToolbar',
			items: items
		});
	},
	
	initComponent:function(){
		var me =this;
		me.columns = me.createGcolumns();
		me.callParent(arguments);
	},
	
	createGcolumns:function(){
		var me =this;
		var columns = [{
			text: '中心就诊类型编码',
			dataIndex: 'SickType_Id',
			width: 150,
			isKey: true,
			hideable: false
		}, {
			text: '中心就诊类型名称',
			dataIndex: 'SickType_CName',
			width: 150,
			sortable: true,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '中心就诊类型简码',
			dataIndex: 'SickType_ShortCode',
			width: 150,
			sortable: true,
			menuDisabled: true,
			defaultRenderer: true
		}];
		return columns;
	},
	
	getSickType:function(){
		var me = this;
		var SickType;
		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.selectUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true).join(',');
		
		JShell.Server.get(url, function(data) {
			if (data.success) {
                SickType = data.value.list;
			} else {
                JShell.Msg.error(data.msg);
			}
			
		},false);
		return SickType;
	},
});
