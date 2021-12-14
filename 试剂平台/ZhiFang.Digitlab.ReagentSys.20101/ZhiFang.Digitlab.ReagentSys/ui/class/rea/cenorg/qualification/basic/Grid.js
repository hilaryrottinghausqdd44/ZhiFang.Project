/***
 *  资质证件管理
 * @author longfc
 * @version 2017-07-14
 */
Ext.define('Shell.class.rea.cenorg.qualification.basic.Grid', {
	extend: 'Shell.ux.grid.Panel',
	requires: [
		'Shell.ux.form.field.DateArea',
		'Ext.ux.CheckColumn'
	],
	title: '资质证件管理',

	defaultLoad: true,
	autoSelect: true,
	sortableColumns: false,
	/**是否启用序号列*/
	hasRownumberer: true,

	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用查询框*/
	hasSearch: true,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,

	selectUrl: '/ReagentSysService.svc/ST_UDTO_SearchGoodsQualificationByHQL?isPlanish=true',
	defaultOrderBy: [{
		property: 'GoodsQualification_RegisterNo',
		direction: 'ASC'
	}],
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		//自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();

		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			text: '所属供应商',
			dataIndex: 'GoodsQualification_CompCName',
			width: 100,
			sortable: false,
			hidden: false,
			hideable: true,
			defaultRenderer: true
		}, {
			text: '编号',
			dataIndex: 'GoodsQualification_RegisterNo',
			width: 90,
			sortable: false,
			hideable: true,
			defaultRenderer: true
		}, {
			text: '显示名称',
			dataIndex: 'GoodsQualification_CName',
			//flex: 1,
			minWidth: 180,
			sortable: false,
			hidden: false,
			hideable: true,
			defaultRenderer: true
		}, {
			xtype: 'actioncolumn',
			text: '原件',
			align: 'center',
			width: 35,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					if(record.get("GoodsQualification_RegisterFilePath"))
						return 'button-search hand';
					else
						return '';
				},
				handler: function(grid, rowIndex, colIndex) {
					me.IsSearchForm = false;
					var record = grid.getStore().getAt(rowIndex);
					me.openRegisterFile(record);
				}
			}]
		}, {
			text: 'ID',
			dataIndex: 'GoodsQualification_Id',
			isKey: true,
			sortable: false,
			hidden: true,
			hideable: true,
			defaultRenderer: true
		}, {
			text: '有效开始',
			dataIndex: 'GoodsQualification_RegisterDate',
			isDate: true,
			width: 80,
			sortable: false,
			hideable: true,
			defaultRenderer: true
		}, {
			text: '有效截止',
			dataIndex: 'GoodsQualification_RegisterInvalidDate',
			//isDate: true,
			width: 80,
			sortable: false,
			hideable: true,
			renderer: function(value, meta, record, rowIndex, colIndex, s, v) {
				if(value) {
					var Sysdate = JShell.System.Date.getDate();
					value = Ext.util.Format.date(value, 'Y-m-d');
					var BGColor = "";
					Sysdate = Ext.util.Format.date(Sysdate, 'Y-m-d');
					Sysdate = JShell.Date.getDate(Sysdate);
					var RegisterInvalidDate = value;
					RegisterInvalidDate = JShell.Date.getDate(RegisterInvalidDate);
					var days = parseInt((RegisterInvalidDate - Sysdate) / 1000 / 60 / 60 / 24);
					if(days < 0) {
						BGColor = "red";
					} else if(days >= 0 && days <= 30) {
						BGColor = "#e97f36";
					} else if(days > 30) {
						BGColor = "#568f36";
					}
					if(BGColor)
						meta.style = 'background-color:' + BGColor + ';color:#ffffff;';
				}
				return value;
			}
		}, {
			dataIndex: 'GoodsQualification_ClassType',
			text: '资质类型',
			width: 70,
			renderer: function(value, meta) {
				var v = "";
				switch("" + value) {
					case "1":
						v = "机构资质";
						meta.style = "color:#ffffff;background-color:orange;";
						break;
					case "2":
						v = "授权资质";
						meta.style = "color:#ffffff;background-color:green;";
						break;
					case "3":
						v = "产品资质";
						meta.style = "color:#ffffff;background-color:blue;";
						break;
					default:
						break;
				}

				meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		}, {
			text: '所用医院',
			dataIndex: 'GoodsQualification_CenOrgCName',
			flex: 1,
			minWidth: 100,
			sortable: false,
			hidden: false,
			hideable: true,
			defaultRenderer: true
		}, {
			text: '授权人',
			dataIndex: 'GoodsQualification_AuthorizeCName',
			width: 90,
			sortable: false,
			hidden: false,
			hideable: true,
			defaultRenderer: true
		}, {
			text: '联系电话',
			dataIndex: 'GoodsQualification_Telephone',
			width: 90,
			sortable: false,
			hidden: false,
			hideable: true,
			defaultRenderer: true
		}, {
			text: '原件路径',
			dataIndex: 'GoodsQualification_RegisterFilePath',
			width: 120,
			hidden: true,
			sortable: false,
			hideable: true,
			defaultRenderer: true
		}, {
			text: '有效期提示',
			dataIndex: 'InvalidDateStatus',
			width: 75,
			sortable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				var v = value || '';
				meta = me.getmetaOfvalue(v, meta, record);
				return v;
			}
		}];
		return columns;
	},
	getmetaOfvalue: function(value, meta, record) {
		var BGColor = "";
		switch(value) {
			case "已过期失效":
				BGColor = "red";
				break;
			case "30天内到期":
				BGColor = "#e97f36";
				break;
			case "有效":
				BGColor = "#568f36";
				break;
			default:
				break;
		}
		if(BGColor)
			meta.style = 'background-color:' + BGColor + ';color:#ffffff;';
		return meta;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var end = new Date();
		var start = JShell.Date.getNextDate(end,-30);
		var items = ['refresh', '-'];
		if(me.hasAdd) items.push('add');
		if(me.hasEdit) items.push('edit');
		if(me.hasDel) items.push('del');
		if(me.hasShow) items.push('show');
		if(me.hasSave) items.push('save');
		if(me.hasAdd || me.hasEdit || me.hasDel || me.hasShow || me.hasSave) items.push('-');
		items.push({
			fieldLabel: '资质类型',
			labelWidth: 65,
			width: 160,
			itemId: 'ClassType',
			xtype: 'uxSimpleComboBox',
			value: '',
			hasStyle: true,
			data: [
				['', '全部', 'color:black;'],
				['1', '机构资质', 'color:green;'],
				['2', '授权资质', 'color:orange;'],
				['3', '产品资质', 'color:blue;']
			]
		},{
			xtype:'uxdatearea',itemId:'date',fieldLabel:'有效期',
			value:{start:start,end:end},
			listeners:{
				change:function(){
					setTimeout(function(){me.onSearch();},100);
				},
				enter:function(){
					me.onSearch();
				}
			}
		});
		//查询框信息
		me.searchInfo = {
			width: 200,
			emptyText: '编号/名称/授权人/联系电话',
			isLike: false,
			itemId: "search",
			fields: ['goodsqualification.RegisterNo', 'goodsqualification.CName', 'goodsqualification.AuthorizeCName', 'goodsqualification.Telephone', ]
		};
		items.push('-', '->', {
			type: 'search',
			info: me.searchInfo
		});

		return items;
	},
	getInvalidDateStatusData: function() {
		var me = this,
			data = [];
		data.push(['', '=全部=', 'font-weight:bold;color:#303030;text-align:center']);
		data.push(['<=30', '30天内到期', 'font-weight:bold;color:#e97f36;text-align:center']);
		data.push(['<=0', '已过期失效', 'font-weight:bold;color:red;text-align:center']);
		return data;
	},
	/*查看文件**/
	openRegisterFile: function(record) {
		var me = this;
		var id = "";
		if(record != null) {
			id = record.get('GoodsQualification_Id');
		}
		var maxWidth = document.body.clientWidth * 0.98;
		var height = document.body.clientHeight * 0.96;
		var config = {
			PK: id,
			height: height,
			width: maxWidth,
			resizable: false, //可变大小
			closable: true, //有关闭按钮
			draggable: true,
			listeners: {
				close: function(win) {
					me.IsSearchForm = true;
				}
			}
		};
		JShell.Win.open('Shell.class.rea.goods.qualification.basic.PreviewPDF', config).show();
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			classType = "",
			date=null,
			params = [],
			search = null;
		me.internalWhere = '';
		if(buttonsToolbar) {
			//search = buttonsToolbar.getComponent('search').getValue();
			classType = buttonsToolbar.getComponent('ClassType').getValue();
			date = buttonsToolbar.getComponent('date');
		}
		if(date) {
			var value = date.getValue();
			if(value) {
				if(value.start) {
					params.push("goodsqualification.RegisterDate>='" + JShell.Date.toString(value.start, true) + "'");
				}
				if(value.end) {
					params.push("(goodsqualification.RegisterInvalidDate is null or goodsqualification.RegisterInvalidDate<='" + JShell.Date.toString(value.end, true) + "')");
				}
			}
		}
		if(classType) {
			params.push("goodsqualification.ClassType=" + classType);
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
	changeResult: function(data) {
		var me = this;
		var Sysdate = JcallShell.System.Date.getDate();
		if(!Sysdate) Sysdate = new Date();
		for(var i = 0; i < data.list.length; i++) {
			var RegisterInvalidDate = data.list[i].GoodsQualification_RegisterInvalidDate;
			var InvalidDateStatus = "";
			if(RegisterInvalidDate) {
				RegisterInvalidDate = JShell.Date.getDate(RegisterInvalidDate);
				RegisterInvalidDate = Ext.util.Format.date(RegisterInvalidDate, 'Y-m-d');
				RegisterInvalidDate = JShell.Date.getDate(RegisterInvalidDate);
				Sysdate = Ext.util.Format.date(Sysdate, 'Y-m-d');
				Sysdate = JShell.Date.getDate(Sysdate);
				var days = parseInt((RegisterInvalidDate - Sysdate) / 1000 / 60 / 60 / 24);
				if(days < 0) {
					InvalidDateStatus = "已过期失效";
				} else if(days >= 0 && days <= 30) {
					InvalidDateStatus = "30天内到期";
				} else if(days > 30) {
					InvalidDateStatus = "有效";
				}
			}
			data.list[i].InvalidDateStatus = InvalidDateStatus;
		}
		return data;
	}
});