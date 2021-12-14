/**
 * 模板
 * @author liangyl
 * @version 2016-08-12
 */
Ext.define('Shell.class.qms.equip.templet.emaintenancedata.TempletGrid', {
    extend: 'Shell.ux.grid.Panel',
	title: '仪器模板列表',
	requires: [
		'Shell.class.qms.equip.templet.basic.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox'
	],
	/**获取数据服务路径*/
	selectUrl: '/QMSReport.svc/QMS_UDTO_SearchTempletByEmp?isPlanish=true',
	/**默认排序字段*/
	defaultOrderBy: [{
		property: 'ETemplet_CName',
		direction: 'ASC'
	}],
	height:500,
	width:280,	
	/**默认加载数据*/
	defaultLoad: true,
	/**默认选中数据*/
	autoSelect: true,
	autoScroll: false,
	/**主键列*/
	PKField: 'ETempletEmp_Id',
	defaultWhere:'',
	/**'0', '全部','1', '人员模板','2', '人员岗位模板',对外公开设置默认值*/
	TEMPTLETTYPE:'0',
		/**模板列表*/
	RoleList: [
		['0', '全部', 'font-weight:bold;color:black;'],
		['1', '人员模板', 'font-weight:bold;color:#66CD00;'],
		['2', '人员岗位模板', 'font-weight:bold;color:#DC143C;']
	],
		/**带分页栏*/
	hasPagingtoolbar: true,
	/**默认每页数量*/
	defaultPageSize: 50,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);  
	},
	initComponent: function() {
		var me = this;
			//创建功能按钮栏Items
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//创建数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		
		if (me.hasButtontoolbar) items.push(me.createButtontoolbar());
		if (me.hasPagingtoolbar) items.push(me.createPagingtoolbar());
		items.push(me.createDefaultButtonToolbarItems());

		return items;
	},
	/**默认按钮栏*/
	createDefaultButtonToolbarItems:function(){
		var me = this;
			//查询框信息
		me.searchInfo = {
			width: 115,
			emptyText: '模板名称',
			isLike: true,
			itemId: 'search',
			fields: ['etemplet.CName']
		};
		var	items =  [{
			fieldLabel:'',labelAlign: 'right',
			emptyText:'小组',labelWidth:0,width: 138,	
			name:'HRDept_CName',itemId:'HRDept_CName',xtype:'uxCheckTrigger1',
			className:'Shell.class.qms.equip.templet.basic.CheckTree',
			classConfig: {
				title: '小组选择',
				/**是否显示根节点*/
	            rootVisible:false
			},
			listeners: {
				check: function(p, record) {
					var	buttonsToolbar = me.getComponent('buttonsToolbar2'),
				        Id = buttonsToolbar.getComponent('HRDept_Id'),
			            CName = buttonsToolbar.getComponent('HRDept_CName');
                    if(record==null){
			    		CName.setValue('');
				    	Id.setValue('');
				    	p.close();
			    	    me.onSearch();
			    	    return;
			    	}
			    	if(record.data){
			    		CName.setValue(record.data ? record.data.text : '');
				    	Id.setValue(record.data ? record.data.tid : '');
				    	p.close();
			    	    me.onSearch();
			    	}
				}
			}
		},{
			fieldLabel:'小组ID',hidden:true,
			name:'HRDept_Id',xtype: 'textfield',itemId:'HRDept_Id'
		},{
			type: 'search',
			info: me.searchInfo
		}];

		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'buttonsToolbar2',
			items: items
		});
		
		
		return items;
	},
	/**创建功能按钮栏Items*/
	createButtonToolbarItems:function(){
		var me = this,
			buttonToolbarItems = me.buttonToolbarItems || [];
		buttonToolbarItems.unshift('refresh','-');
		buttonToolbarItems.push({
			width: 95,
			labelWidth: 0,
			fieldLabel: '',
			xtype: 'uxSimpleComboBox',
			itemId: 'TempletType',
			hasStyle: true,
			value: me.TEMPTLETTYPE,
			data: me.RoleList,
			listeners :{
				change:function( com,  newValue,  oldValue,  eOpts ){
					me.onSearch();
				}
			}
			
		});
		
		return buttonToolbarItems;
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			text:'质量记录名称',dataIndex:'ETemplet_Id',width:100,hidden:true,
			sortable:true,menuDisabled:true,isKey: true,defaultRenderer:true
		},{
			text:'质量记录名称',dataIndex:'ETemplet_CName',flex:1,
			sortable:true,menuDisabled:true,defaultRenderer:true
		},{
			text:'模板代码',dataIndex:'ETemplet_UseCode',width:100,hidden:true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text: '开始时间',
			dataIndex: 'ETemplet_BeginDate',
			width: 80,
			sortable:false,menuDisabled:true,hidden:true,
			isDate:true
		}, {
			text: '结束时间',
			dataIndex: 'ETemplet_EndDate',
			width: 80,
			isDate:true,hidden:true,
			sortable:false,menuDisabled:true
		}];
		return columns;
	},
		/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			arr = [],
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			search = null,SectionId=null,TempletType=null;
       if(!buttonsToolbar) return;
		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.selectUrl;

		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true).join(',');
		
		TempletType = buttonsToolbar.getComponent('TempletType').getValue();
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;

		url+='&relationType='+TempletType+'&empID='+userId;
	
		me.internalWhere='';
		//默认条件
		if (me.defaultWhere && me.defaultWhere != '') {
			arr.push(me.defaultWhere);
		}
		//内部条件
		if (me.internalWhere && me.internalWhere != '') {
			arr.push(me.internalWhere);
		}
		//外部条件
		if (me.externalWhere && me.externalWhere != '') {
			arr.push(me.externalWhere);
	 	}
        var	buttonsToolbar2 = me.getComponent('buttonsToolbar2');
		if(buttonsToolbar2){
			search = buttonsToolbar2.getComponent('search').getValue();
		    SectionId = buttonsToolbar2.getComponent('HRDept_Id').getValue();
		}
		if(SectionId){
			arr.push("etemplet.Section.Id=" + SectionId);
		}
		if(search) {
			if(me.internalWhere) {
				arr.push(' and (' + me.getSearchWhere(search) + ')') ;
			} else {
				arr.push(me.getSearchWhere(search));
			}
		}
	    if(arr.length > 0) {
			me.internalWhere = arr.join(' and ');
		} else {
			me.internalWhere = '';
		}
	  
		
		var where = arr.join(") and (");
		if (where) where = "(" + where + ")";

		if (where) {
			url += '&where=' + JShell.String.encode(where);
		}

		return url;
	}
});