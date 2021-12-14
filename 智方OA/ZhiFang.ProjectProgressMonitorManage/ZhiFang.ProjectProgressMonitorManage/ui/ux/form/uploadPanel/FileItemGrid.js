/***
 * @description 多文件上传组件的gridPanel
 * @version 2016-06-20
 * @version 2017-12-22:修改排序方式由后台排序支持为前台排序
 */
Ext.define("Shell.ux.form.uploadPanel.FileItemGrid", {
	extend: 'Ext.grid.Panel',
	xtype: 'fileItemGrid',
	mixins: ['Shell.ux.Langage'],
	columnLines: true,
	autoScroll: true,
	/**附件保存的表的数据对主键名*/
	PKField: "Id",
	/**必须传--关系表的主键列(外部调用必须传入)*/
	fkObjectId: '',
	/**必须传--新增文件所保存的数据对象名称*/
	objectEName: "",
	/**必须传--外键字段(如:任务表--'PTask',工作任务日志表:'PWorkTaskLog',抄送关系表:'PTaskCopyFor')*/
	fkObjectName: '',
	/**获取数据服务路径*/
	selectUrl: '',
	/**默认数据条件*/
	defaultWhere: '',
	/**内部数据条件*/
	internalWhere: '',
	/**外部数据条件*/
	externalWhere: '',
	operateType: '0',
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
	/**隐藏状态列*/
	hideStatusColumn: false,
	/**隐藏自定义名称列*/
	hiddenNewFileNameColumn: false,
	showDeleteMsgBox: true,
	defaultPageSize: 200,
	/**是否后台排序*/
	remoteSort: false,
	SearchType: "fileItemGrid",
	/**必须传--删除文件(只更新是否使用为false,不删除数据库表数据,也不删除文件夹里的文件)服务路径*/
	delUrl: '',
	/**必须传--下载文件服务*/
	downLoadUrl: "",
	/**错误信息样式*/
	errorFormat: '<div style="color:red;text-align:center;margin:5px;font-weight:bold;">{msg}</div>',
	/**消息信息样式*/
	msgFormat: '<div style="color:blue;text-align:center;margin:5px;font-weight:bold;">{msg}</div>',
	/**错误信息*/
	errorInfo: null,
	/**编辑文档类型(如新闻/通知/文档/修订文档--附件需要清空原文档附件的主键id))*/
	FTYPE: '',
	/**应用操作分类*/
	AppOperationType: "",
	/*业务模块代码*/
	BusinessModuleCode: "",
	/**
	 * 排序字段
	 * @exception 
	 * [{property:'DContractPrice_ContractPrice',direction:'ASC'}]
	 */
	defaultOrderBy: [],
	BusinessModuleCodeList: [
		["FFile", "文档业务模块"],
		["PTask", "任务业务模块"],
		["PInvoice", "发票管理业务模块"],
		["BEquip", "仪器业务模块"]
	],
	initComponent: function() {
		var me = this;
		me.defaultLoad=me.defaultLoad||false;
		me.addEvents('changeResult', 'nodata', 'load', 'deleteaction', 'deleteactionafter');
		me.downLoadUrl = (me.downLoadUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.downLoadUrl;
		Ext.apply(me, {
			columns: me.createColumns(),
			store: me.createStore()
		});
		me.callParent();
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		if(me.defaultLoad)me.storeLoad();
	},
	/**创建数据集*/
	createStore: function() {
		var me = this;
		me.defaultOrderBy=me.defaultOrderBy||[];
		if(me.defaultOrderBy.length==0){
			var defaultStr=me.objectEName + '_DataAddTime';			
			me.defaultOrderBy.push({property:defaultStr,direction:'ASC'});
		}		
		return Ext.create('Ext.data.Store', {
			model: "Shell.ux.form.uploadPanel.FileItem",
			fields: me.getStoreFields(),
			pageSize: me.defaultPageSize,
			remoteSort: me.remoteSort,
			sorters: me.defaultOrderBy,
			proxy: {
				type: 'ajax',
				url:'',// me.selectUrl
				reader: {
					type: 'json',
					totalProperty: 'count',
					root: 'list'
				},
				extractResponseData: function(response) {
					var data = JShell.Server.toJson(response.responseText);
					if(data.success) {
						var info = data.value;
						if(info) {
							var type = Ext.typeOf(info);
							if(type == 'object') {
								info = info;
							} else if(type == 'array') {
								info.list = info;
								info.count = info.list.length;
							} else {
								info = {};
							}
							data.count = info.count || 0;
							data.list = info.list || [];
						} else {
							data.count = 0;
							data.list = [];
						}
						data = me.changeResult(data);
						me.fireEvent('changeResult', me, data);
					} else {
						me.errorInfo = data.msg;
					}
					response.responseText = Ext.JSON.encode(data);
					return response;
				}
			},
			listeners: {
				beforeload: function() {
					return me.onBeforeLoad();
				},
				load: function(store, records, successful) {
					me.onAfterLoad(records, successful);
				}
			}
		});
	},
	/**@overwrite 改变返回的数据*/
	changeResult: function(data) {
		var me = this;
		var len = data.list.length;
		var id = me.PKField;
		var oldId = "Old_" + me.PKField;
		for(var i = 0; i < len; i++) {
			data.list[i]["file"] = "";
			data.list[i][oldId] = data.list[i][id];
			//如果是新增修订文档--需要清空原文档附件的主键id)
			if(me.AppOperationType == JcallShell.QMS.Enum.AppOperationType.新增修订文档 && data.value != null) {
				data.list[i][id] = "-1";
				data.list[i]["progress"] = 0;
				data.list[i]["status"] = -1;
				me.fkObjectId = "";
			} else {
				data.list[i]["status"] = "-4"; //已上传
				data.list[i]["progress"] = 100;
			}
		}
		return data;
	},
	/**加载数据前*/
	onBeforeLoad: function() {
		var me = this;
		if(!me.defaultLoad) return false;
		me.getView().update();
		me.store.proxy.url = me.getLoadUrl(); //查询条件
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			arr = [];
		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.selectUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true).join(',');

		if(me.defaultWhere && me.defaultWhere != '') {
			arr.push(me.defaultWhere);
		}
		//内部条件
		if(me.internalWhere && me.internalWhere != '') {
			arr.push(me.internalWhere);
		}
		//外部条件
		if(me.externalWhere && me.externalWhere != '') {
			arr.push(me.externalWhere);
		}
		var where = arr.join(") and (");
		if(where) where = "(" + where + ")";
		if(where) {
			url += '&where=' + JShell.String.encode(where);
		}
		return url;
	},
	/**加载数据后*/
	onAfterLoad: function(records, successful) {
		var me = this;
		if(me.errorInfo) {
			var error = me.errorFormat.replace(/{msg}/, me.errorInfo);
			me.errorInfo = null;
		} else {
			if(!records || records.length <= 0) {
				var msg = me.msgFormat.replace(/{msg}/, JShell.Server.NO_DATA);
			}
		}
		if(!records || records.length <= 0) {
			me.fireEvent('nodata', me);
			return;
		}
	},
	/**创建数据字段*/
	getStoreFields: function(isString) {
		var me = this,
			columns = me.columns || [],
			length = columns.length,
			fields = [];
		for(var i = 0; i < length; i++) {
			var dataIndex = columns[i].dataIndex;
			if(dataIndex) {
				var obj = isString ? dataIndex : {
					name: dataIndex, //me.objectEName + "_" +
					type: columns[i].type ? columns[i].type : 'string'
				};
				fields.push(obj);
			}
		}
		return fields;
	},
	/**创建数据列*/
	createColumns: function() {
		var me = this;
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
		var BusinessModuleCode = me.objectEName + "_BusinessModuleCode"; //业务模块代码
		var DispOrder = me.objectEName + '_DispOrder'; //DispOrder
		var columns = me.columns || [];
		if(me.hasRownumberer) {
			columns.push({
				xtype: 'rownumberer',
				text: "序号",
				width: 30,
				align: 'center'
			});
		}
		columns.push({
			text: 'ID',
			isKey: true,
			hideable: false,
			sortable: false,
			menuDisabled: true,
			hidden: true,
			dataIndex: Id
		}, {
			text: '附件表原ID',
			hideable: false,
			hidden: true,
			width: 70,
			dataIndex: oldId
		}, {
			text: '附件名称(大小 )',
			flex: 1,
			minWidth: 70,
			hideable: false,
			sortable: !me.remoteSort,
			menuDisabled: true,
			dataIndex: FileName,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				var fileSize = "";
				if(record.get(me.objectEName + '_FileSize') != "")
					fileSize = ""+JcallShell.Bytes.toSize(record.get(me.objectEName + '_FileSize'));
				if(fileSize == "" &&(""+record.get(me.PKField))!="-1"&&me.isIE10Less() == true)
					fileSize == "待计算";
				var v = value + (fileSize.length>0?"(" + fileSize + ")":"");
				return v;
			}
		}, {
			text: '自定义名称',
			width: 150,
			hideable: false,
			sortable: !me.remoteSort,
			menuDisabled: true,
			hidden: me.hiddenNewFileNameColumn,
			dataIndex: NewFileName,
			editor: {
				readOnly: false
			}
		},  {
			text: '次序',
			dataIndex: DispOrder,
			width: 50,
			sortable: !me.remoteSort,
			menuDisabled: true,
			hidden:me.objectEName=="PProjectAttachment"?true:false,
			editor: {
				xtype: 'numberfield',
				allowBlank: true
			}
		},{
			text: '业务模块代码',
			dataIndex: BusinessModuleCode,
			width: 85,
			hidden: false,
			align: 'center',
			sortable: false,
			menuDisabled: true,
			renderer: function(value, p, record) {
				var oldValue = record.get(BusinessModuleCode);
				if(value == null || value == "") {
					value = oldValue;
				}
				if(value && value != undefined) {
					for(var i = 0; i < me.BusinessModuleCodeList.length; i++) {
						if(value.toString() == me.BusinessModuleCodeList[i][0].toString()) {
							return Ext.String.format(me.BusinessModuleCodeList[i][1]);
						}
					}
				}
			},
			editor: new Ext.form.field.ComboBox({
				mode: 'local',
				editable: false,
				displayField: 'text',
				valueField: 'value',
				value: me.BusinessModuleCode,
				listClass: 'x-combo-list-small',
				store: new Ext.data.SimpleStore({
					fields: ['value', 'text'],
					data: me.BusinessModuleCodeList
				})
//				,listeners: {
//					change: function(com, newValue) {
//						var record = com.ownerCt.editingPlugin.context.record;
//						record.set(BusinessModuleCode, newValue);
//						//record.commit();
//						me.getView().refresh();
//					}
//				}
			})
		}, {
			text: '文件类型',
			width: 50,
			hideable: false,
			sortable: false,
			menuDisabled: true,
			hidden: true,
			dataIndex: FileType
		}, {
			text: '扩展名',
			width: 50,
			hideable: false,
			sortable: false,
			menuDisabled: true,
			hidden: true,
			dataIndex: FileExt
		}, {
			text: 'file',
			width: 50,
			hidden: true,
			hideable: false,
			menuDisabled: true,
			dataIndex: 'file'
		}, {
			text: '创建人Id',
			width: 50,
			sortable: false,
			hidden: true,
			hideable: false,
			menuDisabled: true,
			dataIndex: CreatorID
		}, {
			text: '创建人',
			width: 80,
			hideable: false,
			sortable: false,
			menuDisabled: true,
			dataIndex: CreatorName,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				return value;
			}
		}, {
			text: '创建时间',
			width: 80,
			hideable: false,
			sortable: !me.remoteSort,
			menuDisabled: true,
			isDate: true,
			dataIndex: DataAddTime,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				return value;
			}
		}, {
			text: '上传进度',
			hideable: false,
			sortable: false,
			menuDisabled: true,
			hidden: me.hideProgressColumn,
			dataIndex: 'progress',
			width: 100,
			renderer: me.formatProgressBar,
			scope: me
		}, {
			text: '上传状态',
			width: 65,
			hideable: false,
			sortable: false,
			menuDisabled: true,
			dataIndex: 'status',
			hidden: me.hideStatusColumn,
			renderer: me.formatStatus
		}, {
			text: '大小',
			hideable: false,
			hidden: true,
			width: 70,
			dataIndex: FileSize,
			renderer: function(v) {
				if(v == "" &&me.isIE10Less() == true)
					v = "待计算";
				else
					v =JcallShell.Bytes.toSize(v);
				return v;
			}
		});
		var len = columns.length;
		for(var i = 0; i < len; i++) {
			if(columns[i].isKey) {
				me.PKField = columns[i].dataIndex;
				continue;
			}
			if(columns[i].isDate) {
				columns[i].renderer = function(value, meta, record, rowIndex, colIndex) {
					var bo = me.columns[colIndex].hasTime ? false : true;
					var v = JShell.Date.toString(value, bo) || '';
					if(v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
					return v;
				}
				continue;
			}
		}
		columns.push({
			xtype: 'actioncolumn',
			width: 35,
			text: "删除",
			hideable: false,
			menuDisabled: true,
			hideable: false,
			align: 'center',
			hidden: me.hideDelColumn,
			items: [{
				iconCls: 'button-del',
				tooltip: '删除附件',
				hideable: false,
				sortable: false,
				align: 'center',
				hidden: me.hideDelColumn,
				menuDisabled: true,
				handler: function(grid, rowIndex, colIndex) {
					var record = grid.store.getAt(rowIndex);
					me.fireEvent('deleteaction', grid, record, rowIndex, colIndex);
					var isExec = true;
					if(me.showDeleteMsgBox) {
						Ext.Msg.show({
							title: '提示信息',
							msg: '确定删除当前选择的附件文件吗?',
							buttons: Ext.Msg.OKCANCEL,
							icon: Ext.Msg.QUESTION,
							fn: function(buttonId, buttonText) {
								if(buttonId === "ok") {

									var id = record.get(me.PKField);
									if(id && id.toString() == "-1") {
										grid.store.remove(record);
									} else {
										me.delOneById(record);
									}
								}
								me.fireEvent('deleteactionafter', grid, rowIndex, colIndex);
							},
							scope: this
						});
					} else {
						var id = record.get(me.PKField);
						if(id && id.toString() == "-1") {
							grid.store.remove(record);
						} else {
							me.delOneById(record);
						}
						me.fireEvent('deleteactionafter', grid, rowIndex, colIndex);
					}
				}
			}]
		}, {
			xtype: 'actioncolumn',
			width: 35,
			text: "查看",
			hideable: false,
			menuDisabled: true,
			hideable: false,
			align: 'center',
			hidden: me.hideShowColumn,
			items: [{
				tooltip: '下载/查看附件',
				iconCls: 'button-show',
				sortable: false,
				hidden: me.hideShowColumn,
				handler: function(grid, rowIndex, colIndex) {
					me.fireEvent('showaction', grid, rowIndex, colIndex);
					//var oldId ="Old_"+me.PKField;
					var id = grid.store.getAt(rowIndex).get("Old_" + me.PKField);
					var record = grid.store.getAt(rowIndex);
					if(id && id.toString() == "-1") {
						JShell.Msg.alert("该文件还没有上传!不能下载查看");
					} else {
						me.ondownLoad(id, record)
					}
				}
			}]
		});
		return columns;
	},
	/**删除一条已上传成功的附件数据()
	 * 只更新是否使用为false,不删除数据库表数据,也不删除文件夹里的文件
	 * */
	delOneById: function(record) {
		var me = this;
		var id = record.get(me.PKField);
		var url = (me.delUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.delUrl;
		var entity = {
			Id: id,
			IsUse: 0
		};
		var params = {
			entity: entity,
			fields: "Id,IsUse"
		};
		params = Ext.JSON.encode(params);
		setTimeout(function() {
			JShell.Server.post(url, params, function(data) {
				if(data.success) {
					if(record) {
						me.getStore().remove(record);
					}
					me.delCount++;
				} else {
					me.delErrorCount++;
					if(record) {
						record.set(me.StatusField, -11);
						record.commit();
					}
				}
				if(me.delCount + me.delErrorCount == me.delLength) {
					//me.hideMask(); //隐藏遮罩层
					if(me.delErrorCount == 0) {
						//me.onSearch();
					} else {
						JShell.Msg.error('存在失败信息，具体错误内容请查看数据行的失败提示！');
					}
				}
			});
		}, 100);
	},

	/**
	 * 格式化状态显示颜色
	 */
	formatStatus: function(val, meta, record, rowIndex, colIndex, store, view) {
		if(val == '-1') { //准备上传
			return "准备上传";
		}
		if(val == '-4') { //上传成功
			return '<span style="color:' + 'green' + ';">' + '已上传' + '</span>';
		}
		if(val == '-3') { //上传失败
			return '<span style="color:' + 'red' + ';">' + '上传失败' + '</span>';
		}
		if(val == '-2') { //正在上传
			return '<span style="color:' + 'blue' + ';">' + '正在上传' + '</span>';
		}
		if(val == '-5') { //暂停上传
			return '<span style="color:' + 'red' + ';">' + '暂停上传' + '</span>';
		}
	},
	/**
	 * 格式化进度条
	 */
	formatProgressBar: function(v) {
		var progressBarTmp = this.getTplStr(v);
		return progressBarTmp;
	},
	getTplStr: function(v) {
		var bgColor = "orange";
		var borderColor = "#008000";
		return Ext.String.format(
			'<div>' +
			'<div style="border:1px solid {0};height:10px;width:{1}px;margin:4px 0px 1px 0px;float:left;">' +
			'<div style="float:left;background:{2};width:{3}%;height:8px;"><div></div></div>' +
			'</div>' +
			'<div style="text-align:center;float:right;width:30px;margin:3px 0px 1px 0px;height:10px;font-size:12px;">{3}%</div>' +
			'</div>', borderColor, (45), bgColor, v);
	},
	/**
	 * 设置选中
	 */
	selectRecord: function(record) {
		this.getSelectionModel().select(record);
	},
	/**
	 * 获得选中行
	 */
	getSelectRecord: function() {
		return this.getSelectionModel().getSelection()[0];
	},
	/**打开或下载文件*/
	ondownLoad: function(id, record) {
		var me = this;
		var url = me.downLoadUrl;
		var NewFileName = me.objectEName + '_NewFileName';
		var fileName = record.get(NewFileName);
		url += "?id=" + id + "&operateType=" + me.operateType + "&" + fileName;
		window.open(url);
	},
	/**@public 根据where条件加载数据*/
	load: function(where, isPrivate, autoSelect) {
		var me = this,
			collapsed = me.getCollapsed();
		me.defaultLoad = true;
		me.externalWhere = isPrivate ? me.externalWhere : where;
		if(me.externalWhere == undefined || me.externalWhere == null) {
			me.externalWhere = "";
		}
		//收缩的面板不加载数据,展开时再加载，避免加载无效数据
		if(collapsed) {
			me.isCollapsed = true;
			return;
		}
		if((me.fkObjectId && me.fkObjectId != "") || (me.externalWhere != "")) {
			me.store.currentPage = 1;
			console.log("load-store.load");
			me.store.load();
		} else {
			me.store.clearData();
		}
	},
	/**@public 根据where条件加载数据*/
	storeLoad: function(where, isPrivate, autoSelect) {
		var me = this,
			collapsed = me.getCollapsed();
		me.defaultLoad = true;
		me.externalWhere = isPrivate ? me.externalWhere : where;
		if(me.externalWhere == undefined || me.externalWhere == null) {
			me.externalWhere = "";
		}
		if((me.fkObjectId && me.fkObjectId != "") || (me.externalWhere != "")) {
			//收缩的面板不加载数据,展开时再加载，避免加载无效数据
			if(collapsed) {
				me.isCollapsed = true;
				return;
			}
			me.store.currentPage = 1;
			me.store.load();
		} else {
			me.store.clearData();
		}
	},
	/**清空数据,禁用功能按钮*/
	clearData: function() {
		var me = this;
		me.disableControl(); //禁用 所有的操作功能
		me.store.removeAll(); //清空数据
	},
	/**显示遮罩*/
	showMask: function(text) {
		var me = this;
		if(me.hasLoadMask) {
			me.body.mask(text);
		}
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if(me.hasLoadMask) {
			me.body.unmask();
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