/**
 * 附件列表
 * @author liangyl
 * @version 2016-08-12
 */
Ext.define('Shell.class.qms.equip.templet.ereportdata.AttachmentGrid', {
	extend: 'Shell.class.qms.equip.templet.attachment.Grid',
	title: '附件列表',
	/**获取数据服务路径*/
	selectUrl: '/QMSReport.svc/ST_UDTO_SearchEAttachmentByHQL?isPlanish=true',
	/**默认排序字段*/
	defaultOrderBy: [{
		property: 'EAttachment_FileName',
		direction: 'ASC'
	}, {
		property: 'EAttachment_FileExt',
		direction: 'ASC'
	}],
	/**默认加载数据*/
	defaultLoad: true,
	/**默认选中数据*/
	autoSelect: true,
	hasRefresh: true,
	PK: null,
    startDate:'',
	endDate:'',
	TempletID: null,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			PClient = null,
			search = null,
			params = [];	    
		if(me.startDate && me.endDate) {
			params.push("eattachment.FileUploadDate>='"+me.startDate +"' and eattachment.FileUploadDate<'" +me.endDate+"'");
		}
		if(me.TempletID) {
			params.push("eattachment.ETemplet.Id=" + me.TempletID);
		}
		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
		return me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this,
			columns = me.callParent(arguments);
		columns.splice(1, 0, {
			text: '上传日期',
			dataIndex: 'EAttachment_FileUploadDate',
			width: 80,isDate:true,
			sortable: false,
			defaultRenderer: true
		});
		return columns;
	}
});