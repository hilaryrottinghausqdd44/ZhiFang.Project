/*
 * 上传表单
 * @author longfc
 * @version 2016-11-1
 */
Ext.define('Shell.test.testApp', {
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
	title: '上传',
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
	PK: '5140692457370078869',

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
		me.createFile2("22");
	},
	/*程序附件**/
	createFile: function(fieldLabel) {
		var me = this;
		me.File = {
			fieldLabel: fieldLabel,
			name: 'file',
			emptyText: '',
			allowBlank: false,
			itemId: 'File',
			xtype: 'filefield',
			buttonText: '选择',
			listeners: {
				change: {
					element: 'el',
					fn: function(com, value, eOpts) {}
				}
			}
		};
	},
	/*程序附件**/
	createFile2: function(fieldLabel) {
		var me = this;
		me.File2 = {
			fieldLabel: fieldLabel,
			name: 'file2',
			emptyText: '',
			allowBlank: false,
			itemId: 'File2',
			xtype: 'filefield',
			buttonText: '选择2',
			listeners: {
				change: {
					element: 'el',
					fn: function(com, value, eOpts) {}
				}
			}
		};
	},
	/**@overwrite 获取列表布局组件内容*/
	getAddFFileTableLayoutItems: function() {
		var me = this,
			items = [];
		me.File.colspan = 1;
		me.File.width = me.defaults.width * me.File.colspan;
		items.push(me.File);

		me.File2.colspan = 1;
		me.File2.width = me.defaults.width * me.File2.colspan;
		items.push(me.File2);

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
			value: "5140692457370078869",
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
		me.submitOfIE();
		//me.createFormIE();
		//me.submit2();
	},
	submitOfIE: function() {
		var me = this;
		var file=me.getComponent("File");
		var filenew = {
			xtype: 'filefield',
			fieldLabel: 'file',
			name: 'file'
		};
		filenew = Ext.Object.merge(file, filenew);
		filenew.itemId = "filenew";
		filenew.id = "filenewId";
		var uploadForm = Ext.create('Ext.form.Panel', {
			//hidden: true,
			//frame: true,
			items: [file]
		});
		var url = (me.uploadUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.uploadUrl;
		uploadForm.getForm().submit({
			url: url,
			method:"POST",
			//waitMsg: "程序信息保存处理中,请稍候...",
			success: function(form, action) {
				var data = action.result;
				if(data.success) {
					me.hideMask();
					me.fireEvent('save', me);
					//JShell.Msg.alert("上传成功!", null, 800);
				}
			},
			failure: function(form, action) {
				var data = action.result;
				me.hideMask();
				me.fireEvent('failure', me);
				//JShell.Msg.error('上传失败！<br />' + data.ErrorInfo);
			}
		});

	},
	
	submit2: function() {
		var me = this;
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
	},
	createFormIE: function() {
		var me = this;
		var url = (me.uploadUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.uploadUrl;
		var formData = document.createElement("form");
		formData.id = "form1";
		formData.name = "form1";
		// 添加到 body 中  
		document.body.appendChild(formData);

		var input = document.createElement("input");
		// 设置相应参数  
		input.type = "text";
		input.name = "EmpId";
		input.value = "5140692457370078869";

		// 将该输入框插入到 form 中  
		formData.appendChild(input);

		var input2 = document.createElement("input");
		// 设置相应参数  
		input2.type = "text";
		input2.name = "objectEName";
		input2.value = "objectEName";
		// 将该输入框插入到 form 中  
		formData.appendChild(input2);

		formData.appendChild(me.File);
		//formData.appendChild(me.File2);
		formData.method = "POST";
		// form 提交路径  
		formData.action = url;
		// 对该 form 执行提交  
		formData.submit();
		// 删除该 form  
		document.body.removeChild(formData);
		//return formData;
	}
});