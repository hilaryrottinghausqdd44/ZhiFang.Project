/**
 * 文档修订
 * @author longfc
 * @version 2016-06-22
 */
Ext.define('Shell.class.qms.file.file.revise.ReviseApp', {
	extend: 'Shell.class.qms.file.basic.App',
	title: '文档修订',
	
	FTYPE: '4',
	/**文件的操作记录类型*/
	fFileOperationType: 1,
	
	/**基本应用的文档确认(通过/同意)操作按钮是否显示*/
	HiddenAgreeButton: false,
	/**基本应用的文档确认(通过/同意)操作按钮显示名称*/
	AgreeButtonText: "确认提交",
	/**基本应用的文档确认(通过/同意)操作按钮的功能类型*/
	AgreeOperationType: 1,
	/**基本应用的文档确认(不通过/不同意)操作按钮是否显示*/
	HiddenDisagreeButton: true,
	DisagreeOperationType: 1,
	/**提交并发布的操作按钮是否显示*/
	HiddenPublishButton: false,
	/**隐藏阅读人信息*/
	HiddenFFileReadingLog: true,	
	/**
	 * 功能按钮是否隐藏:组件是否隐藏,只起草,仅审核,仅批准,自动发布
	 * 第一个参数为功能按钮是否显示或隐藏
	 * 第二个参数为只起草选择项是否显示或隐藏
	 * 第三个参数为仅审核选择项是否显示或隐藏
	 * 第四个参数为仅批准选择项是否显示或隐藏
	 * 第五个参数为发布选择项是否显示或隐藏
	 * */
	hiddenRadiogroupChoose: [false, false, true, true, false],
	/**功能按钮默认选中*/
	checkedRadiogroupChoose: [true, false, false, false],
	
	basicGrid: 'Shell.class.qms.file.file.revise.Grid',
	AppOperationType: JcallShell.QMS.Enum.AppOperationType.新增修订文档,
	/**概要是否默认清空，外部传入，true-清空，false-不清空*/
	ISCLEAR:false,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		var Grid = me.getComponent('Grid');

		Grid.on({
			itemdblclick: function(grid, record, item, index, e, eOpts) {
				var id = record.get('FFile_Id');
				var status = record.get('FFile_Status');
				switch(status) {
					case "5": //状态为
						if(me.FTYPE == "" || me.FTYPE.length < 1) {
							JShell.Msg.alert("当前应用的FTYPE为空,不能操作");
						} else {
							Grid.formtype = "add";
							Grid.setAppOperationType();
							Grid.openFFileForm(record, 1);
						}
						break;
					default:
						if(me.FTYPE == "" || me.FTYPE.length < 1) {
							JShell.Msg.alert("当前应用的FTYPE为空,不能操作");
						} else {
							Grid.formtype = "show";
							Grid.openShowTabPanel(record);
						}
						break;
				}
			},
			onAddClick: function() {},
			onShowClick: function() {
				var records = Grid.getSelectionModel().getSelection();
				if(records && records.length < 1) {
					JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
					return;
				}
				Grid.openShowTabPanel(records[0]);
			},
			onDisagreeSaveClick: function(grid) {}
		});
	},

	initComponent: function() {
		var me = this;
		/**对外公开:允许外部调用应用时传入树节点值(如IDS=123,232)*/
		me.IDS = me.IDS || "";
		/**抄送人,阅读人的按人员选择时的角色姓名传入*/
		me.ROLEHREMPLOYEECNAME = me.ROLEHREMPLOYEECNAME || "";
		/**编辑文档类型(如新闻/通知/文档/修订文档)*/
		me.FTYPE = me.FTYPE || "";
		/**概要是否默认清空，外部传入，true-清空，false-不清空*/
	    me.ISCLEAR = me.ISCLEAR || false;
		me.callParent(arguments);
	}
});