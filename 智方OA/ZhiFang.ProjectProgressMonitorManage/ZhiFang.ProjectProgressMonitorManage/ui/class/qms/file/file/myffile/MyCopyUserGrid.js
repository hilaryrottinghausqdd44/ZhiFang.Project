/**
 * QMS当前登录者的抄送文件列表
 * @author longfc
 * @version 2016-06-28
 */
Ext.define('Shell.class.qms.file.file.myffile.MyCopyUserGrid', {
	extend: 'Shell.class.qms.file.show.Grid',
	title: '我的抄送文件信息',
	hasRefresh: true,
	/**默认加载数据*/
	defaultLoad: true,
	/**默认选中数据*/
	autoSelect: true,
	checkOne: true,
	remoteSort: false,
	isSearchChildNode: true,
	FTYPE: "1",
	/**默认排序字段*/
	defaultOrderBy: [{
		property: 'FFile_IsTop',
		direction: 'DESC'
	}, {
		property: 'FFile_BDictTree_Id',
		direction: 'ASC'
	}, {
		property: 'FFile_Title',
		direction: 'ASC'
	}],
	/**获取我的抄送文件信息数据服务路径*/
	selectUrl: '/QMSService.svc/QMS_UDTO_SearchFFileCopyUserListByHQLAndEmployeeID?isPlanish=true',
	afterRender: function() {
		var me = this;
		me.on({
			itemdblclick: function(grid, record, item, index, e, eOpts) {
				me.openShowTabPanel(record);
			},
			onShowClick: function() {
				var me = this;
				var records = me.getSelectionModel().getSelection();
				if(records && records.length < 1) {
					JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
					return;
				}
				me.openShowTabPanel(records[0]);
			}
		});
		me.callParent(arguments);

	},
	/**文档状态下拉选择框数据处理*/
	removeDataList: function(dataList) {
		var me = this;
		var returndata=[];
		if(!dataList) return returndata;
		var removeIdStr = ["1", "7", "8"]; //暂存,作废,撤消提交
		for(var i = 0; i < dataList.length; i++) {
			var model = dataList[i];
			if(model && Ext.Array.indexOf(removeIdStr,"" + model[0]) == -1) {
				returndata.push(model);
			}
		}
		return returndata;
	}
});