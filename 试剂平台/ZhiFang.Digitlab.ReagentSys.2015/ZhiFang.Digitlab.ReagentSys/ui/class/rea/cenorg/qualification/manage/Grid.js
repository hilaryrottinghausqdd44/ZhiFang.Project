/***
 *  资质证件管理
 * @author longfc
 * @version 2017-07-14
 */
Ext.define('Shell.class.rea.cenorg.qualification.manage.Grid', {
	extend: 'Shell.class.rea.cenorg.qualification.basic.Grid',
	requires: [
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '资质证件管理',

	hasAdd: true,
	hasEdit: true,
	hasDel: true,
	hasSave: true,

	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',

	/**修改服务地址*/
	editUrl: '/ReagentSysService.svc/ST_UDTO_UpdateGoodsQualificationByField',
	delUrl: '/ReagentSysService.svc/ST_UDTO_DelGoodsQualification',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.CENORG_ID = JShell.REA.System.CENORG_ID;
		me.CENORG_NAME = JShell.REA.System.CENORG_NAME;

		me.defaultWhere = me.defaultWhere || '';
		if(me.defaultWhere) {
			me.defaultWhere = me.defaultWhere + " and goodsqualification.Comp.Id=" + me.CENORG_ID;
		} else {
			me.defaultWhere = "goodsqualification.Comp.Id=" + me.CENORG_ID;
		}
		me.buttonToolbarItems = me.createButtonToolbarItems();
		me.callParent(arguments);
	},

	createGridColumns: function() {
		var me = this;
		var columns = me.callParent(arguments);
		columns.splice(3, 0, {
			xtype: 'checkcolumn',
			text: '使用',
			dataIndex: 'GoodsQualification_Visible',
			width: 40,
			align: 'center',
			sortable: false,
			menuDisabled: true,
			stopSelection: false,
			type: 'boolean'
		});
		return columns;
	},
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick: function() {
		var me = this;
		me.showForm(null);
	},
	/**@overwrite 新增按钮点击处理方法*/
	onEditClick: function() {		
		var me = this;
		//me.fireEvent('editclick', this);
		var records = me.getSelectionModel().getSelection();
		if(!records || records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		var id = records[0].get(me.PKField);
		me.showForm(id);
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
			var Visible = rec.get('GoodsQualification_Visible');
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
	/**显示表单*/
	showForm: function(id) {
		var me = this,
			config = {
				width: 280,
				resizable: false,
				listeners: {
					save: function(p, records) {
						p.close();
						me.onSearch();
					}
				}
			};

		if(id) {
			config.formtype = 'edit';
			config.PK = id;
			config.Comp = {
				Id: '',
				Name: '',
				readOnly: true
			}; //供应商信息
		} else {
			config.formtype = 'add';
			config.Comp = {
				Id: me.CENORG_ID,
				Name: me.CENORG_NAME,
				readOnly: true
			}; //供应商信息			
		}

		JShell.Win.open('Shell.class.rea.cenorg.qualification.manage.Form', config).show();
	}
});