/***
 * @version 2016-06-20
 *@description 多文件上传组件
 *@description 公开的参数：
 * 2016=10-24:添加了BusinessModuleCode的公开配置项
 *1、objectEName:对象名
 *2、fkObjectName:外键字段
 *3、fkObjectId:外键值
 *4、hideDelColumn:是否启用删除按钮（默认true）
 *5、hideShowColumn:是否启用查看按钮（默认false）
 *6、创建者用户ID（当禁用删除按钮=false时，用来与附件创建者比对，相同时显示删除按钮）
 *7、hiddenUpButton:隐藏上传按钮
 *8、hiddenStopeUploadButton:隐藏停止上传按钮
 *9、setValues(obj):供外部联动更新参数
 *  值;obj:{fkObjectId:'',objectEName:'',fkObjectName:'':operateType:'0'}
 *10、load():重新加载列表数据
 *11、 save():上传文件
 *12.hiddenDockedItems:隐藏停工具栏按钮
 *13.showMsgBox: 上传文件时是否弹出上传确认框, 默认为true
 * 2016-11-25:添加附件上传时的文件格式验证
 */
Ext.define('Shell.ux.form.uploadPanel.UploadContainer', {
	extend: 'Ext.panel.Panel',
	xtype: 'uploadContainer',
	requires: [
		'Shell.ux.form.uploadPanel.FileUpLoader',
		'Shell.ux.form.uploadPanel.FileSelectButton',
		'Shell.ux.form.uploadPanel.FileItemGrid'
	],
	layout: {
		type: 'fit'
	},
	width: 560,
	height: 180,
	formtype: "add",
	/**隐藏自定义名称列*/
	hiddenNewFileNameColumn: false,
	/**附件保存的表的数据对主键名*/
	PKField: "Id",
	/**必须传--关系表的主键列(外部调用必须传入)*/
	fkObjectId: '',
	/**必须传--新增附件的数据对象名称*/
	objectEName: "",
	/*附件列表类型(ObjId:按附件实体的业务对象名称处理;ObjName:按附件外键属性名称处理*/
	SearchType: "ObjName",
	/*公共附件分类保存文件夹名称*/
	SaveCategory: "",
	/**必须传--附件实体的业务对象名称或外键属性名称*/
	fkObjectName: '',
	/**必须传--获取数据服务路径*/
	selectUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPProjectAttachmentByHQL?isPlanish=true',
	/**必须传--删除文件(只更新是否使用为false,不删除数据库表数据,也不删除文件夹里的文件)服务路径*/
	delUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePProjectAttachmentByField',
	/**必须传--新增附件文件时保存服务*/
	addUrl: '/ProjectProgressMonitorManageService.svc/WM_UploadNewFiles',
	/**必须传--下载文件服务*/
	downLoadUrl: "/ProjectProgressMonitorManageService.svc/WM_UDTO_PProjectAttachmentDownLoadFiles",
	/**必须传--更新附件文件时保存服务*/
	editUrl: '/ProjectProgressMonitorManageService.svc/QMS_UDTO_UpdateFFileAttachmentByField',
	/**默认数据条件(只能查询是否启用为true的数据)*/
	defaultWhere: '',
	/**内部数据条件*/
	internalWhere: '',
	/**外部数据条件*/
	externalWhere: '',
	operateType: '0',
	/**附件的状态*/
	StatusField: 'status',

	delCount: 0,
	/**默认加载数据*/
	defaultLoad: false,
	/**是否启用序号列*/
	hasRownumberer: true,
	/**隐藏删除列*/
	hideDelColumn: false,
	/**隐藏查看附件列*/
	hideShowColumn: true,
	/**隐藏进度条列*/
	hideProgressColumn: false,
	hideStatusColumn: false,
	/**隐藏上传按钮*/
	hiddenUpButton: true,
	/**隐藏停止上传按钮*/
	hiddenStopeUploadButton: true,

	/**隐藏停工具栏按钮*/
	hiddenDockedItems: false,
	/**上传文件时是否弹出上传确认框*/
	showMsgBox: true,
	/**上传文件时是否弹出上传确认框*/
	showDeleteMsgBox: true,
	defaultPageSize: 100,
	/**后台排序*/
	remoteSort: true,
	fileItemGird: null,
	fileUploader: null,
	uploadParams: null,

	/**编辑文档类型(如新闻/通知/文档/修订文档)*/
	FTYPE: "",
	/**应用操作分类*/
	AppOperationType: "",
	uploadCount: 0,
	uploadLength: 0,
	/**上传处理失败的的总行数*/
	uploadErrorCount: 0,
	/**上传附件时是否显示上传进度条信息*/
	isShowProgress: true,
	/**上传附件结束时的提示消息信息*/
	progressMsg: '',
	/*业务模块代码*/
	BusinessModuleCode: "",
	/*附件能上传的文件格式,如.png;.jpg;*/
	fileExts: null,
	initConfig: function() {
		var me = this;
		switch(me.formtype) {
			case 'edit':
				me.hideDelColumn = false;
				me.hideShowColumn = false;
				me.hideProgressColumn = false;
				me.hiddenDockedItems = false;
				break;
			case 'show':
				me.hideDelColumn = true;
				me.hideShowColumn = false;
				me.hideProgressColumn = false;
				me.hideStatusColumn = true;
				me.hiddenDockedItems = true;
				break;
			default:
				break;
		}
		me.BusinessModuleCode = me.BusinessModuleCode || "";
		me.SearchType = me.SearchType || "ObjName";
		me.PKField = me.objectEName + "_Id";
		me.createModel();
		me.uploadParams = {
			"FKObjectId": me.fkObjectId,
			"FKObjectName": me.fkObjectName
		};
		if(me.defaultWhere == "" && me.objectEName != "" && me.objectEName != "" && me.fkObjectId != "" && me.fkObjectName != "") {
			me.getSearchDefaultWhere();
		}
		me.addUrl = (me.addUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.addUrl;
		me.editUrl = (me.editUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.editUrl;
	},
	createfileSelectButton: function() {
		var me = this;
		me.fileButtonIndex++;
		var fileSelectButton = Ext.create('Shell.ux.form.uploadPanel.FileSelectButton', {
			itemId: me.fileButtonIndex,
			name: 'field',
			listeners: {
				fileselected: function(file, fileList, newValue) {
					if(file != null)
						me.onFileSelection(file, fileList);
				}
			}
		});
		return fileSelectButton;
	},
	initComponent: function() {
		var me = this;
		me.initConfig();
		me.createfileItemGird();
		me.fileUploader = Ext.create('Shell.ux.form.uploadPanel.FileUpLoader', {
			PKField: me.PKField,
			uploadParams: me.uploadParams,
			objectEName: me.objectEName,
			fkObjectId: me.fkObjectId,
			BusinessModuleCode: me.BusinessModuleCode,
			fkObjectName: me.fkObjectName,
			SaveCategory: me.SaveCategory,
			addUrl: me.addUrl
		});
		me.initaddEvents();

		Ext.applyIf(me, {
			items: me.fileItemGird,
			dockedItems: me.createdockedItems()
		});
		me.callParent(arguments);
	},
	initaddEvents: function() {
		var me = this;
		me.fileUploader.on('uploadstart', me.handleUploadstart, me);
		me.fileUploader.on('uploadprogress', me.handleUploadProgress, me);
		me.fileUploader.on('uploadfailure', me.handleUploadfailure, me);
		me.fileUploader.on('uploadsuccess', me.handleUploadsuccess, me);

		me.addEvents('selectedfilerender', me);
		me.addEvents('beginupload', me);
		//单个开始上传
		me.addEvents('handleUploadstart', me);
		me.addEvents('handleUploadProgress', me);
		me.addEvents('handleUploadfailure', me);
		//单个上传成功
		me.addEvents('handleUploadsuccess', me);
		//所有附件上传成功
		me.addEvents('uploadcomplete', me);
		//所有的操作处理完
		me.addEvents('save', me);

	},
	createdockedItems: function() {
		var me = this;
		var dockedItems = [{
			xtype: 'toolbar',
			hidden: me.hiddenDockedItems,
			dock: 'top',
			itemId: 'tbardown',
			items: [
				me.createfileSelectButton(),
				{
					text: '上传',
					iconCls: 'button-up',
					itemId: 'btnBeginUpload',
					hidden: me.hiddenUpButton,
					handler: me.beginUpload,
					scope: me
				}, {
					text: '停止上传',
					iconCls: 'button-cancel2',
					itemId: 'btnStopeUpload',
					hidden: me.hiddenStopeUploadButton,
					handler: me.stopeUpload,
					scope: me
				}, {
					xtype: 'tbfill'
				}
			]
		}];
		return dockedItems;
	},
	createfileItemGird: function() {
		var me = this;
		me.fileItemGird = Ext.create("Shell.ux.form.uploadPanel.FileItemGrid", {
			selectUrl: me.selectUrl,
			uploadParams: me.uploadParams,
			PKField: me.PKField,
			fkObjectId: me.fkObjectId,
			objectEName: me.objectEName,
			fkObjectName: me.fkObjectName,
			defaultWhere: me.defaultWhere,
			internalWhere: me.internalWhere,
			externalWhere: me.externalWhere,
			defaultPageSize: me.defaultPageSize,
			remoteSort: me.remoteSort,
			defaultOrderBy: me.defaultOrderBy,
			defaultLoad: me.defaultLoad,
			hasRownumberer: me.hasRownumberer,
			hideDelColumn: me.hideDelColumn,
			hideShowColumn: me.hideShowColumn,
			hideProgressColumn: me.hideProgressColumn,
			hideStatusColumn: me.hideStatusColumn,
			operateType: me.operateType,
			downLoadUrl: me.downLoadUrl,
			showDeleteMsgBox: me.showDeleteMsgBox,
			hiddenNewFileNameColumn: me.hiddenNewFileNameColumn,
			delUrl: me.delUrl,
			editUrl: me.editUrl,
			downLoadUrl: me.downLoadUrl,
			FTYPE: me.FTYPE,
			BusinessModuleCode: me.BusinessModuleCode,
			/**应用操作分类*/
			AppOperationType: me.AppOperationType,
			plugins: Ext.create('Ext.grid.plugin.CellEditing', {
				clicksToEdit: 1
			}),
			border: false,
			listeners: {
				load: function(store, records, successful) {
					var visible = true;
					if(!records || records.length <= 0) {
						visible = false;
					} else {
						me.fileItemGird.getSelectionModel().select(0);
						visible = true;
					}
				},
				deleteaction: function(grid, record, rowIndex, colIndex) {
					var tbardown = me.getComponent("tbardown");
					//IE10以下版本
					if(record && me.isIE10Less())
						tbardown.remove(record.data.file);
				},
				deleteactionafter: function(grid, rowIndex, colIndex) {
					if(me.fileItemGird.getStore().count() > 0) {
						var len = me.fileItemGird.getStore().count() - 1;
						if(rowIndex > len) {
							rowIndex = len;
						} else {
							rowIndex = 0;
						}
						me.fileItemGird.getSelectionModel().select(rowIndex);
					}
				}
			}
		});
		me.fileItemGird.on('itemclick', me.handleItemclick, me);
	},
	/**清空数据,禁用功能按钮*/
	clearData: function() {
		var me = this;
		me.fileItemGird.store.removeAll(); //清空数据
	},
	/**
	 * 创建gridPanel的store所需的mdoel
	 **/
	createModel: function() {
		var me = this;
		var CreatorID = me.objectEName + '_CreatorID';
		var CreatorName = me.objectEName + '_CreatorName';
		var DataAddTime = me.objectEName + '_DataAddTime';
		var CreatorName = me.objectEName + '_CreatorName';
		var FileName = me.objectEName + '_FileName';
		var FileSize = me.objectEName + '_FileSize';
		var FileExt = me.objectEName + '_FileExt';

		var NewFileName = me.objectEName + '_NewFileName'; //文件名称(不带后缀名)
		var FileType = me.objectEName + '_FileType'; //文件内容类型
		var oldId = "Old_" + me.PKField;
		var BusinessModuleCode = me.objectEName + '_BusinessModuleCode';
		Ext.define('Shell.ux.form.uploadPanel.FileItem', {
			extend: 'Ext.data.Model',
			fields: ['file', {
				name: 'status',
				type: 'int'
			}, {
				name: 'progress',
				type: 'string'
			}, {
				name: me.PKField,
				type: 'string'
			}, {
				name: oldId,
				type: 'string'
			}, {
				name: CreatorID,
				type: 'string'
			}, {
				name: DataAddTime,
				type: 'string'
			}, {
				name: CreatorName,
				type: 'string'
			}, {
				name: FileName,
				type: 'string'
			}, {
				name: FileSize,
				type: 'string'
			}, {
				name: FileExt,
				type: 'string'
			}, {
				name: NewFileName,
				type: 'string'
			}, {
				name: FileType,
				type: 'string'
			}, {
				name: BusinessModuleCode,
				type: 'string'
			}]
		});
	},

	/**
	 * 还原初始化设置
	 */
	resetContent: function() {
		var me = this;
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = 0;

		me.uploadCount = 0;
		me.uploadLength = 0;
		me.uploadErrorCount = 0;
		me.fileItemGird.getStore().removeAll();
	},
	/**
	 * 行选择事件
	 */
	handleItemclick: function(grid, record, item, index, e, eOpts) {},
	/**
	 * 选择本地文件
	 * @param {Ext.form.field.File} file
	 * @param {JavaScript html5} FileList
	 */
	onFileSelection: function(file, fileList) {
		var me = this;
		var obj = {},
			fileExt = "",
			newFileName = "";
		//验证选择的附件中的文件格式是否通过
		var isValidator = true;
		if(me.fileExts != undefined && me.fileExts != null && me.fileExts != "") {
			me.fileExts = me.fileExts.toLowerCase();
			Ext.each(fileList, function(f) {
				fileExt = f.name.substring(f.name.toString().lastIndexOf("."), f.name.length).toLowerCase();
				if(isValidator == true && me.fileExts.indexOf(fileExt) < 0) {
					isValidator = false;
				}
			});
		}
		if(isValidator == true) {
			var Id = me.PKField;
			var oldId = "Old_" + me.PKField;
			var CreatorID = me.objectEName + '_CreatorID';
			var CreatorName = me.objectEName + '_CreatorName';
			var DataAddTime = me.objectEName + '_DataAddTime';
			var CreatorName = me.objectEName + '_CreatorName';
			var FileName = me.objectEName + '_FileName';
			var FileSize = me.objectEName + '_FileSize';
			var FileExt = me.objectEName + '_FileExt';
			var NewFileName = me.objectEName + '_NewFileName'; //文件名称(不带后缀名)
			var FileType = me.objectEName + '_FileType'; //文件内容类型
			var BusinessModuleCode = me.objectEName + "_BusinessModuleCode";

			Ext.each(fileList, function(f) {
				//me.isIE10Less() == false是IE10及以上版本和其他浏览器
				var size = 0;
				if(me.isIE10Less() && !f.files) {
					var fileSystem = null;
					try {
						/*
						 * ActiveXObject对象
						 *需要在IE里设置打开Internet Explorer“工具”-->“选项”-->“安全”-->“可信站点”
						 * -->“自定义级别”：“对没有标记为安全的activex控件进行初始化和脚本运行”设置成“启用”
						 * */
						fileSystem = new ActiveXObject("Scripting.FileSystemObject");
					} catch(ex) {
						fileSystem = null;
					}
					if(fileSystem != null) {
						var filePath = f.value;
						if(!fileSystem) {
							var fileIE = fileSystem.GetFile(filePath);
							size = fileIE.Size;
						} else {
							size = "";
						}
					} else {
						size = "";
					}
				} else {
					size = f.size;
				}
				var fname = "" + (me.isIE10Less() ? f.value : "" + f.name);
				if(fname != "" && fname.indexOf("\\") > -1)
					fname = fname.substring(fname.lastIndexOf("\\") + 1, fname.length);

				fileExt = fname.substring(fname.lastIndexOf("."), fname.length);
				newFileName = fname.substring(0, fname.lastIndexOf("."));
				obj = {};
				obj['file'] = f;
				obj['progress'] = 0;
				obj['status'] = -1; //'准备上传',
				obj[Id] = "-1"; //新增的上传文件
				obj[oldId] = "-1";
				obj[CreatorName] = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
				obj[CreatorID] = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
				obj[DataAddTime] = JShell.Date.toString(new Date().toDateString(), true);
				obj[FileName] = fname;
				obj[FileSize] = size;
				obj[FileExt] = fileExt;
				obj[NewFileName] = newFileName;
				obj[FileType] = f.type;
				obj[BusinessModuleCode] = me.BusinessModuleCode;
				var record = Ext.create('Shell.ux.form.uploadPanel.FileItem', obj);
				me.fileItemGird.getStore().add(record);
			});
			if(me.isIE10Less()) {
				//IE10以下版本,选择一个附件后,创建新的上传组件并添加到工具栏中,隐藏上一个的上传组件
				var fileSelectButton = me.createfileSelectButton();
				var tbardown = me.getComponent("tbardown");
				tbardown.insert(0, fileSelectButton);
				file.setVisible(false);
			}
			me.setButtonVisible('btnBeginUpload', !me.hiddenUpButton);
		} else {
			JShell.Msg.alert("选择的附件中,包含有不正确文件格式!只能上传<br>" + me.fileExts);
		}
		me.fireEvent('selectedfilerender');
	},
	fileButtonIndex: 0,
	/***
	 * 每次只循环列表一次,对外公开保存
	 */
	save: function() {
		var me = this;
		me.progressMsg = "";
		me.fireEvent('beginupload', me);
		me.setButtonVisible('btnStopeUpload', true);

		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = 0;

		me.uploadCount = 0;
		me.uploadLength = 0;
		me.uploadErrorCount = 0;

		me.resetRecordsStaus();

		//判断处理是否需要显示提示进度条
		var isShow = false;
		var addRecords = [],
			editRecords = [];
		var BusinessModuleCode = me.objectEName + '_BusinessModuleCode';

		//有没有新上传的附件或需要修改自定义名称的附件
		for(var i = 0; i < me.fileItemGird.getStore().getCount(); i++) {
			var record = me.fileItemGird.getStore().getAt(i);
			var id = "" + record.get(me.PKField);
			//新增上传文件
			if(id && id == "-1") {
				if(record.get(BusinessModuleCode) == "") {
					record.set(BusinessModuleCode, me.BusinessModuleCode);
					record.commit();
				}
				//新文件上传
				addRecords.push(record);
			} else if(id && id != "-1" && record.ditty == true) {
				editRecords.push(record);
			}
		}
		if(addRecords.length > 0 || editRecords.length > 0) isShow = true;
		if(isShow == false) me.isShowProgress = false;
		//me.isIE10Less()== false是IE10及以上版本和其他浏览器
		if(me.isShowProgress && isShow == true && me.isIE10Less() == false)
			Ext.Msg.progress('附件上传处理', '附件上传处理中,请稍候...');

		if(editRecords.length > 0) {
			me.saveLength = editRecords.length;
			for(var i = 0; i < editRecords.length; i++) {
				var newFileName = rec.get(me.objectEName + '_NewFileName');
				me.updateNewFileNameById(id, newFileName, rec);
			}
		}
		if(addRecords.length > 0) {
			me.uploadLength = addRecords.length;
			for(var i = 0; i < addRecords.length; i++) {
				me.uploadOneFile(i, addRecords[i]);
			}
		}
		if(addRecords.length == 0 && editRecords.length == 0) {
			var obj = {
				success: true,
				msg: "没有需要上传的附件!"
			}
			me.fireEvent('save', me, obj);
			me.fireEvent('uploadcomplete', null, me, obj);
		}
	},
	/**上传一条数据*/
	uploadOneFile: function(index, record) {
		var me = this;
		setTimeout(function() {
			me.fileUploader.fkObjectId = me.fkObjectId;
			me.fileUploader.formIndex = index;
			me.fileUploader.uploadFile(record);
		}, 100 * index);
	},
	/**
	 * 在上传之前,先将上次上传失败的记录行的状态还原
	 */
	resetRecordsStaus: function() {
		var me = this;
		for(var i = 0; i < me.fileItemGird.getStore().getCount(); i++) {
			var record = me.fileItemGird.getStore().getAt(i);
			var id = "" + record.get(me.PKField);
			if(id && id == "-1" && record.get("status") == "-3") {
				record.set('status', '-1');
				record.commit();
			}
		}
	},
	/**依文档附件ID的自定义名称编辑保存*/
	updateNewFileNameById: function(id, newFileName, record) {
		var me = this;
		var params = Ext.JSON.encode({
			entity: {
				Id: id,
				NewFileName: newFileName
			},
			fields: 'Id,NewFileName'
		});
		JShell.Server.post(me.editUrl, params, function(data) {
			if(data.success) {
				if(record) {
					record.commit();
				}
				me.saveCount++;
			} else {
				me.saveErrorCount++;
				if(record) {
					record.commit();
				}
			}
			if(me.saveCount + me.saveErrorCount == me.saveLength) {
				//me.isIE10Less() == false是IE10及以上版本和其他浏览器
				if(me.isShowProgress && me.isIE10Less() == false) Ext.Msg.hide();
				if(me.uploadLength == 0) {
					var obj = {
						success: me.uploadFileErrorCount == 0 ? true : false,
						msg: me.progressMsg
					}
					me.fireEvent('save', me, obj);
					me.fireEvent('uploadcomplete', event, me, obj);
				}
			}
		}, false);
	},
	/**
	 * 开始上传
	 */
	beginUpload: function() {
		var me = this;
		me.setButtonVisible('btnBeginUpload', false);
		if(me.showMsgBox) {
			Ext.Msg.show({
				title: '提示信息',
				msg: '确定将文件上传至服务器端吗?',
				buttons: Ext.Msg.OKCANCEL,
				icon: Ext.Msg.QUESTION,
				fn: function(buttonId, buttonText) {
					if(buttonId === "ok") {
						me.save();
					}
				},
				scope: this
			});
		} else {
			me.save();
		}
	},
	/**
	 * 暂停上传
	 */
	stopeUpload: function() {
		var me = this;
		me.setButtonVisible('btnStopeUpload', false);
		me.setButtonVisible('btnBeginUpload', true);
		me.fileUploader.abortUpload();
		for(var i = 0; i < me.fileItemGird.getStore().getCount(); i++) {
			var record = me.fileItemGird.getStore().getAt(i);
			if(record.get("status") == "-2") {
				record.set('status', '-5');
				record.commit();
			}
		}
	},
	/**
	 * 设置按钮状态
	 */
	setButtonDisabled: function(itemId, disabled) {
		var me = this;
		var tbardown = me.getComponent("tbardown");
		if(tbardown && tbardown != null && tbardown != undefined) {
			var tempCom = tbardown.getComponent(itemId);
			if(tempCom && tempCom != null && tempCom != undefined)
				tempCom.setDisabled(disabled);
		}
	},
	/**
	 * 设置按钮状态
	 */
	setButtonVisible: function(itemId, visible) {
		var me = this;
		var tbardown = me.getComponent("tbardown");
		if(tbardown && tbardown != null && tbardown != undefined) {
			var tempCom = tbardown.getComponent(itemId);
			if(!tempCom) tempCom.setVisible(visible);
		}
	},
	/**
	 * 处理上传开始事件
	 * @param {Shell.ux.form.uploadPanel.FileItem} record
	 */
	handleUploadstart: function(event, record) {
		var me = this;
		me.fireEvent('handleUploadstart', event, record);
		me.setButtonVisible('btnBeginUpload', false);
		record.set('status', '-2'); //正在上传
		record.commit();
	},
	/**
	 * 处理上传实时进度
	 * @param {Number} percentage
	 * @param {Shell.ux.form.uploadPanel.FileItem} record
	 */
	handleUploadProgress: function(event, record, percentage) {
		var me = this;
		me.fireEvent('handleUploadProgress', event, record, percentage);
		record.set('progress', percentage);
		record.commit();
		if(me.isShowProgress) {
			bartext = percentage + "%";
			Ext.Msg.updateProgress(percentage / 10, '已完成:' + bartext);
		}
	},

	/**
	 * 处理上传失败
	 * @param {String} msg
	 * @param {Shell.ux.form.uploadPanel.FileItem} record
	 */
	handleUploadfailure: function(event, msg, record) {
		var me = this;
		var newFileName = me.objectEName + '_NewFileName';
		var fileName = record.get(newFileName);
		me.progressMsg = me.progressMsg + "【" + fileName + "】上传失败<br />";
		record.set('status', '-3'); //上传失败
		record.commit();
		me.uploadErrorCount = me.uploadErrorCount + 1;
		if(me.uploadCount + me.uploadErrorCount == me.uploadLength) {
			//me.isIE10Less() == false是IE10及以上版本和其他浏览器
			if(me.isShowProgress && me.isIE10Less() == false) Ext.Msg.hide();
			var obj = {
				success: me.uploadErrorCount == 0 ? true : false,
				msg: msg
			}
			me.fireEvent('save', me, obj);
			me.fireEvent('uploadcomplete', event, me, obj);
		}
		me.setButtonVisible('btnBeginUpload', true);
	},
	/**
	 * 上传成功
	 * @param {Shell.ux.form.uploadPanel.FileItem} record
	 */
	handleUploadsuccess: function(event, record) {
		var me = this;
		var newFileName = me.objectEName + '_NewFileName';
		var fileName = record.get(newFileName);
		me.progressMsg = me.progressMsg + "【" + fileName + "】上传成功<br />";
		record.set('status', '-4'); //上传成功
		record.set('progress', 100);
		record.commit();
		me.uploadCount++;
		me.fireEvent('handleUploadsuccess', event, record);
		if(me.uploadCount + me.uploadErrorCount == me.uploadLength) {
			var obj = {
				success: me.uploadErrorCount == 0 ? true : false,
				msg: me.progressMsg
			}
			//me.isIE10Less()== false是IE10及以上版本和其他浏览器
			if(me.isShowProgress && me.isIE10Less() == false) Ext.Msg.hide();
			me.setButtonVisible('btnBeginUpload', !me.hiddenUpButton);
			me.fireEvent('save', me, obj);
			me.fireEvent('uploadcomplete', event, me, obj);
		}
		me.setButtonVisible('btnStopeUpload', false);
	},
	/**
	 * 删除文件
	 */
	deleteFile: function() {
		var me = this;
		Ext.Msg.show({
			title: '提示信息',
			msg: '确定删除文件吗?',
			buttons: Ext.Msg.OKCANCEL,
			icon: Ext.Msg.QUESTION,
			fn: function(buttonId, buttonText) {
				if(buttonId === "ok") {
					var record = me.fileItemGird.getSelectRecord();
					if(record) {
						var id = record.get(me.PKField);
						if(id && id.toString() == "-1") {
							me.fileItemGird.getStore().remove(record);
						} else {
							me.fileItemGird.delOneById(record);
						}
						var visible = true;
						if(me.fileItemGird.getStore().count() > 0) {
							var rowIndex = 0;
							var len = me.fileItemGird.getStore().count() - 1;
							if(rowIndex > len) {
								rowIndex = len;
							} else {
								rowIndex = 0;
							}
							me.fileItemGird.getSelectionModel().select(rowIndex);
							visible = true;
						} else {
							visible = false;
						}
					} else {
						JShell.Msg.alert("请选择行后再操作!");
					}
				}
			},
			scope: me
		});
	},
	/**
	 * @description:外部联动更新参数值
	 * @param obj:{fkObjectId:'',objectEName:'',fkObjectName:''}
	 * */
	setValues: function(obj) {
		var me = this;
		if(obj.fkObjectId != "" && obj.fkObjectId != undefined) {
			me.fkObjectId = obj.fkObjectId;
			me.fileUploader.fkObjectId = obj.fkObjectId;
			me.fileItemGird.fkObjectId = obj.fkObjectId;
		}
		if(obj.objectEName != "" && obj.objectEName != undefined) {
			me.objectEName = obj.objectEName;
			me.fileUploader.objectEName = obj.objectEName;
			me.fileItemGird.objectEName = obj.objectEName;
		}
		if(obj.fkObjectName != "" && obj.fkObjectName != undefined) {
			me.fkObjectName = obj.fkObjectName;
			me.fileUploader.fkObjectName = obj.fkObjectName;
			me.fileItemGird.fkObjectName = obj.fkObjectName;
		}
		if(obj.operateType != "" && obj.operateType != undefined) {
			me.operateType = obj.operateType;
			me.fileItemGird.operateType = obj.operateType;
		}
	},
	/*
	 * 获取查询的默认条件字符串
	 */
	getSearchDefaultWhere: function() {
		var me = this;
		if(me.defaultWhere == "" && me.objectEName != "" && me.objectEName != "" && me.fkObjectId != "" && me.fkObjectName != "") {
			switch(me.SearchType) {
				case "ObjId":
					me.defaultWhere = me.objectEName.toLowerCase() + ".IsUse=1 and " + me.objectEName.toLowerCase() + "." + me.fkObjectName + "=" + me.fkObjectId;
					break;
				default:
					me.defaultWhere = me.objectEName.toLowerCase() + ".IsUse=1 and " + me.objectEName.toLowerCase() + "." + me.fkObjectName + ".Id=" + me.fkObjectId;
					break;
			}
		}
	},
	/**@public 根据where条件加载数据*/
	load: function(where, isPrivate, autoSelect) {
		var me = this;
		me.resetContent();
		me.fileItemGird.fkObjectId = me.fkObjectId;
		me.fileItemGird.selectUrl = me.selectUrl;
		me.fileItemGird.defaultWhere = me.defaultWhere;
		me.fileItemGird.load(where, isPrivate, autoSelect);
		me.setButtonVisible('btnStopeUpload', false);
		var tbardown = me.getComponent("tbardown");
		switch(me.formtype) {
			case 'edit':
				break;
			case 'show':
				if(tbardown) {
					tbardown.setVisible(false);
				}
				break;
			default:
				break;
		}
	},
	/**
	 * Ext JS 4.1
	 * 判断是否IE10以下浏览器
	 * */
	isIE10Less: function() {
		var isIE = false;
		if(Ext.isIE6 || Ext.isIE7 || Ext.isIE8 || Ext.isIE9) isIE = true;
		return isIE;
	}
});