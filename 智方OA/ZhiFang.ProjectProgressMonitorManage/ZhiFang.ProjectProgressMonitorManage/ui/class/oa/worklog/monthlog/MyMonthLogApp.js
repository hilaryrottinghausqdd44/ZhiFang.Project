/**
 * 我的月工作计划
 * @author longfc
 * @version 2016-06-22
 */
Ext.define('Shell.class.oa.worklog.monthlog.MyMonthLogApp', {
	extend: 'Shell.ux.panel.AppPanel',
	title: '月总结/月计划',
	hasReset: false,
	sendtype: 'MEOWN',
	defaultPageSize: 50,
	/**是否启用新增按钮*/
	hasAdd: true,
	hasEidt: true,
	defaultLoad: true,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		var Form = me.getComponent('Form');
		var Grid = me.getComponent('Grid');

		Grid.on({
			itemclick: function(v, record) {
				var id = record.get("Id");
				JShell.Action.delay(function() {
					if(id != "") {
						Form.isShow(id);
					}
				}, null, 500);
			},
			select: function(RowModel, record) {
				JShell.Action.delay(function() {
					var id = record.get("Id");
					if(id != "") {
						Form.isShow(id);
					}
				}, null, 500);
			},
			/**@overwrite 新增按钮点击处理方法*/
			onAddClick: function() {
				Form.isAdd();
				var PWorkLogCopyFor = Form.getComponent('PWorkLogCopyFor');
				PWorkLogCopyFor.setTypeAdnUserId(false, true);
				PWorkLogCopyFor.clearData();
			},
			/**@overwrite 修改按钮点击处理方法*/
			onEditClick: function() {
				var records = Grid.getSelectionModel().getSelection();
				if(!records || records.length != 1) {
					JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
					return;
				}
				var id = records[0].get(Grid.PKField);
				var Status = records[0].get("Status");
				if(Status.toString() == "true" || Status == "1") {
					JShell.Msg.alert("当前的日志已确认提交,不能编辑!");
				} else {
					Form.PK = id;
					Form.isEdit(id);
				}
			}
		});
		Form.on({
			load: function(winForm, data) {
				//表单的抄送人处理
				JShell.Action.delay(function() {
					me.loadPWorkLogCopyFor();
				}, null, 300);
			},
			save: function(winForm, id) {
				Grid.onSearch();
			}
		});
	},
	/**加载抄送人信息*/
	loadPWorkLogCopyFor: function() {
		var me = this;
		var Form = me.getComponent('Form');
		var PWorkLogCopyFor = Form.getComponent('PWorkLogCopyFor');
		PWorkLogCopyFor.formtype = Form.formtype;
		//PWorkLogCopyFor.PK = Form.PK;
		switch(Form.formtype) {
			case "show":
				PWorkLogCopyFor.setTypeAdnUserId(true, true);
				break;
			case "add":
				PWorkLogCopyFor.setTypeAdnUserId(false, true);
				break;
			case "edit":
				PWorkLogCopyFor.setTypeAdnUserId(false, true);
				break;
			default:
				break;
		}
		if(Form.PK != "") {
			PWorkLogCopyFor.loadDataById(Form.PK);
		}
	},
	initComponent: function() {
		var me = this;
		me.title = me.title || "我的月工作计划";

		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		var EmpID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;

		me.Grid = Ext.create('Shell.class.oa.worklog.monthlog.MonthLogGrid', {
			itemId: 'Grid',
			region: 'west',
			border: true,
			title: '月总结/月计划',
			split: true,
			header:false,
			width: 555,
			/**查询对象*/
			objectEName: 'PWorkMonthLog',
			sendtype: me.sendtype,
			defaultPageSize: me.defaultPageSize,
			/**是否启用新增按钮*/
			hasAdd: me.hasAdd,
			hasEidt: me.hasEidt,
			/**默认加载数据*/
			defaultLoad: me.defaultLoad
		});
		me.Form = Ext.create('Shell.class.oa.worklog.basic.Form', {
			itemId: 'Form',
			border: true,
			title: '我的月总结/月计划',
			region: 'center',
			//height: me.height,
			split: true,
			/**查询对象*/
			objectEName: 'PWorkMonthLog',
			/**获取数据服务路径*/
			selectUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPWorkMonthLogById?isPlanish=true',
			/**新增服务地址*/
			addUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_AddPWorkMonthLogByWeiXin',
			/**修改服务地址*/
			editUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePWorkMonthLogByField'
		});

		return [me.Form, me.Grid];
	},
	/**加载应用数据*/
	load: function() {
		var me = this;
		var Grid = me.getComponent('Grid');
		Grid.load();
	}
});