/**
 * 服务器授权申请明细
 * @author longfc	
 * @version 2016-12-20
 */
Ext.define("Shell.class.wfm.authorization.ahserver.file.SelectAndUploadButton", {
	extend: 'Ext.form.field.File',
	xtype: 'SelectAndUploadButton',

	buttonOnly: true,
	buttonText: '上传',
	buttonConfig: {
		iconCls: 'button-add'
	},
	uploadUrl: '/SingleTableService.svc/ST_UploadAHServerLicenceFile',
	/**客户名称*/
	PClientName: null,
	/**客户ID*/
	PClientID: null,
	/**授权编码*/
	LicenceCode: null,
	uploadFormItemId: 0,
	initComponent: function() {
		var me = this;
		me.addEvents('uploadSuccess');
		me.addEvents('uploadFailure');
		me.addEvents('fileselected');
		me.on('change', me.handlerChange, me);
		me.callParent(arguments);
	},
	handlerChange: function(file, newValue, oldValue, eOpts) {
		if(!newValue) return;
		var me = this;
		var fileupload = null;
		if(file && file.fileInputEl.dom.files) {
			if(file.fileInputEl.dom.files.length > 0)
				fileupload = file.fileInputEl.dom.files[0];
		} else {
			if(file.value != "" && file.value != undefined) {
				fileupload = file;
			}
		}
		if(fileupload == null) {
			JShell.Msg.alert("没有选择到上传授权申请文件!<br>", null, 1000);
		} else {
			if(!me.PClientID) {
				JShell.Msg.alert("请选择客户后再上传!", null, 1000);
			} else {
				//me.createFormIE(fileupload);
				me.isIE10Less() ? me.createFormIE(fileupload) : me.createFormChrome(fileupload);
			}
		}
		me.fireEvent('fileselected', file, [fileupload], newValue);
	},

	/**
	 * IE10以下版本上传文件
	 * @param record
	 */
	createFormIE: function(fileupload) {
		var me = this;
		me.uploadFormItemId++;
		var items = [];
		items = [{
			xtype: 'textfield',
			name: 'PClientID',
			colspan: 1,
			value: me.PClientID
		}, {
			xtype: 'textfield',
			name: 'LicenceCode',
			colspan: 1,
			value: me.LicenceCode
		}, {
			xtype: 'textfield',
			name: 'PClientName',
			colspan: 1,
			value: me.PClientName
		}];
		items.push(fileupload);
		var uploadForm = Ext.create('Ext.form.Panel', {
			hidden: true,
			layout: {
				type: 'table',
				columns: 1
			},
			width: 100,
			height: 10,
			itemId: "uploadForm-" + me.uploadFormItemId,
			items: items
		});
		var url = JShell.System.Path.getRootUrl(me.uploadUrl);
		uploadForm.getForm().submit({
			url: url,
			method: "POST",
			success: function(form, action) {
				var data = action.result;
				if(data.success) {
					var resultDataValue = Ext.JSON.decode(data.ResultDataValue);
					me.fireEvent('uploadSuccess', null, resultDataValue, fileupload, uploadForm);
					//Ext.destroy(uploadForm);
				}
			},
			failure: function(form, action) {
				var data = action.result;
				me.errorMessage = data.message;
				me.fireEvent('uploadFailure', null, data, fileupload, uploadForm);
				//Ext.destroy(uploadForm);
			}
		});
	},
	createFormChrome: function(fileupload) {
		var me = this;
		var formData = new FormData();
		formData.append('PClientID', me.PClientID);
		formData.append('LicenceCode', me.LicenceCode);
		formData.append('PClientName', me.PClientName);
		formData.append('file', fileupload);
		var xhr = me.initXMLHttpRequest();
		xhr.upload.addEventListener("loadstart", me.loadstart, false);
		xhr.upload.addEventListener("progress", me.onUploadProgress, false);
		xhr.addEventListener("error", me.uploadFailed, false);
		xhr.addEventListener("abort", me.abortXhr, false);
		xhr.onload = function(event) {
			me.onLoadEnd(me, this, event);
		}
		xhr.send(formData);
	},
	/**
	 * 请求失败
	 */
	uploadFailed: function(event, fileupload) {
		var request = event.target;
		this.fireEvent('uploadFailed', event, fileupload);
	},
	/**
	 * 上传中 实时进度
	 */
	onUploadProgress: function(event) {
		var percentage = Math.round((event.loaded * 100) / event.total);
		this.fireEvent('uploadprogress', event, percentage);
	},
	/**
	 * 完成上传 失败或成功
	 */
	onLoadEnd: function(me, request, event, fileupload) {
		if(request.status != 200) {
			me.abortXhr();
			me.fireEvent('uploadFailure', event, request.response, fileupload);
		} else {
			var data = Ext.JSON.decode(request.response);
			if(data.success) {
				var resultDataValue = Ext.JSON.decode(data.ResultDataValue);
				me.fireEvent('uploadSuccess', event, resultDataValue, fileupload);
			} else {
				me.abortXhr();
				me.errorMessage = data.message;
				me.fireEvent('uploadFailure', event, request.response, fileupload);
			}
		}
	},
	/**
	 * 初始化XMLHttpRequest
	 */
	initXMLHttpRequest: function() {
		var me = this;
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
		var url = JShell.System.Path.getRootUrl(me.uploadUrl);
		xhr.open('POST', url, false);
		me.abortXhr = function(xhr) {
			//me.suspendEvents();
			if(xhr != null) {
				xhr.abort();
			}
			//me.resumeEvents();
		};
		return xhr;
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