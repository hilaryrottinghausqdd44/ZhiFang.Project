/**
 * 退供应商出库管理出库总单
 * @author liangyl
 * @version 2017-12-14
 */
Ext.define('Shell.class.rea.client.out.retreatsuppliers.DocGrid', {
	extend: 'Shell.class.rea.client.out.basic.DocGrid',

	/**用户UI配置Key*/
	userUIKey: 'out.retreatsuppliers.DocGrid',
	/**用户UI配置Name*/
	userUIName: "退供应商出库列表",
	/**出库类型*/
	defaluteOutType: '4',
	/**库存新增仪器是否允许为空,1是,0否*/
	IsEquip: '0',
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.onSetDateArea(-1);
	},
	initComponent: function() {
		var me = this;
		//初始化参数
		me.initOutParams();
		//创建功能按钮栏Items
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	//初始化参数
	initOutParams: function() {
		var me = this;
		me.initRunParams();
		me.changeType();
		var isUseEmpOut = me.IsEmpOut ? 1 : 2;
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
		me.selectUrl += '&empId=' + userId + '&type=' + me.typeByHQL + '&isUseEmpOut=' + isUseEmpOut;
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.callParent(arguments);
		if(!items) items = [];
		items.push(me.createPrintButtonToolbarItems());
		return items;
	},
	/**新增*/
	onAddClick: function() {
		var me = this;
		var takerObj = {};
		if(me.OutboundIsLogin) {
			if(me.OutboundIsLogin == "2") {
				takerObj = me.getAddDelfaultValue();
			}
			me.openAddPanel(takerObj);
		} else {
			me.getOutTakerParaVal(function(val) {
				if(me.OutboundIsLogin == "2") {
					takerObj = me.getAddDelfaultValue();
				}
				me.openAddPanel(takerObj);
			});
		}
	},
	getAddDelfaultValue: function() {
		var me = this;
		var takerObj = {};
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || "";
		var userName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME) || "";
		var deptId = JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTID) || "";
		var deptName = JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTNAME) || "";
		takerObj.TakerId = userId;
		takerObj.TakerName = userName;
		takerObj.DeptId = deptId;
		takerObj.DeptName = deptName;
		return takerObj;
	},
	/**显示表单*/
	openAddPanel: function(takerObj) {
		var me = this;
		var maxWidth = document.body.clientWidth * 0.99;
		var height = document.body.clientHeight * 0.98;
		var config = {
			resizable: false,
			height: height,
			width: maxWidth,
			SUB_WIN_NO: '1',
			/**库存新增仪器是否允许为空,1是,0否*/
			IsEquip: me.IsEquip,
			/**按领用人权限出库,true 是,false否*/
			IsEmpOut: me.IsEmpOut,
			TakerObj: takerObj,
			formtype: 'add',
			/**出库扫码模式(严格模式:1,混合模式：2)*/
			OutScanCodeModel: me.OutScanCodeModel,
			listeners: {
				save: function(p, records) {
					JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, 1000);
					p.close();
					me.onSearch();
				}
			}
		};
		//完成出库时是否需要出库确认
		JcallShell.REA.RunParams.getRunParamsValue("ReaBmsOutDocRefundSIsCheck", false, function(data) {
			var paraValue = "0";
			if(data.success) {
				var obj = data.value;
				if(obj.ParaValue) paraValue = obj.ParaValue;
			}
			config.IsCheck = paraValue;
			JShell.Win.open('Shell.class.rea.client.out.retreatsuppliers.AddPanel', config).show();
		});
	},
	//根据类型，赋值
	changeType: function() {
		var me = this;
		me.typeByHQL = '2';
	},
	removeSomeStatusList: function() {
		var me = this;
		var tempList = JShell.JSON.decode(JShell.JSON.encode(JShell.REA.StatusList.Status[me.ReaBmsOutDocStatus].List));
		var removeArr = [];
		//只保留全部和出库完成
		if(tempList[7]) removeArr.push(tempList[7]);
		if(tempList[6]) removeArr.push(tempList[6]);
		if(tempList[5]) removeArr.push(tempList[5]);
		if(tempList[4]) removeArr.push(tempList[4]);
		if(tempList[3]) removeArr.push(tempList[3]);
		if(tempList[2]) removeArr.push(tempList[2]);
		if(tempList[1]) removeArr.push(tempList[1]);
		Ext.Array.each(removeArr, function(name, index, countriesItSelf) {
			Ext.Array.remove(tempList, removeArr[index]);
		});
		me.searchStatusValue = tempList;
		return tempList;
	}
});