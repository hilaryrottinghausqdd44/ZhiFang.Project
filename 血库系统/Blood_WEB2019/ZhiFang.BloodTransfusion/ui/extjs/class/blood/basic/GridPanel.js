/**
 * 基本明细列表
 * 添加获取系统运行参数"列表默认分页记录数"的默认每页数量
 * @author longfc
 * @version 2020-02-11
 */
Ext.define('Shell.class.blood.basic.GridPanel', {
	extend: 'Shell.ux.grid.Panel',

	/**默认每页数量*/
	defaultPageSize: 50,
	/**列表当前排序信息*/
	curOrderBy: [],
	/**存储默认排序信息*/
	_defaultOrderBy: null,
	/**是否启用右键菜单项*/
	hasContextMenu: true,
	/**自定义右键菜单项*/
	contextMenuItems: null,
	/**自定义右键菜单宽度*/
	contextMenuWidth: 145,
	/**是否已经初始化过状态管理*/
	isInitSetProvider: false,

	/**UI配置ID*/
	userUIID: null,
	/**用户UI配置Key*/
	userUIKey: null,
	/**用户UI配置Name*/
	userUIName: null,
	/**新增列表UI配置服务地址*/
	addUIUrl: '/BloodTransfusionManageService.svc/BT_UDTO_AddBUserUIConfig',
	/**修改列表UI配置服务地址*/
	editUIUrl: '/BloodTransfusionManageService.svc/BT_UDTO_UpdateBUserUIConfigByField',
	/**删除列表UI配置服务地址*/
	delUIUrl: '/BloodTransfusionManageService.svc/BT_UDTO_DelBUserUIConfig',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.initContextMenu();
		me.on('sortchange', function(ct, column, direction, eOpts) {
			var sortType = Ext.util.Format.uppercase(direction);
			me.curOrderBy = [{
				property: column.dataIndex,
				direction: sortType
			}];
		});
	},
	initComponent: function() {
		var me = this;
		if(me.defaultOrderBy && me.defaultOrderBy.length > 0) {
			me._defaultOrderBy = JShell.JSON.decode(JShell.JSON.encode(me.defaultOrderBy));
		}
		me.callParent(arguments);
	},
	/**获取运行参数配置的默认分页记录数,初始化列表默认分页记录数*/
	initDefaultPageSize: function() {
		var me = this;
		if(!me.hasPagingtoolbar) return;

		var defaultPageSize = JcallShell.BLTF.RunParams.Lists.BLTFUIDefaultPageSize.Value;
		if(!defaultPageSize)defaultPageSize=JcallShell.BLTF.cachedata.getCache("BLTFUIDefaultPageSize");
		
		//defaultPageSize=3000;
		if(!defaultPageSize) {
			//系统运行参数"列表默认分页记录数"
			JcallShell.BLTF.RunParams.getRunParamsValue("BLTFUIDefaultPageSize", false, function(data) {
				var defaultPageSize = JcallShell.BLTF.RunParams.Lists.BLTFUIDefaultPageSize.Value;
				if(!defaultPageSize)defaultPageSize=JcallShell.BLTF.cachedata.getCache("BLTFUIDefaultPageSize");
				if(!defaultPageSize) {
					me.defaultPageSize = 50;
				} else {
					defaultPageSize = parseInt(defaultPageSize);
					if(defaultPageSize <= 0) {
						defaultPageSize = 50;
					}
					if(defaultPageSize > 0) {
						me.defaultPageSize = defaultPageSize;
					}
				}
			});
		} else {
			if(!defaultPageSize) {
				me.defaultPageSize = 50;
			} else {
				defaultPageSize = parseInt(defaultPageSize);
				if(defaultPageSize <= 0) {
					defaultPageSize = 50;
				}
				if(defaultPageSize > 0) {
					me.defaultPageSize = defaultPageSize;
				}
			}
		}
	},
	/**初始化状态管理*/
	initSetProvider: function() {
		var me = this;
		if(me.isInitSetProvider == true) return;
		var provider = Ext.util.LocalStorage.supported ? new Ext.state.LocalStorageProvider() : new Ext.state.CookieProvider();
		Ext.state.Manager.setProvider(provider);
		me.isInitSetProvider = true;
		provider.on('statechange', function(provider, key, value) {
			me.onStateChange(provider, key, value);
		});
	},
	/**初始化右键菜单*/
	initContextMenu: function() {
		var me = this;
		if(!me.hasContextMenu) return;
		me.on(
			"itemcontextmenu",
			function(view, record, item, rowIndex, e, eOpts) {
				e.preventDefault();
				me.onRowContextMenu(e);
			}
		);
	},
	/**用户UI配置还原处理*/
	decreaseUserUI: function() {
		var me = this;
		if(!me.userUIKey) {
			me.initDefaultPageSize();
			return;
		}
		//启用用户UI配置
		var enableUserUIConfig = ""+JcallShell.BLTF.RunParams.Lists.EnableUserUIConfig.Value;
		if(!enableUserUIConfig)enableUserUIConfig=JcallShell.BLTF.cachedata.getCache("EnableUserUIConfig");
		if(!enableUserUIConfig || enableUserUIConfig != "1") {
			me.initDefaultPageSize();
			return;
		}
		JcallShell.BLTF.BUserUIConfig.getUIConfigByKey(me.userUIKey, false, function(userUI) {
			if(!userUI) {
				me.initDefaultPageSize();
				return;
			}
			me.userUIID = userUI["BUserUIConfig_Id"];
			var comment = userUI["BUserUIConfig_Comment"];
			if(!comment) {
				me.initDefaultPageSize();
				return;
			}
			if(comment && Ext.isString(comment)) comment = JShell.JSON.decode(comment);
			var defaultOrderBy = "";
			if(comment && comment["defaultOrderBy"]) defaultOrderBy = comment["defaultOrderBy"];
			if(defaultOrderBy) {
				me.curOrderBy = JShell.JSON.decode(JShell.JSON.encode(defaultOrderBy));
				me.defaultOrderBy = JShell.JSON.decode(JShell.JSON.encode(defaultOrderBy)); //Ext.apply(me.defaultOrderBy,defaultOrderBy);
				me._defaultOrderBy = JShell.JSON.decode(JShell.JSON.encode(defaultOrderBy));
			}
			var defaultPageSize = null;
			if(comment && comment["defaultPageSize"]) defaultPageSize = comment["defaultPageSize"];
			if(defaultPageSize) {
				me.defaultPageSize = defaultPageSize;
				if(me.store) me.store.pageSize = defaultPageSize;
			}
			var columns2 = null;
			if(comment && comment["columns"]) columns2 = comment["columns"];
			me.decreaseColumns(columns2);

			if(!defaultPageSize) {
				me.initDefaultPageSize();
			}
		});
	},
	/**列表的列配置还原处理*/
	decreaseColumns: function(columns2) {
		var me = this;
		if((!me.columns || me.columns.length <= 0) || (!columns2 || columns2.length <= 0)) return;

		var columns = me.columns;
		for(var i = 0; i < columns.length; i++) {
			var column2 = null;
			Ext.Array.forEach(columns2, function(column, columnIndex, allItems) {
				if(columns[i].dataIndex == column.dataIndex) {
					column2 = column;
					if(!column2.columnIndex) column2.columnIndex = columnIndex;
					return false;
				}
			});
			if(column2) {
				columns[i].columnIndex = column2.columnIndex;
				columns[i].hidden = column2.hidden;
				columns[i].width = column2.width;
			} else {
				columns[i].columnIndex = i;
			}
		}
		me.columns = Ext.Array.sort(columns, function(item1, item2) {
			var columnIndex1 = parseInt(item1.columnIndex);
			var columnIndex2 = parseInt(item2.columnIndex);
			if(columnIndex1 < columnIndex2) {
				return -1;
			} else if(columnIndex1 === columnIndex2) {
				return 0;
			} else {
				return 1;
			}
		});
	},
	/**创建右键菜单项*/
	createContextMenu: function() {
		var me = this;
		items = me.contextMenuItems || [];
		//启用用户UI配置
		var enableUserUIConfig = ""+JcallShell.BLTF.RunParams.Lists.EnableUserUIConfig.Value;
		if(!enableUserUIConfig)enableUserUIConfig=JcallShell.BLTF.cachedata.getCache("EnableUserUIConfig");
		if(me.userUIKey && enableUserUIConfig == "1") {
			items.unshift({
				text: "保存列表配置",
				iconCls: 'button-save',
				pressed: false,
				handler: function() {
					me.onSaveUIClick();
				}
			}, {
				text: "刷新列表配置",
				iconCls: 'button-refresh',
				pressed: false,
				hidden: true,
				handler: function() {}
			}, {
				text: "删除列表配置",
				iconCls: 'button-del',
				pressed: false,
				handler: function() {
					me.onDelUIClick();
				}
			}, '-');
		}
		if(!items || items.length <= 0) return null;

		var contextmenu = new Ext.menu.Menu({
			width: me.contextMenuWidth,
			margin: '0 0 10 0',
			items: items
		});
		return contextmenu;
	},
	/**初始化列表右键菜单*/
	onRowContextMenu: function(e) {
		var me = this;
		var contextmenu = me.createContextMenu();
		if(contextmenu) contextmenu.showAt(e.getXY());
	},
	/**获取新增列表列配置数据*/
	getAddUIParams: function() {
		var me = this;
		var columns2 = [];
		Ext.Array.each(me.columns, function(column, columnIndex, allItems) {
			var column2 = {
				//"xtype": column.xtype,
				"columnIndex": columnIndex,
				"dataIndex": column.dataIndex,
				"hidden": column.hidden,
				"width": column.width,
			};
			columns2.push(column2);
		});
		var empID = JcallShell.System.Cookie.get(JcallShell.System.Cookie.map.USERID);
		if(!me.userUIID) me.userUIID = -1;
		var defaultOrderBy = me.curOrderBy;
		if(!defaultOrderBy && me._defaultOrderBy && me._defaultOrderBy.length > 0) defaultOrderBy = me._defaultOrderBy;
		var comment = {
			"columns": columns2,
			"defaultOrderBy": defaultOrderBy,
			"defaultPageSize": me.store.pageSize
		};

		var entity = {
			Id: me.userUIID,
			UserUIKey: me.userUIKey,
			UserUIName: me.userUIName,
			UITypeID: 1, //所属UI类型Id:列表配置
			ModuleId: null, //所属模块Id
			EmpID: empID, //所属员工ID
			IsDefault: 1, //
			IsUse: 1, //
			Comment: JShell.JSON.encode(comment) //配置内容
		};
		return {
			"entity": entity
		};
	},
	getEditUIParams: function() {
		var me = this,
			entity = me.getAddUIParams();
		var fields = ['Id', 'UserUIKey', 'UserUIName', 'Comment'];
		entity.fields = fields.join(',');
		return entity;
	},
	/**保存按钮点击处理方法*/
	onSaveUIClick: function() {
		var me = this;
		var formtype = 'add';
		if(me.userUIID && me.userUIID > 0) formtype = 'edit';

		var url = formtype == 'add' ? me.addUIUrl : me.editUIUrl;
		url = (url.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + url;
		var params = formtype == 'add' ? me.getAddUIParams() : me.getEditUIParams();
		if(!params) return;
		var id = params.entity.Id;
		params = Ext.JSON.encode(params);

		me.showMask(me.saveText); //显示遮罩层
		JShell.Server.post(url, params, function(data) {
			me.hideMask(); //隐藏遮罩层
			if(data.success) {
				id = formtype == 'add' ? data.value.id : id;
				id += '';
				me.userUIID = id;
				params = Ext.JSON.decode(params);
				var entity = params.entity;
				var params2 = {
					BUserUIConfig_Id: me.userUIID,
					BUserUIConfig_IsUse: 1,
					BUserUIConfig_UserUIKey: entity.UserUIKey,
					BUserUIConfig_UserUIName: entity.UserUIName,
					BUserUIConfig_UITypeID: entity.UITypeID,
					BUserUIConfig_ModuleId: entity.ModuleId,
					BUserUIConfig_EmpID: entity.EmpID,
					BUserUIConfig_Comment: entity.Comment
				}
				JcallShell.BLTF.BUserUIConfig.addListByKey(me.userUIKey, params2);
				me.fireEvent('saveUI', me, id);
				JShell.Msg.alert("用户列表配置保存成功!", null, 1500);
			} else {
				JShell.Msg.error(data.msg);
			}
		});
	},
	/**删除UI配置处理*/
	onDelUIClick: function() {
		var me = this;
		if(!me.userUIID) {
			JShell.Msg.error("用户列表配置未保存到服务器,不需要执行删除操作!");
			return;
		}

		var url = (me.delUIUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.delUIUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'id=' + me.userUIID;
		setTimeout(function() {
			JShell.Server.get(url, function(data) {
				if(data.success) {
					me.userUIID = -1;
					JcallShell.BLTF.BUserUIConfig.removeByKey(me.userUIKey);
					JShell.Msg.alert('删除用户列表配置成功！', null, 1500);
				} else {
					JShell.Msg.error('删除用户列表配置失败！' + data.msg);
				}
			});
		}, 100);
	}
});