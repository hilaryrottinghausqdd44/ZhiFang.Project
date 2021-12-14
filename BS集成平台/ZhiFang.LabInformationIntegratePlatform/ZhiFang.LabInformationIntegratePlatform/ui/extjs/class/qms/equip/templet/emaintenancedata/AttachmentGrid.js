/**
 * 附件列表
 * @author liangyl
 * @version 2016-08-24
 */
Ext.define('Shell.class.qms.equip.templet.emaintenancedata.AttachmentGrid', {
	extend: 'Shell.class.qms.equip.templet.attachment.Grid',
	title: '附件列表',
	PK: null,
	TempletID: null,
	/**月保养*/
	TempletType: '',
	/**月保养编码*/
	TempletTypeCode: '',
	downLoadUrl: "/QMSReport.svc/QMS_UDTO_PreviewTempletAttachment",
    /**默认加载数据*/
	defaultLoad: false,
		/**默认每页数量*/
	defaultPageSize: 150,
	/**带分页栏*/
	hasPagingtoolbar: false,
	/**开始时间*/
	startDate: null,
	/**结束时间*/
	endDate: null,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.addEvents('uploadClick');
		me.buttonToolbarItems = [{
			xtype: 'button',
			text: '上传',
			name: 'uploadbtn',
			itemId: 'uploadbtn',
			iconCls: 'button-up',
			handler: function(but, e) {
				me.fireEvent('uploadClick', me);
//				me.showForm();
			}
		}];
		//创建数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this,
			columns = me.callParent(arguments);
		columns.splice(0, 0, {
			text: '上传日期',
			dataIndex: 'EAttachment_FileUploadDate',
			width: 80,isDate:true,
			sortable: false,
			defaultRenderer: true
		});
		columns.push({
			text: '大小',
			dataIndex: 'EAttachment_FileSize',
			width: 80,
			sortable: false,
			defaultRenderer: true
		}, {
			text: '文件格式',
			dataIndex: 'EAttachment_FileType',
			width: 100,
			sortable: false,
			defaultRenderer: true
		}, {
			text: '年月',
			dataIndex: 'EAttachment_FileUploadDate',
			width: 80,
			sortable: false,
			defaultRenderer: true,
			hidden: true
		}, {
			text: '是否使用',
			dataIndex: 'EAttachment_IsUse',
			width: 60,
			align: 'center',
			isBool: true,
			type: 'bool',
			sortable: false,
			defaultRenderer: true
		},{
			xtype: 'actioncolumn',
			text: '删除',
			align: 'center',
			tooltip: '删除',
			width: 60,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					return 'button-del hand';
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var id = rec.get('EAttachment_Id');
					me.updateOneByIsUse(rowIndex,id,0);
				}
			}]
		}, {
			xtype: 'actioncolumn',
			text: '查看',
			align: 'center',
			tooltip: '查看',
			width: 60,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					return 'button-show hand';
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var id = rec.get('EAttachment_Id');
					me.onDwonload(id);
				}
			}]
		});
		return columns;
	},
	/**显示附件信息*/
	showForm: function(FileUploadDate) {
		var me = this;
		JShell.Win.open('Shell.class.qms.equip.templet.attachment.Form', {
			formtype: "add",
			height: 120,
			TempletID: me.PK,
			/**月保养*/
			TempletType: me.TempletType,
			/**月保养编码*/
			TempletTypeCode: me.TempletTypeCode,
			FileUploadDate:FileUploadDate,
			width: 350,
			SUB_WIN_NO:'1',
			listeners: {
				save: function(p) {
					me.onSearch();
					p.close();
				}
			}
		}).show();
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			PClient = null,
			search = null,
			params = [];
//		var Sysdate = JcallShell.System.Date.getDate();
//		var Date = JcallShell.Date.toString(Sysdate, true);
//		var b = Date.split("-");
//		var beginDate = b[0] + '-' + b[1] + '-01 00:00:00';
//		var endDate = JcallShell.Date.getMonthLastDate(b[0], b[1], true);
//		var endDate2=endDate+' 23:59:59';
		if(me.startDate && me.endDate) {
			params.push("eattachment.FileUploadDate >='" + me.startDate + "' and eattachment.FileUploadDate <'" + me.endDate + "'");
		}
		if(me.PK) {
			params.push("eattachment.ETemplet.Id=" + me.PK);
		}
		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
		return me.callParent(arguments);
	},
	/**点击下载文件*/
	onDwonload: function(id) {
		var me = this;
		var url = JShell.System.Path.ROOT + me.downLoadUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'eattachmentID=' + id + '&operateType=1';
		window.open(url);
	},
	/**逻辑删除*/
	updateOneByIsUse: function(index, id, IsUse) {
		var me = this;
		var url = "/QMSReport.svc/ST_UDTO_UpdateEAttachmentByField";
		url = (url.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + url;
		var params = {
			entity: {
				Id: id,
				IsUse: IsUse
			},
			fields: 'Id,IsUse'
		};
		setTimeout(function() {
			JShell.Server.post(url, Ext.JSON.encode(params), function(data) {
				var record = me.store.findRecord('EAttachment_Id', id);
				if(data.success) {
					if(record) {
						record.set(me.DelField, true);
						record.commit();
						me.onSearch();
					}
				} else {
					if(record) {
						record.set(me.DelField, false);
						record.commit();
						me.onSearch();
					}
				}
			});
		}, 100 * index);
	}
});