/**
 * @class Shell.ux.form.uploadPanel.FileUpLoader
 * @extends Ext.util.Observable
 * @description XMLHttpRequest对象Ajax方式上传文件
 * @author longfc
 */
Ext.define("Shell.ux.form.uploadPanel.FileUpLoader", {
	mixins: {
		observable: 'Ext.util.Observable'
	},
	PKField: "",
	/**必须传--关系表的主键列(外部调用必须传入)*/
	fkObjectId: '',
	/**必须传--新增文件所保存的数据对象名称*/
	objectEName: "",
	/**必须传--外键字段(如:任务表--'PTask',工作任务日志表:'PWorkTaskLog',抄送关系表:'PTaskCopyFor')*/
	fkObjectName: '',
	/**提交方式*/
	method: 'POST',
	/*XMLHttpRequest对象*/
	xhr: null,
	addUrl: '/ProjectProgressMonitorManageService.svc/WM_UploadNewFiles',
	uploadParams: null,
	/*公共附件分类保存文件夹名称*/
	SaveCategory: "",
	formIndex: 0,
	constructor: function(config) {
		this.mixins.observable.constructor.call(this, config);
		this.addEvents({
			uploadstart: true,
			uploadfailure: true,
			uploadsuccess: true,
			uploadprogress: true
		});
	},
	/**
	 * 初始化XMLHttpRequest
	 */
	initConnection: function() {
		var xmlhttp = false;
		var xhr = null;
		try {
			xhr = new XMLHttpRequest(); //除 IE 外的浏览器都支持这个方法
		} catch(e) {
			try {
				xhr = ActiveXobject("Msxml12.XMLHTTP"); //新版本的 IE
			} catch(e) {
				try {
					xhr = ActiveXobject("Microsoft.XMLHTTP"); //老版本的 IE
				} catch(failed) {
					xmlhttp = false; //如果失败了还保持false
				}
			}
		}
		if(xhr == null) {
			alert("你的浏览器不支持XMLHttpRequest,不能上传附件!");
		}
		var method = this.method,
			url = this.addUrl;
		xhr.open(method, url, true);
		return xhr;
	},
	/**
	 * 取消你的浏览器不支持XMLHttpRequest
	 * @param fileItem
	 */
	abortXhr: function() {
		var me = this;
		me.suspendEvents();
		if(me.xhr != null) {
			me.xhr.abort();
		}
		me.resumeEvents();
	},
	/**
	 * 上传单个附件文件
	 * @param record
	 */
	uploadFile: function(record) {
		var me = this;
		var id = "" + record.data[me.PKField];
		if(id && id == "-1") {
			//Ext.isIE == false是IE10及以上版本和其他浏览器
			var formData = me.isIE10Less() ? me.createFormIE(record) : me.createFormChrome(record);
			if(me.isIE10Less() == false && formData != undefined) {
				me.xhr = me.initConnection();
				me.xhr.upload.addEventListener('loadstart', Ext.bind(me.onLoadstart, me, [record], true), true);
				me.xhr.upload.addEventListener('progress', Ext.bind(me.onUploadProgress, me, [record], true), true);
				me.xhr.addEventListener('loadend', Ext.bind(me.onLoadEnd, me, [record], true), true);
				me.xhr.send(formData);
			}
		}
	},
	/**
	 * IE10以下版本上传文件
	 * @param record
	 */
	createFormIE: function(record) {
		var me = this;
		var items = [];
		var id = "" + record.data[me.PKField];
		var newFileName = me.objectEName + "_NewFileName";
		var fileSize = record.get(me.objectEName + "_FileSize");
		if(fileSize == "待计算")
			fileSize = "";
		var oldId = "Old_" + me.PKField;
		items = [{
			xtype: 'textfield',
			name: 'FKObjectId',
			colspan: 1,
			value: me.fkObjectId
		}, {
			xtype: 'textfield',
			name: 'ObjectEName',
			colspan: 1,
			value: me.objectEName
		}, {
			xtype: 'textfield',
			name: 'FKObjectName',
			colspan: 1,
			value: me.fkObjectName
		}, {
			xtype: 'textfield',
			name: 'NewFileName',
			colspan: 1,
			value: record.data[newFileName]
		}, {
			xtype: 'textfield',
			name: 'FileSize',
			colspan: 1,
			value: fileSize
		}, {
			xtype: 'textfield',
			name: 'FileName',
			colspan: 1,
			value: record.get(me.objectEName + "_FileName")
		}, {
			xtype: 'textfield',
			name: 'SaveCategory',
			colspan: 1,
			value: me.SaveCategory
		}, {
			xtype: 'textfield',
			colspan: 1,
			name: 'BusinessModuleCode',
			value: record.get(me.objectEName + "_BusinessModuleCode")
		}, {
			xtype: 'textfield',
			colspan: 1,
			name: 'OldObjectId',
			value: record.get(oldId)
		}];
		items.push(record.data.file);
		var uploadForm = Ext.create('Ext.form.Panel', {
			hidden: true,
			layout: {
				type: 'table',
				columns: 1
			},
			width: 100,
			height: 10,
			itemId: "uploadForm-" + me.formIndex,
			items: items
		});
		record.set('status', '-2'); //正在上传
		//record.commit();
		me.fireEvent('uploadstart', null, record);
		uploadForm.getForm().submit({
			url: me.addUrl,
			method: "POST",
			success: function(form, action) {
				var data = action.result;
				if(data.success) {
					me.updateRecordOfsuccess(data, record);
					Ext.destroy(uploadForm);
					me.fireEvent('uploadsuccess', null, record);
				}
			},
			failure: function(form, action) {
				var data = action.result;
				Ext.destroy(uploadForm);
				me.fireEvent('uploadfailure', null, data, record);
			}
		});
	},
	updateRecordOfsuccess: function(data, record) {
		var me = this;
		data.value = Ext.JSON.decode(data.ResultDataValue);
		if(data.value) {
			record.set('status', '-5');
			if(data.value.fileSize) {
				var fileSize = record.get(me.objectEName + "_FileSize");
				var size = "";
				if(data.value.fileSize != "") {
					size = "" + JcallShell.Bytes.toSize(data.value.fileSize);
					record.set(fileSize, size);
					var fileName = me.objectEName + '_FileName';
					var fileNameValue = record.get(fileName) + (size.length > 0 ? "(" + size + ")" : "");
					record.set(fileName, fileNameValue);
				}

			}
			var id = "" + data.value.id;
			if(id && id != "") {
				record.set(me.PKField, id);
				record.set("Old_" + me.PKField, id);
			}
			record.commit();
		}
	},
	createFormChrome: function(record) {
		var me = this;
		var id = "" + record.data[me.PKField];
		var newFileName = me.objectEName + "_NewFileName";
		var fileSize = record.get(me.objectEName + "_FileSize");
		if(fileSize == "待计算")
			fileSize = "";
		var formData = new FormData();
		//其他参数必须在文件的前面
		formData.append("FKObjectId", me.fkObjectId);
		formData.append("ObjectEName", me.objectEName);
		formData.append("FKObjectName", me.fkObjectName);
		formData.append("NewFileName", record.data[newFileName]);
		formData.append("FileSize", fileSize);
		formData.append("FileName", record.get(me.objectEName + "_FileName"));
		//公共附件分类保存的文件夹名称
		formData.append("SaveCategory", me.SaveCategory);
		formData.append("BusinessModuleCode", record.get(me.objectEName + "_BusinessModuleCode"));
		var oldId = "Old_" + me.PKField;
		//附件的原主键Id,新增修订文件用
		formData.append("OldObjectId", record.get(oldId));
		formData.append('file', record.data.file);
		return formData;
	},
	/**
	 * 暂停上传
	 */
	abortUpload: function() {
		var me = this;
		me.abortXhr();
	},
	/**
	 * 开始上传
	 */
	onLoadstart: function(event, record) {
		var me = this;
		me.fireEvent('uploadstart', event, record);
	},
	/**
	 * 上传中 实时进度
	 */
	onUploadProgress: function(event, record) {
		var me = this;
		var percentage = Math.round((event.loaded * 100) / event.total);
		me.fireEvent('uploadprogress', event, record, percentage);
	},

	/**
	 * 完成上传 失败或成功
	 */
	onLoadEnd: function(event, record) {
		var me = this;
		var request = event.target;
		if(request.status != 200) {
			me.abortXhr();
			me.fireEvent('uploadfailure', event, request.response, record);
		} else {
			var data = Ext.JSON.decode(request.response);
			if(data.success) {
				me.updateRecordOfsuccess(data, record);
				me.fireEvent('uploadsuccess', event, record);
			} else {
				me.errorMessage = data.message;
				me.abortXhr();
				me.fireEvent('uploadfailure', event, data.message, record);
			}
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