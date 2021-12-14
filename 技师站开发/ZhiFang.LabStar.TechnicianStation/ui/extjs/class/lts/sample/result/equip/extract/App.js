/**
 * 重新提取仪器结果
 * @author Jcall
 * @version 2020-03-22
 * @author zhangda
 * @version 2020-04-13
 */
Ext.define('Shell.class.lts.sample.result.equip.extract.App', {
	extend:'Shell.ux.panel.AppPanel',
	title:'重新提取仪器结果',
	
	//是否加载过数据
	hasLoaded: false,
	//检验单行数据
	testFormRecord:null,
	/**小组id*/
	sectionId: null,
	/*执行服务*/
	extractUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_AddLisItemResultByEquipResult',

	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		//左侧列表监听
		me.LeftGrid.on({
			select: function (model, record) {
				var EquipFormID = record.get(me.LeftGrid.PKField);
				if (EquipFormID) {
					me.RightGrid.externalWhere = "EquipFormID=" + EquipFormID;
					me.RightGrid.onSearch();
				}
			},
			nodata: function () {
				me.RightGrid.clearData();
			}
		});
	},
	initComponent:function(){
		var me = this;
		//创建挂靠功能栏
		me.dockedItems = me.createDockedItems();
		//创建内部组件
		me.items = me.createItems();
		me.callParent(arguments);
	},
	//创建挂靠功能栏
	createDockedItems: function () {
		var me = this;
		var dockedItems = [{
			xtype: 'uxButtontoolbar',
			dock: 'top',
			itemId: 'topToolbar',
			items: [{
				xtype: 'uxCheckTrigger', fieldLabel: '仪器', name: 'EquipName', itemId: 'EquipName',
				className: 'Shell.class.lts.sample.result.equip.extract.EquipCheckGrid',
				classConfig: {},
				labelWidth: 40, anchor: '90%', allowBlank: false, width: 200,
				emptyText: '必选项',
				listeners: {
					check: function (p, record) {
						me.getComponent('topToolbar').getComponent('EquipName').setValue(record ? record.get('LBEquip_CName') : '');
						me.getComponent('topToolbar').getComponent('EquipId').setValue(record ? record.get('LBEquip_Id') : '');
						p.close();
					}
				}
			}, {
					xtype: 'textfield',
					fieldLabel: '仪器编号',
					name: 'EquipId',
					itemId: 'EquipId',
					hidden: true,
					labelWidth: 80,
					anchor: '90%'
				}, {
					xtype: 'datefield',
					width: 200,
					labelWidth: 90,
					anchor: '100%',
					name: 'ETestDate',
					itemId: 'ETestDate',
					fieldLabel: '仪器检验日期',
					format: 'Y-m-d ',
					emptyText: '必填项',
					value: new Date()
				}, {
					xtype: 'textfield',
					fieldLabel: '',
					width:100,
					name: 'ESampleNo',
					itemId: 'ESampleNo',
					emptyText: '输入仪器样本号',
					anchor: '100%'
				}, '-', {
					xtype: 'button',
					text: '查询',
					iconCls: 'button-search',
					handler: function () {
						me.onSearchClick();
					}
				}, '-', {
					xtype: 'button',
					text: '执行',
					iconCls: 'button-import',
					handler: function () {
						me.onExtract();
					}
				}
			]
		}, {
				xtype: 'uxButtontoolbar',
				dock: 'bottom',
				itemId: 'bottomToolbar',
				items: [
					{ xtype: 'checkbox', boxLabel: '检验单中仪器自增项目删除', itemId: 'IncrementDelete', name: 'IncrementDelete', checked: true,margin:'0 30 0 0' },
					{ xtype: 'checkbox', boxLabel: '检验单样本号变更', itemId: 'SampleNoChange', name: 'SampleNoChange', checked: true }
				]
			}];
		return dockedItems;
	},
	//创建内部组件
	createItems: function () {
		var me = this;
		me.LeftGrid = Ext.create('Shell.class.lts.sample.result.equip.extract.LeftGrid', {
			region: 'center', itemId: 'LeftGrid', width: 400,
			header: true, border: false,
			autoScroll: true, split: true,
			collapsible: false, animCollapse: false
		});
		me.RightGrid = Ext.create('Shell.class.lts.sample.result.equip.extract.RightGrid', {
			region: 'east', itemId: 'RightGrid', width: 430,
			header: true, border: false,
			autoScroll: true, split: true,
			collapsible: false,animCollapse: false
		});

		return [me.LeftGrid, me.RightGrid];
	},
	//查询数据
	onSearch:function(record){
		var me = this;
		me.testFormRecord = record;
		if (me.hasLoaded) return;
		if(!me.hasLoaded){
			me.hasLoaded = true;
		}
		//设置下拉列表仪器 查询
		var picker = me.getComponent("topToolbar").getComponent("EquipName").getPicker();
		if (me.sectionId)
			picker.externalWhere = 'sectionId=' + me.sectionId;
		else
			picker.externalWhere = 'sectionId=-1';
		//查询前清空之前数据
		me.clearData(true);
		me.getComponent('topToolbar').getComponent('EquipName').onTriggerClick();
		//picker.onSearch();
		//if (me.testFormId != record.get("LisTestForm_Id")) {
		//	//相关数据变化
		//	me.testFormId = record.get("LisTestForm_Id");
		//	me.sectionId = record.get("LisTestForm_LBSection_Id");
		//	//设置下拉列表仪器 查询
		//	var picker = me.getComponent("topToolbar").getComponent("EquipName").getPicker();
		//	if (me.sectionId)
		//		picker.externalWhere = 'sectionId=' + me.sectionId;
		//	else
		//		picker.externalWhere = 'sectionId=-1';
		//	//查询前清空之前数据
		//	me.clearData(true);
		//	picker.onSearch();
		//}
	},
	//查询按钮点击事件
	onSearchClick: function () {
		var me = this,
			where = '1=1',
			EquipId = me.getComponent("topToolbar").getComponent("EquipId").getValue(),
			ETestDate = me.getComponent("topToolbar").getComponent("ETestDate").getValue(),
			ESampleNo = me.getComponent("topToolbar").getComponent("ESampleNo").getValue();
		
		//判断仪器是否存在
		if (!EquipId) {
			JShell.Msg.warning('请选择仪器!', null, 1000);
			return;
		}
		//判断日期是否有效
		if (!(ETestDate instanceof Date)) {
			JShell.Msg.warning('请输入正确格式的日期!', null, 1000);
			return;
		}
		where += " and EquipID=" + EquipId;
		where += " and ETestDate>='" + JcallShell.Date.toString(ETestDate) + "' and ETestDate<='" + JcallShell.Date.toString(ETestDate, true) + " 23:59:59'";
		if (ESampleNo) where += " and ESampleNo=" + ESampleNo;
		//左侧列表查询
		me.LeftGrid.externalWhere = where;
		me.LeftGrid.onSearch();
	},
	//执行重新提取操作
	onExtract: function () {
		var me = this,
			msg = [],
			url = JShell.System.Path.ROOT + me.extractUrl,
			testFormId = me.testFormRecord.get("LisTestForm_Id"),//检验单Id
			LisTestForm_GSampleNo = me.testFormRecord.get("LisTestForm_GSampleNo"),//检验单样本号
			LisTestForm_GTestDate = me.testFormRecord.get("LisTestForm_GTestDate"),//检验单时间
			equipFormID = null,//仪器检验单Id long类型
			equipItemIDList = [],//仪器检验项目单id string类型
			LeftGridCheckRow = me.LeftGrid.getSelectionModel().getSelection(),//仪器样本单选中数据
			RightGridCheckRow = me.RightGrid.getSelectionModel().getSelection(),//仪器样本项目选中数据
			isChangeSampleNo = me.getComponent("bottomToolbar").getComponent("SampleNoChange").getValue(),//是否改变检验样本单样本号
			changeTestFormID = true,//是否改变仪器样本单对应的检验样本单
			isDelAuotAddItem = me.getComponent("bottomToolbar").getComponent("IncrementDelete").getValue();//是否删除检验单中仪器自增项目

		if (!testFormId) {
			JShell.Msg.alert('未选择检验单!', null, 1000);
			return;
		}
		if (LeftGridCheckRow.length == 0) {
			JShell.Msg.alert('未选择仪器样本单!', null, 1000);
			return;
		}
		if (RightGridCheckRow.length == 0) {
			JShell.Msg.alert('未选择仪器项目!', null, 1000);
			return;
		}
		equipFormID = LeftGridCheckRow[0].get("LisEquipForm_Id");
		Ext.Array.each(RightGridCheckRow, function (str, index, arr) {
			equipItemIDList.push(str.get("LisEquipItem_Id"));
		});
		if (!equipFormID || equipItemIDList.length <= 0) return;
		//不是同一天，同一个样本号，提示
		if (LisTestForm_GSampleNo != LeftGridCheckRow[0].get("LisEquipForm_ESampleNo") || LisTestForm_GTestDate != LeftGridCheckRow[0].get("LisEquipForm_ETestDate")) {
			msg.push("对应样本 日期：" + LisTestForm_GTestDate + ",样本号：" + LisTestForm_GSampleNo);
			msg.push("对应仪器样本 日期：" + LeftGridCheckRow[0].get("LisEquipForm_ETestDate") + ",样本号：" + LeftGridCheckRow[0].get("LisEquipForm_ESampleNo"));
			msg.push("日期不一致，或者样本号不一致，确定要重新提取吗？");
		}
		if (msg.length == 0) msg.push("确定要重新提取吗？");
		JShell.Msg.confirm({ msg: msg.join("<br/>") }, function (but) {
			if (but != "ok") return;
			JShell.Server.post(url, Ext.JSON.encode({ testFormID: testFormId, equipFormID: equipFormID, equipItemID: '', isChangeSampleNo: isChangeSampleNo, changeTestFormID: changeTestFormID, isDelAuotAddItem: isDelAuotAddItem }), function (res) {
				if (res.success) {
					JShell.Msg.alert('执行成功!', null, 1000);
					me.fireEvent('updateTestFormRecord', me, testFormId);//更新检验单选中行数据
				} else {
					JShell.Msg.error("执行失败，失败信息：" + res.msg);
					return;
				}
			});
		});
	},
	//清空数据信息
	clearData: function (isClear) {
		var me = this,
			isClear = isClear || false;
		if (!isClear) return;
		me.getComponent("topToolbar").getComponent("EquipId").setValue('');
		me.getComponent("topToolbar").getComponent("EquipName").setValue('');
		me.LeftGrid.clearData();
		me.RightGrid.clearData();
	}
});