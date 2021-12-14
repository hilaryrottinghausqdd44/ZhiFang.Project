/**
 * 员工财务账户信息维护
 * @author longfc
 * @version 2016-11-18
 */
Ext.define('Shell.class.sysbase.user.pempfinanceaccount.App', {
	extend: 'Shell.ux.panel.AppPanel',
	title: '员工财务账户信息维护',
	/*是否管理员应用**/
	isManage: false,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);

		me.Tree.on({
			itemclick: function(v, record) {
				me.GridSearch(record);
			},
			select: function(RowModel, record) {
				me.GridSearch(record);
			}
		});

		me.Grid.on({
			itemclick: function(v, record) {
				me.LinkPageForm(record);
			},
			select: function(RowModel, record) {
				me.LinkPageForm(record);
			},
			onAddClick: function() {
				if(me.Grid.store.getCount() < 1) {
					me.Form.isAdd(me.objDefaultValue);
				} else {
					JShell.Msg.alert("该员工财务账户信息已存在!", null, 1000);
				}
			},
			onEditClick: function(grid, record) {
				me.LinkPageForm(record);
			},
			nodata: function() {
				me.Form.clearData();
			}
		});

		me.Form.on({
			save: function() {
				me.Grid.onSearch();
			}
		});
	},
	objDefaultValue: {
		EmpID: '',
		Name: '',
		SName: '',
		PinYinZiTou: '',
		Shortcode: ''
	},
	LinkPageForm: function(record) {
		var me = this;
		me.clearData();
		JShell.Action.delay(function() {
			var id = record.get(me.Grid.PKField);
			var IsExist = record.get("IsExist");
			switch(IsExist) {
				case true:
					me.Form.isEdit(id);
					break;
				default:
					me.objDefaultValue["EmpID"] = record.get("EmpID");
					me.objDefaultValue["Name"] = record.get("Name");
					me.objDefaultValue["PinYinZiTou"] = record.get("PinYinZiTou");
					me.objDefaultValue["Shortcode"] = record.get("Shortcode");
					me.objDefaultValue["SName"] = record.get("SName");
					me.Form.isAdd(me.objDefaultValue);
					break;
			}

		}, null, 500);
	},
	clearData: function() {
		var me = this;
		me.Grid.enableControl();
		me.Grid.DeptId=null;
		me.Grid.defaultWhere = "";
		me.objDefaultValue["EmpID"] = '';
		me.objDefaultValue["Name"] = '';
		me.objDefaultValue["SName"] = '';
		me.objDefaultValue["PinYinZiTou"] = '';
		me.objDefaultValue["Shortcode"] = '';
	},
	GridSearch: function(record) {
		var me = this;
		JShell.Action.delay(function() {
			var id = record.get('tid');
			var leaf = record.get('leaf');
			var pid = record.raw.pid;
			me.Grid.DeptId="";
			me.Grid.clearData();
			me.Form.clearData();
			me.clearData();
			if(id != "0" || id != 0) {
				me.Grid.loadByDeptId(id);
			}

		}, null, 300);
	},
	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;

		me.Tree = Ext.create('Shell.class.sysbase.org.Tree', {
			region: 'west',
			width: 210,
			header: false,
			itemId: 'Tree',
			split: true,
			collapsible: true
		});
		me.Grid = Ext.create('Shell.class.sysbase.user.pempfinanceaccount.Grid', {
			region: 'center',
			header: false,
			isManage: me.isManage,
			itemId: 'Grid'
		});
		me.Form = Ext.create('Shell.class.sysbase.user.pempfinanceaccount.Form', {
			region: 'east',
			width: 220,
			header: false,
			itemId: 'Form',
			isManage: me.isManage,
			split: true,
			collapsible: true
		});

		return [me.Tree, me.Grid, me.Form];
	}
});