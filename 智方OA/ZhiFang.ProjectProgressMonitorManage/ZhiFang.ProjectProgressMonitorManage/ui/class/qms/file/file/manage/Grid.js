/**
 * 文档管理列表
 * @author longfc
 * @version 2016-09-26
 */
Ext.define('Shell.class.qms.file.file.manage.Grid', {
	extend: 'Shell.class.qms.file.manage.Grid',
	title: '文档管理',

	/**是否禁用按钮是否显示*/
	HiddenButtonLock: false,
	/**是否置顶按钮是否显示*/
	HiddenButtonDoTop: false,
	/**撤销置顶按钮是否显示*/
	HiddenButtonDoNoTop: false,
	/**文档状态默认为发布*/
	defaultStatusValue: "",
	isManageApp: true,
	/**物理删除*/
	hasDel: true,
	/**删除文档信息(物理删除)*/
	delPhysicalUrl: '/QMSService.svc/QMS_UDTO_DelFFileOfPhysicalById',

	initComponent: function() {
		var me = this;
		var dt = new Date();
		dt = Ext.Date.format(dt, 'Y-m-d');
		me.defaultWhere = me.defaultWhere || "(ffile.IsUse=1) and ((ffile.BeginTime is null and ffile.EndTime is null) or (ffile.BeginTime<='" + dt + "')  or (ffile.EndTime>='" + dt + "'))";
		me.callParent(arguments);
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	/**创建数据列*/
	createNewColumns: function() {
		var me = this;
		var columns = [];
		columns = me.initDefaultColumn(columns);

		columns.push(me.createEditBDictTreeColumn());
		columns.push(me.createEditColumn());
		//修改发布信息列
		columns.push(me.createPublisher());
		//是否有交流列
		columns.push(me.createInteraction());
		//是否有操作记录查看列
		columns.push(me.createOperation());
		//阅读记录列
		columns.push(me.createreadinglog());
		//物理删除
		if(me.hasDel) {
			columns.push(me.createDelColumn());
		}
		columns.push(me.createreFFileNo(), me.createreVersionNo());
		columns.push(me.createreKeyword());
		columns.push(me.createreIsUse());
		me.createOtherColumn(columns);
		if(me.hasDel) {
			columns.push({
				dataIndex: me.DelField,
				text: '',
				width: 40,
				hideable: false,
				sortable: false,
				menuDisabled: true,
				renderer: function(value, meta, record) {
					var v = '';
					if(value === 'true') {
						v = '<b style="color:green">' + JShell.All.SUCCESS_TEXT + '</b>';
					}
					if(value === 'false') {
						v = '<b style="color:red">' + JShell.All.FAILURE_TEXT + '</b>';
					}
					var msg = record.get('ErrorInfo');
					if(msg) {
						meta.tdAttr = 'data-qtip="<b style=\'color:red\'>' + msg + '</b>"';
					}

					return v;
				}
			});
		}
		return columns;
	},
	/*创建物理删除操作列**/
	createDelColumn: function() {
		var me = this;
		return {
			xtype: 'actioncolumn',
			text: '删除',
			align: 'center',
			width: 40,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			sortable: false,
			menuDisabled: true,
			items: [{
				getClass: function(v, meta, record) {
					//只有作废的文档才能物理删除
					var status = "" + record.get("FFile_Status");
					if(status == "7") {
						return 'button-del hand';
					} else {
						return "";
					}
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.onDelClick(rec);
				}
			}]
		};
	},
	/**打开编辑表单*/
	openFFileForm: function(record, fFileStatus, formtype) {
		var me = this;
		var Grid = me.getComponent('Grid');
		var id = "",
			OriginalFileID = "",
			BDictTreeId = me.BDictTreeId,
			BDictTreeCName = me.BDictTreeCName;
		if(record != null) {
			id = record.get('FFile_Id');
			BDictTreeId = record.get('FFile_BDictTree_Id');
			BDictTreeCName = record.get('FFile_BDictTree_CName');
			OriginalFileID = record.get('FFile_OriginalFileID');
			if(!OriginalFileID) {
				OriginalFileID = id;
			}
		}
		var maxWidth = document.body.clientWidth * 0.78;
		var height = document.body.clientHeight - 10;

		var config = {
			showSuccessInfo: false,
			SUB_WIN_NO: me.FTYPE.toString() + "1",
			height: height,
			width: maxWidth,
			hasReset: me.hasReset,
			title: "编辑文档",
			formtype: 'edit',
			BDictTreeId: BDictTreeId,
			BDictTreeCName: BDictTreeCName,
			fFileStatus: fFileStatus,
			FTYPE: me.FTYPE,
			IDS: me.IDS,
			/**获取树的最大层级数*/
			LEVEL: me.LEVEL,
			listeners: {
				save: function(win, e) {
					if(e.success) {
						me.onSearch();
						win.close();
					}
				}
			}
		};
		var form = 'Shell.class.qms.file.manage.EditTabPanel';
		if(id && id != null) {
			config.formtype = formtype || 'edit';
			config.PK = id;
			config.FFileId = id;
			title: "编辑文档";
			config.OriginalFileID = OriginalFileID;
		}
		JShell.Win.open(form, config).show(); //JShell.Win
	},
	/**初始化查询栏内容*/
	createSearchtoolbar: function() {
		var me = this;
		var items = me.callParent(arguments);
		return items;
	},
	/**删除按钮点击处理方法*/
	onDelClick: function(record) {
		var me = this;
		//只有作废的才能物理删除
		var status = "" + record.get("FFile_Status");
		if(status != "7") {
			JShell.Msg.error("只有作废的文档才能进行物理删除操作!");
			return;
		}
		JShell.Msg.confirm({
			title: '<div style="text-align:center;">文档删除操作</div>',
			msg: "<b style='color:red;'>文档物理删除后无法恢复，请确认是否继续删除？</b>",
			closable: false,
			multiline: false
		}, function(but, text) {
			if(but != "ok") return;

			me.delErrorCount = 0;
			me.delCount = 0;
			me.delLength = 1;

			me.showMask(me.delText); //显示遮罩层
			var id = record.get(me.PKField);
			me.delOneById(1, id);
		});
	},
	/**删除一条数据*/
	delOneById: function(index, id) {
		var me = this;
		var url = (me.delPhysicalUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.delPhysicalUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'id=' + id;

		setTimeout(function() {
			JShell.Server.get(url, function(data) {
				var record = me.store.findRecord(me.PKField, id);
				if (data.success) {
					if (record) {
						record.set(me.DelField, true);
						record.commit();
					}
					me.delCount++;
				} else {
					me.delErrorCount++;
					if (record) {
						record.set(me.DelField, false);
						record.set('ErrorInfo', data.msg);
						record.commit();
					}
				}
				if (me.delCount + me.delErrorCount == me.delLength) {
					me.hideMask(); //隐藏遮罩层
					if (me.delErrorCount == 0){
						me.onSearch();
					}else{
						JShell.Msg.error('存在失败信息，具体错误内容请查看数据行的失败提示！');
					}
				}
			});
		}, 100 * index);
	}
});