/**
 * 订货总单列表-管理员专用
 * @author Jcall
 * @version 2017-07-17
 */
Ext.define('Shell.class.rea.order.manage.DocGrid', {
	extend: 'Shell.ux.grid.Panel',
	requires:[
	    'Shell.ux.form.field.SimpleComboBox',
	    'Shell.ux.form.field.CheckTrigger'
    ],
	title: '订货总单列表-管理员专用',
	
	/**获取数据服务路径*/
	selectUrl: '/ReagentSysService.svc/ST_UDTO_SearchBmsCenOrderDocByHQL?isPlanish=true',
	/**删除状态变更服务路径*/
	deleteFlagUrl:'/ReagentService.svc/RS_UDTO_SetOrderDocDeleteFlagByID',
	
	/**默认加载*/
	defaultLoad: true,
	/**后台排序*/
	remoteSort: true,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**是否启用序号列*/
	hasRownumberer: true,
	
	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
	hasDel: true,
	
	/**排序字段*/
	defaultOrderBy:[{property:'BmsCenOrderDoc_OperDate',direction:'DESC'}],
	
	/**默认单据状态*/
	defaultStatusValue:0,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		var buttonsToolbar = me.getComponent('buttonsToolbar'),
			LabName = buttonsToolbar.getComponent('LabName'),
			LabId = buttonsToolbar.getComponent('LabID'),
			CompName = buttonsToolbar.getComponent('CompName'),
			CompId = buttonsToolbar.getComponent('CompID');

		if(LabName){
			LabName.on({
				check:function(p, record) {
					LabName.setValue(record ? record.get('CenOrg_CName') : '');
					LabId.setValue(record ? record.get('CenOrg_Id') : '');
					p.close();
				}
			});
		}
		if(CompName){
			CompName.on({
				check:function(p, record) {
					CompName.setValue(record ? record.get('CenOrg_CName') : '');
					CompId.setValue(record ? record.get('CenOrg_Id') : '');
					p.close();
				}
			});
		}
	},
	initComponent: function() {
		var me = this;
		
		//查询框信息
		me.searchInfo = {
			width: 130,
			emptyText: '订货单号',
			itemId:'search',
			isLike: true,
			fields: ['bmscenorderdoc.OrderDocNo']
		};
		me.buttonToolbarItems = me.buttonToolbarItems || ['refresh',{
			xtype:'splitbutton',
            textAlign:'left',
			iconCls:'button-edit',
			text:'操作',
			handler:function(btn,e){btn.overMenuTrigger = true;btn.onClick(e);},
			menu:[{
				text:'数据删除',iconCls:'button-cancel',
				listeners:{click:function(but) {me.onChangeDeleteFlag(1);}}
			},{
				text:'数据还原',iconCls:'button-accept',
				listeners:{click:function(but) {me.onChangeDeleteFlag(0);}}
			}]
		},'-',{
			fieldLabel:'供货方',xtype:'uxCheckTrigger',itemId:'CompName',
			width:160,labelWidth:50,labelAlign:'right',
			className:'Shell.class.rea.cenorg.CheckGrid'
		},{
			fieldLabel:'供货方主键ID',itemId:'CompID',xtype:'textfield',hidden:true
		},{
			fieldLabel:'订货方',xtype:'uxCheckTrigger',itemId:'LabName',
			width:160,labelWidth:50,labelAlign:'right',
			className:'Shell.class.rea.cenorg.CheckGrid'
		},{
			fieldLabel:'订货方主键ID',itemId:'LabID',xtype:'textfield',hidden:true
		},'-',{
			fieldLabel:'单据状态',xtype:'uxSimpleComboBox',
			itemId:'status',allowBlank:false,value:me.defaultStatusValue,
			width:130,labelWidth:60,labelAlign:'right',hasStyle:true,
			data:JcallShell.REA.Enum.getList('BmsCenOrderDoc_Status',true,true),
			listeners:{change:function(){me.onSearch();}}
		},'-',{
			fieldLabel:'删除标志',xtype:'uxSimpleComboBox',
			itemId:'DeleteFlag',allowBlank:false,value:'-1',
			width:120,labelWidth:60,labelAlign:'right',hasStyle:true,
			data:[
				['-1','全部','font-weight:bold;color:black;'],
				['0','正常','font-weight:bold;color:green;'],
				['1','删除','font-weight:bold;color:red;']
			],
			listeners:{change:function(){me.onSearch();}}
		},'-',{
			type: 'search',
			info: me.searchInfo
		}];
		
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var columns = [{
			dataIndex: 'BmsCenOrderDoc_DeleteFlag',
			text: '删除标志',
			align:'center',
			width: 60,
			renderer: function(value, meta) {
				var v = '正常',
					color = 'green';
					
				if(value == '1'){
					v = '删除';
					color = 'red';
				}
				if (v) meta.tdAttr = 'data-qtip="<b style=\'color:' + color + '\'>' + v + '</b>"';
				meta.style = 'color:' + color;
				return v;
			}
		},{
			dataIndex: 'BmsCenOrderDoc_OperDate',
			text: '订购日期',
			align:'center',
			width: 130,
			isDate:true,
			hasTime:true
		},{
			dataIndex: 'BmsCenOrderDoc_Comp_CName',
			text: '供货方',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenOrderDoc_Lab_CName',
			text: '订货方',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenOrderDoc_SaleDocNo',
			text: '订货单号',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenOrderDoc_TotalPrice',
			text: '总价',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenOrderDoc_UrgentFlag',
			text: '紧急标志',
			align:'center',
			width: 60,
			renderer: function(value, meta) {
				var info = JShell.REA.Enum.BmsCenOrderDoc_UrgentFlag['E' + value] || {};
				var v = info.value || '';
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = 'background-color:' + (info.bcolor || '#FFFFFF') + 
					';color:' + (info.color || '#000000');
				return v;
			}
		},{
			dataIndex: 'BmsCenOrderDoc_Status',
			text: '单据状态',
			align:'center',
			width: 60,
			renderer: function(value, meta) {
				var info = JShell.REA.Enum.BmsCenOrderDoc_Status['E' + value] || {};
				var v = info.value || '';
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = 'background-color:' + (info.bcolor || '#FFFFFF') + 
					';color:' + (info.color || '#000000');
				return v;
			}
		},{
			dataIndex: 'BmsCenOrderDoc_UserName',
			text: '订购人员',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenOrderDoc_Checker',
			text: '审核人员',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenOrderDoc_CheckTime',
			text: '审核日期',
			align:'center',
			width: 130,
			isDate:true,
			hasTime:true
		},{
			dataIndex: 'BmsCenOrderDoc_Confirm',
			text: '确认人员',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenOrderDoc_ConfirmTime',
			text: '确认时间',
			width: 130,
			isDate:true,
			hasTime:true
		},{
			dataIndex: 'BmsCenOrderDoc_Memo',
			text: '备注',
			width: 200,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenOrderDoc_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}];
		
		return columns;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			arr = [];

		me.internalWhere = me.getInternalWhere();

		return me.callParent(arguments);;
	},
	/**获取内部条件*/
	getInternalWhere:function(){
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			CompID = buttonsToolbar.getComponent('CompID'),
			LabID = buttonsToolbar.getComponent('LabID'),
			status = buttonsToolbar.getComponent('status'),
			DeleteFlag = buttonsToolbar.getComponent('DeleteFlag'),
			search = buttonsToolbar.getComponent('search'),
			where = [];
		
		if(CompID){
			var value = CompID.getValue();
			if(value){
				where.push('bmscenorderdoc.Comp.Id=' + value);
			}
		}
		if(LabID){
			var value = LabID.getValue();
			if(value){
				where.push('bmscenorderdoc.Lab.Id=' + value);
			}
		}
		if(status){
			var value = status.getValue();
			if(value){
				where.push('bmscenorderdoc.Status=' + value);
			}
		}
		if(DeleteFlag){
			var value = DeleteFlag.getValue();
			if(value == '0'){
				where.push('(bmscenorderdoc.DeleteFlag=0 or bmscenorderdoc.DeleteFlag is null)');
			}else if(value == '1'){
				where.push('bmscenorderdoc.DeleteFlag=1');
			}
		}
		if(search){
			var value = search.getValue();
			if(value){
				var searchWhere = me.getSearchWhere(value);
				if(searchWhere){
					where.push('(' + searchWhere + ')');
				}
				
			}
		}
		
		return where.join(" and ");
	},
	/**修改删除标志*/
	onChangeDeleteFlag:function(value){
		var me = this,
			records = me.getSelectionModel().getSelection(),
			len = records.length;

		if (len == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		
		var url = JShell.System.Path.ROOT + me.deleteFlagUrl;
		var idList = [];
		for(var i=0;i<len;i++){
			idList.push(records[i].get(me.PKField));
		}
		
		var params = {
			idList:idList.join(','),
			deleteFlag:value
		};

		JShell.Msg.del(function(but) {
			if (but != "ok") return;

			me.showMask('数据处理中...'); //显示遮罩层
			JShell.Server.post(url,Ext.JSON.encode(params),function(data){
				me.hideMask();//隐藏遮罩层
				if(data.success){
					JShell.Msg.alert('数据处理成功！',null,1000);
					me.onSearch();
				}else{
					JShell.Msg.error(data.msg);
				}
			});
		});
	}
});