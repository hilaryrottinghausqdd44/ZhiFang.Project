/**
 * 模板列表(简单)
 * @author liangyl
 * @version 2016-08-24
 */
Ext.define('Shell.class.qms.equip.register.SimpleGrid', {
	extend: 'Shell.ux.grid.Panel',
	title: '仪器模板列表',
	requires: [
		'Shell.class.qms.equip.templet.basic.CheckTrigger'
	],
	/**获取数据服务路径*/
	selectUrl: '/QMSReport.svc/QMS_UDTO_SearchTempletByEmp?isPlanish=true',
	/**默认排序字段*/
	defaultOrderBy: [{
		property: 'ETemplet_CName',
		direction: 'ASC'
	}],
	height:500,
	width:400,	
	/**默认加载数据*/
	defaultLoad: true,
	/**默认选中数据*/
	autoSelect: true,
	autoScroll: false,
	/**主键列*/
	PKField: 'Id',
	defaultWhere:'',
	/**默认每页数量*/
	defaultPageSize: 200,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);  
	},
	initComponent: function() {
		var me = this;

		//查询框信息
		me.searchInfo = {
			width: 125,
			emptyText: '模板名称',
			isLike: true,
			itemId: 'search',
			fields: ['etemplet.CName']
		};
		me.buttonToolbarItems = ['refresh','-',{
			fieldLabel:'',labelAlign: 'right',
			emptyText:'小组',labelWidth:0,width: 125,	
			name:'HRDept_CName',itemId:'HRDept_CName',xtype:'uxCheckTrigger1',
			className:'Shell.class.qms.equip.templet.basic.CheckTree',
			classConfig: {
				title: '小组选择',
				/**是否显示根节点*/
	            rootVisible:false
			},
			listeners: {
				check: function(p, record) {
					var	buttonsToolbar = me.getComponent('buttonsToolbar'),
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
		//创建数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**改变颜色*/
	changeColor:function(value, meta, record,isName){
		var me = this;
		var v = "",Color='';
		v =value;
		//是否审核
		var IsCheck =record.get('ETemplet_IsCheck');
		//是否已填写数据
		var IsFillData =record.get('ETemplet_IsFillData');
		if(!IsCheck)IsCheck='0';
		if(!IsFillData)IsFillData='0';
		//0未审，0未填写数据
		if(IsFillData=='0' && IsCheck=='0' ){
            Color = '<span style="padding:0px;color:white;"></span>&nbsp;&nbsp;'
		    v = Color + value;
		}
		//1已审，1已填写数据
		if(IsFillData=='1' && IsCheck=='1' ){
			Color = '<span style="padding:0px;color:red; border:solid 5px red"></span>&nbsp;&nbsp;'
		    v = Color + value;
		}
		//1已审，未填写数据
		if(IsFillData=='0' && IsCheck=='1' ){
			Color = '<span style="padding:0px;color:red; border:solid 5px red"></span>&nbsp;&nbsp;'
		    v = Color + value;
		}
		//未审，已填写数据
		if(IsFillData=='1' && IsCheck=='0' ){
			Color = '<span style="padding:0px;color:blue; border:solid 5px blue"></span>&nbsp;&nbsp;'
		    v = Color + value;
		}
		//按天审核  1已审，1已填写数据
		if(IsFillData=='2'){
			meta.style = 'background-color:#008B00;color:#ffffff;';
			if(isName){
				meta.style +='margin:0px 0px 0px 7px;';
			}
			v =  value;
		}
		return v;
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			text:'',dataIndex:'',width:13,hidden:true,
			sortable:true,menuDisabled:true,align: 'center',
			renderer: function(value, meta, record) {
				var v = "",Color='';
				v =value;
				//是否审核
				var IsCheck =record.get('ETemplet_IsCheck');
				//是否已填写数据
				var IsFillData =record.get('ETemplet_IsFillData');
				if(!IsCheck)IsCheck='0';
				if(!IsFillData)IsFillData='0';
				//0未审，0未填写数据
				if(IsFillData=='0' && IsCheck=='0' ){
                    Color = '<span style="padding:0px;color:white;"></span>&nbsp;&nbsp;'
			    	v = Color + value;
				}
				//1已审，1已填写数据
				if(IsFillData=='1' && IsCheck=='1' ){
					Color = '<span style="padding:0px;color:red; border:solid 5px red"></span>&nbsp;&nbsp;'
				    v = Color + value;
				}
				return v;
			}
		},{
			text:'是否审核',dataIndex:'ETemplet_IsCheck',width:50,hidden:false,
			hidden:true,sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'是否已填写数据',dataIndex:'ETemplet_IsFillData',width:100,hidden:false,
			hidden:true,sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'编号',dataIndex:'Id',width:10,
			sortable:false,menuDisabled:true,defaultRenderer:true,hidden:true
		},{
			text:'仪器模板id',dataIndex:'ETemplet_Id',width:10,
			sortable:false,menuDisabled:true,defaultRenderer:true,hidden:true
		},{
			text:'质量记录名称',dataIndex:'ETemplet_CName',flex:1,
			sortable:true,menuDisabled:true,
			renderer: function(value, meta, record) {
				value=me.changeColor(value, meta, record,true);
	            return value;
			}
		},{
			text:'小组',dataIndex:'ETemplet_Section_CName',width:100,hidden:false,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'仪器名称',dataIndex:'ETemplet_EEquip_CName',width:100,hidden:false,
			sortable:false,menuDisabled:true,defaultRenderer:true
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
		},{
			text:'填充类型',dataIndex:'ETemplet_FillType',width:70,hidden:true,
			align: 'center',sortable:true,defaultRenderer:true
		},{
			text:'审核类型',dataIndex:'ETemplet_CheckType',width:70,
			align: 'center',sortable:true,hidden:false,
			renderer : function(value, meta) {
				var v = value + '';
				if (v == '1') {
					v ='按天审核';
				} else if (v == '0') {
					v = '按月审核';
				} else {
					v == '';
				}

				return v;
			}
		},{
			text:'不为空 ,显示列表(模板多次记录数据)',dataIndex:'ETemplet_ShowFillItem',width:100,hidden:true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		}];
		return columns;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			arr = [],
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			search = null,HRDeptId=null;
        if(!buttonsToolbar) return;
        var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;

		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.selectUrl;

		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true).join(',');
		url+='&relationType=1&empID='+userId;
//		if(buttonsToolbar){
//			DutyName = buttonsToolbar.getComponent('DutyName').getValue();
//			search = buttonsToolbar.getComponent('search').getValue();
//			SectionId = buttonsToolbar.getComponent('HRDept_Id').getValue();
//		}
		if(buttonsToolbar){
		    HRDeptId=buttonsToolbar.getComponent('HRDept_Id').getValue();
		    search = buttonsToolbar.getComponent('search').getValue();
		}
		
		
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
		if(HRDeptId){
			arr.push("etemplet.Section.Id=" + HRDeptId);
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