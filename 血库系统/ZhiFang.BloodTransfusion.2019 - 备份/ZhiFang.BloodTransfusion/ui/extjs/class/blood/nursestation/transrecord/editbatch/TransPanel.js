/**
 * 输血过程记录:批量修改--左区域(发血记录主单)
 * @description 批量登记(不区分输血结束前登记及输血结束登记)
 * @author longfc
 * @version 2020-03-23
 */
Ext.define('Shell.class.blood.nursestation.transrecord.editbatch.TransPanel', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '发血记录信息',
	bodyPadding: '0px',
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,
	/**自定义按钮功能栏*/
	buttonToolbarItems: null,
	/**功能按钮栏位置*/
	buttonDock: 'bottom',
	//新增还是编辑
	formtype: "edit",
	/**发血血袋明细记录Id字符串值:如123,234,4445*/
	outDtlIdStr:null,
	//当前选中发血血袋记录集合
	outDtlRrecords: [],
	/**当前选择的发血主单行记录信息*/
	outDocRecord: null,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.onListeners();
	},
	initComponent: function() {
		var me = this;
		me.addEvents('save', 'nodata');
		me.items = me.createItems();
		me.dockedItems = me.createDockedItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		var width1=me.width;
		
		var docHeight = 125;
		//输血过程主单信息south
		me.DocForm = Ext.create("Shell.class.blood.nursestation.transrecord.editbatch.TransDocForm", {
			region: 'north',
			header: false,
			height: docHeight,
			border: false,
			width: width1,
			outDtlIdStr:me.outDtlIdStr,
			itemId: 'DocForm'
		});
		var height1=parseInt((me.height - docHeight));
		if(height1>600){
			height1=parseInt(height1*0.82);
		}
		//病人体征信息
		me.DtlForm = Ext.create('Shell.class.blood.nursestation.transrecord.editbatch.TransDtlForm', {
			region: 'center',
			header: true,
			itemId: 'DtlForm',
			height: height1,
			border: false,
			width: width1,
			split: false,
			outDtlIdStr:me.outDtlIdStr,
			collapsible: false
		});
		return [me.DocForm, me.DtlForm];
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];

		if (me.hasButtontoolbar) {
			var buttontoolbar = me.createButtontoolbar();
			if (buttontoolbar) items.push(buttontoolbar);
		}
		return items;
	},
	/**创建功能按钮栏*/
	createButtontoolbar: function() {
		var me = this,
			items = me.buttonToolbarItems || [];
		if (items.length == 0) {
			if (me.hasSave) items.push('save');
			if (me.hasReset) items.push('reset');
			if (items.length > 0) items.unshift('->');
		}
		if (items.length == 0) return null;
		var hidden = me.openFormType && (me.formtype == 'show' ? true : false);
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: me.buttonDock,
			itemId: 'buttonsToolbar',
			items: items,
			hidden: hidden
		});
	},
	/*程序列表的事件监听**/
	onListeners: function() {
		var me = this;
		me.DocForm.on({
			nodata: function(p) {

			}
		});
	},
	clearData: function() {
		var me = this;
		me.outDtlIdStr=null;
		me.DocForm.outDtlIdStr=null;
		me.DtlForm.outDtlIdStr=null;
		me.DocForm.getForm().reset();
		me.DtlForm.getForm().reset();
		//me.fireEvent('nodata', me);
	},
	//发血信息赋值处理
	setOutDocInfo:function(){
		var me = this;
		if (me.outDocRecord && me.outDocRecord.data) {
			me.DocForm.getForm().setValues(me.outDocRecord.data);
		}
	},
	loadData: function() {
		var me = this;
		if(me.formtype=="edit"){
			me.isEdit(me.outDtlIdStr);
		}else{
			me.isAdd();
		}
		me.setOutDocInfo();		
	},
	isAdd: function() {
		var me = this;
		me.formtype = "add";
		me.DocForm.outDtlRrecords = me.outDtlRrecords;
		me.DocForm.isAdd();
		me.setOutDocInfo();	
		
		me.DtlForm.outDtlRrecords = me.outDtlRrecords;
		me.DtlForm.isAdd();
	},
	isEdit: function(outDtlIdStr) {
		var me = this;
		me.formtype = "edit";
		
		me.DocForm.outDtlIdStr=outDtlIdStr;
		me.DocForm.outDtlRrecords = me.outDtlRrecords;
		me.DocForm.isEdit(outDtlIdStr);
		me.setOutDocInfo();
		
		me.DtlForm.PK=outDtlIdStr;
		me.DtlForm.outDtlRrecords = me.outDtlRrecords;
		me.DtlForm.isEdit(outDtlIdStr);
	},
	/**@overwrite 重置按钮点击处理方法*/
	onResetClick: function() {
		var me = this;
		//处理意见
		JShell.Msg.confirm({
			title: '<div style="text-align:center;">确认重置输血过程记录登记信息?</div>',
			msg: '重置操作',
			closable: false,
			multiline: false
		}, function(but, text) {
			if (but != "ok") return;
			me.loadData();
		});
	},
	/**保存按钮点击处理方法*/
	onSaveClick: function() {
		var me = this;
		//验证通过后处理
		
		me.fireEvent('save', me, me.DocForm, me.DtlForm);
	}
});
