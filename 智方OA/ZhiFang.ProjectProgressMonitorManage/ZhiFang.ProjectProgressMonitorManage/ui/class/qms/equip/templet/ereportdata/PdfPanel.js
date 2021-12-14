/**
 * 查看附件功能
 * @author liangyl
 * @version 2015-07-02
 */
Ext.define('Shell.class.qms.equip.templet.ereportdata.PdfPanel', {
	extend: 'Shell.ux.panel.AppPanel',
	title: '查看模板附件',
	/**必须传--下载文件服务*/
	downLoadUrl: "/QMSReport.svc/QMS_UDTO_PreviewTempletAttachment",

	layout: {
		type: 'border',
		regionWeights: {
			south: 1,
			center: 1
		}
	},
	operateType: '1',
	TempletID: '',
	PK: '',
	beginDate: '',
	endDate: '',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.AttachmentGrid.on({
			itemclick: function(v, record) {
				JShell.Action.delay(function() {
					var Id = record.get('EAttachment_Id');
					if(Id) {
						me.getPdf(Id);
					}
				}, null, 500);
			},
			select: function(RowModel, record) {
				JShell.Action.delay(function() {
					var Id = record.get('EAttachment_Id');
					if(Id) {
						me.getPdf(Id);
					}
				}, null, 500);
			}
		});
	},
	//查看pdf
	getPdf: function(eattachmentID) {
		var me = this;
		var url = JShell.System.Path.ROOT + me.downLoadUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'eattachmentID=' + eattachmentID + '&operateType=1';
		me.showPdf(url);
	},
	//查看pdf
	showPdf: function(url, isClear) {
		var me = this;
		var a = '%22';
		var panel = me.CenterPanel;
		var html = '<div style=%22text-align:center;font-weight:bold;color:red;margin:10px;' + a + '>文件还未获取</div>';
		if(url) {
			html =
				'<iframe style=height:100%;width:100%;' +
				'position:absolute;top:0px;left:0px;right:0px;bottom:0px' +
				' src=' + url + '></iframe>';
		}
		if(isClear == true) html = '';
		panel.update(html);
	},
	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.AttachmentGrid = Ext.create('Shell.class.qms.equip.templet.ereportdata.AttachmentGrid', {
			region: 'west',
			header: false,
			formtype: 'show',
			width: 295,
			title: '附件列表',
		    split: true,
			collapsible: true,
			collapseMode:'mini',
			startDate: me.beginDate,
			endDate: me.endDate,
			TempletID: me.TempletID,
			itemId: 'AttachmentGrid',
			name: 'AttachmentGrid'
		});
		me.CenterPanel = Ext.create('Ext.panel.Panel', {
			region: 'center',
			header: false,
			title: 'pdf',
			itemId: 'CenterPanel',
			name: 'CenterPanel'
		});
		return [me.AttachmentGrid, me.CenterPanel];
	}
});