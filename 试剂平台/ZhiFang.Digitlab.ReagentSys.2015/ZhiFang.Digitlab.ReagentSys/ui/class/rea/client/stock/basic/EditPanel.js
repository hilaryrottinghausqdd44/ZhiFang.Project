/**
 * 客户端入库
 * @author liangyl
 * @version 2017-12-01
 */
Ext.define('Shell.class.rea.client.stock.basic.EditPanel', {
	extend: 'Ext.panel.Panel',

	title: '入库信息',
	header: false,
	border: false,
	//width:680,
	/**默认加载数据时启用遮罩层*/
	hasLoadMask: true,
	bodyPadding: 1,

	layout: {
		type: 'border'
	},
	/**录入:entry/审核:check*/
	OTYPE: "entry",
	/**当前选择的主单Id*/
	PK: null,
	/**新增/编辑/查看*/
	formtype: 'show',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//明细列表 的货品明细添加,删除,数量改变后,需要重新计算总价格并联动更新表单总价格及表单供货方编辑状态处理
		me.DtlGrid.on({
			nodata: function(p) {}
		});
	},
	initComponent: function() {
		var me = this;
		me.addEvents('onFullScreenClick');
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.DtlGrid = Ext.create('Shell.class.rea.client.stock.basic.DtlGrid', {
			header: false,
			itemId: 'DtlGrid',
			region: 'center',
			collapsible: false,
			PK: me.PK,
			defaultLoad: false,
			collapsed: false,
			formtype: me.formtype,
			OTYPE: me.OTYPE
		});
		me.DocForm = Ext.create('Shell.class.rea.client.stock.basic.DocForm', {
			header: false,
			itemId: 'DocForm',
			region: 'north',
			width: me.width,
			height: 130,
			split: false,
			collapsible: false,
			collapsed: false,
			PK: me.PK,
			formtype: me.formtype,
			OTYPE: me.OTYPE
		});
		var appInfos = [me.DtlGrid, me.DocForm];
		return appInfos;
	},
	nodata: function() {
		var me = this;
		var me = this;
		me.PK = null;
		me.formtype = "show";

		me.DocForm.PK = null;
		me.DocForm.formtype = "show";
		me.DocForm.StatusName = "";
		me.DocForm.isShow();
		me.DocForm.getForm().reset();
		me.DocForm.getComponent('buttonsToolbar').hide();

		me.DtlGrid.PK = null;
		me.DtlGrid.formtype = "show";
		me.DtlGrid.defaultWhere = "";
		me.DtlGrid.Status = null;
		me.DtlGrid.store.removeAll();
		me.DtlGrid.disableControl();
	},
	clearData: function() {
		var me = this;
		me.nodata();
	},
	isAdd: function() {
		var me = this;
		me.PK = null;
		me.formtype = "add";

		me.DocForm.PK = null;
		me.DocForm.formtype = "add";
		me.DocForm.isAdd();
		me.DocForm.getComponent('buttonsToolbar').hide();

		me.DtlGrid.PK = null;
		me.DtlGrid.formtype = "add";
		me.DtlGrid.defaultWhere = "";
		me.DtlGrid.Status = "1";
		me.DtlGrid.store.removeAll();
		me.DtlGrid.enableControl();
	},
	isEdit: function(record, applyGrid) {
		var me = this;
		var id = record.get("ReaBmsInDoc_Id");
		
	
		me.PK = id;
		me.formtype = "edit";

		me.DocForm.formtype = "edit";
		me.DocForm.PK = id;
		var statusName = "",
			color = "#1c8f36";
		if(applyGrid.StatusEnum != null) statusName = applyGrid.StatusEnum[record.get("ReaBmsInDoc_Status")];
		if(applyGrid.StatusBGColorEnum != null) color = applyGrid.StatusBGColorEnum[record.get("ReaBmsInDoc_Status")];
		statusName = '<b style="color:' + color + ';">' + statusName + '</b> ';
		me.DocForm.StatusName = statusName;
		me.DocForm.isEdit(id);
		me.DocForm.getComponent('buttonsToolbar').hide();

		me.DtlGrid.PK = id;
		me.DtlGrid.formtype = "edit";
		me.DtlGrid.Status = record.get("ReaBmsInDoc_Status");
		me.loadDtlGrid(me.PK);
	},
	/**主订单联动明细及表单*/
	isShow: function(record, applyGrid) {

		var me = this;
		var id = record.get("ReaBmsInDoc_Id");
		me.PK = id;
		me.formtype = "show";

		me.DocForm.PK = id;
		me.DocForm.formtype = "show";
		var statusName = "",
			color = "#1c8f36";
		if(applyGrid.StatusEnum != null) statusName = applyGrid.StatusEnum[record.get("ReaBmsInDoc_Status")];
		if(applyGrid.StatusBGColorEnum != null) color = applyGrid.StatusBGColorEnum[record.get("ReaBmsInDoc_Status")];
		statusName = '<b style="color:' + color + ';">' + statusName + '</b> ';
		me.DocForm.StatusName = statusName;
		me.DocForm.isShow(id);

		me.DtlGrid.PK = id;
		me.DtlGrid.formtype = "show";
		me.DtlGrid.Status = record.get("ReaBmsInDoc_Status");
		me.loadDtlGrid(me.PK);
	},
	loadDtlGrid: function(id) {
		var me = this;
		if(!id)
			var defaultWhere = "";
		if(id) defaultWhere = "reabmsindtl.ReaBmsInDoc.Id=" + id;
		me.DtlGrid.defaultWhere = defaultWhere;
		me.DtlGrid.onSearch();
	},
	onSave:function(){
		var me=this;
		
		if(!me.DocForm.getForm().isValid()) return;

		var docEntity = me.DocForm.getEditParams();
		if(!docEntity.entity.Id)return;
		
		var records = me.DtlGrid.store.getModifiedRecords();
		var fieldsDtl= me.DtlGrid.getUpdateFields();
		var editArr=[];
		for(var i = 0; i < records.length; i++) {
			var entityDtl=me.DtlGrid.getOneUpdateInfo(records[i]);
			editArr.push(entityDtl);
		}
		docEntity.entity.BmsCenSaleDtlConfirmList =editArr;
		var entity = {
			entity: docEntity.entity,
			fields:docEntity.fields,
			fieldsDtl: fieldsDtl
		};
		var params = Ext.JSON.encode(entity);
		if(!params) return;
		
		me.showMask(me.DocForm.saveText); //显示遮罩层
		var url ='/ReaSysManageService.svc/ST_UDTO_UpdateReaSaleDocConfirmAndDtl';
		url = (url.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + url;
		JShell.Server.post(url, params, function(data) {
			me.hideMask(); //隐藏遮罩层
			if(data.success) {
				me.fireEvent('save', me, true);
				JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, me.DocForm.hideTimes);
			} else {
				JShell.Msg.error(data.msg);
			}
		});
	},
	/**显示遮罩*/
	showMask: function(text) {
		var me = this;
		if(me.hasLoadMask) {
			me.body.mask(text);
		} //显示遮罩层
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if(me.hasLoadMask) {
			me.body.unmask();
		}
	}
});