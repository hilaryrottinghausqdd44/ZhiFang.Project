/**
 * lodop模板维护
 * @author longfc
 * @version 2019-09-18
 */
Ext.define('Shell.class.sysbase.lodoptemplet.App', {
	extend: 'Shell.ux.panel.AppPanel',
	title: 'lodop模板维护',
	//打印内容变量,与ContentVName定义的名称要保持一致
	PrintData: "",

	afterRender: function() {
		var me = this;
		me.callParent(arguments);

		me.Grid.on({
			itemclick: function(v, record) {
				JShell.Action.delay(function() {
					me.Form.isEdit(record.get(me.Grid.PKField));
				}, null, 500);
			},
			select: function(RowModel, record) {
				JShell.Action.delay(function() {
					me.Form.isEdit(record.get(me.Grid.PKField));
				}, null, 500);
			},
			addclick: function() {
				me.Grid.setEnableControl(true);
				me.Form.CurModel = me.Grid.getSelectModel();
				me.Form.isAdd();
			},
			copyclick: function(p, record) {
				me.onCopy(record);
			},
			editclick: function(p, record) {
				me.Grid.setEnableControl(true);
				me.Form.isEdit(record.get(me.Grid.PKField));
			},
			nodata: function(p) {
				me.Form.clearData();
			},
			modelchange: function(newValue) {
				me.onModelChange(newValue);
			},
			onprint: function(type) {
				me.onPrintClick(type);
			}
		});
		me.Form.on({
			save: function(p, id) {
				me.Grid.onSearch(id);
			}
		});
	},

	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;

		me.Grid = Ext.create('Shell.class.sysbase.lodoptemplet.Grid', {
			region: 'center',
			header: false,
			itemId: 'Grid'
		});
		me.Form = Ext.create('Shell.class.sysbase.lodoptemplet.Form', {
			region: 'east',
			itemId: 'Form',
			width: 375,
			split: false,
			collapsible: false
		});

		return [me.Grid, me.Form];
	},
	getCLodop: function(type) {
		var me = this;
		//加载Lodop组件
		me.Lodop = me.Lodop || Ext.create('Shell.lodop.Lodop');
		var LODOP = me.Lodop.getLodop(); //true
		if (!LODOP) {
			//JShell.Msg.error("LODOP打印控件获取出错!");
			return;
		}
		return LODOP;
	},
	/**
	 * 复制新增处理
	 * @param {Object} newValue
	 */
	onCopy: function(record) {
		var me = this;
		me.Form.CurModel = me.Grid.getSelectModel();
		me.Form.isAdd();
		var defaultV=record.data;
		defaultV["BLodopTemplet_Id"]="";
		me.Form.setDefaultVale(defaultV);
	},
	/**
	 * 列表模板选择项改变后处理
	 * @param {Object} newValue
	 */
	onModelChange: function(newValue) {
		var me = this;
		if (me.Form.formtype = "add") {
			me.Form.CurModel = me.Grid.getSelectModel();
			me.Form.setDefaultVale();
		}
	},
	/**打印事件*/
	onPrintClick: function(type) {
		var me = this;
		//从当前表单里获取打印信息
		var info = me.Form.getAddParams().entity;
		if (!info) {
			JShell.Msg.error("获取打印实体信息为空!");
			return;
		}
		var LODOP = me.getCLodop();
		if (!LODOP) {
			JShell.Msg.error("LODOP打印控件获取出错!");
			return;
		}
		if (type == 1) { //设计模板
			me.onDesign(info);
		} else if (type == 2) { //预览打印
			me.onPreview(info);
		} else if (type == 3) { //直接打印
			me.onPrint(info);
		}
	},
	/**
	 * 设计模板
	 * @param {Object} info
	 */
	onDesign: function(info) {
		var me = this;
		var LODOP = me.getCLodop();
		var templateCode = me.Form.getComponent('BLodopTemplet_TemplateCode');
		me.PrintData = ""; //打印内容变量,与ContentVName定义的名称要保持一致
		if (info.TemplateCode) {
			me.PrintData = eval(info.TemplateCode);
		} else {
			me.PrintData = eval(templateCode.getValue());
		}
		//指定打印机选择
		var printer = me.Grid.getPrinter();
		if(printer)
			LODOP.SET_PRINTER_INDEXA(printer.getValue());
		//设置内容参数的变量名(只有这样设置后,才能对设计模板进行反复调整及还原设计内容)
		LODOP.SET_PRINT_STYLEA(0, "ContentVName", "PrintData"); 
		if (LODOP.CVERSION) {
			CLODOP.On_Return = function(TaskID, Value) {
				templateCode.setValue(Value);
			};
		}
		templateCode.setValue(LODOP.PRINT_DESIGN());
	},
	/**
	 * 预览打印
	 * @param {Object} info
	 */
	onPreview: function(info) {
		var me = this;
		var LODOP = me.getCLodop();		
		if (info.TemplateCode) {
			me.PrintData = eval(info.TemplateCode);
		} else {
			var templateCode = me.Form.getComponent('BLodopTemplet_TemplateCode');
			me.PrintData = eval(templateCode.getValue());
		}
		//指定打印机选择
		var printer = me.Grid.getPrinter();
		if(printer)
			LODOP.SET_PRINTER_INDEXA(printer.getValue());
			
		if (LODOP.CVERSION) CLODOP.On_Return = null;
		LODOP.PREVIEW();
	},
	/**
	 * 直接打印
	 * @param {Object} info
	 */
	onPrint: function(info) {
		var me = this;
		var LODOP = me.getCLodop();
	}

});
