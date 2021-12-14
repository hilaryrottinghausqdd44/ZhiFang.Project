/**
 * 客户端入库
 * @author liangyl
 * @version 2017-12-14
 */
Ext.define('Shell.class.rea.client.stock.reconciliations.DocForm', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '入库信息',
  formtype: 'show',
	width: 420,
	height: 280,

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaBmsInDocById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ReaSysManageService.svc/ST_UDTO_AddReaBmsInDocOfManualInput',
	/**修改服务地址*/
	editUrl: '/ReaSysManageService.svc/ST_UDTO_UpdateReaBmsInDocOfManualInput',
	
	/**是否启用保存按钮*/
	hasSave: false,
	/**是否重置按钮*/
	hasReset: true,
	/**带功能按钮栏*/
	hasButtontoolbar: false,
	/**内容周围距离*/
	bodyPadding: '15px 0px 0px 0px',
	/**布局方式*/
	layout: {
		type: 'table',
		columns: 4 //每行有几列
	},
	StatusList:[['1','暂存'],['2','入库']],
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 70,
		width: 180,
		labelAlign: 'right'
	},
	Status:'1',
	
	/**入库类型名称*/
    InDocInTypeList:[],
    StatusIDList:[],
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.getInDocInTypeList();
		me.getStatusIDListData();
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this,
			items = [];
		//供应商
		items.push( {
			fieldLabel: '供应商',name: 'ReaBmsInDoc_CompanyName',itemId: 'ReaBmsInDoc_CompanyName',
			xtype: 'uxCheckTrigger',
			colspan: 2, width: me.defaults.width * 2,
			className: 'Shell.class.rea.client.reacenorg.basic.CheckGrid',
			classConfig: {
				title: '供应商选择',checkOne:true,
				defaultWhere:'reacenorg.OrgType=0',
				width:300
			}
		},{
			fieldLabel: '供货方主键ID',
			hidden: true,
			name: 'ReaBmsInDoc_CompanyID',
			itemId: 'ReaBmsInDoc_CompanyID'
		});
    //入库类型
		items.push({
			fieldLabel: '入库类型',
			emptyText: '必填项',
			name: 'ReaBmsInDoc_InType',
			itemId: 'ReaBmsInDoc_InType',
			xtype: 'uxSimpleComboBox',
			hasStyle: true,
			data: me.InDocInTypeList,
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true,
			value: "1"
		});
		//单据状态
		items.push({
			fieldLabel: '单据状态',
			name: 'ReaBmsInDoc_Status',
			itemId: 'ReaBmsInDoc_Status',
			xtype: 'uxSimpleComboBox',
			hasStyle: true,
			data: me.StatusIDList,
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true,
			value: "2"
		});
		//入库总单号
		items.push({
			fieldLabel: '入库总单号',
			name: 'ReaBmsInDoc_InDocNo',
			colspan: 2,
			width: me.defaults.width * 2
		});
		//	总单金额
		items.push({
			fieldLabel: '合计金额',
			name: 'ReaBmsInDoc_TotalPrice',
			colspan: 1,
			width: me.defaults.width * 1
		});
		items.push({
			fieldLabel: '主键ID',
			name: 'ReaBmsInDoc_Id',
			hidden: true,
			type: 'key'
		});
		//操作日期
		items.push({
			xtype: 'datefield',
			fieldLabel: '操作日期',
			format: 'Y-m-d',
			name: 'ReaBmsInDoc_OperDate',
			itemId: 'ReaBmsInDoc_OperDate',
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true
		},{
			xtype: 'datefield',
			fieldLabel: '操作日期',
			format: 'Y-m-d',
			name: 'ReaBmsInDoc_DataAddTime',
			itemId: 'ReaBmsInDoc_DataAddTime',
			colspan: 1,
			width: me.defaults.width * 1,
			hidden:true
		});
		
	    //操作者
		items.push({
			fieldLabel: '操作者',
			name: 'ReaBmsInDoc_CreaterName',
			itemId: 'ReaBmsInDoc_CreaterName',
			colspan: 1,hidden:true,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true
		}, {
			fieldLabel: '操作人员ID',
			hidden: true,
			name: 'ReaBmsInDoc_CreaterID',
			itemId: 'ReaBmsInDoc_CreaterID'
		});
		//备注
		items.push({
			xtype: 'textarea',
			fieldLabel: '备注',
			name: 'ReaBmsInDoc_Memo',
			itemId: 'ReaBmsInDoc_Memo',
			colspan: 4,
			width: me.defaults.width * 4,
			height: 50
		});
		return items;
	},
	
	/**订货方选择*/
	onCompAccept: function(record) {
		var me = this;
		var ComId = me.getComponent('ReaBmsInDoc_CompanyID');
		var ComName = me.getComponent('ReaBmsInDoc_CompanyName');

		ComId.setValue(record ? record.get('ReaCenOrg_Id') : '');
		ComName.setValue(record ? record.get('ReaCenOrg_CName') : '');
		me.fireEvent('reacompcheck', me, record);
	},
    /**订货方选择*/
	onStorageTypeAccept: function(record) {
		var me = this;
	    var InTypeId = me.getComponent('ReaBmsInDoc_InType');
		var InTypeName = me.getComponent('ReaBmsInDoc_InTypeName');

		InTypeId.setValue(record ? record.get('BStorageType_Id') : '');
		InTypeName.setValue(record ? record.get('BStorageType_Name') : '');
		me.fireEvent('storagetypecheck', me, record);
	},
	
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		if(data.ReaBmsInDoc_DataAddTime) data.ReaBmsInDoc_DataAddTime = JcallShell.Date.toString(data.ReaBmsInDoc_DataAddTime, true);
		if(data.ReaBmsInDoc_OperDate) data.ReaBmsInDoc_OperDate = JcallShell.Date.toString(data.ReaBmsInDoc_OperDate, true);
		return data;
	},
	isShow:function(id){
		var me = this;
		me.setReadOnly(true);
		me.formtype = 'show';
		me.changeTitle();//标题更改
		me.disableControl();
		me.load(id);
	},
		/**客户端入库类型*/
	getInDocInTypeList: function(callback) {
		var me = this;
		if(me.InDocInTypeList.length > 0) return;
		var params = {},
			url = JShell.System.Path.getRootUrl(JcallShell.System.ClassDict._classDicListUrl);
		params = Ext.encode({
			"jsonpara": [{
				"classname": "ReaBmsInDocInType",
				"classnamespace": "ZhiFang.Entity.ReagentSys.Client"
			}]
		});
		me.InDocInTypeList = [];
		var tempArr = [];
		JcallShell.Server.post(url, params, function(data) {
			if(data.success) {
				if(data.value) {
					if(data.value[0].ReaBmsInDocInType.length > 0) {
						me.InDocInTypeList.push(["", '请选择', 'font-weight:bold;text-align:center;']);
						Ext.Array.each(data.value[0].ReaBmsInDocInType, function(obj, index) {
							var style = ['font-weight:bold;text-align:center;'];
							if(obj.BGColor) {
								style.push('color:' + obj.BGColor);
							}
							tempArr = [obj.Id, obj.Name, style.join(';')];
							me.InDocInTypeList.push(tempArr);
						});
					}
				}
			}
		}, false);
	},
		/**获取状态信息*/
	getStatusIDListData: function(callback) {
		var me = this;
		if(me.StatusIDList.length > 0) return;
		var params = {},
			url = JShell.System.Path.getRootUrl(JcallShell.System.ClassDict._classDicListUrl);
		params = Ext.encode({
			"jsonpara": [{
				"classname": "ReaBmsInDocStatus",
				"classnamespace": "ZhiFang.Entity.ReagentSys.Client"
			}]
		});
		me.StatusIDList = [];
		var tempArr = [];
		JcallShell.Server.post(url, params, function(data) {
			if(data.success) {
				if(data.value) {
					if(data.value[0].ReaBmsInDocStatus.length > 0) {
						me.StatusIDList.push(["", '请选择', 'font-weight:bold;text-align:center;']);
						Ext.Array.each(data.value[0].ReaBmsInDocStatus, function(obj, index) {
							var style = ['font-weight:bold;text-align:center;'];
							if(obj.BGColor) {
								style.push('color:' + obj.BGColor);
							}
							tempArr = [obj.Id, obj.Name, style.join(';')];
							me.StatusIDList.push(tempArr);
						});
					}
				}
			}
		}, false);
	}
	
});