/**
 * 服务器授权文件重新生成
 * @author longfc	
 * @version 2016-12-26
 */
Ext.define('Shell.class.wfm.authorization.admin.ServerGrid', {
	extend: 'Shell.class.wfm.authorization.ahserver.basic.ServerGrid',
	title: '服务器授权审核',
	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用查询框*/
	hasSearch: false,
	/**默认加载*/
	defaultLoad: true,
	/**是否管理员模块*/
	isAdmin: true,
	/**是否单选*/
	checkOne: false,
	/**是否有日期范围*/
	hasDates: true,
	errorMsg: "",
	/**重新生成服务器授权返回文件服务路径*/
	afreshUrl: '/SingleTableService.svc/ST_UDTO_RegenerateAHServerLicenceById',
	initComponent: function() {
		var me = this;
		//复选框
		me.multiSelect = true;
		me.selType = 'checkboxmodel';
		me.callParent(arguments);
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.on({
			itemdblclick: function(view, record) {
				var id = record.get(me.PKField);
				var Status = record.get('Status');
				me.openShowForm(record);
			}
		});
	},
	/**获取状态列表*/
	getLicenceStatusData: function(StatusList) {
		var me = this,
			data = [];
		data.push(['', '=全部=', 'font-weight:bold;color:#303030;text-align:center']);
		for(var i in StatusList) {
			var obj = StatusList[i];
			if(obj && (obj.Id == 4 || obj.Id == 9)) {
				var style = ['font-weight:bold;text-align:center'];
				if(obj.BGColor) {
					style.push('color:' + obj.BGColor);
				}
				data.push([obj.Id, obj.Name, style.join(';')]);
			}
		}
		return data;
	},
	/**查询数据*/
	onSearch: function(autoSelect) {
		var me = this;
		//商务授权通过并且不需要特批;
		me.defaultWhere = 'IsUse=1 and ((IsSpecially=0 and Status=4) or (IsSpecially=1 and Status=7) or Status=9)';
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		if(!buttonsToolbar) {
			return;
		}
		if(me.hasDates == true) {
			var BeginDate = buttonsToolbar.getComponent('BeginDate').getValue();
			var EndDate = buttonsToolbar.getComponent('EndDate').getValue();
			var StartDateValue = JcallShell.Date.toString(BeginDate, true);
			var EndDateValue = JcallShell.Date.toString(EndDate, true);
			if(StartDateValue > EndDateValue) {
				JShell.Msg.error('结束日期不能小于开始日期!');
				return;
			}
		}
		JShell.System.ClassDict.init('ZhiFang.Entity.ProjectProgressMonitorManage', 'LicenceType', function() {
			if(!JShell.System.ClassDict.LicenceType) {
				JShell.Msg.error('未获取到授权类型，请刷新列表');
				return;
			}
		});
		JShell.System.ClassDict.init('ZhiFang.Entity.ProjectProgressMonitorManage', 'LicenceStatus', function() {
			if(!JShell.System.ClassDict.LicenceStatus) {
				JShell.Msg.error('未获取到授权状态，请刷新列表');
				return;
			}
			var CheckStatus = buttonsToolbar.getComponent('CheckStatus');
			var StatusList = JShell.System.ClassDict.LicenceStatus;
			if(CheckStatus.store.data.items.length == 0) {
				CheckStatus.loadData(me.getLicenceStatusData(StatusList));
			}
			me.load(null, true, autoSelect);
		});
	},
	onRegenerateAHServerLicence: function(autoSelect) {
		var me = this,
			records = me.getSelectionModel().getSelection();
		if(records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		var updateArr = [];
		for(var i in records) {
			var status = "" + records[i].get("Status");
			//特批授权通过,授权完成
			if(status == "7" || status == "9") {
				updateArr.push(records[i]);
			} else if(status == "4") {
				var IsSpecially = "" + records[i].get("IsSpecially");
				switch(IsSpecially.toLocaleLowerCase()) {
					case "true":
						IsSpecially = 1;
						break;
					case "1":
						IsSpecially = 1;
						break;
					case "false":
						IsSpecially = 0;
						break;
					default:
						break;
				}
				if(IsSpecially == 0) {
					updateArr.push(records[i]);
				}
			}
		}
		if(updateArr.length == 0) {
			JShell.Msg.error("没有选择授权记录!");
			return;
		}
		me.updateErrorCount = 0;
		me.updateCount = 0;
		me.updateLength = updateArr.length;
		me.showMask("批量重新生成授权文件中..."); //显示遮罩层
		me.errorMsg = "";
		var index = 0;
		for(var i in updateArr) {
			index++;
			me.RegenerateOneById(index, updateArr[i]);
		}
	},

	/**删除一条数据*/
	RegenerateOneById: function(index, record) {
		var me = this;
		var id = record.get(me.PKField);
		var url = (me.afreshUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.afreshUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'id=' + id;

		setTimeout(function() {
			JShell.Server.get(url, function(data) {
				//var record = me.store.findRecord(me.PKField, id);
				if(data.success) {
					me.updateCount++;
				} else {
					me.errorMsg = me.errorMsg + data.msg + "<br />";
					me.updateErrorCount++;
				}
				if(me.updateCount + me.updateErrorCount == me.updateLength) {
					me.hideMask(); //隐藏遮罩层
					if(me.updateErrorCount == 0) {
						//me.onSearch();
						JShell.Msg.alert('批量重新生成授权文件完成!', null, 1000);
					} else {
						JShell.Msg.error('批量重新生成授权文件失败!<br />' + me.errorMsg);
					}
				}
			});
		}, 500 * index);
	}
});