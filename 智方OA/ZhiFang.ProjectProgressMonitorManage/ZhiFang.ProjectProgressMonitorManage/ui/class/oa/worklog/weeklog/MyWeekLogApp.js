/**
 * 周工作计划应用
 * @author longfc
 * @version 2016-08-04
 */
Ext.define('Shell.class.oa.worklog.weeklog.MyWeekLogApp', {
	extend: 'Shell.ux.panel.AppPanel',
	title: '周总结/周计划',
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
						Form.setTitle(Form.defaultTitle);//还原表单标题，@author Jcall，@version 2016-08-18
						Form.isShow(id);
					}
				}, null, 500);
			},
			select: function(RowModel, record) {
				JShell.Action.delay(function() {
					var id = record.get("Id");
					if(id != "") {
						Form.setTitle(Form.defaultTitle);//还原表单标题，@author Jcall，@version 2016-08-18
						Form.isShow(id);
					}
				}, null, 500);
			},
			/**@overwrite 新增按钮点击处理方法*/
			onAddClick: function() {
				me.changeFormTitle();//改变表单标题，@author Jcall，@version 2016-08-18
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
				if(Status == "true" || Status == "1") {
					JShell.Msg.alert("当前的日志已确认提交,不能编辑!");
				} else {
					Form.setTitle(Form.defaultTitle);
					Form.PK = id;
					Form.isEdit(id);
				}
			},
			nodata:function(p){
			    Form.clearData();
				Form.disableControl();
			}
		});
		Form.on({
			load: function(winForm, data) {	
				//表单的抄送人处理
				//表单的抄送人处理
				JShell.Action.delay(function() {
					me.loadPWorkLogCopyFor();
				}, null, 300);
			},
			save: function(winForm, id) {
				Form.disableControl();
				Grid.onSearch();
			}
		});
	},

	initComponent: function() {
		var me = this;
		me.title = me.title || "周总结/周计划";

		me.items = me.createItems();
		me.callParent(arguments);
	},
	/**加载应用数据*/
	load: function() {
		var me = this;
		var Grid = me.getComponent('Grid');
		Grid.load();
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

	createItems: function() {
		var me = this;

		me.Grid = Ext.create('Shell.class.oa.worklog.weeklog.WeekLogGrid', {
			itemId: 'Grid',
			region: 'west',
			border: true,
			title: '周总结/周计划',
			split: true,
			header:false,
			width: 555,
			/**查询对象*/
			objectEName: 'PWorkWeekLog',
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
			title: '我的周总结/周计划',
			region: 'center',
			//height: me.height,
			split: true,
			/**查询对象*/
			objectEName: 'PWorkWeekLog',
			/**获取数据服务路径*/
			selectUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPWorkWeekLogById?isPlanish=true',
			/**新增服务地址*/
			addUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_AddPWorkWeekLogByWeiXin',
			/**修改服务地址*/
			editUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePWorkWeekLogByField'
		});

		return [me.Form, me.Grid];
	},
	
	/**改变表单标题
	 * @author Jcall
	 * @version 2016-08-19
	 */
	changeFormTitle:function(){
		var me =this,
			date = JShell.System.Date.getDate(),
			year = date.getFullYear(),//年
			month = date.getMonth() + 1,//月
			week = JShell.Date.getMonthWeekByDate(date),//周
			DateText = '';
			
		DateText = year + '年' + month + '月 第' + week + '周';
		
		me.Form.setTitle(me.Form.defaultTitle + ' ( ' + DateText + ' ) ');
	}
});