/**
 * 员工列表
 * @author liangyl	
 * @version 2018-11-09
 */
Ext.define('Shell.class.qms.equip.user.Grid', {
	extend: 'Shell.class.sysbase.user.Grid',
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
			listeners:{click:function(but) {me.onExportExcel(2,'1');}}
		}]);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			text: '员工姓名',
			dataIndex: 'HREmployee_CName',
			width: 100,
			sortable: true,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '手机号',
			dataIndex: 'HREmployee_MobileTel',
			width: 100,
			sortable: true,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '工号',
			dataIndex: 'HREmployee_UseCode',
			width: 100,
			sortable: true,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '隶属部门',
			dataIndex: 'HREmployee_HRDept_CName',
			width: 100,
			sortable: true,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			xtype: 'checkcolumn',
			text: '使用',
			dataIndex: 'HREmployee_IsUse',
			width: 40,
			align: 'center',
			sortable: true,
			menuDisabled: true,
			stopSelection: false,
			type: 'boolean'
		}, {
			text: '主键ID',
			dataIndex: 'HREmployee_Id',
			isKey: true,
			hidden: true,
			hideable: false
		}, {
			text: '时间戳',
			dataIndex: 'HREmployee_DataTimeStamp',
			hidden: true,
			hideable: false
		}];

		if(me.hasManager) {
			columns.push({
				text: '直接上级',
				dataIndex: 'HREmployee_ManagerName',
				width: 100,
				sortable: true,
				menuDisabled: true,
				defaultRenderer: true
			});
		}
		return columns;
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
				downloadUrl += '?isUpLoadFile=1&operateType=0&downFileName=员工信息数据&fileName=' + fileName.split('\/')[2];
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
		var me = this,params=[],
			buttonsToolbar = me.getComponent('buttonsToolbar');
		var  search = buttonsToolbar.getComponent('search').getValue();
		me.internalWhere="";
		
		//根据部门ID查询模式
		if(me.DeptTypeModel) {
			var strWhere ="";
			if(me.DeptId!='' || me.DeptId!=null){
				strWhere = 'id=' + me.DeptId+'^';
			}
			if(search) {
			    me.internalWhere = strWhere +"("+ me.getSearchWhere(search)+")";
			}
		} else {
			params.push('hremployee.HRDept.Id=' + me.DeptId);
			if(params.length > 0 && !me.DeptTypeModel) {
				me.internalWhere = params.join(' and ');
			} else {
				me.internalWhere = '';
			}
			if(search) {
				if(me.internalWhere && !me.DeptTypeModel) {
					me.internalWhere += ' and (' + me.getSearchWhere(search) + ')';
				}else {
					me.internalWhere = me.getSearchWhere(search);
				}
			}
		}
		return me.internalWhere;
	}
});