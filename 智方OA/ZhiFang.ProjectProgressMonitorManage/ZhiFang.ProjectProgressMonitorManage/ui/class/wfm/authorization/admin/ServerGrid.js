/**
 * 服务器授权文件重新生成
 * @author longfc	
 * @version 2016-12-26
 */
Ext.define('Shell.class.wfm.authorization.admin.ServerGrid', {
	extend: 'Shell.class.wfm.authorization.ahserver.basic.ServerGrid',

	title: '服务器授权审核',

	/**获取数据服务路径:支持主单查询条件及仪器明细查询条件*/
	selectUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchAHServerLicenceByDocAndDtlHQL',
	/**重新生成服务器授权返回文件服务路径*/
	afreshUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_RegenerateAHServerLicenceById',

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
	initComponent: function() {
		var me = this;
		//商务授权通过并且不需要特批;
		me.defaultWhere ='ahserverlicence.IsUse=1 and ((ahserverlicence.IsSpecially=0 and ahserverlicence.Status=4) or (ahserverlicence.IsSpecially=1 and ahserverlicence.Status=7) or ahserverlicence.Status=9)';

		me.callParent(arguments);
	},
	/**获取状态列表*/
	getLicenceStatusData: function(statusList) {
		var me = this,
			data = [];
		data.push(['', '=全部=', 'font-weight:bold;color:#303030;text-align:center']);
		for (var i in statusList) {
			var obj = statusList[i];
			if (obj && (obj.Id == 4 || obj.Id == 9)) {
				var style = ['font-weight:bold;text-align:center'];
				if (obj.BGColor) {
					style.push('color:' + obj.BGColor);
				}
				data.push([obj.Id, obj.Name, style.join(';')]);
			}
		}
		return data;
	},
	//重新生成授权文件
	onRegenerateAHServerLicence: function(autoSelect) {
		var me = this,
			records = me.getSelectionModel().getSelection();
		if (records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		var updateArr = [];
		for (var i in records) {
			var status = "" + records[i].get("Status");
			//特批授权通过,授权完成
			if (status == "7" || status == "9") {
				updateArr.push(records[i]);
			} else if (status == "4") {
				var IsSpecially = "" + records[i].get("IsSpecially");
				switch (IsSpecially.toLocaleLowerCase()) {
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
				if (IsSpecially == 0) {
					updateArr.push(records[i]);
				}
			}
		}
		if (updateArr.length == 0) {
			JShell.Msg.error("没有选择授权记录!");
			return;
		}
		me.updateErrorCount = 0;
		me.updateCount = 0;
		me.updateLength = updateArr.length;
		me.showMask("批量重新生成授权文件中..."); //显示遮罩层
		me.errorMsg = "";
		var index = 0;
		for (var i in updateArr) {
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
				if (data.success) {
					me.updateCount++;
				} else {
					me.errorMsg = me.errorMsg + data.msg + "<br />";
					me.updateErrorCount++;
				}
				if (me.updateCount + me.updateErrorCount == me.updateLength) {
					me.hideMask(); //隐藏遮罩层
					if (me.updateErrorCount == 0) {
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
