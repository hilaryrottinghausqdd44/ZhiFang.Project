/**
 * 出库库房权限
 * @author liangyl	
 * @version 2017-11-08
 */
Ext.define('Shell.class.rea.client.shelves.storage.LinkGrid', {
	extend: 'Shell.class.rea.client.shelves.storage.BasicLinkGrid',

	/**库房人员关系类型:
	 * 1:按库房管理权限;
	 * 2:按移库申请权限;
	 * 3:按出库申请权限;
	 * */
	OperType: "1",
	StorageID: null,
	StorageName: null,

	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			search = null,
			StorageID = null,
			params = [];
		me.internalWhere = '';

		if(me.StorageID) {
			params.push('reauserstoragelink.StorageID=' + me.StorageID + ' and reauserstoragelink.PlaceID is null');
		}
		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
		if(search) {
			if(me.internalWhere) {
				me.internalWhere += ' and (' + me.getSearchWhere(search) + ')';
			} else {
				me.internalWhere = "(" + me.getSearchWhere(search) + ")";
			}
		}
		return me.callParent(arguments);
	},
	onAddClick: function() {
		this.showForm();
	},
	/**显示表单*/
	showForm: function() {
		var me = this,
			config = {
				resizable: false,
				checkOne: false,
				listeners: {
					accept: function(p, record) {
						me.onSave(record, p);
					}
				}
			};
		JShell.Win.open('Shell.class.sysbase.user.CheckApp', config).show();
	},
	onSave: function(records, p) {
		var me = this,
			ids = [],
			addIds = [];

		if(records.length == 0) return;
		me.showMask(me.saveText); //显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = records.length;
		for(var i in records) {
			ids.push(records[i].get('HREmployee_Id'));
		}
		//获取现有关系用于验证过滤已经存在的关系
		me.getLinkByIds(ids, function(list) {
			addIds = [];
			for(var i in records) {
				var userId = records[i].get('HREmployee_Id');
				var CName = records[i].get('HREmployee_CName');
				var hasLink = false;
				for(var j in list) {
					if(userId == list[j].OperID) {
						hasLink = true;
						break;
					}
				}
				if(!hasLink) {
					var obj = {
						OperID: userId,
						OperName: CName
					};
					addIds.push(obj);
				}
				if(hasLink) {
					me.hideMask(); //隐藏遮罩层
					p.close();
				}
			}
			//循环保存数据
			for(var i in addIds) {
				me.saveLength = addIds.length;
				me.onAddOneLink(addIds[i], function() {
					p.close();
					me.onSearch();
				});
			}
		});
	},
	/**根据IDS获取关系数据，用于验证勾选的出库人是否已经存在于关系中*/
	getLinkByIds: function(ids, callback) {
		var me = this,
			url = JShell.System.Path.ROOT + me.selectUrl.split('?')[0] +
			'?fields=ReaUserStorageLink_OperID,ReaUserStorageLink_Id' +
			'&where=reauserstoragelink.OperID in(' + ids.join(',') + ')' +
			' and reauserstoragelink.OperType=' + me.OperType +
			' and reauserstoragelink.StorageID=' + me.StorageID + ' and reauserstoragelink.PlaceID is null';
		JShell.Server.get(url, function(data) {
			if(data.success) {
				var list = (data.data || {}).list || [];
				callback(data.value.list);
			} else {
				JShell.Msg.error(data.msg);
			}
		});
	},
	/**新增关系数据*/
	onAddOneLink: function(addIds, callback) {
		var me = this,
			url = JShell.System.Path.ROOT + me.addUrl;
		var entity = {
			OperType: me.OperType,
			StorageID: me.StorageID,
			StorageName: me.StorageName,
			OperID: addIds.OperID,
			OperName: addIds.OperName,
			OperID: addIds.OperID,
			OperName: addIds.OperName,
			Visible: 1
		};
		var Sysdate = JcallShell.System.Date.getDate();
		var DataAddTime = JcallShell.Date.toString(Sysdate);
		entity.DataUpdateTime = JShell.Date.toServerDate(DataAddTime) ? JShell.Date.toServerDate(DataAddTime) : null;
		entity.DataAddTime = JShell.Date.toServerDate(DataAddTime) ? JShell.Date.toServerDate(DataAddTime) : null;
		var params = {
			entity: entity
		};
		//提交数据到后台
		JShell.Server.post(url, Ext.JSON.encode(params), function(data) {
			if(data.success) {
				me.saveCount++;
			} else {
				me.saveErrorCount++;
			}
			if(me.saveCount + me.saveErrorCount == me.saveLength) {
				me.hideMask(); //隐藏遮罩层
				if(me.saveErrorCount == 0) {
					callback();
				}
			}
		});
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			dataIndex: 'ReaUserStorageLink_OperName',
			text: '库房权限人员',
			width: 150,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaUserStorageLink_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}, {
			dataIndex: 'ReaUserStorageLink_OperID',
			text: '出库人ID',
			hidden: true,
			hideable: false
		}];
		return columns;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = ['refresh', '-', 'del', '-', {
			text: '添加人',
			tooltip: '添加人选择',
			iconCls: 'button-add',
			itemId: 'addbtn',
			handler: function() {
				me.onAddClick();
			}
		}];
		return items;
	}
});