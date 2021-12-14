/**
 * 工作日志应用
 * @author liangyl
 * @version 2016-08-02
 */
Ext.define('Shell.class.oa.worklog.daylog.App', {
	extend: 'Shell.ux.panel.AppPanel',
	title: '日总结/日计划',
	layout: {
		type: 'border',
		regionWeights: {
			west: 2,
			north: 1
		}
	},
	width: 1000,
	height: 800,
	/**任务ID*/
	TaskId: '',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		var Form = me.getComponent('Form');
		var Grid = me.getComponent('Grid');
		Grid.on({
			itemclick: function(v, record) {
				var id = record.get("Id");
				var TaskId=record.get("PTaskID");
				JShell.Action.delay(function() {
					if(id != "") {
						if(TaskId){
							Form.ShowVisible(true);
						}else{
							Form.SetVisible(false);
						}
						Form.isShow(id);
					}
				}, null, 500);
			},
			select: function(RowModel, record) {
				JShell.Action.delay(function() {
					var id = record.get("Id");
					var TaskId=record.get("PTaskID");
					if(id != "") {
						if(TaskId){
							Form.ShowVisible(true);
						}else{
							Form.SetVisible(false);
						}
						Form.isShow(id);
					}
				}, null, 500);
			},
			/**@overwrite 新增按钮点击处理方法*/
			onAddClick: function() {
				me.TaskId = '';
				me.openTaskForm();
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
				var TaskId=records[0].get("PTaskID");
				
				if(Status == "true" || Status == "1") {
					JShell.Msg.alert("当前的日志已确认提交,不能编辑!");
				} else {
					Form.PK = id;
					if(TaskId){
						Form.EditVisible(false);
					}else{
						Form.SetVisible(false);
					}
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
				JShell.Action.delay(function() {
					me.loadPWorkLogCopyFor();
					Form.changeWorkload(data);
				}, null, 300);
			},
			save: function(winForm, id) {
				Form.disableControl();
				Grid.onSearch();
			}
		});
	},

	/**打开任务选择列表*/
	openTaskForm: function() {
		var me = this;
		var Form = me.getComponent('Form');
		var config = {
			width: 280,
			height: 300,
			listeners: {
				load: function(store, win) {
					var obj = {
						CName: '',
						IdString: '',
					};
					var rec = store.findRecord('IdString', '');
					if(!rec) {
						store.insert(0, obj);
					}
				},
				onClick: function(grid, rec, win) {
					if(rec != null) {
						me.TaskId = rec.get('IdString');
					} else {
						me.TaskId = '';
					}
					if(me.TaskId){
						Form.EditVisible(false);
					}else{
						Form.SetVisible(false);
					}
					Form.PTaskId = me.TaskId;
					Form.isAdd();
				    var PTaskCopyFor = Form.getComponent('PWorkLogCopyFor');
				    PTaskCopyFor.setTypeAdnUserId(false, true);
				    PTaskCopyFor.clearData();
					win.close();
				}
			}
		};
		var panel = 'Shell.class.oa.worklog.daylog.PTaskGrid';
		JShell.Win.open(panel, config).show();
	},
	initComponent: function() {
		var me = this;
		me.title = me.title || "日总结/日计划";
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.Grid = Ext.create('Shell.class.oa.worklog.daylog.Grid', {
			border: true,
			title: '日总结/日计划',
			region: 'west',
			sendtype: 'MEOWN',
			ownempid: '-1',
			width: 555,
			split: true,
			/**查询对象*/
			objectEName: 'WorkLogDay',
			header: false,
			itemId: 'Grid'
		});
		me.Form = Ext.create('Shell.class.oa.worklog.daylog.Form', {
			itemId: 'Form',
			border: true,
			split: true,
			region: 'center',
			title: '我的日总结/日计划',
			split: true
		});
		return [me.PTaskGrid, me.Grid, me.Form];
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
	}
});