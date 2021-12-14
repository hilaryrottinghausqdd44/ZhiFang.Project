/**
 * 项目监控合同选择
 * @author longfc
 * @version 2017-03-27
 */
Ext.define('Shell.class.wfm.business.pproject.contract.CheckGrid', {
	extend: 'Shell.ux.grid.CheckPanel',
	title: '合同选择列表',
	height: 460,
	width: 350,
	/**获取数据服务路径*/
	selectUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPContractByHQL?isPlanish=false',
	/**是否单选*/
	checkOne: true,
	/**时间类型列表*/
	DateTypeList:[
		['DataAddTime','创建时间'],['ApplyDate','申请时间'],
		['SignDate','签署日期'],['ReviewDate','商务评审时间'],
		['TechReviewDate','技术评审时间']
	],
	defaultDateType:'DataAddTime',
	afterRender: function () {
        var me = this;
        me.callParent(arguments);
        //初始化监听
        me.initListeners();
    },
	initComponent: function() {
		var me = this;
		//数据列
		me.columns = me.createGridColumns();

		me.callParent(arguments);
	},

	initButtonToolbarItems:function(){
		var me = this;
	    me.searchInfo = {width: 135,emptyText: '名称',itemId:'search',isLike: true,fields: ['pcontract.Name']};
		if(me.checkOne){
			if(!me.searchInfo.width) me.searchInfo.width = 145;
			//自定义按钮功能栏
			me.buttonToolbarItems = me.buttonToolbarItems || [];
			me.buttonToolbarItems.push({
				fieldLabel: '部门',emptyText: '部门选择',name: 'DeptName',itemId: 'DeptName',xtype: 'uxCheckTrigger',
				labelAlign: 'right',className: 'Shell.class.wfm.service.accept.CheckGrid',labelWidth: 30,width: 140,
				classConfig: {height: 450,checkOne: true,title: '部门选择'}
			}, {
				fieldLabel: '所属部门ID',emptyText: '所属部门ID',name: 'DeptID',itemId: 'DeptID',hidden: true,xtype: 'textfield'
			},{
				labelWidth: 70,width: 160,xtype:'uxCheckTrigger',itemId:'UserName',
				className:'Shell.class.sysbase.user.CheckApp',
				fieldLabel: '销售负责人',emptyText: '销售负责人'
			},{
				xtype:'textfield',itemId:'UserID',fieldLabel:'申请人主键ID',hidden:true
			},{
				width:160,labelWidth:60,labelAlign:'right',
				xtype:'uxSimpleComboBox',itemId:'DateType',fieldLabel:'时间范围',
				data:me.DateTypeList,
				value:me.defaultDateType
			},{
				width:95,itemId:'BeginDate',xtype:'datefield',format:'Y-m-d'
			},{
				width:100,labelWidth:5,fieldLabel:'-',labelSeparator:'',
				itemId:'EndDate',xtype:'datefield',format:'Y-m-d'
			},{type:'search',info:me.searchInfo});
			
			if(me.hasClearButton){
				me.buttonToolbarItems.unshift({
					text:'清除',iconCls:'button-cancel',tooltip:'<b>清除原先的选择</b>',
					handler:function(){me.fireEvent('accept',me,null);}
				},'-','->');
			}
		
			if(me.hasAcceptButton){
				me.buttonToolbarItems.push('-','->','accept');
			}
		}else{
			if(!me.searchInfo.width) me.searchInfo.width = 205;
			//自定义按钮功能栏
			me.buttonToolbarItems = [{type:'search',info:me.searchInfo}];
			if(me.hasAcceptButton) me.buttonToolbarItems.push('->','accept');
		}
	},
	
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			text: '合同编号',
			dataIndex: 'ContractNumber',
			width: 100,
			sortable: false,
			defaultRenderer: true
		}, {
			text: '客户名称',
			dataIndex: 'PClientName',
			flex:1,
//			width: 150,
			sortable: false,
			defaultRenderer: true
		}, {
			text: '客户Id',
			dataIndex: 'PClientID',
			hidden: true,
			sortable: false,
			defaultRenderer: true
		}, {
			text: '合同名称',
			dataIndex: 'Name',
			flex:1,
			sortable: false,
			defaultRenderer: true
		}, {
			text: '主键ID',
			dataIndex: 'Id',
			isKey: true,
			hidden: true,
			hideable: false
		}, {
			text: '合同类型',
			dataIndex: 'Content',
			width: 150,
			hidden: true,
			sortable: false,
			defaultRenderer: true
		}, {
			text: '合同类型Id',
			dataIndex: 'ContentID',
			hidden: true,
			sortable: false,
			defaultRenderer: true
		}, {
			text: '省份',
			dataIndex: 'ProvinceName',
			hidden: true,
			sortable: false,
			defaultRenderer: true
		}, {
			text: '省份Id',
			dataIndex: 'ProvinceID',
			hidden: true,
			sortable: false,
			defaultRenderer: true
		},{
			text: '实施负责人',
			dataIndex: 'PManager',
			hidden: true,
			sortable: false,
			defaultRenderer: true
		}, {
			text: '实施负责人ID',
			dataIndex: 'PManagerID',
			hidden: true,
			sortable: false,
			defaultRenderer: true
		},{
			text: '销售负责人',
			dataIndex: 'Principal',
			hidden: true,
			sortable: false,
			defaultRenderer: true
		}, {
			text: '销售负责人ID',
			dataIndex: 'PrincipalID',
			hidden: true,
			sortable: false,
			defaultRenderer: true
		}, {
			text: '创建时间',dataIndex: 'DataAddTime',width: 85,sortable: false,isDate: true,defaultRenderer: true
		},{
			text: '签署日期',dataIndex: 'SignDate',width: 85,sortable: false,isDate: true,defaultRenderer: true
		}, {
			text: '商务评审时间',dataIndex: 'ReviewDate',width: 85,sortable: false,isDate: true,defaultRenderer: true
		},{
			text: '技术评审时间',dataIndex: 'TechReviewDate',width: 85,sortable: false,isDate: true,defaultRenderer: true
		}];
		
		return columns;
	},
	/**改变默认条件*/
	changeDefaultWhere:function(){
		var me = this;
		me.defaultWhere = '(pcontract.ContractStatus=6 or pcontract.ContractStatus=7)';
		//defaultWhere追加上IsUse约束
		if(me.defaultWhere){
			var index = me.defaultWhere.indexOf('pcontract.IsUse=1');
			if(index == -1){
				me.defaultWhere += ' and pcontract.IsUse=1';
			}
		}else{
			me.defaultWhere = 'pcontract.IsUse=1';
		}
	},
	initListeners:function(){
		var me=this;
		var buttonsToolbar= me.getComponent('buttonsToolbar');
		
		var DeptID = buttonsToolbar.getComponent('DeptID'),
		DeptName = buttonsToolbar.getComponent('DeptName');

	    DeptName.on({
			check: function(p, record) {
				DeptName.setValue(record ? record.get('HRDept_CName') : '');
				DeptID.setValue(record ? record.get('HRDept_Id') : '');
				p.close();
			},change:function(){me.onGridSearch();}
		});
		
		//人员
		var UserName = buttonsToolbar.getComponent('UserName'),
			UserID = buttonsToolbar.getComponent('UserID');

		if(UserName){
			UserName.on({
				check:function(p, record) {
					UserName.setValue(record ? record.get('HREmployee_CName') : '');
					UserID.setValue(record ? record.get('HREmployee_Id') : '');
					p.close();
				},
				change:function(){me.onGridSearch();}
			});
		}
		
       //时间类型+时间
		var DateType = buttonsToolbar.getComponent('DateType'),
			BeginDate = buttonsToolbar.getComponent('BeginDate'),
			EndDate = buttonsToolbar.getComponent('EndDate');
		if(DateType) {
			DateType.on({
				change: function(com, newValue, oldValue, eOpts) {
					if(BeginDate.getValue() && EndDate.getValue()){
						me.onGridSearch();
					}
				}
			});
		}
		if(BeginDate) {
			BeginDate.on({
				change: function(com, newValue, oldValue, eOpts) {
					if(newValue && EndDate.getValue()){
						me.onGridSearch();
					}				
				}
			});
		}
		if(EndDate) {
			EndDate.on({
				change: function(com, newValue, oldValue, eOpts) {
					if(newValue && BeginDate.getValue()){
						me.onGridSearch();
					}
				}
			});
		}
	},
	    /**综合查询*/
	onGridSearch:function(){
		var me = this;
		JShell.Action.delay(function(){
			me.onSearch();
		},100);
	},
	 /**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			DateType = null,BeginDate = null,EndDate = null,DeptID = null,UserID = null,
			search = null,params = [];
	
	    me.changeDefaultWhere();
		me.internalWhere = '';
			
		if(buttonsToolbar){
			UserID = buttonsToolbar.getComponent('UserID').getValue();
		    DeptID = buttonsToolbar.getComponent('DeptID').getValue();
		    DateType = buttonsToolbar.getComponent('DateType').getValue();
			BeginDate = buttonsToolbar.getComponent('BeginDate').getValue();
			EndDate = buttonsToolbar.getComponent('EndDate').getValue();
			search = buttonsToolbar.getComponent('search').getValue();
		}
		//UserID
		if(UserID) {
			params.push("pcontract.PrincipalID='" + UserID + "'");
		}
		//DeptID
		if(DeptID) {
			params.push("pcontract.DeptID='" + DeptID + "'");
		}
		
		//时间
		if(DateType) {
			if(BeginDate) {
				params.push("pcontract." + DateType + ">='" + JShell.Date.toString(BeginDate, true) + "'");
			}
			if(EndDate) {
				params.push("pcontract." + DateType + "<'" + JShell.Date.toString(JShell.Date.getNextDate(EndDate), true) + "'");
			}
		}
		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
		if(search) {
			if(me.internalWhere) {
				me.internalWhere += ' and (' + me.getSearchWhere(search) + ')';
			} else {
				me.internalWhere = me.getSearchWhere(search);
			}
		}
		return me.callParent(arguments);
	}
});