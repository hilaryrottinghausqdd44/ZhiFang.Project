/**
 * 功能按钮栏
 * @author longfc
 * @version 2016-07-13
 */
Ext.define('Shell.class.qms.file.basic.toolbarButtons', {
	extend: 'Ext.toolbar.Toolbar',
	alias: 'widget.toolbarButtons',
	mixins: ['Shell.ux.Langage'],

	/**提示框样式*/
	tooltipFormat: '<b>{msg}</b>',
	/**按钮方法规则*/
	handlerFormat: 'on{fun}Click',
	hasCancel: false,
	formtype: "edit",
	DisagreeButtonText: "",
	AgreeButtonText: "",
	HiddenAgreeButton: true,
	HiddenDisagreeButton: true,
	/**
	 * 功能按钮是否隐藏:组件是否隐藏,只起草,仅审核,仅批准,自动发布
	 * 第一个参数为功能按钮是否显示或隐藏
	 * 第二个参数为只起草选择项是否显示或隐藏
	 * 第三个参数为仅审核选择项是否显示或隐藏
	 * 第四个参数为仅批准选择项是否显示或隐藏
	 * 第五个参数为发布选择项是否显示或隐藏
	 * */
	hiddenRadiogroupChoose: [false, false, false, false, false],
	/**功能按钮是的boxLabel显示*/
	boxLabelRadiogroupChoose: ['只起草', '仅审核', '仅批准', '发布'],
	/**功能按钮默认选中*/
	checkedRadiogroupChoose: [true, false, false, false],
	//***下一执行者的所属角色/
	RoleHREmployeeCName: JcallShell.QMS.Enum.getEmployeeRoleName("r1;r6"),
	/***操作记录备注的前缀信息*/
	MemoPrefix: "",
	/***是否存在审核人*/
	hasNextExecutor: true,
	initComponent: function() {
		var me = this;
		//替换语言包
		me.changeLangage('Shell.ux.toolbar.Button');
		me.items = me.createItems();
		me.addEvents('onDisagreeSaveClick');
		me.addEvents('onAgreeSaveClick');
		me.addEvents('onCloseClick');
		me.addEvents('onradiogroupchange');
		me.callParent(arguments);
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//选择类型按钮如果为不隐藏时,选择类型按钮默认值处理
		if(me.hiddenRadiogroupChoose[0] == false) {
			var radioItem = me.getComponent('radiogroupChoose');
			var checked = {
				choose: 1
			};
			checked.choose = me.fFileOperationType;
			if(radioItem) {
				radioItem.setValue(checked);
			}
			JShell.Action.delay(function() {
				me.onradiogroupchange(radioItem, checked);
			}, null, 300);
		}
	},
	/**创建功能按钮栏*/
	createItems: function() {
		var me = this,
			items = [];
		if(items.length == 0) {

			items.push("->");
			var arrItems = [];
			if(me.hiddenRadiogroupChoose[1] == false) {
				arrItems.push({
					boxLabel: me.boxLabelRadiogroupChoose[0],
					name: 'choose',
					hidden: me.hiddenRadiogroupChoose[1],
					inputValue: 1,
					checked: me.checkedRadiogroupChoose[0]
				});
			}
			if(me.hiddenRadiogroupChoose[2] == false) {
				arrItems.push({
					boxLabel: me.boxLabelRadiogroupChoose[1],
					name: 'choose',
					hidden: me.hiddenRadiogroupChoose[2],
					inputValue: 3,
					checked: me.checkedRadiogroupChoose[1]
				});
			}
			if(me.hiddenRadiogroupChoose[3] == false) {
				arrItems.push({
					boxLabel: me.boxLabelRadiogroupChoose[2],
					name: 'choose',
					hidden: me.hiddenRadiogroupChoose[3],
					inputValue: 4,
					checked: me.checkedRadiogroupChoose[2]
				});
			}
			if(me.hiddenRadiogroupChoose[4] == false) {
				arrItems.push({
					boxLabel: me.boxLabelRadiogroupChoose[3],
					name: 'choose',
					hidden: me.hiddenRadiogroupChoose[4],
					inputValue: 5,
					checked: me.checkedRadiogroupChoose[3]
				});
			}
			var width = arrItems.length * 85;
			items.push({
				xtype: 'radiogroup',
				itemId: 'radiogroupChoose',
				hidden: me.hiddenRadiogroupChoose[0],
				columns: arrItems.length,
				width: width,
				vertical: true,
				items: arrItems,
				listeners: {
					change: {
						fn: function(rdgroup, checked) {
							me.onradiogroupchange(rdgroup, checked);
						}
					}
				}
			});
			if(me.hasNextExecutor) {
				items.push("-");
				//指的是如果当前文档为起草,为审核人
				items.push({
					fieldLabel: "审核人",
					labelWidth: 60,
					labelAlign: 'right',
					name: 'NextExecutorName',
					itemId: 'NextExecutorName',
					xtype: 'uxCheckTrigger',
					className: 'Shell.class.sysbase.user.role.CheckGrid',
					classConfig: {
						checkOne: true,
						searchInfoWidth: '70%',
						RoleHREmployeeCName: JcallShell.QMS.Enum.getEmployeeRoleName("r1;r2;r3;r4;r6")
					}
				}, {
					fieldLabel: 'ID',
					hidden: true,
					xtype: 'textfield',
					itemId: 'NextExecutorId',
					name: 'NextExecutorId'
				}, '-');
			}
			if(!me.HiddenAgreeButton) {
				items.push({
					xtype: 'button',
					itemId: 'btnAgree',
					iconCls: 'button-save',
					text: me.AgreeButtonText,
					tooltip: me.AgreeButtonText,
					hidden: me.HiddenAgreeButton,
					handler: function() {
						me.fireEvent('onAgreeSaveClick', me);
					}
				});
			}
			if(!me.HiddenDisagreeButton) {
				items.push({
					xtype: 'button',
					itemId: 'btnDisagree',
					iconCls: 'button-save',
					hidden: me.HiddenDisagreeButton,
					text: me.DisagreeButtonText,
					tooltip: me.DisagreeButtonText,
					handler: function() {
						me.fireEvent('onDisagreeSaveClick', me);
					}
				});
			}
			if(me.formtype != "show" && me.hasReset) items.push('reset');
			if(me.hasCancel) items.push('cancel');
			items.push({
				xtype: 'button',
				itemId: 'btnColse',
				iconCls: 'button-del',
				text: "关闭",
				tooltip: '关闭',
				handler: function() {
					me.fireEvent('onCloseClick', me);
					//me.close();
				}
			});
			if(items.length > 0) items.unshift('->');
		}
		if(items.length == 0) return null;
		return items;

	},

	/***选择类型项改变后,对起草人,审核人等设置默认值*/
	onradiogroupchange: function(rdgroup, checked) {
		var me = this;
		var returnDatas = {
			ffileOperationMemo: ""
		};
		if(me.hasNextExecutor) {
			//下一执行者处理
			var NextExecutorName = me.getComponent('NextExecutorName');
			var NextExecutorId = me.getComponent('NextExecutorId');
			var fieldLabel = "审核人";
			if(NextExecutorName) {
				NextExecutorName.setVisible(true);
			}
			NextExecutorId.setValue("");
			NextExecutorName.setValue("");

			var obj = {
				checkOne: true,
				RoleHREmployeeCName: JcallShell.QMS.Enum.getEmployeeRoleName("r2")
			};
			switch(checked.choose) {
				case 1: //只起草
					returnDatas.ffileOperationMemo = "";
					fieldLabel = "审核人";
					obj.RoleHREmployeeCName = JcallShell.QMS.Enum.getEmployeeRoleName("r2");
					break;
				case 3: //自动审核
					returnDatas.ffileOperationMemo = me.MemoPrefix + "并直接自动审核";
					fieldLabel = "审批人";
					obj.RoleHREmployeeCName = JcallShell.QMS.Enum.getEmployeeRoleName("r3");
					break;
				case 4:
					returnDatas.ffileOperationMemo = me.MemoPrefix + "并直接自动审批";
					obj.RoleHREmployeeCName = JcallShell.QMS.Enum.getEmployeeRoleName("r4");
					fieldLabel = "发布人";
					break;
				case 5:
					returnDatas.ffileOperationMemo = me.MemoPrefix + "并直接自动发布";
					if(NextExecutorName) {
						NextExecutorName.setVisible(false);
					}
					break;
				default:
					me.ffileOperationMemo = "";
					break;
			}
			NextExecutorName.changeClassConfig(obj);
			if(NextExecutorName) {
				NextExecutorName.setFieldLabel(fieldLabel);
			}
		}
		me.fireEvent('onradiogroupchange', rdgroup, checked, returnDatas);
	}
});