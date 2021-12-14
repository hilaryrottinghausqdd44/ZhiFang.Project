/**
 * 职责模板关系
 * @author liangyl
 * @version 2017-11-23
 */
Ext.define('Shell.class.qms.equip.res.register.Grid', {
    extend: 'Shell.ux.grid.Panel',
	requires: [
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.class.qms.equip.templet.basic.CheckTrigger'
	],
	title: '职责模板关系',
	/**获取数据服务路径*/
	selectUrl: '/QMSReport.svc/QMS_UDTO_SearchTempletByEmp?isPlanish=true',
	/**获取职责人员关系数据服务路径*/
	selectResEmpUrl: '/QMSReport.svc/ST_UDTO_SearchEResEmpByHQL?isPlanish=true',
	/**默认排序字段*/
	defaultOrderBy: [{property: 'ETemplet_CName',direction: 'ASC'}],
	/**默认加载数据*/
	defaultLoad: false,
	/**默认选中数据*/
	autoSelect: true,
	/**后台排序*/
	remoteSort: false,
	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**职责数据集*/
	ResponsibilityList:[],
	/**我的职责数据ID */
	ResponsibilityIds:[],
	/**'0', '全部','1', '人员模板','2', '人员岗位模板',*/
	TempletType:'2',
	layout:'fit',
	/**默认每页数量*/
	defaultPageSize: 200,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		var buttonsToolbar = me.getComponent('buttonsToolbar'),
		    DutyName=buttonsToolbar.getComponent('DutyName');

		me.getResponsibility(function(data){
			if(data && data.value){
				me.ResponsibilityList = [];
				me.ResponsibilityIds=[];
				var list=data.value.list;
				for(var i = 0; i < list.length; i++) {
					var Id=list[i].EResEmp_EResponsibility_Id;
					var CName=list[i].EResEmp_EResponsibility_CName;
					me.ResponsibilityIds.push(Id);
					me.ResponsibilityList.push([Id,CName ]);
				}
			}
		});
		me.ResponsibilityList.splice(0,0,['-1','全部']);
		DutyName.loadData(me.ResponsibilityList);
		DutyName.setValue('-1',1);
		var Ids = me.ResponsibilityIds.join(",");
		DutyName.on({
			change:function(com,  newValue, oldValue,  eOpts ){
				me.onSearch();
			}
		});
		me.onSearch();
	},
	initComponent: function() {
		var me = this;
		
		//创建功能按钮栏Items
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//创建数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建功能按钮栏Items*/
	createButtonToolbarItems:function(){
		var me = this,
			buttonToolbarItems =  [];
				//查询框信息
		me.searchInfo = {
			width: 125,
			emptyText: '模板名称',
			isLike: true,
			itemId: 'search',
			fields: ['etemplet.CName']
		};
		buttonToolbarItems.push('refresh','-',{
			fieldLabel: '',
			emptyText: '职责',
			name: 'DutyName',
			itemId: 'DutyName',
			xtype: 'uxSimpleComboBox',
			labelAlign: 'right',
			width: 100
		},{
			fieldLabel:'',labelAlign: 'right',
			emptyText:'小组',labelWidth:0,width: 100,	
			name:'HRDept_CName',itemId:'HRDept_CName',xtype:'uxCheckTrigger1',
			className:'Shell.class.qms.equip.templet.basic.CheckTree',
			classConfig: {
				title: '小组选择',
				/**是否显示根节点*/
	            rootVisible:false,
	            readOnly:true
		       
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
		});
		
		return buttonToolbarItems;
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
			sortable:true,menuDisabled:true,align: 'right',
			renderer: function(value, meta, record) {
				var v = "",Color='';
				//是否审核
				var IsCheck =record.get('ETemplet_IsCheck');
				//是否已填写数据
				var IsFillData =record.get('ETemplet_IsFillData');
				if(!IsCheck)IsCheck='0';
				if(!IsFillData)IsFillData='0';
				//0未审，0未填写数据
				if(IsFillData=='0' && IsCheck=='0' ){
                    Color = '<span style="padding:0px;color:white;"></span>&nbsp;&nbsp;'
				}
				//1已审，1已填写数据
				if(IsFillData=='1' && IsCheck=='1' ){
					Color = '<span style="padding:0px;color:red; border:solid 5px red"></span>&nbsp;&nbsp;'
				}
				//1已审，未填写数据
				if(IsFillData=='0' && IsCheck=='1' ){
					Color = '<span style="padding:0px;color:red; border:solid 5px red"></span>&nbsp;&nbsp;'
				}
				//未审，已填写数据
				if(IsFillData=='1' && IsCheck=='0' ){
					Color = '<span style="padding:0px;color:blue; border:solid 5px blue"></span>&nbsp;&nbsp;'
				}
				v = Color;
				return v;
			}
		},{
			text:'是否审核',dataIndex:'ETemplet_IsCheck',width:50,hidden:false,
			hidden:true,sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'是否已填写数据',dataIndex:'ETemplet_IsFillData',width:50,hidden:false,
			hidden:true,sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'质量记录名称',dataIndex:'ETemplet_CName',flex:1,minWidth:180,
			sortable:true,
			renderer: function(value, meta, record) {
				value=me.changeColor(value, meta, record,true);
	            return value;
			}
		},{
			text:'模板id',dataIndex:'ETemplet_Id',isKey: true,hidden:true,
			sortable:true,defaultRenderer:true
		},{
			text:'小组',dataIndex:'ETemplet_Section_CName',width:100,hidden:false,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'登记类型',dataIndex:'ETemplet_CheckType',width:70,
			align: 'center',sortable:true,
			renderer : function(value, meta) {
				var v = value + '';
				if (v == '1') {
					v ='按天登记';
				} else if (v == '0') {
					v = '按月登记';
				} else {
					v == '';
				}

				return v;
			}
		},{
			text:'仪器名称',dataIndex:'ETemplet_EEquip_CName',width:100,hidden:false,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'填充类型',dataIndex:'ETemplet_FillType',width:70,hidden:true,
			align: 'center',sortable:true,defaultRenderer:true
		},{
			text:'不为空 ,显示列表(模板多次记录数据)',dataIndex:'ETemplet_ShowFillItem',width:100,hidden:true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		}];
		return columns;
	},
	/**获取当前登录者的职责信息*/
	getResponsibility:function(callback){
		var me = this;
        if(!JShell.System.Cookie.map.USERID){
			JShell.Msg.error('用户登录信息不存在，请重新登录后操作！');
			return;
		}
        var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		var url = JShell.System.Path.ROOT + me.selectResEmpUrl.split('?')[0] + 
				'?fields=EResEmp_Id,EResEmp_EResponsibility_Id,EResEmp_EResponsibility_CName' +
				'&isPlanish=true&where=eresemp.HREmployee.Id ='+userId+
				'&sort=[{"property":"EResEmp_EResponsibility_CName","direction":"ASC"}]';
		
		JShell.Server.get(url,function(data){
			if(data.success){
				callback(data);
			}else{
				JShell.Msg.error(data.msg);
			}
		},false);
	},
		/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			arr = [],
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			search = null,DutyName=null,SectionId=null;
        if(!buttonsToolbar) return;
        var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;

		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.selectUrl;

		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true).join(',');
		url+='&relationType='+me.TempletType+'&empID='+userId;
		if(buttonsToolbar){
			DutyName = buttonsToolbar.getComponent('DutyName').getValue();
			search = buttonsToolbar.getComponent('search').getValue();
			SectionId = buttonsToolbar.getComponent('HRDept_Id').getValue();
		}
		
		if(DutyName && DutyName!='-1'){
			url+="&resWhere=etempletres.EResponsibility.Id in (" + DutyName + ")";
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