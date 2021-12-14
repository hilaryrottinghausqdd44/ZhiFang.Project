Ext.define("Shell.class.weixin.dict.lab.BLabSickType.contrast.Grid", {
	extend: "Shell.ux.grid.Panel",

	selectUrl: '/ServerWCF/DictionaryService.svc/ST_UDTO_SearchBLabSickTypeAndControl',
	/**是否启用修改按钮*/
	hasEdit: false,
	/**是否启用保存按钮*/
	hasSave: false,
	/**默认加载*/
	defaultLoad: false,
	/**是否启用查询框*/
	hasSearch: true,
	/**查询框信息*/
	searchInfo: {
		width: 120,
		emptyText: '编码/名称',
		isLike: true,
		fields: ['blabsicktype.CName',"blabsicktype.LabSickTypeNo"]
	},
	//实验室id
	labCode: '',
	//对照状态
	controlType: '',
	initComponent: function() {
		var me = this;
		me.columns = me.createGcolumns();
		me.callParent(arguments);
	},

	createGcolumns: function() {
		var me = this;
		var cloumns = [{
				text: '',
				dataIndex: 'BLabSickType_isContrast',
				width: 10,
				sortable: true,
				menuDisabled: true,
				align: 'left',
				renderer: function(value, meta, record) {
					var v = "",
						Color = '';
					var isContrast = record.get('BLabSickType_isContrast');
					var UseFlag = record.get('BLabSickType_UseFlag');
					//禁用
					if(UseFlag == '0') {
						Color = '<span style="padding:0px;color:gray; border:solid 3px gray"></span>'
					}
					//启用 并且对照状态=未对照
					if(UseFlag == '1' && isContrast == "" ) {
						Color = '<span style="padding:0px;color:Violet; border:solid 3px Violet"></span>&nbsp;'
					}
					//启用 并且对照状态=已对照
					if(UseFlag == '1' && isContrast != "") {
						Color = '<span style="padding:0px;color:white;"></span>'
					}
					v = Color;
					return v;
				}
			},
			{
				text: 'SickTypeID',
				dataIndex: 'BLabSickType_Id',
				width: 100,
				isKey: true,
				hidden: true,
				hideable: false
			},
			{
				text: 'LabCode',
				dataIndex: 'BLabSickType_LabCode',
				width: 100,
				hidden: true,
				menuDisabled: true
			}, {
				text: '实验室就诊类型编码',
				dataIndex: 'BLabSickType_LabSickTypeNo',
				width: 150,
				menuDisabled: true
			}, {
				text: '实验室就诊类型名称',
				dataIndex: 'BLabSickType_CName',
				width: 150,
				menuDisabled: true
			}, {
				text: '实验室就诊类型简码',
				sortable: false,
				dataIndex: 'BLabSickType_ShortCode',
				width: 150,
				menuDisabled: true
			}, {
				text: '中心就诊类型编码',
				sortable: false,
				dataIndex: 'BLabSickType_sickTypeId',
				width: 150,
				menuDisabled: true
			}, {
				text: '中心就诊类型名称',
				dataIndex: 'BLabSickType_sickTypeCname',
				sortable: false,
				width: 150,
				menuDisabled: true
			},
			{
				text: '是否在用',
				dataIndex: 'BLabSickType_UseFlag',
				sortable: false,
				width: 100,
				hidden: true,
				menuDisabled: true,
			}
		];
		return cloumns;
	},

	/**重写   创建功能按钮栏*/
	createButtontoolbar: function() {
		var me = this,
		items = [];
		items.push({
			xtype: "label",
			text: '中心就诊表对照关系',
			style: "font-weight:bold;color:#0000EE;"
		}, '-', {
			xtype: "radiogroup",
			vertical: true,
			width: 200,
			cloumns: 3,
			height: 25,
			items: [{
					boxLabel: "全部",
					name: "rb",
					inputValue: 0,
					checked: true
				},
				{
					boxLabel: "已对照",
					name: 'rb',
					inputValue: 1
				},
				{
					boxLabel: "未对照",
					name: 'rb',
					inputVaule: 2,
				}
			],
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					if(!me.labCode) {
						JShell.Msg.error('实验室编号为空!');
						return;
					}
					if(!newValue) {
						JShell.Msg.error('查询类型为空!');
						return;
					}
					if(newValue.rb == "on") {
						me.controlType = 2;
					} else {
						me.controlType = newValue.rb;
					}
					me.store.reload();
				}
			}

		}, '->', {
			type: 'search',
			info: me.searchInfo
			//				xtype:'trigger',
			//				triggerCls:'x-form-search-trigger',
			//				emptyText:'就诊名称',
		});

		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'buttonsToolbar',
			items: items
		});
	},

	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			arr = [];

		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.selectUrl;

		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true).join(',');
		if(me.controlType == null || me.controlType.length <= 0) {
			me.controlType = 0;
		}
		url += "&labCode=" + me.labCode + '&controlType=' + me.controlType;

		//默认条件
		if(me.defaultWhere && me.defaultWhere != '') {
			arr.push(me.defaultWhere);
		}
		//内部条件
		if(me.internalWhere && me.internalWhere != '') {
			arr.push(me.internalWhere);
		}
		//外部条件
		if(me.externalWhere && me.externalWhere != '') {
			arr.push(me.externalWhere);
		}
		var where = arr.join(") and (");
		if(where) where = "(" + where + ")";

		if(where) {
			url += '&where=' + JShell.String.encode(where);
		} else {
			url += "&where=";
		}

		return url;
	},
	
	getLabSickType:function(){
		var me = this;
		var labSickType;
		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.selectUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true).join(',');
		url += "&labCode=" + me.labCode + '&controlType=2&where=';
		
		JShell.Server.get(url, function(data) {
			if (data.success) {
                labSickType = data.value.list;
			} else {
                JShell.Msg.error(data.msg);
			}
			
		},false);
		return labSickType;
	},
});