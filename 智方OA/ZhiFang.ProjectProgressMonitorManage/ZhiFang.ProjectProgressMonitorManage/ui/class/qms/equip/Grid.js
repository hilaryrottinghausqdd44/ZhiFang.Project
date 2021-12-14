/**
 * 仪器列表
 * @author liangyl
 * @version 2016-08-12
 */
Ext.define('Shell.class.qms.equip.Grid', {
	extend: 'Shell.class.qms.equip.SimpleGrid',
	title: '仪器列表',
	/**默认排序字段*/
	defaultOrderBy: [{property: 'EEquip_CName',direction: 'ASC'},{property: 'EEquip_DispOrder',direction: 'ASC'}],
	/**默认加载数据*/
	defaultLoad: true,
	/**默认选中数据*/
	autoSelect: true,
	hasRefresh:true,
	hasAdd:true,
	hasEdit:true,
	hasDel:true,
	/**是否启用查询框*/
	hasSearch: true,
	/**默认每页数量*/
	defaultPageSize: 200,
	 /**下载Excel文件*/
	downLoadExcelUrl:'/QMSReport.svc/QMS_UDTO_DownLoadExcel',
	/**导出Excel文件*/
	reportExcelUrl:'/QMSReport.svc/QMS_UDTO_GetReportDetailExcelPath',


    afterRender: function() {
		var me = this;
		me.callParent(arguments);
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var len =0;
		if(buttonsToolbar.items.length>2){
			len=buttonsToolbar.items.length-2;
		}
		buttonsToolbar.insert(len,['-',{
			text:'导出',iconCls: 'file-excel',tooltip: '导出复合条件的货品信息到EXCEL',
			listeners:{click:function(but) {me.onExportExcel(2,'2');}}
		}]);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this,
			columns = me.callParent(arguments);
		columns.push({
			text:'仪器编号',dataIndex:'EEquip_EquipNo',width:100,
			sortable:true,defaultRenderer:true
		},{
			text:'英文名称',dataIndex:'EEquip_EName',width:100,
			sortable:true,defaultRenderer:true
		},{
			text:'简称',dataIndex:'EEquip_SName',width:100,
			sortable:true,defaultRenderer:true
		},{
			text:'快捷码',dataIndex:'EEquip_Shortcode',width:100,
			sortable:true,defaultRenderer:true
		},{
			text:'拼音字头',dataIndex:'EEquip_PinYinZiTou',width:100,
			sortable:true,defaultRenderer:true
		},{
			text:'仪器类型',dataIndex:'EEquip_EquipType_CName',width:100,
			sortable:true,defaultRenderer:true
		},{
			text:'厂商',dataIndex:'EEquip_FactoryName',flex:1,minWidth:100,
			sortable:true,defaultRenderer:true
		},{
			text:'出厂编号',dataIndex:'EEquip_FactoryOutNo',width:80,
			sortable:true,defaultRenderer:true
		},{
			text:'放置区域',dataIndex:'EEquip_StoreArea',width:100,
			sortable:true,defaultRenderer:true
		},{
			text:'启用日期',dataIndex:'EEquip_EnableDate',width:85,
			sortable:true,isDate:true,defaultRenderer:true
		},{
			text:'校准有效期',dataIndex:'EEquip_CalibrateDate',width:85,
			sortable:true,isDate:true,defaultRenderer:true
		},{
			text:'是否使用',dataIndex:'EEquip_IsUse',width:70,
			align: 'center',isBool: true,type: 'bool',sortable:true,defaultRenderer:true
		},{
			text:'备注',dataIndex:'EEquip_Comment',width:200,hidden:true,
			sortable:true,defaultRenderer:true
		});
		return columns;
	},
	/**打开查看表单*/
	openForm: function(id,formtype,NO) {
		var me = this;
		if(!NO){
			NO='1';
		}
		var config = {
			formtype:formtype,
			SUB_WIN_NO:NO,
			 maximizable: false, //是否带最大化功能
			listeners: {
				save: function(win) {
					me.onSearch();
					win.close();
				}
			}
		};
		if(id && id != null) {
			config.PK = id;
			
		}
		JShell.Win.open('Shell.class.qms.equip.Form', config).show();
	},
	changeDefaultWhere:function(){
		
	},
		  /**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			search = null,params = [];
			
		//改变默认条件
		me.changeDefaultWhere();
			
		me.internalWhere = '';
			
		if(buttonsToolbar){
			search = buttonsToolbar.getComponent('search').getValue();
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
	},
	onEditClick:function(){
		var  me =this;
		var	records = me.getSelectionModel().getSelection();
			
		if(records.length != 1){
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		me.openForm(records[0].get('EEquip_Id'),'edit','2');
	},
	/**表单方式提交*/
	onExportExcel:function(type,reportType){
		var me = this;
			
		me.UpdateForm = me.UpdateForm || Ext.create('Ext.form.Panel',{
			items:[
				{xtype:'filefield',name:'file'},
				{xtype:'textfield',name:'reportType',value:reportType},
				{xtype:'textfield',name:'idList'},
				{xtype:'textfield',name:'where'},
				{xtype:'textfield',name:'isHeader',value:"1"}
			]
		});
		//清空数据
		me.UpdateForm.getForm().setValues({idList:'',where:''});
		if(type == 1){//类型为勾选导出
			var records = me.getSelectionModel().getSelection(),
				len = records.length,
				ids = [];
				
			if (len == 0) {
				JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
				return;
			}
			
			for(var i=0;i<len;i++){
				ids.push(records[i].get(me.PKField));
				me.UpdateForm.getForm().setValues({idList:ids.join(",")});
			}
		}else if(type == 2){//类型为条件导出
			var where = me.getWhere();
			if(where.length==0){
				where='1=1';
			}else{
				where="("+where+")";
			}
			me.UpdateForm.getForm().setValues({where:where});
		}
	    me.showMask("数据请求中...");
		var url = JShell.System.Path.ROOT +me.reportExcelUrl;
		me.UpdateForm.getForm().submit({
			url:url,
            //waitMsg:JShell.Server.SAVE_TEXT,
            success:function (form,action) {
            	me.hideMask();
        		var fileName = action.result.ResultDataValue;
        		var downloadUrl = JShell.System.Path.ROOT + me.downLoadExcelUrl;
				downloadUrl += '?isUpLoadFile=1&operateType=0&downFileName=仪器信息数据&fileName=' + fileName.split('\/')[2];
				downloadUrl = encodeURI(downloadUrl);
				window.open(downloadUrl);
            },
            failure:function(form,action){
            	me.hideMask();
				JShell.Msg.error(action.result.ErrorInfo);
			}
		});
	},
	/**获取带查询参数的URL*/
	getWhere: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar');
		var  search = buttonsToolbar.getComponent('search').getValue();
		return me.internalWhere;
	}
	
});