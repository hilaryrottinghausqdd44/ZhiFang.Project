/**
 * 电子签名上传表单
 * @author longfc
 * @version 2016-11-1
 */
Ext.define('Shell.class.sysbase.user.empsign.Form', {
	extend: 'Shell.ux.form.Panel',
	bodyPadding: '5px 5px 5px 5px',
	layout: {
		type: 'table',
		columns: 1 //每行有几列
	},
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 75,
		width: 260,
		labelAlign: 'right'
	},
	formtype: "edit",
	title: '电子签名上传',
	width: 300,
	height: 160,
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: false,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**获取数据服务路径(编辑时不需要更新总阅读数)*/
	selectUrl: '/SystemCommonService.svc/SC_UDTO_DownLoadEmpSignByEmpId',
	/**上传服务地址*/
	uploadUrl: '/SystemCommonService.svc/SC_UDTO_UploadEmpSignByEmpId',
	/**文件下载服务路径*/
	downloadUrl: "/SystemCommonService.svc/PGM_UDTO_DownLoadEmpSignByEmpId",
	/**显示成功信息*/
	showSuccessInfo: false,

	autoScroll: true,
	/*员工Id*/
	PK: '',

	afterRender: function() {
		var me = this;
		me.addEvents('failure', 'load', 'beforesave', 'save');
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];
		me.buttonToolbarItems = ['->', 'save', 'reset'];
		me.createAddShowItems();
		items = items.concat(me.getAddFFileTableLayoutItems());
		//创建隐形组件
		items = items.concat(me.createHideItems());
		return items;
	},

	/**创建可见组件*/
	createAddShowItems: function() {
		var me = this;
		me.createFile("电子签名");
		me.createPreviewImage("预览");
	},
	/*程序附件**/
	createFile: function(fieldLabel) {
		var me = this;
		me.File = {
			fieldLabel: fieldLabel,
			name: 'file',
			emptyText: '只能上传png格式',
			allowBlank: false,
			itemId: 'File',
			xtype: 'filefield',
			buttonText: '选择',
			validator: function(value) {
				var arr = value.split('.');
				if(arr[arr.length - 1].toString().toLowerCase() != 'png') {
					return '文件格式不正确！只能上传.png';
				} else {
					return true;
				}
			},
			listeners: {
				change: {
					element: 'el',
					fn: function(com, value, eOpts) {}
				}
			}
		};
	},
	/*原附件信息**/
	createPreviewImage: function(fieldLabel) {
		var me = this;
		var src = JShell.System.Path.ROOT + '/Images/EmpSign/' + me.PK + ".png";
		me.PreviewImage = {
			xtype: 'image',
			fieldLabel: fieldLabel,
			src: src,
			height: 60,
			name: 'PreviewImage',
			itemId: 'PreviewImage'
		};
	},
	/**@overwrite 获取列表布局组件内容*/
	getAddFFileTableLayoutItems: function() {
		var me = this,
			items = [];
		me.File.colspan = 1;
		me.File.width = me.defaults.width * me.File.colspan;
		items.push(me.File);
		me.PreviewImage.colspan = 1;
		me.PreviewImage.width = me.defaults.width * me.PreviewImage.colspan;
		items.push(me.PreviewImage);
		//创建隐形组件
		items = items.concat(me.createHideItems());
		return items;
	},
	/**返回数据处理方法*/
	changeResult: function(data) {
		return data;
	},
	/**更改标题*/
	changeTitle: function() {
		//不做处理
	},
	/**创建隐形组件*/
	createHideItems: function() {
		var me = this,
			items = [];
		items.push({
			fieldLabel: '员工ID',
			hidden: true,
			value: me.PK,
			itemId: 'EmpId',
			name: 'EmpId'
		});
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this;
	},
	/**@overwrite 获取修改的数据*/
	getEditParams: function() {
		var me = this;
	},
	/**保存按钮点击处理方法*/
	onSaveClick: function() {
		var me = this;

		if(!me.getForm().isValid()) return;
		me.showMask(me.saveText); //显示遮罩层
		me.fireEvent('beforesave', me);
		var url = (me.uploadUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.uploadUrl;
		me.getForm().submit({
			url: url,
			//waitMsg: "程序信息保存处理中,请稍候...",
			success: function(form, action) {
				var data = action.result;
				if(data.success) {
					me.hideMask();
					me.fireEvent('save', me);
					JShell.Msg.alert("上传成功!", null, 800);
				}
			},
			failure: function(form, action) {
				var data = action.result;
				me.hideMask();
				me.fireEvent('failure', me);
				JShell.Msg.error('上传失败！<br />' + data.ErrorInfo);
			}
		});
	}
});