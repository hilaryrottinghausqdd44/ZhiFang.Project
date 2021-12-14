Ext.define("Shell.class.weixin.dict.lab.BLabSampleType.contrast.SampleType",{
	extend:"Shell.ux.grid.Panel",
	PKField: 'SampleType_Id',
	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/DictionaryService.svc/ST_UDTO_SearchSampleTypeByHQL?isPlanish=true',
	/**默认加载*/
	defaultLoad: true,
	/**是否启用查询框*/
	hasSearch: true,
	/**查询框信息*/
	searchInfo: {
		width: 120,
		emptyText: '编码/名称',
		isLike: true,
		fields: ['sampletype.CName','sampletype.Id']
	},
	
	/**创建功能按钮栏*/
	createButtontoolbar: function() {
		var me = this,
			items = me.buttonToolbarItems || [];

		if (items.length == 0) {
			items.push({
				xtype:'label',
				text:'中心样本 ',
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
			text: '中心样本编码',
			dataIndex: 'SampleType_Id',
			width: 150,
			isKey: true,
			hideable: false
		}, {
			text: '中心样本名称',
			dataIndex: 'SampleType_CName',
			width: 150,
			sortable: true,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '中心样本简码',
			dataIndex: 'SampleType_ShortCode',
			width: 150,
			sortable: true,
			menuDisabled: true,
			defaultRenderer: true
		}];
		return columns;
	},
	
	getSampleType:function(){
		var me = this;
		var SampleType;
		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.selectUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true).join(',');
		
		JShell.Server.get(url, function(data) {
			if (data.success) {
                SampleType = data.value.list;
			} else {
                JShell.Msg.error(data.msg);
			}
			
		},false);
		return SampleType;
	},
});
