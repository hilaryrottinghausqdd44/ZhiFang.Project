/**
 * 项目列表
 * @author Jcall
 * @version 2020-01-06
 */
Ext.define('Shell.class.lts.sample.result.sample.Grid', {
	extend: 'Shell.ux.grid.Panel',

	//批量更新样本项目结果
	editUrl: '/ServerWCF/LabStarService.svc/LS_UDTO_EditBatchLisTestItemResult',
	//删除数据服务路径
	//delUrl: '/ServerWCF/LabStarService.svc/LS_UDTO_DelLisTestItem',
	//修改数据服务路径
	logicDelUrl: '/ServerWCF/LabStarService.svc/LS_UDTO_DeleteBatchLisTestItem',
	//获得结果状态颜色
	getResultStatusColorUrl: '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBDictByHQL?isPlanish=true&sort=[{property:"LBDict_DispOrder",direction:"ASC"}]&fields=LBDict_Id,LBDict_CName,LBDict_DictCode,LBDict_EName,LBDict_SName,LBDict_IsUse,LBDict_DispOrder,LBDict_ColorValue,LBDict_ColorDefault',
	//结果状态类型
	ResultStatusDictType: 'ResultStatus',
	//结果状态信息
	ResultStatusList:[],
	bodyPadding: 1,
	title: '项目列表',

	//默认加载
	defaultLoad: false,
	//不加载时默认禁用功能按钮
	defaultDisableControl: false,
	//是否启用序号列
	hasRownumberer: false,
	//带功能按钮栏
	hasButtontoolbar: false,
	//带分页栏
	hasPagingtoolbar: false,
	//后台排序
	remoteSort: false,
	//是否开启排序
	sortableColumns: false,
	//排序字段
	defaultOrderBy: [
		{ property: 'LisTestItem_PLBItem_DispOrder', direction: 'ASC' },
		{ property: 'LisTestItem_LBItem_DispOrder', direction: 'ASC' }
	],
	//网格线
	columnLines: true,

	//复选框
	multiSelect: true,
	selType: 'checkboxmodel',

	afterRender: function () {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function () {
		var me = this;
		//获得结果状态颜色
		me.getResultStatusColor();
		//分组控件
		//内部排序逻辑：按组合项目名称进行正序，即按字符串排序，需要改为按组合项目排序列正序
		me.groupingFeature = Ext.create('Ext.grid.feature.Grouping', {
			groupHeaderTpl: '{columnName}: {name} (共包含 {rows.length} 项)',
			hideGroupedHeader: true,
			enableGroupingMenu: true,
			enableNoGroups: true,
			groupByText: '按本列排序',
			showGroupsText: '显示分组'
		});
		me.features = [me.groupingFeature];
		//可编辑单元格 -- 放到上面会多个tabPanel中的可编辑单元格失效 并且关闭tabPanel报错
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', { clicksToEdit: 1 }),
		//列
		me.columns = [
		{ text: '组合项目名称', dataIndex: 'LisTestItem_PLBItem_CName', width: 100, defaultRenderer: true  },
			{ text: '采样项目', dataIndex: 'LisTestItem_LisOrderItem_HisItemName', width: 100, defaultRenderer: true, hidden: true },
			{ text: '检测项目主键', dataIndex: 'LisTestItem_LBItem_Id', width: 100, defaultRenderer: true, hidden: true, hideable: false },
			{ text: '检测项目', dataIndex: 'LisTestItem_LBItem_CName', width: 100, renderer: me.setGridColumnsStyle },
			{ text: '检测项目英文', dataIndex: 'LisTestItem_LBItem_EName', width: 100, hidden: true, renderer: me.setGridColumnsStyle },
			{ text: '检测项目简称', dataIndex: 'LisTestItem_LBItem_SName', width: 100, renderer: me.setGridColumnsStyle },
			{ text: '检测项目精度', dataIndex: 'LisTestItem_LBItem_Prec', width: 100, defaultRenderer: true, hidden: true, hideable: false },
			{
				text: '报告值', dataIndex: 'LisTestItem_ReportValue', width: 80,
				editor: {}, style: 'font-weight:bold;color:white;background:orange;',renderer: me.setGridColumnsStyle
			},
			{ text: '定量结果', dataIndex: 'LisTestItem_QuanValue', width: 80, defaultRenderer: true, hidden: true  },
			{ text: '原始值', dataIndex: 'LisTestItem_OriglValue', width: 80, defaultRenderer: true },
			{ text: '结果状态', dataIndex: 'LisTestItem_ResultStatus', width: 80, renderer: me.setGridColumnsStyle },
			{ text: '检验结果状态码', dataIndex: 'LisTestItem_ResultStatusCode', width: 80, hidden: true, renderer: me.setGridColumnsStyle },
			{ text: '结果单位', dataIndex: 'LisTestItem_Unit', width: 80, defaultRenderer: true },
			{ text: '参考范围', dataIndex: 'LisTestItem_RefRange', width: 80, defaultRenderer: true },
			{ text: '上次结果', dataIndex: 'LisTestItem_PreValue', width: 80, defaultRenderer: true },
			{ text: '历史对比', dataIndex: 'LisTestItem_PreCompStatus', width: 80, defaultRenderer: true },
			{ text: '历史值2', dataIndex: 'LisTestItem_PreValue2', width: 80, defaultRenderer: true, hidden: true },
			{ text: '历史值3', dataIndex: 'LisTestItem_PreValue3', width: 80, defaultRenderer: true, hidden: true },
			{ text: '结果说明', dataIndex: 'LisTestItem_ResultComment', width: 100, defaultRenderer: true },
			{ text: '主键ID', dataIndex: 'LisTestItem_Id', width: 190, isKey: true, hidden: true, hideable: false },
			{ text: '医嘱单项目ID', dataIndex: 'LisTestItem_LisOrderItem_Id', width: 80, defaultRenderer: true, hidden: true, hideable: false },
			{ text: '组合项目排序', dataIndex: 'LisTestItem_PLBItem_DispOrder', width: 90, hidden: true, hideable: false, type: 'int' },
			{ text: '项目排序', dataIndex: 'LisTestItem_LBItem_DispOrder', width: 90, hidden: true, hideable: false, type: 'int' },
			{ text: '检验单Id', dataIndex: 'LisTestItem_LisTestForm_Id', width: 90, hidden: true, hideable: false },
			{
				text: '是否采用特殊提示色', dataIndex: 'LisTestItem_BAlarmColor', width: 80,
				hidden: true, renderer: function (value, meta, record) {
					if (String(value) == "true") {
						return "是";
					} else {
						return "否";
					}
				}
			},
			{
				text: '结果警示特殊颜色', dataIndex: 'LisTestItem_AlarmColor', width: 80,
				hidden: true, renderer: function (value, meta, record) {
					if (value) {
						meta.style = 'color:' + value + ';';
					}
					return value;
				}
			}
		];

		me.callParent(arguments);
	},

	//更新数据
	onLoadByData: function (data) {
		var me = this,
			list = data,
			len = list.length;

		for (var i = 0; i < len; i++) {
			//项目=组合项目
			if (list[i].LisTestItem_LBItem_CName == list[i].LisTestItem_PLBItem_CName) {
				list[i].LisTestItem_PLBItem_CName = '';//不存在组合项目则置空
				list[i].LisTestItem_PLBItem_DispOrder = 9999999;
			}
		}

		me.store.loadData(data);
	},
	//按所有检验项目展示
	showByItem: function () {
		var me = this;
		me.groupingFeature.disable();
	},
	//按医嘱项目展示
	showByPItem: function () {
		var me = this;
		me.groupingFeature.enable();
	},
	//删除按钮点击处理方法
	onDelClick: function (delOneCallback, delAllCallback) {
		var me = this,
			msg = [],
			records = me.getSelectionModel().getSelection();

		if (records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}

		var delIDList = [];
		var LisTestFormID = records[0].get('LisTestItem_LisTestForm_Id');
		for (var i in records) {
			if (records[i].get("LisTestItem_LisOrderItem_Id")) msg.push(records[i].get("LisTestItem_LBItem_CName") + " 是医嘱项目;");
			if (records[i].get("LisTestItem_ReportValue")) msg.push(records[i].get("LisTestItem_LBItem_CName") + " 已出检验结果=" + records[i].get("LisTestItem_ReportValue")+";");
			var id = records[i].get(me.PKField);
			delIDList.push(id);
		}
		if (msg.length > 0) {
			msg.push("建议不要删除,是否仍要删除？");
		} else {
			msg.push("确定要删除吗？");
		}

		JShell.Msg.confirm({ msg: msg.join("<br/>") }, function (but) {
			if (but != "ok") return;
			me.showMask(me.delText); //显示遮罩层
			me.delBatchLisTestItem(LisTestFormID, delIDList);
		});
	},
	//批量删除项目
	delBatchLisTestItem: function (LisTestFormID, delIDList) {
		var me = this,
			url = (me.logicDelUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.logicDelUrl,
			LisTestFormID = LisTestFormID || null,
			delIDList = delIDList || [];
		if (LisTestFormID == null || delIDList.length == 0) return;
		JShell.Server.post(url, Ext.JSON.encode({ testFormID: LisTestFormID, delIDList: delIDList.join(",") }),
			function (data) {
				me.hideMask(); //隐藏遮罩层
				if (data.success) {
					JShell.Msg.alert('检验项目删除成功！',null,1000);
					me.fireEvent("delBatchLisTestItem",me);
					//delAllCallback && delAllCallback();
				} else {
					JShell.Msg.error('检验项目删除失败！');
				}
			});

	},
	//删除一条数据 -- 逻辑删除
	delOneById: function(index, id,delOneCallback,delAllCallback) {
		var me = this,
			url = (me.logicDelUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.logicDelUrl,
			id = id;
		setTimeout(function () {
			JShell.Server.post(url, Ext.JSON.encode({ entity: { MainStatusID: -2, Id: id }, fields: 'Id,MainStatusID' }),
				function (data) {
					var record = me.store.findRecord(me.PKField, id);
					if (data.success) {
						if (record) {
							me.store.remove(record);
						}
						me.delCount++;
					} else {
						me.delErrorCount++;
						if (record) {
							record.commit();
						}
					}
					if (me.delCount + me.delErrorCount == me.delLength) {
						me.hideMask(); //隐藏遮罩层
						if (me.delErrorCount == 0) {
							delAllCallback && delAllCallback();
						} else {
							JShell.Msg.error(me.delErrorCount + '条数据删除失败！');
						}
					}
					delOneCallback && delOneCallback(id);
			});
		}, 100 * index);
	},
	//保存项目
	onSaveClick: function (testFormRecord) {
		if (!testFormRecord) return;
		var me = this,
			lisTestFormID = testFormRecord.get("LisTestForm_Id"),
			records = me.store.getRange(),//获取所有记录
			len = records.length,
			list = [];
			
		for(var i=0;i<len;i++){
			var rec = records[i];
			if (rec.dirty) {//本记录被修改了
				var ReportValue = rec.get('LisTestItem_ReportValue');
				list.push({
					Id:rec.get(me.PKField),
					ReportValue: ReportValue,
					LBItem: {
						Id: rec.get('LisTestItem_LBItem_Id'),
						CName: rec.get('LisTestItem_LBItem_CName'),
						Prec: rec.get('LisTestItem_LBItem_Prec'),
						DataTimeStamp: [0,0,0,0,0,0,0,0]
					}
				});
			}
		}
		if (list.length == 0) return;
		me.showMask(me.saveText);//显示遮罩层
		var url = JShell.System.Path.ROOT + me.editUrl;
		JShell.Server.post(url, Ext.JSON.encode({
			testFormID: lisTestFormID,
			listTestItemResult:list
		}),function(data){
			me.hideMask();//隐藏遮罩层
			if(data.success){
				JShell.Msg.alert("保存成功！",null,1000);
				me.fireEvent('aftersave',me);
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	},
	/**创建数据集*/
	createStore: function() {
		var me = this;
		return Ext.create('Ext.data.Store', {
			groupField:'LisTestItem_PLBItem_CName',//确定哪一项分组
			fields: me.getStoreFields(),
			pageSize: me.defaultPageSize,
			remoteSort: me.remoteSort,
			sorters: me.defaultOrderBy,
			proxy: {
				type: 'ajax',
				url: '',
				reader: {
					type: 'json',
					totalProperty: 'count',
					root: 'list'
				},
				extractResponseData: function (response) {
					var data = JShell.Server.toJson(response.responseText);
					if (data.success) {
						var info = data.value;
						if (info) {
							var type = Ext.typeOf(info);
							if (type == 'object') {
								info = info;
							} else if (type == 'array') {
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
		for(var i in data.list.length){
			//项目=组合项目
			if(data.list[i].LisTestItem_LBItem_CName == data.list[i].LisTestItem_PLBItem_CName){
				data.list[i].LisTestItem_PLBItem_CName = '';
				data.list[i].LisTestItem_PLBItem_DispOrder = 9999999;
			}
		}
		
		return data;
	},
	//清空数据
	clearData: function() {
		this.store.removeAll(); //清空数据
	},
	//获得结果状态颜色
	getResultStatusColor: function () {
		var me = this,
			url = (me.logicDelUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.getResultStatusColorUrl + "&where=IsUse=1 and DictType='" + me.ResultStatusDictType + "'";
		JShell.Server.get(url, function (res) {
			if (res.success) {
				me.ResultStatusList = res.value.list;
			} else {
				me.ResultStatusList = [];
			}
		});
	},
	//处理列表状态颜色
	setGridColumnsStyle: function (value, meta, record) {
		var me = this;
		var BAlarmColor = String(record.get("LisTestItem_BAlarmColor"));
		if (BAlarmColor == "true") {
			var fontColor = '#fff';
			if (record.get("LisTestItem_AlarmColor") == "#fff" || record.get("LisTestItem_AlarmColor") == "#ffffff") fontColor = '#000';
			meta.style = 'color:' + fontColor + ';background-color:' + record.get("LisTestItem_AlarmColor");
		} else {
			if (me.ResultStatusList.length > 0) {
				var ResultStatusCode = record.get("LisTestItem_ResultStatusCode");
				Ext.Array.each(me.ResultStatusList, function (str, index, arr) {
					if (ResultStatusCode == str["LBDict_DictCode"] && str["LBDict_ColorValue"]) {
						var fontColor = '#fff';
						if (str["LBDict_ColorValue"] == "#fff" || str["LBDict_ColorValue"] == "#ffffff") fontColor = '#000';
						meta.style = 'color:' + fontColor + ';background-color:' + str["LBDict_ColorValue"] + ';';
					}
				});
			}
		}
		return value;
	}
});