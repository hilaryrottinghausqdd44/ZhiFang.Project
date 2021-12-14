/**
 * 输血过程记录:批量修改--左区域(发血血袋及输血过程记录项信息)
 * @description 分步批量登记(分为输血结束前登记及输血结束登记)
 * @author longfc
 * @version 2020-03-23
 */
Ext.define('Shell.class.blood.nursestation.transrecord.batchregister.DtlPanel', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '血袋&病人体征信息',
	bodyPadding: '2px',

	//新增还是编辑
	formtype: "add",
	/**发血血袋明细记录Id字符串值:如123,234,4445*/
	outDtlIdStr:null,
	//当前选中发血血袋记录集合
	outDtlRrecords: [],
	/**输血记录类型 1:输血结束前;2:输血结束;*/
	transTypeId:"",
	//发血主单ID
	outDocId: null,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.onListeners();
	},
	initComponent: function() {
		var me = this;
		me.addEvents('save', 'nodata');
		me.items = me.createItems();
		//me.dockedItems = me.createDockedItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		var width1=250;
		
		//发血血袋列表信息
		me.OutDtlGrid = Ext.create("Shell.class.blood.nursestation.transrecord.batchregister.OutDtlGrid", {
			region: 'west',
			header: true,
			border: true,
			width: width1,
			outDocId: me.outDocId,
			outDtlIdStr:me.outDtlIdStr,
			itemId: 'OutDtlGrid'
		});		
		//病人体征信息
		me.DtlForm = Ext.create('Shell.class.blood.nursestation.transrecord.batchregister.TransDtlForm', {
			region: 'center',
			header: true,
			itemId: 'DtlForm',
			border: true,
			split: true,
			width: me.width-width1,
			height: me.height,
			outDtlIdStr:me.outDtlIdStr,
			/**输血记录类型 1:输血结束前;2:输血结束;*/
			transTypeId:me.transTypeId,
			collapsible: false
		});
		return [me.OutDtlGrid, me.DtlForm];
	},
	/*程序列表的事件监听**/
	onListeners: function() {
		var me = this;
		me.OutDtlGrid.on({
			nodata: function(p) {

			}
		});
	},
	clearData: function() {
		var me = this;
		me.outDtlIdStr=null;
		
		me.OutDtlGrid.outDtlIdStr=null;
		me.OutDtlGrid.onSearch();;
		
		me.DtlForm.outDtlIdStr=null;
		me.DtlForm.getForm().reset();
		//me.fireEvent('nodata', me);
	},
	loadData: function() {
		var me = this;
		if(me.formtype=="edit"){
			me.isEdit(me.outDtlIdStr);
		}else{
			me.isAdd();
		}
	},
	isAdd: function() {
		var me = this;
		me.formtype = "add";
		
		me.OutDtlGrid.outDtlRrecords = me.outDtlRrecords;
		me.OutDtlGrid.onSearch();
		
		me.DtlForm.outDtlRrecords = me.outDtlRrecords;
		me.DtlForm.isAdd();
	},
	isEdit: function(outDtlIdStr) {
		var me = this;
		me.formtype = "edit";
		
		me.OutDtlGrid.outDtlIdStr=outDtlIdStr;
		me.OutDtlGrid.outDtlRrecords = me.outDtlRrecords;
		me.OutDtlGrid.onSearch();
		
		me.DtlForm.PK=outDtlIdStr;
		me.DtlForm.outDtlRrecords = me.outDtlRrecords;
		me.DtlForm.isEdit(outDtlIdStr);
	}
});
