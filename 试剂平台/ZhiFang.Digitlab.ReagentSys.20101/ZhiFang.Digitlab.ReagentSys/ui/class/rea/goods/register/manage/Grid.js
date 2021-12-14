/***
 *  注册证管理
 * @author longfc
 * @version 2017-05-19
 */
Ext.define('Shell.class.rea.goods.register.manage.Grid', {
	extend: 'Shell.class.rea.goods.register.basic.Grid',
	requires: [
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '注册证管理',

	hasAdd: true,
	hasEdit: true,
	hasDel: true,
	hasSave: true,
	/**选择行或点击行后是否联动查询表单*/
	IsSearchForm: true,
	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',

	/**修改服务地址*/
	editUrl: '/ReagentSysService.svc/ST_UDTO_UpdateGoodsRegisterByField',
	delUrl: '/ReagentSysService.svc/ST_UDTO_DelGoodsRegister',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		var cenOrgID = JShell.REA.System.CENORG_ID;
		me.defaultWhere = me.defaultWhere || '';
		if(me.defaultWhere) {
			me.defaultWhere = me.defaultWhere + " and goodsregister.CenOrgID=" + cenOrgID;
		} else {
			me.defaultWhere = "goodsregister.CenOrgID=" + cenOrgID;
		}
		me.buttonToolbarItems = me.createButtonToolbarItems();
		me.callParent(arguments);
	},
	/**创建功能按钮栏Items*/
	createButtonToolbarItems: function() {
		var me = this,
			items = me.buttonToolbarItems || [];
		if(me.hasRefresh) items.push('refresh');
		if(me.hasAdd) items.push('add');
		if(me.hasEdit) items.push('edit');
		if(me.hasDel) items.push('del');
		if(me.hasShow) items.push('show');
		if(me.hasSave) items.push('save');
		items.push('-', {
			width: 215,
			labelWidth: 75,
			labelAlign: 'right',
			hasStyle: true,
			xtype: 'uxSimpleComboBox',
			itemId: 'InvalidDateStatus',
			fieldLabel: '有效期选择',
			data: me.getInvalidDateStatusData(),
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					me.onSearch();
				}
			}
		});
		me.searchInfo = {
			width: 300,
			emptyText: '中文名称/英文名称/产品编号/注册证编号',
			isLike: false,
			itemId: "search",
			fields: ['goodsregister.CName', 'goodsregister.EName', 'goodsregister.GoodsNo', 'goodsregister.RegisterNo']
		};
		items.push('->', {
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
	createGridColumns: function() {
		var me = this;
		var columns = me.callParent(arguments);
		columns.splice(1, 0, {
			xtype: 'checkcolumn',
			text: '使用',
			dataIndex: 'GoodsRegister_Visible',
			width: 40,
			align: 'center',
			sortable: false,
			menuDisabled: true,
			stopSelection: false,
			type: 'boolean'
		});
		columns.splice(-2, 0, {
			text: '有效期提示',
			dataIndex: 'InvalidDateStatus',
			width: 75,
			sortable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				var v = value || '';
				meta = me.getmetaOfvalue(v, meta, record);
				return v;
			}
		});
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
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick: function() {
		this.fireEvent('addclick', this);
	},
	/**@overwrite 新增按钮点击处理方法*/
	onEditClick: function() {
		this.fireEvent('editclick', this);
	},
	onSaveClick: function() {
		var me = this,
			records = me.store.getModifiedRecords(), //获取修改过的行记录
			len = records.length;

		if(len == 0) return;

		me.showMask(me.saveText); //显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;

		for(var i = 0; i < len; i++) {
			var rec = records[i];
			var id = rec.get(me.PKField);
			var Visible = rec.get('GoodsRegister_Visible');
			me.updateOneByVisible(i, id, Visible);
		}
	},
	updateOneByVisible: function(index, id, Visible) {
		var me = this;
		var url = JShell.System.Path.getUrl(me.editUrl);
		if(Visible == false || Visible == "false") Visible == 0;
		if(Visible == "1" || Visible == "true" || Visible == true) Visible == 1;
		Visible = Visible == true ? 1 : 0;
		var params = Ext.JSON.encode({
			entity: {
				Id: id,
				Visible: Visible
			},
			fields: 'Id,Visible'
		});

		setTimeout(function() {
			JShell.Server.post(url, params, function(data) {
				var record = me.store.findRecord(me.PKField, id);
				if(data.success) {
					if(record) {
						record.set(me.DelField, true);
						record.commit();
					}
					me.saveCount++;
				} else {
					me.saveErrorCount++;
					if(record) {
						record.set(me.DelField, false);
						record.commit();
					}
				}
				if(me.saveCount + me.saveErrorCount == me.saveLength) {
					me.hideMask(); //隐藏遮罩层
					if(me.saveErrorCount == 0) me.onSearch();
				}
			});
		}, 100 * index);
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			Status = null,
			params = [],
			search = null;
		me.internalWhere = '';
		if(buttonsToolbar) {
			search = buttonsToolbar.getComponent('search').getValue();
			Status = buttonsToolbar.getComponent('InvalidDateStatus').getValue();
		}
		if(Status) {
			var Sysdate = JcallShell.System.Date.getDate();
			if(!Sysdate) Sysdate = new Date();
			switch(Status) {
				case "<=0": //已过期失效
					params.push("goodsregister.RegisterInvalidDate<='" + JShell.Date.toString(Sysdate, true) + "'");
					break;
				case "<=30": //30天内到期
					params.push("goodsregister.RegisterInvalidDate>='" + JShell.Date.toString(Sysdate, true) + "'");
					params.push("goodsregister.RegisterInvalidDate<='" + JShell.Date.toString(JcallShell.Date.getNextDate(Sysdate, 30), true) + "'");
					break;
				default:
					break;
			}

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
			var RegisterInvalidDate = data.list[i].GoodsRegister_RegisterInvalidDate;
			var InvalidDateStatus = "";
			if(RegisterInvalidDate) {
				RegisterInvalidDate = JShell.Date.getDate(RegisterInvalidDate);
				RegisterInvalidDate = Ext.util.Format.date(RegisterInvalidDate, 'Y-m-d');
				RegisterInvalidDate = JShell.Date.getDate(RegisterInvalidDate);
				Sysdate = Ext.util.Format.date(Sysdate, 'Y-m-d');
				Sysdate = JShell.Date.getDate(Sysdate);
				var days = parseInt((RegisterInvalidDate - Sysdate) / 1000 / 60 / 60 / 24);
				//				if(days > 0)
				//					days = days + 1;
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